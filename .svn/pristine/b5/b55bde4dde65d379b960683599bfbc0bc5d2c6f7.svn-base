(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.ReworkRecord').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.ReworkRecord.ReworkRecord.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', 'common.services.security.securityService', 'common.services.security.functionRightModel', 'i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, securityService, FunctionRightModel, i18nService) {
        var self = this;
        var logger, rootstate, messageservice, backendService;
        i18nService.setCurrentLang('zh-cn');

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.ReworkRecord.ReworkRecord');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();
            //初始化子表grid选项
            initGridOptionsDetail();
            initGridOptionsDetail2();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_ReworkRecord_ReworkRecord';
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
            //报工不良
            self.searchParams3 = {};
            self.selectedItemDetail2 = null;

            //初始化数据字典
            initDictionary();

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            self.checkButtonHandler = checkButtonHandler;//质量确认
            //子明细 

            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            self.FactoryChange = FactoryChange;

            //按钮权限
            ButtonAuthInitFalse();
            self.ReworkDel = false;//删除
            self.ReworkQualityConfirm = false;//质量确认
            ButtonAuthInit();
        }

        function ButtonAuthInitFalse() {
            //1.定义初始按钮
            self.isReworkDel = false;//删除
            self.isReworkQualityConfirm = false;//质量确认
        }

        //按钮权限
        function ButtonAuthInit() {
            var PageName = "Rework";
            var jo = {
                PageName: PageName
            };

            var url = commonService.getMesApiAddress() + 'Base/GetButtonAuthList';
            //var url = 'http://localhost:49888/' + 'Base/GetButtonAuthList';
            commonService.callWebApiPost(url, jo).then(function (res) {
                self.funRightListModel = [];

                if ((res) && (res.data.success) && res.data.resultData.length > 0) {
                    //数据
                    for (var i = 0; i < res.data.resultData.length; i++) {
                        self.funRightListModel.push(new FunctionRightModel('business_command', '' + res.data.resultData[i].FullName + '', 'invoke'));
                    }
                    securityService.canPerformOp(self.funRightListModel).then(function (data) {
                        if (data) {
                            if (data.length > 0) {
                                for (var i = 0; i < data.length; i++) {
                                    if (data[i].objectName.split('.')[7] == PageName + "Del") {
                                        self.ReworkDel = data[i].isAccessible;//删除
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "QualityConfirm") {
                                        self.ReworkQualityConfirm = data[i].isAccessible;//质量确认
                                    }
                                }
                            }
                        }
                    }, function (resError) {
                        backendService.genericError('获取数据出错', resError);
                    });

                } else {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_2'), commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_3'));
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_4'));
            });
        }

        function initDictionary() {

            self.Factory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5'), ResourceCode: "" }]
            };
            self.CurrentProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5'), ResourceCode: "" }]
            };
            self.DutyProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5'), ResourceCode: "" }]
            };
            self.ReworkProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5'), ResourceCode: "" }]
            };
            self.ReworkStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5'), ItemValue: "" }]
            };
            self.ConfirmStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5'), ItemValue: "" },
                { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_6'), ItemValue: "0" },
                { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_7'), ItemValue: "1" }]
            };

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.Factory.value = res.data.resultData[0];
                    }
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5')
                    });
                    initGridData();
                }
            });

            commonService.getDataItemDuatil("ReworkStatus").then(function (res) {
                if (res && res.data.success) {
                    self.ReworkStatus.options = res.data.resultData;
                    self.ReworkStatus.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })

        };

        function FactoryChange(oldItem, newItem) {

            commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.CurrentProcess.options = angular.copy(res.data.resultData);
                    self.CurrentProcess.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5')
                    });
                    self.DutyProcess.options = angular.copy(res.data.resultData);
                    self.DutyProcess.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5')
                    });

                    self.ReworkProcess.options = angular.copy(res.data.resultData);
                    self.ReworkProcess.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_5')
                    });

                }
            });
        }

        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })

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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_8'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'ReworkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_10'),
                        width: 150
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_11'),
                        width: 150
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_12'),
                        width: 100
                    },
                    {
                        field: 'Status',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_13'),
                        width: 100,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.Status==\'1\'"><span ng-cell-text>未开始</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.Status==\'2\'"><span ng-cell-text>正在返工</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.Status==\'3\'"><span ng-cell-text>已完成</span></div>'
                    },

                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_17'),
                        width: 100
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_18'),
                        width: 200
                    },
                    {
                        field: 'PalletQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_19'),
                        width: 100
                    },
                    {
                        field: 'CurrentProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_20'),
                        width: 100
                    },
                    {
                        field: 'DutyProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_21'),
                        width: 100
                    },
                    {
                        field: 'ReworkProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_22'),
                        width: 100
                    },

                    {
                        field: 'Operator',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_23'),
                        width: 100
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_24'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'BGPalletyQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_25'),
                        width: 100
                    },
                    {
                        field: 'QualityConfirmUser',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_26'),
                        width: 100
                    },
                    {
                        field: 'QualityConfirmTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_27'),
                        width: 170,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_28'),
                        width: 100
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

                                //按钮权限
                                self.isReworkDel = self.ReworkDel;//删除
                                self.isReworkQualityConfirm = self.ReworkQualityConfirm;//质量确认
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                                //按钮权限
                                ButtonAuthInitFalse();
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
            //按钮权限
            ButtonAuthInitFalse();

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//创建时间
                sord: 'desc'
            };

            if (!self.Factory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_29'));
                return;
            }
            self.searchParams.FactoryCode = self.Factory.value.ResourceCode;
            self.searchParams.CurrentProcess = self.CurrentProcess.value.ResourceCode;
            self.searchParams.ReworkProcess = self.ReworkProcess.value.ResourceCode;
            self.searchParams.DutyProcess = self.DutyProcess.value.ResourceCode;
            self.searchParams.Status = self.ReworkStatus.value.ItemValue;
            self.searchParams.ConfirmStatus = self.ConfirmStatus.value.ItemValue;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ReworkRecord/PM_ReworkRecordPageDataTableList';

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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_1'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
        }

        //新增
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        //编辑
        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            if (self.selectedItem.Status != "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_30'), commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_1'));
                return
            }
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }


        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            if (self.selectedItem.Status != "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_31'), commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_1'));
                return
            }
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_32');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_33');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ReworkRecord/RemovePM_ReworkRecord';

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
                        console.log('删除数据出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_35'));
                    }
                }, function (error) {
                    console.log("RemovePM_ReworkRecord--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_1'));
                });
            }, title);
        }

        function checkButtonHandler() {

            if (self.selectedItem.Status == "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_36'), commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_37'));
                return;
            }
            if (self.selectedItem.ConfirmStatus == "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_38'), commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_37'));
                return;
            }

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ReworkRecord/CheckRecord';
            var postData = {
                KeyValue: self.selectedItem.Id
            };

            commonService.callWebApiPost(url, postData).then(function (res) {
                console.log("CheckRecord----------------------------" + JSON.stringify(res));
                if ((res) && (res.data.success)) {
                    var resultData = res.data.resultData;
                    //成功
                    commonService.showInfo(res.data.returnMsg);
                    //重新刷新列表
                    initGridData();
                    self.gridOptionsDetail.data = [];
                    self.selectedItem = null;
                    self.isButtonVisible = false;
                } else {
                    //失败
                    commonService.showWarning(res.data.returnMsg);
                    console.log('审核数据出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);

                }
            }, function (error) {
                console.log("CheckRecord--error--------------------------" + JSON.stringify(error));
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_1'));
            });
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_8'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    //{
                    //    field: 'ReworkId',
                    //    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_40'),
                    //    width: 200
                    //},
                    {
                        field: 'CardCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_41'),
                        width: 250
                    },
                    {
                        field: 'CardName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_42'),
                        width: 100
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_43'),
                        width: 120
                    },
                    {
                        field: 'BadQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_44'),
                        width: 120
                    },
                    {
                        field: 'PTeamName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_45'),
                        width: 120
                    },
                    {
                        field: 'BGUser',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_46'),
                        width: 120
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_47'),
                        width: 170,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'ReworkStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_48'),
                        width: 90
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
                                //console.log (self.selectedItemDetail);
                                //子表明细关联
                                initGridDataDetail2();
                            } else {
                                self.selectedItemDetail = null;
                                self.isDetailButtonVisible = false;
                                self.gridOptionsDetail2.data = [];
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
                sidx: 'CardName',//托号
                sord: 'asc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.ReworkId = self.selectedItem.Id;
            }
            else {
                self.gridOptionsDetail.data = [];
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2,
                reworkProductType: self.selectedItem.ReworkProductType
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ReworkRecord_Detail/PM_ReworkRecord_DetailPageDataTableList';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                //console.log ('-self.Post_ResultData----------------------' + JSON.stringify(res));
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsDetail.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsDetail.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsDetail.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_1'));
            });
        }

        function initGridOptionsDetail2() {
            self.gridOptionsDetail2 = {
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
                    // {
                    //     name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_8'), width: 80, enableSorting: false, cellTemplate:
                    //         '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    // },
                    {
                        field: 'BadItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_49'),
                        width: 120
                    },
                    {
                        field: 'BadQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_44'),
                        width: 120
                    },
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApiDetail2 = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataDetail2();
                    });
                    //行选中事件
                    $scope.gridApiDetail2.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail2 = row.entity;

                            } else {
                                self.selectedItemDetail2 = null;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //子表查询方法,数据绑定
        function initGridDataDetail2() {
            self.selectedItemDetail2 = null;
            let Pagination = {
                rows: self.gridOptionsDetail2.paginationPageSize,
                page: self.gridOptionsDetail2.paginationCurrentPage,
                sidx: 'BadItemCode',//不良项目编码
                sord: 'asc'
            };

            if (self.selectedItemDetail != null) {
                if (!self.selectedItemDetail.BGID) {
                    self.gridOptionsDetail2.data = [];
                    return;
                }

                //关联字段
                self.searchParams3.BGID = self.selectedItemDetail.BGID;
            }
            else {
                self.gridOptionsDetail2.data = [];
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams3,
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_BGBadRecord/PM_BGBadRecordPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsDetail2.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsDetail2.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsDetail2.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_1'));
            });
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_ReworkRecord';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_ReworkRecord';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/ReworkRecord';

        var state = {
            name: moduleStateName + '_ReworkRecord',
            url: '/' + moduleStateUrl + '_ReworkRecord',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ReworkRecord-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.ReworkRecord.JS.Tips_50'
            }
        };
        $stateProvider.state(state);
    }
}());
