(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.FormulaManage').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.FormulaManage.FormulaRecord.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.FormulaManage.FormulaRecord');

            init();
            //初始化grid选项
            initGridOptions();
            //初始化子表grid选项
            initGridOptionsDetail();
            setTimeout(function () {
                //初始化grid数据、查询
                initGridData();
            }, 1000);//如果查询条件有下拉参数，请调整此值到1000
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_FormulaManage_FormulaRecord';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};
            //子表明细
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            self.viewerOptions2 = {};
            self.viewerData2 = [];
            self.searchParams2 = {};

            initdictionary();

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询

            self.typeFormulaChange = typeFormulaChange;

        }
        function initdictionary() {

            self.typeFormula = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_1'), ResourceCode: "" },
                options: [
                    { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_1'), ResourceCode: "" },
                    { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_2'), ResourceCode: "FHWL" },
                    { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_3'), ResourceCode: "FHXL" }
                ]
            };

            self.typeMachine = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_1'), ResourceCode: "" }]
            };

        }
        function typeFormulaChange(oldval, newval) {
            debugger
            commonService.getResourceListByParentResource({ ParentResource: newval.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeMachine.options = res.data.resultData;
                    self.typeMachine.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_1')
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_4'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_5'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.FactoryCode==\'3001\'"><span ng-cell-text>富华工厂</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.FactoryCode==\'3002\'"><span ng-cell-text>华丽二厂</span></div>'
                    },
                    {
                        field: 'FormulaTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'FormulaCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_9'),
                        width: 110
                    },
                    {
                        field: 'FormulaName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'FormulaTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_11'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'MachineName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_12'),
                        width: 110
                    },
                    {
                        field: 'FormulaNum',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_13'),
                        width: 110
                    },

                    {
                        field: 'UserName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_14'),
                        width: 110
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_15'),
                        width: 180,
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
                                //子表明细关联
                                initGridDataDetail();
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
                sidx: 'CreateTime',//报工时间
                sord: 'desc'
            };

            self.searchParams.FormulaMachine = self.typeMachine.value.ResourceCode;
            self.searchParams.FormulaType = self.typeFormula.value.ResourceCode;

            if (self.StartTime && self.EndTime) {
                self.searchParams.StartTime = commonService.ConvertToLocalTime(self.StartTime);
                self.searchParams.EndTime = commonService.ConvertToLocalTime(self.EndTime);
            } else {
                self.searchParams.StartTime = "";
                self.searchParams.EndTime = "";
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_FormulaRecord/PM_FormulaRecordPageDataTableList';

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
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_16'), commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_16'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
            initGridDataDetail();
        }

        //新增
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        //编辑
        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_17');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_18');
            backendService.confirm(text, function () {

                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_FormulaRecord/RemovePM_FormulaRecord';
                console.log("删除----------------" + url);
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

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_16'));
                });
            }, title);
        }

        //初始化子表grid选项
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
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_4'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_20'),
                        width: 200
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_21'),
                        width: 200
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_22'),
                        width: 200
                    },
                    {
                        field: 'ActQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_23'),
                        width: 200
                    },
                    {
                        field: 'TheoryQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_24'),
                        width: 200
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_25'),
                        width: 200
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApiDetail = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataDetail();
                    });
                    //行选中事件
                    $scope.gridApiDetail.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailButtonVisible = true;
                                //console.log (self.selectedItemDetail);
                                //子表明细关联
                                //initGridDataDetail();
                            } else {
                                self.selectedItemDetail = null;
                                self.isDetailButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //子表查询方法,数据绑定
        function initGridDataDetail() {
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsDetail.paginationPageSize,
                page: self.gridOptionsDetail.paginationCurrentPage,
                sidx: 'MaterialCode',//物料编码
                sord: 'asc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.FormulaId = self.selectedItem.Id;
            }
            else {
                self.gridOptionsDetail.data = [];
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_FormulaRecordDetail/PM_FormulaRecordDetailPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {

                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsDetail.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsDetail.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsDetail.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_16'));
            });
        }

        //查询
        function search2ButtonHandler() {
            initGridDataDetail();
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_FormulaManage';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_FormulaManage';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/FormulaManage';

        var state = {
            name: moduleStateName + '_FormulaRecord',
            url: '/' + moduleStateUrl + '_FormulaRecord',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/FormulaRecord-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.FormulaRecord.JS.Tips_26'
            }
        };
        $stateProvider.state(state);
    }
}());
