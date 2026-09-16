(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.ScrapRecord').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.ScrapRecord.ScrapRecord.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.ScrapRecord.ScrapRecord');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_ScrapRecord_ScrapRecord';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.searchParams = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.importButtonHandler = importButtonHandler; //导入
            self.deleteButtonHandler = deleteButtonHandler;
            self.searchButtonHandler = searchButtonHandler;//查询
            self.importShareButtonHandler = importShareButtonHandler; //导入分摊

            self.typeFactoryChange = typeFactoryChange;
            self.typeProcessChange = typeProcessChange;

            initDictionary();
        }
        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1'), ResourceCode: "" }]
            };
            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1'), ResourceCode: "" }]
            };

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1')
                    });
                    initGridData();
                }
            });
            //报废原因
            self.typeScrapReason = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1'), ItemValue: "" }]
            };

        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeProcess.options = res.data.resultData;
                        self.typeProcess.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1')
                        });
                    }
                });
                //报废原因
                commonService.getDataItemDuatil("PoorWorkReport").then(function (res) {
                    if (res && res.data.success) {
                        self.typeScrapReason.options = res.data.resultData.filter(t => t.Remark1 == newItem.ResourceCode);
                    }
                })

            }
            else {
                self.typeProcess = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1'), ResourceCode: "" }]
                };
                self.typeScrapReason = {
                    value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1'), ItemValue: "" },
                    options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1'), ItemValue: "" }]
                };
            }
        }

        function typeProcessChange(oldItem, newItem) {
            // if (newItem.ResourceCode) {
            //     commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
            //         if (res && res.data.success) {
            //             self.typeProcess.options = res.data.resultData;
            //             self.typeProcess.options.splice(0, 0, {
            //                 ResourceCode: "",
            //                 ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1')
            //             });
            //         }
            //     });
            // }
            // else {
            //     self.typeProcess = {
            //         value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1'), ResourceCode: "" },
            //         options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_1'), ResourceCode: "" }]
            //     };
            // }
        }


        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })

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
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_3'),
                        width: 110
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_4'),
                        width: 110
                    },
                    {
                        field: 'ScrapCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'BadItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_6'),
                        width: 200
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_7'),
                        width: 110
                    },
                    {
                        field: 'ScrapQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'StartTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_9'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'EndTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_10'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    // {
                    //     field: 'Remark',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_11'),
                    //     width: 200
                    // },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_12'),
                        width: 100
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_13'),
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
                sord: 'Desc'
            };

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_14'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.ProcessCode = self.typeProcess.value.ResourceCode;
            self.searchParams.BadItemCode = self.typeScrapReason.value.ItemValue;

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
            var url = commonService.getMesApiAddress("ProduceManage") + 'PMScrapRecord/GetPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_15'));
            });
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
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //导入
        function importButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select');
        }

        //导入分摊
        function importShareButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.import');
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_16');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_17');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("ProduceManage") + 'PMScrapRecord/RemoveForm';
                //提交删除当前选择数据实体
                var postData = {
                    id: self.selectedItem.Id
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_15'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_ScrapRecord';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_ScrapRecord';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/ScrapRecord';

        var state = {
            name: moduleStateName + '_ScrapRecord',
            url: '/' + moduleStateUrl + '_ScrapRecord',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ScrapRecord-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.ScrapRecord.JS.Tips_18'
            }
        };
        $stateProvider.state(state);
    }
}());
