(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderManage').controller('Siemens.SimaticIT.PlanApp.WorkOrderManage.EditBomItem',
        ['common.base', '$filter', '$scope', '$uibModalInstance', 'data', 'commonService',
            function (common, $filter, $scope, $modalInstance, data, commonService) {
                var vm = this;
                var sidePanelManager, backendService, propertyGridHandler;
                vm.data = angular.copy(data);
                vm.currentItem = vm.data.selectedItem;
                vm.lang = 'zh-cn';
                //vm.currentItem = null;
                vm.selectedItem = null;
                vm.validInputs = false;

                activate();

                function activate() {
                    init();
                    registerEvents();
                }
                vm.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
                    if (col.filters[0].term) {
                        return 'header-filtered';
                    } else {
                        return '';
                    }
                };

                function init() {
                    sidePanelManager = common.services.sidePanel.service;
                    backendService = common.services.runtime.backendService;

                    initDictionary();
                    vm.materialChange = materialChange;
                    materialChange(null, vm.currentItem.MaterialCode);
                }

                function initDictionary() {
                    //初始化 物料分类
                    vm.MaterialClass = {
                        value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ItemValue: "" },
                        options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ItemValue: "" }]
                    };

                    //初始化 物料小类
                    vm.SmallClass = {
                        value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ItemValue: "" },
                        options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ItemValue: "" }]
                    };

                    vm.typeUnit = {
                        value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ItemValue: "" },
                        options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ItemValue: "" }]
                    };


                    vm.Warehouse = {
                        value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ResourceCode: "" },
                        options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ResourceCode: "" }]
                    };

                    vm.Process = {
                        value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ResourceCode: "" },
                        options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ResourceCode: "" }]
                    };
                    vm.TypeBom = {
                        value: { BOMCode: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), Id: "" },
                        options: [{ BOMCode: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), Id: "" }]
                    };

                    commonService.getDataItemDuatil("MaterialType").then(function (res) {
                        if (res && res.data.success) {
                            vm.MaterialClass.options = res.data.resultData;
                            vm.MaterialClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                        }
                    })
                    commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                        if (res && res.data.success) {
                            vm.SmallClass.options = res.data.resultData;
                            vm.SmallClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                        }
                    })
                    commonService.getDataItemDuatil("Unit").then(function (res) {
                        if (res && res.data.success) {
                            vm.typeUnit.options = res.data.resultData;
                            vm.typeUnit.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                        }
                    })

                    commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
                        if (res && res.data.success) {
                            vm.Warehouse.options = res.data.resultData;
                            vm.Warehouse.options.splice(0, 0, {
                                ResourceCode: "",
                                ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1')
                            });
                        }
                        vm.Warehouse.value = vm.Warehouse.options.find(t => t.ResourceCode == vm.currentItem.Warehouse);
                    });
                    commonService.getResourceExtendInfo({ LevelCode: "Process" }).then(function (res) {
                        if (res && res.data.success) {
                            vm.Process.options = res.data.resultData;
                            vm.Process.options.splice(0, 0, {
                                ResourceCode: "",
                                ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1')
                            });
                        }
                        vm.Process.value = vm.Process.options.find(t => t.ResourceCode == vm.currentItem.ProcessCode);
                    });
                    //外发标识
                    vm.typeWFMark = {
                        value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ItemValue: "" },
                        options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ItemValue: "" }]
                    };
                    commonService.getDataItemDuatil("WFMark").then(function (res) {
                        if (res && res.data.success) {
                            vm.typeWFMark.options = res.data.resultData;
                        }
                        vm.typeWFMark.value = vm.typeWFMark.options.find(t => t.ItemValue == vm.currentItem.WFMark);
                    })

                }

                function materialChange(oldvalu, newvalue) {
                    //获取物料基本信息
                    var url1 = commonService.getMesApiAddress("material") + 'Base_Material/GetDataTable_TestOtherEntity?checkType=' + newvalue;
                    commonService.callWebApiGet(url1, null).then(function (res) {
                        if (res && res.data.success && res.data.resultData.length > 0) {
                            var ent = res.data.resultData[0];
                            vm.currentItem.MaterialName = ent.MaterialName;
                            vm.currentItem.Spec = ent.Spec;
                            vm.MaterialClass.value = vm.MaterialClass.options.find(t => t.ItemValue == ent.MaterialClass);
                            vm.SmallClass.value = vm.SmallClass.options.find(t => t.ItemValue == ent.SmallClass);
                            vm.typeUnit.value = vm.typeUnit.options.find(t => t.ItemValue == ent.Unit);
                        } else {
                            vm.currentItem.MaterialClass = "";
                            vm.currentItem.MaterialName = "";
                            vm.currentItem.Spec = "";
                            vm.MaterialClass.value = { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ItemValue: "" };
                            vm.SmallClass.value = { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ItemValue: "" };
                            vm.typeUnit.value = { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), ItemValue: "" };
                        }
                    });

                    //获取物料BOM信息
                    var url2 = commonService.getMesApiAddress("material") + 'BS_BOM/GetBS_BOMList?checkType=' + newvalue;
                    commonService.callWebApiGet(url2, null).then(function (res) {
                        if (res && res.data.success) {
                            vm.TypeBom.options = res.data.resultData;
                            vm.TypeBom.options.splice(0, 0, {
                                Id: "",
                                BOMCode: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1')
                            });
                            if (!!vm.currentItem.BOMCode) {
                                vm.TypeBom.value = vm.TypeBom.options.find(t => t.BOMCode == vm.currentItem.BOMCode);
                            }
                        } else {
                            vm.TypeBom.value = { BOMCode: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editBomItemJs.Tips_1'), Id: "" };
                        }
                    });

                };


                vm.save = function () {
                    vm.currentItem.Unit = vm.typeUnit.value.ItemValue;
                    vm.currentItem.Warehouse = vm.Warehouse.value.ResourceCode;
                    vm.currentItem.ProcessCode = vm.Process.value.ResourceCode;
                    vm.currentItem.UnitName = vm.typeUnit.value.ItemValue == "" ? "" : vm.typeUnit.value.ItemName;
                    vm.currentItem.WarehouseName = vm.Warehouse.value.ResourceName;
                    vm.currentItem.ProcessName = vm.Process.value.ResourceName;
                    vm.currentItem.MaterialClass = vm.MaterialClass.value.ItemValue == "" ? "" : vm.MaterialClass.value.ItemValue;
                    vm.currentItem.MaterialClassName = vm.MaterialClass.value.ItemValue == "" ? "" : vm.MaterialClass.value.ItemName;
                    vm.currentItem.SmallClass = vm.SmallClass.value.ItemValue == "" ? "" : vm.SmallClass.value.ItemValue;
                    vm.currentItem.SmallClassName = vm.SmallClass.value.ItemValue == "" ? "" : vm.SmallClass.value.ItemName;
                    vm.currentItem.WFMark = vm.typeWFMark.value.ItemValue;
                    vm.currentItem.WFMarkName = vm.typeWFMark.value.ItemName;
                    if (vm.TypeBom.value.Id != "") {
                        vm.currentItem.BOMCode = vm.TypeBom.value.BOMCode;
                    }
                    var param = {
                        entity: vm.currentItem
                    }
                    $modalInstance.close(param);
                };

                vm.cancel = function () {
                    $modalInstance.dismiss();
                };

                function registerEvents() {
                    $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
                }
                function onPropertyGridValidityChange(event, params) {
                    vm.validInputs = params.validity;
                }
            }
        ]);
}());