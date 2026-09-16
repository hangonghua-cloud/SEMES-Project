(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.TransferList').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.TransferList.TransferCard.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.TransferList.TransferCard');

            init();
            initGridOptions();
            initGridOptionsDetail();
            initPersonGridOptions();
            initMBGridOptions();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_TransferList_TransferCard';
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

            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
            self.FactoryChange = FactoryChange;
            self.ProcessChange = ProcessChange;
            self.tabPerson = tabPerson;
            self.tabBadItem = tabBadItem;
            self.tabMB = tabMB;
            self.search1ButtonHandler = search1ButtonHandler;
        }

        function initDictionary() {
            self.Factory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_1'), ResourceCode: "" }]
            };
            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_1'), ResourceCode: "" }]
            };
            self.Machine = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_1'), ResourceCode: "" }]
            };
            self.WorkOrderType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_1'), ItemValue: "" }]
            };
            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_1'), ResourceCode: "" }]
            };
            // commonService.getResourceExtendInfo({ LevelCode: "Process" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeProcess.options = res.data.resultData;
            //         self.typeProcess.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_1')
            //         });
            //     }
            // });

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.Factory.value = res.data.resultData[0];
                    }
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_1')
                    });
                    initGridData();
                }
            });
            commonService.getDataItemDuatil("WorkOrderType").then(function (res) {
                if (res && res.data.success) {
                    self.WorkOrderType.options = res.data.resultData;
                    self.WorkOrderType.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
        }

        function FactoryChange(oldItem, newItem) {
            commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeProcess.options = res.data.resultData;
                    self.typeProcess.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_1')
                    });
                }
            });
        }
        function ProcessChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.Machine.options = res.data.resultData;
                    self.Machine.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_1')
                    });
                }
            });
        }

        $rootScope.$on("to-parentPerson", function (event, data) {
            initPersonGridData();
        });
        $rootScope.$on("to-parentBadItem", function (event, data) {
            if (!!self.selectedItem) {
                self.selectedItem.BadQty = data.BadQty;
            }
            //let mainIndex = self.gridOptions.data.findIndex(t => t.Id == data.BGID);
            //self.gridOptions.data[mainIndex].BadQty += data.BadQty;
            initGridDataDetail();
        });
        $rootScope.$on("to-parentMB", function (event, data) {
            initMBGridData();
        });

        function search1ButtonHandler() {
            initPersonGridData();
            initGridDataDetail();
            initMBGridData();
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_3'),
                        width: 110
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_4'),
                        width: 140
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_5'),
                        width: 80
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_6'),
                        width: 130
                    },
                    {
                        field: 'WorkOrderTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_7'),
                        width: 110
                    },
                    {
                        field: 'SmallClass',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_8'),
                        width: 80
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_9'),
                        width: 150
                    },
                    {
                        field: 'MMCJ',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_11'),
                        width: 140
                    },
                    {
                        field: 'BWXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_12'),
                        width: 100
                    },
                    {
                        field: 'UV',
                        displayName: 'UV',
                        width: 100
                    },
                    {
                        field: 'KCKX',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_13'),
                        width: 100
                    },
                    // {
                    //     field: 'CardName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_14'),
                    //     width: 100
                    // },
                    // {
                    //     field: 'CardCode',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_15'),
                    //     width: 210
                    // },
                    // {
                    //     field: 'CardTypeName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_16'),
                    //     width: 100
                    // },
                    // {
                    //     field: 'ProcessName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_17'),
                    //     width: 150
                    // },
                    // {
                    //     field: 'MachineName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_18'),
                    //     width: 100
                    // },
                    // {
                    //     field: 'Qty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_19'),
                    //     width: 100
                    // },
                    // {
                    //     field: 'BadQty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_20'),
                    //     width: 100
                    // },
                    // {
                    //     field: 'CreateTime',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_21'),
                    //     width: 160,
                    //     type: 'date',
                    //     cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    // },
                    // {
                    //     field: 'BGUser',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_22'),
                    //     width: 100
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
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_23'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'ProductOrder Desc,Cast(ContainerNO as int)',//创建时间
                sord: 'asc'
            };

            self.searchParams.FactoryCode = self.Factory.value.ResourceCode;
            //self.searchParams.ProcessCode = self.Process.value.ResourceCode;
            //self.searchParams.MachineCode = self.Machine.value.ResourceCode;
            self.searchParams.WorkOrderType = self.WorkOrderType.value.ItemValue;

            // if (self.StartTime && self.EndTime) {
            //     self.searchParams.StartTime = commonService.ConvertToLocalTime(self.StartTime);
            //     self.searchParams.EndTime = commonService.ConvertToLocalTime(self.EndTime);
            // } else {
            //     self.searchParams.StartTime = "";
            //     self.searchParams.EndTime = "";

            // }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress('ProduceManage') + 'PM_TranferCardBGRecord/PM_TranferCardBGRecordPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridOptions.totalItems = res.data.resultData.records;
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_24'));
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
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_25');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_26');
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_24'));
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
                        field: 'CardCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_27'),
                        width: 260
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_17'),
                        width: 110
                    },

                    {
                        field: 'BadItemCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_28'),
                        width: 140
                    },
                    {
                        field: 'BadItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_29'),
                        width: 200
                    },
                    {
                        field: 'BadQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_20'),
                        width: 140
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_21'),
                        width: 140,
                        type: "date",
                        cellFilter: 'alpDatetimeFilterMM'

                    },



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
            self.searchParams2.ProcessCode = self.typeProcess.value.ResourceCode;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress('ProduceManage') + 'PM_BGBadRecord/PM_BGBadRecordPageDataTableList1';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_24'));
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
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_25');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_26');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('ProduceManage') + 'PM_BGBadRecord/RemovePM_BGBadRecord';

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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_24'));
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
                        field: 'CardCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_27'),
                        width: 260
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_17'),
                        width: 110
                    },
                    {
                        field: 'PTeamCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_30'),
                        width: 140
                    },
                    {
                        field: 'PTeamName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_31'),
                        width: 140
                    },
                    {
                        field: 'UserNames',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_32'),
                        width: 300
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_21'),
                        width: 140,
                        type: "date",
                        cellFilter: 'alpDatetimeFilterMM'
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_19'),
                        width: 140
                    },

                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_33'),
                        width: 140
                    },

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
                sidx: 'CreateTime',//人员编码
                sord: 'desc'
            };

            if (self.selectedItem != null) {
                //关联字段
                self.searchParams3.WorkOrder = self.selectedItem.WorkOrder;
            }
            else {
                self.gridPersonOptions.data = [];
                return;
            }

            self.searchParams3.ProcessCode = self.typeProcess.value.ResourceCode;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams3
            };

            var url = commonService.getMesApiAddress('ProduceManage') + 'PM_TransferBGPersonRecord/PM_TransferBGPersonRecordPageDataTableList1';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridPersonOptions.totalItems = res.data.resultData.records;
                    self.gridPersonOptions.data = res.data.resultData.rows;
                } else {
                    self.gridPersonOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_24'));
            });
        }
        //新增人员
        function add3ButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addUser', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }
        //删除人员 事件
        function delete3ButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_25');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_26');
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_24'));
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
                        field: 'CardCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_27'),
                        width: 260
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_17'),
                        width: 110
                    },

                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_34'),
                        width: 140
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_35'),
                        width: 140
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_11'),
                        width: 160
                    },
                    //{
                    //    field: 'MaterialGroup',
                    //    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_36'),
                    //    width: 200
                    //},
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_37'),
                        width: 140
                    },
                    {
                        field: 'RecoilQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_38'),
                        width: 140
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_33'),
                        width: 140
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_21'),
                        width: 140,
                        type: "date",
                        cellFilter: 'alpDatetimeFilterMM'

                    },
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
                sord: 'desc'
            };
            if (self.selectedItem != null) {
                //关联字段
                self.searchParams4.WorkOrder = self.selectedItem.WorkOrder;
            }
            else {
                self.gridMBOptions.data = [];
                return;
            }

            self.searchParams4.ProcessCode = self.typeProcess.value.ResourceCode;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams4
            };

            var url = commonService.getMesApiAddress('ProduceManage') + 'PM_MaterialBatchConsumeRecord/PM_MaterialBatchConsumeRecordPageList1';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridMBOptions.totalItems = res.data.resultData.records;
                    self.gridMBOptions.data = res.data.resultData.rows;
                } else {
                    self.gridMBOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_24'));
            });
        }

        function tabPerson() {
            self.isPersonButtonVisible = true;
            self.isDetailButtonVisible = false;
            self.isDetailDeleteButtonVisible = false;
            self.isMBButtonVisible = false;
            self.showTab = "1";
        }
        function tabBadItem() {

            self.isDetailButtonVisible = true;
            self.isPersonButtonVisible = false;
            self.isPersonDeleteButtonVisible = false;
            self.isMBButtonVisible = false;
            self.showTab = "2";
        }

        function tabMB() {
            self.isMBButtonVisible = true;
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_TransferList';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_TransferList';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/TransferList';

        var state = {
            name: moduleStateName + '_TransferCard',
            url: '/' + moduleStateUrl + '_TransferCard',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/TransferCard-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.TransferList.JS.Tips_39'
            }
        };
        $stateProvider.state(state);
    }
}());
