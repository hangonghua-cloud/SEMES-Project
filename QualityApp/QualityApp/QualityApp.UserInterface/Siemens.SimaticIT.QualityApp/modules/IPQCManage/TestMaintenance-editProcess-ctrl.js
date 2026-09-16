(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.IPQCManage').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenance.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceeditProcessctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            console.log(commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceeditProcessctrl.Tips_2'), self.currentItem);
            self.validInputs = false;
            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }
        function initDictionary() {

            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceeditProcessctrl.Tips_3'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceeditProcessctrl.Tips_3'), ResourceCode: "" }]
            };

            // commonService.getResourceExtendInfo({ LevelCode: "Process" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeProcess.options = res.data.resultData;
            //         self.typeProcess.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceeditProcessctrl.Tips_3')
            //         });
            //         self.typeProcess.value = self.typeProcess.options.find(t => t.ResourceCode == self.currentItem.ProcessCode);
            //     }
            // });
            commonService.getProcessByFactory({ LevelCode: self.currentItem.FactoryCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeProcess.options = res.data.resultData;
                    self.typeProcess.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceeditProcessctrl.Tips_3')
                    });
                    self.typeProcess.value = self.typeProcess.options.find(t => t.ResourceCode == self.currentItem.ProcessCode);
                }
            });
        }
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceeditProcessctrl.Tips_4') });

            self.currentItem.ProcessCode = self.typeProcess.value.ResourceCode;
            self.currentItem.ProcessName = self.typeProcess.value.ResourceName;


            var postData = {
                KeyValue: self.currentItem.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };


            var url = commonService.getMesApiAddress("quality") + 'QC_TestProcessMaintenance/SaveQC_TestProcessMaintenance';

            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

            busyIndicatorService.hide();
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceeditProcessctrl.Tips_5'));
                //刷新局部
                $rootScope.$emit('to-parentProcess', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceeditProcessctrl.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceeditProcessctrl.Tips_6'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_IPQCManage_TestMaintenance';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/IPQCManage';

        var state = {
            name: screenStateName + '.editProcess',
            url: '/editProcess/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/TestMaintenance-editProcess.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceeditProcessctrl.Tips_7'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
