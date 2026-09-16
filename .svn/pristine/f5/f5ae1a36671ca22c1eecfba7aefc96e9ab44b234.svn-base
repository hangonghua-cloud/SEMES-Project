(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.FactoryModelApp.ModelLevel').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevel.service', '$state', '$stateParams', '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'commonService', 'common.widgets.busyIndicator.service','$timeout'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, commonService, busyIndicatorService, $timeout) {
        var self = this;
        var logger, rootstate, messageservice, backendService;


        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevel');

            init();
            initGridOptions();
            initGridData();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_FactoryModelApp_ModelLevel_ModelLevel';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;
            
            //Initialize Model Data
            self.selectedItem = null;
            //左-新增
            self.is_addButtonHandler = true;
            //右-编辑
            self.is_editButtonHandler = false;
            //左-删除
            self.is_deleteButtonHandler = false;

            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            if (!!$stateParams.itemId) {
                //self.selectedDataItemId = $stateParams.itemId;
                //getDataItemDetailList(self.selectedDataItemId);
                initGridData();
            }
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
             //分页信息
            //$scope.currentPage = 1;
            //$scope.pageSize = paginationOptions.pageSize;
            //查询参数
            self.queryParameters = {
                LevelCode: null,
                LevelName: null
            }

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;
            //参数查询
            self.searchButtonHandler = searchButtonHandler;
        }

        //分页信息
        //$scope.currentPage = 1;
        //$scope.pageSize = paginationOptions.pageSize;

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
                data: [],
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_1'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    }, {
                        field: 'LevelCode',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_2'),
                        width: 200
                    }, {
                        field: 'LevelName',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_3'),
                        width: 200
                    }, {
                        field: 'Level',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_4'),
                        width: 200
                    }, {
                        field: 'Describe',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_5'),
                        width: 500
                    }, {
                        field: 'CreateUser',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_6'),
                        visible: false,
                        width: 200
                    }, {
                        field: 'CreateDate',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_7'),
                        visible: false,
                        width: 200,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"'
                    }, {
                        field: 'ModifyUser',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_8'),
                        visible: false,
                        width: 200
                    }, {
                        field: 'ModifyDate',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_9'),
                        visible: false,
                        width: 200,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"'
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData();
                    });
                    //行选中事件
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row.isSelected) {
                            self.selectedItem = row.entity;
                            // console.log(row.entity);
                            //左-新增
                            self.is_addButtonHandler = true;
                            //左-删除
                            self.is_deleteButtonHandler = true;
                            //右-编辑
                            self.is_editButtonHandler = true;
                        }
                        else{
                            self.selectedItem = null;
                            // console.log(row.entity);
                            //左-新增
                            self.is_addButtonHandler = false;
                            //左-删除
                            self.is_deleteButtonHandler = false;
                            //右-编辑
                            self.is_editButtonHandler = false;
                        }
                    });
                }
            }
        }

        function initGridData() {
            //分页设置
            var paginationOptions = {
                pageNumber: self.gridOptions.paginationCurrentPage,//1,
                pageSize: self.gridOptions.paginationPageSize,//20,
                sort: {
                    columnName: 'Level',
                    direction: 'asc',
                }
            };
            dataService.getListWithPaging(paginationOptions.pageSize, paginationOptions.pageNumber, paginationOptions.sort.columnName, paginationOptions.sort.direction, self.queryParameters).then(function (res) {
             
                if ((res) && (res.data.success)) {
                    /*self.gridOptions.totalItems = res.data.resultData.records;
                    $scope.totalPage = res.data.resultData.total;//如果后端不返回总页数，就改用方法：Math.ceil(self.gridOptions.totalItems / $scope.pageSize);
                    self.gridOptions.data = res.data.resultData.rows;*/
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据            
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                    backendService.genericError(res.data.returnMsg, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_10'));
                }
                //self.gridOptions.data = [];
                setButtonsVisibility(false);//setButtonsVisibilityItem
                busyIndicatorService.hide();
            }, function (error) {
                //$window.alert(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_11') + error.data.returnMsg);
                backendService.genericError('[' + error.status + '] - ' + commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_11') + error.statusText, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_10'));
                busyIndicatorService.hide();
            });
            /*dataService.getAll().then(function (data) {
                if ((data) && (data.succeeded)) {
                    self.viewerData = data.value;
                } else {
                    self.viewerData = [];
                }
            }, backendService.backendError);*/
        }

        function searchButtonHandler(clickedCommand) {
            initGridData();
        }

        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            if(self.selectedItem != null){
                $state.go(rootstate + '.edit', { id: self.selectedItem.LevelCode, selectedItem: self.selectedItem });
            }
            else{
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_12'));
            }
        }

        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.LevelCode, selectedItem: self.selectedItem });
        }

        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_13');
            var text = commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_14') + self.selectedItem.LevelCode + commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_15');
            var url = "";//commonService.getMesApiAddress() + '/LevelManage/BsModelLevel/DeleteForm?code=' + self.selectedItem.LevelCode;
            if(self.selectedItem != null){
                backendService.confirm(text, function () {
                    url = commonService.getMesApiAddress("factory") + '/LevelManage/BsModelLevel/DeleteForm?code=' + self.selectedItem.LevelCode;
                    commonService.callWebApiGet(url, null).then(function (res) {
                        if ((res) && (res.data.success)) {
                            var resultData = res.data.resultData;
                            initGridData();
                            //self.BOMgridOptions.data = resultData;
                            // self.EquipTypeConfig.options.splice(0, 0, {
                            //     ItemValue: "",
                            //     ItemName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_16')
                            // })
                            //左-新增
                            self.is_addButtonHandler = true;
                            //左-删除
                            self.is_deleteButtonHandler = false;
                            //右-编辑
                            self.is_editButtonHandler = false;
                        } else {
                            self.BOMgridOptions.data = [];
                        }
                    }, function (error) {
                        backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_10'));
                    });
                }, title);
            }
            else{
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_12'));
            }
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
        var moduleStateName = 'home.Siemens_SimaticIT_FactoryModelApp_ModelLevel';
        var moduleStateUrl = 'Siemens.SimaticIT_FactoryModelApp_ModelLevel';
        var moduleFolder = 'Siemens.SimaticIT.FactoryModelApp/modules/ModelLevel';

        var state = {
            name: moduleStateName + '_ModelLevel',
            url: '/' + moduleStateUrl + '_ModelLevel',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ModelLevel-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevellistctrl.Tips_17'
            }
        };
        $stateProvider.state(state);
    }
}());
