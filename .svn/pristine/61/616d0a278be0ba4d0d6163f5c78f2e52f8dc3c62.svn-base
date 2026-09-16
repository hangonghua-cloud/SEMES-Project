(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MMSaleDomestic').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticOut.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticOuteditctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.BoxtoPiece = self.currentItem.PieceQty / self.currentItem.BoxQty;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            //self.WarehouseChange = WarehouseChange;
            self.BoxQtyChange = BoxQtyChange;
            initDictionary();
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function initDictionary() {
            self.typeWarehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticOuteditctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticOuteditctrl.Tips_2'), ResourceCode: "" }]
            };
            self.typeLocation = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticOuteditctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticOuteditctrl.Tips_2'), ResourceCode: "" }]
            };

            //仓库
            commonService.get_ResourceExtendByLevelField({ LevelCode: "Warehouse", FieldCode: "CKLX", FieldValue: "3" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeWarehouse.options = res.data.resultData;
                    self.typeWarehouse.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticOuteditctrl.Tips_2')
                    });
                    self.typeWarehouse.value = self.typeWarehouse.options.find(t => t.ResourceCode == self.currentItem.WhsCode);
                    WarehouseChange(self.typeWarehouse.value);
                }
            });
        }
        function WarehouseChange(newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeLocation.options = res.data.resultData;
                    self.typeLocation.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticOuteditctrl.Tips_2')
                    });
                    self.typeLocation.value = self.typeLocation.options.find(t => t.ResourceCode == self.currentItem.LocationCode);
                }
            });
        }
        function BoxQtyChange(oldval, newval) {
            self.currentItem.PieceQty = Math.round(self.BoxtoPiece * newval);
        }

        function save() {

            self.currentItem.WhsCode = self.typeWarehouse.value.ResourceCode;
            self.currentItem.LocationCode = self.typeLocation.value.ResourceCode;


            var postData = {
                KeyValue: self.currentItem.Id,     //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("material") + 'MM_SaleDomesticOutDetail/SaveMM_SaleDomesticOutDetail';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticOuteditctrl.Tips_3') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticOuteditctrl.Tips_4'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticOuteditctrl.Tips_5'));
            }
        }
        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticOuteditctrl.Tips_5'));
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MMSaleDomestic_SaleDomesticOut';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MMSaleDomestic';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/SaleDomesticOut-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticOuteditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
