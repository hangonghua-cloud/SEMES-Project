(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderSort').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderSort.WorkOrderSort.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.PlanApp.WorkOrderSort.WorkOrderSort');

            init();
            //初始化grid选项
            initGridOptions();
            initGridOptionsDetail();
            setTimeout(function () {
                //初始化grid数据、查询
                initGridData();
            }, 100);//如果查询条件有下拉参数
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_PlanApp_WorkOrderSort_WorkOrderSort';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};
            self.searchParams1 = {};
            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;
            self.searchButtonHandler = searchButtonHandler;
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
                paginationPageSizes: [100, 300, 500,1000], //每页显示个数选项
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
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_1'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'RuleCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_2'),
                        width: 500
                    },
                    {
                        field: 'RuleName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_3'),
                        width: 500
                    },
                    // {
                    //     field: 'FieldName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_4'),
                    //     width: 120
                    // },
                    // {
                    //     field: 'IsAsc',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_5'),
                    //     width: 100,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.IsAsc==true"><span ng-cell-text >正序</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.IsAsc!=true"><span ng-cell-text >倒序</span></div>'
                    // },
                    // {
                    //     field: 'SortCode',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_8'),
                    //     width: 100
                    // },
                    // {
                    //     field: 'Creator',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_9'),
                    //     width: 140
                    // },
                    // {
                    //     field: 'CreateTime',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_10'),
                    //     width: 170,
                    //     type: 'date',
                    //     cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    // },
                    // {
                    //     field: 'ModifyBy',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_11'),
                    //     width: 140
                    // },
                    // {
                    //     field: 'ModifyTime',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_12'),
                    //     width: 170,
                    //     type: 'date',
                    //     cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    // }
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
                                initGridDataDetail();
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
                sidx: 'RuleCode ASC ',//排序规则编码
                sord: 'asc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
 
            var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrderSortRule/PL_WorkOrderSortRulePageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_13'));
            });
        }

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
                paginationPageSizes: [100, 300, 500,1000], //每页显示个数选项
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_1'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    // {
                    //     field: 'RuleCode',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_2'),
                    //     width: 150
                    // },
                    // {
                    //     field: 'RuleName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_3'),
                    //     width: 150
                    // },
                    {
                        field: 'FieldName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_4'),
                        width: 200
                    },
                    {
                        field: 'IsAsc',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_5'),
                        width: 200,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.IsAsc==true"><span ng-cell-text >正序</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.IsAsc!=true"><span ng-cell-text >倒序</span></div>'
                    },
                    {
                        field: 'SortCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_8'),
                        width: 200
                    },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_9'),
                        width: 140
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_10'),
                        width: 170,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'ModifyBy',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_11'),
                        width: 140
                    },
                    {
                        field: 'ModifyTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_12'),
                        width: 170,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
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
                    // $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                    //     if (row) {
                    //         if (row.isSelected) {
                    //             self.selectedItem = row.entity;
                    //             self.isButtonVisible = true;
                    //             //console.log (self.selectedItem);
                    //         } else {
                    //             self.selectedItem = null;
                    //             self.isButtonVisible = false;
                    //         }
                    //     }
                    // });
                },
                data: []
            }
        }

        //查询
        function searchButtonHandler() {
            initGridDataDetail();
            initGridData();
        }

        //查询方法,数据绑定
        function initGridDataDetail() {

            //self.selectedItem = null;
            //self.isButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsDetail.paginationPageSize,
                page: self.gridOptionsDetail.paginationCurrentPage,
                sidx: 'RuleCode ASC,SortCode ',//排序规则编码
                sord: 'asc'
            };
            if (!!self.selectedItem) {
                self.searchParams1.RuleCode = self.selectedItem.RuleCode;
            } else {
                self.gridOptionsDetail.data = [];
                return;
            }


            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams1
            };
            console.log('queryParmeters-----' + JSON.stringify(queryParmeters));
            var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrderSortRule/PL_WorkOrderSortRulePageList';
            //var url = 'http://localhost:49849/' + 'PL_WorkOrderSortRule' + '/PL_WorkOrderSortRulePageDataTableList'; 
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                //console.log ('-self.Post_ResultData----------------------' + JSON.stringify(res));
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsDetail.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsDetail.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsDetail.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_13'));
            });
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

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_14');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_15');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrderSortRule/RemovePL_WorkOrderSortRule';
                //console.log("删除----------------" + url);
                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];
                self.UserCode = user.loginName;
                self.UserName = user.fullName;
                self.selectedItem.UpdateByCode = self.UserCode;
                self.selectedItem.UpdateByName = self.UserCode + '-' + self.UserName;
                if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                    self.selectedItem.UpdateByName = self.UserCode;
                }
                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItem
                };
                // console.log("new postData------------------------------------" + JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (res) {
                    //console.log("RemovePL_WorkOrderSortRule----------------------------" + JSON.stringify(res));
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridData();
                        initGridDataDetail();
                        self.selectedItem = null;
                        self.isButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                        //  console.log('删除数据出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_18'));
                    }
                }, function (error) {
                    //console.log("RemovePL_WorkOrderSortRule--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_13'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderSort';
        var moduleStateUrl = 'Siemens.SimaticIT_PlanApp_WorkOrderSort';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderSort';

        var state = {
            name: moduleStateName + '_WorkOrderSort',
            url: '/' + moduleStateUrl + '_WorkOrderSort',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/WorkOrderSort-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderSort.JS.Tips_19'
            }
        };
        $stateProvider.state(state);
    }
}());
