(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.Plan').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.PlanApp.Plan.ProductionOrder.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', 'common.services.security.securityService', 'common.services.security.functionRightModel', 'i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, securityService, FunctionRightModel, i18nService) {
        var self = this;
        var logger, rootstate, messageservice, backendService;
        i18nService.setCurrentLang('zh-cn');

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.PlanApp.Plan.ProductionOrder');

            init();
            initGridOptions();
            initGridOptionsDetail();
            setTimeout(function () {
                initGridData();
            }, 100);//如果查询条件有下拉参数，请调整此值到1000
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_PlanApp_Plan_ProductionOrder';
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
            self.viewerOptions2 = {};
            self.viewerData2 = [];
            self.searchParams2 = {};

            initDictionary();
            //Expose Model Methods
            self.importVCWorderButtonHandler = importVCWorderButtonHandler;
            self.importPrdButtonHandler = importPrdButtonHandler;
            self.importWorderButtonHandler = importWorderButtonHandler;
            self.downloadPrdButtonHandler = downloadPrdButtonHandler;
            self.downloadPrdButtonHandler2 = downloadPrdButtonHandler2;
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑订单
            self.selectButtonHandler = selectButtonHandler;//查看//
            self.deleteButtonHandler = deleteButtonHandler;

            self.delete2ButtonHandler = delete2ButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            self.editMateiralButtonHandler = editMateiralButtonHandler; //生成物料需求
            self.selecMaterialtButtonHandler = selecMaterialtButtonHandler;

            self.editDetailButtonHandler = editDetailButtonHandler;//编辑工单
            self.editStuffingButtonHandler = editStuffingButtonHandler;//编辑装柜
            self.editLockButtonHandler = editLockButtonHandler;//冻结,解冻
            self.editPublishButtonHandler = editPublishButtonHandler;//发布
            self.TransferButtonHandler = TransferButtonHandler;//跨工厂调拨
            self.GetButtonHandler = GetButtonHandler;//获取物料最新属性
            self.search2ButtonHandler = search2ButtonHandler;
            self.DelIssue = DelIssue;
            self.Issue = Issue;
            self.markPreivew = markPreivew;
            self.sapPublish = sapPublish;
            //1.定义初始按钮
            //按钮权限
            self.isProductionOrderTransfer = false;//跨工厂调拨
            self.isProductionOrderPublish = false; //工单发布
            self.isProductionOrderFrozen = false; //冻结
            self.isProductionOrderDelWO = false;//工单删除
            self.isProductionOrderEditWO = false;//修改工单

            self.isProductionOrderEditMaterial = false;//生成物料需求
            self.isProductionOrderDel = false; //订单删除
            self.isProductionOrderEdit = false; //订单修改
            self.isProductionOrderImportWO = false;//导入工单
            self.isProductionOrderImportPrd = false;//导入订单
            self.isProductionOrderLoad = false;//下载订单
            self.isProductionOrderSapPublish = false;//SAP发布


            //2.定义跟按钮相对应的按钮权限变量，读取到权限信息后，存到变量里
            self.ProductionOrderPublish = false; //工单发布
            self.ProductionOrderFrozen = false; //冻结
            self.ProductionOrderDelWO = false;//工单删除
            self.ProductionOrderEditWO = false;//修改工单
            self.ProductionOrderEditMaterial = false;//生成物料需求
            self.ProductionOrderTransfer = false;//跨工厂调拨
            self.ProductionOrderDel = false; //订单删除
            self.ProductionOrderEdit = false; //订单修改
            //self.ProductionOrderImportWO=false;//导入工单
            //self.ProductionOrderImportPrd=false;//导入订单
            //self.ProductionOrderLoad=false;//下载订单
            self.ProductionOrderSapPublish = false;//SAP发布

            //3.按钮权限
            ButtonAuthInit();

        }
        //按钮权限
        function ButtonAuthInit() {
            var PageName = "ProductionOrder";
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

                                    if (data[i].objectName.split('.')[7] == PageName + "Load") {
                                        self.isProductionOrderLoad = data[i].isAccessible;//下载订单
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "ImportPrd") {
                                        self.isProductionOrderImportPrd = data[i].isAccessible;//导入订单
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "ImportWO") {
                                        self.isProductionOrderImportWO = data[i].isAccessible;//导入工单
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Edit") {
                                        self.ProductionOrderEdit = data[i].isAccessible;//修改订单
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Del") {
                                        self.ProductionOrderDel = data[i].isAccessible;//删除订单
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "EditMaterial") {
                                        self.ProductionOrderEditMaterial = data[i].isAccessible;//生成物料需求
                                    }

                                    else if (data[i].objectName.split('.')[7] == PageName + "Publish") {
                                        self.ProductionOrderPublish = data[i].isAccessible;//工单发布
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Transfer") {
                                        self.ProductionOrderTransfer = data[i].isAccessible;//跨工厂调拨
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Frozen") {
                                        self.ProductionOrderFrozen = data[i].isAccessible;//冻结
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "DelWO") {
                                        self.ProductionOrderDelWO = data[i].isAccessible;//删除工单
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "EditWO") {
                                        self.ProductionOrderEditWO = data[i].isAccessible;//修改工单
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "SapPublish") {
                                        self.ProductionOrderSapPublish = data[i].isAccessible;//SAP发布
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
        $rootScope.$on("to-parentDetail", function (event, data) {
            initGridDataDetail();
        })
        function initDictionary() {

            self.typeOrderType = {
                value: { RuleName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), RuleCode: "" },
                options: [{ RuleName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), RuleCode: "" }]
            };
            self.typeOrderStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), ItemValue: "" }]
            };

            self.typeWorkOrderStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), ItemValue: "" }]
            };
            self.typeWoType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), ItemValue: "" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_6'), ItemValue: "1" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_7'), ItemValue: "2" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_8'), ItemValue: "3" },
                ]
            };



            commonService.getDataItemDuatil("OrderStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeOrderStatus.options = res.data.resultData;
                    self.typeOrderStatus.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("OrderType").then(function (res) {
                if (res && res.data.success) {
                    self.typeOrderType.options = res.data.resultData;
                    self.typeOrderType.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("WorkOrderStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeWorkOrderStatus.options = res.data.resultData;
                    self.typeWorkOrderStatus.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5')
                    });
                }
            });
            //PO状态
            self.typePO = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("PoStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typePO.options = res.data.resultData;
                    self.typePO.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            //冻结标记
            self.typeFreezeFlag = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), ItemValue: "" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), ItemValue: "" },
                    { ItemName: "未冻结", ItemValue: "0" },
                    { ItemName: "已冻结", ItemValue: "1" }
                ]
            };

            //订单关闭
            self.typeOrderClosed = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_5'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("OrderClosed").then(function (res) {
                if (res && res.data.success) {
                    self.typeOrderClosed.options = res.data.resultData;
                    self.typeOrderClosed.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_9'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_10'),
                        width: 130
                    },
                    {
                        field: 'CustomerName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_11'),
                        width: 130
                    },
                    {
                        field: 'OrderType',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_12'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'1\'"><span ng-cell-text>出口</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'2\'"><span ng-cell-text>内销</span></div>'
                    },
                    {
                        field: 'ProductPlanNo',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_15'),
                        width: 180
                    },
                    {
                        field: 'OrderDate',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_16'),
                        width: 130,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'DeliveryDate',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_17'),
                        width: 130,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'BoxDate',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_18'),
                        width: 130,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'
                    },
                    // {
                    //     field: 'OrderStatus',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_19'),
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'1\'"><span ng-cell-text>创建</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'2\'"><span ng-cell-text>审核</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'3\'"><span ng-cell-text>生产中</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'4\'"><span ng-cell-text>已完成</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'5\'"><span ng-cell-text>已发货</span></div>'
                    // },
                    {
                        field: 'OrderStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_19'),
                        width: 110
                    },
                    // {
                    //     field: 'Technology',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_25'),
                    //     width: 180
                    // },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_26'),
                        width: 180
                    },
                    {
                        field: 'IssueStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_27'),
                        width: 140,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.IssueStatus==\'0\'"><span ng-cell-text>未下发</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.IssueStatus==\'1\'"><span ng-cell-text>已下发</span></div>'


                    },
                    {
                        field: 'WoStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_30'),
                        width: 140,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.WoStatus==\'1\'"><span ng-cell-text>未发布</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.WoStatus==\'2\'"><span ng-cell-text>部分发布</span></div>' +

                            '<div class="ngCellText" ng-if="row.entity.WoStatus==\'3\'"><span ng-cell-text>已发布</span></div>'
                    },
                    {
                        field: 'Salesman',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_34'),
                        width: 120
                    },
                    {
                        field: 'Codename',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_35'),
                        width: 120
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_36'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'AuditName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_37'),
                        width: 120
                    },
                    {
                        field: 'AuditTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_38'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'CDownloadUser',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_38_1'),
                        width: 120
                    },
                    {
                        field: 'CDownloadTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_38_2'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'PDownloadUser',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_38_3'),
                        width: 120
                    },
                    {
                        field: 'PDownloadTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_38_4'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    // {
                    //     field: 'ModifyBy',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_39'),
                    //     width: 140
                    // },
                    // {
                    //     field: 'ModifyTime',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_40'),
                    //     width: 160,
                    //     type: 'date',
                    //     cellFilter: 'date:"yyyy-MM - dd HH: mm: ss"'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    // }
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
                                self.isProductionOrderDel = self.ProductionOrderDel;
                                self.isProductionOrderEdit = self.ProductionOrderEdit;
                                self.isProductionOrderEditMaterial = self.ProductionOrderEditMaterial;

                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                                self.isProductionOrderDel = false;
                                self.isProductionOrderEdit = false;
                                self.isProductionOrderEditMaterial = false;
                            }
                            initGridDataDetail();
                        }
                    });
                },
                data: []
            }
        }
        function ButtonVisibleFalse() {
            self.isProductionOrderDel = false;
            self.isProductionOrderEdit = false;
            self.isProductionOrderEditMaterial = false;
        }

        //查询方法,数据绑定
        function initGridData() {
            ButtonVisibleFalse();
            self.selectedItem = null;
            self.isButtonVisible = false;
            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime Desc,ProductOrder',//生产订单
                sord: 'desc'
            };
            self.searchParams.OrderStatus = self.typeOrderStatus.value.ItemValue;
            self.searchParams.OrderType = self.typeOrderType.value.ItemValue;
            self.searchParams.WoStatus = self.typeWoType.value.ItemValue;
            if (self.StartPrepay && self.EndPrepay) {
                self.searchParams.StartPrepay = commonService.ConvertToLocalTime(self.StartPrepay);
                self.searchParams.EndPrepay = commonService.ConvertToLocalTime(self.EndPrepay);
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress('plan') + 'PL_ProductionOrder/PL_ProductionOrderPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_1'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
            initGridDataDetail();
        }
        function importPrdButtonHandler(clickedCommand) {
            $state.go(rootstate + '.importPrd');
        }
        function importVCWorderButtonHandler(clickedCommand) {
            $state.go(rootstate + '.importVCWrd');
        }
        function importWorderButtonHandler(clickedCommand) {
            $state.go(rootstate + '.importWrd');
        }
        //新增
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }
        //取消下发
        function DelIssue(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_41');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_42');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('plan') + 'PL_ProductionOrder/SavePL_ProductionOrder';
                var user = commonService.getLoginUser();
                self.selectedItem.IssueStatus = '0';
                //提交删除当前选择数据实体
                var postData = {
                    KeyValue: self.selectedItem.Id,
                    Entity: self.selectedItem
                };

                commonService.callWebApiPost(url, postData).then(function (res) {

                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_43'));
                        //重新刷新列表
                        initGridData();
                        self.selectedItem = null;
                        self.isButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_1'));
                });
            }, title);
        }
        //下发
        function Issue(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_44');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_45');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('plan') + 'PL_ProductionOrder/SavePL_ProductionOrder';
                var user = commonService.getLoginUser();
                self.selectedItem.IssueStatus = '1';
                //提交删除当前选择数据实体
                var postData = {
                    KeyValue: self.selectedItem.Id,
                    Entity: self.selectedItem
                };

                commonService.callWebApiPost(url, postData).then(function (res) {

                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_46'));
                        //重新刷新列表
                        initGridData();
                        self.selectedItem = null;
                        self.isButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_1'));
                });
            }, title);
        }
        //编辑订单
        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }
        //生成物料需求
        function editMateiralButtonHandler(clickedCommand) {

            var rows = $scope.gridApi.selection.getSelectedRows();
            if (rows.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_47'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
                busyIndicatorService.hide();
                return false;
            }

            var ent = self.gridOptionsDetail.data.find(t => t.OrderStatus == "1");

            if (!ent) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_49'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
                busyIndicatorService.hide();
                return false;
            }
            var postData = {
                KeyValue: self.selectedItem.Id,
                Entity: {
                    Id: self.selectedItem.Id,
                    ProductOrder: self.selectedItem.ProductOrder,
                    OrderType: self.selectedItem.OrderType,
                }
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_50') });
            var url = commonService.getMesApiAddress("plan") + "PL_ProductionOrder/SaveMaterialRequirement"
            commonService.callWebApiPost(url, postData).then(function (res) {
                busyIndicatorService.hide();
                if (res && res.data.success) {
                    initGridData();
                    initGridDataDetail();
                    commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_51'));
                } else {
                    commonService.showWarning(res.data.returnMsg);
                }
            }, function (error) {
                busyIndicatorService.hide();
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_52'));
            })
        }

        function selecMaterialtButtonHandler() {
            $state.go(rootstate + '.material', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }
        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_53');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_54');
            if (self.selectedItem.OrderStatus != "1" && self.selectedItem.OrderStatus != "2") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_55'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_1'));
                return;
            }

            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('plan') + 'PL_ProductionOrder/DeletePL_ProductionOrder';
                var user = commonService.getLoginUser();

                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItem
                };

                commonService.callWebApiPost(url, postData).then(function (res) {

                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_56'));
                        //重新刷新列表
                        initGridData();
                        self.selectedItem = null;
                        self.isButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_1'));
                });
            }, title);
        }

        //删除 事件
        function delete2ButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_53');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_54');
            if (self.selectedItemDetail.OrderStatus != "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_57'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_1'));
                return;
            }

            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('plan') + 'PL_WorkOrder/RemovePL_WorkOrder';
                var user = commonService.getLoginUser();

                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItemDetail
                };

                commonService.callWebApiPost(url, postData).then(function (res) {

                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridDataDetail();
                        self.selectedItemDetail = null;
                        self.isDetailButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_1'));
                });
            }, title);
        }

        //审核下载订单
        function downloadPrdButtonHandler() {

            var title = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_58');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_59');

            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('plan') + 'PL_ProductionOrder/PL_ProductionOrder_export';
                var user = commonService.getLoginUser();

                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItem
                };

                commonService.callWebApiPost(url, postData).then(function (res) {

                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        var url = commonService.getMesApiAddress('plan') + resultData;
                        console.log(url);
                        window.location.href = url;
                        self.isDetailButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_1'));
                });
            }, title);
        }
        //生产下载
        function downloadPrdButtonHandler2() {

            var title = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_58');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_59');

            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('plan') + 'PL_ProductionOrder/PL_ProductionOrder_export2';
                var user = commonService.getLoginUser();

                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItem
                };
                commonService.callWebApiPost(url, postData).then(function (res) {

                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        var url = commonService.getMesApiAddress('plan') + resultData;
                        console.log(url);
                        window.location.href = url;
                        //成功
                        self.isDetailButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_1'));
                });
            }, title);
        }

        //唛头预览
        function markPreivew() {

            var url = commonService.getMesApiAddress('plan') + 'PL_WorkOrder/WorkMarkPreview';

            //提交删除当前选择数据实体
            var postData = {
                productOrder: self.selectedItem.ProductOrder
            };

            commonService.callWebApiPost(url, postData).then(function (res) {
                if ((res) && (res.data.success)) {
                    let arrFilePath = res.data.resultData;
                    let baseUrl = commonService.getMesApiAddress("plan");
                    arrFilePath.forEach(item => {
                        window.open(baseUrl + item);
                    });
                } else {
                    //失败
                    commonService.showWarning(res.data.returnMsg);
                }
            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_1'));
            });
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
                enableSelectAll: true, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: true,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_9'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_60'),
                        width: 110
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_61'),
                        width: 120
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_62'),
                        width: 160
                    },
                    {
                        field: 'CustomerPO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_63'),
                        width: 120
                    },
                    {
                        field: 'OrderDate',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_16'),
                        width: 120,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'
                    },
                    {
                        field: 'GiveTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_17'),
                        width: 120,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_64'),
                        width: 120
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_65'),
                        width: 110
                    }, {
                        field: 'MMCJ',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_66'),
                        width: 80
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_67'),
                        width: 140
                    },
                    {
                        field: 'BWXH',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_68'),
                        width: 140
                    },
                    {
                        field: 'OrderPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_69'),
                        width: 130
                    },
                    {
                        field: 'PackPalletNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_70'),
                        width: 130
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_71'),
                        width: 80
                    },
                    {
                        field: 'UV',
                        displayName: 'UV',
                        width: 140
                    },
                    {
                        field: 'KCKX',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_72'),
                        width: 140
                    },
                    {
                        field: 'BarCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_73'),
                        width: 140
                    },
                    {
                        field: 'OrderBox',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_74'),
                        width: 130
                    },
                    {
                        field: 'Packing',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_75'),
                        width: 130
                    },
                    {
                        field: 'OrderStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_76'),
                        width: 110
                    },
                    {
                        field: 'POStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_79'),
                        width: 130
                    },
                    {
                        field: 'KCPieceQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_83'),
                        width: 110
                    },
                    {
                        field: 'OrderPiecesAll',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_84'),
                        width: 130
                    },
                    {
                        field: 'OrderPiecesNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_85'),
                        width: 130
                    },
                    {
                        field: 'OrderPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_86'),
                        width: 140
                    },
                    {
                        field: 'OrderStartPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_87'),
                        width: 140
                    },
                    {
                        field: 'OrderWholePallet',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_88'),
                        width: 150
                    },
                    {
                        field: 'DeliveryPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_89'),
                        width: 130
                    },
                    {
                        field: 'DeliveryBox',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_90'),
                        width: 130
                    },
                    {
                        field: 'DeliveryPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_91'),
                        width: 130
                    },
                    {
                        field: 'DeliveryStartPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_92'),
                        width: 140
                    },
                    {
                        field: 'DeliveryWholePallet',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_93'),
                        width: 150
                    },
                    // {
                    //     field: 'TotalSheets',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_94'),
                    //     width: 130
                    // },
                    // {
                    //     field: 'Yield',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_95'),
                    //     width: 80
                    // },
                    // {
                    //     field: 'ActualSheets',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_96'),
                    //     width: 110
                    // },
                    {
                        field: 'Process',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_97'),
                        width: 110
                    },


                    {
                        field: 'AvoidProduce',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_98'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.AvoidProduce==true"><span ng-cell-text class="green">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.AvoidProduce!=true"><span ng-cell-text class="red">否</span></div>'
                    },
                    {
                        field: 'FreezeFlag',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_101'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.FreezeFlag==true"><span ng-cell-text class="green">已冻结</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.FreezeFlag!=true"><span ng-cell-text class="red">未冻结</span></div>'
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_26'),
                        width: 110
                    },
                    {
                        field: 'ReleaseName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_104'),
                        width: 110
                    }, {
                        field: 'ReleaseTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_105'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'HarbourName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_106'),
                        width: 200
                    },
                    {
                        field: 'ShowOrderClosed',
                        displayName: '订单关闭',
                        width: 120
                    },
                    {
                        field: 'SAP_AUFNR',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_123'),
                        width: 130
                    },
                    {
                        field: 'SAPSync',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_124'),
                        width: 110
                    },
                    {
                        field: 'PostedMsg',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_125'),
                        width: 150
                    },
                    {
                        field: 'PostedTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_126'),
                        width: 130,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'PostedUser',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_127'),
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

                                //子表按钮权限
                                self.isProductionOrderPublish = self.ProductionOrderPublish; //工单发布
                                self.isProductionOrderFrozen = self.ProductionOrderFrozen; //冻结
                                self.isProductionOrderDelWO = self.ProductionOrderDelWO;//工单删除
                                self.isProductionOrderEditWO = self.ProductionOrderEditWO;//修改工单
                                self.isProductionOrderTransfer = self.ProductionOrderTransfer;//跨工厂调拨
                                self.isProductionOrderSapPublish = self.ProductionOrderSapPublish;//SAP工单发布
                            } else {
                                //self.selectedItemDetail = null;
                                self.isDetailButtonVisible = false;

                                //子表按钮权限
                                self.isProductionOrderPublish = false; //工单发布
                                self.isProductionOrderFrozen = false; //冻结
                                self.isProductionOrderDelWO = false;//工单删除
                                self.isProductionOrderEditWO = false;//修改工单
                                self.isProductionOrderTransfer = false;//跨工厂调拨
                                self.isProductionOrderSapPublish = false;//SAP工单发布
                            }
                        }
                    });
                    //全选事件enableSelectAll（在grid上选中全选时触发）
                    $scope.gridApiDetail.selection.on.rowSelectionChangedBatch($scope, function (allRow, event) {
                        let len = $scope.gridApiDetail.selection.getSelectedRows().length;
                        if (len > 0) {
                            self.isDetailButtonVisible = true;

                            //子表按钮权限
                            self.isProductionOrderPublish = self.ProductionOrderPublish; //工单发布
                            self.isProductionOrderFrozen = self.ProductionOrderFrozen; //冻结
                            self.isProductionOrderDelWO = self.ProductionOrderDelWO;//工单删除
                            self.isProductionOrderEditWO = self.ProductionOrderEditWO;//修改工单
                            self.isProductionOrderTransfer = self.ProductionOrderTransfer;//跨工厂调拨
                            self.isProductionOrderSapPublish = self.ProductionOrderSapPublish;//SAP发布
                        }
                        else {
                            self.isDetailButtonVisible = false;

                            //子表按钮权限
                            self.isProductionOrderPublish = false; //工单发布
                            self.isProductionOrderFrozen = false; //冻结
                            self.isProductionOrderDelWO = false;//工单删除
                            self.isProductionOrderEditWO = false;//修改工单
                            self.isProductionOrderTransfer = false;//跨工厂调拨
                            self.isProductionOrderSapPublish = false;//SAP发布
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

            //子表按钮权限
            self.isProductionOrderPublish = false; //工单发布
            self.isProductionOrderFrozen = false; //冻结
            self.isProductionOrderDelWO = false;//工单删除
            self.isProductionOrderEditWO = false;//修改工单
            self.isProductionOrderTransfer = false;//跨工厂调拨
            self.isProductionOrderSapPublish = false;//SAP发布

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_107'));
                return;
            }

            let Pagination = {
                rows: self.gridOptionsDetail.paginationPageSize,
                page: self.gridOptionsDetail.paginationCurrentPage,
                // sidx: 'CAST(ContainerNO as int),OrderStartPallet,MaterialCode',
                sidx: 'WorkOrder',
                sord: 'asc'
            };

            self.searchParams2.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams2.POStatus = self.typePO.value.ItemValue;
            self.searchParams2.FreezeFlag = self.typeFreezeFlag.value.ItemValue;
            self.searchParams2.OrderClosed = self.typeOrderClosed.value.ItemValue;
            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.ProductOrder = self.selectedItem.ProductOrder;
            }
            else {
                self.gridOptionsDetail.data = [];
                return;
            }
            self.searchParams2.OrderStatus = self.typeWorkOrderStatus.value.ItemValue;


            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };

            var url = commonService.getMesApiAddress('plan') + 'PL_WorkOrder/GetProductOrderPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_1'));
            });
        }

        //查询
        function search2ButtonHandler() {
            initGridDataDetail();
        }

        //编辑工单
        function editDetailButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service

            var data = $scope.gridApiDetail.selection.getSelectedRows();
            if (data.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_108'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
                return false;
            }
            self.selectedItemDetail = data[0];
            //self.selectedItemDetail.IsReadOnly = false;
            if (!self.selectedItem) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_109'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
                return false;
            }
            // if (self.selectedItem.OrderStatus == "5") {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_110'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
            //     return false;
            // }
            // if (self.selectedItem.OrderStatus == "3" || self.selectedItem.OrderStatus == "4") {
            //     self.selectedItemDetail.IsReadOnly = true;
            // }



            $state.go(rootstate + '.editOrder', { id: self.selectedItemDetail.ID, selectedItem: self.selectedItemDetail });
        }
        //编辑装柜
        function editStuffingButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            //PO号状态必须是非已发货状态
            var workOrderString = "";
            var data = $scope.gridApiDetail.selection.getSelectedRows();
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_111'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
                return false;
            }

            var flag = false;
            data.forEach((item, index, arr) => {
                if (item.POStatus == "5" || !item.WorkOrder) {
                    flag = true;
                    return;
                }
                workOrderString += "'" + item.WorkOrder + "',"
            });
            if (flag) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_112'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
                return;
            }

            if (workOrderString.length > 1) {
                workOrderString = workOrderString.substring(0, workOrderString.length - 1);
            }
            var params = {
                ProductOrder: self.selectedItemDetail.ProductOrder,
                workOrderString: workOrderString,
            }

            // POStatus
            $state.go(rootstate + '.stuffing', { id: self.selectedItemDetail.ProductOrder, selectedItem: params });
        }
        //冻结，解冻
        function editLockButtonHandler(clickedCommand) {
            // if(!self.selectedItem){
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_109'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
            //     return false;
            // }
            // if(self.selectedItem.OrderStatus=="5"){
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_110'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
            //     return false;
            // }
            var data = $scope.gridApiDetail.selection.getSelectedRows();
            if (data.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_108'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
                return false;
            }
            self.selectedItemDetail = data[0];
            var flag = false;
            if (self.selectedItemDetail.FreezeFlag == true) {
                flag = false;
            } else {
                flag = true;
            }
            var title = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_113');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_114');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('plan') + 'PL_WorkOrder/WorkOrderFreezeFlag';
                var postData = {
                    KeyValue: self.selectedItemDetail.WorkOrderId,
                    Entity: {
                        Id: self.selectedItemDetail.WorkOrderId,
                        FreezeFlag: flag
                    }
                };
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        commonService.showInfo(res.data.returnMsg);
                        initGridDataDetail();
                        self.selectedItemDetail = null;
                        self.isDetailButtonVisible = false;
                    } else {
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_1'));
                });
            }, title);

        }
        function TransferButtonHandler(clickedCommand) {
            var data = $scope.gridApiDetail.selection.getSelectedRows();
            var lessNum = 0;
            var moreNum = 0;
            var statusNum = 0;
            //判断工单状态
            data.forEach((item, index, arr) => {
                if (item.OrderStatus == 6 || item.OrderStatus == 7 || item.OrderStatus == 8 || item.OrderStatus == 20 || item.OrderStatus == 40) {
                    statusNum = statusNum + 1;
                }
            })
            if (statusNum != 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_115'));
                return false;
            }
            data.forEach((item, index, arr) => {
                if (item.OrderStatus <= 4) {
                    lessNum = lessNum + 1;
                } else {
                    moreNum = moreNum + 1;
                }
            });
            if (lessNum != 0 && moreNum != 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_116'));
                return false;
            }

            $state.go(rootstate + '.Transfer', { selectedItem: data });
        }

        function GetButtonHandler(clickedCommand) {
            var rows = [];
            var data = $scope.gridApiDetail.selection.getSelectedRows();
            var flag = false;
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_47'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
                return false;
            }
            data.forEach((item, index, arr) => {
                rows.push({
                    Id: item.WorkOrderId,
                    FactoryCode: item.FactoryCode,
                    FactoryName: item.FactoryName,
                    ProductOrder: item.ProductOrder,
                    WorkOrder: item.WorkOrder,
                    DXZH: item.DXZH,
                    OrderPieces: item.OrderPieces,
                    IsVC: item.IsVC,
                    MaterialCode: item.MaterialCode
                })
            });

            var postData = {
                Entity: rows
            };
            var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/GetWorkListMaterial';
            //提交数据
            var req = commonService.callWebApiPost(url, postData).then(function (res) {
                if ((res) && (res.data.success)) {
                    commonService.showInfo(res.data.returnMsg);
                    initGridDataDetail();
                    initGridData();
                    self.isButtonVisible = false;
                } else {

                    commonService.showWarning(res.data.returnMsg);
                }
            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_117'));
            });
        }
        //发布
        function editPublishButtonHandler(clickedCommand) {
            var rows = [];
            var flag = false;
            var data = $scope.gridApiDetail.selection.getSelectedRows();
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_47'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
                return false;
            }
            var frozenExsit = data.filter(item => {
                return item.FreezeFlag == true;
            });
            if (frozenExsit.length > 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_118'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
                return false;
            }

            //需要判断是否已经生成物料需求
            data.forEach((item, index, arr) => {
                if (item.OrderStatus != "2") {
                    flag = true;
                }
                rows.push({
                    Id: item.WorkOrderId,
                    FactoryCode: item.FactoryCode,
                    FactoryName: item.FactoryName,
                    ProductOrder: item.ProductOrder,
                    WorkOrderType: item.AvoidProduce == true ? "4" : "1",
                    WorkOrder: item.WorkOrder,
                    OrderStatus: "3",
                    DXZH: item.DXZH,
                    OrderPieces: item.OrderPieces
                })
            });
            if (flag) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_119'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
                return false;
            }

            var title = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_120');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_121');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('plan') + 'PL_WorkOrder/PublishBatchPL_WorkOrderForm';
                var postData = {
                    data: rows
                };
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        commonService.showInfo(res.data.returnMsg);
                        initGridDataDetail();
                        self.selectedItemDetail = null;
                        self.isDetailButtonVisible = false;
                    } else {
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_1'));
                });
            }, title);
        }

        //SAP发布
        function sapPublish() {
            var rows = [];
            var flag = false;
            var data = $scope.gridApiDetail.selection.getSelectedRows();
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_47'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
                return false;
            }
            var frozenExsit = data.filter(item => {
                return item.FreezeFlag == true;
            });
            if (frozenExsit.length > 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_118'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_48'));
                return false;
            }

            //需要判断是否已经生成物料需求
            data.forEach((item, index, arr) => {
                rows.push({
                    Id: item.WorkOrderId,
                    FactoryCode: item.FactoryCode,
                    FactoryName: item.FactoryName,
                    ProductOrder: item.ProductOrder,
                    WorkOrderType: item.AvoidProduce == true ? "4" : "1",
                    WorkOrder: item.WorkOrder,
                    OrderStatus: "3",
                    DXZH: item.DXZH,
                    OrderPieces: item.OrderPieces
                })
            });

            var title = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_120');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_121');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('plan') + 'PL_WorkOrder/PublishSAPWorkOrder';
                var postData = {
                    data: rows
                };
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        commonService.showInfo(res.data.returnMsg);
                        initGridDataDetail();
                        self.selectedItemDetail = null;
                        self.isDetailButtonVisible = false;
                    } else {
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.JS.Tips_1'));
                });
            }, title);
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
        var moduleStateName = 'home.Siemens_SimaticIT_PlanApp_Plan';
        var moduleStateUrl = 'Siemens.SimaticIT_PlanApp_Plan';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/Plan';

        var state = {
            name: moduleStateName + '_ProductionOrder',
            url: '/' + moduleStateUrl + '_ProductionOrder',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ProductionOrder-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.Plan.JS.Tips_122'
            }
        };
        $stateProvider.state(state);
    }
}());

