(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.AbrasiveOrder').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AbrasiveOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //初始化前端变量数据
            self.mainCurrentItem = angular.copy($stateParams.mainSelectedItem);
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
            initDictionary();
            self.PTeamChange = PTeamChange;
            self.selectClick = selectClick;
        }

        function initDictionary() {
            self.currentItem.WorkOrder = self.mainCurrentItem.WorkOrder;
            self.currentItem.SmallClassName = self.mainCurrentItem.SmallClassName;
            self.currentItem.MaterialCode = self.mainCurrentItem.MaterialCode;
            self.currentItem.MaterialName = self.mainCurrentItem.MaterialName;
            self.currentItem.Spec = self.mainCurrentItem.Spec;

            self.PTeam = {
                value: { PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_2'), PTeamCode: "" },
                options: [{ PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_2'), PTeamCode: "" }]
            };
            self.Machine = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_2'), ResourceCode: "" }]
            };
            self.Shift = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_2'), ItemValue: "" }]
            };

            //生产小组
            var url = commonService.getMesApiAddress('ProduceManage') + 'PM_TeamPerson/GetPM_TeamPersonList?ProcessCode=' + self.currentItem.ProcessCode;
            commonService.callWebApiGet(url).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.PTeam.options = res.data.resultData;
                    self.PTeam.options.splice(0, 0, {
                        PTeamCode: "",
                        PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_2')
                    });
                    self.PTeam.value = self.PTeam.options.find(t => t.PTeamCode == self.currentItem.UserGroup);
                }
            });
            //机台
            commonService.getResourceListByParentResource({ ParentResource: self.currentItem.ProcessCode }).then(function (res) {
                if (res && res.data.success) {
                    self.Machine.options = res.data.resultData;
                    self.Machine.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_2')
                    });
                    self.Machine.value = self.Machine.options.find(t => t.ResourceCode == self.currentItem.Machine);
                }
            });
            //班次
            commonService.getDataItemDuatil("Shift").then(function (res) {
                if (res && res.data.success) {
                    self.Shift.options = res.data.resultData;
                    self.Shift.options.splice(0, 0, {
                        ItemValue: "",
                        ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_2')
                    });
                    self.Shift.value = self.Shift.options.find(t => t.ItemValue == self.currentItem.Shift);
                }
            })
        }

        function PTeamChange(oldItem, newItem) {

            //人员
            if (newItem.PTeamCode) {
                var url = commonService.getMesApiAddress('ProduceManage') + 'PM_TeamPerson_Items/GetPM_TeamPerson_ItemsList?pTeamCode=' + newItem.PTeamCode;
                commonService.callWebApiGet(url).then(function (res) {
                    if ((res) && (res.data.success)) {
                        let arrNew = res.data.resultData.map(item => {
                            return item.UserName;
                        })
                        self.currentItem.UserNames = arrNew.join(',');
                    }
                    else {
                        self.currentItem.UserNames = "";
                    }
                });
            }
            else {
                self.currentItem.UserNames = "";
            }
        }
        //选择人员
        function selectClick() {
            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress() + 'Base/GetListUser',
                            method: "Post",
                            queryParmeters: {
                                Name: "",
                            },
                            pagination: {
                                rows: 80,//每页显示条数
                                page: 1,//页码
                            },
                            multiple: true,
                            sidx: "Code",
                            sord: "asc",
                            columnDefs: [
                                {
                                    field: 'Code',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_3'),
                                    width: 200
                                },
                                {
                                    field: 'Name',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_4'),
                                    width: 200
                                },
                                {
                                    field: 'DeptName',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_5'),
                                    width: 300
                                },
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                self.currentItem.UserCode = "";
                self.currentItem.UserNames = "";
                data.forEach(item => {
                    self.currentItem.UserCode += item.Code + ",";
                    self.currentItem.UserNames += item.Name + ",";
                })
                self.currentItem.UserNames = self.currentItem.UserNames.substring(0, self.currentItem.UserNames.length - 1);
            });
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_6') });

            //self.currentItem.PTeamCode = self.PTeam.value.PTeamCode;
            self.currentItem.Machine = self.Machine.value.ResourceCode;
            self.currentItem.Shift = self.Shift.value.ItemValue;
            self.currentItem.BGDate = commonService.ConvertToLocalDate(self.currentItem.BGDate);
            self.currentItem.ProcessCode = self.mainCurrentItem.ProcessCode;

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_AbrasiveBG/SavePM_AbrasiveBG';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_7'));
                //刷新局部
                $rootScope.$emit('to-parent', 'edit');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_8'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_8'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_AbrasiveOrder_AbrasiveOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/AbrasiveOrder';

        var state = {
            name: screenStateName + '.editbg',
            url: '/editbg/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/AbrasiveOrder-editbg.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.AbrasiveOrder.EditBGJS.Tips_1',
            },
            params: {
                mainSelectedItem: null,
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
