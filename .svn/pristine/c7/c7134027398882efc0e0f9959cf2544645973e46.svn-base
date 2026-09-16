(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatch.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', 'i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, i18nService) {
        var self = this;
        var logger, rootstate, messageservice, backendService;
        i18nService.setCurrentLang('zh - cn');

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatch');

            init();
            initGridOptions();
            initGridOptionsDetail();
            initGridOptionsDispatchDetail();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_MMRawMaterialDispatch_MMRawMaterialDispatch';
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

            //发货明细
            self.searchParams3 = {};

            self.searchButtonHandler = searchButtonHandler;
            self.editButtonHandler = editButtonHandler; //编辑
            self.deliveryButtonHandler = deliveryButtonHandler; //发货
            self.lineBack = lineBack;//行退货
            self.backOff = backOff;//退货冲销

            //数据字典
            initDictionary();
        }

        //回调函数
        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })

        function initDictionary() {

            //发货单状态
            self.typeStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_1'), ItemValue: "" }]
            }

            commonService.getDataItemDuatil("DeliveryStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeStatus.options = res.data.resultData;
                }
            })

            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_1')
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_2'), width: 70, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'DeliveryNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'GrossWeight',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'Volume',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'InvoiceNO',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'LoadingBill',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'StatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'DeliveryDate',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_11'),
                        width: 120,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2',
                    },
                    {
                        field: 'ContainerID',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_12'),
                        width: 120
                    },
                    {
                        field: 'CarNumber',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_13'),
                        width: 120
                    },
                    {
                        field: 'ForkliftWorker',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_14'),
                        width: 120
                    },
                    {
                        field: 'WoodWorker',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_15'),
                        width: 120
                    },
                    {
                        field: 'SealingNo',
                        displayName: '封箱号',
                        width: 120
                    },
                    {
                        field: 'DeliveryUserName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_16'),
                        width: 120
                    },
                    {
                        field: 'ActualDeliveryTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_17'),
                        width: 120,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter',
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_18'),
                        width: 120
                    },
                    // {
                    //     field: 'CreateByName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_19'),
                    //     width: 120
                    // },
                    //  {
                    //      field: 'CreateTime',
                    //      displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_20'),
                    //      width: 120,
                    //      type: 'date',
                    //      cellFilter: 'alpDatetimeFilterMM',
                    //  },

                    {
                        field: 'CusdeclarationDate',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_21'),
                        width: 120,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM',
                    },
                    {
                        field: 'CusdeclarationNum',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_22'),
                        width: 120
                    },
                    {
                        field: 'Harbor',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_23'),
                        width: 120
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
                                self.selectedItemDetail = null;
                                self.isDetailButtonVisible = false;
                                self.gridOptionsDetail.data = [];
                                self.gridOptionsDispatchDetail.data = [];
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
            self.gridOptionsDispatchDetail.data = [];

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_24'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//创建时间
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.Status = self.typeStatus.value.ItemValue;//发货单状态

            if (self.StartTime && self.EndTime) {
                self.searchParams.StartTime = commonService.ConvertToLocalDate(self.StartTime);
                self.searchParams.EndTime = commonService.ConvertToLocalDate(self.EndTime);
            } else {
                self.searchParams.StartTime = "";
                self.searchParams.EndTime = "";
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("material") + 'MMRawMaterialDispatch/GetPageDataTableList'
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridOptions.totalItems = res.data.resultData.records;
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_26'));
            });
        }

        //查询
        function searchButtonHandler(clickedCommand) {
            self.gridOptions.data = [];
            self.gridOptionsDetail.data = [];
            initGridData();
        }

        //发货
        function deliveryButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            let data = {
                main: self.selectedItem,
                detail: self.selectedItemDetail
            }
            $state.go(rootstate + '.delivery', { id: self.selectedItem.Id, selectedItem: data });
        }

        //编辑
        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //退货冲销
        function backOff(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_27');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_28');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("material") + 'MMRawMaterialDispatchDetail/BackOff';
                //提交删除当前选择数据实体
                var postData = {
                    Id: self.selectedItem.Id
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_26'));
                });
            }, title);
        }

        //发货单子表
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
                paginationPageSize: 100, //每页显示个数
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_29'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_30'),
                        width: 150
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_31'),
                        width: 200
                    },
                    // {
                    //     field: 'Spec',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_32'),
                    //     width: 120
                    // },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_33'),
                        width: 150
                    },
                    {
                        field: 'DetailGrossWeight',
                        displayName: "毛重",
                        width: 100
                    },
                    {
                        field: 'DetailVolume',
                        displayName: "体积",
                        width: 100
                    },
                    {
                        field: 'LineNum',
                        displayName: "行号",
                        width: 100
                    },
                    {
                        field: 'ProductLine',
                        displayName: "订单行号",
                        width: 110
                    },
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApiDetail) {
                    $scope.gridApiDetail = gridApiDetail;
                    //分页按钮事件
                    gridApiDetail.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataDetail();
                    });
                    //行选中事件
                    $scope.gridApiDetail.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailButtonVisible = true;
                                initGridDataDispatchDetail();

                            } else {
                                self.selectedItemDetail = null;
                                self.isDetailButtonVisible = false;
                                self.gridOptionsDispatchDetail.data = [];
                            }
                        }
                    });
                },
                data: []
            }
        }

        //发货单子表
        function initGridDataDetail() {

            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            self.gridOptionsDispatchDetail.data = [];

            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.DispatchId = self.selectedItem.Id
            }

            let queryParmeters = {
                queryJson: self.searchParams2
            };

            var url = commonService.getMesApiAddress("material") + 'MMRawMaterialDispatchSub/GetPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //数据
                    self.gridOptionsDetail.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsDetail.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_25'));
            });
        }

        //行退货
        function lineBack(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_38');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_39');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("material") + 'MMRawMaterialDispatchDetail/LineBack';
                var postData = {
                    Id: self.selectedItemDetail.Id
                };
                busyIndicatorService.show();//打开遮罩层
                commonService.callWebApiPost(url, postData).then(function (res) {
                    busyIndicatorService.hide();//关闭遮罩层
                    if ((res) && (res.data.success)) {
                        commonService.showInfo(res.data.returnMsg);
                        initGridDataDetail();
                        self.selectedItemDetail = null;
                        self.isDetailButtonVisible = false;
                        //重新刷新列表
                    } else {
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    busyIndicatorService.hide();//关闭遮罩层
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_26'));
                });
            }, title);
        }

        //发货明细
        function initGridOptionsDispatchDetail() {
            self.gridOptionsDispatchDetail = {
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
                paginationPageSize: 100, //每页显示个数
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_29'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    // {
                    //     field: 'MaterialCode',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_30'),
                    //     width: 150
                    // },
                    // {
                    //     field: 'MaterialName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_31'),
                    //     width: 200
                    // },
                    // {
                    //     field: 'Spec',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_32'),
                    //     width: 120
                    // },
                    // {
                    //     field: 'Qty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_33'),
                    //     width: 120
                    // },
                    // {
                    //     field: 'UnitName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_35'),
                    //     width: 80
                    // },
                    {
                        field: 'DeliveryQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_34'),
                        width: 150
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_36'),
                        width: 200
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_37'),
                        width: 120
                    },
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApiDispatchDetail) {
                    $scope.gridApiDispatchDetail = gridApiDispatchDetail;
                    //分页按钮事件
                    gridApiDispatchDetail.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataDispatchDetail();
                    });
                    //行选中事件
                    $scope.gridApiDispatchDetail.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {

                            } else {

                            }
                        }
                    });
                },
                data: []
            }
        }

        //发货明细
        function initGridDataDispatchDetail() {

            if (self.selectedItemDetail != null) {
                //关联字段
                self.searchParams3.DispatchSubId = self.selectedItemDetail.Id
            }

            let queryParmeters = {
                queryJson: self.searchParams3
            };

            var url = commonService.getMesApiAddress("material") + 'MMRawMaterialDispatchDetail/GetPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //数据
                    self.gridOptionsDispatchDetail.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsDispatchDetail.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_25'));
            });
        }


    }
    ListScreenRouteConfig.$inject = ['$stateProvider'];
    function ListScreenRouteConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_MMRawMaterialDispatch';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_MMRawMaterialDispatch';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MMRawMaterialDispatch';

        var state = {
            name: moduleStateName + '_MMRawMaterialDispatch',
            url: '/' + moduleStateUrl + '_MMRawMaterialDispatch',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/MMRawMaterialDispatch-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchlistctrl.Tips_40'
            }
        };
        $stateProvider.state(state);
    }
}());
