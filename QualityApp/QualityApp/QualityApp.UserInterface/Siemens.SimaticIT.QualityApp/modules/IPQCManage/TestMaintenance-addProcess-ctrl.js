(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.IPQCManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenance.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddProcessctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            self.selectedItem = angular.copy($stateParams.selectedItem);
            //Initialize Model Data
            self.currentItem = {};
            self.currentItem.FactoryCode = self.selectedItem.FactoryCode;
            self.currentItem.FactoryName = self.selectedItem.FactoryName;
            self.validInputs = false;
            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }
        function initDictionary() {

            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddProcessctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddProcessctrl.Tips_2'), ResourceCode: "" }]
            };

            // commonService.getResourceExtendInfo({ LevelCode: "Process" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeProcess.options = res.data.resultData;
            //         self.typeProcess.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddProcessctrl.Tips_2')
            //         });
            //     }
            // });
            commonService.getProcessByFactory({ LevelCode: self.currentItem.FactoryCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeProcess.options = res.data.resultData;
                    self.typeProcess.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddProcessctrl.Tips_2')
                    });
                }
            });
        }
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {


            self.currentItem.ProcessCode = self.typeProcess.value.ResourceCode;
            self.currentItem.ProcessName = self.typeProcess.value.ResourceName;
            self.currentItem.TestMaintenanceId = self.selectedItem.Id;

            var postData = {
                KeyValue: '',
                Entity: self.currentItem
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddProcessctrl.Tips_3') });
            var url = commonService.getMesApiAddress("quality") + 'QC_TestProcessMaintenance/SaveQC_TestProcessMaintenance';
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
            debugger;
            //console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                // sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddProcessctrl.Tips_4'));
                //刷新局部
                $rootScope.$emit('to-parentProcess', 'parent');
                // $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddProcessctrl.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddProcessctrl.Tips_5'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_IPQCManage_TestMaintenance';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/IPQCManage';

        var state = {
            name: screenStateName + '.addProcess',
            url: '/addProcess',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/TestMaintenance-addProcess.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddProcessctrl.Tips_6'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
