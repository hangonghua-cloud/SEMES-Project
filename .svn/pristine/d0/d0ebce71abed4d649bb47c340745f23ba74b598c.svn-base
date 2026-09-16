(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.RawMaterialStock').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions1();
            initGridOptions2();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = null;
            self.validInputs = false;
            self.selectedItem1 = null;
            self.selectedItem2 = null;
            self.IsShowButten = false;
            self.IsShowDeleteButten = false;
            self.searchParams = {};
            self.index = 0;
            self.PostDate = new Date();

            initDictionary();
            self.typeFactoryChange = typeFactoryChange;//工厂change事件
            // self.WarehouseChange = WarehouseChange;
            // self.ObjWarehouseChange = ObjWarehouseChange;
            self.selectClick2 = selectClick2;//选择库位
            self.selectClick3 = selectClick3;//选择目标库位

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.search = search;
            self.addForm = addForm;
            self.deleteForm = deleteForm;
        }


        function initDictionary() {
            //仓库
            self.Warehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2'), ResourceCode: "" }]
            };

            //库位
            self.Location = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2'), ResourceCode: "" }]
            };
            //管理方式
            // self.ManageMode = {
            //     value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2'), ItemValue: "" },
            //     options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2'), ItemValue: "" },
            //     { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_3'), ItemValue: "1" },
            //     { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_4'), ItemValue: "0" }]
            // }

            // let query = {
            //     LevelCode: "Warehouse",
            //     FieldCode: "CKLX",
            //     FieldValue: "1"
            // };
            // commonService.getResourceExtendInfo(query).then(function (res) {
            //     if (res && res.data.success) {
            //         self.Warehouse.options = res.data.resultData;
            //         self.Warehouse.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2')
            //         });
            //     }
            // });
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2')
                    });
                    initGridData1();
                }
            });
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                let query = {
                    factoryCode: newItem.ResourceCode
                };
                commonService.getWarehouseByFactory(query).then(function (res) {
                    if (res && res.data.success) {

                        self.Warehouse.options = res.data.resultData;
                        self.Warehouse.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2')
                        });

                    }
                });
            } else {
                self.Warehouse = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2'), ResourceCode: "" }]
                };

            }
        }
        //仓库改变事件
        // function WarehouseChange(oldItem, newItem) {
        // commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
        //     if (res && res.data.success) {
        //         self.Location.options = res.data.resultData;
        //         self.Location.options.splice(0, 0, {
        //             ResourceCode: "",
        //             ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2')
        //         });
        //     }
        // });
        // }


        function selectClick2() {
            if (!self.Warehouse.value.ResourceCode) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_5'), commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_6'));
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
                                ParentResource: self.Warehouse.value.ResourceCode
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Post",
                            sidx: "ResourceCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_7'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ResourceCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_8'),
                                    width: 200
                                },
                                {
                                    field: 'ResourceName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_9'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_10'));
                } else {
                    self.searchParams.LocationCode = data[0].ResourceCode;
                    self.searchParams.LocationName = data[0].ResourceName;
                }
            });
        }
        //目标仓库改变事件
        // function ObjWarehouseChange(oldItem, newItem) {
        //     commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
        //         if (res && res.data.success) {
        //             self.Location.options = res.data.resultData;
        //             self.Location.options.splice(0, 0, {
        //                 ResourceCode: "",
        //                 ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_2')
        //             });
        //         }
        //     });
        //     }

        function selectClick3() {
            if (!self.selectedItem1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_11'), commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_6'));
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
                                ParentResource: self.selectedItem1.WhsCode
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Post",
                            sidx: "ResourceCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_7'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ResourceCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_8'),
                                    width: 200
                                },
                                {
                                    field: 'ResourceName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_9'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_10'));
                } else {
                    self.searchParams.ObjLocationCode = data[0].ResourceCode;
                    self.searchParams.ObjLocationName = data[0].ResourceName;
                }
            });
        }

        function initGridOptions1() {
            self.gridOptionsItem1 = {
                enablePagination: false,
                enablePaginationControls: true,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 100, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                rowHeight: 35,
                multiSelect: false,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: false,
                enableRowSelection: false,
                enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_7'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_8'),
                        width: 120
                    },

                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_12'),
                        width: 150
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_19'),
                        width: 200
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_13'),
                        width: 130
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_14'),
                        width: 110
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_15'),
                        width: 80
                    },
                    {
                        field: 'SupplierName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_16'),
                        width: 140
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_17'),
                        width: 110
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_18'),
                        width: 100
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_20'),
                        width: 120
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_21'),
                        width: 120
                    },

                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData1();
                    });
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem1 = row.entity;
                            //setButtonsVisibility(true);
                            //GetWorkOrderBomUnitConsome();
                            self.IsShowButten = true;
                        } else {
                            self.selectedItem1 = null;
                            self.IsShowButten = false;
                            //setButtonsVisibility(false);
                        }
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }

        function initGridData1() {
            self.selectedItem = null;
            self.isButtonVisible = false;
            //按钮权限

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_22'))
                return;
            }

            let Pagination = {
                rows: self.gridOptionsItem1.paginationPageSize,
                page: self.gridOptionsItem1.paginationCurrentPage,
                sidx: 'WhsCode,LocationCode,MaterialCode',//仓库编码、物料编码
                sord: 'asc'
            };
            if (self.searchParams.LocationName == "" || self.searchParams.LocationName == null) {
                self.searchParams.LocationCode = "";
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.WhsCode = self.Warehouse.value.ResourceCode;
            // self.searchParams.LocationCode = self.Location.value.ResourceCode;
            // self.searchParams.ManageMode = self.ManageMode.value.ItemValue;
            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialStock/MM_RawMaterialStockPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsItem1.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsItem1.data = res.data.resultData.rows;
                    self.searchParams.MaterialCode = "";
                } else {
                    self.gridOptionsItem1.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_23'));
            });
        }
        function search() {
            initGridData1();
        }

        function addForm() {

            if (!self.selectedItem1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_24'), commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_6'));
                return false;
            }
            if (self.selectedItem1.Qty < self.currentItem.ArrivalQty) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_25'), commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_6'));
                return false;
            }
            if (self.selectedItem1.LocationName == self.searchParams.ObjLocationName) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_26'), commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_6'));
                return;
            }
            var data = self.gridOptionsItem2.data;
            // var ent = data.find(t => t.MaterialCode == self.selectedItem1.MaterialCode && t.PurchaseOrder == self.selectedItem1.PurchaseOrder);
            // if (ent != null) {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_27'), commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_6'));
            //     return false;
            // }

            self.currentItem.PostDate = commonService.ConvertToLocalDate(self.PostDate);
            self.currentItem.ArrivalTime = commonService.ConvertToLocalDate(self.ArrivalTime);
            self.index = self.index + 1;
            data.push({
                index: self.index,
                FactoryCode: self.selectedItem1.FactoryCode,
                FactoryName: self.selectedItem1.FactoryName,
                WhsName: self.selectedItem1.WhsName,
                WhsCode: self.selectedItem1.WhsCode,
                LocationName: self.searchParams.ObjLocationName,
                LocationCode: self.searchParams.ObjLocationCode,
                MaterialCode: self.selectedItem1.MaterialCode,
                MaterialName: self.selectedItem1.MaterialName,
                Spec: self.selectedItem1.Spec,
                SmallClass: self.selectedItem1.SmallClass,
                SmallClassName: self.selectedItem1.SmallClassName,
                MaterialClass: self.selectedItem1.MaterialClass,
                Unit: self.selectedItem1.Unit,
                BatchNo: self.selectedItem1.BatchNo,
                SupplierCode: self.selectedItem1.Supplier,
                SupplierName: self.selectedItem1.SupplierName,
                Qty: self.currentItem.ArrivalQty,
                Id: self.selectedItem1.Id,
                PostDate: self.currentItem.PostDate
            });
            self.gridOptionsItem2.data = data;
            self.currentItem.ArrivalQty = "";
        }

        function initGridOptions2() {
            self.gridOptionsItem2 = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
                rowHeight: 35,
                multiSelect: false,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: false,
                enableRowSelection: false,
                //enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_17'),
                        width: 100
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_18'),
                        width: 100
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_8'),
                        width: 150
                    },
                    // {
                    //     field: 'ManageMode',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_28'),
                    //     width: 120
                    // },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_12'),
                        width: 150
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_19'),
                        width: 200
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_20'),
                        width: 120
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_21'),
                        width: 120
                    },
                    {
                        field: 'SupplierName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_16'),
                        width: 150
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_13'),
                        width: 150
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_15'),
                        width: 80
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_29'),
                        width: 110
                    },

                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi2 = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem2 = row.entity;
                            self.IsShowDeleteButten = true;
                            //setButtonsVisibility(true);

                        } else {
                            self.selectedItem2 = null;
                            self.IsShowDeleteButten = false;
                        }
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi2.core.handleWindowResize();
                        $scope.gridApi2.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }


        function deleteForm() {
            self.gridOptionsItem2.data = _.filter(self.gridOptionsItem2.data, function (item) {
                return item.index != self.selectedItem2.index;
            })
        }

        function save() {

            var data = self.gridOptionsItem2.data;
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_30'), commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_6'));
                return false;
            }

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                data: data
            };
            console.log("jpf1234" + JSON.stringify(postData));
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialStock/RawMaterialStockMoveList';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_31') });
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

        }

        //取消
        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_32'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_6'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_RawMaterialStock_RawMaterialStockManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/RawMaterialStock';

        var state = {
            name: screenStateName + '.Newedit',
            url: '/Newedit',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/RawMaterialStockManage-Newedit.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageNeweditctrl.Tips_33'
            }
        };
        $stateProvider.state(state);
    }
}());
