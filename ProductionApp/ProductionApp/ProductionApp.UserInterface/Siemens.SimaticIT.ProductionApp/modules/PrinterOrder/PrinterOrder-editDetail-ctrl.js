(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PrinterOrder').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PrinterOrder.PrinterOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editDetailJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
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
                value: { ProcessName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editDetailJS.Tips_2'), ProcessCode: "" },
                options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editDetailJS.Tips_2'), ProcessCode: "" }]
            };
            var url1 = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=";
            commonService.callWebApiGet(url1, null).then(function (res) {
                if (res && res.data.success) {
                    self.ProcessRoute.options = res.data.resultData;
                    self.ProcessRoute.options.splice('0', '0', {
                        ProcessCode: "",
                        ProcessName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editDetailJS.Tips_2')
                    });
                    self.ProcessRoute.value = self.ProcessRoute.options.find(t => t.ProcessCode == self.currentItem.ProcessRoute);
                }
            })
        }
        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editDetailJS.Tips_3') });

            self.currentItem.ProcessRoute = self.ProcessRoute.value.ProcessCode;


            var postData = {
                KeyValue: self.currentItem.Id,
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
            //console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                //sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editDetailJS.Tips_5'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', { gridId: self.currentItem.gridId, GridDetailId: self.currentItem.Id });
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editDetailJS.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editDetailJS.Tips_6'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_PrinterOrder_PrinterOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PrinterOrder';

        var state = {
            name: screenStateName + '.editDetail',
            url: '/editDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PrinterOrder-editDetail.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PrinterOrder.editDetailJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
