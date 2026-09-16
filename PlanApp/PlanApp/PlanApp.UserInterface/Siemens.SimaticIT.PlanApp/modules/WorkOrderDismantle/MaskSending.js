(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderDismantle').controller('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSending',
        ['common.base', '$filter', '$scope', '$uibModalInstance', 'data', 'commonService',
            function (common, $filter, $scope, $modalInstance, data, commonService) {
                var vm = this;
                var self = this;
                var sidePanelManager, backendService, propertyGridHandler;
                vm.data = angular.copy(data);
                vm.lang = 'zh-cn';
                vm.currentItem = null;
                vm.selectedItem = null;
                console.log(vm.data);
                // //分页变量
                var pagination = {
                    rows: 20,//每页显示条数
                    page: 1,//页码
                    sidx: vm.data.sidx,//排序字段
                    sord: vm.data.sord//排序方式
                };
                //查询参数
                vm.currentItem = vm.data.queryParmeters;
                // vm.queryParmeters = {
                //     Type: 1,//物料类型（1-产品；2-包装物料；3-原料；）
                //     Name: null//物料名称（模糊查询）
                // };
                activate();

                function activate() {
                    init();
                    vm.loadData = loadData;
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
                    $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
                    vm.validInputs = false;

                    vm.searchParam = {
                        BatchNo: ""
                    }

                    initGridData();
                    loadData();

                    initDictionary();
                    setTimeout(function () {
                        //初始化grid数据、查询
                        LoadFactory();
                    }, 100);//如果查询条件有下拉参数，请调整此值到1000
                    vm.WarehouseChange = WarehouseChange;
                    vm.ShouldChange = ShouldChange;
                    self.WhsNameChange = WhsNameChange;
                }

                function initDictionary() {
                    //工厂
                    self.WhsName = {
                        value: null,
                        options: []
                    };
                    vm.typeWarehouse = {
                        value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_1'), ResourceCode: "" },
                        options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_1'), ResourceCode: "" }]
                    };
                    vm.typeLocation = {
                        value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_1'), ResourceCode: "" },
                        options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_1'), ResourceCode: "" }]
                    };

                    //仓库
                    // commonService.get_ResourceExtendByLevelField({ LevelCode: "Warehouse", FieldCode: "CKSX", FieldValue: "2" }).then(function (res) {
                    //     if (res && res.data.success) {
                    //         vm.typeWarehouse.options = res.data.resultData;
                    //         vm.typeWarehouse.options.splice(0, 0, {
                    //             ResourceCode: "",
                    //             ResourceName: "--请选择--"
                    //         });
                    //     }
                    // });
                    let query = {
                        factoryCode: vm.currentItem.FactoryCode,
                        fieldCode: "CKSX",
                        fieldValue: "2"
                    }
                    commonService.getWarehouseByFactoryExtendInfo(query).then(function (res) {
                        if (res && res.data.success) {
                            vm.typeWarehouse.options = res.data.resultData;
                            vm.typeWarehouse.options.splice(0, 0, {
                                ResourceCode: "",
                                ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_1')
                            });
                        }
                    });
                }

                function WarehouseChange(oldItem, newItem) {
                    commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                        if (res && res.data.success) {
                            vm.typeLocation.options = res.data.resultData;
                            vm.typeLocation.options.splice(0, 0, {
                                ResourceCode: "",
                                ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_1')
                            });
                        }
                    });
                }
                function ShouldChange(oldItem, newItem) {
                    if (!isNaN(newItem) && !isNaN(vm.currentItem.TotalNum))
                        vm.currentItem.ForecastNum = (vm.currentItem.TotalNum / newItem).toFixed(2);
                }
                function LoadFactory() {
                    var url = commonService.getMesApiAddress('factory') + "level/GetWhsNameByFactory";
                    var post = { factoryCode: vm.currentItem.FactoryCode }
                    commonService.callWebApiPost(url, post).then(function (data) {
                        if ((data) && (data.data.success)) {
                            self.WhsName.options = data.data.resultData;;
                        }
                    });
                }
                function WhsNameChange(oldVal, newVal) {
                    self.currentItem.WhsCode = "";

                    newVal.forEach(x => {
                        self.currentItem.WhsCode += x.ResourceCode + ",";

                    });
                };
                function initGridData() {
                    vm.gridOptions = {
                        enableFullRowSelection: true,
                        enableRowSelection: false,
                        enableSelectAll: false,//是否多选
                        enableMultiSelection: false,//是否多选
                        selectionRowHeaderWidth: 35,
                        enableRowHeaderSelection: true,
                        //Added for custom paging      
                        paginationPageSizes: [pagination.rows, pagination.rows * 2, pagination.rows * 4, pagination.rows * 10, pagination.rows * 20],
                        paginationPageSize: pagination.rows,
                        useExternalPagination: true, // custom      
                        useExternalSorting: true, // custom      
                        useExternalFiltering: true, // custom 
                        totalItems: null,
                        multiSelect: false,//是否多选
                        enableSorting: true,
                        enableFiltering: false,
                        columnDefs: [
                            // {
                            //     field: 'FactoryCode',
                            //     displayName: '工厂编码',
                            //     width: 120
                            // },
                            // {
                            //     field: 'FactoryName',
                            //     displayName: '工厂名称',
                            //     width: 120
                            // },
                            {
                                field: 'MaterialName',
                                displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_2'),
                                width: 120
                            },

                            {
                                field: 'LocationName',
                                displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_3'),
                                width: 180
                            },
                            {
                                field: 'SupplierName',
                                displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_4'),
                                width: 180
                            },
                            {
                                field: 'BatchNo',
                                displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_5'),
                                width: 120
                            },
                            {
                                field: 'Unit',
                                displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_6'),
                                width: 80
                            },
                            {
                                field: 'WorkOrder',
                                displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_7'),
                                width: 120
                            },
                            {
                                field: 'SuperNum',
                                displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_8'),
                                width: 100
                            },
                            {
                                field: 'Qty',
                                displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_9'),
                                width: 100
                            },
                            {
                                field: 'ActNum',
                                displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_10'),
                                width: 100,

                            },
                        ],
                        onRegisterApi: function (gridApi) {
                            $scope.gridApi = gridApi;
                            gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                                // var msg = 'row selected ' + row.isSelected;
                                // console.log(msg);
                                if (row && row.isSelected === true) {
                                    vm.selectedItem = row.entity;
                                } else {
                                    vm.selectedItem = null;
                                }
                            });
                            gridApi.selection.on.rowSelectionChangedBatch($scope, function (rows) {
                                var msg = 'rows changed ' + rows.length;
                                console.log(msg);
                            });
                            //Added for custom paging      
                            gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                                pagination.page = newPage;
                                pagination.rows = pageSize;
                                vm.loadData();
                            });
                            //custom sort      
                            $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                                if (sortColumns.length === 0) {//不排序
                                    pagination.sidx = "";
                                    pagination.sord = "";
                                } else {//按指定的一个字段排序
                                    if (sortColumns.length === 1) {
                                        pagination.sidx = sortColumns[0].field;
                                        pagination.sord = sortColumns[0].sort.direction;
                                    }
                                    else {//组合排序
                                        var sortname = "";
                                        for (var i = 0, len = sortColumns.length; i < len; i++) {
                                            if (i > 0) {
                                                sortname += ", ";
                                            }
                                            sortname += sortColumns[i].field;
                                            if (i !== len - 1) {
                                                sortname += " " + sortColumns[i].sort.direction;
                                            }
                                        }
                                        //console.log('-----------------sortname------------------');
                                        //console.log(sortname);
                                        pagination.sidx = sortname;
                                        pagination.sord = sortColumns[len - 1].sort.direction;
                                    }
                                }
                                loadData();
                            });
                        },
                        data: []
                    };

                }
                function loadData() {

                    //vm.gridOptions.data = rows;
                    var postdata = {
                        queryJson: {
                            FactoryCode: vm.currentItem.FactoryCode,
                            MaterialCode: vm.currentItem.MMXH,
                            IsFrozen: "0",
                            QtyStr: ">0",
                            FieldValue: "1",
                            BatchNo: vm.searchParam.BatchNo,
                            ResourceName: vm.currentItem.LocationName,
                            WhsCode: self.currentItem.WhsCode,
                        }
                    }
                    var url = commonService.getMesApiAddress("material") + "MM_RawMaterialStock/GetRawMaterialStock";
                    commonService.callWebApiPost(url, postdata).then(function (res) {
                        if (res && res.data.success) {
                            //vm.gridOptions.totalItems = res.data.resultData.length;
                            vm.gridOptions.data = res.data.resultData.rows;
                        } else {
                            vm.gridOptions.data = [];
                            commonService.showError("获取数据出错:" + res.data.Error.Message);
                        }
                        commonService.hideLoading();
                    }, function (error) {
                        commonService.showError('[' + error.status + '] - ' + '获取数据时出现错误 ' + error.statusText);
                        commonService.hideLoading();
                    });

                }
                function onPropertyGridValidityChange(event, params) {
                    vm.validInputs = params.validity;
                }


                vm.edit = function () {
                    if (!vm.selectedItem) {
                        backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_11'), "操作出错");
                        return false;
                    }
                    if (vm.selectedItem.Qty < vm.currentItem.ActNum && vm.selectedItem.Qty < vm.currentItem.ActNum) {
                        backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_12'), "操作出错");
                        return false;
                    }

                    vm.selectedItem.ActNum = vm.currentItem.ActNum;
                    var num = 0;
                    var data = vm.gridOptions.data;
                    data.forEach((item, index, arr) => {
                        if (item.ActNum > 0) {
                            num += item.ActNum
                        }
                    })
                    vm.currentItem.UseNum = num;
                }

                vm.save = function () {
                    debugger;
                    //let selectionRows = $scope.gridApi.selection.getSelectedRows();
                    var data = vm.gridOptions.data;
                    var rows = [];
                    // if (!vm.currentItem.UseNum || vm.currentItem.UseNum < vm.currentItem.TotalNum) {
                    //     backendService.genericError("面膜发料数量不足", "操作出错");
                    //     return false;
                    // }

                    var flag = false;
                    data.forEach((item, index, arr) => {
                        if (item.ActNum > 0) {
                            if (item.LocationCode == vm.typeLocation.value.ResourceCode) flag = true;

                            rows.push({
                                Id: item.Id,
                                FactoryCode: item.FactoryCode,
                                FactoryName: item.FactoryName,
                                OldWhsCode: item.WhsCode,
                                OldLocationCode: item.LocationCode,
                                WhsCode: vm.typeWarehouse.value.ResourceCode,
                                LocationCode: vm.typeLocation.value.ResourceCode,
                                MaterialCode: item.MaterialCode,
                                MaterialName: item.MaterialName,
                                SupplierCode: item.SupplierCode,
                                BatchNo: item.BatchNo,
                                Spec: item.Spec,
                                Unit: item.Unit,
                                Qty: item.Qty - item.ActNum,
                                ActNum: item.ActNum,
                                SmallClass: item.SmallClass
                            })
                        }
                    })


                    if (flag) {
                        backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSendJS.Tips_13'), "操作出错");
                        return false;
                    }

                    var param = {
                        UseNum: vm.currentItem.UseNum,
                        WhsCode: vm.typeWarehouse.value.ResourceCode,
                        LocationCode: vm.typeLocation.value.ResourceCode,
                        data: rows
                    }
                    $modalInstance.close(param);
                };

                vm.cancel = function () {
                    $modalInstance.dismiss();
                };
            }
        ]);
}());