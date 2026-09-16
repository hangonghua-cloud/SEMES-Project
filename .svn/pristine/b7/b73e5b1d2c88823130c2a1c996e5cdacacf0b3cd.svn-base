(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.ExeWorkOrderSW.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.ExeWorkOrderSW');

            init();
            initGridOptions();
        }

        function init() {

            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_ExeWorkOrderSW_ExeWorkOrderSW';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            initDictionary();
            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//挤出派工
            self.addButtonHandler2 = addButtonHandler2;//开槽派工
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            self.seqButtonHandler = seqButtonHandler;

            self.typeFactoryChange = typeFactoryChange;
            self.ProcessChange = ProcessChange;
        }
        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_1'), ResourceCode: "" }]
            };
            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_1'), ResourceCode: "" }]
            };
            self.Machine = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_1'), ResourceCode: "" }]
            };
            self.SWStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_1'), ItemValue: "" }]
            };
            //工厂
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_1')
                    });
                    initGridData();
                }
            });
            commonService.getDataItemDuatil("SWStatus").then(function (res) {
                if (res && res.data.success) {
                    self.SWStatus.options = res.data.resultData;
                    self.SWStatus.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
        }

        function typeFactoryChange(oldItem, newItem) {
            commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.Process.options = res.data.resultData;
                    self.Process.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_1')
                    });
                }
            });
        }
        function ProcessChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.Machine.options = res.data.resultData;
                    self.Machine.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_1')
                    });
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_2'), width: 70, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_3'),
                        width: 100
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_4'),
                        width: 100
                    },
                    {
                        field: 'EquipName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_5'),
                        width: 100
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_6'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'PlanProductTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_7'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    // {
                    //     field: 'ExeWorkOrder',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_8'),
                    //     width: 150
                    // },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_35'),
                        width: 150
                    },
                    {
                        field: 'WorkOrderTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_9'),
                        width: 100
                    },
                    {
                        field: 'SWStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_10'),
                        width: 120,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.SWStatus==\'1\'"><span ng-cell-text>未开工</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.SWStatus==\'2\'"><span ng-cell-text>正在生产</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.SWStatus==\'3\'"><span ng-cell-text>已完成</span></div>'

                    },
                    //{
                    //    field: 'ProductOrder',
                    //    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_14'),
                    //    width: 150
                    //},
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_15'),
                        width: 100
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_16'),
                        width: 120
                    },
                    {
                        field: 'MMCJ',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_17'),
                        width: 120
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_18'),
                        width: 100
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_19'),
                        width: 150
                    },
                    {
                        field: 'BWXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_20'),
                        width: 100
                    },
                    {
                        field: 'UV',
                        displayName: 'UV',
                        width: 100
                    },
                    {
                        field: 'KCKX',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_21'),
                        width: 100
                    },
                    {
                        field: 'OrderPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_22'),
                        width: 100
                    },
                    {
                        field: 'ActualSheets',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_23'),
                        width: 100
                    },
                    {
                        field: 'BGQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_24'),
                        width: 100
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
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_25'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'PlanProductTime',//排序字段
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.ProcessCode = self.Process.value.ResourceCode;
            self.searchParams.EquipCode = self.Machine.value.ResourceCode;
            self.searchParams.SWStatus = self.SWStatus.value.ItemValue;

            if (self.StartTime) {
                self.searchParams.StartTime = commonService.ConvertToLocalDate(self.StartTime);
            }
            else {
                self.searchParams.StartTime = "";
            }
            if (self.EndTime) {
                self.searchParams.EndTime = commonService.ConvertToLocalDate(self.EndTime);
            }
            else {
                self.searchParams.EndTime = "";
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ExeWorkOrderSW/PM_ExeWorkOrderSWPageDataTableList';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_26') });
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                busyIndicatorService.hide();
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                busyIndicatorService.hide();
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_27'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
        }



        //挤出派工
        function addButtonHandler(clickedCommand) {
            // $state.go(rootstate + '.add', { processType: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_28'), title: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_29'), process: "FHJC" });
            $state.go(rootstate + '.add', { processType: "JC", title: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_29') });
        }
        //开槽派工
        function addButtonHandler2(clickedCommand) {
            $state.go(rootstate + '.add', { processType: "KC", title: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_31') });
        }

        //编辑
        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }
        //派工顺序调整
        function seqButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.seq', {});
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_32');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_33');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress() + 'PM_ExeWorkOrderSW/RemovePM_ExeWorkOrderSW';
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
                        self.viewerOptions.serverDataOptions.optionsString = self.optionsStr;
                        self.viewerOptions.refresh();
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_27'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_ExeWorkOrderSW';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_ExeWorkOrderSW';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/ExeWorkOrderSW';

        var state = {
            name: moduleStateName + '_ExeWorkOrderSW',
            url: '/' + moduleStateUrl + '_ExeWorkOrderSW',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ExeWorkOrderSW-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.JS.Tips_34'
            }
        };
        $stateProvider.state(state);
    }
}());
