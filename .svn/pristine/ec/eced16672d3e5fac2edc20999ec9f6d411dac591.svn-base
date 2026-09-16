(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderManage').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderManage.WorkOrder.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.PlanApp.WorkOrderManage.WorkOrder');

            init();
            //初始化grid选项
            initGridOptions();
            //初始化子表grid选项
            initGridOptionsDetail();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_PlanApp_WorkOrderManage_WorkOrder';
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
            self.pagination = {
                rows: 20,
                page: 1,
                sidx: 'WorkOrder,DeliveryDate ',//工厂编码
                sord: 'asc'
            };

            initDictionary();

            //Expose Model Methods
            self.searchButtonHandler = searchButtonHandler; //查询
            self.editPlan = editPlan;//计划编辑
            self.editQuality = editQuality;//质量编辑
            self.addSupplementary = addSupplementary;//新增补料单
            self.addPick = addPick;//新增拣余单
            self.editDemand = editDemand;//要料，取消
            self.addSort = addSort;//新增排序
            self.addMeasure = addMeasure;//放量规则维护
            self.sortChange = sortChange;
            self.editBom = editBom;//修改bom
            self.VCeditBom = VCeditBom;
            self.editProcess = editProcess; //修改工艺路线
            self.deleteButtonHandler = deleteButtonHandler;
            self.GetButtonHandler = GetButtonHandler;//获取最新物料属性
            self.downloadWorkOrder = downloadWorkOrder;//下载补料单
            self.finishCase = finishCase;//SAP结案

            //按钮按钮
            self.isWorkOrderAddSupple = false;//新增补料单
            self.isWorkOrderAddPick = false;//新增拣余单
            self.isWorkOrderEditPlan = false;//计划编辑
            self.isWorkOrderEditQuality = false;//质量编辑
            self.isWorkOrderDemand = false;//要料
            self.isWorkOrderEditBom = false;//修改bom
            self.isWorkOrderEditProcess = false;//修改工艺
            self.isWorkOrderDel = false;//删除
            self.isWorkOrderGet = false;//获取最新物料属性
            self.isWorkOrderFinishCase = false;//SAP结案

            //按钮按钮
            self.WorkOrderAddSupple = false;//新增补料单
            self.WorkOrderAddPick = false;//新增拣余单
            self.WorkOrderEditPlan = false;//计划编辑
            self.WorkOrderEditQuality = false;//质量编辑
            self.WorkOrderDel = false;//删除
            self.WorkOrderDemand = false;//要料
            self.WorkOrderEditBom = false;//修改bom
            self.WorkOrderEditProcess = false;//修改工艺
            self.WorkOrderGet = false;
            self.WorkOrderFinishCase = false;//SAP结案

            ButtonAuthInit();
        }

        //按钮权限
        function ButtonAuthInit() {
            // debugger
            var PageName = "WorkOrder";
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

                                    if (data[i].objectName.split('.')[7] == PageName + "AddSupple") {
                                        self.WorkOrderAddSupple = data[i].isAccessible;//新增补料单
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "AddPick") {
                                        self.WorkOrderAddPick = data[i].isAccessible;//新增拣余单
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "EditPlan") {
                                        self.WorkOrderEditPlan = data[i].isAccessible;//计划编辑
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "EditQuality") {
                                        self.WorkOrderEditQuality = data[i].isAccessible;//质量编辑
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Demand") {
                                        self.WorkOrderDemand = data[i].isAccessible;//要料
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "EditBom") {
                                        self.WorkOrderEditBom = data[i].isAccessible;//修改bom
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "EditProcess") {
                                        self.WorkOrderEditProcess = data[i].isAccessible;//修改工艺
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Del") {
                                        self.WorkOrderDel = data[i].isAccessible;//删除
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Get") {
                                        self.WorkOrderGet = data[i].isAccessible; //获取物料属性
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "FinishCase") {
                                        self.WorkOrderFinishCase = data[i].isAccessible; //SAP结案
                                    }
                                }
                            }
                        }
                    }, function (resError) {
                        backendService.genericError('获取数据出错', resError);
                    });

                } else {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_2'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_3'));
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_4'));
            });
        }

        //按钮权限
        function ButtonVisible(flag) {
            if (flag) {
                self.isWorkOrderAddSupple = self.WorkOrderAddSupple;//新增补料单
                self.isWorkOrderAddPick = self.WorkOrderAddPick;//新增拣余单
                self.isWorkOrderEditPlan = self.WorkOrderEditPlan;//计划编辑
                self.isWorkOrderEditQuality = self.WorkOrderEditQuality;//质量编辑
                self.isWorkOrderDel = self.WorkOrderDel;//删除
                self.isWorkOrderGet = self.WorkOrderGet;//获取物料属性
                self.isWorkOrderDemand = self.WorkOrderDemand;//要料
                self.isWorkOrderEditBom = self.WorkOrderEditBom;//修改bom
                self.isWorkOrderEditProcess = self.WorkOrderEditProcess;//修改工艺
                self.isWorkOrderFinishCase = self.WorkOrderFinishCase;//SAP结案
            }
            else {
                self.isWorkOrderAddSupple = false;//新增补料单
                self.isWorkOrderAddPick = false;//新增拣余单
                self.isWorkOrderEditPlan = false;//计划编辑
                self.isWorkOrderEditQuality = false;//质量编辑
                self.isWorkOrderDemand = false;//要料
                self.isWorkOrderEditBom = false;//修改bom
                self.isWorkOrderEditProcess = false;//修改工艺
                self.isWorkOrderDel = false;//删除
                self.isWorkOrderGet = false;//获取最新物料属性
                self.isWorkOrderFinishCase = false;//SAP结案
            }
        }

        function initDictionary() {

            self.typeWO = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_5'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_5'), ItemValue: "" }]
            };
            self.typePO = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_5'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_5'), ItemValue: "" }]
            };
            self.typeWOType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_5'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_5'), ItemValue: "" }]
            };


            commonService.getDataItemDuatil("WorkOrderStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeWO.options = res.data.resultData;
                    self.typeWO.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("PoStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typePO.options = res.data.resultData;
                    self.typePO.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("WorkOrderType").then(function (res) {
                if (res && res.data.success) {
                    self.typeWOType.options = res.data.resultData;
                    self.typeWOType.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })

            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_5'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_5'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_5')
                    });
                    initGridData();
                }
            });

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

        $rootScope.$on('to-parent', function (event, data) {
            initGridData();
        });

        function sortChange(oldval, newval) {
            debugger
            var sidx = "";
            var sord = "";
            if (newval.RuleCode != "") {
                var param = {
                    queryJson: {
                        RuleCode: newval.RuleCode
                    }
                }
                var url = commonService.getMesApiAddress("plan") + "PL_WorkOrderSortRule/PL_WorkOrderSortRulePageList";
                commonService.callWebApiPost(url, param).then(function (res) {
                    if (res && res.data.success) {
                        var rows = res.data.resultData.rows;
                        for (var i = 0; i < rows.length; i++) {
                            if (i == rows.length - 1) {
                                sidx += rows[i].FieldCode;
                                sord = rows[i].IsAsc == true ? "ASC" : "DESC"
                            } else {
                                sidx += rows[i].FieldCode + " " + (rows[i].IsAsc == true ? "ASC" : "DESC") + ","
                            }
                        }
                        console.log(sidx)
                        console.log(sord)
                        self.pagination.sidx = sidx;
                        self.pagination.sord = sord
                    } else {
                        self.pagination.sidx = "DeliveryDate"
                        self.pagination.sord = "ASC"
                    }
                });
            } else {
                self.pagination.sidx = "DeliveryDate"
                self.pagination.sord = "ASC"
            }
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
                enableSelectAll: true, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: true,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_6'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_7'),
                        width: 120,
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_9'),
                        width: 80
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_11'),
                        width: 140
                    },
                    {
                        field: 'KCPieceQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_12'),
                        width: 110
                    },
                    {
                        field: 'DemandMaterial',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_13'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.DemandMaterial==true"><span ng-cell-text class="green">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.DemandMaterial!=true"><span ng-cell-text class="red">否</span></div>'
                    },
                    {
                        field: 'TotalSheets',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_16'),
                        width: 110
                    },
                    {
                        field: 'ActualSheets',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_17'),
                        width: 110
                    },
                    {
                        field: 'BWXH',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_18'),
                        width: 110
                    },
                    {
                        field: 'KCKX',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_19'),
                        width: 110
                    },
                    {
                        field: 'FirstInspectionConfirm',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_20'),
                        width: 140,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.FirstInspectionConfirm==true"><span ng-cell-text class="green">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.FirstInspectionConfirm!=true"><span ng-cell-text class="red">否</span></div>'
                    },
                    {
                        field: 'AvoidProduce',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_21'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.AvoidProduce==true"><span ng-cell-text class="green">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.AvoidProduce!=true"><span ng-cell-text class="red">否</span></div>'
                    },

                    {
                        field: 'CustomerPO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_22'),
                        width: 120
                    },

                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_23'),
                        width: 160
                    },
                    // {
                    //     field: 'OrderTypeName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_24'),
                    //     width: 200
                    // },
                    // {
                    //     field: 'OrderStatus',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_25'),
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'1\'"><span ng-cell-text>创建</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'2\'"><span ng-cell-text>审核</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'3\'"><span ng-cell-text>发布</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'4\'"><span ng-cell-text>已发料</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'5\'"><span ng-cell-text>生产中</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'6\'"><span ng-cell-text>已完成</span></div>'

                    // },
                    {
                        field: 'OrderStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_25'),
                        width: 110,
                    },
                    // {
                    //     field: 'WorkOrderType',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_32'),
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'1\'"><span ng-cell-text>正常工单</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'2\'"><span ng-cell-text>补料单</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'3\'"><span ng-cell-text>拣余单</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'4\'"><span ng-cell-text>免产单</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'5\'"><span ng-cell-text>超产品单</span></div>'
                    // },
                    {
                        field: 'WorkOrderTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_32'),
                        width: 110,
                    },
                    // {
                    //     field: 'POStatus',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_38'),
                    //     width: 130,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.POStatus==\'1\'"><span ng-cell-text>创建</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.POStatus==\'2\'"><span ng-cell-text>生产中</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.POStatus==\'3\'"><span ng-cell-text>已完成</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.POStatus==\'4\'"><span ng-cell-text>已入库</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.POStatus==\'5\'"><span ng-cell-text>已发货</span></div>'
                    // },
                    {
                        field: 'POStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_38'),
                        width: 130,
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_41'),
                        width: 110
                    },

                    {
                        field: 'MMCJ',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_42'),
                        width: 120
                    },


                    {
                        field: 'OrderPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_43'),
                        width: 120
                    },
                    {
                        field: 'OrderBox',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_44'),
                        width: 140
                    },
                    {
                        field: 'OrderPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_45'),
                        width: 140
                    },
                    {
                        field: 'OrderStartPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_46'),
                        width: 140
                    },
                    {
                        field: 'DeliveryPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_47'),
                        width: 140
                    },
                    {
                        field: 'DeliveryBox',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_48'),
                        width: 140
                    },
                    {
                        field: 'DeliveryPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_49'),
                        width: 140
                    },
                    {
                        field: 'DeliveryStartPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_50'),
                        width: 140
                    },

                    {
                        field: 'Yield',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_51'),
                        width: 100
                    },
                    {
                        field: 'DXZH',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_52'),
                        width: 130
                    },

                    {
                        field: 'HD',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_53'),
                        width: 120
                    },

                    {
                        field: 'UV',
                        displayName: 'UV',
                        width: 100
                    },

                    {
                        field: 'OrderDate',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_54'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd"'
                    },
                    {
                        field: 'DeliveryDate',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_55'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd"'
                    },
                    // {
                    //     field: 'LoadingDate',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_56'),
                    //     width: 140,
                    //     type: 'date',
                    //     cellFilter: 'date:"yyyy-MM-dd"'
                    // },


                    {
                        field: 'PackingEndTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_57'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd"'
                    },

                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_58'),
                        width: 110
                    },
                    {
                        field: 'StartOperationName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_59'),
                        width: 110,
                    },
                    {
                        field: 'TransferBy',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_60'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TransferBy==\'1\'"><span ng-cell-text>按柜</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TransferBy==\'2\'"><span ng-cell-text>按托</span></div>'
                    },
                    {
                        field: 'FirstInspectionOperation',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_63'),
                        width: 160
                    },

                    {
                        field: 'FreezeFlag',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_64'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.FreezeFlag==true"><span ng-cell-text class="green">已冻结</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.FreezeFlag!=true"><span ng-cell-text class="red">未冻结</span></div>'
                    },
                    {
                        field: 'IsEnabled',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_67'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.IsEnabled!=true"><span ng-cell-text class="green">已删除</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.IsEnabled==true"><span ng-cell-text class="red">未删除</span></div>'
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_70'),
                        width: 160
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_71'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'
                    },
                    {
                        field: 'ShowOrderClosed',
                        displayName: '订单关闭',
                        width: 120
                    },
                    {
                        field: 'SAP_AUFNR',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_112'),
                        width: 130
                    },
                    {
                        field: 'SAPSync',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_113'),
                        width: 110
                    },
                    {
                        field: 'PostedMsg',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_114'),
                        width: 150
                    },
                    {
                        field: 'PostedTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_115'),
                        width: 130,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'PostedUser',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_116'),
                        width: 130
                    },
                    {
                        field: 'CaseTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_121'),
                        width: 130,
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
                                ButtonVisible(true);
                            } else {
                                self.selectedItem = null;

                                //按钮权限
                                ButtonVisible(false);
                            }
                        }
                    });
                },
                data: []
            }
        }

        function initGridData() {
            //按钮权限
            ButtonVisible(false);

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_72'));
                return;
            }

            self.gridOptionsDetail.data = [];
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.pagination.rows = self.gridOptions.paginationPageSize;
            self.pagination.page = self.gridOptions.paginationCurrentPage;

            self.searchParams.LookWorkOrder = "3";
            self.searchParams.POStatus = self.typePO.value.ItemValue;
            self.searchParams.OrderStatus = self.typeWO.value.ItemValue;
            self.searchParams.WorkOrderType = self.typeWOType.value.ItemValue;
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.OrderClosed = self.typeOrderClosed.value.ItemValue;

            if (self.StartPrepay && self.EndPrepay) {
                self.searchParams.StartPrepay = commonService.ConvertToLocalTime(self.StartPrepay);
                self.searchParams.EndPrepay = commonService.ConvertToLocalTime(self.EndPrepay);
            } else {
                self.searchParams.StartPrepay = null;
                self.searchParams.EndPrepay = null;
            }
            if (self.CreateTime && self.EndCreateTime) {
                self.searchParams.CreateTime = commonService.ConvertToLocalTime(self.CreateTime);
                self.searchParams.EndCreateTime = commonService.ConvertToLocalTime(self.EndCreateTime);
            } else {
                self.searchParams.CreateTime = null;
                self.searchParams.EndCreateTime = null;
            }

            let queryParmeters = {
                pagination: self.pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/PL_WorkOrderPageDataTableList';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridOptions.totalItems = res.data.resultData.records;
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_1'));
            });
        }

        //查询
        function searchButtonHandler() {
            self.gridOptionsDetail.data = [];
            initGridData();
        }
        //下载补料单 jpf 2023-2-24
        function downloadWorkOrder() {
            var title = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_73');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_74');

            backendService.confirm(text, function () {

                var data = $scope.gridApi.selection.getSelectedRows();
                var statusNum = 0;
                data.forEach((item, index, arr) => {
                    if (item.WorkOrderType != '2') {
                        statusNum = statusNum + 1;
                    }
                })
                if (statusNum > 0) {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_75'));
                    return false;
                }
                var url = commonService.getMesApiAddress('plan') + 'PL_WorkOrder/PL_BLWorkOrder_export';
                var user = commonService.getLoginUser();

                //提交删除当前选择数据实体
                var postData = {
                    Entity: data
                };
                debugger
                commonService.callWebApiPost(url, postData).then(function (res) {

                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        var url = commonService.getMesApiAddress('plan') + resultData;
                        console.log(url);
                        window.location.href = url;
                        //成功
                        debugger
                        // commonService.showInfo(res.data.returnMsg);



                        self.isDetailButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_1'));
                });
            }, title);
        }
        //修改vcBOM jpf add 2022-11-23 
        function VCeditBom() {
            var flag = false;
            var copyData = $scope.gridApi.selection.getSelectedRows();
            if (copyData.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_76'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }
            if (copyData[0].WorkOrderType == "4") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_78'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }
            if (!copyData[0].IsVC) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_79'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }

            $state.go(rootstate + '.VCbom', { id: self.selectedItem.Id, selectedItem: copyData[0] });
        }

        function editBom() {
            var copyData = $scope.gridApi.selection.getSelectedRows();
            // if (copyData.length != 1) {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_76'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
            //     return;
            // }
            // if (copyData[0].WorkOrderType == "4") {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_78'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
            //     return;
            // }
            // if (copyData[0].IsVC) {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_80'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
            //     return;
            // }
            if (copyData.find(t => t.WorkOrderType == "4")) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_78'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }
            if (copyData.find(t => t.IsVC == true)) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_80'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }

            let arrMaterialCode = copyData.map(item => { return item.MaterialCode; });
            arrMaterialCode = [...new Set(arrMaterialCode)];
            let arrProcess = copyData.map(item => { return item.Process; });
            arrProcess = [...new Set(arrProcess)];
            if (arrMaterialCode.length > 1 || arrProcess.length > 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_81'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }

            $state.go(rootstate + '.bom', { id: self.selectedItem.Id, selectedItem: copyData });
        }

        //计划编辑
        function editPlan(clickedCommand) {
            var flag = false;
            var copyData = $scope.gridApi.selection.getSelectedRows();
            if (copyData.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_82'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }
            //判断是否符合逻辑
            /**
             * ①工艺路线编辑：工单状态必须是发布、已发料或生产中；
             * 针对生产中的工单，更改后的工艺路线必须可完全包含已执行的工序；
             * 超始工序由系统默认自动带出首工序，可人工修改；
             * ②流转方式编辑：按柜或按托，默认按柜，工单状态必须是发布、已发料或生产中）；
             * ③良率：针对尚未生成执行工单的可在此对良率进行手工修改
             */
            self.selectedItem = copyData[0];
            self.selectedItem.isReadYield = false;
            copyData.forEach((item, index, arr) => {
                if (item.OrderStatus != "3" && item.OrderStatus != "4" && item.OrderStatus != "5") {
                    flag = true;
                }
                if (item.OrderStatus == "5") {
                    self.selectedItem.isReadYield = true;
                }
            });

            if (flag) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_83'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }
            self.selectedItem.data = copyData;

            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }
        //质量编辑
        function editQuality(clickedCommand) {
            var flag = false;
            var copyData = $scope.gridApi.selection.getSelectedRows();
            if (copyData.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_84'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }
            //判断是否符合逻辑 工单状态必须发布或已发料状态
            copyData.forEach((item, index, arr) => {
                if (item.OrderStatus != "3" && item.OrderStatus != "4") {
                    flag = true;
                }
            });
            if (flag) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_85'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }
            self.selectedItem.data = copyData;
            $state.go(rootstate + '.editQuality', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }
        //补料单
        function addSupplementary(clickedCommand) {

            // if (!self.selectedItem.SAP_AUFNR) {
            //     //选择的工单未同步SAP
            //     commonService.showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_117'));
            //     return;
            // }

            var copyData = $scope.gridApi.selection.getSelectedRows();
            if (copyData.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_82'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }

            if (copyData[0].WorkOrderType != "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_86'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }
            let selectItem = {
                Title: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_87'),
                WorkOrderType: 2,
                FactoryCode: copyData[0].FactoryCode,
                FactoryName: copyData[0].FactoryName,
                OldWorkOrder: copyData[0].WorkOrder,
                MaterialCode: copyData[0].MaterialCode,
                ProductOrder: copyData[0].ProductOrder,
                OrderType: copyData[0].OrderType,
                Yield: copyData[0].Yield,
                Spec: copyData[0].Spec,
                DXZH: copyData[0].DXZH,
                IsVC: copyData[0].IsVC,
                StartOperation: copyData[0].StartOperation,
                StartOperationName: copyData[0].StartOperationName
            };
            $state.go(rootstate + '.add', { selectedItem: selectItem });
        }
        //拣余单
        function addPick(clickedCommand) {

            // if (!self.selectedItem.SAP_AUFNR) {
            //     //选择的工单未同步SAP
            //     commonService.showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_117'));
            //     return;
            // }

            var copyData = $scope.gridApi.selection.getSelectedRows();
            if (copyData.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_82'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }

            if (copyData[0].WorkOrderType != "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_86'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }

            let selectItem = {
                Title: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_88'),
                FactoryCode: copyData[0].FactoryCode,
                FactoryName: copyData[0].FactoryName,
                WorkOrderType: 3,
                OldWorkOrder: copyData[0].WorkOrder,
                MaterialCode: copyData[0].MaterialCode,
                ProductOrder: copyData[0].ProductOrder,
                FactoryCode: copyData[0].FactoryCode,
                OrderType: copyData[0].OrderType,
                Yield: copyData[0].Yield,
                ContainerNO: copyData[0].ContainerNO,
                Spec: copyData[0].Spec,
                DXZH: copyData[0].DXZH,
                IsVC: copyData[0].IsVC,
                StartOperation: copyData[0].StartOperation,
                StartOperationName: copyData[0].StartOperationName
            };
            $state.go(rootstate + '.add', { selectedItem: selectItem });
        }

        //要料 取消
        function editDemand(clickedCommand) {
            debugger
            var flag = false;
            var flag1 = false;
            var copyData = $scope.gridApi.selection.getSelectedRows();
            if (copyData.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_82'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }
            var DemandMaterial;
            var rows = []
            copyData.forEach((item, index, arr) => {
                if (item.AvoidProduce == true) flag1 = true;
                if (DemandMaterial == undefined) DemandMaterial = item.DemandMaterial;
                else if (DemandMaterial != item.DemandMaterial) flag = true;

                rows.push({
                    Id: item.WorkOrderId,
                    DemandMaterial: !item.DemandMaterial
                })
            })
            if (flag1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_89'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }
            if (flag) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_90'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }

            var postData = {
                data: rows
            };


            backendService.confirm(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_91'), function () {


                var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/SavePL_DemandMaterialForm';
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_92'));
                });
            }, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_93'))

        }
        //排序
        function addSort(clickedCommand) {
            $state.go(rootstate + '.sort');
        }
        //放量
        function addMeasure(clickedCommand) {
            $state.go(rootstate + '.measure');
        }
        //修改工艺路线
        function editProcess(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //获取最新物料属性
        function GetButtonHandler() {
            var rows = $scope.gridApi.selection.getSelectedRows();
            if (rows.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_94'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }

            var postData = {
                Entity: self.selectedItem
            };

            debugger
            var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/GetMaterial';
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
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_92'));
            });

        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            debugger
            var rows = $scope.gridApi.selection.getSelectedRows();
            if (rows.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_94'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }

            if (self.selectedItem.WorkOrderType == "3" && self.selectedItem.OrderStatus == "4") {

                var title = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_95');
                var text = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_96');
                backendService.confirm(text, function () {
                    var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/DeletePL_WorkOrder';
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

                        backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_1'));
                    });
                }, title);

            } else if (self.selectedItem.WorkOrderType == "2" && self.selectedItem.OrderStatus == "3") {

                var title = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_95');
                var text = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_96');
                backendService.confirm(text, function () {
                    //commonService.getMesApiAddress("plan") = '/sitSrvApi/'
                    var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/DeletePL_WorkOrder';

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
                        backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_1'));
                    });
                }, title);

            } else {

                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_97'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_77'));
                return;
            }
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
                var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/FinishCase';
                var postData = {
                    data: rows
                };
                busyIndicatorService.show();//打开遮罩层
                commonService.callWebApiPost(url, postData).then(function (res) {
                    busyIndicatorService.hide();//关闭遮罩层
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
                    busyIndicatorService.hide();//关闭遮罩层
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
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_6'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },

                    // {
                    //     field: 'FactoryCode',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_7'),
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.FactoryCode==\'3001\'"><span ng-cell-text>富华工厂</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.FactoryCode==\'3002\'"><span ng-cell-text>华丽二厂</span></div>'
                    // },
                    // {
                    //     field: 'ProductOrder',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_8'),
                    //     width: 120
                    // },
                    // {
                    //     field: 'CustomerPO',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_22'),
                    //     width: 120
                    // },
                    // {
                    //     field: 'ContainerNO',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_9'),
                    //     width: 100
                    // },
                    {
                        field: 'ExeWorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_100'),
                        width: 180
                    },
                    // {
                    //     field: 'OrderStatus',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_25'),
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'1\'"><span ng-cell-text>创建</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'2\'"><span ng-cell-text>审核</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'3\'"><span ng-cell-text>发布</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'4\'"><span ng-cell-text>已发料</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'5\'"><span ng-cell-text>生产中</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'6\'"><span ng-cell-text>已完成</span></div>'

                    // },
                    {
                        field: 'ExeOrderType',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_101'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.ExeOrderType==\'1\'"><span ng-cell-text>正常工单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.ExeOrderType==\'2\'"><span ng-cell-text>补料单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.ExeOrderType==\'3\'"><span ng-cell-text>拣余单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.ExeOrderType==\'4\'"><span ng-cell-text>免产单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.ExeOrderType==\'5\'"><span ng-cell-text>超产品单</span></div>'
                    },
                    // {
                    //     field: 'POStatus',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_38'),
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.POStatus==\'1\'"><span ng-cell-text>创建</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.POStatus==\'2\'"><span ng-cell-text>生产中</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.POStatus==\'3\'"><span ng-cell-text>已完成</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.POStatus==\'4\'"><span ng-cell-text>已入库</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.POStatus==\'5\'"><span ng-cell-text>已发货</span></div>'
                    // },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_102'),
                        width: 160
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_103'),
                        width: 110
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_10'),
                        width: 110
                    },

                    {
                        field: 'Process',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_58'),
                        width: 110
                    },
                    {
                        field: 'StartOperationName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_59'),
                        width: 110,

                    },
                    // {
                    //     field: 'TotalPieces',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_104'),
                    //     width: 110
                    // },
                    // {
                    //     field: 'Yield',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_51'),
                    //     width: 100
                    // },
                    {
                        field: 'DXZH',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_52'),
                        width: 130
                    },
                    {
                        field: 'SheetsQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_16'),
                        width: 110
                    },
                    {
                        field: 'PSheetsQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_17'),
                        width: 110
                    },
                    // {
                    //     field: 'Unit',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_105'),
                    //     width: 80,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.Unit==1"><span ng-cell-text >米</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.Unit==2"><span ng-cell-text >张</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.Unit!=3"><span ng-cell-text >片</span></div>'
                    // },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_70'),
                        width: 100
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_71'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
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
                sidx: 'WorkOrder',//工单号
                sord: 'asc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.WorkOrder = self.selectedItem.WorkOrder;
            }
            else {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_109'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_110'));
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress("plan") + 'PL_PlanStoreIssue/GetListWithPageExeWorkOrder';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_1'));
            });
        }

        //查询
        function search2ButtonHandler() {
            initGridDataDetail();
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
        var moduleStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderManage';
        var moduleStateUrl = 'Siemens.SimaticIT_PlanApp_WorkOrderManage';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderManage';

        var state = {
            name: moduleStateName + '_WorkOrder',
            url: '/' + moduleStateUrl + '_WorkOrder',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/WorkOrder-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderManage.JS.Tips_111'
            }
        };
        $stateProvider.state(state);
    }
}());
