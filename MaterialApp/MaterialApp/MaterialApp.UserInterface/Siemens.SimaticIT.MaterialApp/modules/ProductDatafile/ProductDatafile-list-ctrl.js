(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.ProductDatafile').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafile.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafile');

            init();
            //初始化grid选项
            initGridOptions();
            setTimeout(function () {
                initGridData();
            }, 100);//如果查询条件有下拉参数，请调整此值到1000

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_ProductDatafile_ProductDatafile';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.searchParams = {};
            //子表明细
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            self.viewerOptions2 = {};
            self.viewerData2 = [];
            self.searchParams2 = {};
            self.pagination = {
                rows: 20,
                page: 1,
                sidx: 'WorkOrder,DeliveryDate ',//工厂编码
                sord: 'asc'
            };

            initDictionary();

            self.searchButtonHandler = searchButtonHandler;
            self.addMeasure = addMeasure;
            self.addSort = addSort;
        }




        function initDictionary() {

            self.typeOrderType = {
                value: { RuleName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_1'), RuleCode: "" },
                options: [{ RuleName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_1'), RuleCode: "" }]
            };
            self.typeOrderStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_1'), ItemValue: "" }]
            };

            self.typeWorkOrderStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_1'), ItemValue: "" }]
            };
            self.typeWoType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_1'), ItemValue: "" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_1'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_2'), ItemValue: "1" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_3'), ItemValue: "2" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_4'), ItemValue: "3" },
                ]
            };

            commonService.getDataItemDuatil("OrderStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeOrderStatus.options = res.data.resultData;
                    self.typeOrderStatus.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("OrderType").then(function (res) {
                if (res && res.data.success) {
                    self.typeOrderType.options = res.data.resultData;
                    self.typeOrderType.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
        }

        $rootScope.$on('to-parent', function (event, data) {
            initGridData();
        });

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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_5'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_6'),
                        width: 130
                    },
                    {
                        field: 'CustomerName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_7'),
                        width: 130
                    },
                    {
                        field: 'OrderType',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_8'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'1\'"><span ng-cell-text>出口</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'2\'"><span ng-cell-text>内销</span></div>'
                    },
                    {
                        field: 'ProductPlanNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_9'),
                        width: 180
                    },
                    {
                        field: 'OrderDate',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_10'),
                        width: 130,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'DeliveryDate',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_11'),
                        width: 130,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'BoxDate',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_12'),
                        width: 130,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'
                    },
                    {
                        field: 'OrderStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_13'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'1\'"><span ng-cell-text>创建</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'2\'"><span ng-cell-text>审核</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'3\'"><span ng-cell-text>生产中</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'4\'"><span ng-cell-text>已完成</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'5\'"><span ng-cell-text>已发货</span></div>'
                    },
                    // {
                    //     field: 'Technology',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_14'),
                    //     width: 180
                    // },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_15'),
                        width: 180
                    },
                    {
                        field: 'IssueStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_16'),
                        width: 140,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.IssueStatus==\'0\'"><span ng-cell-text>未下发</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.IssueStatus==\'1\'"><span ng-cell-text>已下发</span></div>'


                    },
                    {
                        field: 'WoStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_17'),
                        width: 140,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.WoStatus==\'1\'"><span ng-cell-text>未发布</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.WoStatus==\'2\'"><span ng-cell-text>部分发布</span></div>' +

                            '<div class="ngCellText" ng-if="row.entity.WoStatus==\'3\'"><span ng-cell-text>已发布</span></div>'
                    },
                    {
                        field: 'Salesman',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_18'),
                        width: 120
                    },
                    {
                        field: 'Codename',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_19'),
                        width: 120
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_20'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'AuditName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_21'),
                        width: 120
                    },
                    {
                        field: 'AuditTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_22'),
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

                            } else {
                                self.selectedItem = null;
                                //self.isButtonVisible = false;

                            }
                        }
                    });
                },
                data: []
            }
        }

        function initGridData() {

            self.selectedItem = null;
            self.isButtonVisible = false;
            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//生产订单
                sord: 'asc'
            };
            self.searchParams.OrderStatus = self.typeOrderStatus.value.ItemValue;
            self.searchParams.OrderType = self.typeOrderType.value.ItemValue;
            self.searchParams.WoStatus = self.typeWoType.value.ItemValue;
            if (self.StartPrepay && self.EndPrepay) {
                self.searchParams.StartPrepay = commonService.ConvertToLocalTime(self.StartPrepay);
                self.searchParams.EndPrepay = commonService.ConvertToLocalTime(self.EndPrepay);
            }
            self.searchParams.QueryFilter1 = "1";//排除待归档的订单

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress('plan') + 'PL_ProductionOrder/PL_ProductionOrderPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_23'));
            });
        }

        //查询
        function searchButtonHandler() {

            initGridData();
        }


        //按日期归档
        function addSort(clickedCommand) {
            $state.go(rootstate + '.add');
        }
        //按订单进行归档
        function addMeasure(clickedCommand) {
            let data = $scope.gridApi.selection.getSelectedRows();
            // if (data.length == 0) {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_24'), commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_25'));
            //     return;
            // }
            // $state.go(rootstate + '.edit', { selectedItem: data });

            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_26');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_27');

            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('material') + 'Base_DataFileCon/Save_ProductOrderFileCon';

                //提交删除当前选择数据实体
                var postData = {
                    data: data
                };
                busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_28') });
                commonService.callWebApiPost(url, postData).then(function (res) {
                    busyIndicatorService.hide();
                    if ((res) && (res.data.success)) {
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        initGridData();
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    busyIndicatorService.hide();
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_23'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_ProductDatafile';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_ProductDatafile';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/ProductDatafile';

        var state = {
            name: moduleStateName + '_ProductDatafile',
            url: '/' + moduleStateUrl + '_ProductDatafile',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ProductDatafile-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafilelistctrl.Tips_29'
            }
        };
        $stateProvider.state(state);
    }
}());
