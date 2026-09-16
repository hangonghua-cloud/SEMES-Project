(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PMPerformanceManage').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PMPerformanceManage.PMPerformanceManage.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.PMPerformanceManage');

            init();
            initGridOptions();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_PMPerformanceManage_PMPerformanceManage';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.searchParams = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler; //新增
            self.importButtonHandler = importButtonHandler; //导入
            self.exportButtonHandler = exportButtonHandler; //导出
            self.selectButtonHandler = selectButtonHandler; //查看
            self.searchButtonHandler = searchButtonHandler; //查询

            self.typeFactoryChange = typeFactoryChange;
            self.typeWorkShopChange = typeWorkShopChange;
            self.typeProcessChange = typeProcessChange;

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
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1')
                    });
                    initGridData();
                }
            });

            //车间
            self.typeWorkShop = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1'), ResourceCode: "" }]
            };
            //工序
            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1'), ResourceCode: "" }]
            };
            //岗位
            self.typePost = {
                value: { Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1'), Col1: "" },
                options: [{ Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1'), Col1: "" }]
            };
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeWorkShop.options = res.data.resultData;
                        self.typeWorkShop.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1')
                        });
                    }
                });
            } else {
                self.typeWorkShop = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1'), ResourceCode: "" }]
                };
            }
        }

        function typeWorkShopChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeProcess.options = res.data.resultData;
                        self.typeProcess.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1')
                        });
                    }
                });
            } else {
                self.typeProcess = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1'), ResourceCode: "" }]
                };
            }
        }

        function typeProcessChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getKeyParameterItem({ ItemCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typePost.options = res.data.resultData;
                        self.typePost.options.splice(0, 0, {
                            Col1: "",
                            Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1')
                        });
                    }
                });
            } else {
                self.typePost = {
                    value: { Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1'), Col1: "" },
                    options: [{ Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_1'), Col1: "" }]
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_2'), width: 75, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'WorkshopName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'PostName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'UserCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'UserName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'PayrollDate',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_9'),
                        width: 120,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2',//年月日
                    },
                    {
                        field: 'CardCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_11'),
                        width: 120
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_12'),
                        width: 120
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_13'),
                        width: 120
                    },
                    {
                        field: 'Price',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_14'),
                        width: 120
                    },
                    {
                        field: 'Coefficient',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_15'),
                        width: 120
                    },
                    {
                        field: 'TotalCoefficient',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_16'),
                        width: 120
                    },

                    // {
                    //     field: 'PTeamCode',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_17'),
                    //     width: 120
                    // },
                    // {
                    //     field: 'PeopleQty',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_18'),
                    //     width: 120
                    // },
                    // {
                    //     field: 'UnitName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_19'),
                    //     width: 120
                    // },
                    {
                        field: 'EquipCoefficient',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_20'),
                        width: 120
                    },
                    {
                        field: 'Salary',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_21'),
                        width: 120
                    },
                    {
                        field: 'InfoSource',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_22'),
                        width: 120
                    },
                    {
                        field: 'Description',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_23'),
                        width: 120
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_24'),
                        width: 120
                    },

                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_25'),
                        width: 120
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_26'),
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
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_27'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.WorkshopCode = self.typeWorkShop.value.ResourceCode;
            self.searchParams.ProcessCode = self.typeProcess.value.ResourceCode;
            self.searchParams.PostCode = self.typePost.value.Col1;
            if (self.StartTime)
                self.searchParams.StartTime = commonService.ConvertToLocalDate(self.StartTime);
            else
                self.searchParams.StartTime = "";
            if (self.EndTime)
                self.searchParams.EndTime = commonService.ConvertToLocalDate(self.EndTime);
            else
                self.searchParams.StartTime = "";

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PMPerformanceManage/GetPageDataTableList'
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridOptions.totalItems = res.data.resultData.records;
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_29'));
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

        //导入
        function importButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.import', { selectedItem: self.selectedItem });
        }
        //导出
        function exportButtonHandler(clickedCommand) {

            // if (!self.typeFactory.value.ResourceCode) {
            //     commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_27'));
            //     return;
            // }
            // self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            // self.searchParams.WorkshopCode = self.typeWorkShop.value.ResourceCode;
            // self.searchParams.ProcessCode = self.typeProcess.value.ResourceCode;
            // self.searchParams.PostCode = self.typePost.value.Col1;
            // if (self.StartTime)
            //     self.searchParams.StartTime = commonService.ConvertToLocalDate(self.StartTime);
            // else
            //     self.searchParams.StartTime = "";
            // if (self.EndTime)
            //     self.searchParams.EndTime = commonService.ConvertToLocalDate(self.EndTime);
            // else
            //     self.searchParams.StartTime = "";

            let postData = {
                queryJson: self.searchParams
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_30') });
            var url = commonService.getMesApiAddress('ProduceManage') + 'PMPerformanceManage/PMPerformanceManage_Export';
            commonService.callWebApiPost(url, postData).then(function (res) {
                busyIndicatorService.hide();
                if ((res) && (res.data.success)) {
                    if (res.data.resultData != null) {
                        let filePath = commonService.getMesApiAddress('ProduceManage') + res.data.resultData;
                        commonService.openDownloadDialog(filePath, '');
                    }
                }
            }, function (error) {
                busyIndicatorService.hide();
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_28'));
            });
        }

        //查看
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { selectedItem: self.selectedItem });
        }

        //删除
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_31');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_32');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("ProduceManage") + 'PMPerformanceManage/RemoveForm';
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_29'));
                });
            }, title);
        }
    }
    ListScreenRouteConfig.$inject = ['$stateProvider'];
    function ListScreenRouteConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_PMPerformanceManage';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_PMPerformanceManage';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PMPerformanceManage';

        var state = {
            name: moduleStateName + '_PMPerformanceManage',
            url: '/' + moduleStateUrl + '_PMPerformanceManage',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/PMPerformanceManage-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PMPerformanceManage.JS.Tips_33'
            }
        };
        $stateProvider.state(state);
    }
}());
