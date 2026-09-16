(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MMReceiptNotice').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNotice.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', '$timeout', 'i18nService', 'common.services.security.securityService',
        'common.services.security.functionRightModel'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, $timeout, i18nService, securityService, FunctionRightModel) {
        var self = this;
        var logger, rootstate, messageservice, backendService;
        i18nService.setCurrentLang('zh-cn');

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNotice');

            init();
            //初始化grid选项
            initGridOptions();
            //初始化子表grid选项
            initGridOptionsDetail();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_MMReceiptNotice_ReceiptNotice';
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
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            self.fileManage = () => {
                var item = self.selectedItem;
                commonService.FileManageModal({ pId: item.ReceiptCode, module: '物料模块', tableName: 'MM_ReceiptNotice' });
            }
            //子明细
            self.add2ButtonHandler = add2ButtonHandler;//新增入库
            self.edit2ButtonHandler = edit2ButtonHandler;//编辑
            self.delete2ButtonHandler = delete2ButtonHandler;//删除入库

            //按钮权限
            self.isReceiptNoticeAdd = false; //新增
            self.isReceiptNoticeEdit = false; //编辑
            self.isReceiptNoticeDelete = false; //删除

            //定义跟按钮相对应的按钮权限变量，读取到权限信息后，存到变量里
            self.ReceiptNoticeAdd = false;//新增
            self.ReceiptNoticeEdit = false;//编辑
            self.ReceiptNoticeDelete = false;//删除

            //3.按钮权限
            ButtonAuthInit();
        }

        //按钮权限
        function ButtonAuthInit() {
            var PageName = "ReceiptNotice";
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
                                        self.ReceiptNoticeAdd = data[i].isAccessible;//新增
                                        self.isReceiptNoticeAdd = self.ReceiptNoticeAdd;
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Edit") {
                                        self.ReceiptNoticeEdit = data[i].isAccessible;//编辑
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Delete") {
                                        self.ReceiptNoticeDelete = data[i].isAccessible;//删除
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

        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })
        $rootScope.$on("to-parentDetail1", function (event, data) {
            self.backSelectItem = data;
            initGridData();
        })
        $rootScope.$on("to-parentDetail", function (event, data) {
            initGridDataDetail();
        })

        function initDictionary() {
            self.typeQualityJudgement = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_2'), ItemValue: "" }]
            };
            self.typeReceiptStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_2'), ItemValue: "" }]
            };
            self.typeSmallClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_2'), ItemValue: "" }]
            };


            commonService.getDataItemDuatil("QualityJudgement").then(function (res) {
                if (res && res.data.success) {
                    self.typeQualityJudgement.options = res.data.resultData;
                    self.typeQualityJudgement.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_2'), ItemValue: "" };
                }
            })
            commonService.getDataItemDuatil("ReceivingNoticeStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeReceiptStatus.options = res.data.resultData;
                    self.typeReceiptStatus.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_2'), ItemValue: "" };
                }
            })
            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.typeSmallClass.options = res.data.resultData;
                    self.typeSmallClass.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_2'), ItemValue: "" };
                }
            })
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_2')
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
                paginationPageSizes: [20, 30, 50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 20, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: false, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    // {
                    //     name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_3'), width: 80, enableSorting: false, cellTemplate:
                    //         '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    // },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'ReceiptCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_5'),
                        width: 130
                    },
                    {
                        field: 'LineNum',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_6'),
                        width: 80
                    },
                    {
                        field: 'PurchaseOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_7'),
                        width: 140
                    },
                    {
                        field: 'SAPPurchaseOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_54'),
                        width: 130
                    },
                    {
                        field: 'SAPPurchaseOrderLineNum',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_55'),
                        width: 130
                    },
                    {
                        field: 'ArrivalTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_8'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },

                    {
                        field: 'BoxDate',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_9'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },

                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_11'),
                        width: 130
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_12'),
                        width: 110
                    },
                    {
                        field: 'Spec',
                        displayName: '规格',
                        width: 160
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_13'),
                        width: 80
                    },
                    {
                        field: 'PurchaseNum',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_14'),
                        width: 120
                    },
                    {
                        field: 'ArrivalQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_15'),
                        width: 130
                    },
                    {
                        field: 'InQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_16'),
                        width: 130
                    },
                    {
                        field: 'ResourceName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_17'),
                        width: 130
                    },

                    {
                        field: 'SupplierName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_18'),
                        width: 200
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
                    // {
                    //     field: 'ManufacturerName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_19'),
                    //     width: 110
                    // },
                    {
                        field: 'ReceiptStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_20'),
                        width: 110,
                        // cellTemplate:
                        //     '<div class="ngCellText" ng-if="row.entity.ReceiptStatus==\'1\'"><span ng-cell-text>创建</span></div>' +
                        //     '<div class="ngCellText" ng-if="row.entity.ReceiptStatus==\'2\'"><span ng-cell-text>检验中</span></div>' +
                        //     '<div class="ngCellText" ng-if="row.entity.ReceiptStatus==\'3\'"><span ng-cell-text>待入库</span></div>' +
                        //     '<div class="ngCellText" ng-if="row.entity.ReceiptStatus==\'4\'"><span ng-cell-text>已完成</span></div>'
                    },
                    {
                        field: 'zj',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_21'),
                        width: 130
                    },
                    // {
                    //     field: 'zhjshul',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_22'),
                    //     width: 100
                    // },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_23'),
                        width: 200
                    },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_24'),
                        width: 100
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_25'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
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
                                ButtonVisibleFalse(true);
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                                self.gridOptionsDetail.data = [];
                                //按钮权限
                                ButtonVisibleFalse(false);
                            }
                        }
                    });
                },
                data: []
            }
        }

        //按钮权限
        function ButtonVisibleFalse(flag) {
            if (flag) {
                self.isReceiptNoticeEdit = self.ReceiptNoticeEdit;
                self.isReceiptNoticeDelete = self.ReceiptNoticeDelete;
            }
            else {
                self.isReceiptNoticeEdit = false;
                self.isReceiptNoticeDelete = false;
            }
        }
        //查询方法,数据绑定
        function initGridData() {
            //按钮权限
            ButtonVisibleFalse(false);

            self.selectedItem = null;
            self.isButtonVisible = false;

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_26'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime Desc,LineNum',//创建时间
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.QualityType = self.typeQualityJudgement.value.ItemValue;
            self.searchParams.ReceiptStatus = self.typeReceiptStatus.value.ItemValue;
            self.searchParams.SmallClass = self.typeSmallClass.value.ItemValue;

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
            var url = commonService.getMesApiAddress("material") + 'MM_ReceiptNotice/MM_ReceiptNoticePageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptions.data = res.data.resultData.rows;
                    if (!!self.backSelectItem) {
                        $timeout(function () {
                            console.log(self.backSelectItem);
                            _.each(self.gridOptions.data, function (item, index) {
                                if (self.gridOptions.data[index].Id == self.backSelectItem.Id) {
                                    // console.log(self.gridOptions.data[i]);
                                    $scope.gridApi.selection.selectRow(self.gridOptions.data[index]);
                                    initGridDataDetail();
                                    self.backSelectItem = null;
                                }
                            })
                        }, 100)
                    }
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_27'));
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

        //编辑
        function editButtonHandler(clickedCommand) {
            // if (self.selectedItem.ReceiptStatus == "4") {
            //     commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_28'));
            //     return;
            // }
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {

            // if (self.selectedItem.ReceiptStatus != "1") {
            //     commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_29'));
            //     return;
            // }
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_30');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_31');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("material") + 'MM_ReceiptNotice/RemoveMM_ReceiptNotice';
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
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_27'));
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

        //查询
        function search2ButtonHandler() {
            initGridDataDetail();
        }

        //新增
        function add2ButtonHandler(clickedCommand) {

            $state.go(rootstate + '.addDetail', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //编辑
        function edit2ButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.editDetail', { id: self.selectedItemDetail.Id, mainSelectedItem: self.selectedItem, selectedItem: self.selectedItemDetail });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.selectDetail', { id: self.selectedItemDetail.Id, selectedItem: self.selectedItemDetail });
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
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_MMReceiptNotice';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_MMReceiptNotice';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MMReceiptNotice';

        var state = {
            name: moduleStateName + '_ReceiptNotice',
            url: '/' + moduleStateUrl + '_ReceiptNotice',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ReceiptNotice-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticelistctrl.Tips_42'
            }
        };
        $stateProvider.state(state);
    }
}());
