(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.OwnProduct').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.OwnProduct.OwnProductBG.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            initDictionary();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.PTeamChange = PTeamChange;
        }

        function initDictionary() {
            self.PTeam = {
                value: { PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_2'), PTeamCode: "" },
                options: [{ PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_2'), PTeamCode: "" }]
            };
            self.User = {
                value: { UserName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_2'), UserCode: "" },
                options: [{ UserName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_2'), UserCode: "" }]
            };
            self.Post = {
                value: { Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_2'), Col1: "" },
                options: [{ Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_2'), Col1: "" }]
            };
            //生产小组
            var url = commonService.getMesApiAddress('ProduceManage') + 'PM_TeamPerson/GetPM_TeamPersonList?ProcessCode=' + self.currentItem.ProcessCode;
            commonService.callWebApiGet(url).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.PTeam.options = res.data.resultData;
                    self.PTeam.options.splice(0, 0, {
                        PTeamCode: "",
                        PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_2')
                    });
                }
            });

            //岗位
            commonService.getKeyParameterItem({ ItemCode: self.currentItem.ProcessCode, EnCode: "ProcessPost" }).then(function (res) {
                if (res && res.data.success) {
                    self.Post.options = res.data.resultData;
                    self.Post.options.splice(0, 0, {
                        Col1: "",
                        Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_2')
                    });
                }
            });

        }

        function PTeamChange(oldItem, newItem) {
            //人员
            var url = commonService.getMesApiAddress('ProduceManage') + 'PM_TeamPerson_Items/GetPM_TeamPerson_ItemsList?pTeamCode=' + newItem.PTeamCode;
            commonService.callWebApiGet(url).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.User.options = res.data.resultData;
                    self.User.options.splice(0, 0, {
                        UserCode: "",
                        UserName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_2')
                    });
                }
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {

            self.currentItem.PTeamCode = self.PTeam.value.PTeamCode;
            self.currentItem.PteamName = self.PTeam.value.PTeamName;
            self.currentItem.PostCode = self.Post.value.Col1;
            self.currentItem.PostName = self.Post.value.Col2;
            self.currentItem.UserCode = self.User.value.UserCode;
            self.currentItem.UserName = self.User.value.UserName;
            self.currentItem.BGID = self.currentItem.Id;
            self.currentItem.Id = "";

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_3') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_TransferBGPersonRecord/SavePM_TransferBGPersonRecord';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

        }

        //取消
        function cancel() {
            //关闭侧边栏
            sidePanelManager.close();
            //返回列表(父页面)
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {

            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_4'));
                //刷新局部
                $rootScope.$emit('to-parentPerson', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_5'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_OwnProduct_OwnProductBG';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/OwnProduct';

        var state = {
            name: screenStateName + '.addUser',
            url: '/addUser',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/OwnProductBG-addUser.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.OwnProductBG.adduserJS.Tips_6'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
