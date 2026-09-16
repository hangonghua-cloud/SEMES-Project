(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.OrderMaterial').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.PlanApp.OrderMaterial.OrderMaterial.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service','i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, i18nService) {
        var self = this;
        var logger, rootstate, messageservice, backendService;
        i18nService.setCurrentLang('zh-cn');

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.PlanApp.OrderMaterial.OrderMaterial');

            init();
            //初始化grid选项
            initGridOptions();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_PlanApp_OrderMaterial_OrderMaterial';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.searchParams = {};
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];

            initDictionary();
            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
        }

        function initDictionary() {

            self.typeDemandMaterial = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_1'), ItemValue: "" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_1'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_2'), ItemValue: 1 },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_3'), ItemValue: 0 }
                ]
            };
            self.typeOrderStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_1'), ItemValue: "" }]
            };


            commonService.getDataItemDuatil("OrderStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeOrderStatus.options = res.data.resultData;
                    self.typeOrderStatus.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_1')
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
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: true,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_4'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_6'),
                        width: 130
                    },
                    {
                        field: 'OrderStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_7'),
                        width: 120,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'1\'"><span ng-cell-text>创建</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'2\'"><span ng-cell-text>审核</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'3\'"><span ng-cell-text>生产中</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'4\'"><span ng-cell-text>已完成</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'5\'"><span ng-cell-text>已发货</span></div>'
                    },
                    {
                        field: 'Customer',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_13'),
                        width: 120
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_14'),
                        width: 120
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_15'),
                        width: 120
                    },

                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_16'),
                        width: 110
                    },
                    {
                        field: 'ShouldNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_17'),
                        width: 110
                    },
                    {
                        field: 'ActualNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_18'),
                        width: 110
                    },
                    {
                        field: 'SuperNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_19'),
                        width: 110
                    },
                    {
                        field: 'CancellingNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_20'),
                        width: 110
                    },
                    {
                        field: 'ConsumeNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_21'),
                        width: 150
                    },
                    // {
                    //     field: 'Creator', 
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_22'),
                    //     width: 110
                    // },
                    //{
                    //    field: 'CreateTime',
                    //    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_23'),
                    //    width: 160,
                    //    type: 'date',
                    //    cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    //},
                    //{
                    //    field: 'Remark',
                    //    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_24'),
                    //    width: 200
                    //},

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

        //查询方法,数据绑定
        function initGridData() {
            self.selectedItem = null;
            self.isButtonVisible = false;

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_25'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'ProductOrder',//执行工单号
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.OrderStatus = self.typeOrderStatus.value.ItemValue;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress('plan') + 'PL_ExeWorkOrderWearingLayer/GetOrderListWithPage';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_26'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
        }

        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
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
        var moduleStateName = 'home.Siemens_SimaticIT_PlanApp_OrderMaterial';
        var moduleStateUrl = 'Siemens.SimaticIT_PlanApp_OrderMaterial';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/OrderMaterial';

        var state = {
            name: moduleStateName + '_OrderMaterial',
            url: '/' + moduleStateUrl + '_OrderMaterial',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/OrderMaterial-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.OrderMaterial.JS.Tips_27'
            }
        };
        $stateProvider.state(state);
    }
}());
