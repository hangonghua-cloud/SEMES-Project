(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.WorkOrderBatch').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.WorkOrderBatch.WorkOrderBatch.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.WorkOrderBatch');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_WorkOrderBatch_WorkOrderBatch';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.searchParams = {};

            //Expose Model Methods
            self.groupBatchButtonHandler = groupBatchButtonHandler;//合批
            self.unGroupBatchButtonHandler = unGroupBatchButtonHandler;//取消合批
            self.selectButtonHandler = selectButtonHandler; //查看
            self.publishButtonHandler = publishButtonHandler;//发布
            self.searchButtonHandler = searchButtonHandler;//查询

            initDictionary();
        }
        function initDictionary() {
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_1')
                    });
                    initGridData();
                }
            });
            //工单状态
            self.typeWO = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_1'), ItemValue: "" }]
            };
            let arrStatus = ['2', '3'];
            commonService.getDataItemDuatil("WorkOrderStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeWO.options = res.data.resultData.filter(t => arrStatus.includes(t.ItemValue));
                    self.typeWO.options.splice(0, 0, {
                        ItemValue: "",
                        ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_1')
                    });
                }
            })
        }

        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })

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
                enableFullRowSelection: false, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_3'),
                        width: 120,
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_4'),
                        width: 110
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_5'),
                        width: 160
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_6'),
                        width: 80
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_7'),
                        width: 110
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_9'),
                        width: 140
                    },
                    {
                        field: 'KCPieceQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'DemandMaterial',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_11'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.DemandMaterial==true"><span ng-cell-text class="green">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.DemandMaterial!=true"><span ng-cell-text class="red">否</span></div>'
                    },
                    {
                        field: 'TotalSheets',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_14'),
                        width: 110
                    },
                    {
                        field: 'ActualSheets',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_15'),
                        width: 110
                    },
                    {
                        field: 'BWXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_16'),
                        width: 110
                    },
                    {
                        field: 'KCKX',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_17'),
                        width: 110
                    },
                    {
                        field: 'FirstInspectionConfirm',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_18'),
                        width: 140,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.FirstInspectionConfirm==true"><span ng-cell-text class="green">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.FirstInspectionConfirm!=true"><span ng-cell-text class="red">否</span></div>'
                    },
                    {
                        field: 'AvoidProduce',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_19'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.AvoidProduce==true"><span ng-cell-text class="green">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.AvoidProduce!=true"><span ng-cell-text class="red">否</span></div>'
                    },

                    {
                        field: 'CustomerPO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_20'),
                        width: 120
                    },
                    {
                        field: 'OrderStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_21'),
                        width: 110,
                    },
                    {
                        field: 'WorkOrderTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_22'),
                        width: 110,
                    },

                    {
                        field: 'POStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_23'),
                        width: 130,
                    },
                    {
                        field: 'MMCJ',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_24'),
                        width: 120
                    },
                    {
                        field: 'OrderPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_25'),
                        width: 120
                    },
                    {
                        field: 'OrderBox',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_26'),
                        width: 140
                    },
                    {
                        field: 'OrderPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_27'),
                        width: 140
                    },
                    {
                        field: 'OrderStartPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_28'),
                        width: 140
                    },
                    {
                        field: 'DeliveryPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_29'),
                        width: 140
                    },
                    {
                        field: 'DeliveryBox',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_30'),
                        width: 140
                    },
                    {
                        field: 'DeliveryPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_31'),
                        width: 140
                    },
                    {
                        field: 'DeliveryStartPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_32'),
                        width: 140
                    },

                    {
                        field: 'Yield',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_33'),
                        width: 100
                    },
                    {
                        field: 'DXZH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_34'),
                        width: 130
                    },

                    {
                        field: 'HD',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_35'),
                        width: 120
                    },

                    {
                        field: 'UV',
                        displayName: 'UV',
                        width: 100
                    },

                    {
                        field: 'OrderDate',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_36'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'
                    },
                    {
                        field: 'DeliveryDate',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_37'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'
                    },
                    {
                        field: 'PackingEndTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_38'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_39'),
                        width: 110
                    },
                    {
                        field: 'StartOperationName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_40'),
                        width: 110,
                    },
                    {
                        field: 'TransferBy',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_41'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TransferBy==\'1\'"><span ng-cell-text>按柜</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TransferBy==\'2\'"><span ng-cell-text>按托</span></div>'
                    },
                    {
                        field: 'FirstInspectionOperation',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_44'),
                        width: 160
                    },

                    {
                        field: 'FreezeFlag',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_45'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.FreezeFlag==true"><span ng-cell-text class="green">已冻结</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.FreezeFlag!=true"><span ng-cell-text class="red">未冻结</span></div>'
                    },
                    {
                        field: 'BatchStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_48'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.BatchStatus==0"><span ng-cell-text>未合批</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.BatchStatus==1"><span ng-cell-text>合批</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.BatchStatus==2"><span ng-cell-text>被合批</span></div>'
                    },
                    // {
                    //     field: 'IsEnabled',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_52'),
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.IsEnabled!=true"><span ng-cell-text class="green">已删除</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.IsEnabled==true"><span ng-cell-text class="red">未删除</span></div>'
                    // },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_55'),
                        width: 160
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
                sord: 'Desc'
            };

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_56'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.OrderStatus = self.typeWO.value.ItemValue;
            if (self.GiveTime)
                self.searchParams.GiveTime = commonService.ConvertToLocalDate(self.GiveTime);
            else
                self.searchParams.GiveTime = "";

            // self.searchParams.OrderType = "2";//内销  暂时去掉
            self.searchParams.queryCode1 = "2,3";//工单状态:审核、发布
            self.searchParams.queryCode2 = "0,1";//未合批、合批工单
            self.searchParams.queryCode3 = "0";//无免产标记
            self.searchParams.WorkOrderType = "1";//正常工单
            // self.searchParams.IsVC = '0';

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/PL_WorkOrderPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_57'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
        }

        //合批
        function groupBatchButtonHandler(clickedCommand) {
            let rows = $scope.gridApi.selection.getSelectedRows();
            if (rows.length == 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_58'), commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_59'));
                return false;
            }
            if (rows.find(t => t.BatchStatus == "1")) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_60'));
                return;
            }

            let arrMaterialCode = rows.map(item => { return item.MaterialCode });
            let newArr = Array.from(new Set(arrMaterialCode));
            if (newArr.length > 1) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_61'));
                return;
            }
            let arrWorkOrder = rows.map(item => { return item.WorkOrder });
            var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/GroupBatch';
            var postData = {
                arrWorkOrder: arrWorkOrder
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
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_57'));
            });
        }

        //取消合批
        function unGroupBatchButtonHandler(clickedCommand) {

            let rows = $scope.gridApi.selection.getSelectedRows();
            if (rows.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_62'), commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_59'));
                return false;
            }
            if (self.selectedItem.BatchStatus != "1") {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_63'));
                return;
            }

            var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/UnGroupBatch';
            var postData = {
                workOrder: self.selectedItem.WorkOrder
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
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_57'));
            });
        }

        //查看
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { selectedItem: self.selectedItem });
        }

        //发布 事件
        function publishButtonHandler(clickedCommand) {
            let rows = $scope.gridApi.selection.getSelectedRows();
            if (rows.length == 0) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_64'));
                return;
            }
            if (rows.find(t => t.BatchStatus != "1")) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_65'));
                return;
            }
            if (rows.find(t => t.OrderStatus == "3")) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_66'));
                return;
            }

            var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/HePiPublish';
            var postData = {
                data: rows
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
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_57'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_WorkOrderBatch';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_WorkOrderBatch';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/WorkOrderBatch';

        var state = {
            name: moduleStateName + '_WorkOrderBatch',
            url: '/' + moduleStateUrl + '_WorkOrderBatch',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/WorkOrderBatch-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.WorkOrderBatch.JS.Tips_67'
            }
        };
        $stateProvider.state(state);
    }
}());
