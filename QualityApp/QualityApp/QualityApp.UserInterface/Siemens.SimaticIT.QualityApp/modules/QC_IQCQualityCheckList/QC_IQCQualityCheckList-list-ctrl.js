/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： IQC品质检验记录表
*  2. 创建人员： 丁零
*  3. 创建日期： 2021-08-27
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckList.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckList');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_QualityApp_QC_IQCQualityCheckList_QC_IQCQualityCheckList';
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

            initDictionary();

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增实验室
            self.editButtonHandler = editButtonHandler;//编辑
            self.searchButtonHandler = searchButtonHandler;
            self.selectButtonHandler = selectButtonHandler;

            self.addQualityButtonHandler = addQualityButtonHandler;//新增质量

            self.printButtonHandler = printButtonHandler;// 打印

            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

        }
        function initDictionary() {
            //实验室状态
            self.LaboratioryConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            //质检状态
            self.QualityConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            //判定状态
            self.DeterminationConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            commonService.getDataItemDuatil("LaboratoryStatus").then(function (res) {
                self.LaboratioryConfig.options = res.data.resultData;;
            });
            commonService.getDataItemDuatil("QualityStatus").then(function (res) {
                self.QualityConfig.options = res.data.resultData;;
            });
            commonService.getDataItemDuatil("ComprehensiveJudgement").then(function (res) {
                self.DeterminationConfig.options = res.data.resultData;;
            });
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_1')
                    });
                    initGridData();
                }
            });
        }

        $rootScope.$on("to-parent", function (event, editData) {
            initGridData();
        });

        $rootScope.$on("to-detail", function (event, callData) {
            let dept = callData.dept;
            let testResult = callData.testResult;
            if (dept == "1") {
                self.selectedItem.LabStatus = "3";
            }
            else if (dept == "2") {
                self.selectedItem.QualityStatus = "2";
                self.selectedItem.TestResult = testResult;
            }
            initGridDetailData();
        });



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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'ReceiptCode',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_4'),
                        width: 130
                    },
                    {
                        field: 'LineNum',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_5'),
                        width: 100
                    },
                    {
                        field: 'InWhsTime',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_37'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'ReceiptRemark',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_6'),
                        width: 160
                    },
                    {
                        field: 'InspectNo',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_7'),
                        width: 130
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_8'),
                        width: 130
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_9'),
                        width: 130
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_10'),
                        width: 130
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_11'),
                        width: 110
                    },
                    {
                        field: 'Abbr',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_12'),
                        width: 130
                    },
                    {
                        field: 'LabStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_13'),
                        width: 120,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.LabStatus==\'1\'"><span ng-cell-text>无需检验</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.LabStatus==\'2\'"><span ng-cell-text>待检验</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.LabStatus==\'3\'"><span ng-cell-text>检验完成</span></div>'
                    },
                    {
                        field: 'QualityStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_14'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.QualityStatus==\'1\'"><span ng-cell-text>待检验</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.QualityStatus==\'2\'"><span ng-cell-text>检验完成</span></div>'
                    },
                    {
                        field: 'TestResult',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_15'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TestResult==\'1\'"><span ng-cell-text>合格</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestResult==\'2\'"><span ng-cell-text>不合格</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestResult==\'3\'"><span ng-cell-text>让步接收</span></div>'
                    },
                    {
                        field: 'ArrivalQty',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_16'),
                        width: 140
                    },
                    // {
                    //     field: 'LabInspectorName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_17'),
                    //     width: 100
                    // },
                    {
                        field: 'LabInspectionTime',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_18'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'QualityTime',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_19'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_20'),
                        width: 200
                    },
                    {
                        field: 'Attachment',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_21'),
                        width: 80
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
                                self.gridOptionsItem.data = [];

                                initGridDetailData();
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                                self.gridOptionsItem.data = [];
                                //self.isQualityButtonVisible = false;

                            }
                        }
                    });
                },
                data: []
            }
            self.gridOptionsItem = {
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'TestDepartment',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_22'),
                        width: 200,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'1\'"><span ng-cell-text>实验室</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'2\'"><span ng-cell-text>质量部</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'3\'"><span ng-cell-text>生产部</span></div>'
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_23'),
                        width: 200
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_24'),
                        width: 200
                    },
                    {
                        field: 'DataTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_25'),
                        width: 140
                    },
                    {
                        field: 'BadNum',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_26'),
                        width: 200
                    },
                    {
                        field: 'ItemValue',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_27'),
                        width: 200
                    },

                    /*,
                        {
                            field: 'CreatorName', 
                            displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_28'),
                            width: 200
                        },
                        {
                            field: 'CreatTime', 
                            displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_29'),
                            width: 200,
                            type: 'date',
                            cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                        }*/
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
                                self.selectedItemDetail = row.entity;
                                //console.log (self.selectedItem);
                            } else {
                                self.selectedItemDetail = null;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //查询方法,数据绑定
        function initGridData() {
            // debugger
            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_30'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            if (self.LaboratioryConfig.selectedOption != null) {
                self.searchParams.LabStatus = self.LaboratioryConfig.selectedOption.ItemValue;
            }
            else {
                self.searchParams.LabStatus = "";
            }
            if (self.QualityConfig.selectedOption != null) {
                self.searchParams.QualityStatus = self.QualityConfig.selectedOption.ItemValue;
            }
            else {
                self.searchParams.QualityStatus = "";
            }
            if (self.DeterminationConfig.selectedOption != null) {
                self.searchParams.TestResult = self.DeterminationConfig.selectedOption.ItemValue;
            }
            else {
                self.searchParams.TestResult = "";
            }

            if (self.StartDate && self.EndDate) {
                self.searchParams.StartTime = commonService.ConvertToLocalTime(self.StartDate);
                self.searchParams.EndTime = commonService.ConvertToLocalTime(self.EndDate);
            } else {
                self.searchParams.StartTime = "";
                self.searchParams.EndTime = "";
            }


            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//收料通知单行号
                sord: 'desc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("quality") + 'QC_IQCQualityCheck/QC_IQCQualityCheckListPageDataTableList';

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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_31'));
            });
        }

        //查询方法,数据绑定
        function initGridDetailData() {
            //self.selectedItem = null;

            let Pagination = {
                rows: self.gridOptionsItem.paginationPageSize,
                page: self.gridOptionsItem.paginationCurrentPage,
                sidx: 'TestDepartment',//单据类型
                sord: 'asc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: {
                    IQCId: self.selectedItem.Id
                }
            };

            var url = commonService.getMesApiAddress("quality") + 'QC_IQCQualityCheckItem/QC_TestDetailPageDataTableList';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {

                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsItem.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsItem.data = res.data.resultData.rows;

                } else {
                    self.gridOptionsItem.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_31'));
            });
        }

        //查询
        function searchButtonHandler() {
            self.gridOptionsItem.data = [];
            initGridData();
            //initGridDetailData();
        }

        //新增实验室
        function addButtonHandler(clickedCommand) {
            if (!self.selectedItem.SmallClassName) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_32'));
                return;
            }
            self.selectedItem.TitleName = commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_33');
            self.selectedItem.TestDepartment = "1";

            $state.go(rootstate + '.add', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }
        //新增质量
        function addQualityButtonHandler(clickedCommand) {
            if (!self.selectedItem.SmallClassName) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_32'));
                return;
            }
            self.selectedItem.TitleName = commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_34');
            self.selectedItem.TestDepartment = "2";
            $state.go(rootstate + '.addresult', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //编辑
        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //打印
        function printButtonHandler(clickedCommand) {
            alert(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_35'))
            // $state.go(rootstate + '.editresult', { id: self.selectedItem.Id, selectedItem: self.selectedItem, selectedItemDetail: self.selectedItemDetail });
        }

        function selectButtonHandler() {
            $state.go(rootstate + '.editresult', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
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
        var moduleStateName = 'home.Siemens_SimaticIT_QualityApp_QC_IQCQualityCheckList';
        var moduleStateUrl = 'Siemens.SimaticIT_QualityApp_QC_IQCQualityCheckList';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_IQCQualityCheckList';

        var state = {
            name: moduleStateName + '_QC_IQCQualityCheckList',
            url: '/' + moduleStateUrl + '_QC_IQCQualityCheckList',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/QC_IQCQualityCheckList-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListlistctrl.Tips_36'
            }
        };
        $stateProvider.state(state);
    }
}());
