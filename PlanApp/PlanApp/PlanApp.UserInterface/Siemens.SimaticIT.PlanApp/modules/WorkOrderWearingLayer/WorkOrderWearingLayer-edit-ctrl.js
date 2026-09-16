(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.WorkOrderWearingLayer.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope,
        commonService, auth, notificationService, busyIndicatorService, $modal, $interval) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_1'));
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
            initTypeBatchNo();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.WarehouseChange = WarehouseChange;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function initDictionary() {

            self.typeBatchNo = {
                value: { BatchNo: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2'), Id: "" },
                options: [{ BatchNo: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2'), Id: "" }]
            };
            self.typeWarehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2'), ResourceCode: "" }]
            };
            self.typeLocation = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2'), ResourceCode: "" }]
            };

            self.typeWorkShopReceive = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2'), ResourceCode: "" }]
            };
            self.typeLocaltionReceive = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2'), ResourceCode: "" }]
            };

            //接收仓库
            // commonService.get_ResourceExtendByLevelField({ LevelCode: "Warehouse", FieldCode: "CKSX", FieldValue: "1" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeWorkShopReceive.options = res.data.resultData;
            //         self.typeWorkShopReceive.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2')
            //         });
            //     }
            // });
            let query = {
                factoryCode: self.currentItem.FactoryCode,
                fieldCode: "CKSX",
                fieldValue: "1"
            };
            commonService.getWarehouseByFactoryExtendInfo(query).then(function (res) {
                if (res && res.data.success) {
                    self.typeWorkShopReceive.options = res.data.resultData;
                    self.typeWorkShopReceive.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2')
                    });
                }
            });

            //退库仓库
            // commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeWarehouse.options = res.data.resultData;
            //         self.typeWarehouse.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2')
            //         });

            //         self.typeWarehouse.value = self.typeWarehouse.options.find(t => t.ResourceCode == self.currentItem.WhsCode);
            //     }
            // });
            commonService.getWarehouseByFactory({ factoryCode: self.currentItem.FactoryCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeWarehouse.options = res.data.resultData;
                    self.typeWarehouse.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2')
                    });
                    self.typeWarehouse.value = self.typeWarehouse.options.find(t => t.ResourceCode == self.currentItem.WhsCode);
                }
            });
            commonService.getResourceExtendInfo({ LevelCode: "StorageLocation" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeLocation.options = res.data.resultData;
                    self.typeLocation.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2')
                    });
                    self.typeLocation.value = self.typeLocation.options.find(t => t.ResourceCode == self.currentItem.LocationCode);
                }
            });
        }
        function WarehouseChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeLocaltionReceive.options = res.data.resultData;
                    self.typeLocaltionReceive.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2')
                    });
                }
            });
        }
        function initTypeBatchNo() {
            //查询最近30天的批次
            var postData = {
                materialCode: self.currentItem.WearingLayerCode,
                factoryCode: self.currentItem.FactoryCode
            }
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialOut/GetList_TestOtherEntity';
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    self.typeBatchNo.options = res.data.resultData;
                    self.typeBatchNo.options.splice(0, 0, {
                        Id: "",
                        BatchNo: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_2')
                    })
                }
            });
        }


        function save() {

            //字典类型 取值参考

            if (self.currentItem.ConsumeNum < self.currentItem.Qty) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_3'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_4'));
                busyIndicatorService.hide();
                return;
            }

            var postData = {
                Entity: {
                    Id: self.currentItem.Id,
                    FactoryCode: self.currentItem.FactoryCode,
                    FactoryName: self.currentItem.FactoryName,
                    WorkOrder: self.currentItem.WorkOrder,
                    ExeWorkOrder: self.currentItem.ExeWorkOrder,
                    MaterialCode: self.currentItem.WearingLayerCode,
                    MaterialName: self.currentItem.WearingLayerName,

                    WhsCode: self.typeWorkShopReceive.value.ResourceCode,
                    LocationCode: self.typeLocaltionReceive.value.ResourceCode,
                    BatchNo: self.typeBatchNo.value.BatchNo,
                    SupplierCode: self.typeBatchNo.value.SupplierCode,
                    Qty: self.currentItem.Qty,
                    Unit: self.currentItem.Unit,
                    DanHao: self.currentItem.DanHao
                }
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_5') });
            var url = commonService.getMesApiAddress("plan") + 'PL_ExeWorkOrderWearingLayer/Save_CancellingStocks';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_6'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_4'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_4'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderWearingLayer_WorkOrderWearingLayer';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderWearingLayer';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrderWearingLayer-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.editJS.Tips_7'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
