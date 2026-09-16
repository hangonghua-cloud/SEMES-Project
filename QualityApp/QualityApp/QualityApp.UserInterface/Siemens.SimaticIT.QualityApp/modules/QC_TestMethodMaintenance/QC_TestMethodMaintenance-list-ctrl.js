/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 原材料基础检验配置信息
*  2. 创建人员： 丁零
*  3. 创建日期： 2021-08-31
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenance.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', 'i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, i18nService) {
        //国际化 
        i18nService.setCurrentLang('zh-cn');
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenance');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_QualityApp_QC_TestMethodMaintenance_QC_TestMethodMaintenance';
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
            self.isShowDetailVisible = false;
            self.isDetailButtonVisible = false;
            self.viewerOptions2 = {};
            self.viewerData2 = [];
            self.searchParams2 = {};
            //工序
            self.selectedItemProcess = null;
            self.isShowProcessVisible = false;
            self.isProcessButtonVisible = false;
            self.viewerOptions3 = {};
            self.viewerData3 = [];
            self.searchParams3 = {};

            initDictionary();

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
            //关联物料小类
            self.add3ButtonHandler = add3ButtonHandler;//新增
            //self.edit3ButtonHandler = edit3ButtonHandler;//编辑
            //self.delete3ButtonHandler = delete3ButtonHandler;//删除

            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            self.tabClick1 = tabClick1;
            self.tabClick2 = tabClick2;

        }

        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_1'), ResourceCode: "" }]
            };

            self.TypeConfig = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_1'), ItemValue: "" }]
            };

            self.StatusConfig = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_1'), ItemValue: "" },
                { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_2'), ItemValue: "1" },
                { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_3'), ItemValue: "0" }]
            };

            // commonService.getDataItemDuatil("EffectiveState").then(function (res) {
            //     if (res && res.data.success) {
            //         self.StatusConfig.options = res.data.resultData;
            //         self.StatusConfig.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
            //     }
            // })
            commonService.getDataItemDuatil("RawMaterialInspection").then(function (res) {
                if (res && res.data.success) {
                    self.TypeConfig.options = res.data.resultData;
                    self.TypeConfig.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_1')
                    });
                    initGridData();
                }
            });

        }

        $rootScope.$on('to-parent', function (event, editData) {
            initGridData();
            initGridDataDetail();
            initGridMaterialData();
        });

        $rootScope.$on('to-DetailItem', function (event, editData) {
            initGridDataDetail();
        });

        $rootScope.$on('to-material', function (event, editData) {
            initGridMaterialData();
        });

        function tabClick1() {
            console.log(commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_4'))
            self.isShowGird = true;
            initGridDataDetail();
        }

        function tabClick2() {
            console.log(commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_5'))
            self.isShowGird = false;
            initGridMaterialData();
        }


        //初始化grid选项
        function initGridOptions() {
            //主表
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_6'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    }, {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'TestTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'TestMethodCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_9'),
                        width: 160
                    },
                    {
                        field: 'TestMethodName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_10'),
                        width: 200
                    },
                    {
                        field: 'TestMethodDescription',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_11'),
                        width: 200
                    },
                    {
                        field: 'IsEnabled',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_12'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.IsEnabled==true"><span ng-cell-text class="green">有效</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.IsEnabled!=true"><span ng-cell-text class="red">无效</span></div>'
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_13'),
                        width: 110
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_14'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
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
                                //console.log (self.selectedItem);
                                if (self.isShowGird) initGridDataDetail();
                                else initGridMaterialData();

                            } else {
                                self.gridItemOptions.data = [];
                                self.gridMaterialOptions.data = []
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

            //检测项目
            self.gridItemOptions = {
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_6'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'TestItemCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_15'),
                        width: 200
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_16'),
                        width: 200
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_17'),
                        width: 200
                    },
                    {
                        field: 'TestDepartment',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_18'),
                        width: 200,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'1\'"><span ng-cell-text>实验室</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'2\'"><span ng-cell-text>质量部</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'3\'"><span ng-cell-text>生产部</span></div>'
                    },
                    {
                        field: 'DataTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_19'),
                        width: 200
                    },
                    {
                        field: 'IsEnabled',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_12'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.IsEnabled==true"><span ng-cell-text class="green">有效</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.IsEnabled!=true"><span ng-cell-text class="red">无效</span></div>'
                    },
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.grid2Api = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataDetail();
                    });
                    //行选中事件
                    $scope.grid2Api.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailButtonVisible = true;
                                //console.log (self.selectedItem);
                            } else {
                                self.selectedItemDetail = null;
                                self.isDetailButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
            //关联物料小类
            self.gridMaterialOptions = {
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_6'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'SmallClass',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_20'),
                        width: 200
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_21'),
                        width: 200
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_22'),
                        width: 200,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'
                    },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_23'),
                        width: 200
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.grid3Api = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridMaterialData();
                    });
                    //行选中事件
                    $scope.grid3Api.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedMaterialItem = row.entity;
                                //console.log (self.selectedItem);
                                self.isShowProcessVisible = true;
                            } else {
                                self.selectedMaterialItem = null;
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
                sidx: 'CreateTime',//检验类型
                sord: 'desc'
            };
            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_24'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.IsEnabled = self.StatusConfig.value.ItemValue;
            self.searchParams.TestType = self.TypeConfig.value.ItemValue;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("quality") + 'QC_TestMethodMaintenance/QC_TestMethodMaintenancePageDataTableList';

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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_25'));
            });
        }

        //检测项目
        function initGridDataDetail() {

            let Pagination = {
                rows: self.gridItemOptions.paginationPageSize,
                page: self.gridItemOptions.paginationCurrentPage,
                sidx: 'TestItemCoading',//工厂
                sord: 'asc'
            };

            self.isShowProcessVisible = false;
            self.isProcessButtonVisible = false;
            self.isDetailButtonVisible = false;

            if (self.selectedItem == null) {
                self.gridItemOptions.data = [];
                return;
            } else {
                self.isShowDetailVisible = true;

            }


            let queryParmeters = {
                pagination: Pagination,
                queryJson: { "TestMethodId": self.selectedItem.Id }
            };

            var url = commonService.getMesApiAddress("quality") + 'QC_TestMethodItemMaintenance/QC_TestMethodItemMaintenancePageDataTableList';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {

                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridItemOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridItemOptions.data = res.data.resultData.rows;
                } else {
                    self.gridItemOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_25'));
            });
        }
        //关联物料小类
        function initGridMaterialData() {
            //self.selectedItem = null;
            let Pagination = {
                rows: self.gridMaterialOptions.paginationPageSize,
                page: self.gridMaterialOptions.paginationCurrentPage,
                sidx: 'SmallClass',//设备编码
                sord: 'asc'
            };
            self.isShowDetailVisible = false;

            self.isShowDetailVisible = false;
            self.isProcessButtonVisible = false;

            if (self.selectedItem == null) {
                self.gridMaterialOptions.data = [];
                return;
            } else {
                self.isShowProcessVisible = true;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: {
                    "TestMethodId": self.selectedItem.Id
                }
            };

            var url = commonService.getMesApiAddress('quality') + 'QC_TestMethodMaterial/QC_TestMethodMaterialPageDataTableList';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {

                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridMaterialOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridMaterialOptions.data = res.data.resultData.rows;
                } else {
                    self.gridMaterialOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_25'));
            });
        }


        function searchButtonHandler() {
            initGridData();
            initGridDataDetail();
            initGridMaterialData();
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
            var title = commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_26');
            var text = commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_27');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("quality") + 'QC_TestMethodMaintenance/RemoveQC_TestMethodMaintenance';

                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];
                self.UserCode = user.loginName;
                self.UserName = user.fullName;
                self.selectedItem.ModifyBy = self.UserCode;
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

                        self.gridItemOptions.data = [];
                        self.isButtonVisibleNew = false;
                        self.isButtonVisibleEdit = false;

                        self.gridMaterialOptions.data = [];
                        self.isButtonVisibleMaterial = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);

                    }
                }, function (error) {

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_25'));
                });
            }, title);
        }

        function add2ButtonHandler(clickedCommand) {
            //
            $state.go(rootstate + '.adddetail', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function edit2ButtonHandler(clickedCommand) {
            //
            $state.go(rootstate + '.editdetail', { id: self.selectedItemDetail.Id, selectedItem: self.selectedItemDetail });
        }

        function delete2ButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_26');
            var text = commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_27');
            backendService.confirm(text, function () {

                var url = commonService.getMesApiAddress("quality") + 'QC_TestMethodItemMaintenance/DeleteQC_TestMethodItemMaintenance';

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
                        self.isButtonVisibleDetail = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);

                    }
                }, function (error) {

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_25'));
                });
            }, title);
        }

        function add3ButtonHandler(clickedCommand) {
            //
            $state.go(rootstate + '.addmaterial', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
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
        var moduleStateName = 'home.Siemens_SimaticIT_QualityApp_QC_TestMethodMaintenance';
        var moduleStateUrl = 'Siemens.SimaticIT_QualityApp_QC_TestMethodMaintenance';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_TestMethodMaintenance';

        var state = {
            name: moduleStateName + '_QC_TestMethodMaintenance',
            url: '/' + moduleStateUrl + '_QC_TestMethodMaintenance',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/QC_TestMethodMaintenance-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenancelistctrl.Tips_28'
            }
        };
        $stateProvider.state(state);
    }
}());
