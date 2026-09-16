(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.OQCManage').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheck.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheck');

            init();
            //初始化grid选项
            initGridOptions();
            //初始化子表grid选项
            initGridOptionsDetail();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_QualityApp_OQCManage_OQCQualityCheck';
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
            initDictionary();

            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            self.addButtonHandler = addButtonHandler;

        }
        function initDictionary() {
            self.typeCheckStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_1'), ItemValue: "" }]
            };
            self.typeCheckResult = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_1'), ItemValue: "" }]
            };

            self.typeEnabled = {
                value: { ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_2') },
                options: [
                    { ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_1') },
                    { ItemValue: "1", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_3') },
                    { ItemValue: "0", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_4') }
                ]
            };

            //TestConclusion

            commonService.getDataItemDuatil("OQCCheckStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeCheckStatus.options = res.data.resultData;
                    self.typeCheckStatus.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("TestConclusion").then(function (res) {
                if (res && res.data.success) {
                    self.typeCheckResult.options = res.data.resultData;
                    self.typeCheckResult.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_1')
                    });
                    initGridData();
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_5'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'InspectNo',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_7'),
                        width: 140
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_8'),
                        width: 140
                    },
                    {
                        field: 'CustomerPO',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_10'),
                        width: 80
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_11'),
                        width: 110
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_12'),
                        width: 110
                    },

                    {
                        field: 'PackTransferCode',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_13'),
                        width: 140
                    },
                    {
                        field: 'CheckStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_14'),
                        width: 120,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.CheckStatus==\'1\'"><span ng-cell-text >待检验</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.CheckStatus==\'2\'"><span ng-cell-text >检验中</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.CheckStatus==\'3\'"><span ng-cell-text >已完成</span></div>'
                    },

                    {
                        field: 'CheckResult',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_15'),
                        width: 140
                    },

                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_16'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },


                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_17'),
                        width: 120
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_18'),
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
                sidx: 'CreateTime',//检测Id
                sord: 'desc'
            };

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_19'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.CheckResult = self.typeCheckResult.value.ItemValue;
            self.searchParams.CheckStatus = self.typeCheckStatus.value.ItemValue;
            if (self.StartDate && self.EndDate) {
                self.searchParams.StartDate = commonService.ConvertToLocalDate(self.StartDate);
                self.searchParams.EndDate = commonService.ConvertToLocalDate(self.EndDate);
            } else {
                self.searchParams.StartDate = null;
                self.searchParams.EndDate = null;
            }
            if (self.CheckStartDate && self.CheckEndDate) {
                self.searchParams.CheckStartDate = commonService.ConvertToLocalDate(self.CheckStartDate);
                self.searchParams.CheckEndDate = commonService.ConvertToLocalDate(self.CheckEndDate);
            } else {
                self.searchParams.CheckStartDate = null;
                self.searchParams.CheckEndDate = null;
            }


            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("quality") + 'QC_OQCQualityCheck/QC_OQCQualityCheckPageDataTableList';

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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_20'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
        }

        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }


        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_21');
            var text = commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_22');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress("quality") = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("quality") + 'QC_OQCQualityCheck/RemoveQC_OQCQualityCheck';
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_20'));
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_5'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },

                    {
                        field: 'TestItemCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_23'),
                        width: 140
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_24'),
                        width: 140
                    },

                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_25'),
                        width: 140
                    },

                    {
                        field: 'DataTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_26'),
                        width: 140
                    },
                    {
                        field: 'TestDepartment',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_27'),
                        width: 140,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'1\'"><span ng-cell-text>实验室</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'2\'"><span ng-cell-text>质量部</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'3\'"><span ng-cell-text>生产部</span></div>'
                    },
                    {
                        field: 'CheckResult',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_15'),
                        width: 140
                    },
                    {
                        field: 'Distinguish',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_28'),
                        width: 140
                    },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_29'),
                        width: 140
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_30'),
                        width: 140,
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
                        initGridDataDetail();
                    });
                    //行选中事件
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailButtonVisible = true;
                                //console.log (self.selectedItemDetail);
                                //子表明细关联
                                initGridDataDetail();
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
                sidx: 'TestDepartment,CreateTime',//检验记录Id
                sord: 'desc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.OQCQualityCheckId = self.selectedItem.Id;
            }
            else {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_31'), commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_32'));
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };

            var url = commonService.getMesApiAddress("quality") + 'QC_OQCQualityCheckItem/QC_OQCQualityCheckItemPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_20'));
            });
        }

        //查询
        function search2ButtonHandler() {
            initGridDataDetail();
        }



        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.selectDetail', { id: self.selectedItemDetail.ID, selectedItem: self.selectedItemDetail });
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
            name: moduleStateName + '_OQCQualityCheck',
            url: '/' + moduleStateUrl + '_OQCQualityCheck',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/OQCQualityCheck-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityChecklistctrl.Tips_33'
            }
        };
        $stateProvider.state(state);
    }
}());
