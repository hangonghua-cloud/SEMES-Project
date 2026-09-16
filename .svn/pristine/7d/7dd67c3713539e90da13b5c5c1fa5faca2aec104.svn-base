(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PrinterOrder').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PrinterOrder.PrinterOrder.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', '$timeout', 'i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, $timeout, i18nService) {
        var self = this;
        var logger, rootstate, messageservice, backendService;
        i18nService.setCurrentLang('zh-cn');

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.PrinterOrder.PrinterOrder');

            init();
            //初始化grid选项
            initGridOptions();
            //初始化子表grid选项
            //initGridOptionsDetail();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_PrinterOrder_PrinterOrder';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};
            self.callBackData = null;
            initDictionary();
            //子表明细
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData2 = [];
            self.searchParams2 = {};


            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            //子明细
            //self.add2ButtonHandler = add2ButtonHandler;//新增
            //self.edit2ButtonHandler = edit2ButtonHandler;//编辑
            // self.delete2ButtonHandler = delete2ButtonHandler;//删除
            self.editBGButtonHandler = editBGButtonHandler;//报工
            self.importButtonHandler = importButtonHandler;
        }
        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })
        $rootScope.$on("to-parentDetail", function (event, data) {
            self.callBackData = data;
            initGridData();
            // loadGridSelected(data.gridId,data.GridDetailId);

        })

        function initDictionary() {
            self.Factory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_1'), ResourceCode: "" }]
            };
            self.typeOrderType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_1'), ItemValue: "" }]
            };


            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.Factory.value = res.data.resultData[0];
                    }
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_1')
                    });
                    initGridData();
                }
            });
            commonService.getDataItemDuatil("PrinterStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeOrderType.options = res.data.resultData;
                    self.typeOrderType.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_3'),
                        width: 110
                    },
                    {
                        field: 'PrinterOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_4'),
                        width: 110
                    },
                    {
                        field: 'PlanOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_5'),
                        width: 110
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_6'),
                        width: 110
                    },


                    {
                        field: 'OrderDate',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_7'),
                        width: 120,
                        //type: 'date',
                        //cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'DeliveryDate',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_8'),
                        width: 120,
                        //type: 'date',
                        //cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_9'),
                        width: 130
                    },
                    {
                        field: 'Cylinder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'DesignColour',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_11'),
                        width: 110
                    },
                    {
                        field: 'MeterNum',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_12'),
                        width: 110
                    },
                    {
                        field: 'ReelNum',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_13'),
                        width: 110
                    },
                    {
                        field: 'ReelNum',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_14'),
                        width: 110
                    },
                    {
                        field: 'YSMeterNum',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_15'),
                        width: 140
                    },
                    {
                        field: 'YSReelNum',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_16'),
                        width: 140
                    },

                    {
                        field: 'YSWeightNum',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_17'),
                        width: 150
                    },
                    {
                        field: 'DFMeterNum',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_18'),
                        width: 140
                    },
                    {
                        field: 'ActReelNum',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_19'),
                        width: 140
                    },

                    {
                        field: 'Packaging',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_20'),
                        width: 110
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_21'),
                        width: 110
                    },
                    {
                        field: 'WorkOrderType',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_22'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'1\'"><span ng-cell-text>未生产</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'2\'"><span ng-cell-text>正在生产</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'3\'"><span ng-cell-text>已完成</span></div>'
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_26'),
                        width: 100
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_27'),
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
                                //initGridDataDetail();
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

            if (!self.Factory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_28'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//制单时间
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.Factory.value.ResourceCode;
            self.searchParams.WorkOrderType = self.typeOrderType.value.ItemValue;

            if (self.StartOrder && self.EndOrder) {
                self.searchParams.StartOrder = commonService.ConvertToLocalTime(self.StartOrder);
                self.searchParams.EndOrder = commonService.ConvertToLocalTime(self.EndOrder);
            } else {
                self.searchParams.StartOrder = "";
                self.searchParams.EndOrder = "";
            }
            if (self.StartDelivery && self.EndDelivery) {
                self.searchParams.StartDelivery = commonService.ConvertToLocalTime(self.StartDelivery);
                self.searchParams.EndDelivery = commonService.ConvertToLocalTime(self.EndDelivery);
            } else {
                self.searchParams.StartDelivery = "";
                self.searchParams.EndDelivery = "";
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_PrinterWorkOrder/PM_PrinterWorkOrderPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptions.data = res.data.resultData.rows;
                    if (self.callBackData != null) {
                        $timeout(function () {
                            var row = self.gridOptions.data.find(t => t.Id == self.callBackData.gridId);
                            $scope.gridApi.selection.selectRow(row);
                        }, 500);
                    }
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_29'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
            // initGridDataDetail();
        }
        function importButtonHandler() {
            $state.go(rootstate + '.import');
        }

        //新增
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        //编辑
        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            if (self.selectedItem.WorkOrderType != "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_30'), commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_31'));
                return;
            }
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_32');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_33');
            if (self.selectedItem.WorkOrderType != "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_30'), commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_31'));
                return;
            }


            backendService.confirm(text, function () {

                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_PrinterWorkOrder/RemovePM_PrinterWorkOrder';


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

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_29'));
                });
            }, title);
        }

        function editBGButtonHandler() {
            if (self.selectedItem.WorkOrderType == "3") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_34'), commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_31'));
                return;
            }
            $state.go(rootstate + '.editBG', { id: self.selectedItem.Id, selectedItem: self.selectedItem });

        }


        //删除 事件
        function delete2ButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_32');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_33');
            if (self.selectedItemDetail.WorkOrderType != "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_30'), commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_31'));
                return;
            }
            self.callBackData = {
                gridId: self.selectedItem.Id,
                gridDetialId: self.selectedItemDetail.Id,
            }

            backendService.confirm(text, function () {

                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_PrinterWorkOrder/RemovePM_PrinterWorkOrder';

                var postData = {
                    Entity: self.selectedItemDetail
                };

                commonService.callWebApiPost(url, postData).then(function (res) {

                    if ((res) && (res.data.success)) {
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridData()
                        //initGridDataDetail();
                        self.selectedItemDetail = null;
                        self.isDetailButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_29'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_PrinterOrder';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_PrinterOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PrinterOrder';

        var state = {
            name: moduleStateName + '_PrinterOrder',
            url: '/' + moduleStateUrl + '_PrinterOrder',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/PrinterOrder-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PrinterOrder.JS.Tips_35'
            }
        };
        $stateProvider.state(state);
    }
}());
