(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW').config(ViewScreenStateConfig);

    ViewScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.ExeWorkOrderSW.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function ViewScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        activate();
        function activate() {
            init();

            sidePanelManager.setTitle('查看');
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = {};
            self.WorkOrder = $stateParams.selectedItem.WorkOrder;
            getExeWorkOrderInfo();

            //Expose Model Methods
            self.cancel = cancel;
        }

        function getExeWorkOrderInfo() {
            let data = {
                workOrder: self.WorkOrder
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ExeWorkOrderSW/GetExeWorkOrderInfo';
            //var url = 'http://localhost:49849/' + 'ProductRule' + '/PL_ProductRulePageDataTableList'; 
            busyIndicatorService.show({ message: "加载中，请稍后……" });
            commonService.callWebApiPost(url, data).then(function (res) {
                //console.log ('-self.Post_ResultData----------------------' + JSON.stringify(res));
                busyIndicatorService.hide();
                if ((res) && (res.data.success) && (res.data.resultData)) {
                    self.currentItem = res.data.resultData[0];
                    console.log("dd" + JSON.stringify(self.currentItem));
                } else {
                    self.currentItem = {};
                }
            }, function (error) {
                busyIndicatorService.hide();
                backendService.genericError('获取数据出错', "获取数据出错");
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
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_ExeWorkOrderSW_ExeWorkOrderSW';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/ExeWorkOrderSW';

        var state = {
            name: screenStateName + '.select',
            url: '/select/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ExeWorkOrderSW-select.html',
                    controller: ViewScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: '查看'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
