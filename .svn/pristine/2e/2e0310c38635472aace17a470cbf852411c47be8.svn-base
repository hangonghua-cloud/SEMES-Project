(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.OQCManage').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfig.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfig');

            init();
            //初始化grid选项
            initGridOptions();
            //初始化子表grid选项
            initGridOptionsDetail();
            initGridOptionsProcess();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_QualityApp_OQCManage_OQCCheckConfig';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.isShowGird = true;


            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};
            //子表明细
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            self.viewerOptions2 = {};
            self.viewerData2 = [];
            self.searchParams2 = {};

            self.searchParams3 = {};
            self.selectedItemProcess = null;
            self.isShowProcessVisible = false;
            self.isProcessButtonVisible = false;

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            //子明细
            self.add2ButtonHandler = add2ButtonHandler;//新增
            self.edit2ButtonHandler = edit2ButtonHandler;//编辑
            self.delete2ButtonHandler = delete2ButtonHandler;//删除

            self.add3ButtonHandler = add3ButtonHandler;


            initDictionary();
            self.tabClick1 = tabDetail;
            self.tabClick2 = tabProcess;

        }

        //切换Tab
        function tabDetail() {
            console.log(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_1'))
            self.isShowGird = true;
            if (self.selectedItem) {
                initGridDataDetail();
            }
        }
        function tabProcess() {
            console.log(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_2'))
            self.isShowGird = false;
            initGridDataMaterialGroup();
        }

        function initDictionary() {
            self.typeEnabled = {
                value: { ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_3') },
                options: [
                    { ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_4') },
                    { ItemValue: "1", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_5') },
                    { ItemValue: "0", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_6') }
                ]
            };
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_4'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_4'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_4')
                    });
                    initGridData();
                }
            });
        }

        //回调函数
        $rootScope.$on("to-parentMaterialGroup", function (event, data) {
            initGridDataMaterialGroup();
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
                paginationPageSizes: [20, 30, 50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 20, //每页显示个数
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_7'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'TestMethodCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_9'),
                        width: 140
                    },
                    {
                        field: 'TestMethodName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_10'),
                        width: 140
                    },
                    {
                        field: 'TestMethodDescription',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_11'),
                        width: 200
                    },
                    // {
                    //     field: 'SmallClassName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_12'),
                    //     width: 140
                    // },
                    {
                        field: 'IsEnabled',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_13'),
                        width: 240,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.IsEnabled==true"><span ng-cell-text class="green">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.IsEnabled!=true"><span ng-cell-text class="red">否</span></div>'
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_14'),
                        width: 200
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_15'),
                        width: 200,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
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
                                if (self.isShowGird) {
                                    initGridDataDetail();
                                } else {
                                    initGridDataMaterialGroup();
                                }
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                                self.isShowDetailVisible = false;
                                self.isShowProcessVisible = false;
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
                sidx: 'CreateTime',//工厂
                sord: 'desc'
            };

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_16'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.IsEnabled = self.typeEnabled.value.ItemValue;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("quality") + 'QC_OQCCheckConfig/QC_OQCCheckConfigPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_17'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
            initGridDataDetail();
            initGridDataMaterialGroup();
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
            var title = commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_18');
            var text = commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_19');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress("quality") = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("quality") + 'QC_OQCCheckConfig/RemoveQC_OQCCheckConfig';

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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_17'));
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
                paginationPageSizes: [20, 30, 50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 20, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮

                //选中  
                rowTemplate: "<div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_7'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },

                    {
                        field: 'TestItemCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_20'),
                        width: 140
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_21'),
                        width: 200
                    },

                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_22'),
                        width: 200
                    },

                    {
                        field: 'DataTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_23'),
                        width: 140
                    },
                    {
                        field: 'TestDepartment',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_24'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'1\'"><span ng-cell-text>实验室</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'2\'"><span ng-cell-text>质量部</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'3\'"><span ng-cell-text>生产部</span></div>'
                    },
                    {
                        field: 'IsEnabled',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_13'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.IsEnabled==true"><span ng-cell-text class="green">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.IsEnabled!=true"><span ng-cell-text class="red">否</span></div>'

                    },

                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataDetail();
                    });
                    //行选中事件
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailButtonVisible = true;


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
            self.isShowDetailVisible = true;
            self.isShowProcessVisible = false;
            self.isProcessButtonVisible = false;
            self.isDetailButtonVisible = false;

            let Pagination = {
                rows: self.gridOptionsDetail.paginationPageSize,
                page: self.gridOptionsDetail.paginationCurrentPage,
                sidx: 'CreateTime',//OQCId
                sord: 'asc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.OQCCheckConfigId = self.selectedItem.Id;
            }
            else {
                self.gridOptionsDetail.data = [];
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };

            var url = commonService.getMesApiAddress("quality") + 'QC_OQCCheckConfigItem/QC_OQCCheckConfigItemPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_17'));
            });
        }

        //查询
        function search2ButtonHandler() {
            initGridDataDetail();
        }

        //新增
        function add2ButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addDetail', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //编辑
        function edit2ButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.editDetail', { id: self.selectedItemDetail.ID, selectedItem: self.selectedItemDetail });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.selectDetail', { id: self.selectedItemDetail.ID, selectedItem: self.selectedItemDetail });
        }

        //删除 事件
        function delete2ButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_18');
            var text = commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_19');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("quality") + 'QC_OQCCheckConfigItem/DeleteQC_OQCCheckConfigItem';
                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];
                self.UserCode = user.loginName;
                self.UserName = user.fullName;
                self.selectedItemDetail.UpdateByCode = self.UserCode;
                self.selectedItemDetail.UpdateByName = self.UserCode + '-' + self.UserName;
                if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                    self.selectedItemDetail.UpdateByName = self.UserCode;
                }
                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItemDetail
                };

                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridDataDetail();
                        self.selectedItemDetail = null;
                        self.isDetailButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_17'));
                });
            }, title);
        }


        //初始化关联工序grid
        function initGridOptionsProcess() {
            self.gridOptionsProcess = {
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_7'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'SmallClass',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_25'),
                        width: 400
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_26'),
                        width: 400
                    },

                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApiProcess = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataMaterialGroup();
                    });
                    //行选中事件
                    $scope.gridApiProcess.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemProcess = row.entity;
                                self.isProcessButtonVisible = true;
                                //console.log (self.selectedItemDetail);
                            } else {
                                self.selectedItemDetail = null;
                                self.isProcessButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //关联物料组,数据绑定
        function initGridDataMaterialGroup() {
            self.selectedItemProcess = null;

            self.isShowDetailVisible = false;
            self.isShowProcessVisible = true;
            self.isProcessButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsProcess.paginationPageSize,
                page: self.gridOptionsProcess.paginationCurrentPage,
                sidx: 'SmallClass',//检测项目编码
                sord: 'asc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams3.OQCCheckConfigId = self.selectedItem.Id;
            }
            else {
                self.gridOptionsProcess.data = [];
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams3
            };
            var url = commonService.getMesApiAddress("quality") + 'QC_OQCCheckConfigMaterial/QC_OQCCheckConfigMaterialPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsProcess.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsProcess.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsProcess.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_17'));
            });
        }

        function add3ButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addMaterial', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
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
        var moduleStateName = 'home.Siemens_SimaticIT_QualityApp_OQCManage';
        var moduleStateUrl = 'Siemens.SimaticIT_QualityApp_OQCManage';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/OQCManage';

        var state = {
            name: moduleStateName + '_OQCCheckConfig',
            url: '/' + moduleStateUrl + '_OQCCheckConfig',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/OQCCheckConfig-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfiglistctrl.Tips_27'
            }
        };
        $stateProvider.state(state);
    }
}());
