(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderDismantle').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderDismantle.WorkOrderDismantle.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope,
        commonService, auth, notificationService, busyIndicatorService, $modal, $interval) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.TotalPieces = angular.copy(self.currentItem.TotalPieces);

            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }
        function initDictionary() {

            self.typeProcessOperation = {
                value: { ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_2'), ProcessCode: "" },
                options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_2'), ProcessCode: "" }]
            };
            self.typeStartProcess = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_2'), ItemValue: "" }]
            };
            self.typeWorkOrder = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_2'), ItemValue: "" }]
            }

            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_2'), ResourceCode: "" }]
            };
            commonService.getDataItemDuatil("WorkOrderType").then(function (res) {
                if (res && res.data.success) {
                    self.typeWorkOrder.options = res.data.resultData;
                    self.typeWorkOrder.value = self.typeWorkOrder.options.find(t => t.ItemValue == self.currentItem.ExeOrderType);
                }
            })

            commonService.getResourceExtendInfo({ LevelCode: "Process" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeStartProcess.options = res.data.resultData;
                    self.typeStartProcess.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_2')
                    });
                }
                self.typeStartProcess.value = self.typeStartProcess.options.find(t => t.ResourceCode == self.currentItem.StartOperation);
            });

            var url = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=";
            commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.typeProcessOperation.options = res.data.resultData;
                    self.typeProcessOperation.options.splice('0', '0', {
                        ProcessCode: "",
                        ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_2')
                    });
                    self.typeProcessOperation.value = self.typeProcessOperation.options.find(t => t.ProcessCode == self.currentItem.Process);
                }
            })
        }
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
           
            //字典类型 取值参考
            if (self.currentItem.TotalPieces < 0 || self.currentItem.TotalPieces >= self.TotalPieces) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_3') + self.TotalPieces, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_4'));
                return false;
            }
            self.currentItem.Process = self.typeProcessOperation.value.ProcessCode;
            self.currentItem.StartOperation = self.typeStartProcess.value.ResourceCode;

            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem,
                HistoryNum: self.TotalPieces
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_5') });
            var url = commonService.getMesApiAddress("plan") + 'PL_PlanStoreIssue/Update_SuperProduct';

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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_6'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_4'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_4'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderDismantle_WorkOrderDismantle';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderDismantle';

        var state = {
            name: screenStateName + '.editDetail',
            url: '/editDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrderDismantle-editDetail.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editDetailJS.Tips_7'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
