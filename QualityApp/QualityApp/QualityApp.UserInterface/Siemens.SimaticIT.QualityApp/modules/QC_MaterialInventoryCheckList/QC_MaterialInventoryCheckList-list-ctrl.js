/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 原材料库存检验记录表
*  2. 创建人员： 丁零
*  3. 创建日期： 2021-08-27
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckList.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckList');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_QualityApp_QC_MaterialInventoryCheckList_QC_MaterialInventoryCheckList';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.isButtonVisible = false;
            self.isButtonVisibleNew = false;
            self.isButtonVisibleEdit = false;
            self.isQualityButtonVisible = false;
            self.isQualityButtonVisible2 = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            self.auditCheckButtonHandler = auditCheckButtonHandler;

            self.addDetailButtonHandler = addDetailButtonHandler;//新增
            self.editDetailButtonHandler = editDetailButtonHandler;//编辑

            self.typeFactoryChange = typeFactoryChange;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            initDictionary();
        }


        $rootScope.$on("to-parent", function (event, editData) {
            initGridData();
        });

        $rootScope.$on("to-editChildItem", function (event, editData) {
            initGridDetailData();
        });

        function initDictionary() {

            self.typeWhsCode = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_1'), ResourceCode: "" }]
            };
            self.typeTestDepartment = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_1'), ItemValue: "" }]
            };
            self.typeTestResult = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_1'), ItemValue: "" }]
            };

            // commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeWhsCode.options = res.data.resultData;
            //         self.typeWhsCode.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_1')
            //         });
            //     }
            // });
            commonService.getDataItemDuatil("AssayDepartment").then(function (res) {
                if (res && res.data.success) {
                    self.typeTestDepartment.options = res.data.resultData;
                    self.typeTestDepartment.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("QualityJudgement").then(function (res) {
                if (res && res.data.success) {
                    self.typeTestResult.options = res.data.resultData;
                    self.typeTestResult.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_1')
                    });
                    initGridData();
                }
            });

        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getWarehouseByFactory({ factoryCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeWhsCode.options = res.data.resultData;
                        self.typeWhsCode.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_1')
                        });
                    }
                });
            } else {
                self.typeWhsCode = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_1'), ResourceCode: "" }]
                };
            }
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_3'),
                        width: 140
                    },
                    {
                        field: 'InspectNo',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_4'),
                        width: 140
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_5'),
                        width: 110
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_6'),
                        width: 110
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_7'),
                        width: 110
                    },
                    {
                        field: 'SupplierName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_9'),
                        width: 110
                    },
                    {
                        field: 'TestResult',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_10'),
                        width: 100,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TestResult==\'1\'"><span ng-cell-text>合格</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestResult==\'2\'"><span ng-cell-text>不合格</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestResult==\'3\'"><span ng-cell-text>让步接收</span></div>'
                    },
                    {
                        field: 'IsFrozen',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_11'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.IsFrozen==\'0\'"><span ng-cell-text>未冻结</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.IsFrozen==\'1\'"><span ng-cell-text>冻结</span></div>'
                    },
                    {
                        field: 'TestDepartment',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_12'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'1\'"><span ng-cell-text>实验室</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'2\'"><span ng-cell-text>质量部</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'3\'"><span ng-cell-text>生产部</span></div>'
                    },
                    // {
                    //     field: 'InspectorName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_13'),
                    //     width: 200
                    // },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_14'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'
                    },

                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_15'),
                        width: 200
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

                                initGridDetailData();
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'TestItemCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_16'),
                        width: 200
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_17'),
                        width: 200
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_18'),
                        width: 200
                    },

                    {
                        field: 'ItemValue',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_19'),
                        width: 200
                    }/*,
                        {
                            field: 'CreatorName', 
                            displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_13'),
                            width: 200
                        },
                        {
                            field: 'CreatTime', 
                            displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_14'),
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
            self.selectedItem = null;
            self.isButtonVisible = false;
            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//
                sord: 'desc'
            };
            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_20'))
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.WhsCode = self.typeWhsCode.value.ResourceCode;
            self.searchParams.TestDepartment = self.typeTestDepartment.value.ItemValue;
            self.searchParams.TestResult = self.typeTestResult.value.ItemValue;

            if (self.StartDate && self.EndDate) {
                self.searchParams.StartTime = commonService.ConvertToLocalTime(self.StartDate);
                self.searchParams.EndTime = commonService.ConvertToLocalTime(self.EndDate);
            } else {
                self.searchParams.StartTime = "";
                self.searchParams.EndTime = "";
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            // debugger
            var url = commonService.getMesApiAddress("quality") + 'QC_MaterialInventoryCheck/QC_MaterialInventoryCheckPageDataTableList';

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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_21'));
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
            if (!self.selectedItem) {
                self.gridOptionsItem.data = [];
                return
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: {
                    MaterialInventoryId: self.selectedItem.Id
                }
            };

            var url = commonService.getMesApiAddress("quality") + 'QC_MaterialInventoryCheckItem/QC_MaterialInventoryCheckItemPageDataTableList';

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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_21'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
            initGridDetailData();
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

        function addDetailButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addresult', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function editDetailButtonHandler(clickedCommand) {
            $state.go(rootstate + '.editresult', { id: self.selectedItem.Id, selectedItem: self.selectedItem, selectedItemDetail: self.selectedItemDetail });
        }

        function auditCheckButtonHandler(clickedCommand) {
            $state.go(rootstate + '.editaudit', { id: self.selectedItem.Id, selectedItem: self.selectedItem, selectedItemDetail: self.selectedItemDetail });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.editresult', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_22');
            var text = commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_23');
            backendService.confirm(text, function () {

                var url = commonService.getMesApiAddress("quality") + 'QC_MaterialInventoryCheck/RemoveQC_MaterialInventoryCheck';

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

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_21'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_QualityApp_QC_MaterialInventoryCheckList';
        var moduleStateUrl = 'Siemens.SimaticIT_QualityApp_QC_MaterialInventoryCheckList';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_MaterialInventoryCheckList';

        var state = {
            name: moduleStateName + '_QC_MaterialInventoryCheckList',
            url: '/' + moduleStateUrl + '_QC_MaterialInventoryCheckList',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/QC_MaterialInventoryCheckList-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListlistctrl.Tips_24'
            }
        };
        $stateProvider.state(state);
    }
}());
