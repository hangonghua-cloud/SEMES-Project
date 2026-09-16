(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.ProductDispatch').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatch.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatch');

            init();
            initGridOptions();
            initGridOptionsItem();
            //initGridOptionsDetail();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_ProductDispatch_ProductDispatch';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //初始化数据字典
            initDictionary();
            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.searchParams = {};
            //子表明细
            self.selectedItemItem = null;
            self.isItemButtonVisible = false;
            self.searchParams2 = {};

            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            self.searchParams3 = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler; //创建发货单
            self.printButtonHandler = printButtonHandler;
            self.editButtonHandler = editButtonHandler;//编辑
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.delete2ButtonHandler = delete2ButtonHandler;
            self.markSureSendOut = markSureSendOut;//确认发货
            self.takeoff = takeoff;//退货

            //按钮权限
            self.isProductDispatchAdd = false; //新增
            self.isProductDispatchEdit = false; //编辑
            self.isProductDispatchDelete = false;//删除

            //定义跟按钮相对应的按钮权限变量，读取到权限信息后，存到变量里
            self.ProductDispatchAdd = false; //新增
            self.ProductDispatchEdit = false; //编辑
            self.ProductDispatchDelete = false;//删除

            //3.按钮权限
            ButtonAuthInit();
        }

        //按钮权限
        function ButtonAuthInit() {
            var PageName = "ProductDispatch";
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

                                    if (data[i].objectName.split('.')[7] == PageName + "Add") {
                                        self.ProductDispatchAdd = data[i].isAccessible;
                                        self.isProductDispatchAdd = self.ProductDispatchAdd;
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Edit") {
                                        self.ProductDispatchEdit = data[i].isAccessible;
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Delete") {
                                        self.ProductDispatchDelete = data[i].isAccessible;
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
                self.isProductDispatchEdit = self.ProductDispatchEdit;
                self.isProductDispatchDelete = self.ProductDispatchDelete;
            }
            else {
                self.isProductDispatchEdit = false;
                self.isProductDispatchDelete = false;
            }
        }

        function initDictionary() {

            //发货类型
            self.DeliveryType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_1'), ItemValue: "" }]
            }
            commonService.getDataItemDuatil("DeliveryType").then(function (res) {
                if (res && res.data.success) {
                    self.DeliveryType.options = res.data.resultData;
                }
            })
            //发货单状态
            self.typeStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_1'), ItemValue: "" }]
            }
            commonService.getDataItemDuatil("DeliveryStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeStatus.options = res.data.resultData;
                }
            })

            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_1')
                    });
                    initGridData();
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
                paginationPageSizes: [20, 30, 50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 20, //每页显示个数
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_7'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'DocNum',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'DeliveryTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_11'),
                        width: 120
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_12'),
                        width: 120
                    },
                    {
                        field: 'CustomerPO',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_13'),
                        width: 120
                    },
                    {
                        field: 'PalletQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_14'),
                        width: 130
                    },
                    {
                        field: 'Qty',
                        displayName: '片数',
                        width: 130
                    },
                    {
                        field: 'BoxNum',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_15'),
                        width: 110
                    },
                    {
                        field: 'GrossWeight',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_16'),
                        width: 110
                    },
                    {
                        field: 'Volume',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_17'),
                        width: 110
                    },
                    {
                        field: 'InvoiceNO',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_18'),
                        width: 130
                    },
                    {
                        field: 'LoadingBill',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_19'),
                        width: 130
                    },
                    {
                        field: 'StatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_20'),
                        width: 130
                    },
                    {
                        field: 'DeliveryDate',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_21'),
                        width: 120,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'ContainerID',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_22'),
                        width: 120
                    },
                    {
                        field: 'CarNumber',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_23'),
                        width: 120
                    },
                    {
                        field: 'ForkliftWorker',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_24'),
                        width: 120
                    },
                    {
                        field: 'WoodWorker',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_25'),
                        width: 120
                    },
                    {
                        field: 'SealingNo',
                        displayName: '封箱号',
                        width: 120
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_26'),
                        width: 120
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_27'),
                        width: 160
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_28'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'Operator',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_45'),
                        width: 160
                    },
                    {
                        field: 'SendTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_46'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'PostDate',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_47'),
                        width: 130,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'PostMarkName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_48'),
                        width: 120
                    },
                    {
                        field: 'SAPSync',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_49'),
                        width: 120
                    },
                    {
                        field: 'PostedMsg',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_50'),
                        width: 120
                    },
                    {
                        field: 'PostedUser',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_51'),
                        width: 120
                    },
                    {
                        field: 'PostedTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_52'),
                        width: 120,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'SAP_VBELN',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_53'),
                        width: 120
                    },
                    {
                        field: 'SAP_MBLNR',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_54'),
                        width: 120
                    },
                    {
                        field: 'Off_SAPSync',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_55'),
                        width: 120
                    },
                    {
                        field: 'Off_PostedMsg',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_56'),
                        width: 120
                    },
                    {
                        field: 'Off_PostedUser',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_57'),
                        width: 120
                    },
                    {
                        field: 'Off_PostedTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_58'),
                        width: 120,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'Off_SAP_VBELN',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_59'),
                        width: 120
                    },
                    {
                        field: 'Off_SAP_MBLNR',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_60'),
                        width: 120
                    },
                    {
                        field: 'Off_PostDate',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_61'),
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

                                initGridDataItem();
                            } else {
                                self.isButtonVisible = false;
                                self.isItemButtonVisible = false;
                                self.isDetailButtonVisible = false;
                                self.selectedItem = null;
                                //按钮权限
                                ButtonVisible(false);

                                self.gridOptionsItem.data = [];


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
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_29'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//发货单号
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.DeliveryType = self.DeliveryType.value.ItemValue; //发货类型
            self.searchParams.Status = self.typeStatus.value.ItemValue;//发货单状态

            if (self.StartTime && self.EndTime) {
                self.searchParams.StartTime = commonService.ConvertToLocalDate(self.StartTime);
                self.searchParams.EndTime = commonService.ConvertToLocalDate(self.EndTime);
            } else {
                self.searchParams.StartTime = ""
                self.searchParams.EndTime = ""
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'MM_ProductDispatchItem/MM_ProductDispatchItemPageDataTableList';

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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_30'));
            });
        }

        //查询
        function searchButtonHandler() {
            self.gridOptions.data = [];
            self.gridOptionsItem.data = [];
            initGridData();
        }

        function addButtonHandler() {
            $state.go(rootstate + '.add');
        }
        //打印
        function printButtonHandler(clickedCommand) {
            $state.go(rootstate + '.select');
        }

        //编辑
        function editButtonHandler(clickedCommand) {
            // if (self.selectedItem.Status != "1") {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_31'), commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_32'));
            //     return false;
            // }
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            if (self.selectedItem.Status != "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_33'), commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_32'));
                return false;
            }

            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_34');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_35');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("material") + 'MM_ProductDispatchItem/RemoveMM_ProductDispatchItem';

                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItem
                };
                self.gridOptions.data = [];
                self.gridOptionsItem.data = [];
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_30'));
                });
            }, title);
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //发货明细
        function initGridOptionsItem() {
            self.gridOptionsItem = {
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_7'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_36'),
                        width: 200
                    },
                    // {
                    //     field: 'MarkName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_37'),
                    //     width: 120
                    // },
                    // {
                    //     field: 'BoxQty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_38'),
                    //     width: 120
                    // },
                    // {
                    //     field: 'LocationCode',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_39'),
                    //     width: 120
                    // },
                    {
                        field: 'PalletQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_40'),
                        width: 120
                    },
                    {
                        field: 'PieceQty',
                        displayName: '片数',
                        width: 120
                    },
                    {
                        field: 'LineNum',
                        displayName: '行号',
                        width: 120
                    },
                    {
                        field: 'ProductOrder',
                        displayName: '订单号',
                        width: 120
                    },
                    {
                        field: 'ProductLine',
                        displayName: '订单行号',
                        width: 120
                    },
                    {
                        field: 'WorkOrder',
                        displayName: '工单号',
                        width: 150
                    },
                    {
                        field: 'DetailGrossWeight',
                        displayName: '毛重',
                        width: 120
                    },
                    {
                        field: 'DetailVolume',
                        displayName: '体积',
                        width: 120
                    },
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApiItem = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataItem();
                    });
                    //行选中事件
                    $scope.gridApiItem.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemItem = row.entity;
                                self.isItemButtonVisible = true;

                            } else {
                                self.selectedItemItem = null;
                                self.isItemButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //发货明细
        function initGridDataItem() {

            self.selectedItemItem = null;
            self.isItemButtonVisible = false;

            let Pagination = {
                rows: self.gridOptionsItem.paginationPageSize,
                page: self.gridOptionsItem.paginationCurrentPage,
                sidx: 'ContainerNO',//柜号
                sord: 'asc'
            };

            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.DispatchItemId = self.selectedItem.Id
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };

            var url = commonService.getMesApiAddress("material") + 'MM_ProductDispatchDetail/MM_ProductDispatchDetailPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsItem.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsItem.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_30'));
            });
        }

        function delete2ButtonHandler() {
            if (self.selectedItemItem.Status == commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_42')) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_43'), commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_32'));
                return
            }
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_34');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_35');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("material") + 'MM_ProductDispatchItem/RemoveMM_ProductDispatchItem';
                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItemItem
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_30'));
                });
            }, title);
        }

        //确认发货
        function markSureSendOut() {

            var url = commonService.getMesApiAddress("material") + 'MM_ProductDispatchItem/markSureSendOut';
            var postData = {
                Entity: self.selectedItem
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_27') });
            commonService.callWebApiPost(url, postData).then(function (res) {
                if ((res) && (res.data.success)) {
                    //成功
                    commonService.showInfo(res.data.returnMsg);
                    //重新刷新列表
                    initGridData();
                    self.selectedItem = null;
                    self.isButtonVisible = false;
                    self.gridOptionsItem.data = [];
                } else {
                    //失败
                    commonService.showWarning(res.data.returnMsg);
                }
                busyIndicatorService.hide();
            })
        }

        //退货
        function takeoff() {
            if (self.selectedItem.Status != "3") {
                //请选择已完成发货的数据
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_62'));
                return;
            }

            $state.go(rootstate + '.takeoff', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
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
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_ProductDispatch';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_ProductDispatch';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/ProductDispatch';

        var state = {
            name: moduleStateName + '_ProductDispatch',
            url: '/' + moduleStateUrl + '_ProductDispatch',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ProductDispatch-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchlistctrl.Tips_44'
            }
        };
        $stateProvider.state(state);
    }
}());
