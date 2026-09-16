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
    angular.module('Siemens.SimaticIT.QualityApp.QC_IPQCDetail').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetail.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetail');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_QualityApp_QC_IPQCDetail_QC_IPQCDetail';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.isButtonVisibleNew = false;
            self.isButtonVisibleEdit = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            self.checkRow = null;

            //Expose Model Methods
            // self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看附件
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询

            initDictionary();

            self.typeFactoryChange = typeFactoryChange;
            self.ProcessChange = ProcessChange;

        }

        function initDictionary() {
            //工序
            self.ProcessConfig = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_1'), ResourceCode: "" }]
            };

            //
            self.TestMachineConfig = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_1'), ResourceCode: "" }]
            };
            // commonService.getResourceExtendInfo({ LevelCode: "Process" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.ProcessConfig.options = res.data.resultData;
            //         self.ProcessConfig.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_1')
            //         });
            //     }
            // });
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_1')
                    });
                    initGridData();
                }
            });
        }
        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.ProcessConfig.options = res.data.resultData;
                        self.ProcessConfig.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_1')
                        });
                    }
                });
            } else {
                self.ProcessConfig = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_1'), ResourceCode: "" }]
                };
            }
        }

        function ProcessChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.TestMachineConfig.options = res.data.resultData;
                    self.TestMachineConfig.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_1')
                    });
                }
            });
        }


        $rootScope.$on("to-addChildItem", function (event, editData) {

        });

        $rootScope.$on("to-editItem", function (event, editData) {
            self.checkRow = editData;
            initGridData();
            //initGridDetailData();
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_5'),
                        width: 110
                    },
                    {
                        field: 'FlowCardId',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_6'),
                        width: 250
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_7'),
                        width: 300
                    },
                    {
                        field: 'SmallClass',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'ProductionWorkshopName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_9'),
                        width: 110
                    },
                    {
                        field: 'TestMachineName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'InspectorName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_11'),
                        width: 100
                    },
                    {
                        field: 'Dept',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_12'),
                        width: 300
                    },
                    {
                        field: 'InspectionTimeStr',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_13'),
                        width: 160
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_14'),
                        width: 160
                    },
                    {
                        field: 'Attachment',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_15'),
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

                                self.gridOptionsItem.data = [];
                                self.isButtonVisibleNew = false;
                                self.isButtonVisibleEdit = false;

                                initGridDetailData();
                                //console.log (self.selectedItem);
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                                self.gridOptionsItem.data = [];
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'TestItemCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_16'),
                        width: 300
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_17'),
                        width: 300
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_18'),
                        width: 300
                    },
                    {
                        field: 'TestItemResult',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_19'),
                        width: 300
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
            self.selectedItemDetail = null;
            self.isButtonVisible = false;
            self.isButtonVisibleNew = false;
            self.isButtonVisibleEdit = false;

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_20'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            if (self.TestMachineConfig.value != null) {
                self.searchParams.TestMachine = self.TestMachineConfig.value.ResourceCode;
            }
            else {
                self.searchParams.TestMachine = "";
            }
            if (self.ProcessConfig.value != null) {
                self.searchParams.ProductionWorkshop = self.ProcessConfig.value.ResourceCode;
            }
            else {
                self.searchParams.ProductionWorkshop = "";
            }
            if (self.StartDate != null && self.StartDate != "") {
                self.searchParams.StartTime = moment(self.StartDate).format("YYYY-MM-DD");
            }
            else {
                self.searchParams.StartTime = "";
            }
            if (self.EndDate != null && self.EndDate != "") {
                self.searchParams.EndTime = moment(self.EndDate).format("YYYY-MM-DD");
            }
            else {
                self.searchParams.EndTime = "";
            }
            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//流转卡编号
                sord: 'desc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("quality") + 'QC_IPQCDetail/QC_IPQCDetailPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_21'));
            });
        }

        function initGridDetailData() {
            let Pagination = {
                rows: self.gridOptionsItem.paginationPageSize,
                page: self.gridOptionsItem.paginationCurrentPage,
                sidx: 'TestItemCoading',//流转卡编号
                sord: 'asc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: {
                    FlowCardId: self.selectedItem.Id,
                    TestMaintenanceId: self.selectedItem.CalibrationMethod
                }
            };
            var url = commonService.getMesApiAddress("quality") + 'QC_IPQCDetailResult/GetCheckPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {// && res.data.resultData.rows.length > 0
                    //总条数
                    self.gridOptionsItem.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsItem.data = res.data.resultData.rows;
                    var aa = self.gridOptionsItem.data.filter(item => item.Id == null);
                    if (self.gridOptionsItem.data.filter(item => item.Id != null).length > 0) {
                        self.isButtonVisibleNew = false;
                        self.isButtonVisibleEdit = true;
                    }
                    else {
                        self.isButtonVisibleNew = true;
                        self.isButtonVisibleEdit = false;
                    }
                } else {
                    self.gridOptionsItem.data = [];
                    self.isButtonVisibleNew = true;
                    self.isButtonVisibleEdit = false;

                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_21'));
            });
        }

        //查询
        function searchButtonHandler() {
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.gridOptionsItem.data = [];
            self.isButtonVisibleNew = false;
            self.isButtonVisibleEdit = false;
            initGridData();
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
            var title = commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_22');
            var text = commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_23');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("quality") + 'QC_IPQCDetail/RemoveQC_IPQCDetail';
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
                        self.gridOptionsItem.data = [];
                        self.isButtonVisibleNew = false;
                        self.isButtonVisibleEdit = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_21'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_QualityApp_QC_IPQCDetail';
        var moduleStateUrl = 'Siemens.SimaticIT_QualityApp_QC_IPQCDetail';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_IPQCDetail';

        var state = {
            name: moduleStateName + '_QC_IPQCDetail',
            url: '/' + moduleStateUrl + '_QC_IPQCDetail',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/QC_IPQCDetail-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaillistctrl.Tips_24'
            }
        };
        $stateProvider.state(state);
    }
}());
