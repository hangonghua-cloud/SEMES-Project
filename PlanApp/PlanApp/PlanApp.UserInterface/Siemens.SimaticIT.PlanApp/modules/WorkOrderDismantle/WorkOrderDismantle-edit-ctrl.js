(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderDismantle').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderDismantle.WorkOrderDismantle.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope,
        commonService, auth, notificationService, busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();


            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.selectedItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.isReadOnly = false;
            self.currentItem = {};

            initData();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.WarehouseChange = WarehouseChange;
            self.typeWarehouseChange = typeWarehouseChange;


            self.selectClick2 = selectClick2;
        }

        function initData() {
            var url = commonService.getMesApiAddress("plan") + 'PL_ExeWorkOrder/GetEntityByQuery?KeyValue=' + self.selectedItem.Id;
            commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.currentItem = res.data.resultData;
                    initDictionary();
                    initTypeBatchNo();
                }
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function initDictionary() {
            self.typeBatchNo = {
                value: { BatchNo: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2'), Id: "" },
                options: [{ BatchNo: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2'), Id: "" }]
            };
            self.typeWarehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2'), ResourceCode: "" }]
            };
            self.typeLocation = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2'), ResourceCode: "" }]
            };

            self.typeWorkShopReceive = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2'), ResourceCode: "" }]
            };
            self.typeLocaltionReceive = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2'), ResourceCode: "" }]
            };

            //接收仓库
            // commonService.get_ResourceExtendByLevelField({ LevelCode: "Warehouse", FieldCode: "CKSX", FieldValue: "1" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeWorkShopReceive.options = res.data.resultData;
            //         self.typeWorkShopReceive.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2')
            //         });
            //     }
            // });
            let query = {
                factoryCode: self.selectedItem.FactoryCode,
                fieldCode: "CKSX",
                fieldValue: "1"
            }
            commonService.getWarehouseByFactoryExtendInfo(query).then(function (res) {
                if (res && res.data.success) {
                    self.typeWorkShopReceive.options = res.data.resultData;
                    self.typeWorkShopReceive.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2')
                    });
                }
            });
            //退库仓库
            // commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeWarehouse.options = res.data.resultData;
            //         self.typeWarehouse.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2')
            //         });
            //         self.typeWarehouse.value = self.typeWarehouse.options.find(t => t.ResourceCode == self.currentItem.WhsCode);
            //     }
            // });
            commonService.getWarehouseByFactory({ factoryCode: self.selectedItem.FactoryCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeWarehouse.options = res.data.resultData;
                    self.typeWarehouse.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2')
                    });
                    self.typeWarehouse.value = self.typeWarehouse.options.find(t => t.ResourceCode == self.currentItem.WhsCode);
                }
            });


            // commonService.getResourceExtendInfo({ LevelCode: "StorageLocation" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeLocation.options = res.data.resultData;
            //         self.typeLocation.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2')
            //         });
            //         self.typeLocation.value = self.typeLocation.options.find(t => t.ResourceCode == self.currentItem.LocationCode);
            //     }
            // });
        }
        //退库库位
        function typeWarehouseChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeLocation.options = res.data.resultData;
                    self.typeLocation.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2')
                    });
                    self.typeLocation.value = self.typeLocation.options.find(t => t.ResourceCode == self.currentItem.LocationCode);
                    self.isReadOnly = true;
                }
            });
        }
        //接收库位
        function WarehouseChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeLocaltionReceive.options = res.data.resultData;
                    self.typeLocaltionReceive.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2')
                    });
                }
            });
        }

        function initTypeBatchNo() {
            console.log(self.currentItem)
            //查询最近30天的批次
            let postData = {
                materialCode: self.currentItem.MMXH,
                locationCode: self.currentItem.LocationCode,
                factoryCode: self.currentItem.FactoryCode
            };
            var url = commonService.getMesApiAddress("plan") + 'PL_PlanStoreIssue/GetTuiKuBatchNo';
            var req = commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    self.typeBatchNo.options = res.data.resultData;
                    self.typeBatchNo.options.splice(0, 0, {
                        Id: "",
                        BatchNo: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_2')
                    })
                }
            });
        }

        function selectClick2() {
            debugger
            if (!self.typeWorkShopReceive.value.ResourceCode) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_3'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_4'));
                return;
            }
            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("factory") + 'level/GetListByParentResource',
                            queryParmeters: {
                                Name: "",
                                ParentResource: self.typeWorkShopReceive.value.ResourceCode
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Post",
                            sidx: "ResourceCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                // {
                                //     field: 'ResourceCode',
                                //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_6'),
                                //     width: 200
                                // },
                                {
                                    field: 'ResourceName',
                                    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_7'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_8'));
                } else {
                    self.currentItem.LocationCode = data[0].ResourceCode;
                    self.currentItem.LocationName = data[0].ResourceName;
                }
            });
        }

        function save() {

            //字典类型 取值参考

            if (self.currentItem.ConsumeNum < self.currentItem.Qty) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_9'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_4'));
                busyIndicatorService.hide();
                return;
            }

            var postData = {
                Entity: {
                    Id: self.currentItem.Id,
                    FactoryCode: self.currentItem.FactoryCode,
                    FactoryName: self.currentItem.FactoryName,
                    WorkOrder: self.currentItem.WorkOrder,
                    MaterialCode: self.currentItem.MMXH,
                    MaterialName: self.currentItem.MaterialName,
                    OldWhsCode: self.typeWarehouse.value.ResourceCode,
                    OldLocationCode: self.typeLocation.value.ResourceCode,
                    WhsCode: self.typeWorkShopReceive.value.ResourceCode,
                    LocationCode: self.currentItem.LocationCode,
                    BatchNo: self.typeBatchNo.value.BatchNo,
                    SupplierCode: self.typeBatchNo.value.SupplierCode,
                    Qty: self.currentItem.Qty,
                    Unit: self.currentItem.Unit,
                    UnitName: self.currentItem.UnitName,
                    DanHao: self.currentItem.MaskConsume,
                    DXZH: self.currentItem.DXZH
                }
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_10') });
            var url = commonService.getMesApiAddress("plan") + 'PL_PlanStoreIssue/Save_CancellingStocks';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_11'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_4'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_4'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderDismantle_WorkOrderDismantle';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderDismantle';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrderDismantle-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderDismantle.editJS.Tips_12'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
