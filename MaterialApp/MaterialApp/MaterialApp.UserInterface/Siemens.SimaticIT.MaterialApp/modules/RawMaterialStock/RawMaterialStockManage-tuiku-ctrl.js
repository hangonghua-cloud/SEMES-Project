(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.RawMaterialStock').config(tuikuScreenStateConfig);

    tuikuScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function tuikuScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService,
        busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions1();
            initGridOptions2();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;
            self.selectedItem1 = null;
            self.selectedItem2 = null;
            self.searchParams = {};
            self.index = 0;
            self.PostDate = new Date();

            initDictionary();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.search = search;
            self.addForm = addForm;
            self.deleteForm = deleteForm;
            self.typeFactoryChange = typeFactoryChange;
            self.selectClick1 = selectClick1;
            self.selectClick2 = selectClick2;
        }

        function initDictionary() {

            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2')
                    });
                    initGridData1();
                }
            });

            //物料小类
            self.typeSmall = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ItemValue: "" }]
            }
            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.typeSmall.options = res.data.resultData;
                    // self.typeOutMode.options.splice(0, 0, { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ItemValue: "" });
                    // self.typeOutMode.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ItemValue: "" };
                }
            })


            //出库方式
            self.typeOutMode = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ItemValue: "" }]
            }
            commonService.getDataItemDuatil("OutMode").then(function (res) {
                if (res && res.data.success) {
                    self.typeOutMode.options = res.data.resultData;
                    // self.typeOutMode.options.splice(0, 0, { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ItemValue: "" });
                    // self.typeOutMode.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ItemValue: "" };
                }
            })

            //车间
            self.typeWorkShop = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ResourceCode: "" }]
            };
            //仓库
            self.typeWarehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ResourceCode: "" }]
            };
            //库位
            self.typeLocation = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ResourceCode: "" }]
            };

        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                //车间
                commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeWorkShop.options = res.data.resultData;
                        self.typeWorkShop.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2')
                        });
                    }
                });
            }
            else {
                //车间
                self.typeWorkShop = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), ResourceCode: "" }]
                };
            }
        }

        //选择仓库
        function selectClick1() {

            if (!self.typeFactory.value.ResourceCode) {
                //请选择工厂
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_3'));
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
                            url: commonService.getMesApiAddress("factory") + 'level/GetWarehouseByFactory',
                            queryParmeters: {
                                Name: "",
                                factoryCode: self.typeFactory.value.ResourceCode
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Post",
                            sidx: "ResourceCode",
                            sord: "asc",
                            columnDefs: [
                                // {
                                //     name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                //         '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                // },
                                {
                                    //仓库编码
                                    field: 'ResourceCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_4'),
                                    width: 200
                                },
                                {
                                    //仓库名称
                                    field: 'ResourceName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_5'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    //请选择科目
                    commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_6'));
                } else {
                    self.currentItem.WhsCode = data[0].ResourceCode;
                    self.currentItem.WhsName = data[0].ResourceName;
                }
            });

        }
        //选择库位
        function selectClick2() {
            if (!self.currentItem.WhsCode) {
                //请选择仓库
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_7'));
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
                                ParentResource: self.currentItem.WhsCode
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Post",
                            sidx: "ResourceCode",
                            sord: "asc",
                            columnDefs: [
                                // {
                                //     name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_2'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                //         '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                // },
                                {
                                    //库位编码
                                    field: 'ResourceCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_8'),
                                    width: 200
                                },
                                {
                                    //库位名称
                                    field: 'ResourceName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_9'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    //请选择科目
                    showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_6'));
                } else {
                    self.currentItem.LocationCode = data[0].ResourceCode;
                    self.currentItem.LocationName = data[0].ResourceName;
                }
            });
        }

        function initGridOptions1() {
            self.gridOptionsItem1 = {
                //分页属性
                enablePagination: true, //是否分页,default为true
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [20, 30, 50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 20, //每页显示个数
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
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'MoveTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_11'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_12'),
                        width: 150
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_13'),
                        width: 200
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_14'),
                        width: 200
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_15'),
                        width: 200
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_16'),
                        width: 100
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_17'),
                        width: 100
                    },
                    {
                        field: 'WorkShopName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_18'),
                        width: 100
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_19'),
                        width: 110
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_20'),
                        width: 80
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_21'),
                        width: 130
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData1();
                    });
                    //行选中事件
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem1 = row.entity;

                        } else {
                            self.selectedItem1 = null;
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

            if (!self.typeFactory.value.ResourceCode) {
                //请选择工厂
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_22'))
                return;
            }

            let Pagination = {
                rows: self.gridOptionsItem1.paginationPageSize,
                page: self.gridOptionsItem1.paginationCurrentPage,
                sidx: 'CreateTime',//仓库编码、物料编码
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.MoveType = self.typeOutMode.value.ItemValue;
            self.searchParams.WorkShopCode = self.typeWorkShop.value.ResourceCode;
            self.searchParams.OutType = "10"; //原材料出库
            var postData = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialOut/GetPageDataTableList';
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    //总条数
                    self.gridOptionsItem1.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsItem1.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem1.data = []
                }
            })
        }
        function search() {
            initGridData1();
        }

        function addForm() {
            if (!self.selectedItem1) {
                //请选择要退库的信息
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_23'));
                return false;
            }
            var data = self.gridOptionsItem2.data;
            var isAny = data.find(t => t.BusinessId == self.selectedItem1.Id);
            if (isAny) {
                //同一行出库信息不能重复退库
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_24'));
                return;
            }
            if (!self.currentItem.Qty) {
                //请填写退料数量
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_25'));
                return;
            }
            if (!self.currentItem.WhsCode) {
                //请选择退料仓库
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_26'));
                return;
            }
            if (!self.currentItem.LocationCode) {
                //请选择退料库位
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_27'));
                return;
            }
            //if (self.currentItem.Qty > self.selectedItem1.Qty) {
            //    //退库不能大于出库数量
            //    commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_28'));
            //    return;
            //}
            if (!self.PostDate) {
                //请选择过账日期
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_29'));
                return;
            }

            self.index = self.index + 1;
            data.push({
                index: self.index,
                FactoryCode: self.selectedItem1.FactoryCode,
                FactoryName: self.selectedItem1.FactoryName,
                WhsCode: self.currentItem.WhsCode,
                WhsName: self.currentItem.WhsName,
                LocationCode: self.currentItem.LocationCode,
                LocationName: self.currentItem.LocationName,
                MaterialCode: self.selectedItem1.MaterialCode,
                MaterialName: self.selectedItem1.MaterialName,
                Spec: self.selectedItem1.Spec,
                SmallClass: self.selectedItem1.SmallClass,
                SmallClassName: self.selectedItem1.SmallClassName,
                SupplierCode: self.selectedItem1.SupplierCode,
                SupplierName: self.selectedItem1.SupplierName,
                BatchNo: self.selectedItem1.BatchNo,
                Qty: self.currentItem.Qty,
                Unit: self.selectedItem1.Unit,
                InType: "11", //原材料退库
                MoveType: self.selectedItem1.MoveType,
                WorkShopCode: self.selectedItem1.WorkShopCode,
                WorkShopName: self.selectedItem1.WorkShopName,
                Remark: self.currentItem.Remark,
                PostDate: commonService.ConvertToLocalDate(self.PostDate),
                BusinessId: self.selectedItem1.Id, //出库表Id
                BusinessTable: "MM_RawMaterialOut"
            });
            self.gridOptionsItem2.data = data;
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
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_30'),
                        width: 110
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_31'),
                        width: 150
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_32'),
                        width: 200
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_33'),
                        width: 120
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_34'),
                        width: 120
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_35'),
                        width: 200
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_36'),
                        width: 200
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_37'),
                        width: 150
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_38'),
                        width: 80
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_39'),
                        width: 110
                    },
                    {
                        field: 'PostDate',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_40'),
                        width: 110
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi2 = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem2 = row.entity;

                        } else {
                            self.selectedItem2 = null;
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

            //字典类型 取值参考
            let data = self.gridOptionsItem2.data;
            if (data.length < 1) {
                //没有要保存的行
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_41'));
                return false;
            }

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                data: data
            };
            // debugger
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialStock/RawMaterialStockTuiKu';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_42') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_43'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_44'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_45'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    tuikuScreenStateConfig.$inject = ['$stateProvider'];
    function tuikuScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_RawMaterialStock_RawMaterialStockManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/RawMaterialStock';

        var state = {
            name: screenStateName + '.tuiku',
            url: '/tuiku/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/RawMaterialStockManage-tuiku.html',
                    controller: tuikuScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagetuikuctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
