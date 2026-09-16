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

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addDetailJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = angular.copy($stateParams.selectedItem);
            self.currentItem = {};
            self.validInputs = false;
            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function initDictionary() {
            self.ProcessRoute = {
                value: { ProcessName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addDetailJS.Tips_2'), ProcessCode: "" },
                options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addDetailJS.Tips_2'), ProcessCode: "" }]
            };
            var url1 = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=";
            commonService.callWebApiGet(url1, null).then(function (res) {
                if (res && res.data.success) {
                    self.ProcessRoute.options = res.data.resultData;
                    self.ProcessRoute.options.splice('0', '0', {
                        ProcessCode: "",
                        ProcessName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addDetailJS.Tips_2')
                    });
                }
            })
        }

        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addDetailJS.Tips_3') });
            //字典类型 取值参考
            self.currentItem.ProcessRoute = self.ProcessRoute.value.ProcessCode;
            self.currentItem.PrinterOrder = self.selectedItem.PrinterOrder;
            self.currentItem.WorkOrderType = "1";

            var postData = {
                KeyValue: '',
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_PrinterWorkOrder/SavePM_PrinterWorkOrder';
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
                //sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addDetailJS.Tips_4'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', { gridId: self.selectedItem.Id, GridDetailId: null });
                // $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addDetailJS.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.addDetailJS.Tips_5'));
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
            name: screenStateName + '.addDetail',
            url: '/addDetail',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PrinterOrder-addDetail.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PrinterOrder.addDetailJS.Tips_6'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
