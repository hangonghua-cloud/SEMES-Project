(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.SuperProductStock').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStock.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStock');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();
            initGridOptionsDetail();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_SuperProductStock_SuperProductStock';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;



            //Initialize Model Data
            self.selectedItem = {};
            self.isButtonVisible = false;
            self.searchParams = {};

            self.selectedItemDetail = {};
            self.isButtonVisibleDetail = false;
            self.searchParams2 = {};

            //Expose Model Methods
            self.searchButtonHandler = searchButtonHandler;//查询

            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
            initDictionary();
            self.FactoryChange = FactoryChange;
        }

        function initDictionary() {
            self.Factory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_1'), ResourceCode: "" }]
            };
            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_1'), ResourceCode: "" }]
            };
            //仓库
            //self.Warehouse = {
            //    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_1'), ResourceCode: "" },
            //    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_1'), ResourceCode: "" }]
            //};
            //工厂
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.Factory.value = res.data.resultData[0];
                    }
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_1')
                    });
                    initGridData();
                }
            });
            //仓库
            //commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
            //    if (res && res.data.success) {
            //        self.Warehouse.options = res.data.resultData;
            //        self.Warehouse.options.splice(0, 0, {
            //            ResourceCode: "",
            //            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_1')
            //        });
            //    }
            //});
        }

        function FactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.Process.options = res.data.resultData;
                        self.Process.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_1')
                        });
                    }
                });
            }
            else {
                self.Process = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_1'), ResourceCode: "" }]
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
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_3'),
                        width: 110
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_5'),
                        width: 150
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_6'),
                        width: 200
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_7'),
                        width: 100
                    },
                    //{
                    //    field: 'WhsName',
                    //    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_8'),
                    //    width: 150
                    //},
                    {
                        field: 'StockQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'LockedQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'NoLockQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_11'),
                        width: 120
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

            if (!self.Factory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_12'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'ProcessCode,MaterialCode',//工厂编码
                sord: 'asc'
            };

            self.searchParams.FactoryCode = self.Factory.value.ResourceCode;
            self.searchParams.ProcessCode = self.Process.value.ResourceCode;
            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'MM_SuperProductStock/GetPageDataTableMList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_13'));
            });
        }

        //查询
        function searchButtonHandler(clickedCommand) {
            initGridData();
            initGridDataDetail();
        }

        //初始化grid选项
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
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_5'),
                        width: 150
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_6'),
                        width: 200
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_7'),
                        width: 100
                    },
                    //{
                    //    field: 'WhsName',
                    //    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_8'),
                    //    width: 150
                    //},
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_14'),
                        width: 120
                    },
                    {
                        field: 'StockQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'LockedQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'NoLockQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_11'),
                        width: 120
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
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isButtonVisibleDetail = true;
                                //console.log (self.selectedItem);
                            } else {
                                self.selectedItemDetail = null;
                                self.isButtonVisibleDetail = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //查询方法,数据绑定明细
        function initGridDataDetail() {
            self.selectedItemDetail = null;
            self.isButtonVisibleDetail = false;
            let Pagination = {
                rows: self.gridOptionsDetail.paginationPageSize,
                page: self.gridOptionsDetail.paginationCurrentPage,
                sidx: 'ProcessCode,MaterialCode,BatchNo',//工厂编码
                sord: 'asc'
            };
            if (!!self.selectedItem) {
                self.searchParams2.ProcessCode = self.selectedItem.ProcessCode;
                self.searchParams2.MaterialCode = self.selectedItem.MaterialCode;
            } else {
                self.gridOptionsDetail.data = [];
                return;
            }


            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress("material") + 'MM_SuperProductStock/GetPageDataTableDList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_13'));
            });
        }

        function onGridItemSelectionChanged(items, item) {
            if (item && item.selected == true) {
                self.selectedItem = item;
                setButtonsVisibility(true);
            } else {
                self.selectedItem = null;
                setButtonsVisibility(false);
            }
        }
        // Internal function to make item-specific buttons visible
        function setButtonsVisibility(visible) {
            self.isButtonVisible = visible;
        }
    }

    ListScreenRouteConfig.$inject = ['$stateProvider'];
    function ListScreenRouteConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_SuperProductStock';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_SuperProductStock';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/SuperProductStock';

        var state = {
            name: moduleStateName + '_SuperProductStock',
            url: '/' + moduleStateUrl + '_SuperProductStock',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/SuperProductStock-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStocklistctrl.Tips_15'
            }
        };
        $stateProvider.state(state);
    }
}());
