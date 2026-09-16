(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.SuperProductStock').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManage.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManage');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_SuperProductStock_SuperProductStockManage';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//转入
            self.editButtonHandler = editButtonHandler;//转出
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            self.adjustButtonHandler = adjustButtonHandler;//库存校准

            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
            initDictionary();
            self.typeFactoryChange = typeFactoryChange;
        }

        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_1'), ResourceCode: "" }]
            };
            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_1'), ResourceCode: "" }]
            };
            //仓库
            //self.Warehouse = {
            //    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_1'), ResourceCode: "" },
            //    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_1'), ResourceCode: "" }]
            //};
            //工厂
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_1')
                    });
                    initGridData();
                }
            });
            ////仓库
            //commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
            //    if (res && res.data.success) {
            //        self.Warehouse.options = res.data.resultData;
            //        self.Warehouse.options.splice(0, 0, {
            //            ResourceCode: "",
            //            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_1')
            //        });
            //    }
            //});
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeProcess.options = res.data.resultData;
                        self.typeProcess.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_1')
                        });
                    }
                });
            }
            else {
                self.typeProcess = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_1'), ResourceCode: "" }]
                };
            }
        }

        //初始化grid选项
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
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_3'),
                        width: 100
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_5'),
                        width: 150
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_6'),
                        width: 320
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_7'),
                        width: 200
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_18'),
                        width: 130
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_19'),
                        width: 130
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'StockQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_10'),
                        width: 130
                    },
                    {
                        field: 'LockedQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_11'),
                        width: 130
                    },
                    {
                        field: 'NoLockQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_12'),
                        width: 130
                    }
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
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_13'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'ProcessCode,MaterialCode,BatchNo',//工序编码、物料编码、批次号
                sord: 'asc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.ProcessCode = self.typeProcess.value.ResourceCode;
            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'MM_SuperProductStock/MM_SuperProductStockPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_14'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
        }

        //转入
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        //转出
        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }
        //库存校准
        function adjustButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.adjust', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_15');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_16');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("material") + 'MM_SuperProductStock/RemoveMM_SuperProductStock';
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_14'));
                });
            }, title);
        }

        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        });

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
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_SuperProductStock';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_SuperProductStock';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/SuperProductStock';

        var state = {
            name: moduleStateName + '_SuperProductStockManage',
            url: '/' + moduleStateUrl + '_SuperProductStockManage',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/SuperProductStockManage-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManagelistctrl.Tips_17'
            }
        };
        $stateProvider.state(state);
    }
}());
