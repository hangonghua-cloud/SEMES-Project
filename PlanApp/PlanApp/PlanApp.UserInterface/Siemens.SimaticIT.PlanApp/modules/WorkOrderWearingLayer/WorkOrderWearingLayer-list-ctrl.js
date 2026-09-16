(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.WorkOrderWearingLayer.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.WorkOrderWearingLayer');

            init();
            //初始化grid选项
            initGridOptions();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_PlanApp_WorkOrderWearingLayer_WorkOrderWearingLayer';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.searchParams = {};
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];

            initDictionary();
            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询

            self.editConsume = editConsume;//转耗用
        }

        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })

        function initDictionary() {

            self.typeDemandMaterial = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_1'), ItemValue: "" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_1'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_2'), ItemValue: 1 },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_3'), ItemValue: 0 }
                ]
            };
            self.typeStoreIssue = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_1'), ItemValue: "" }]
            };
            self.typeSupe = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_1'), ItemValue: "" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_1'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_4'), ItemValue: ">0" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_5'), ItemValue: "=0" },
                ]
            };

            commonService.getDataItemDuatil("MaterialReleaseStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeStoreIssue.options = res.data.resultData;
                    self.typeStoreIssue.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_1')
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
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
                paginationPageSize: 100, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: true, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: true,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_6'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_8'),
                        width: 130
                    },
                    {
                        field: 'ExeWorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_9'),
                        width: 200
                    },
                    {
                        field: 'WearingLayerCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_10'),
                        width: 150
                    },
                    {
                        field: 'WearingLayerName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_11'),
                        width: 120
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_12'),
                        width: 120
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_13'),
                        width: 120
                    },
                    {
                        field: 'SheetsQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_14'),
                        width: 120
                    },
                    {
                        field: 'OrderType',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_15'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'1\'"><span ng-cell-text>正常工单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'2\'"><span ng-cell-text>补料单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'3\'"><span ng-cell-text>拣余单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'4\'"><span ng-cell-text>免产单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'5\'"><span ng-cell-text>超产品单</span></div>'
                    },
                    {
                        field: 'WearLayerStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_21'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.WearLayerStatus==\'1\'"><span ng-cell-text>未发料</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.WearLayerStatus==\'2\'"><span ng-cell-text>已发料</span></div>'
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_24'),
                        width: 110
                    },
                    {
                        field: 'StartOperationName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_25'),
                        width: 110
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_26'),
                        width: 110
                    },
                    {
                        field: 'ShouldNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_27'),
                        width: 110
                    },
                    {
                        field: 'ActualNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_28'),
                        width: 110
                    },
                    {
                        field: 'SuperNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_29'),
                        width: 110
                    },
                    {
                        field: 'CancellingNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_30'),
                        width: 110
                    },
                    {
                        field: 'ConsumeNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_31'),
                        width: 150
                    },
                    // {
                    //     field: 'Creator', 
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_32'),
                    //     width: 110
                    // },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_33'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_34'),
                        width: 200
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
                            } else {
                                self.selectedItem = null;
                                //self.isButtonVisible = false;
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

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_35'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//执行工单号
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.WearLayerStatus = self.typeStoreIssue.value.ItemValue;
            self.searchParams.SuperNum = self.typeSupe.value.ItemValue;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress('plan') + 'PL_ExeWorkOrderWearingLayer/PL_ExeWorkOrderWearingLayerPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_36'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
        }

        //转耗用
        function editConsume() {
            var title = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_37');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_38');
            var copyData = $scope.gridApi.selection.getSelectedRows();
            if (copyData.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_39'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_40'));
                return;
            }

            if (copyData[0].SuperNum <= 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_41'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_40'));
                return;
            }

            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('plan') + 'PL_ExeWorkOrderWearingLayer/SavePL_ExeWorkOrderWearingLayer';

                //提交删除当前选择数据实体
                var postData = {
                    KeyValue: copyData[0].Id,
                    Entity: {
                        Id: copyData[0].Id,
                        ConsumeNum: copyData[0].ConsumeNum + copyData[0].SuperNum,
                        SuperNum: 0
                    }
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_36'));
                });
            }, title);
        }

        //发料
        function addButtonHandler(clickedCommand) {

            var copyData = $scope.gridApi.selection.getSelectedRows();
            var flag = false;
            var Spec = ""
            var resmsg = ""
            var TotalNum = 0;

            if (copyData.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_42'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_40'));
                return;
            }

            copyData.forEach((item, index, arr) => {
                // if (item.WearLayerStatus == "2") {
                //     flag = true;
                //     resmsg = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_43')
                // }
                if (Spec == "") Spec = item.Spec;
                else if (Spec != "" && Spec != item.Spec) {
                    flag = true;
                    resmsg = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_44');
                }
                TotalNum += item.ShouldNum1
            });

            if (flag) {
                backendService.genericError(resmsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_40'));
                return;
            }

            $state.go(rootstate + '.add', { id: self.selectedItem.Id, selectedItem: { data: copyData, TotalNum: TotalNum } });
        }

        //退库
        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            var copyData = $scope.gridApi.selection.getSelectedRows();
            if (copyData.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_45'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_40'));
                return;
            }

            if (copyData[0].ActualNum <= 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_46'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_40'));
                return;
            }
            $state.go(rootstate + '.edit', { id: copyData[0].Id, selectedItem: copyData[0] });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_47');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_48');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress('plan') + 'PL_ExeWorkOrderWearingLayer/RemovePL_ExeWorkOrderWearingLayer';
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_36'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderWearingLayer';
        var moduleStateUrl = 'Siemens.SimaticIT_PlanApp_WorkOrderWearingLayer';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderWearingLayer';

        var state = {
            name: moduleStateName + '_WorkOrderWearingLayer',
            url: '/' + moduleStateUrl + '_WorkOrderWearingLayer',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/WorkOrderWearingLayer-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.JS.Tips_49'
            }
        };
        $stateProvider.state(state);
    }
}());
