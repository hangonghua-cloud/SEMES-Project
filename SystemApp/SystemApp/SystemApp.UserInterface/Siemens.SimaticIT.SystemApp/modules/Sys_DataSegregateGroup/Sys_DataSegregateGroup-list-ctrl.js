(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup').config(ListScreenRouteConfig);


    ListScreenController.$inject = ['Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGroup.service', '$state', '$stateParams', '$rootScope',
        '$scope', 'common.base', 'common.services.logger.service', '$timeout', 'commonService', 'common.widgets.busyIndicator.service', 'common.widgets.notificationTile.globalService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, $timeout, commonService, busyIndicatorService, notification) {
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGroup');

            init();
            initGridOptions();
            initGridData();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_SystemApp_Sys_DataSegregateGroup_Sys_DataSegregateGroup';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.lableButtonHandler = lableButtonHandler;
            self.personButtonHandler = personButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;
            self.searchButtonHandler = initGridData;

            $rootScope.$on('to-parent', function (event, OnData) {
                
                $timeout(function () {
                    initGridData();  
                }, 1500); 
            });
            $rootScope.$on('to-addparent', function (event, OnData) {
                $timeout(function () {
                    initGridData();  
                }, 1500); 
               
            });
        }

        function initGridOptions() {
            //生产计划工单表
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
                paginationPageSizes: [20, 25, 30, 50, 75, 100], //每页显示个数选项
                paginationPageSize: 25, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮          
                //选中
                // rowTemplate: "<div ng-dblclick=\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_1'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    }, {
                        field: 'GroupCode',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_2'),
                        width: 180
                    }, {
                        field: 'GroupName',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_3'),
                        width: 180
                    }, {
                        field: 'GroupLabelValue',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_4'),
                        width: 180
                    }, {
                        field: 'CreateUserName',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_5'),
                        width: 180
                    }
                    , {
                        field: 'CreateDate',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_6'),//计划开工日期
                        width: 180,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter',
                        filterCellFiltered: true
                    }, {
                        field: 'ModifyUserName',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_7'),//计划完工日期
                        width: 180
                    }, {
                        field: 'ModifyDate',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_8'),//计划开工日期
                        width: 180,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter',
                        filterCellFiltered: true
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

                                initGridLabelData(self.selectedItem.GroupCode);
                                initGridPersonData(self.selectedItem.GroupCode);

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

            self.gridOptionsLabel = {
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
                paginationPageSizes: [20, 25, 30, 50, 75, 100], //每页显示个数选项
                paginationPageSize: 25, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮          
                //选中
                // rowTemplate: "<div ng-dblclick=\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_1'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    }, {
                        field: 'GroupCode',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_2'),
                        width: 200
                    }, {
                        field: 'LabelValue',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_9'),
                        width: 200
                    }, , {
                        field: 'LabelColor',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_10'),
                        width: 150,
                        cellTemplate:
                            '<div class="colorLabel" style=" background-color: {{row.entity.LabelColor}}"></div>'
                    }
                ],
                onRegisterApi: function (gridApi) {
                    /*$scope.gridProcessApi = gridApi;
                    $scope.gridProcessApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row.isSelected) {
                            self.selectedItemDetail = row.entity;
                            if (self.selectedItemDetail.SplitState == 0)
                                self.isButtonVisible = true;
                            else
                                self.isButtonVisible = false;
                            //console.log(self.selectedItemDetail);
                            //initGridOperationOptions();
                        } else {
                            self.selectedItemDetail = null;
                        }
                    });*/
                },
                data: []
            }
            //订单bom表
            self.gridOptionsPerson = {
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
                paginationPageSizes: [20, 25, 30, 50, 75, 100], //每页显示个数选项
                paginationPageSize: 25, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮          
                //选中
                // rowTemplate: "<div ng-dblclick=\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_1'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    }, {
                        field: 'GroupCode',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_2'),
                        width: 200
                    }, {
                        field: 'PersonCode',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_11'),
                        width: 200
                    }, {
                        field: 'PersonName',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_12'),
                        width: 200
                    }
                ],
                onRegisterApi: function (gridApi) {
                    //$scope.gridApi = gridApi;
                    //$scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                    //    if (row.isSelected) {
                    //        self.selectedDetail = row.entity;
                    //        // //console.log(row.entity);
                    //    }
                    //});
                },
                data: []
            }
        }
        function initGridData() {
            self.Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateDate',
                sord: 'desc'
            };
            var param = {
                pagination: self.Pagination,
                queryJson: {
                    GroupCode: self.searchParams.GroupCode,
                    GroupName: self.searchParams.GroupName
                }
            };

            var url = commonService.getMesApiAddress() + 'SystemManage/Sys_DataSegregateGroup/GetPageListJson';
            // //console.log("-self.searchParam----------------------------" + JSON.stringify(param));
            commonService.callWebApiPost(url, param).then(function (res) {
                if ((res) && (res.data.success)) {
                    //console.log(res.data.resultData.rows);
                    var resultData = res.data.resultData.rows;
                    self.gridOptions.data = resultData;
                    self.gridOptions.totalItems = res.data.resultData.records;
                    $scope.totalPage = res.data.resultData.total;
                    //console.log(self.gridOptions.data);
                } else {
                    //self.gridOptions.data = [];
                }

            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_13'));
            });
        }
        function initGridLabelData(code) {
            self.Pagination = {
                rows: self.gridOptionsLabel.paginationPageSize,
                page: self.gridOptionsLabel.paginationCurrentPage,
                sidx: 'Id',
                sord: 'asc'
            };
            var param = {
                pagination: self.Pagination,
                groupCode: code
            };

            var url = commonService.getMesApiAddress() + 'SystemManage/Sys_DataSegregateGroupToLabel/GetPageListJson';
            // //console.log("-self.searchParam----------------------------" + JSON.stringify(param));
            commonService.callWebApiPost(url, param).then(function (res) {
                if ((res) && (res.data.success)) {
                    //console.log(res.data.resultData.rows);
                    var resultData = res.data.resultData.rows;
                    self.gridOptionsLabel.data = resultData;
                    self.gridOptionsLabel.totalItems = res.data.resultData.records;
                    $scope.totalPage = res.data.resultData.total;
                    //console.log(self.gridOptionsLabel.data);
                } else {
                    //self.gridOptionsLabel.data = [];
                }

            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_13'));
            });
        }
        function initGridPersonData(code) {
            self.Pagination = {
                rows: self.gridOptionsPerson.paginationPageSize,
                page: self.gridOptionsPerson.paginationCurrentPage,
                sidx: 'Id',
                sord: 'asc'
            };
            var param = {
                pagination: self.Pagination,
                groupCode: code
            };

            var url = commonService.getMesApiAddress() + 'SystemManage/Sys_DataSegregateGroupToPerson/GetPageListJson';
            console.log("-self.searchParam----------------------------" + JSON.stringify(param));
            commonService.callWebApiPost(url, param).then(function (res) {
                if ((res) && (res.data.success)) {
                    //console.log(res.data.resultData.rows);
                    var resultData = res.data.resultData.rows;
                    self.gridOptionsPerson.data = resultData;
                    self.gridOptionsPerson.totalItems = res.data.resultData.records;
                    $scope.totalPage = res.data.resultData.total;
                    //console.log(self.gridOptionsPerson.data);
                } else {
                    //self.gridOptionsPerson.data = [];
                }

            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_13'));
            });
        }

        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        function editButtonHandler(clickedCommand) {
            
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }
        function personButtonHandler(clickedCommand) {

            $state.go(rootstate + '.person', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }
        function lableButtonHandler(clickedCommand) {

            $state.go(rootstate + '.lable', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }
        

        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function deleteButtonHandler(clickedCommand) {
            var title = "Delete";
            var url = commonService.getMesApiAddress() + 'SystemManage/Sys_DataSegregateGroup/DeleteForm?keyValue=' + self.selectedItem.Id;
            var text = "要删除隔离组编号为 '" + self.selectedItem.GroupCode + "' 的记录吗?";
            backendService.confirm(text, function () { 
                if (self.selectedItem.Foreman == null) {
                    commonService.callWebApiGet(url, null).then(function (res) {
                        if ((res) && (res.data.success)) {
                            notification.warning(res.data.returnMsg);
                            initGridData();
                        } else {
                            notification.warning(res.data.returnMsg);
                            self.gridOptions.data = [];
                            initGridData();
                        }
                    }, function (error) {
                        backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_16'));
                    });
                        }
                else {
                    notification.warning(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_17'));
                }
            }, commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_18'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_SystemApp_Sys_DataSegregateGroup';
        var moduleStateUrl = 'Siemens.SimaticIT_SystemApp_Sys_DataSegregateGroup';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/Sys_DataSegregateGroup';

        var state = {
            name: moduleStateName + '_Sys_DataSegregateGroup',
            url: '/' + moduleStateUrl + '_Sys_DataSegregateGroup',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/Sys_DataSegregateGroup-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplistctrl.Tips_19'
            }
        };
        $stateProvider.state(state);
    }
}());
