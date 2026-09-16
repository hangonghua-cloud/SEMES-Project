(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabel.service',
        '$state', '$stateParams', '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'commonService', 'common.widgets.notificationTile.globalService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, commonService,notificationService) {
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabel');

            init();
            initGridOptions();
            initGridData();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_SystemApp_Sys_DataSegregateLabel_Sys_DataSegregateLabel';
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
            self.deleteButtonHandler=deleteButtonHandler;
            self.searchButtonHandler = searchButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_1'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'LabelName',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_2'),
                        width: 150
                    }
                    , {
                        field: 'LabelColor',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_3'),
                        width: 150,
                         cellTemplate:
                        '<div class="colorLabel" style=" background-color: {{row.entity.LabelColor}}"></div>'
                    }
                    // , {
                    //     field: 'LabelValue',
                    //     displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_4'),
                    //     width: 150
                    // }
                //     , {
                //         field: 'CreateUser',
                //         displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_5'),
                //         width: 150
                //     } , {
                //         field: 'CreateDate',
                //         displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_6'),
                //         width: 200
                //         , type: 'date',
                //         cellFilter: 'date:"yyyy-MM-dd"'
                //     }, {
                //         field: 'ModifyUser',
                //         displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_7'),
                //         width: 150
                //     } 
                //    , {
                //         field: 'ModifyDate',
                //         displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_8'),
                //         width: 200,
                //         type: 'date',
                //         cellFilter: 'alpDatetimeFilter'
                //     }
                    , {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_9'),
                        width: 400
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
                                self.isButtonVisible = true;
                                self.selectedItem = row.entity;                           
                                console.log(self.selectedItemDetail);                              
                            } else {
                                self.isButtonVisible = false;
                                self.selectedItem = null;
                            }
                        }
                    });
                },
                data: []
            }
        }
        //查询方法,数据绑定
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

                }
            };

            var url = commonService.getMesApiAddress() + 'Sys_DataSegregate/Get_PageData'; 
            console.log("url----------------" + url);
            console.log("uqueryParmetersrl----------------" + JSON.stringify(queryParmeters));
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                console.log("-self.GetPage_Equipment----------------------------" + JSON.stringify(res));
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据            
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {

                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_10'));
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
            // TODO: Put here the properties of the entity managed by the service
            var text = "确定要删除 '" + self.selectedItem.LabelName + "'?";
            backendService.confirm(text, function () {
                let url = commonService.getMesApiAddress() + 'Sys_DataSegregate/Delete' ;
                let params={
                    Id:self.selectedItem.Id,
                    userName:commonService.getLoginUser().loginName
                };
                console.log("delete------------params"+JSON.stringify());
                commonService.callWebApiPost(url, params).then(function (res) {
                    if ((res) && (res.data.success)) {
                       
                        var resultData = res.data.resultData;
                        notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_12'));
                        initGridData();
                    } else {
                        self.gridOptions.data = [];
                        backendService.genericError(res.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_10'));
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_10'));
                });

            }, commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_13'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_SystemApp_Sys_DataSegregateLabel';
        var moduleStateUrl = 'Siemens.SimaticIT_SystemApp_Sys_DataSegregateLabel';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/Sys_DataSegregateLabel';

        var state = {
            name: moduleStateName + '_Sys_DataSegregateLabel',
            url: '/' + moduleStateUrl + '_Sys_DataSegregateLabel',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/Sys_DataSegregateLabel-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabellistctrl.Tips_14'
            }
        };
        $stateProvider.state(state);
    }
}());
