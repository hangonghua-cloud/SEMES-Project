(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.APPRole').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.SystemApp.APPRole.APPRole.service', '$state', '$stateParams', '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'commonService', 'common.widgets.notificationTile.globalService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, commonService, notificationService) {
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.SystemApp.APPRole.APPRole');

            init();
            initGridOptions();
            initGridData();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_SystemApp_APPRole_APPRole';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;
            
            //Initialize Model Data
            self.selectedItem = null;
            self.searchParams = {};
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;
            self.authorButtonHandler = selectButtonHandler;
            self.searchButtonHandler = initGridData;
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
                paginationPageSizes: [20, 25, 30, 50, 75, 100], //每页显示个数选项
                paginationPageSize: 25, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮          
                //选中
                rowTemplate: "<div ng-dblclick=\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRolelistctrl.Tips_1'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    }, {
                        field: 'Id',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRolelistctrl.Tips_2'),
                        visible: false,
                        width: 200
                    }
                    , {
                        field: 'RoleCode',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRolelistctrl.Tips_3'),
                        width: 200
                    }
                    , {
                        field: 'RoleName',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRolelistctrl.Tips_4'),
                        width: 200
                    }
                    , {
                        field: 'CreateUserName',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRolelistctrl.Tips_5'),
                        width: 200
                    }, {
                        field: 'CreateDate',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRolelistctrl.Tips_6'),
                        width: 200,
                        type: 'date',
                        cellFilter: 'date:\'yyyy-MM-dd\'',
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
                                self.isButtonVisible = true;
                                console.log(self.selectedItem);
                                //initGridOperationOptions();
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

        function initGridData() {
            var Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateDate',
                sord: 'desc'
            };
            let queryParmeters = {
                pagination: Pagination,
                queryJson: {
                    Code: self.searchParams.RoleCode,
                    Name: self.searchParams.RoleName
                }
            };


            var url = commonService.getMesApiAddress() + 'BSAppRole/GetPageListJson';
            // var url = 'http://localhost:49852/Equipment/GetPage_MaintenanceItem';
            console.log("url----------------" + url);
            console.log("uqueryParmetersrl----------------" + JSON.stringify(queryParmeters));
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

                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRolelistctrl.Tips_7'));
            });
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
            console.log(self.selectedItem.Id)
            var url = commonService.getMesApiAddress() + 'BSAppRole/DeleteForm?userId=' + commonService.getLoginUser().loginName + '&keyValue=' + self.selectedItem.Id;
            var text = "确定要删除编号为 '" + self.selectedItem.RoleCode + "' 的记录吗?";
            backendService.confirm(text, function () {
                commonService.callWebApiGet(url, null).then(function (res) {
                    if (res.data.success) {
                        reload: true;
                        var resultData = res.data.resultData;
                        notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRolelistctrl.Tips_10'));
                        initGridData();
                    } else {
                        var resultData = res.data.resultData;
                        notificationService.warning(res.data.returnMsg);
                        initGridData();
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRolelistctrl.Tips_7'));
                });

            }, commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRolelistctrl.Tips_11'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_SystemApp_APPRole';
        var moduleStateUrl = 'Siemens.SimaticIT_SystemApp_APPRole';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/APPRole';

        var state = {
            name: moduleStateName + '_APPRole',
            url: '/' + moduleStateUrl + '_APPRole',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/APPRole-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.APPRole.APPRolelistctrl.Tips_12'
            }
        };
        $stateProvider.state(state);
    }
}());
