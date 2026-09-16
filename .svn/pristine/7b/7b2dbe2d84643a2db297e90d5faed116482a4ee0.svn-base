(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PrinterOrder').config(ViewScreenStateConfig);

    ViewScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PrinterOrder.PrinterOrder.service', '$state', '$stateParams', 'common.base', '$filter', '$scope', 'commonService'];
    function ViewScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.selectJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = $stateParams.selectedItem;


            self.OrderDate = new Date(self.currentItem.OrderDate);
            self.DeliveryDate = new Date(self.currentItem.DeliveryDate);
            //Expose Model Methods
            self.cancel = cancel;
            self.FactoryChange = FactoryChange;
            initDictionary();
        }
        function initDictionary() {
            self.Factory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.selectJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.selectJS.Tips_2'), ResourceCode: "" }]
            };
            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.selectJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.selectJS.Tips_2'), ResourceCode: "" }]
            };


            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.selectJS.Tips_2')
                    });
                }
                self.Factory.value = self.Factory.options.find(t => t.ResourceCode == self.currentItem.FactoryCode);
                FactoryChange(null, { ResourceCode: self.currentItem.FactoryCode });
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
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.selectJS.Tips_2')
                    });
                    self.Process.value = self.Process.options.find(t => t.ResourceCode == self.currentItem.ProcessCode);
                }
            });
        }



        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            sidePanelManager.close();
            $state.go('^', {}, { reload: true });
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    ViewScreenStateConfig.$inject = ['$stateProvider'];
    function ViewScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_PrinterOrder_PrinterOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PrinterOrder';

        var state = {
            name: screenStateName + '.select',
            url: '/select/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PrinterOrder-select.html',
                    controller: ViewScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PrinterOrder.selectJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
