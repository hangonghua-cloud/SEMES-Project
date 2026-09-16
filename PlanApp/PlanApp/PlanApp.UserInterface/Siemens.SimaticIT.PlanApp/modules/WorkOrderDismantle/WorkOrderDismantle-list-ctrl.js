(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderDismantle').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderDismantle.WorkOrderDismantle.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.WorkOrderDismantle');

            init();
            initGridOptions();
            initGridOptionsDetail();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_PlanApp_WorkOrderDismantle_WorkOrderDismantle';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.isDetailButtonVisible = false;
            self.viewerOptions = {};
            self.searchParams = {};
            self.searchParams2 = {};

            initDictionary();

            //Expose Model Methods
            self.searchButtonHandler = searchButtonHandler;
            self.editDismantle = editDismantle;
            self.editCancellingStocks = editCancellingStocks; //退库
            self.editConsume = editConsume;
            self.editSuperProduct = editSuperProduct;
            self.deleteSuperProduct = deleteSuperProduct;
            self.reissueDismantle = reissueDismantle;//生产补料
            self.printButtonHandler = printButtonHandler;//打印面膜批次
            self.SelectErpArrivalCodeModal = SelectErpArrivalCodeModal;
        }

        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })
        $rootScope.$on("to-parentDetail", function (event, data) {
            initGridDataDetail();
        })
        $rootScope.$on("to-parentPrint", function (event, data) {
            let filePath = data;
            let baseUrl = commonService.getMesApiAddress("plan");
            window.open(baseUrl + filePath);
        })

        function initDictionary() {

            self.typeDemandMaterial = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_1'), ItemValue: "" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_1'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_2'), ItemValue: 1 },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_3'), ItemValue: 0 }
                ]
            };
            self.typeStoreIssue = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_1'), ItemValue: "" }]
            };
            self.typeSupe = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_1'), ItemValue: "" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_1'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_4'), ItemValue: ">0" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_5'), ItemValue: "=0" },
                ]
            };
            self.typeMaterialSmall = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_1'), ItemValue: "" }]
            };

            self.typeWorkOrder = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_1'), ItemValue: "" }]
            };


            commonService.getDataItemDuatil("MaterialReleaseStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeStoreIssue.options = res.data.resultData;
                    self.typeStoreIssue.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.typeMaterialSmall.options = res.data.resultData;
                    self.typeMaterialSmall.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("WorkOrderType").then(function (res) {
                if (res && res.data.success) {
                    self.typeWorkOrder.options = res.data.resultData;
                    self.typeWorkOrder.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_1')
                    });
                    initGridData();
                }
            });
        }

        function SelectErpArrivalCodeModal() {
            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress() + 'SystemManage/DataItemDetail/GetDataItemListJson_UA?EnCode=WorkOrderStatus',
                            method: "Get",
                            queryParmeters: {
                                Name: "",
                            },
                            pagination: null,
                            multiple: true,
                            sidx: "SortCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_6'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ItemValue',
                                    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_7'),
                                    width: 200
                                },
                                {
                                    field: 'ItemName',
                                    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_8'),
                                    width: 200
                                },
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                var name = "";
                var str = "";
                if ((!data || data.length <= 0)) {
                    // yoti.warn(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_9'));
                    self.searchParams.OrderStatusName = ""
                    self.searchParams.workOrderStatusStr = ""
                } else {
                    console.log(data);
                    data.forEach((item, index, arr) => {
                        name += "'" + item.ItemValue + "',";
                        str += item.ItemName + ","
                    });
                    self.searchParams.OrderStatusName = str;
                    self.searchParams.workOrderStatusStr = name.substring(0, name.length - 1);

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
                    // {
                    //     name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_6'), width: 80, enableSorting: false, cellTemplate:
                    //         '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    // },
                    // {
                    //     field: 'ProductOrder',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_10'),
                    //     width: 110
                    // },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_11'),
                        width: 120
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_12'),
                        width: 150
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_13'),
                        width: 90
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_14'),
                        width: 140
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_15'),
                        width: 140
                    },
                    {
                        field: 'WorkOrderType',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_16'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'1\'"><span ng-cell-text>正常工单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'2\'"><span ng-cell-text>补料单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'3\'"><span ng-cell-text>拣余单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'4\'"><span ng-cell-text>免产单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'5\'"><span ng-cell-text>超产品单</span></div>'
                    },
                    {
                        field: 'DemandMaterial',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_22'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.DemandMaterial==true"><span ng-cell-text class="green">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.DemandMaterial!=true"><span ng-cell-text class="red">否</span></div>'
                    },
                    {
                        field: 'OrderNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_25'),
                        width: 110
                    },
                    {
                        field: 'ProductNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_26'),
                        width: 110
                    },
                    {
                        field: 'ShouldNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_27'),
                        width: 110
                    },
                    {
                        field: 'ActualNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_28'),
                        width: 110
                    },
                    {
                        field: 'ModifyTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_29'),
                        width: 130,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'
                    },
                    {
                        field: 'StoreIssueParam',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_30'),
                        width: 110
                    },
                    {
                        field: 'SuperNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_31'),
                        width: 110
                    },

                    {
                        field: 'CancellingNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_32'),
                        width: 100
                    },
                    {
                        field: 'ConsumeNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_33'),
                        width: 120
                    },

                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_34'),
                        width: 140
                    },


                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_35'),
                        width: 140
                    },

                    {
                        field: 'SmallClass',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_36'),
                        width: 110
                    },
                    // {
                    //     field: 'UnProductNum',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_37'),
                    //     width: 110
                    // },

                    {
                        field: 'StoreIssueNo',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_38'),
                        width: 130
                    },
                    {
                        field: 'MaskStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_39'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.MaskStatus==\'1\'"><span ng-cell-text>未发料</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.MaskStatus==\'2\'"><span ng-cell-text>已发料</span></div>'

                    },

                    // {
                    //     field: 'Unit',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_42'),
                    //     width: 80,
                    //      cellTemplate:
                    //         '<div class="ngCellText"><span ng-cell-text>米</span></div>' 
                    //     //     '<div class="ngCellText" ng-if="row.entity.Unit==2"><span ng-cell-text >张</span></div>' +
                    //     //     '<div class="ngCellText" ng-if="row.entity.Unit==3"><span ng-cell-text >片</span></div>'
                    // },
                    {
                        field: 'FreezeFlag',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_46'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.FreezeFlag==true"><span ng-cell-text class="green">已冻结</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.FreezeFlag!=true"><span ng-cell-text class="red">未冻结</span></div>'
                    },
                    {
                        field: 'MMCJ',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_91'),
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
                                //console.log (self.selectedItem);
                                //子表明细关联
                                initGridDataDetail();
                            } else {
                                //self.selectedItem = null;
                                //self.isButtonVisible = false;
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

        function initGridData() {

            self.isButtonVisible = false;
            self.isButtonVisible = false;

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_49'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'ProductOrder desc,CAST(ContainerNO as int)',//排序规则编码
                sord: 'asc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.DemandMaterial = self.typeDemandMaterial.value.ItemValue;
            self.searchParams.SmallClass = self.typeMaterialSmall.value.ItemValue;
            self.searchParams.MaskStatus = self.typeStoreIssue.value.ItemValue;
            self.searchParams.SupeNum = self.typeSupe.value.ItemValue;
            self.searchParams.WorkOrderType = self.typeWorkOrder.value.ItemValue;

            self.searchParams.DemandStatus = "'1','2'";

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("plan") + 'PL_PlanStoreIssue/GetListWithPage';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridOptions.totalItems = res.data.resultData.records;
                    self.gridOptions.data = res.data.resultData.rows;

                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_50'));
            });
        }

        //查询
        function searchButtonHandler() {
            self.gridOptionsDetail.data = [];
            initGridData();
        }
        //拆解发料
        function editDismantle(clickedCommand) {

            var msg = null;
            var copyData = $scope.gridApi.selection.getSelectedRows();
            if (copyData.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_51'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_52'));
                return;
            }
            let arrFreezeFlag = copyData.filter(item => {
                return item.FreezeFlag == true;
            });
            if (arrFreezeFlag.length > 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_53'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_52'));
                return;
            }

            let detailEntity = self.gridOptionsDetail.data.find(t => t.FactoryCode == self.selectedItem.FactoryCode);
            if (detailEntity) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_54'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_52'));
                return;
            }

            //判断逻辑
            var MMXH = ""
            var array = [];
            copyData.forEach((item, index, arr) => {
                //if (item.UnProductNum == 0) msg = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_55');
                // if (item.SuperNum > 0) msg = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_56');
                if (MMXH == "") MMXH = item.MMXH;
                else if (MMXH != "" && MMXH != item.MMXH) msg = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_57');
                if (item.MaskStatus == "2" && copyData.length > 1) msg = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_58')
                // item.ActNum = angular.copy(item.UnProductNum);
                //item.SuperProdunction = 500;//超发数量
                array.push({
                    WorkOrder: item.WorkOrder
                })
            });

            if (!!msg) {
                backendService.genericError(msg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_52'));
                return;
            }
            //str = str.substring(0, str.length - 1);
            $state.go(rootstate + '.split', {
                id: null,
                selectedItem: {
                    FactoryCode: copyData[0].FactoryCode,
                    FactoryName: copyData[0].FactoryName,
                    array: array,
                    MMXH: copyData[0].MMXH,
                    MaterialCode: copyData[0].MaterialCode
                }
            });
        }

        //退库
        function editCancellingStocks(clickedCommand) {

            // var copyData = $scope.gridApi.selection.getSelectedRows();
            // if (copyData.length != 1) {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_59'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_52'));
            //     return;
            // }
            // if (copyData[0].OrderStatus == "5") {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_60'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_52'));
            //     return;
            // }

            // if (copyData[0].ActualNum <= 0) {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_61'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_52'));
            //     return;
            // }
            if (self.selectedItemDetail.ExeOrderType != "1" && self.selectedItemDetail.ExeOrderType != "2") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_62'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_52'));
                return;
            }

            $state.go(rootstate + '.edit', { id: self.selectedItemDetail.Id, selectedItem: self.selectedItemDetail });
        }
        //超发转耗用
        function editConsume(clickedCommand) {

            if (self.selectedItem && self.selectedItem.SuperNum > 0) {

                var params = {
                    WorkOrder: self.selectedItem.WorkOrder,
                    WorkOrderType: self.selectedItem.WorkOrderType,
                    ConsumeNum: self.selectedItem.ConsumeNum + self.selectedItem.SuperNum
                }
                var url = commonService.getMesApiAddress("plan") + "PL_PlanStoreIssue/Save_SuperToConsume";
                commonService.callWebApiPost(url, params).then(function (res) {
                    if (res && res.data.success) {
                        commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_63'));
                        $state.go(rootstate, {}, { reload: true });
                    }
                })
            } else {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_64'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_52'));
                return;
            }
        }

        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function deleteButtonHandler(clickedCommand) {
            var title = "Delete";
            // TODO: Put here the properties of the entity managed by the service
            var text = "Do you want to delete '" + self.selectedItem.Id + "'?";

            backendService.confirm(text, function () {
                dataService.delete(self.selectedItem).then(function () {
                    $state.go(rootstate, {}, { reload: true });
                }, backendService.backendError);
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_6'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },

                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_65'),
                        width: 110
                    },
                    {
                        field: 'ExeWorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_66'),
                        width: 180
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_14'),
                        width: 200
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_67'),
                        width: 150
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_13'),
                        width: 80
                    },
                    // {
                    //     field: 'PiecesQty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_68'),
                    //     width: 110
                    // },
                    {
                        field: 'SheetsQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_69'),
                        width: 110
                    },
                    {
                        field: 'PSheetsQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_26'),
                        width: 110
                    },
                    {
                        field: 'ExeOrderType',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_16'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.ExeOrderType==\'1\'"><span ng-cell-text>正常工单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.ExeOrderType==\'2\'"><span ng-cell-text>补料单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.ExeOrderType==\'3\'"><span ng-cell-text>拣余单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.ExeOrderType==\'4\'"><span ng-cell-text>免产单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.ExeOrderType==\'5\'"><span ng-cell-text>超产品</span></div>'
                    },
                    // {
                    //     field: 'MaskStatus',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_39'),
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.MaskStatus==\'1\'"><span ng-cell-text>未发料</span></div>' +  
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'2\'"><span ng-cell-text>已发料</span></div>'
                    // },
                    {
                        field: 'Process',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_71'),
                        width: 120
                    },
                    {
                        field: 'StartOperationName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_72'),
                        width: 110,
                    },

                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_42'),
                        width: 80,
                        // cellTemplate:
                        //     '<div class="ngCellText" ng-if="row.entity.Unit==1"><span ng-cell-text >米</span></div>' +
                        //     '<div class="ngCellText" ng-if="row.entity.Unit==2"><span ng-cell-text >张</span></div>' +
                        //     '<div class="ngCellText" ng-if="row.entity.Unit==3"><span ng-cell-text >片</span></div>'
                    },
                    {
                        field: 'ShouldNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_74'),
                        width: 110
                    },

                    {
                        field: 'ActualNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_75'),
                        width: 110
                    },
                    {
                        field: 'CancellingNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_76'),
                        width: 110
                    },
                    {
                        field: 'ConsumeNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_77'),
                        width: 120
                    },
                    {
                        field: 'SendOutBatch',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_78'),
                        width: 300
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_79'),
                        width: 100
                    },

                ],
                //---------------api---------------------
                onRegisterApi: function (gridDetailApi) {
                    $scope.gridDetailApi = gridDetailApi;
                    //分页按钮事件
                    gridDetailApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataDetail();
                    });
                    //行选中事件
                    $scope.gridDetailApi.selection.on.rowSelectionChanged($scope, function (row, event) {
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
                sidx: 'ExeWorkOrder',//执行工单号
                sord: 'asc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.WorkOrder = self.selectedItem.WorkOrder;
            }
            else {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_80'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_81'));
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_50'));
            });
        }

        //生产补料
        function reissueDismantle() {
            debugger;
            if (self.selectedItemDetail == null) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_82'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_52'));
                return false;
            }
            if (self.selectedItemDetail.ExeOrderType != "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_83'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_52'));
                return false;
            }

            var modalInstance = commonService.openModel({
                templateUrl: 'Siemens.SimaticIT.PlanApp/modules/WorkOrderDismantle/MaskSending.html',
                controller: 'Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSending',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            queryParmeters: {
                                FactoryCode: self.selectedItemDetail.FactoryCode,
                                FactoryName: self.selectedItemDetail.FactoryName,
                                TotalNum: self.selectedItemDetail.ShouldNum,
                                WorkOrder: self.selectedItemDetail.WorkOrder,
                                MaterialCode: self.selectedItemDetail.MaterialCode,
                                MMXH: self.selectedItemDetail.MMXH
                            }
                        };
                    }
                }
            });
            modalInstance.result.then(function (res) {
                debugger;
                let useNum = res.UseNum;//面膜(米)
                self.materialItem = res.data;
                self.selectedItemDetail.ActualNum += useNum;
                self.selectedItemDetail.ConsumeNum += useNum;
                if (self.selectedItemDetail.ActualNum > self.selectedItemDetail.ShouldNum) {
                    self.selectedItemDetail.SuperNum = self.selectedItemDetail.ActualNum - self.selectedItemDetail.ShouldNum;
                }
                var postData = {
                    exeWorkOrder: self.selectedItemDetail,//执行工单
                    materialItem: self.materialItem,//面膜物料扣除
                }
                busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_84') });
                var url = commonService.getMesApiAddress("plan") + 'PL_PlanStoreIssue/ProductReissueSave';
                var req = commonService.callWebApiPost(url, postData).then(function (res) {
                    busyIndicatorService.hide();
                    if ((res) && (res.data.success)) {
                        //成功
                        commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_85'));
                        //重新刷新列表
                        initGridDataDetail();
                        self.selectedItemDetail = null;
                        self.isDetailButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                        backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_86'));
                    }
                }, function (error) {
                    busyIndicatorService.hide();
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_50'));
                });
            });
        }

        function editSuperProduct(clickedCommand) {

            if (!self.selectedItemDetail || self.selectedItemDetail.ExeOrderType != "5") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_87'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_81'));
                return;
            }
            $state.go(rootstate + '.editDetail', { id: self.selectedItemDetail.Id, selectedItem: self.selectedItemDetail });
        }

        function deleteSuperProduct(clickedCommand) {
            //删除超产品
            if (!self.selectedItemDetail || self.selectedItemDetail.ExeOrderType != "5") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_87'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_81'));
                return;
            }
            var title = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_88');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_89');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("plan") + 'PL_PlanStoreIssue/Delete_SuperProduct';
                var postData = {
                    KeyValue: self.selectedItemDetail.Id,
                    Entity: self.selectedItemDetail
                };
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
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
                        backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_86'));
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_50'));
                });
            }, title);
        }

        //打印面膜批次
        function printButtonHandler() {
            if (!self.selectedItemDetail || self.selectedItemDetail.ExeOrderType != "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_83'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_81'));
                return;
            }
            $state.go(rootstate + '.print', { id: self.selectedItemDetail.Id, selectedItem: self.selectedItemDetail });
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
        var moduleStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderDismantle';
        var moduleStateUrl = 'Siemens.SimaticIT_PlanApp_WorkOrderDismantle';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderDismantle';

        var state = {
            name: moduleStateName + '_WorkOrderDismantle',
            url: '/' + moduleStateUrl + '_WorkOrderDismantle',  
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/WorkOrderDismantle-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderDismantle.JS.Tips_90'
            }
        };
        $stateProvider.state(state);
    }
}());
