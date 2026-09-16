(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.PurchaseManage').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.PlanApp.PurchaseManage.PurchaseOrder.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.PlanApp.PurchaseManage.PurchaseOrder');

            init();
            initGridOptions();
            //初始化子表grid选项
            initGridOptionsDetail();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_PlanApp_PurchaseManage_PurchaseOrder';
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
            self.addButtonHandler = addButtonHandler;//按订单库存
            self.editButtonHandler = editButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;
            self.exportButtonHandler = exportButtonHandler;
            self.searchButtonHandler = searchButtonHandler;
            //子表操作
            self.inButtonHandler = inButtonHandler;//入库
            self.delete2ButtonHandler = delete2ButtonHandler;//删除

            self.addOrderButtonHandler = addOrderButtonHandler;//按订单
            self.addInventoryButtonHandler = addInventoryButtonHandler;//按库存
            self.finishButtonHandler = finishButtonHandler;
            self.selectSupplier = selectSupplier;
            self.supplierChange = supplierChange;
            self.fileManage = () => {
                var item = self.selectedItem;
                commonService.FileManageModal({ pId: item.PurchaseOrder, module: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_1'), tableName: 'PL_PurchaseOrder' });
            }

            //按钮权限
            self.isPurchaseManageAdd = false;//按订单采购、按库存采购、按订单库存采购
            self.isPurchaseManageEdit = false;//编辑
            self.isPurchaseManageDelete = false;//删除
            self.isPurchaseManageDeleteDetail = false;//删除入库信息

            //定义跟按钮相对应的按钮权限变量，读取到权限信息后，存到变量里
            self.PurchaseManageAdd = false;//按订单采购、按库存采购、按订单库存采购
            self.PurchaseManageEdit = false;//编辑
            self.PurchaseManageDelete = false;//删除
            self.PurchaseManageDeleteDetail = false;//删除入库信息

            //3.按钮权限
            ButtonAuthInit();
        }

        //按钮权限
        function ButtonAuthInit() {
            var PageName = "PurchaseManage";
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
                                        self.PurchaseManageAdd = data[i].isAccessible;//新增
                                        self.isPurchaseManageAdd = self.PurchaseManageAdd;
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Edit") {
                                        self.PurchaseManageEdit = data[i].isAccessible;//编辑
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Delete") {
                                        self.PurchaseManageDelete = data[i].isAccessible;//删除
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "DeleteDetail") {
                                        self.PurchaseManageDeleteDetail = data[i].isAccessible;//删除入库
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

        function initDictionary() {
            self.typeOrderType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_2'), ItemValue: "" }]
            };
            self.typeArrivalStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_2'), ItemValue: "" }]
            };

            self.typeSmallClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_2'), ItemValue: "" }]
            };

            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.typeSmallClass.options = res.data.resultData;
                    self.typeSmallClass.value = { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_2'), ItemValue: "" };
                }
            })

            commonService.getDataItemDuatil("ProcureType").then(function (res) {
                if (res && res.data.success) {
                    self.typeOrderType.options = res.data.resultData;
                    self.typeOrderType.value = { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_2'), ItemValue: "" };
                }
            })
            commonService.getDataItemDuatil("OrderArrivalStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeArrivalStatus.options = res.data.resultData;
                    self.typeArrivalStatus.value = { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_2'), ItemValue: "" };
                }
            })
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_2')
                    });
                    initGridData();
                }
            });
        }

        //选择供应商  使用公用方法
        function selectSupplier() {
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                // {
                //     field: 'EquipmentId',
                //     displayName: '供应商编码',
                //     width: 200
                // },
                {
                    field: 'Abbr',
                    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_5'),
                    width: 200
                }
            ];
            //queryName: "",
            //queryCode: ""
            /*功能描述:单选弹窗方法
            *创    建:刘万军
            *创建时间:2021-1-22
            *参    数:PostUrl API接口
            *         sidx  排序字段
            *         sord  排序方式
            *         columnDefs   grid显示字段列表
            *         callback  回调方法
            */
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_6'), commonService.getMesApiAddress("material") + "Base_SupplierManage/Base_SupplierManagePageList", [{ 'FieldCode': 'Abbr', 'FileldName': commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_5'), 'FiledType': 'Text' }], "SupplierCode", "asc", columnDefs, supplierCallback);
        }

        //选择弹窗回调方法  返回 选择实体
        function supplierCallback(res) {
            self.searchParams.Abbr = res.Abbr;
            self.searchParams.Supplier = res.SupplierCode;
        }

        function supplierChange(oldVal, newVal) {

            if (!newVal) {
                self.searchParams.Supplier = "";
            }
            console.log(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_4'), self.searchParams.Supplier);
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_3'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'PurchaseOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_8'),
                        width: 140
                    },
                    {
                        field: 'OrderType',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_9'),
                        width: 140,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'DD\'"><span ng-cell-text>按订单采购</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'KC\'"><span ng-cell-text>按库存采购</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'DDKC\'"><span ng-cell-text>按订单库存采购</span></div>'
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_13'),
                        width: 140
                    },
                    {
                        field: 'ProductDeliveryDate',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_14'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_15'),
                        width: 110
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_16'),
                        width: 180
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_17'),
                        width: 100
                    },
                    {
                        field: 'Abbr',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_6'),
                        width: 140
                    },

                    {
                        field: 'ContractNo',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_18'),
                        width: 140
                    },
                    {
                        field: 'InvoiceNo',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_19'),
                        width: 140
                    },
                    {
                        field: 'PurchaseNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_20'),
                        width: 110
                    },
                    {
                        field: 'OrderNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_21'),
                        width: 130
                    },
                    {
                        field: 'ArrivalQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_22'),
                        width: 130
                    },
                    {
                        field: 'Coefficient',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_23'),
                        width: 120
                    },
                    // {
                    //     field: 'PurchaseDeliveryDate',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_24'),
                    //     width: 140,
                    //     type: 'date',
                    //     cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    // },
                    {
                        field: 'ArrivalStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_25'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.ArrivalStatus==\'1\'"><span ng-cell-text>未到货</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.ArrivalStatus==\'2\'"><span ng-cell-text>部分到货</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.ArrivalStatus==\'3\'"><span ng-cell-text>已到货</span></div>'
                    },
                    {
                        field: 'SupplierName2',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_43'),
                        width: 200
                    },
                    {
                        field: 'SupplierName3',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_44'),
                        width: 200
                    },
                    {
                        field: 'SupplierName4',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_45'),
                        width: 200
                    },
                    {
                        field: 'SupplierName5',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_46'),
                        width: 200
                    },
                    {
                        field: 'SupplierName6',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_47'),
                        width: 200
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_29'),
                        width: 140
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_30'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_31'),
                        width: 140
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
                                //按钮权限
                                self.isPurchaseManageEdit = self.PurchaseManageEdit;//编辑
                                self.isPurchaseManageDelete = self.PurchaseManageDelete;//删除
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                                self.gridOptionsDetail.data = [];
                                //按钮权限
                                self.isPurchaseManageEdit = false;//编辑
                                self.isPurchaseManageDelete = false;//删除
                            }
                        }
                    });
                    $scope.gridApi.selection.on.rowSelectionChangedBatch($scope, function (allRow, event) {
                        let len = $scope.gridApi.selection.getSelectedRows().length;
                        if (len > 0) {
                            self.isButtonVisible = true;
                        }
                        else {
                            self.isButtonVisible = false;
                        }
                    });
                },
                data: []
            }
        }

        //按钮权限
        function ButtonVisibleFalse() {
            self.isPurchaseManageEdit = false;
            self.isPurchaseManageDelete = false;
        }
        //按钮权限-明细
        function ButtonDetailVisibleFalse(flag) {
            if (flag) {
                self.isPurchaseManageDeleteDetail = self.PurchaseManageDeleteDetail;
            } else {
                self.isPurchaseManageDeleteDetail = false;
            }
        }

        //查询方法,数据绑定
        function initGridData() {
            //按钮权限
            ButtonVisibleFalse();
            ButtonDetailVisibleFalse(false);

            self.selectedItem = null;
            self.isButtonVisible = false;

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_32'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//采购订单号
                sord: 'desc'
            };
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.OrderType = self.typeOrderType.value.ItemValue;
            self.searchParams.ArrivalStatus = self.typeArrivalStatus.value.ItemValue;
            self.searchParams.SmallClass = self.typeSmallClass.value.ItemValue;

            // if (self.StartPurchase && self.EndPurchase) {
            //     self.searchParams.StartPurchase = commonService.ConvertToLocalTime(self.StartPurchase);
            //     self.searchParams.EndPurchase = commonService.ConvertToLocalTime(self.EndPurchase);
            // }
            // if (self.StartProduct && self.EndProduct) {
            //     self.searchParams.StartProduct = commonService.ConvertToLocalTime(self.StartProduct);
            //     self.searchParams.EndProduct = commonService.ConvertToLocalTime(self.EndProduct);
            // }
            if (self.StartTime && self.EndTime) {
                self.searchParams.StartTime = commonService.ConvertToLocalDate(self.StartTime);
                self.searchParams.EndTime = commonService.ConvertToLocalDate(self.EndTime);
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("plan") + 'PL_PurchaseOrder/PL_PurchaseOrderPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                // debugger;
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_33'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
        }
        //按订单采购
        function addOrderButtonHandler(clickedCommand) {
            $state.go(rootstate + '.order');
        }
        //按库存采购
        function addInventoryButtonHandler(clickedCommand) {
            $state.go(rootstate + '.inventory');
        }
        //按订单库存采购
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        //编辑
        function editButtonHandler(clickedCommand) {
            var rows = $scope.gridApi.selection.getSelectedRows();
            if (rows.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_34'), commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_35'));
                return;
            }
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function finishButtonHandler() {

            var rows = $scope.gridApi.selection.getSelectedRows();

            //判断逻辑
            var ArrivalStatus = ""
            var msg = "";
            rows.forEach((item, index, arr) => {
                //if (item.UnProductNum == 0) msg = commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_36');
                // if (item.SuperNum > 0) msg = commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_37');
                if (ArrivalStatus == "") ArrivalStatus = item.ArrivalStatus;
                if (ArrivalStatus == "1") msg = commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_38')
                else if (ArrivalStatus != "" && ArrivalStatus != item.ArrivalStatus) msg = commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_39');
            });

            if (!!msg) {
                backendService.genericError(msg, commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_35'));
                return;
            }

            var title = commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_40');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_41');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("plan") + 'PL_PurchaseOrder/UpdatePL_PurchaseOrder';
                var postData = {
                    data: rows,
                    ArrivalStatus: ArrivalStatus == "2" ? "3" : "2"
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
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_42'));
                    }
                }, function (error) {

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_33'));
                });
            }, title);
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var rows = $scope.gridApi.selection.getSelectedRows();
            if (rows.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_34'), commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_35'));
                return;
            }

            if (self.selectedItem.ArrivalStatus != "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_43'), commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_35'));
                return;
            }

            var title = commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_44');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_45');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("plan") + 'PL_PurchaseOrder/RemovePL_PurchaseOrder';

                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];
                self.UserCode = user.loginName;
                self.UserName = user.fullName;
                self.selectedItem.UpdateByCode = self.UserCode;
                self.selectedItem.UpdateByName = self.UserCode + '-' + self.UserName;
                if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                    self.selectedItem.UpdateByName = self.UserCode;
                }
                //提交删除当前选择数据实体
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
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_42'));
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_33'));
                });
            }, title);
        }

        function exportButtonHandler(clickedCommand) {

            if (self.StartTime && self.EndTime) {
                self.searchParams.StartTime = commonService.ConvertToLocalDate(self.StartTime);
                self.searchParams.EndTime = commonService.ConvertToLocalDate(self.EndTime);
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.OrderType = self.typeOrderType.value.ItemValue;
            self.searchParams.ArrivalStatus = self.typeArrivalStatus.value.ItemValue;
            self.searchParams.SmallClass = self.typeSmallClass.value.ItemValue;

            let postData = {
                queryJson: self.searchParams
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_46') });
            var url = commonService.getMesApiAddress('plan') + 'PL_PurchaseOrder/PL_PurchaseOrder_Export';
            commonService.callWebApiPost(url, postData).then(function (res) {
                busyIndicatorService.hide();
                if ((res) && (res.data.success)) {
                    if (res.data.resultData != null) {
                        let filePath = commonService.getMesApiAddress('plan') + res.data.resultData;
                        commonService.openDownloadDialog(filePath, '');
                    }
                }
            }, function (error) {
                busyIndicatorService.hide();
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_33'));
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_3'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_32'),
                        width: 120
                    },
                    // {
                    //     field: 'DocNum',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_33'),
                    //     width: 200
                    // },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_11'),
                        width: 110
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_12'),
                        width: 110
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_13'),
                        width: 110
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_34'),
                        width: 110
                    },
                    {
                        field: 'SupplierName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_18'),
                        width: 110
                    },
                    // {
                    //     field: 'QualityStatus',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_35'),
                    //     width: 200
                    // },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_36'),
                        width: 100
                    },
                    // {
                    //     field: 'InType',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_37'),
                    //     width: 200
                    // },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_38'),
                        width: 110
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_39'),
                        width: 150
                    },
                    // {
                    //     field: 'Remark',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_23'),
                    //     width: 200
                    // },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_40'),
                        width: 110
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_41'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'SAP_MBLNR',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_48'),
                        width: 110
                    },
                    {
                        field: 'SAP_MJAHR',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_49'),
                        width: 110
                    },
                    {
                        field: 'SAPSync',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_50'),
                        width: 110
                    },
                    {
                        field: 'PostedMsg',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_51'),
                        width: 150
                    },
                    {
                        field: 'PostedTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_52'),
                        width: 130,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'PostedUser',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_53'),
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
                                //console.log (self.selectedItemDetail);
                                //按钮权限-明细
                                ButtonDetailVisibleFalse(true);

                            } else {
                                self.selectedItemDetail = null;
                                self.isDetailButtonVisible = false;
                                //按钮权限-明细
                                ButtonDetailVisibleFalse(false);
                            }
                        }
                    });
                },
                data: []
            }
        }

        //子表查询方法,数据绑定
        function initGridDataDetail() {
            //按钮权限-明细
            ButtonDetailVisibleFalse(false);

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
                self.searchParams2.BusinessId = self.selectedItem.Id;
            }
            else {
                self.gridOptionsDetail.data = [];
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };
            // console.log('queryParmeters-----' + JSON.stringify(queryParmeters));
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialIn/MM_RawMaterialInPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_27'));
            });
        }

        //入库
        function inButtonHandler(clickedCommand) {

            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.in', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }
        //删除入库 事件
        function delete2ButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_30');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_31');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialIn/RemoveMM_RawMaterialIn';
                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItemDetail
                };
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridData();
                        initGridDataDetail();
                        self.selectedItemDetail = null;
                        self.isDetailButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_27'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_PlanApp_PurchaseManage';
        var moduleStateUrl = 'Siemens.SimaticIT_PlanApp_PurchaseManage';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/PurchaseManage';

        var state = {
            name: moduleStateName + '_PurchaseOrder',
            url: '/' + moduleStateUrl + '_PurchaseOrder',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/PurchaseOrder-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.PurchaseManage.JS.Tips_47'
            }
        };
        $stateProvider.state(state);
    }
}());
