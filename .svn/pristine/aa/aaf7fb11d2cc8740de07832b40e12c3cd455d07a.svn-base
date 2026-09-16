(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PMProductPrice').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PMProductPrice.PMProductPrice.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.PMProductPrice.PMProductPrice');

            init();
            initGridOptions();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_PMProductPrice_PMProductPrice';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.searchParams = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.add2ButtonHandler = add2ButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.importWLButtonHandler = importWLButtonHandler;
            self.importVCButtonHandler = importVCButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;
            self.searchButtonHandler = searchButtonHandler;

            self.typeFactoryChange = typeFactoryChange;

            //数据字典
            initDictionary();
        }

        //回调函数
        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })

        function initDictionary() {

            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_1'), ResourceCode: "" },
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_1'), ResourceCode: "" },
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_1')
                    });
                    initGridData();
                }
            });
            //工序
            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_1'), ResourceCode: "" }]
            };
            //工价类型
            self.typePriceType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_1'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("PriceType").then(function (res) {
                if (res && res.data.success) {
                    self.typePriceType.options = res.data.resultData;
                }
            })
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeProcess.options = res.data.resultData;
                        self.typeProcess.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_1')
                        });
                    }
                });
            } else {
                self.typeProcess = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_1'), ResourceCode: "" }]
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_2'), width: 75, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    // {
                    //     field: 'FactoryCode',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_3'),
                    //     width: 120
                    // },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_4'),
                        width: 120
                    },
                    // {
                    //     field: 'ProcessCode',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_5'),
                    //     width: 120
                    // },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'PostName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'PriceTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_10'),
                        width: 200
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_11'),
                        width: 200
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_12'),
                        width: 80
                    },
                    {
                        field: 'PeopleQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_13'),
                        width: 120
                    },
                    {
                        field: 'Price',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_14'),
                        width: 80
                    },
                    {
                        field: 'IsDefault', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_15'),
                        width: 100,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.IsDefault==true"><span ng-cell-text class="">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.IsDefault!=true"><span ng-cell-text class="">否</span></div>'
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_18'),
                        width: 120
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_19'),
                        width: 120
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_20'),
                        width: 150,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM',
                        //cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
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
            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//创建时间
                sord: 'desc'
            };

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_21'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.ProcessCode = self.typeProcess.value.ResourceCode;
            self.searchParams.PriceType = self.typePriceType.value.ItemValue;
            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PMProductPrice/GetPageDataTableList'
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridOptions.totalItems = res.data.resultData.records;
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_22'), commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_23'));
            });
        }

        //查询
        function searchButtonHandler(clickedCommand) {
            initGridData();
        }

        //新增
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        //VC新增
        function add2ButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addDetail');
        }

        //编辑
        function editButtonHandler(clickedCommand) {
            if (self.selectedItem.PriceType == "1")
                $state.go(rootstate + '.edit', { selectedItem: self.selectedItem });
            else
                $state.go(rootstate + '.editDetail', { selectedItem: self.selectedItem });
        }

        //物料导入
        function importWLButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.importWL');
        }

        //物料导入
        function importVCButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.importVC');
        }

        //删除
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_24');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_25');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("ProduceManage") + 'PMProductPrice/RemoveForm';
                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItem
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_23'));
                });
            }, title);
        }
    }
    ListScreenRouteConfig.$inject = ['$stateProvider'];
    function ListScreenRouteConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_PMProductPrice';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_PMProductPrice';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PMProductPrice';

        var state = {
            name: moduleStateName + '_PMProductPrice',
            url: '/' + moduleStateUrl + '_PMProductPrice',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/PMProductPrice-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PMProductPrice.JS.Tips_26'
            }
        };
        $stateProvider.state(state);
    }
}());
