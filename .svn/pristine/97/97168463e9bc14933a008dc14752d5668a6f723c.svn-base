(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.MaterialBatchUpRecord.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.MaterialBatchUpRecord');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_MaterialBatchUpRecord_MaterialBatchUpRecord';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            initDictionary();

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询

            self.FactoryChange = FactoryChange;
            self.ProcessChange = ProcessChange;
        }
        function initDictionary() {
            self.Factory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_1'), ResourceCode: "" }]
            };
            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_1'), ResourceCode: "" }]
            };
            self.Machine = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_1'), ResourceCode: "" }]
            };

            self.typeMaterialtype = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_1'), ItemValue: "" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_1'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_2'), ItemValue: 1 },
                    { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_3'), ItemValue: 0 }
                ]
            };

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.Factory.value = res.data.resultData[0];
                    }
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_1')
                    });
                    initGridData();
                }
            });
            // commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeMaterialtype.options = res.data.resultData;
            //         self.typeMaterialtype.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
            //     }
            // })
        }

        function FactoryChange(oldItem, newItem) {
            commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.Process.options = res.data.resultData;
                    self.Process.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_1')
                    });
                }
            });
        }


        // commonService.getResourceExtendInfo({ LevelCode: "Process" }).then(function (res) {
        //     if (res && res.data.success) {
        //         self.Process.options = res.data.resultData;
        //         self.Process.options.splice(0, 0, {
        //             ResourceCode: "",
        //             ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_1')
        //         });
        //     }
        // });


        function ProcessChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.Machine.options = res.data.resultData;
                    self.Machine.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_1')
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
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_4'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },

                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_6'),
                        width: 100
                    },
                    {
                        field: 'MachineName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_7'),
                        width: 150
                    },

                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_8'),
                        width: 120
                    },
                    // {
                    //     field: 'CustomerPO',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_9'),
                    //     width: 80
                    // },

                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_10'),
                        width: 200
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_11'),
                        width: 150
                    },
                    {
                        field: 'isFlag',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_12'),
                        width: 110
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_13'),
                        width: 100
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_14'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },

                    {
                        field: 'Modify',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_15'),
                        width: 100
                    },
                    {
                        field: 'ModifyTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_16'),
                        width: 160,
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

            if (!self.Factory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_17'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//订单号
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.Factory.value.ResourceCode;
            self.searchParams.ProcessCode = self.Process.value.ResourceCode;
            self.searchParams.MachineCode = self.Machine.value.ResourceCode;

            self.searchParams.Flag = self.typeMaterialtype.value.ItemValue;


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
            // debugger
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_MaterialBatchUpRecord/PM_MaterialBatchUpRecordPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_18'));
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
            $state.go(rootstate + '.edit', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_19');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_20');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress("ProduceManage") = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_MaterialBatchUpRecord/RemovePM_MaterialBatchUpRecord';
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
                console.log("new postData------------------------------------" + JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (res) {
                    console.log("RemovePM_MaterialBatchUpRecord----------------------------" + JSON.stringify(res));
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
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_23'));
                    }
                }, function (error) {
                    console.log("RemovePM_MaterialBatchUpRecord--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_18'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_MaterialBatchUpRecord';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_MaterialBatchUpRecord';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/MaterialBatchUpRecord';

        var state = {
            name: moduleStateName + '_MaterialBatchUpRecord',
            url: '/' + moduleStateUrl + '_MaterialBatchUpRecord',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/MaterialBatchUpRecord-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.MaterialBatchUpRecord.JS.Tips_24'
            }
        };
        $stateProvider.state(state);
    }
}());
