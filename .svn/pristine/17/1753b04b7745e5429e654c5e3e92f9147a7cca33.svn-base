(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.Process').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.Process.ProcessOperation.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.Process.ProcessOperation');

            init();
            initGridOptions();
            initGridOptionsAttr();
            initGridOptionsDetail();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_Process_ProcessOperation';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //初始化数据字典
            initDictionary();
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

            self.isAttrButtonVisible = false;
            self.isAddAttrButtonVisible = false;
            self.selectedItemAttr = null;
            self.searchParams3 = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            self.workOrderSync = workOrderSync;//同步工单工艺路线

            //子明细
            self.add2ButtonHandler = add2ButtonHandler;//新增
            self.edit2ButtonHandler = edit2ButtonHandler;//编辑
            self.delete2ButtonHandler = delete2ButtonHandler;//删除

            //属性
            self.add3ButtonHandler = add3ButtonHandler;
            self.edit3ButtonHandler = edit3ButtonHandler;
            self.delete3ButtonHandler = delete3ButtonHandler;
            self.CopyButtonHandler = CopyButtonHandler;//复制bom
            self.stickButtonHandler = stickButtonHandler;//粘贴bom
        }
        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })
        $rootScope.$on("to-parentDetail", function (event, data) {
            initGridDataDetail();
        })
        $rootScope.$on("to-parentAttr", function (event, data) {
            initGridDataAttr();
        })

        function initDictionary() {

            //初始化 物料分类
            self.MaterialClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_1'), ItemValue: "" }]
            };

            //初始化 
            self.SmallClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_1'), ItemValue: "" }]
            };
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_1'), ResourceCode: "" }]
            }

            commonService.getDataItemDuatil("MaterialType").then(function (res) {
                if (res && res.data.success) {
                    self.MaterialClass.options = res.data.resultData;
                    self.MaterialClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.SmallClass.options = res.data.resultData;
                    self.SmallClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_1')
                    });
                    initGridData();
                }
            });
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'ProcessCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_5'),
                        width: 120
                    },
                    // {
                    //     field: 'MaterialClass',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_6'),
                    //     width: 100
                    // },
                    {
                        field: 'MaterialClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_6'),
                        width: 120
                    },
                    // {
                    //     field: 'SmallClass',
                    //     displayName: '',
                    //     width: 100
                    // },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_8'),
                        width: 200
                    },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_9'),
                        width: 200
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_10'),
                        width: 200,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'ModifyBy',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_11'),
                        width: 200
                    },
                    {
                        field: 'ModifyTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_12'),
                        width: 200,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM - dd HH: mm: ss"'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
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
                                //子表明细关联
                                initGridDataDetail();
                            } else {
                                self.gridOptionsDetail.data = [];
                                self.gridOptionsAttr.data = [];
                                self.isAddAttrButtonVisible = false;
                                self.isAttrButtonVisible = false;
                                self.isDetailButtonVisible = false;
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

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_13'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'FactoryCode',//工厂编码
                sord: 'asc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.MaterialClass = self.MaterialClass.value.ItemValue;
            self.searchParams.SmallClass = self.SmallClass.value.ItemValue;
            self.searchParams.ProcessType = "1";
            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'BS_Process/BS_ProcessPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_14'));
            });
        }

        //查询
        function searchButtonHandler() {
            self.gridOptionsDetail.data = [];
            self.gridOptionsAttr.data = [];
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
        //复制bom       
        function CopyButtonHandler() {
            debugger

            if (self.gridOptionsDetail.data != null) {
                self.copyparams = self.gridOptionsDetail.data;
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_15'));
            } else {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_16'));
                return;
            }
        }

        //粘贴bom
        function stickButtonHandler() {
            debugger
            if (self.selectedItem != null) {
                //关联字段
                self.copyparams.BOMId = self.selectedItem.Id;
            }
            else {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_17'));
                return;
            }

            var postData = {
                KeyValue: self.selectedItem.ProcessCode,
                Entity: self.copyparams
            };
            console.log("jpf123456789" + JSON.stringify(postData));
            var url = commonService.getMesApiAddress("material") + 'BS_Process/CopyBs_Process';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_18') });
            var req = commonService.callWebApiPost(url, postData).then(function (res) {
                if ((res) && (res.data.success)) {
                    var resultData = res.data.resultData;
                    //成功
                    commonService.showInfo(res.data.returnMsg);
                    busyIndicatorService.hide();
                    //重新刷新列表
                    initGridDataDetail();
                } else {
                    //失败
                    commonService.showWarning(res.data.returnMsg);
                    busyIndicatorService.hide();
                }


            }, function (error) {

                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_14'));
            });
        }



        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_19');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_20');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress("material") = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("material") + 'BS_Process/RemoveBS_Process';
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
                self.gridOptionsDetail.data = [];
                console.log("new postData------------------------------------" + JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (res) {
                    console.log("RemoveBS_Process----------------------------" + JSON.stringify(res));
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
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_21'));
                    }
                }, function (error) {
                    console.log("RemoveBS_Process--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_14'));
                });
            }, title);
        }

        //同步工单工艺路线 事件
        function workOrderSync() {
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_22');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_23');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("material") + 'BS_Process/WorkOrderProcessSync';
                var postData = {
                    processCode: self.selectedItem.ProcessCode
                };
                busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_24') });
                commonService.callWebApiPost(url, postData).then(function (res) {
                    busyIndicatorService.hide();
                    if ((res) && (res.data.success)) {
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
                    busyIndicatorService.hide();
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_14'));
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
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'SN',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_25'),
                        width: 120
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_3'),
                        width: 120
                    },

                    {
                        field: 'OperationCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_26'),
                        width: 120
                    },
                    {
                        field: 'OperationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_27'),
                        width: 120
                    },
                    {
                        field: 'CuringCycle',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_28'),
                        width: 160
                    },
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApiDetail = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataDetail();
                    });
                    //行选中事件
                    $scope.gridApiDetail.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailButtonVisible = true;
                                self.isAddAttrButtonVisible = true;
                                //console.log (self.selectedItemDetail);
                                //子表明细关联
                                //initGridDataDetail();
                                initGridDataAttr();
                            } else {
                                self.gridOptionsAttr.data = [];
                                self.selectedItemDetail = null;
                                self.isDetailButtonVisible = false;
                                self.isAddAttrButtonVisible = false;
                                self.isAttrButtonVisible = false;
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
                sidx: 'SN',//顺序号
                sord: 'asc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.ProcessCode = self.selectedItem.ProcessCode;
            }
            else {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_29'), commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_30'));
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress("material") + 'BS_ProcessOfOperations/BS_ProcessOfOperationsPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_14'));
            });
        }

        //查询
        function search2ButtonHandler() {
            initGridDataDetail();
        }

        //新增
        function add2ButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addDetail', { selectedItem: self.selectedItem });
        }

        //编辑
        function edit2ButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.editDetail', { id: self.selectedItemDetail.ID, selectedItem: self.selectedItemDetail });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.selectDetail', { id: self.selectedItemDetail.ID, selectedItem: self.selectedItemDetail });
        }

        //删除 事件
        function delete2ButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_19');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_20');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress("material") = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("material") + 'BS_ProcessOfOperations/RemoveBS_ProcessOfOperations';
                //var url = 'http://localhost:49849/' + 'BS_ProcessOfOperations' + '/RemoveBS_ProcessOfOperations'; 
                console.log("删除----------------" + url);
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
                console.log("new postData------------------------------------" + JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (res) {
                    console.log("RemoveBS_ProcessOfOperations----------------------------" + JSON.stringify(res));
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
                        console.log('删除数据出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_21'));
                    }
                }, function (error) {
                    console.log("RemoveBS_ProcessOfOperations--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_14'));
                });
            }, title);
        }

        //属性
        function initGridOptionsAttr() {
            self.gridOptionsAttr = {
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
                    //     name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                    //         '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    // },
                    {
                        field: 'AttrCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_31'),
                        width: 120
                    },
                    {
                        field: 'AttrName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_32'),
                        width: 120
                    },
                    {
                        field: 'SortCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_25'),
                        width: 160
                    },
                    {
                        field: 'AttrValue',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_33'),
                        width: 160
                    },

                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApiAttr = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataAttr();
                    });
                    //行选中事件
                    $scope.gridApiAttr.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemAttr = row.entity;
                                self.isAttrButtonVisible = true;
                                //console.log (self.selectedItemDetail);
                                //子表明细关联
                                //initGridDataDetail();
                            } else {
                                self.selectedItemAttr = null;
                                self.isAttrButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        function initGridDataAttr() {
            //console.log(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_34'));
            self.selectedItemAttr = null;
            self.isAttrButtonVisible = false;

            if (self.selectedItem != null) {
                //关联字段
                self.searchParams3.OperationsId = self.selectedItemDetail.Id;
            }

            let queryParmeters = {
                queryJson: self.searchParams3
            };

            var url = commonService.getMesApiAddress("material") + 'BS_ProcessOfOperations/BS_ProcessOfOperationsAttrPage';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsAttr.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsAttr.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsAttr.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_14'));
            });
        }

        function add3ButtonHandler() {
            if (!self.selectedItem.FactoryCode || !self.selectedItemDetail.OperationCode) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_35'), commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_14'));
                return false;
            }
            var params = {
                FactoryCode: self.selectedItem.FactoryCode,
                Id: self.selectedItemDetail.Id,
                Process: self.selectedItemDetail.OperationCode
            }
            $state.go(rootstate + '.addAttr', { selectedItem: params });

        }
        function edit3ButtonHandler() {

            $state.go(rootstate + '.editAttr', { id: self.selectedItemAttr.Id, selectedItem: self.selectedItemAttr });
        }
        function delete3ButtonHandler() {
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_19');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_20');
            backendService.confirm(text, function () {

                var url = commonService.getMesApiAddress("material") + 'BS_ProcessOfOperations/RemoveProcessOfOperationsAttr?keyValue=' + self.selectedItemAttr.Id;

                commonService.callWebApiGet(url, null).then(function (res) {
                    if ((res) && (res.data.success)) {
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridDataAttr();
                        self.selectedItemAttr = null;
                        self.isAttrButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);

                    }
                }, function (error) {

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_14'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_Process';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_Process';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/Process';

        var state = {
            name: moduleStateName + '_ProcessOperation',
            url: '/' + moduleStateUrl + '_ProcessOperation',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ProcessOperation-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.Process.ProcessOperationlistctrl.Tips_36'
            }
        };
        $stateProvider.state(state);
    }
}());
