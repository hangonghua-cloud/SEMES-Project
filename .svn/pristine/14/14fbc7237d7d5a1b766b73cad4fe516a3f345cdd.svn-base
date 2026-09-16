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

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = angular.copy($stateParams.selectedItem);
            self.currentItem = null;
            self.validInputs = false;
            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }
        function initDictionary() {
            //初始化 物料分类
            self.typeDepartment = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddDetailctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddDetailctrl.Tips_2'), ItemValue: "" }]
            };
            self.typeData = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddDetailctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddDetailctrl.Tips_2'), ItemValue: "" }]
            };


            self.typeEnabled = {
                value: { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddDetailctrl.Tips_3') },
                options: [
                    { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddDetailctrl.Tips_3') },
                    { ItemValue: false, ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddDetailctrl.Tips_4') }
                ]
            };
            commonService.getDataItemDuatil("AssayDepartment").then(function (res) {
                if (res && res.data.success) {
                    self.typeDepartment.options = res.data.resultData;
                    self.typeDepartment.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("DataType").then(function (res) {
                if (res && res.data.success) {
                    self.typeData.options = res.data.resultData;
                    self.typeData.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddDetailctrl.Tips_5') });

            self.currentItem.TestDepartment = self.typeDepartment.value.ItemValue;
            self.currentItem.DataType = self.typeData.value.ItemValue;
            self.currentItem.DataTypeName = self.typeData.value.ItemName;
            self.currentItem.IsEnabled = self.typeEnabled.value.ItemValue;
            self.currentItem.TestMaintenanceId = self.selectedItem.Id;

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };


            var url = commonService.getMesApiAddress("quality") + 'QC_TestItemMaintenance/SaveQC_TestItemMaintenance';
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
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddDetailctrl.Tips_6'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddDetailctrl.Tips_7'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddDetailctrl.Tips_7'));
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
            name: screenStateName + '.addDetail',
            url: '/addDetail',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/TestMaintenance-addDetail.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddDetailctrl.Tips_8'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
