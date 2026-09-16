/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 巡检检验记录表
*  2. 创建人员： 丁零
*  3. 创建日期： 2021-08-23
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.QC_PollingDetail').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetail.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', '$timeout','i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, $timeout, i18nService) {
        var self = this;
        var logger, rootstate, messageservice, backendService;
        i18nService.setCurrentLang('zh-cn');

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetail');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_QualityApp_QC_PollingDetail_QC_PollingDetail';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.isButtonVisibleNew = false;
            self.isButtonVisibleEdit = false;
            self.isQualityButtonVisible = false;
            self.isQualityButtonVisible2 = false;
            self.checkRow = null;

            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.labButtonHandler = labButtonHandler;//实验室
            self.qualityButtonHandler = qualityButtonHandler;//质量
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询



            initDictionary();

            self.typeFactoryChange = typeFactoryChange;
            self.ProcessChange = ProcessChange;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

        }
        function initDictionary() {

            // //单据类型
            // self.ListTypeConfig = {
            //     value: null,
            //     selectedOption: null,
            //     options: []
            // };

            //实验室状态
            self.LaboratoryConfig = {
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
            //工序
            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_1'), ResourceCode: "" }]
            };


            self.TestMachineConfig = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_1'), ResourceCode: "" }]
            };
            // commonService.getResourceExtendInfo({ LevelCode: "Process" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.ProcessConfig.options = res.data.resultData;
            //         self.ProcessConfig.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_1')
            //         });
            //     }
            // });
            // commonService.getDataItemDuatil("BillsTypes").then(function (res) {
            //     self.ListTypeConfig.options = res.data.resultData;;
            // });
            commonService.getDataItemDuatil("LaboratoryStatus").then(function (res) {
                self.LaboratoryConfig.options = res.data.resultData;;
            });
            commonService.getDataItemDuatil("ComprehensiveJudgement").then(function (res) {
                self.DeterminationConfig.options = res.data.resultData;;
            });
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_1')
                    });
                    initGridData();
                }
            });
        }
        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.Process.options = res.data.resultData;
                        self.Process.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_1')
                        });
                    }
                });
            } else {
                self.Process = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_1'), ResourceCode: "" }]
                };
            }
        }

        function ProcessChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.TestMachineConfig.options = res.data.resultData;
                    self.TestMachineConfig.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_1')
                    });
                }
            });
        }



        $rootScope.$on("to-editItem", function (event, editData) {
            self.checkRow = editData;
            initGridData();
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_5'),
                        width: 100
                    },
                    {
                        field: 'FlowCardId',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_6'),
                        width: 250
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_7'),
                        width: 140
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_8'),
                        width: 140
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_9'),
                        width: 300
                    },
                    // {
                    //     field: 'SmallClass',
                    //     displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_10'),
                    //     width: 110
                    // },
                    {
                        field: 'TestProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_11'),
                        width: 110
                    },
                    // {
                    //     field: 'ProductionMachineName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_12'),
                    //     width: 110
                    // },
                    {
                        field: 'LaboratoryTestStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_13'),
                        width: 110
                    },
                    {
                        field: 'DeterminationName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_14'),
                        width: 100
                    },
                    {
                        field: 'Inspector',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_15'),
                        width: 120,

                    },
                    {
                        field: 'InspectorName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_16'),
                        width: 120
                    },
                    {
                        field: 'DeptName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_17'),
                        width: 200
                    },
                    {
                        field: 'InspectionTime',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_18'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_19'),
                        width: 140
                    },
                    {
                        field: 'Attachment',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_20'),
                        width: 120
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
                                if (self.selectedItem.ListType == "1") self.isQualityButtonVisible2 = true;
                                else self.isQualityButtonVisible2 = false;
                                self.gridOptionsItem.data = [];
                                self.isButtonVisibleNew = false;
                                self.isButtonVisibleEdit = false;

                                initGridDetailData();
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                                self.gridOptionsItem.data = [];
                                self.isQualityButtonVisible = false;
                                self.isButtonVisibleNew = false;
                                self.isButtonVisibleEdit = false;
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'TestDepartment',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_21'),
                        width: 200,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'1\'"><span ng-cell-text>实验室</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'2\'"><span ng-cell-text>质量部</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'3\'"><span ng-cell-text>生产部</span></div>'
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_22'),
                        width: 200
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_23'),
                        width: 200
                    },
                    {
                        field: 'TestItemResult',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_24'),
                        width: 200
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_25'),
                        width: 200
                    },
                    {
                        field: 'CreatTime',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_18'),
                        width: 200,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApi1 = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData();
                    });
                    //行选中事件
                    $scope.gridApi1.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                //console.log (self.selectedItem);
                            } else {
                                self.selectedItemDetail = null;
                                self.isButtonVisibleNew = false;
                                self.isButtonVisibleEdit = false;
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
            self.isButtonVisibleEdit = false;
            self.isButtonVisibleNew = false;

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_26'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.TestProcess = self.Process.value.ResourceCode;

            if (self.TestMachineConfig.value != null) {
                self.searchParams.ProductionMachine = self.TestMachineConfig.value.ResourceCode;
            }
            else {
                self.searchParams.ProductionMachine = "";
            }
            // if (self.ListTypeConfig.selectedOption != null) {
            //     self.searchParams.ListType = self.ListTypeConfig.selectedOption.ItemValue;
            // }
            // else {
            //     self.searchParams.ListType = "";
            // }
            if (self.LaboratoryConfig.selectedOption != null) {
                self.searchParams.LaboratoryTestStatus = self.LaboratoryConfig.selectedOption.ItemValue;
            }
            else {
                self.searchParams.LaboratoryTestStatus = "";
            }
            if (self.DeterminationConfig.selectedOption != null) {
                self.searchParams.Determination = self.DeterminationConfig.selectedOption.ItemValue;
            }
            else {
                self.searchParams.Determination = "";
            }
            if (self.StartDate != "") {
                self.searchParams.StartTime = moment(self.StartDate).format("YYYY-MM-DD");
            }
            else {
                self.searchParams.StartTime = "";
            }
            if (self.EndDate != "") {
                self.searchParams.EndTime = moment(self.EndDate).format("YYYY-MM-DD");
            }
            else {
                self.searchParams.EndTime = "";
            }
            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//
                sord: 'desc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("quality") + 'QC_PollingDetail/QC_PollingDetailPageDataTableList';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {

                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptions.data = res.data.resultData.rows;

                    if (!!self.checkRow) {
                        $timeout(function () {
                            var rowEntity = self.gridOptions.data.find(t => t.Id == self.checkRow.Id);
                            $scope.gridApi.selection.selectRow(rowEntity);
                            self.checkRow = null;
                        }, 300);

                    }
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_27'));
            });
        }

        //查询方法,数据绑定
        function initGridDetailData() {
            //self.selectedItem = null;
            //self.isButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsItem.paginationPageSize,
                page: self.gridOptionsItem.paginationCurrentPage,
                sidx: 'TestDepartment',//单据类型
                sord: 'desc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: {
                    PollingDetailId: self.selectedItem.Id,
                }
            };
            var url = commonService.getMesApiAddress("quality") + 'QC_PollingDetailResult/QC_PollingDetailResultPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsItem.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsItem.data = res.data.resultData.rows;
                    if (self.gridOptionsItem.data.filter(item => item.Id != null).length == 0) {
                        self.isButtonVisibleNew = true;
                        self.isButtonVisibleEdit = false;
                    }
                    else {
                        self.isButtonVisibleNew = false;
                        self.isButtonVisibleEdit = true;
                    }
                    if (self.gridOptionsItem.data.filter(item => item.TestItemResult == null).length == 0 && self.isQualityButtonVisible2 == false) {
                        self.isQualityButtonVisible = true;
                    }
                    else {
                        self.isQualityButtonVisible = false;
                    }
                } else {
                    self.gridOptionsItem.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_27'));
            });
        }

        //查询
        function searchButtonHandler() {
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.gridOptionsItem.data = [];
            self.isQualityButtonVisible = false;
            self.isButtonVisibleNew = false;
            self.isButtonVisibleEdit = false;
            initGridData();
        }

        function addButtonHandler() {
            $state.go(rootstate + '.add');
        }

        //实验室录入
        function labButtonHandler(clickedCommand) {

            if (self.selectedItem.LaboratoryTestStatus == "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_28'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_27'));
                return;
            }

            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //质量盘点
        function qualityButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            if (self.selectedItem.LaboratoryTestStatus == "2") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_29'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_27'));
                return;
            }

            $state.go(rootstate + '.editresult', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }


        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_30');
            var text = commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_31');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("quality") + 'QC_PollingDetail/RemoveQC_PollingDetail';
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
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);

                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_27'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_QualityApp_QC_PollingDetail';
        var moduleStateUrl = 'Siemens.SimaticIT_QualityApp_QC_PollingDetail';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_PollingDetail';

        var state = {
            name: moduleStateName + '_QC_PollingDetail',
            url: '/' + moduleStateUrl + '_QC_PollingDetail',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/QC_PollingDetail-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaillistctrl.Tips_32'
            }
        };
        $stateProvider.state(state);
    }
}());
