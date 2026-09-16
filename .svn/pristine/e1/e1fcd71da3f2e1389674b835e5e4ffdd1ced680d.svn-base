(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MMReceiptNotice').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNotice.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.mainCurrentItem = angular.copy($stateParams.mainSelectedItem);
            self.validInputs = false;
            initdictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.WarehouseChange = WarehouseChange;
        }

        function initdictionary() {

            self.typeWarehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditDetailctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditDetailctrl.Tips_2'), ResourceCode: "" }]
            };
            self.Location = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditDetailctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditDetailctrl.Tips_2'), ResourceCode: "" }]
            };
            // commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeWarehouse.options = res.data.resultData;
            //         self.typeWarehouse.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditDetailctrl.Tips_2')
            //         });
            //         self.typeWarehouse.value = self.typeWarehouse.options.find(t => t.ResourceCode == self.currentItem.WhsCode);
            //     }
            // });
            commonService.getWarehouseByFactory({ factoryCode: self.currentItem.FactoryCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeWarehouse.options = res.data.resultData;
                    self.typeWarehouse.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditDetailctrl.Tips_2')
                    });
                    self.typeWarehouse.value = self.typeWarehouse.options.find(t => t.ResourceCode == self.currentItem.WhsCode);
                }
            });
        }

        //仓库改变事件
        function WarehouseChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.Location.options = res.data.resultData;
                    self.Location.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditDetailctrl.Tips_2')
                    });
                    self.Location.value = self.Location.options.find(t => t.ResourceCode == self.currentItem.LocationCode);
                }
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {

            self.currentItem.Warehouse = self.typeWarehouse.value.ResourceCode;

            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem,
                ArrivalQty: self.mainCurrentItem.ArrivalQty
            };

            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialIn/SaveMM_RawMaterialIn';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditDetailctrl.Tips_3') });
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
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditDetailctrl.Tips_4'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditDetailctrl.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditDetailctrl.Tips_5'));
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MMReceiptNotice_ReceiptNotice';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MMReceiptNotice';

        var state = {
            name: screenStateName + '.editDetail',
            url: '/editDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ReceiptNotice-editDetail.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditDetailctrl.Tips_6'
            },
            params: {
                selectedItem: null,
                mainSelectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
