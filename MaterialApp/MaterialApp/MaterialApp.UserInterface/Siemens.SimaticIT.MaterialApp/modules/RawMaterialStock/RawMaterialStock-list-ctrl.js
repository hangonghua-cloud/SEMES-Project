(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.RawMaterialStock').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStock.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', 'i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, i18nService) {
        var self = this;
        var logger, rootstate, messageservice, backendService;
        i18nService.setCurrentLang('zh-cn');

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStock');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();
            initGridOptionsDetail();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_RawMaterialStock_RawMaterialStock';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            self.selectedItemDetail = null;
            self.isButtonVisibleDetail = false;
            self.viewerOptions2 = {};
            self.viewerData2 = [];
            self.searchParams2 = {};

            //初始化 是否启用
            self.IsEnabled = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_1'), ResourceCode: "" },
                { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_2'), ResourceCode: "1" },
                { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_3'), ResourceCode: "0" }
                ]
            };
            //Expose Model Methods
            self.printButtonHandler = printButtonHandler;//打印
            self.frozenButtonHandler = frozenButtonHandler;//冻结
            self.cancelFrozenButtonHandler = cancelFrozenButtonHandler;//解冻
            self.searchButtonHandler = searchButtonHandler;//查询

            self.typeFactoryChange = typeFactoryChange;//工厂change事件

            initDictionary();
        }

        function initDictionary() {
            // //仓库
            self.Warehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_1'), ResourceCode: "" }]
            };
            // let query = {
            //     LevelCode: "Warehouse",
            //     FieldCode: "CKLX",
            //     FieldValue: "1"
            // };
            // commonService.get_ResourceExtendByLevelField(query).then(function (res) {
            //     if (res && res.data.success) {
            //         self.Warehouse.options = res.data.resultData;
            //         self.Warehouse.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_1')
            //         });
            //     }
            // });
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_1')
                    });
                    initGridData();
                }
            });
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                let query = {
                    factoryCode: newItem.ResourceCode,
                    fieldCode: "CKLX",
                    fieldValue: "1"
                }
                commonService.getWarehouseByFactoryExtendInfo(query).then(function (res) {
                    if (res && res.data.success) {
                        self.Warehouse.options = res.data.resultData;
                        self.Warehouse.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_1')
                        });
                    }
                });
            } else {
                self.Warehouse = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_1'), ResourceCode: "" }]
                };
            }
        }

        //初始化grid选项
        function initGridOptions() {
            self.gridOptions = {
                fastWatch: true,
                rowHeight: 35,
                minimumColumnSize: 100,
                enableMultiSelection: false,
                enableFiltering: false,
                //基础属性
                enableSorting: true,//是否支持排序(列)
                useExternalSorting: false,//是否支持自定义的排序规则
                enableGridMenu: false,//是否显示表格 菜单
                showGridFooter: false,//时候显示表格的footer
                enableHorizontalScrollbar: 1,//表格的水平滚动条
                enableVerticalScrollbar: 1,//表格的垂直滚动条 (两个都是 1-显示,0-不显示)
                selectionRowHeaderWidth: 30,
                enableCellEditOnFocus: false,//default为false,true的时候单击即可打开编辑(cellEdit为true的时候,需要引入'ui.grid.cellNav')
                //分页属性
                enablePagination: true, //是否分页,default为true
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [20, 30, 50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 20, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: false, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_4'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_5'),
                        width: 100
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_6'),
                        width: 100
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_7'),
                        width: 150
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_8'),
                        width: 200
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_9'),
                        width: 80
                    },
                    {
                        field: 'ActualQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_10'),
                        width: 150,
                        cellTemplate:
                            '<div class="ngCellText" style="background:#FF0000" ng-if="row.entity.SafetyQty > row.entity.ActualQty">{{row.entity.ActualQty}}</div>'
                    },
                    {
                        field: 'KWQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_11'),
                        width: 110
                    },
                    {
                        field: 'FKWQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_12'),
                        width: 130
                    },
                    {
                        field: 'SafetyQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_13'),
                        width: 200

                    },
                    {
                        field: 'isLower',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_14'),
                        width: 200,
                        cellTemplate:
                            '<div class="ngCellText" style="padding-left:5px;height:30px;line-height:30px;" ng-if="row.entity.isLower==1">是</div>' +
                            '<div class="ngCellText" style="padding-left:5px;height:30px;line-height:30px;" ng-if="row.entity.isLower==2">否</div>'
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData();
                    });
                    //行选中事件
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItem = row.entity;
                                self.isButtonVisible = true;
                                initGridDataDetail();
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //查询方法,数据绑定
        function initGridData() {
            self.selectedItem = null;
            self.isButtonVisible = false;

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_15'))
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'WhsCode,MaterialCode',//仓库编码、物料编码
                sord: 'asc'
            };

            self.searchParams.isLower = self.IsEnabled.value.ResourceCode;
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.WhsCode = self.Warehouse.value.ResourceCode;
            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialStock/GetPageDataTableMList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_16'));
            });
            self.selectedItemDetail = {};
            self.gridOptionsDetail.data = [];
        }

        //查询
        function searchButtonHandler() {
            initGridData();
        }

        //初始化gridDetail选项
        function initGridOptionsDetail() {
            self.gridOptionsDetail = {
                fastWatch: true,
                rowHeight: 35,
                minimumColumnSize: 100,
                enableMultiSelection: false,
                enableFiltering: false,
                //基础属性
                enableSorting: true,//是否支持排序(列)
                useExternalSorting: false,//是否支持自定义的排序规则
                enableGridMenu: false,//是否显示表格 菜单
                showGridFooter: false,//时候显示表格的footer
                enableHorizontalScrollbar: 1,//表格的水平滚动条
                enableVerticalScrollbar: 1,//表格的垂直滚动条 (两个都是 1-显示,0-不显示)
                selectionRowHeaderWidth: 30,
                enableCellEditOnFocus: false,//default为false,true的时候单击即可打开编辑(cellEdit为true的时候,需要引入'ui.grid.cellNav')
                //分页属性
                enablePagination: true, //是否分页,default为true
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [20, 30, 50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 20, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: false, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: true, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: true,// 是否可以选择多个,默认为true;
                noUnselect: false,//defaultr为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_4'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_6'),
                        width: 100
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_7'),
                        width: 150
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_8'),
                        width: 200
                    },
                    {
                        field: 'SupplierName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_17'),
                        width: 150
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_18'),
                        width: 150
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_9'),
                        width: 70
                    },
                    {
                        field: 'ActualQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_10'),
                        width: 130
                    },
                    {
                        field: 'KWQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_11'),
                        width: 110
                    },
                    {
                        field: 'FKWQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_12'),
                        width: 130
                    },
                    {
                        field: 'IsFrozen',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_19'),
                        width: 130
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridDetailApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataDetail();
                    });
                    //行选中事件
                    $scope.gridDetailApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        let len = $scope.gridDetailApi.selection.getSelectedRows().length;
                        if (row) {
                            if (len > 0) {
                                self.isButtonVisibleDetail = true;
                            }
                            else {
                                self.isButtonVisibleDetail = false;
                            }
                        }
                    });
                    //全选事件enableSelectAll（在grid上选中全选时触发）
                    $scope.gridDetailApi.selection.on.rowSelectionChangedBatch($scope, function (allRow, event) {
                        debugger;
                        let len = $scope.gridDetailApi.selection.getSelectedRows().length;
                        if (len > 0) {
                            self.isButtonVisibleDetail = true;
                        }
                        else {
                            self.isButtonVisibleDetail = false;
                        }
                    });
                },
                data: []
            }
        }

        //查询方法,数据绑定
        function initGridDataDetail() {
            self.selectedItemDetail = null;
            self.isButtonVisibleDetail = false;
            let Pagination = {
                rows: self.gridOptionsDetail.paginationPageSize,
                page: self.gridOptionsDetail.paginationCurrentPage,
                sidx: 'WhsCode,MaterialCode,BatchNo',//仓库编码、物料编码、批次
                sord: 'asc'
            };

            self.searchParams2.WhsCode = self.selectedItem.WhsCode;
            self.searchParams2.MaterialCode = self.selectedItem.MaterialCode;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialStock/GetPageDataTableDList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsDetail.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsDetail.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsDetail.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_16'));
            });
        }

        //打印
        function printButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //冻结 事件
        function frozenButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_20');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_21');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialStock/RawMaterialStockFrozen';
                //提交删除当前选择数据实体
                var postData = {
                    status: "1",
                    Entity: $scope.gridDetailApi.selection.getSelectedRows()
                };
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridDataDetail();
                        self.selectedItemDetail = {};
                        self.isButtonVisibleDetail = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_16'));
                });
            }, title);
        }
        //解冻 事件
        function cancelFrozenButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_22');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_23');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialStock/RawMaterialStockFrozen';
                //提交删除当前选择数据实体
                var postData = {
                    status: "0",
                    Entity: $scope.gridDetailApi.selection.getSelectedRows()
                };
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_24'));
                        //重新刷新列表
                        initGridDataDetail();
                        self.selectedItemDetail = {};
                        self.isButtonVisibleDetail = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_16'));
                });
            }, title);
        }
    }

    ListScreenRouteConfig.$inject = ['$stateProvider'];
    function ListScreenRouteConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_RawMaterialStock';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_RawMaterialStock';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/RawMaterialStock';

        var state = {
            name: moduleStateName + '_RawMaterialStock',
            url: '/' + moduleStateUrl + '_RawMaterialStock',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/RawMaterialStock-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStocklistctrl.Tips_25'
            }
        };
        $stateProvider.state(state);
    }
}());
