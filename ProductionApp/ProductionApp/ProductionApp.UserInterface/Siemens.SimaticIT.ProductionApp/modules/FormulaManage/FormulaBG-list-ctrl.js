(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.FormulaManage').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.FormulaManage.FormulaBG.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.FormulaManage.FormulaBG');

            init();
            initGridOptions();
            initGridOptionsDetail();
            initGridOptions2Detail();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_FormulaManage_FormulaBG';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            initDictionary();

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            self.tabSwitch = "1";

            //子表明细
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            self.viewerOptions2 = {};
            self.viewerData2 = [];
            self.searchParams2 = {};

            //唛头
            self.selectedItem2Detail = null;
            self.isDetail2ButtonVisible = false;
            self.viewerOptions3 = {};
            self.viewerData3 = [];
            self.searchParams3 = {};

            //Expose Model Methods

            self.editButtonHandler = editButtonHandler;//报工
            self.searchButtonHandler = searchButtonHandler;


            self.tab1Click = tab1Click;
            self.tab2Click = tab2Click;
        }
        function tab1Click() {
            self.tabSwitch = "1";
            initGridDataDetail();
        }
        function tab2Click() {
            self.tabSwitch = "2";
            initGridData2Detail();
        }

        function initDictionary() {
            //物料小类
            self.SmallClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_1'), ItemValue: "" }]
            };

            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.SmallClass.options = res.data.resultData;
                    self.SmallClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })

            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_1')
                    });
                    initGridData();
                }
            });
        }


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
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
                paginationPageSize: 300, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'OrderType',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_4'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'1\'"><span ng-cell-text>出口</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'2\'"><span ng-cell-text>内销</span></div>'
                    },

                    {
                        field: 'BOMCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_7'),
                        width: 160
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_8'),
                        width: 200
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_9'),
                        width: 260
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_10'),
                        width: 260
                    },

                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_11'),
                        width: 260,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },


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
                                //console.log (self.selectedItem);
                                //子表明细关联
                                if (self.tabSwitch == "1") {
                                    initGridDataDetail();
                                } else {
                                    initGridData2Detail();
                                }

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
            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//创建时间
                sord: 'desc'
            };

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_12'));
                return;
            }

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.SmallClass = self.SmallClass.value.ItemValue;

            if (self.StartTime) {
                self.searchParams.StartTime = commonService.ConvertToLocalTime(self.StartTime);
            }
            else {
                self.searchParams.StartTime = "";
            }
            if (self.EndTime) {
                self.searchParams.EndTime = commonService.ConvertToLocalTime(self.EndTime);
            } else {
                self.searchParams.EndTime = "";
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("material") + 'BS_BOM/GetFormulaBOM';

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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_13'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
            initGridDataDetail();
            initGridData2Detail();
        }


        //报工
        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.add', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

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
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
                paginationPageSize: 300, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_14'),
                        width: 120
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_15'),
                        width: 120
                    },
                    {
                        field: 'BOMCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'Num',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_16'),
                        width: 100
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_17'),
                        width: 100
                    },
                    {
                        field: 'WarehouseName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_18'),
                        width: 140
                    },
                    {
                        field: 'ProcessCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_19'),
                        width: 130
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_20'),
                        width: 130
                    },


                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApiDetail = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataDetail();
                    });
                    //行选中事件
                    $scope.gridApiDetail.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailButtonVisible = true;
                                //console.log (self.selectedItemDetail);
                                //子表明细关联
                                //initGridDataDetail();
                            } else {
                                self.selectedItemDetail = null;
                                self.isDetailButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        function initGridDataDetail() {
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsDetail.paginationPageSize,
                page: self.gridOptionsDetail.paginationCurrentPage,
                sidx: 'CreateTime',//报工时间
                sord: 'desc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.BOMId = self.selectedItem.Id;
            }
            else {
                self.gridOptionsDetail.data = [];
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };

            var url = commonService.getMesApiAddress("material") + 'BS_BOMItems/BS_BOMItemsPageDataTableList';

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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_13'));
            });
        }

        function initGridOptions2Detail() {
            self.gridOptionsDetail2 = {
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
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
                paginationPageSize: 300, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    // {
                    //     field: 'FactoryCode', 
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_21'),
                    //     width: 200
                    // },
                    {
                        field: 'FormulaTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'FormulaCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_9'),
                        width: 110
                    },
                    {
                        field: 'FormulaName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'FormulaTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_22'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'MachineName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_23'),
                        width: 110
                    },
                    {
                        field: 'FormulaNum',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_24'),
                        width: 110
                    },
                    // {
                    //     field: 'PTeamName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_25'),
                    //     width: 110
                    // },
                    {
                        field: 'UserNames',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_26'),
                        width: 110
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_27'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },

                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApiDetail = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData2Detail();
                    });
                    //行选中事件
                    $scope.gridApiDetail.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItem2Detail = row.entity;
                                self.isDetail2ButtonVisible = true;
                                //console.log (self.selectedItemDetail);
                                //子表明细关联
                                //initGridDataDetail();
                            } else {
                                self.selectedItem2Detail = null;
                                self.isDetail2ButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        function initGridData2Detail() {
            self.selectedItem2Detail = null;
            self.isDetailButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsDetail2.paginationPageSize,
                page: self.gridOptionsDetail2.paginationCurrentPage,
                sidx: 'CreateTime',//报工时间
                sord: 'desc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams3.BOMId = self.selectedItem.Id;
            }
            else {
                self.gridOptionsDetail2.data = [];
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams3
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_FormulaRecord/PM_FormulaRecordPageDataTableList';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {

                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsDetail2.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsDetail2.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsDetail2.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_13'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_FormulaManage';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_FormulaManage';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/FormulaManage';

        var state = {
            name: moduleStateName + '_FormulaBG',
            url: '/' + moduleStateUrl + '_FormulaBG',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/FormulaBG-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.FormulaBG.JS.Tips_28'
            }
        };
        $stateProvider.state(state);
    }
}());
