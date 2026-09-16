(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.ProductStock').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.ProductStock.ProductStock.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $interval) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions1();
            initGridOptions2();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_1'));
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
            self.OldData = [];
            self.validInputs = false;
            self.selectedItem1 = null;
            self.selectedItem2 = null;
            self.IsShowButten = false;
            self.IsShowDeleteButten = false;
            self.searchParams = {};
            self.index = 0;

            initDictionary();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.search = search;
            self.addForm = addForm;
            self.deleteForm = deleteForm;
            self.WarehouseChange = WarehouseChange;
            self.typeFactoryChange = typeFactoryChange;
            self.selectClick = selectClick;
        }

        function initDictionary() {
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_2')
                    });
                    initGridData1();
                }
            });
            self.typeWarehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_2'), ResourceCode: "" }]
            };
            self.typeLocation = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_2'), ResourceCode: "" }]
            };

            // //仓库
            // commonService.get_ResourceExtendByLevelField({ LevelCode: "Warehouse", FieldCode: "CKLX", FieldValue: "3" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeWarehouse.options = res.data.resultData;
            //         self.typeWarehouse.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_2')
            //         });
            //     }
            // });

        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                let query = {
                    factoryCode: newItem.ResourceCode,
                    fieldCode: "CKLX",
                    fieldValue: "3"
                }
                commonService.getWarehouseByFactoryExtendInfo(query).then(function (res) {
                    if (res && res.data.success) {
                        self.typeWarehouse.options = res.data.resultData;
                        self.typeWarehouse.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_2')
                        });
                    }
                });
            } else {
                self.typeWarehouse = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_2'), ResourceCode: "" }]
                };
            }
        }

        function WarehouseChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeLocation.options = res.data.resultData;
                    self.typeLocation.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_2')
                    });
                }
            });
        }

        function selectClick() {

            if (!self.typeWarehouse.value.ResourceCode)
                return;

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
                                ParentResource: self.typeWarehouse.value.ResourceCode
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Post",
                            sidx: "ResourceCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditctrl.Tips_2'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ResourceCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditctrl.Tips_3'),
                                    width: 200
                                },
                                {
                                    field: 'ResourceName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditctrl.Tips_4'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    commonservice.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditctrl.Tips_5'));
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
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
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
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_4'),
                        width: 130
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_5'),
                        width: 80
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_6'),
                        width: 130
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_7'),
                        width: 130
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_9'),
                        width: 130
                    },
                    // {
                    //     field: 'PalletQty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_10'),
                    //     width: 130
                    // },
                    // {
                    //     field: 'BoxQty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_11'),
                    //     width: 130
                    // },
                    {
                        field: 'CPBZTPSL',
                        displayName: '单托片数',
                        width: 130
                    },
                    {
                        field: 'PieceQty',
                        displayName: '片数',
                        width: 130
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_3'),
                        width: 100
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_8'),
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

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_12'))
                return;
            }
            let Pagination = {
                rows: self.gridOptionsItem1.paginationPageSize,
                page: self.gridOptionsItem1.paginationCurrentPage,
                sidx: 'CreateTime',//订单号、柜号
                sord: 'asc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.Status = "DRK";
            var postData = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'MM_ProductStock/GetProductStockPageDataTableMainList';
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    //总条数
                    self.gridOptionsItem1.totalItems = res.data.resultData.records;
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
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_13'), commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_14'));
                return false;
            }
            var data = self.gridOptionsItem2.data;
            var ent = data.find(t => t.WorkOrder == self.selectedItem1.WorkOrder);
            if (ent != null) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_15'), commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_14'));
                return false;
            }
            if (self.currentItem.PieceQty > self.selectedItem1.PieceQty) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_16'));
                return;
            }

            // let pieceQty = self.currentItem.PalletQty * self.selectedItem1.PerPalletPieceQty;
            // let boxQty = self.currentItem.PalletQty * self.selectedItem1.PerPalletBoxQty;

            self.index = self.index + 1;

            let newEntity = angular.copy(self.selectedItem1);
            newEntity.index = self.index;
            newEntity.WhsCode = self.typeWarehouse.value.ResourceCode;
            newEntity.WhsName = self.typeWarehouse.value.ResourceName;
            // newEntity.LocationName = self.typeLocation.value.ResourceName;
            // newEntity.LocationCode = self.typeLocation.value.ResourceCode;
            newEntity.LocationCode = self.currentItem.LocationCode;
            newEntity.LocationName = self.currentItem.LocationName;
            // newEntity.PalletQty = self.currentItem.PalletQty;
            // newEntity.BoxQty = boxQty;
            // newEntity.PieceQty = pieceQty;
            newEntity.PieceQty = self.currentItem.PieceQty;
            newEntity.Remark = self.currentItem.Remark;
            data.push(newEntity);

            let oldEntity = angular.copy(self.selectedItem1);
            // oldEntity.PalletQty = self.selectedItem1.PalletQty - self.currentItem.PalletQty;
            // oldEntity.BoxQty = self.selectedItem1.BoxQty - boxQty;
            // oldEntity.PieceQty = self.selectedItem1.PieceQty - pieceQty;
            oldEntity.PieceQty = self.selectedItem1.PieceQty - self.currentItem.PieceQty;
            self.OldData.push(oldEntity);

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
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_17'),
                        width: 120
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_18'),
                        width: 120
                    },
                    // {
                    //     field: 'PalletQty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_10'),
                    //     width: 120
                    // },
                    // {
                    //     field: 'BoxQty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_11'),
                    //     width: 120
                    // },
                    {
                        field: 'PieceQty',
                        displayName: '片数',
                        width: 120
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_19'),
                        width: 160
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

            //字典类型 取值参考
            let data = self.gridOptionsItem2.data;
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_20'), commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_14'));
                return false;
            }


            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                OldData: self.OldData,
                Entity: data
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_21') });
            var url = commonService.getMesApiAddress("material") + 'MM_ProductStock/SaveBatchMM_ProductStock';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_22'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_14'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_14'));
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
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_ProductStock_ProductStock';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/ProductStock';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductStock-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockaddctrl.Tips_23'
            }
        };
        $stateProvider.state(state);
    }
}());
