(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.PMOwnSemiProductOrder.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.PMOwnSemiProductOrder');

            init();
            initGridOptions();
            //初始化子表grid选项
            initGridOptionsDetail();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_PMOwnSemiProductOrder_PMOwnSemiProductOrder';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.searchParams = {};

            self.selectedItemDetail = {};
            self.isDetailButtonVisible = false;
            self.searchParams2 = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;
            self.searchButtonHandler = searchButtonHandler;
            self.importButtonHandler = importButtonHandler;
            self.outWhsButtonHandler = outWhsButtonHandler;

            self.typeFactoryChange = typeFactoryChange;

            //数据字典
            initDictionary();
        }

        //回调函数
        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })

        //回调函数
        $rootScope.$on("to-parentDetail", function (event, data) {
            initGridDataDetail();
        })

        function initDictionary() {

            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_1')
                    });
                    initGridData();
                }
            });
            //  self.typeStatus = {
            //      value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_1'), ItemValue: "" },
            //      options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_1'), ItemValue: "" }]
            //  }

            //  commonService.getDataItemDuatil("typeStatus").then(function (res) {
            //      if (res && res.data.success) {
            //          self.typeStatus.options = res.data.resultData;
            //          // self.typeStatus.options.splice(0, 0, {
            //          //     ItemValue: "",
            //          //     ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_1')
            //          // })
            //      }
            //  })
            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_1'), ResourceCode: "" }]
            };
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeProcess.options = res.data.resultData;
                        self.typeProcess.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_1')
                        });
                    }
                });
            } else {
                self.typeProcess = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_1'), ResourceCode: "" }]
                };
            }
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
                paginationPageSize: 100, //每页显示个数
                paginationCurrentPage: 1, //当前的页码
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_2'), width: 75, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_8'),
                        width: 200
                    },
                    {
                        field: 'ProductQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'PalletNum',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_11'),
                        width: 120
                    },
                    {
                        field: 'VolumeNum',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_12'),
                        width: 120
                    },
                    {
                        field: 'DeliveryQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_13'),
                        width: 120
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_14'),
                        width: 120
                    },
                    {
                        field: 'Meters',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_15'),
                        width: 120
                    },
                    {
                        field: 'OrderStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_16'),
                        width: 120
                    },
                    {
                        field: 'CustomerName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_17'),
                        width: 120
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_18'),
                        width: 120
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_19'),
                        width: 150,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM',
                        //cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
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
                                self.gridOptionsDetail.data = [];
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
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            self.gridOptionsDetail.data = [];

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//创建时间
                sord: 'desc'
            };

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_20'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.ProcessCode = self.typeProcess.value.ResourceCode;
            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PMOwnSemiProductOrder/GetPageDataTableList'
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridOptions.totalItems = res.data.resultData.records;
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_22'));
            });
        }

        //查询
        function searchButtonHandler(clickedCommand) {
            initGridData();
        }

        //新增
        function importButtonHandler(clickedCommand) {
            $state.go(rootstate + '.import');
        }

        //新增
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        //编辑
        function editButtonHandler(clickedCommand) {

            if (self.selectedItem.OrderStatus == "3") {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_23'));
                return;
            }
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { selectedItem: self.selectedItem });
        }

        //查看
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { selectedItem: self.selectedItem });
        }

        //删除
        function deleteButtonHandler(clickedCommand) {

            if (self.selectedItem.OrderStatus != "1") {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_24'));
                return;
            }
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_25');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_26');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("ProduceManage") + 'PMOwnSemiProductOrder/RemoveForm';
                //提交删除当前选择数据实体
                var postData = {
                    id: self.selectedItem.Id
                };
                busyIndicatorService.show();//打开遮罩层
                commonService.callWebApiPost(url, postData).then(function (res) {
                    busyIndicatorService.hide();//关闭遮罩层
                    if ((res) && (res.data.success)) {
                        commonService.showInfo(res.data.returnMsg);
                        initGridData();
                        self.selectedItem = null;
                        self.isButtonVisible = false;
                        //重新刷新列表
                    } else {
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    busyIndicatorService.hide();//关闭遮罩层
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_22'));
                });
            }, title);
        }
        //出库
        function outWhsButtonHandler() {
            // if (self.selectedItem.OrderStatus == "3") {
            //     commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_27'));
            //     return;
            // }
            $state.go(rootstate + '.out');
        }

        //初始化子表grid选项
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
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_28'),
                        width: 120
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_29'),
                        width: 120
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_30'),
                        width: 120
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_31'),
                        width: 120
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_32'),
                        width: 120
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_33'),
                        width: 150,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM',
                        //cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataDetail();
                    });
                    //行选中事件
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {

                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailButtonVisible = true;
                                //console.log (self.selectedItemDetail);

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

        //子表查询方法,数据绑定
        function initGridDataDetail() {
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsDetail.paginationPageSize,
                page: self.gridOptionsDetail.paginationCurrentPage,
                sidx: 'CreateTime',//创建时间
                sord: 'asc'
            };

            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.BusinessId = self.selectedItem.Id;
            }
            else {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_34'), commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_35'));
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialOut/GetPageDataTableList'
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_21'));
            });
        }
    }
    ListScreenRouteConfig.$inject = ['$stateProvider'];
    function ListScreenRouteConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_PMOwnSemiProductOrder';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_PMOwnSemiProductOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PMOwnSemiProductOrder';

        var state = {
            name: moduleStateName + '_PMOwnSemiProductOrder',
            url: '/' + moduleStateUrl + '_PMOwnSemiProductOrder',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/PMOwnSemiProductOrder-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.JS.Tips_36'
            }
        };
        $stateProvider.state(state);
    }
}());
