(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.OQCManage').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfig.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigeditctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            initDictionary();
        }
        function initDictionary() {
            self.typeEnabled = {
                value: { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigeditctrl.Tips_2') },
                options: [
                    { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigeditctrl.Tips_2') },
                    { ItemValue: false, ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigeditctrl.Tips_3') }
                ]
            };
            self.typeEnabled.value == self.typeEnabled.options.find(t => t.ItemValue == self.currentItem.IsEnabled);
            self.typeSmallClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigeditctrl.Tips_4'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigeditctrl.Tips_4'), ItemValue: "" }]
            }
            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.typeSmallClass.options = res.data.resultData;
                    self.typeSmallClass.value = self.typeSmallClass.options.find(t => t.ItemValue == self.currentItem.SmallClass);
                }
            })
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigeditctrl.Tips_5') });
            //字典类型 取值参考
            self.currentItem.IsEnabled = self.typeEnabled.value.ItemValue;
            self.currentItem.SmallClass = self.typeSmallClass.value.ItemValue;

            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("quality") + 'QC_OQCCheckConfig/SaveQC_OQCCheckConfig';

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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigeditctrl.Tips_6'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigeditctrl.Tips_7'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigeditctrl.Tips_7'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_OQCManage_OQCCheckConfig';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/OQCManage';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/OQCCheckConfig-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigeditctrl.Tips_8'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
