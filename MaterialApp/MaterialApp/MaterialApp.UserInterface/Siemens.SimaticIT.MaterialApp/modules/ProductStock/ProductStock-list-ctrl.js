(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.ProductStock').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.ProductStock.ProductStock.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', 'i18nService', 'common.services.security.securityService',
        'common.services.security.functionRightModel'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, i18nService, securityService, FunctionRightModel) {
        //国际化 
        i18nService.setCurrentLang('zh-cn');
        var self = this;
        var logger, rootstate, messageservice, backendService;
        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStock');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();
            // initGridOptionsDetail();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_ProductStock_ProductStock';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.searchParams = {};

            self.selectedItemDetail = null;
            self.isButtonVisibleDetail = false;
            self.viewerOptions2 = {};
            self.viewerData2 = [];
            self.searchParams2 = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler; //入库
            self.moveButtonHandler = moveButtonHandler; //移库
            self.move2ButtonHandler = move2ButtonHandler;//调柜
            self.adjustButtonHandler = adjustButtonHandler;//库存校准
            self.searchButtonHandler = searchButtonHandler;

            initDictionary();

            //按钮权限
            self.isProductStockManageInWhs = false; //入库
            self.isProductStockManageAdjust = false; //库存校准

            //定义跟按钮相对应的按钮权限变量，读取到权限信息后，存到变量里
            self.ProductStockManageInWhs = false;//入库
            self.ProductStockManageAdjust = false;//库存校准

            //3.按钮权限
            ButtonAuthInit();
        }

        //按钮权限
        function ButtonAuthInit() {
            var PageName = "ProductStockManage";
            var jo = {
                PageName: PageName
            };

            var url = commonService.getMesApiAddress() + 'Base/GetButtonAuthList';
            //var url = 'http://localhost:49888/' + 'Base/GetButtonAuthList';
            commonService.callWebApiPost(url, jo).then(function (res) {
                self.funRightListModel = [];

                if ((res) && (res.data.success) && res.data.resultData.length > 0) {
                    //数据
                    for (var i = 0; i < res.data.resultData.length; i++) {
                        self.funRightListModel.push(new FunctionRightModel('business_command', '' + res.data.resultData[i].FullName + '', 'invoke'));
                    }
                    securityService.canPerformOp(self.funRightListModel).then(function (data) {
                        if (data) {
                            if (data.length > 0) {
                                for (var i = 0; i < data.length; i++) {

                                    if (data[i].objectName.split('.')[7] == PageName + "InWhs") {
                                        self.ProductStockManageInWhs = data[i].isAccessible;//入库
                                        self.isProductStockManageInWhs = self.ProductStockManageInWhs;
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Adjust") {
                                        self.ProductStockManageAdjust = data[i].isAccessible;//库存校准
                                    }
                                }
                            }
                        }
                    }, function (resError) {
                        backendService.genericError('获取数据出错', resError);
                    });

                } else {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_2'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_3'));
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_4'));
            });
        }

        //按钮权限
        function ButtonVisible(flag) {
            if (flag) {
                self.isProductStockManageAdjust = self.ProductStockManageAdjust;
            }
            else {
                self.isProductStockManageAdjust = false;
            }
        }

        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_1'), ResourceCode: "" }]
            };

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_1')
                    });
                    initGridData();
                }
            });
        }

        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })

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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_3'),
                        width: 120
                    },
                    {

                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_4'),
                        width: 100
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_5'),
                        width: 80
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'CustomerPO',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_7'),
                        width: 220
                    },
                    // {
                    //     field: 'AvoidProduce',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_8'),
                    //     width: 120
                    // },
                    // {
                    //     field: 'FinisthStatus',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_9'),
                    //     width: 120
                    // }, 
                    {
                        field: 'OrderPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_10'),
                        width: 130
                    }, {
                        field: 'OrderBox',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_11'),
                        width: 130
                    }, {
                        field: 'OrderPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_12'),
                        width: 130
                    }, {
                        field: 'DeliveryPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_13'),
                        width: 130
                    }, {
                        field: 'DeliveryBox',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_14'),
                        width: 130
                    }, {
                        field: 'DeliveryPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_15'),
                        width: 130
                    }, {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_16'),
                        width: 120
                    }, {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_17'),
                        width: 100
                    },
                    // {
                    //     field: 'PalletQty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_18'),
                    //     width: 120
                    // },
                    // {
                    //     field: 'BoxQty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_19'),
                    //     width: 120
                    // },
                    {
                        field: 'PieceQty',
                        displayName: '片数',
                        width: 120
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

                                //按钮权限
                                ButtonVisible(true);
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;

                                //按钮权限
                                ButtonVisible(false);
                            }
                        }
                    });
                },
                data: []
            }
        }

        //查询方法,数据绑定
        function initGridData() {
            //按钮权限
            ButtonVisible(false);

            self.selectedItem = null;
            self.isButtonVisible = false;

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_20'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'ProductOrder,ContainerNo',//订单号、柜号
                sord: 'asc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.CKLX = '3';

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'MM_ProductStock/GetProductStockPageDataTableMainList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_21'));
            });
            self.selectedItemDetail = {};
            // self.gridOptionsDetail.data = [];
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_22'),
                        width: 200
                    },
                    {
                        field: 'MarkName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_23'),
                        width: 110
                    },
                    {
                        field: 'PieceQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_24'),
                        width: 130
                    },
                    {
                        field: 'LocationCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_25'),
                        width: 110
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_26'),
                        width: 100
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_27'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'StatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_28'),
                        width: 110
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
                sidx: 'MarkName',//唛头名称
                sord: 'asc'
            };

            self.searchParams2.WorkOrder = self.selectedItem.WorkOrder;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress("material") + 'MM_ProductStock/GetProductStockPageDataTableDetailList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_21'));
            });
        }

        //入库
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        //移库
        function moveButtonHandler(clickedCommand) {
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }
        //调柜
        function move2ButtonHandler(clickedCommand) {
            $state.go(rootstate + '.editDetail', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }
        //库存校准
        function adjustButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.adjust', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
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
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_ProductStock';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_ProductStock';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/ProductStock';

        var state = {
            name: moduleStateName + '_ProductStock',
            url: '/' + moduleStateUrl + '_ProductStock',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ProductStock-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.ProductStock.ProductStocklistctrl.Tips_29'
            }
        };
        $stateProvider.state(state);
    }
}());
