(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.OwnProduct').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.OwnProduct.OwnProductOrder.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', 'i18nService', 'common.services.security.securityService',
        'common.services.security.functionRightModel'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, i18nService, securityService, FunctionRightModel) {
        var self = this;
        var logger, rootstate, messageservice, backendService;
        i18nService.setCurrentLang('zh-cn');

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.OwnProduct.OwnProductOrder');

            init();
            //初始化grid选项
            initGridOptions();
            //初始化子表grid选项
            initGridOptionsDetail();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_OwnProduct_OwnProductOrder';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.searchParams = {};
            //子表明细
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            self.searchParams2 = {};

            initDictionary();

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            self.finishButtonHandler = finishButtonHandler;
            self.importButtonHandler = importButtonHandler; //导入
            //子明细
            self.add2ButtonHandler = add2ButtonHandler;//流转卡打印
            self.edit2ButtonHandler = edit2ButtonHandler;//补打流转卡
            self.delete2ButtonHandler = delete2ButtonHandler;//删除流转卡
            self.editBom = editBom;//修改bom
            self.typeFactoryChange = typeFactoryChange;
            self.GetButtonHandler = GetButtonHandler;//获取最新物料属性
            self.finishCase = finishCase;//SAP结案

            //按钮权限
            self.isOwnProductFinishCase = false; //SAP结案

            //定义跟按钮相对应的按钮权限变量，读取到权限信息后，存到变量里
            self.OwnProductFinishCase = false; //SAP结案

            //3.按钮权限
            ButtonAuthInit();

        }

        //按钮权限
        function ButtonAuthInit() {
            var PageName = "OwnProduct";
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

                                    if (data[i].objectName.split('.')[7] == PageName + "FinishCase") {
                                        self.OwnProductFinishCase = data[i].isAccessible;
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
                self.isOwnProductFinishCase = self.OwnProductFinishCase;
            }
            else {
                self.isOwnProductFinishCase = false;
            }
        }

        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_1'), ResourceCode: "" }]
            };
            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_1'), ResourceCode: "" }]
            };
            //工单状态
            self.typeWorkOrderStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_1'), ItemValue: "" }]
            };
            self.typeMaterialSmall = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_1'), ItemValue: "" }]
            };


            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_1')
                    });
                    initGridData();
                }
            });
            commonService.getDataItemDuatil("WorkOrderStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeWorkOrderStatus.options = res.data.resultData;
                    self.typeWorkOrderStatus.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.typeMaterialSmall.options = res.data.resultData;
                    self.typeMaterialSmall.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.Process.options = res.data.resultData;
                        self.Process.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_1')
                        });
                    }
                });
            }
            else {
                self.Process = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_1'), ResourceCode: "" }]
                };
            }
        }

        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })
        $rootScope.$on("to-parentDetail", function (event, data) {
            self.rowBackdata = data;
            initGridDataDetail();
            let filePath = data;
            let baseUrl = commonService.getMesApiAddress("ProduceManage");
            window.open(baseUrl + filePath);
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
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: true, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: true,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_3'),
                        width: 110
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_4'),
                        width: 110
                    },
                    // {
                    //     field: 'OrderStatus',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_5'),
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'1\'"><span ng-cell-text>未生产</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'2\'"><span ng-cell-text>正在生产</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'3\'"><span ng-cell-text>已完成</span></div>'
                    // },
                    {
                        field: 'OrderStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_5'),
                        width: 110
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_11'),
                        width: 160
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_12'),
                        width: 190
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_13'),
                        width: 190
                    },
                    {
                        field: 'ProcessRouteName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_14'),
                        width: 120
                    },
                    {
                        field: 'BOMCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_15'),
                        width: 150
                    },

                    {
                        field: 'PlanQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_16'),
                        width: 110
                    },
                    {
                        field: 'ProductQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_51'),
                        width: 110
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_57'),
                        width: 80
                    },
                    {
                        field: 'OrderStatusName',
                        displayName: "工单状态",
                        width: 110
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_18'),
                        width: 200
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_19'),
                        width: 100
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_20'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'SAP_AUFNR',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_52'),
                        width: 130
                    },
                    {
                        field: 'SAPSync',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_53'),
                        width: 110
                    },
                    {
                        field: 'PostedMsg',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_54'),
                        width: 150
                    },
                    {
                        field: 'PostedTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_55'),
                        width: 130,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'PostedUser',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_56'),
                        width: 130
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
                                initGridDataDetail();

                                //按钮权限
                                ButtonVisible(true);
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                                self.isDetailButtonVisible = false;
                                self.gridOptionsDetail.data = [];
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
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_21'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//工厂编码
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.ProcessCode = self.Process.value.ResourceCode;
            self.searchParams.SmallClass = self.typeMaterialSmall.value.ItemValue;
            self.searchParams.OrderStatus = self.typeWorkOrderStatus.value.ItemValue;

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

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_OwnProductOrder/PM_OwnProductOrderPageDataTableList';

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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_22'));
            });
        }

        //获取最新物料属性
        function GetButtonHandler() {
            var rows = $scope.gridApi.selection.getSelectedRows();
            if (rows.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_23'), commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_24'));
                return;
            }

            var postData = {
                Entity: self.selectedItem
            };

            debugger
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_OwnProductOrder/PM_OwnGetMaterial';
            //提交数据
            var req = commonService.callWebApiPost(url, postData).then(function (res) {
                if ((res) && (res.data.success)) {
                    commonService.showInfo(res.data.returnMsg);
                    initGridData();
                    self.selectedItem = null;
                    self.isButtonVisible = false;
                } else {

                    commonService.showWarning(res.data.returnMsg);
                }
            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_25'));
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
            // TODO: Put here the properties of the entity managed by the service
            if (self.selectedItem.OrderStatus != "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_26'), commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_27'));
                return;
            }
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function finishButtonHandler(clickedCommand) {
            if (self.selectedItem.OrderStatus != "2") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_28'), commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_27'));
                return;
            }
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_29');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_30');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_OwnProductOrder/SavePM_OwnProductOrder';

                var postData = {
                    KeyValue: self.selectedItem.Id,
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_22'));
                });
            }, title);
        }

        //导入
        function importButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_31');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_32');

            if (self.selectedItem.OrderStatus != "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_33'), commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_27'));
                return;
            }
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress("ProduceManage") = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_OwnProductOrder/RemovePM_OwnProductOrder';

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

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_22'));
                });
            }, title);
        }

        function editBom() {

            $state.go(rootstate + '.bom', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //SAP结案 事件
        function finishCase() {

            var rows = $scope.gridApi.selection.getSelectedRows();
            if (rows.length == 0) {
                //请选择要结案的工单
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_118'));
                return;
            }
            //结案
            var title = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_119');
            //确认结案吗？
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_120');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_OwnProductOrder/FinishCase';
                var postData = {
                    data: rows
                };
                commonService.showLoading();
                commonService.callWebApiPost(url, postData).then(function (res) {
                    commonService.hideLoading();
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
                    commonService.hideLoading();
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_1'));
                });
            }, title);
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
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: true, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: true,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'BatchNumber',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_34'),
                        width: 150
                    },

                    {
                        field: 'TransferCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_35'),
                        width: 150
                    },
                    {
                        field: 'TransferName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_36'),
                        width: 150
                    },
                    {
                        field: 'TransferStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_37'),
                        width: 140,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TransferStatus==\'1\'"><span ng-cell-text>未报工</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TransferStatus==\'2\'"><span ng-cell-text>已报工</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TransferStatus==\'3\'"><span ng-cell-text>已完成</span></div>'

                    },
                    {
                        field: 'UserNames',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_40'),
                        width: 180
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_41'),
                        width: 150,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'BGProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_42'),
                        width: 150
                    },
                    {
                        field: 'BGQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_43'),
                        width: 150
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

                            } else {
                                self.selectedItemDetail = null;
                                // self.isDetailButtonVisible = false;
                            }
                        }
                    });
                    $scope.gridApiDetail.selection.on.rowSelectionChangedBatch($scope, function (allRow, event) {
                        let len = $scope.gridApiDetail.selection.getSelectedRows().length;
                        if (len > 0) {
                            self.isDetailButtonVisible = true;
                        }
                        else {
                            self.isDetailButtonVisible = false;
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
                sidx: 'TransferName',//自制半成品Id
                sord: 'desc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.WorkOrder = self.selectedItem.WorkOrder;
            }
            else {
                self.gridOptionsDetail.data = [];
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_OwnProductTransfer/PM_OwnProductTransferPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_22'));
            });
        }

        //流转卡打印
        function add2ButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addDetail', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //补打流转卡
        function edit2ButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service

            var rows = $scope.gridApiDetail.selection.getSelectedRows();
            if (!self.selectedItem) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_44'), commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_22'));
                return;
            }
            if (rows.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_45'), commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_22'));
                return;
            }

            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_46');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_47');

            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_OwnProductTransfer/PrintPM_OwnProductTransfer';
                //提交删除当前选择数据实体
                var postData = {
                    data: rows
                };
                commonService.callWebApiPost(url, postData).then(function (res) {

                    if ((res) && (res.data.success)) {
                        let filePath = res.data.resultData;
                        let baseUrl = commonService.getMesApiAddress("ProduceManage");
                        window.open(baseUrl + filePath);
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_22'));
                });
            }, title);

        }

        //删除流转卡 事件
        function delete2ButtonHandler(clickedCommand) {

            var rows = $scope.gridApiDetail.selection.getSelectedRows();
            if (rows.length > 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_48'), commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_22'));
                return;
            }
            if (self.selectedItemDetail.BGProcessName) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_49'));
                return;
            }

            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_31');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_32');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_OwnProductTransfer/RemovePM_OwnProductTransfer';

                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];
                self.UserCode = user.loginName;
                self.UserName = user.fullName;
                self.selectedItemDetail.UpdateByCode = self.UserCode;
                self.selectedItemDetail.UpdateByName = self.UserCode + '-' + self.UserName;
                if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                    self.selectedItemDetail.UpdateByName = self.UserCode;
                }
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_22'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_OwnProduct';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_OwnProduct';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/OwnProduct';

        var state = {
            name: moduleStateName + '_OwnProductOrder',
            url: '/' + moduleStateUrl + '_OwnProductOrder',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/OwnProductOrder-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.OwnProductOrder.JS.Tips_50'
            }
        };
        $stateProvider.state(state);
    }
}());
