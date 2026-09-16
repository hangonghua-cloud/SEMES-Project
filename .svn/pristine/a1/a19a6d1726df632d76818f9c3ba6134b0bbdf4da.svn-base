(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.TransferCardResume').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.TransferCardResume.RawMaterialReceive.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.TransferCardResume.RawMaterialReceive');

            init();
            //初始化grid选项
            initGridOptions();
            //初始化子表grid选项
            initGridOptionsDetail();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_TransferCardResume_RawMaterialReceive';
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

            self.searchButtonHandler = searchButtonHandler;//查询
            self.typeFactoryChange = typeFactoryChange;

        }
        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_1'), ResourceCode: "" }]
            };
            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_1')
                    });
                    initGridData();
                }
            });

            // commonService.getResourceExtendInfo({ LevelCode: "Process" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.Process.options = res.data.resultData;
            //         self.Process.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_1')
            //         });
            //     }
            // });
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.Process.options = res.data.resultData;
                        self.Process.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_1')
                        });
                    }
                });
            } else {
                self.Process = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_1'), ResourceCode: "" }]
                };
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
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_5'),
                        width: 180
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_7'),
                        width: 180
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_8'),
                        width: 180
                    },
                    {
                        field: 'JCGG',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_9'),
                        width: 200
                    },
                    {
                        field: 'PalletNum',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_10'),
                        width: 140
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_11'),
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

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_12'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'ProductOrder',//创建时间
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.ProcessCode = self.Process.value.ResourceCode;


            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_TransferCardResume/GetPageDataTableListReceive';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_13'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
            initGridDataDetail();
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_14'),
                        width: 110
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_5'),
                        width: 140
                    },

                    {
                        field: 'CustomerPO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_15'),
                        width: 140
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_6'),
                        width: 110
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_8'),
                        width: 140
                    },
                    {
                        field: 'JCGG',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_9'),
                        width: 180
                    },
                    {
                        field: 'CardCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_16'),
                        width: 180
                    },
                    {
                        field: 'ExeWorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_17'),
                        width: 140
                    },
                    {
                        field: 'WorkOrderType',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_18'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'1\'"><span ng-cell-text>正常工单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'2\'"><span ng-cell-text>补料单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'3\'"><span ng-cell-text>拣余单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'4\'"><span ng-cell-text>免产单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.WorkOrderType==\'5\'"><span ng-cell-text>超产品单</span></div>'
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_11'),
                        width: 140
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_24'),
                        width: 200
                    },
                    {
                        field: 'UserName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_25'),
                        width: 140
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_26'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'
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
                sidx: 'PT.CreateTime',//创建时间
                sord: 'desc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.ProcessCode = self.selectedItem.ProcessCode;
                self.searchParams2.ProductOrder = self.selectedItem.ProductOrder;
                self.searchParams2.MaterialCode = self.selectedItem.MaterialCode;
                self.searchParams2.ContainerNO = self.selectedItem.ContainerNO;
                self.searchParams2.MMXH = self.selectedItem.MMXH;
                self.searchParams2.Spec = self.selectedItem.JCGG;

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
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_TransferCardResume/GetPageDataTableListReceiveItem';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_13'));
            });
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_TransferCardResume';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_TransferCardResume';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/TransferCardResume';

        var state = {
            name: moduleStateName + '_RawMaterialReceive',
            url: '/' + moduleStateUrl + '_RawMaterialReceive',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/RawMaterialReceive-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.RawMaterialReceive.JS.Tips_27'
            }
        };
        $stateProvider.state(state);
    }
}());
