(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.OQCManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfig.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = angular.copy($stateParams.selectedItem);
            self.currentItem = null;
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            initDictionary();
        }
        function initDictionary() {
            //初始化 物料分类
            self.typeDepartment = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddDetailctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddDetailctrl.Tips_2'), ItemValue: "" }]
            };
            self.typeData = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddDetailctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddDetailctrl.Tips_2'), ItemValue: "" }]
            };


            self.typeEnabled = {
                value: { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddDetailctrl.Tips_3') },
                options: [
                    { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddDetailctrl.Tips_3') },
                    { ItemValue: false, ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddDetailctrl.Tips_4') }
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
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddDetailctrl.Tips_5') });
            //字典类型 取值参考
            self.currentItem.TestDepartment = self.typeDepartment.value.ItemValue;
            self.currentItem.DataType = self.typeData.value.ItemValue;
            self.currentItem.DataTypeName = self.typeData.value.ItemName;
            self.currentItem.IsEnabled = self.typeEnabled.value.ItemValue;
            self.currentItem.OQCCheckConfigId = self.selectedItem.Id;

            var postData = {
                KeyValue: '',
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("quality") + 'QC_OQCCheckConfigItem/SaveQC_OQCCheckConfigItem';

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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddDetailctrl.Tips_6'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddDetailctrl.Tips_7'));
            }
        }
        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddDetailctrl.Tips_7'));
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_OQCManage_OQCCheckConfig';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/OQCManage';

        var state = {
            name: screenStateName + '.addDetail',
            url: '/addDetail',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/OQCCheckConfig-addDetail.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddDetailctrl.Tips_8'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
