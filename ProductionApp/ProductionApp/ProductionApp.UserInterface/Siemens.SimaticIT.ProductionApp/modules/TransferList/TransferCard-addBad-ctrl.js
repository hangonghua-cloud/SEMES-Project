(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.TransferList').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.TransferList.TransferCard.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.addBadJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.mainCurrentItem = angular.copy($stateParams.selectedItem);
            self.currentItem = {};
            self.validInputs = false;

            initDictionary();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function initDictionary() {
            self.BadItem = {
                value: { BadItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.addBadJS.Tips_2'), BadItemCode: "" },
                options: [{ BadItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.addBadJS.Tips_2'), BadItemCode: "" }]
            };

            //不良项目
            var url = commonService.getMesApiAddress('ProduceManage') + 'PM_ProcessBadItem/GetPM_ProcessBadItemList?processCode=' + self.mainCurrentItem.ProcessCode;
            commonService.callWebApiGet(url).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.BadItem.options = res.data.resultData;
                    self.BadItem.options.splice(0, 0, {
                        BadItemCode: "",
                        BadItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.addBadJS.Tips_2')
                    });
                }
            });

        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.addBadJS.Tips_3') });
            //字典类型 取值参考

            self.currentItem.BadItemCode = self.BadItem.value.BadItemCode;
            self.currentItem.BadItemName = self.BadItem.value.BadItemName;
            self.currentItem.BGID = self.mainCurrentItem.Id;

            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            self.mainCurrentItem.BadQty = self.mainCurrentItem.BadQty + self.currentItem.BadQty;

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_BGBadRecord/SavePM_BGBadRecord';

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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.addBadJS.Tips_4'));
                //刷新局部
                $rootScope.$emit('to-parentBadItem', self.mainCurrentItem);
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.addBadJS.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.addBadJS.Tips_5'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_TransferList_TransferCard';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/TransferList';

        var state = {
            name: screenStateName + '.addBad',
            url: '/addBad',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/TransferCard-addBad.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.TransferList.addBadJS.Tips_6'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
