(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PrinterOrder').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PrinterOrder.PrinterOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addJS.Tips_1'));
            sidePanelManager.open("e");
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.OrderDate = new Date();
            self.currentItem = {};
            self.validInputs = false;
            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.FactoryChange = FactoryChange;
        }
        function initDictionary() {
            self.Factory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addJS.Tips_2'), ResourceCode: "" }]
            };
            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addJS.Tips_2'), ResourceCode: "" }]
            };


            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addJS.Tips_2')
                    });
                }
            });
            // commonService.getDataItemDuatil("WorkOrderType").then(function (res) {
            //     if (res && res.data.success) {
            //         self.WorkOrderType.options = res.data.resultData;
            //         self.WorkOrderType.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
            //     }
            // })
        }

        function FactoryChange(oldItem, newItem) {
            commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.Process.options = res.data.resultData;
                    self.Process.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addJS.Tips_2')
                    });
                }
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addJS.Tips_3') });

            self.currentItem.FactoryCode = self.Factory.value.ResourceCode;
            self.currentItem.ProcessCode = self.Process.value.ResourceCode;
            self.currentItem.orderType = "1";

            self.currentItem.OrderDate = commonService.ConvertToLocalTime(self.OrderDate);
            self.currentItem.DeliveryDate = commonService.ConvertToLocalTime(self.DeliveryDate);

            var postData = {
                KeyValue: '',
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_PrinterOrder/SavePM_PrinterOrder';

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
            //console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addJS.Tips_5'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addJS.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addJS.Tips_6'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_PrinterOrder_PrinterOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PrinterOrder';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PrinterOrder-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PrinterOrder.addJS.Tips_7'
            }
        };
        $stateProvider.state(state);
    }
}());
