(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum').config(EditVCScreenStateConfig);

    EditVCScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.PMOperationPalletNum.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditVCScreenController(dataService, $state, $stateParams,
        common, $filter, $scope, commonService, auth, notificationService,
        busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();

        // Initialization function
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.editVCJS.Tips_1'));
            sidePanelManager.open('e');
            //sidePanelManager.open({
            //  mode: "e",
            //   size: "wide"
            //});
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            //数据字典
            initDictionary();
        }

        function initDictionary() {

            self.typeMaterialClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.editVCJS.Tips_2'), ItemValue: "" },
                options: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.editVCJS.Tips_2'), ItemValue: "" },
            }

            commonService.getDataItemDuatil("").then(function (res) {
                if (res && res.data.success) {
                    self.typeMaterialClass.options = res.data.resultData;
                }
                self.typeMaterialClass.value = self.typeMaterialClass.options.find(t => t.ItemValue == self.currentItem.MaterialClass);
            })

            //单位
            self.typeUnit = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.editVCJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.editVCJS.Tips_2'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("Unit").then(function (res) {
                if (res && res.data.success) {
                    self.typeUnit.options = res.data.resultData;
                    self.typeUnit.value = res.data.resultData.find(t => t.ItemName == self.currentItem.UnitName);
                }
            })
        }

        function save() {

            self.currentItem.UnitName = self.typeUnit.value.ItemName;
            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.editVCJS.Tips_3') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PMOperationPalletNum/SaveForm';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        //取消
        function cancel() {
            sidePanelManager.close();//关闭侧边栏
            $state.go('^');//返回列表(父页面)
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();//关闭遮罩层
                sidePanelManager.close();//关闭侧边栏
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.editVCJS.Tips_4'));
                $rootScope.$emit('to-parent', 'parent');//刷新局部
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.editVCJS.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.editVCJS.Tips_5'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }
    EditVCScreenStateConfig.$inject = ['$stateProvider'];
    function EditVCScreenStateConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_PMOperationPalletNum_PMOperationPalletNum';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PMOperationPalletNum';

        var state = {
            name: moduleStateName + '.editVC',
            url: '/editVC',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PMOperationPalletNum-editVC.html',
                    controller: EditVCScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.editVCJS.Tips_1'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
