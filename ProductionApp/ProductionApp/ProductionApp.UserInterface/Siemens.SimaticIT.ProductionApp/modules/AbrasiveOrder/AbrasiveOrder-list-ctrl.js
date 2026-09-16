(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.AbrasiveOrder').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AbrasiveOrder.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AbrasiveOrder');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();
            //初始化子表grid选项
            initGridOptionsDetail();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_AbrasiveOrder_AbrasiveOrder';
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

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.bgButtonHandler = bgButtonHandler;//生产报工
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            self.finishButtonHandler = finishButtonHandler;//工单完成
            self.editBom = editBom;//修改bom
            self.GetButtonHandler = GetButtonHandler;//获取最新物料属性
            //子明细
            self.add2ButtonHandler = add2ButtonHandler;//新增
            self.edit2ButtonHandler = edit2ButtonHandler;//编辑
            self.delete2ButtonHandler = delete2ButtonHandler;//删除


            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
            initDictionary();

            self.typeFactoryChange = typeFactoryChange;
        }

        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_1'), ResourceCode: "" }]
            };
            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_1'), ResourceCode: "" }]
            };
            self.OrderStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_1'), ItemValue: "" }]
            };

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_1')
                    });
                    initGridData();
                }
            });
            commonService.getDataItemDuatil("ExeWorkOrderStatus").then(function (res) {
                if (res && res.data.success) {
                    self.OrderStatus.options = res.data.resultData;
                    self.OrderStatus.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
        }

        function typeFactoryChange(oldItem, newItem) {
            commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.Process.options = res.data.resultData;
                    self.Process.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_1')
                    });
                }
            });
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_3'),
                        width: 100
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_4'),
                        width: 100
                    },
                    {
                        field: 'OrderStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_5'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'1\'"><span ng-cell-text>未生产</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'2\'"><span ng-cell-text>正在生产</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'3\'"><span ng-cell-text>已完成</span></div>'
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_9'),
                        width: 150
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_11'),
                        width: 150
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_12'),
                        width: 150
                    },

                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_13'),
                        width: 150
                    },
                    {
                        field: 'ProcessRouteName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_14'),
                        width: 150
                    },
                    // {
                    //     field: 'PlanQty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_15'),
                    //     width: 100
                    // },
                    // {
                    //     field: 'BGQty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_16'),
                    //     width: 100
                    // },
                    {
                        field: 'BOMCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_42'),
                        width: 200
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_43'),
                        width: 200
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_17'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter' //'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_18'),
                        width: 120
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_19'),
                        width: 200
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
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_20'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//制单时间
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.ProcessCode = self.Process.value.ResourceCode;
            self.searchParams.OrderStatus = self.OrderStatus.value.ItemValue;

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
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_AbrasiveOrder/PM_AbrasiveOrderPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_21'));
            });
            self.gridOptionsDetail.data = [];
        }

        //查询
        function searchButtonHandler() {
            initGridData();
        }

        //新增
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        //生产报工
        function bgButtonHandler(clickedCommand) {
            if (self.selectedItem.OrderStatus == "3") {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_22'));
                return;
            }
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.addbg', { selectedItem: self.selectedItem });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //工单完成
        function finishButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_23');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_24');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_AbrasiveOrder/FinishPM_AbrasiveOrder';
                var postData = {
                    KeyValue: self.selectedItem.Id
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_21'));
                });
            }, title);
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_25');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_26');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_AbrasiveOrder/RemovePM_AbrasiveOrder';

                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItem
                };
                busyIndicatorService.show();//打开遮罩层
                commonService.callWebApiPost(url, postData).then(function (res) {
                    busyIndicatorService.hide();//关闭遮罩层
                    if ((res) && (res.data.success)) {
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
                    busyIndicatorService.hide();//关闭遮罩层
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_21'));
                });
            }, title);
        }

        //修改BOM
        function editBom() {

            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //获取最新物料属性
        function GetButtonHandler() {

            var postData = {
                Entity: self.selectedItem
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_AbrasiveOrder/PM_GetMaterialAttr';
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'MachineName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_27'),
                        width: 160
                    },
                    {
                        field: 'BGQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_28'),
                        width: 160
                    },
                    // {
                    //     field: 'BatchNo',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_29'),
                    //     width: 100
                    // },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_30'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'ShiftName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_31'),
                        width: 160
                    },
                    {
                        field: 'PackingType',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_32'),
                        width: 160,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.PackingType==\'1\'"><span ng-cell-text>小包</span></div>' +

                            '<div class="ngCellText" ng-if="row.entity.PackingType==\'2\'"><span ng-cell-text>吨包</span></div>'
                    },


                    // {
                    //     field: 'PTeamName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_35'),
                    //     width: 160
                    // },
                    {
                        field: 'UserNames',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_36'),
                        width: 200
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
                sord: 'desc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.AbrasiveId = self.selectedItem.Id;
            }
            else {
                // backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_37'), commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_38'));
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_AbrasiveBG/PM_AbrasiveBGPageDataTableList';
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
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_21'), commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_21'));
            });
        }

        //查询
        function search2ButtonHandler() {
            initGridDataDetail();
        }

        //新增
        function add2ButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addDetail', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //编辑
        function edit2ButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.editbg', { id: self.selectedItemDetail.Id, mainSelectedItem: self.selectedItem, selectedItem: self.selectedItemDetail });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.selectDetail', { id: self.selectedItemDetail.ID, selectedItem: self.selectedItemDetail });
        }

        //审核
        function audit2ButtonHandler(clickedCommand) {

            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_39');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_40');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_AbrasiveBG/AuditPM_AbrasiveBG';
                //提交审核当前选择数据实体
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_21'));
                });
            }, title);
        }

        //删除 事件
        function delete2ButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_25');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_26');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_AbrasiveBG/RemovePM_AbrasiveBG';
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_21'));
                });
            }, title);
        }

        $rootScope.$on("to-parent", function (event, data) {
            initGridDataDetail();
            if (data == "add") {
                self.selectedItem.OrderStatus = "2";
            }
        });

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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_AbrasiveOrder';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_AbrasiveOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/AbrasiveOrder';

        var state = {
            name: moduleStateName + '_AbrasiveOrder',
            url: '/' + moduleStateUrl + '_AbrasiveOrder',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/AbrasiveOrder-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.AbrasiveOrder.JS.Tips_41'
            }
        };
        $stateProvider.state(state);
    }
}());
