(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.OwnProduct').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.OwnProduct.OwnProductBG.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.OwnProduct.OwnProductBG');

            init();
            initGridOptions();
            initGridOptionsDetail();
            initPersonGridOptions();
            initMBGridOptions();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_OwnProduct_OwnProductBG';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};
            //子表明细
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            self.isDetailDeleteButtonVisible = false;
            self.viewerOptions2 = {};
            self.viewerData2 = [];
            self.searchParams2 = {};

            //生产人员
            self.selectedPerson = null;
            self.isPersonButtonVisible = false;
            self.isPersonDeleteButtonVisible = false;
            self.searchParams3 = {};
            //物料批次
            self.selectedMB = null;
            self.isMBButtonVisible = false;
            self.searchParams4 = {};

            self.showTab = "1"; //1：生产人员 2：不良记录 3：物料批次绑定记录

            initDictionary();
            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            //不良记录
            self.add2ButtonHandler = add2ButtonHandler;//新增
            self.edit2ButtonHandler = edit2ButtonHandler;//编辑
            self.delete2ButtonHandler = delete2ButtonHandler;//删除
            //生产人员
            self.add3ButtonHandler = add3ButtonHandler;//新增
            self.delete3ButtonHandler = delete3ButtonHandler;//删除

            self.tabPerson = tabPerson;
            self.tabBadItem = tabBadItem;
            self.tabMB = tabMB;
        }
        function initDictionary() {
            self.Factory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_1'), ResourceCode: "" }]
            };
            self.WorkOrderStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_1'), ItemValue: "" }]
            };
            self.typeMaterialSmall = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_1'), ItemValue: "" }]
            };

            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.typeMaterialSmall.options = res.data.resultData;
                    self.typeMaterialSmall.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            self.typeMaterialSmall = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_1'), ItemValue: "" }]
            };

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.Factory.value = res.data.resultData[0];
                    }
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_1')
                    });
                    initGridData();
                }
            });
            commonService.getDataItemDuatil("ExeWorkOrderStatus").then(function (res) {
                if (res && res.data.success) {
                    self.WorkOrderStatus.options = res.data.resultData;
                    self.WorkOrderStatus.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
        }
        $rootScope.$on("to-parentPerson", function (event, data) {
            initPersonGridData();
        });
        $rootScope.$on("to-parentBadItem", function (event, data) {
            initGridDataDetail();
        });
        $rootScope.$on("to-parentMB", function (event, data) {
            initMBGridData();
        });

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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_3'),
                        width: 110
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_4'),
                        width: 110
                    },
                    {
                        field: 'OrderType',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_5'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'1\'"><span ng-cell-text>未生产</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'2\'"><span ng-cell-text>正在生产</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'3\'"><span ng-cell-text>已完成</span></div>'
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_9'),
                        width: 110
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_11'),
                        width: 110
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_12'),
                        width: 110
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_13'),
                        width: 110
                    },
                    {
                        field: 'ProcessRouteName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_14'),
                        width: 110
                    },
                    {
                        field: 'BOMCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_15'),
                        width: 110
                    },

                    // {
                    //     field: 'PlanQty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_16'),
                    //     width: 110
                    // },
                    {
                        field: 'TransferCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_17'),
                        width: 130
                    },
                    {
                        field: 'TransferName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_18'),
                        width: 130
                    },
                    {
                        field: 'BGProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_19'),
                        width: 110
                    },
                    {
                        field: 'BGMachineName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_20'),
                        width: 110
                    },
                    {
                        field: 'BGQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_21'),
                        width: 110
                    },
                    // {
                    //     field: 'BGShift',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_22'),
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.OrderType==\'1\'"><span ng-cell-text>早班</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderType==\'2\'"><span ng-cell-text>晚班</span></div>'
                    // },
                    // {
                    //     field: 'UserGroupName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_25'),
                    //     width: 110
                    // },
                    // {
                    //     field: 'BadQty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_26'),
                    //     width: 100
                    // },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_27'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'BGUser',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_28'),
                        width: 100
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
                                if (self.showTab == "1") {
                                    tabPerson();
                                }
                                else if (self.showTab == "2") {
                                    tabBadItem();
                                }
                                else if (self.showTab == "3") {
                                    tabMB();
                                }
                                //子表明细关联
                                initGridDataDetail();//不良信息
                                initPersonGridData();//生产人员记录
                                initMBGridData();//物料批次绑定记录
                            } else {
                                self.selectedItem = null;

                                self.gridOptionsDetail.data = [];
                                self.gridPersonOptions.data = [];
                                self.gridMBOptions.data = [];
                                self.isButtonVisible = false;
                                self.isPersonButtonVisible = false;
                                self.isPersonDeleteButtonVisible = false;
                                self.isDetailButtonVisible = false;
                                self.isDetailDeleteButtonVisible = false;
                                self.isMBButtonVisible = false;
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
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_29'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//创建时间
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.Factory.value.ResourceCode;

            self.searchParams.OrderType = self.WorkOrderStatus.value.ItemValue;//工单状态
            self.searchParams.MaterialClass = self.typeMaterialSmall.value.ItemValue;//物料小类

            if (self.StartTime && self.EndTime) {
                self.searchParams.StartTime = commonService.ConvertToLocalTime(self.StartTime);
                self.searchParams.EndTime = commonService.ConvertToLocalTime(self.EndTime);
            } else {
                self.searchParams.StartTime = "";
                self.searchParams.EndTime = "";

            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress('ProduceManage') + 'PM_OwnProductBG/PM_OwnProductBGPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridOptions.totalItems = res.data.resultData.records;
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_30'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
        }

        //新增
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        //编辑
        function editButtonHandler(clickedCommand) {
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_31');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_32');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('ProduceManage') + 'PM_TranferCardBGRecord/RemovePM_TranferCardBGRecord';
                var postData = {
                    Entity: self.selectedItem
                };
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridData();
                        self.selectedItem = null;
                        self.isButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_30'));
                });
            }, title);
        }

        //初始化子表grid选项(不良信息)
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'BadItemCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_33'),
                        width: 200
                    },
                    {
                        field: 'BadItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_34'),
                        width: 200
                    },
                    {
                        field: 'BadQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_26'),
                        width: 200
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridBadItemApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataDetail();
                    });
                    //行选中事件
                    $scope.gridBadItemApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailDeleteButtonVisible = true;
                            } else {
                                self.selectedItemDetail = null;
                                self.isDetailDeleteButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //子表查询方法,数据绑定(不良信息)
        function initGridDataDetail() {
            self.selectedItemDetail = null;
            // self.isDetailButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsDetail.paginationPageSize,
                page: self.gridOptionsDetail.paginationCurrentPage,
                sidx: 'CreateTime',//报工ID
                sord: 'asc'
            };
            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.BGID = self.selectedItem.Id;
            }
            else {
                self.gridOptionsDetail.data = [];
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress('ProduceManage') + 'PM_BGBadRecord/PM_BGBadRecordPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_30'));
            });
        }

        //查询
        function search2ButtonHandler() {
            initGridDataDetail();
        }

        //新增
        function add2ButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addBad', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //编辑
        function edit2ButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.editBad', { id: self.selectedItemDetail.Id, selectedItem: self.selectedItemDetail });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.selectBad', { id: self.selectedItemDetail.Id, selectedItem: self.selectedItemDetail });
        }

        //删除 事件
        function delete2ButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_31');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_32');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('ProduceManage') + 'PM_BGBadRecord/RemovePM_BGBadRecordOwnProduct';

                var postData = {
                    Entity: self.selectedItemDetail
                };
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        if (!!self.selectedItem) {
                            self.selectedItem.BadQty = self.selectedItem.BadQty - self.selectedItemDetail.BadQty
                        }
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridDataDetail();
                        self.selectedItemDetail = null;
                        self.isDetailDeleteButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);

                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_30'));
                });
            }, title);
        }
        //生产人员
        function initPersonGridOptions() {
            self.gridPersonOptions = {
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'PTeamCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_35'),
                        width: 200
                    },
                    {
                        field: 'PTeamName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_36'),
                        width: 200
                    },
                    {
                        field: 'PostName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_37'),
                        width: 200
                    },

                    {
                        field: 'UserCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_38'),
                        width: 200
                    },
                    {
                        field: 'UserName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_39'),
                        width: 200
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridPersonApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initPersonGridData();
                    });
                    //行选中事件
                    $scope.gridPersonApi.selection.on.rowSelectionChanged($scope, function (row, event) {

                        if (row) {
                            if (row.isSelected) {
                                self.selectedPerson = row.entity;
                                self.isPersonDeleteButtonVisible = true;
                                //console.log (self.selectedItem);
                            } else {
                                self.selectedPerson = null;
                                self.isPersonDeleteButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //查询方法,数据绑定，生产人员
        function initPersonGridData() {
            self.selectedPerson = null;
            // self.isPersonButtonVisible = false;
            let Pagination = {
                rows: self.gridPersonOptions.paginationPageSize,
                page: self.gridPersonOptions.paginationCurrentPage,
                sidx: 'UserCode',//人员编码
                sord: 'asc'
            };

            if (self.selectedItem != null) {
                //关联字段
                self.searchParams3.BGID = self.selectedItem.Id;
            }
            else {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_40'), commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_41'));
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams3
            };

            var url = commonService.getMesApiAddress('ProduceManage') + 'PM_TransferBGPersonRecord/PM_TransferBGPersonRecordPageDataTableList';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridPersonOptions.totalItems = res.data.resultData.records;
                    self.gridPersonOptions.data = res.data.resultData.rows;
                } else {
                    self.gridPersonOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_30'));
            });
        }
        //新增人员
        function add3ButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addUser', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }
        //删除人员 事件
        function delete3ButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_31');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_32');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_TransferBGPersonRecord/RemovePM_TransferBGPersonRecord';
                var postData = {
                    Entity: self.selectedPerson
                };
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initPersonGridData();
                        self.selectedPerson = null;
                        self.isPersonDeleteButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_30'));
                });
            }, title);
        }
        //初始化grid选项(物料批次绑定)
        function initMBGridOptions() {
            self.gridMBOptions = {
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_11'),
                        width: 200
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_12'),
                        width: 200
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_13'),
                        width: 200
                    },
                    //{
                    //    field: 'MaterialGroup',
                    //    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_42'),
                    //    width: 200
                    //},
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_43'),
                        width: 200
                    },
                    {
                        field: 'RecoilQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_44'),
                        width: 200
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridMBApi = gridApi;
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        initMBGridData();
                    });
                    $scope.gridMBApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedMB = row.entity;
                                self.isMBButtonVisible = true;
                            } else {
                                self.selectedMB = null;
                                self.isMBButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //查询方法,数据绑定(物料批次绑定)
        function initMBGridData() {
            self.selectedMB = null;
            self.isMBButtonVisible = false;
            let Pagination = {
                rows: self.gridMBOptions.paginationPageSize,
                page: self.gridMBOptions.paginationCurrentPage,
                sidx: 'CreateTime',//
                sord: 'asc'
            };
            if (self.selectedItem != null) {
                //关联字段
                self.searchParams4.BGID = self.selectedItem.Id;
            }
            else {
                self.gridMBOptions.data = [];
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams4
            };

            var url = commonService.getMesApiAddress('ProduceManage') + 'PM_MaterialBatchConsumeRecord/PM_MaterialBatchConsumeRecordPageList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridMBOptions.totalItems = res.data.resultData.records;
                    self.gridMBOptions.data = res.data.resultData.rows;
                } else {
                    self.gridMBOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_30'));
            });
        }

        function tabPerson() {
            if (!!self.selectedItem) self.isPersonButtonVisible = true;

            self.isDetailButtonVisible = false;
            self.isDetailDeleteButtonVisible = false;
            self.isMBButtonVisible = false;
            self.showTab = "1";
        }
        function tabBadItem() {

            if (!!self.selectedItem) self.isDetailButtonVisible = true;
            self.isPersonButtonVisible = false;
            self.isPersonDeleteButtonVisible = false;
            self.isMBButtonVisible = false;
            self.showTab = "2";
        }

        function tabMB() {
            if (!!self.selectedItem) self.isMBButtonVisible = true;
            self.isDetailButtonVisible = false;
            self.isPersonButtonVisible = false;
            self.showTab = "3";
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_OwnProduct';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_OwnProduct';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/OwnProduct';

        var state = {
            name: moduleStateName + '_OwnProductBG',
            url: '/' + moduleStateUrl + '_OwnProductBG',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/OwnProductBG-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.OwnProductBG.JS.Tips_45'
            }
        };
        $stateProvider.state(state);
    }
}());
