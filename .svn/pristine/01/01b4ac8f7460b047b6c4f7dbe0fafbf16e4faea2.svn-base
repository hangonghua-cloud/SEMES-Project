(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EquipmentAccount').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccount.service', '$state', '$stateParams', '$rootScope', '$scope', 'common.base',
        'common.services.logger.service', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService', '$timeout', 'i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, commonService, auth, notificationService, $timeout, i18nService) {
        //国际化 
        i18nService.setCurrentLang('zh-cn');
        var self = this;
        var logger, rootstate, messageservice, backendService;


        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccount');

            init();
            //初始化grid选项
            initGridOptions();
            //初始化子表grid选项
            initGridOptionsDetail();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_EquipmentApp_EquipmentAccount_EquipmentAccount';
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

            //Expose Model Methods
            //self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑

            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            //子明细
            self.add2ButtonHandler = add2ButtonHandler;//新增
            self.edit2ButtonHandler = edit2ButtonHandler;//编辑
            self.delete2ButtonHandler = delete2ButtonHandler;//删除
            self.select2ButtonHandler = select2ButtonHandler;//查看//子表/明细//关联

            initDictionary();
            self.FactoryChange = FactoryChange;

            $rootScope.$on('to-parent', function (event, OnData) {
                $timeout(function () {
                    //绑定gridView方法
                    initGridData();
                }, 1500);
            });

            $rootScope.$on('to-addChildItem', function (event, OnData) {
                $timeout(function () {
                    //绑定gridView方法
                    initGridDataDetail();
                }, 1500);
            });
        }

        function initDictionary() {
            self.CodadConfig = {
                selectedOption: { ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ResourceCode: "" }]
            };
            self.ProcessConfig = {
                selectedOption: { ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ResourceCode: "" }]
            };
            self.SiteConfig = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ResourceCode: "" }]
            };
            //状态
            self.StatusConfig = {
                selectedOption: { ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ItemValue: "" }]
            };

            self.TypeConfig = {
                selectedOption: { ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ItemValue: "" }]
            };

            commonService.getDataItemDuatil("EquipmentStatus").then(function (res) {
                self.StatusConfig.options = res.data.resultData;
                self.StatusConfig.selectedOption = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
            });

            commonService.getDataItemDuatil("EquipmentTypes").then(function (res) {
                self.TypeConfig.options = res.data.resultData;
                self.TypeConfig.selectedOption = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };

            });
            self.typeQulaityTest = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ItemValue: "" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_2'), ItemValue: "1" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_3'), ItemValue: "0" },
                ]
            };
            self.typeFirstTest = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ItemValue: "" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_2'), ItemValue: "1" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_3'), ItemValue: "0" },
                ]
            };

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.CodadConfig.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.CodadConfig.selectedOption = res.data.resultData[0];
                    }
                    self.CodadConfig.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1')
                    });
                    initGridData();
                }
            });

        }

        function FactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                //工序
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.ProcessConfig.options = res.data.resultData;
                        self.ProcessConfig.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1')
                        });
                    }
                });
                //车间
                commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.SiteConfig.options = res.data.resultData;
                        self.SiteConfig.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1')
                        });
                    }
                });
            } else {
                self.ProcessConfig = {
                    selectedOption: { ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ResourceCode: "" }]
                };
                self.SiteConfig = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_1'), ResourceCode: "" }]
                };
            }
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_4'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    }, {
                        field: 'TestMethodCoadinName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_5'),
                        width: 110
                    }, {
                        field: 'EquipmentId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_6'),
                        width: 110
                    }, {
                        field: 'EquipmentName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_7'),
                        width: 110
                    }, {
                        field: 'EquipmentTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_8'),
                        width: 110
                    },
                    //  {
                    //     field: 'EquipmentStatusName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_9'),
                    //     width: 110
                    // }, 
                    // {
                    //     field: 'SpedificationsMode',
                    //     displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_10'),
                    //     width: 110
                    // },
                    {
                        field: 'InstallationSiteName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_11'),
                        width: 110
                    }, {
                        field: 'ProcessBelongName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_12'),
                        width: 110
                    },
                    //{
                    //     field: 'LineName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_13'),
                    //     width: 110
                    // },
                    // {
                    //     field: 'Remark',
                    //     displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_14'),
                    //     width: 200
                    // },
                    //  {
                    //     field: 'Creator',
                    //     displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_15'),
                    //     width: 150,
                    //     visible: true
                    // }, 
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_16'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridProcessApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData();
                    });
                    //行选中事件
                    $scope.gridProcessApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                debugger
                                self.selectedItem = row.entity;
                                self.isButtonVisible = true;
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

        function initGridData() {
            if (!self.CodadConfig.selectedOption.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_17'));
                return;
            }
            self.Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',
                sord: 'desc'
            };

            if (self.CodadConfig.selectedOption != null && self.CodadConfig.selectedOption.ResourceCode != "")
                self.searchParams.TestMethodCoadin = self.CodadConfig.selectedOption.ResourceCode;
            else
                self.searchParams.TestMethodCoadin = "";



            if (self.SiteConfig.selectedOption != null && self.SiteConfig.selectedOption.ResourceCode != "")
                self.searchParams.InstallationSite = self.SiteConfig.selectedOption.ResourceCode;
            else
                self.searchParams.InstallationSite = "";

            if (self.ProcessConfig.selectedOption != null && self.ProcessConfig.selectedOption.ResourceCode != "")
                self.searchParams.ProcessBelong = self.ProcessConfig.selectedOption.ResourceCode;
            else
                self.searchParams.ProcessBelong = "";

            if (self.TypeConfig.selectedOption != null && self.TypeConfig.selectedOption.ItemValue != "")
                self.searchParams.EquipmentType = self.TypeConfig.selectedOption.ItemValue;
            else
                self.searchParams.EquipmentType = "";

            var queryParmeters = {
                pagination: self.Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("equipment") + 'EquipmentManage/GetPage_Equipment';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                // console.log("-self.GetPage_Equipment----------------------------" + JSON.stringify(res));

                if ((res) && (res.data.success)) {
                    var resultData = res.data.resultData;
                    //总条数
                    self.gridOptions.totalItems = resultData.records;

                    self.gridOptions.data = resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {

                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_18'));
            });
        }

        //设备查询
        function searchButtonHandler() {
            initGridData();
        }

        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }


        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        function deleteButtonHandler(clickedCommand) {
            backendService.confirm(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_19'), function () {
                var url = commonService.getMesApiAddress("equipment") + 'EquipmentManage/DeleteForm?keyValue=' + self.selectedItemDetail.ID;
                debugger;
                commonService.callWebApiGet(url, null).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        initGridData();
                        self.is_editButtonHandler = false;
                        //self.selectedItemDetail = null;
                        self.isButtonVisible = false;
                        self.isDeleteBtnVisible = false;
                    } else {
                        self.gridOptions.data = [];
                        backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_18'));
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_18'));
                });

            }, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_20'));
        }

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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_4'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'EquipCode',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_21'),
                        width: 140
                    },
                    {
                        field: 'EquipName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_22'),
                        width: 140
                    },
                    {
                        field: 'EquipStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_9'),
                        width: 110
                    },
                    {
                        field: 'EquipClass',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_23'),
                        width: 110
                    },


                    {
                        field: 'EquipSpec',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'Manufacturer',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_24'),
                        width: 120
                    },
                    {
                        field: 'ProducedDate',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_25'),
                        width: 120,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'
                    },
                    {
                        field: 'UserDate',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_26'),
                        width: 120,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_14'),
                        width: 140
                    },
                    {
                        field: 'Attachment',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_27'),
                        width: 140
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_15'),
                        width: 140
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_28'),
                        width: 140,
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
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsDetail.paginationPageSize,
                page: self.gridOptionsDetail.paginationCurrentPage,
                sidx: 'CreateTime',//设备台账Id
                sord: 'desc'
            };

            if (self.StatusConfig.selectedOption != null && self.StatusConfig.selectedOption.ItemValue != "")
                self.searchParams2.EquipmentStatus = self.StatusConfig.selectedOption.ItemValue;
            else
                self.searchParams2.EquipmentStatus = "";


            if (self.selectedItem != null) {
                //关联字段

                self.searchParams2.EMId = self.selectedItem.ID;
            }
            else {
                self.gridOptionsDetail.data = []
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentManageItem/EP_EquipmentManageItemPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsDetail.totalItems = res.data.resultData.records;
                    debugger
                    //数据
                    self.gridOptionsDetail.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsDetail.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_18'));
            });
        }

        //查询
        function search2ButtonHandler() {
            initGridDataDetail();
        }

        //新增
        function add2ButtonHandler(clickedCommand) {
            debugger
            $state.go(rootstate + '.addDetail', { id: self.selectedItem.ID, EquipmentId: self.selectedItem.EquipmentId, selectedItem: self.selectedItem });
        }

        //编辑
        function edit2ButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.editDetail', { id: self.selectedItemDetail.Id, selectedItem: self.selectedItemDetail });
        }

        //查看/明细/子表//绑定
        function select2ButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItemDetail.Id, selectedItem: self.selectedItemDetail });
        }

        //删除 事件
        function delete2ButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_20');
            var text = commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_29');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentManageItem/RemoveEP_EquipmentManageItem';
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_18'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_EquipmentApp_EquipmentAccount';
        var moduleStateUrl = 'Siemens.SimaticIT_EquipmentApp_EquipmentAccount';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EquipmentAccount';

        var state = {
            name: moduleStateName + '_EquipmentAccount',
            url: '/' + moduleStateUrl + '_EquipmentAccount',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/EquipmentAccount-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountlistctrl.Tips_30'
            }
        };
        $stateProvider.state(state);
    }
}());
