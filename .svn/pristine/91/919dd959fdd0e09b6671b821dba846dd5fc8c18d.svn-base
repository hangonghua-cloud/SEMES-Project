/**
*  0. 代码生成： UADM单表一键生成前后端html、JS、API接口代码生成器 Ver 2.01 发布日期：2021-04-18  设计师：刘万军
*  1. 功能描述： 跨工厂调拨
*  2. 创建人员： jpf
*  3. 创建日期： 2022-11-16
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.PL_TransfersRecord').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.PlanApp.PL_TransfersRecord.PL_TransfersRecord.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', 'i18nService', 'common.services.security.securityService', 'common.services.security.functionRightModel'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, i18nService, securityService, FunctionRightModel) {
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.PL_TransfersRecord');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_PlanApp_PL_TransfersRecord_PL_TransfersRecord';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//搜索
            self.ReciveButtonHandler = ReciveButtonHandler;//接收操作
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory", role: "admin" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_1')
                    });
                    initGridData();
                }
            });
            self.TransTypeName = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_1'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("TransferType").then(function (res) {
                if (res && res.data.success) {
                    self.TransTypeName.options = res.data.resultData;
                    self.TransTypeName.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            self.TransState = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_1'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("TransferStatus").then(function (res) {
                if (res && res.data.success) {
                    self.TransState.options = res.data.resultData;
                    self.TransState.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
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
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: true,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'SendFactoryCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'SendFactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_4'),
                        width: 150
                    },

                    {
                        field: 'AcceptFactoryCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'AcceptFactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_6'),
                        width: 150
                    },
                    {
                        field: 'TransTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'TransProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'TransStateName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_10'),
                        width: 150
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_11'),
                        width: 150
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_12'),
                        width: 120
                    }
                    ,
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_13'),
                        width: 110
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_14'),
                        width: 140
                    },
                    {
                        field: 'BWXH',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_15'),
                        width: 140
                    },
                    {
                        field: 'UV',
                        displayName: 'UV',
                        width: 140
                    },
                    {
                        field: 'KCKX',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_16'),
                        width: 140
                    },
                    {
                        field: 'OrderPiecesAll',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_17'),
                        width: 130
                    },

                    {
                        field: 'OrderPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_18'),
                        width: 130
                    },

                    {
                        field: 'OrderPiecesNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_19'),
                        width: 130
                    },

                    {
                        field: 'OrderBox',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_20'),
                        width: 130
                    },
                    {
                        field: 'OrderPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_21'),
                        width: 140
                    }
                    ,

                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_22'),
                        width: 200
                    },

                    {
                        field: 'CreateName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_23'),
                        width: 200
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_24'),
                        width: 200,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
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
        //子表查询方法,数据绑定
        function initGridData() {

            self.searchParams.SendFactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.TransTypeCode = self.TransTypeName.value.ItemValue;
            self.searchParams.TransStateCode = self.TransState.value.ItemValue;
            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//创建时间
                sord: 'asc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            console.log('queryParmeters-----' + JSON.stringify(queryParmeters));
            var url = commonService.getMesApiAddress("plan") + 'PL_TransfersRecord/PL_TransfersRecordPageDataTableList';
            //var url = 'http://localhost:49849/' + 'BS_TraitDetails' + '/BS_TraitDetailsPageDataTableList'; 
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                //console.log ('-self.Post_ResultData----------------------' + JSON.stringify(res));
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_25'));
            });
        }
        //新增
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }
        function searchButtonHandler(clickedCommand) {
            initGridData();
        }
        //编辑
        function editButtonHandler(clickedCommand) {
            var data = $scope.gridApi.selection.getSelectedRows();
            //判断选择工单调拨的状态
            var statusNum = 0;
            var Factorynum = 0;
            var Transtypenum = 0;
            data.forEach((item, index, arr) => {
                if (item.TransStateCode != 3) {
                    statusNum = statusNum + 1;
                }
            })
            if (statusNum > 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_26'));
                return false;
            }
            //判断发起工厂是否相同不同，不允许一起回退
            var Factory = data[0].SendFactoryCode;
            data.forEach((item, index, arr) => {
                if (item.SendFactoryCode != Factory) {
                    Factorynum = Factorynum + 1;
                }
            })
            if (statusNum > 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_27'));
                return false;
            }
            var TransTypeCode = data[0].TransTypeCode;
            data.forEach((item, index, arr) => {
                if (item.SendFactoryCode != TransTypeCode) {
                    Transtypenum = Transtypenum + 1;
                }
            })
            if (TransTypeCode > 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_28'));
                return false;
            }

            if (TransTypeCode == "OtherProcessTransfer") {
                //判断选择调拨的工艺路线是否相同
                var data = $scope.gridApi.selection.getSelectedRows();

                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("plan") + 'PL_TransfersRecord/IsidenticalProcess';

                //提交删除当前选择数据实体
                var postData = {
                    Entity: data
                };
                console.log("jpf" + JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (res) {
                    console.log("RemovePL_TransfersRecord----------------------------" + JSON.stringify(res));
                    if ((res) && (res.data.success)) {
                        // TODO: Put here the properties of the entity managed by the service
                        $state.go(rootstate + '.edit', { id: self.selectedItem.ID, selectedItem: data });

                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                        console.log('调拨确认出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_30'));
                    }
                }, function (error) {
                    console.log("RemovePL_TransfersRecord--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_25'));
                });

            } else {
                var title = commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_31');
                var text = commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_32');
                backendService.confirm(text, function () {
                    var data = $scope.gridApi.selection.getSelectedRows();

                    //commonService.getMesApiAddress() = '/sitSrvApi/'
                    var url = commonService.getMesApiAddress("plan") + 'PL_TransfersRecord/Allconfirm_TransfersRecord';

                    //提交删除当前选择数据实体
                    var postData = {
                        Entity: data
                    };
                    console.log("new postData------------------------------------" + JSON.stringify(postData));
                    commonService.callWebApiPost(url, postData).then(function (res) {
                        console.log("RemovePL_TransfersRecord----------------------------" + JSON.stringify(res));
                        if ((res) && (res.data.success)) {
                            var resultData = res.data.resultData;
                            //成功
                            commonService.showInfo(res.data.returnMsg);
                            initGridData();

                        } else {
                            //失败
                            commonService.showWarning(res.data.returnMsg);
                            console.log('调拨接收出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                            //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_30'));
                        }
                    }, function (error) {
                        console.log("RemovePL_TransfersRecord--error--------------------------" + JSON.stringify(error));
                        backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_25'));
                    });
                }, title);
            }

        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        function ReciveButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_34');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_35');
            backendService.confirm(text, function () {
                var data = $scope.gridApi.selection.getSelectedRows();
                //判断选择工单调拨的状态
                var statusNum = 0;
                var Factorynum = 0;
                data.forEach((item, index, arr) => {
                    if (item.TransStateCode != 0) {
                        statusNum = statusNum + 1;
                    }
                })
                if (statusNum > 0) {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_36'));
                    return false;
                }
                //判断发起工厂是否相同不同，不允许一起回退
                var Factory = data[0].SendFactoryCode;
                data.forEach((item, index, arr) => {
                    if (item.SendFactoryCode != Factory) {
                        Factorynum = Factorynum + 1;
                    }
                })
                if (statusNum > 0) {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_37'));
                    return false;
                }
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("plan") + 'PL_TransfersRecord/ReceivePL_TransfersRecord';

                //提交删除当前选择数据实体
                var postData = {
                    Entity: data
                };
                console.log("new postData------------------------------------" + JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (res) {
                    console.log("RemovePL_TransfersRecord----------------------------" + JSON.stringify(res));
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        initGridData();

                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                        console.log('调拨回退出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_30'));
                    }
                }, function (error) {
                    console.log("RemovePL_TransfersRecord--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_25'));
                });
            }, title);
        }
        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_39');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_40');
            backendService.confirm(text, function () {
                var data = $scope.gridApi.selection.getSelectedRows();
                //判断选择工单调拨的状态
                var statusNum = 0;
                var Factorynum = 0;
                data.forEach((item, index, arr) => {
                    if (item.TransStateCode != 0) {
                        statusNum = statusNum + 1;
                    }
                })
                if (statusNum > 0) {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_41'));
                    return false;
                }
                //判断发起工厂是否相同不同，不允许一起回退
                var Factory = data[0].SendFactoryCode;
                data.forEach((item, index, arr) => {
                    if (item.SendFactoryCode != Factory) {
                        Factorynum = Factorynum + 1;
                    }
                })
                if (statusNum > 0) {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_42'));
                    return false;
                }
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("plan") + 'PL_TransfersRecord/FallbackPL_TransfersRecord';

                //提交删除当前选择数据实体
                var postData = {
                    Entity: data
                };
                console.log("new postData------------------------------------" + JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (res) {
                    console.log("RemovePL_TransfersRecord----------------------------" + JSON.stringify(res));
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        initGridData();

                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                        console.log('调拨回退出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_30'));
                    }
                }, function (error) {
                    console.log("RemovePL_TransfersRecord--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_25'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_PlanApp_PL_TransfersRecord';
        var moduleStateUrl = 'Siemens.SimaticIT_PlanApp_PL_TransfersRecord';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/PL_TransfersRecord';

        var state = {
            name: moduleStateName + '_PL_TransfersRecord',
            url: '/' + moduleStateUrl + '_PL_TransfersRecord',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/PL_TransfersRecord-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.PL_TransfersRecord.JS.Tips_43'
            }
        };
        $stateProvider.state(state);
    }
}());
