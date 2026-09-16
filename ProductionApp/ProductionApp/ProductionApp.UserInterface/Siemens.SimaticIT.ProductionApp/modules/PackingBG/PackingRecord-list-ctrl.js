(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PackingBG').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PackingBG.PackingRecord.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.PackingBG.PackingRecord');

            init();
            initGridOptions();
            initGridOptionsDetail();
            initGridOptions2Detail();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_PackingBG_PackingRecord';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            initDictionary();

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            self.tabSwitch = "1";

            //子表明细
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            self.viewerOptions2 = {};
            self.viewerData2 = [];
            self.searchParams2 = {};

            //唛头
            self.selectedItem2Detail = null;
            self.isDetail2ButtonVisible = false;
            self.viewerOptions3 = {};
            self.viewerData3 = [];
            self.searchParams3 = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            //子明细
            self.add2ButtonHandler = add2ButtonHandler;//新增
            self.edit2ButtonHandler = edit2ButtonHandler;//编辑
            self.delete2ButtonHandler = delete2ButtonHandler;//删除

            self.tab1Click = tab1Click;
            self.tab2Click = tab2Click;
        }

        function tab1Click() {
            self.tabSwitch = "1";
            initGridDataDetail();
        }
        function tab2Click() {
            self.tabSwitch = "2";
            initGridData2Detail();
        }

        function initDictionary() {
            self.Factory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_1'), ResourceCode: "" }]
            };

            self.typePacking = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_1'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_1'), ItemValue: "" }]
            };

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.Factory.value = res.data.resultData[0];
                    }
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_1')
                    });
                    initGridData();
                }
            });
            commonService.getDataItemDuatil("PackingStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typePacking.options = res.data.resultData;
                    self.typePacking.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
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
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_3'),
                        width: 110
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_4'),
                        width: 150
                    },
                    {
                        field: 'CustomerPO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_5'),
                        width: 100
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_6'),
                        width: 100
                    },
                    {
                        field: 'POStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_7'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.POStatus==\'1\'"><span ng-cell-text>创建</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.POStatus==\'2\'"><span ng-cell-text>生产中</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.POStatus==\'3\'"><span ng-cell-text>已完成</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.POStatus==\'4\'"><span ng-cell-text>已入库</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.POStatus==\'5\'"><span ng-cell-text>已发货</span></div>'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_13'),
                        width: 110
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_14'),
                        width: 110
                    }, {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_15'),
                        width: 300
                    },
                    {
                        field: 'PaperBox',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_16'),
                        width: 110
                    }, {
                        field: 'DeliveryPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_17'),
                        width: 110
                    }, {
                        field: 'PiecePerPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_18'),
                        width: 110
                    }, {
                        field: 'DeliveryPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_19'),
                        width: 110
                    }, {
                        field: 'DeliveryWholePallet',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_20'),
                        width: 110
                    },
                    {
                        field: 'DeliveryStartPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_21'),
                        width: 110
                    },
                    {
                        field: 'LoadingDate',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_22'),
                        width: 110,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'
                    },
                    {
                        field: 'WorkQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_23'),
                        width: 110

                    },
                    {
                        field: 'PrintNum',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_24'),
                        width: 110
                    },
                    //{
                    //    field: 'Creator',
                    //    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_25'),
                    //    width: 200
                    //},
                    //{
                    //    field: 'CreateTime',
                    //    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_26'),
                    //    width: 160,
                    //    type: 'date',
                    //    cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    //},

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
                                if (self.tabSwitch == "1") {
                                    initGridDataDetail();
                                } else {
                                    initGridData2Detail();
                                }

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
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_27'));
                return;
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//创建时间
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.Factory.value.ResourceCode;
            self.searchParams.PackingStatus = self.typePacking.value.ItemValue;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_PackingRecord/PM_PackingRecordPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_28'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
            initGridDataDetail();
            initGridData2Detail();
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
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_29');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_30');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress("ProduceManage") = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_PackingRecord/RemovePM_PackingRecord';
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
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_33'));
                    }
                }, function (error) {
                    console.log("RemovePM_PackingRecord--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_28'));
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },

                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_4'),
                        width: 150
                    },

                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_6'),
                        width: 150
                    },
                    {
                        field: 'CustomerPO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_5'),
                        width: 150
                    },

                    {
                        field: 'TransferCard',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_34'),
                        width: 150
                    },
                    {
                        field: 'CardName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_35'),
                        width: 150
                    },
                    {
                        field: 'CardType',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_36'),
                        width: 150,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.CardType==\'1\'"><span ng-cell-text>正常</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.CardType==\'2\'"><span ng-cell-text>超产品</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.CardType==\'3\'"><span ng-cell-text>补料</span></div>'
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_40'),
                        width: 150
                    },
                    {
                        field: 'ProcessCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_41'),
                        width: 150
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
                                //console.log (self.selectedItemDetail);
                                //子表明细关联
                                //initGridDataDetail();
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
                sidx: 'CreateTime',//报工时间
                sord: 'desc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.PackingRecordId = self.selectedItem.Id;
            }
            else {
                self.gridOptionsDetail.data = [];
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_PackingBGTransferCard/PM_PackingBGTransferCardPageDataTableList';

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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_28'));
            });
        }

        //查询
        function search2ButtonHandler() {
            initGridDataDetail();
        }

        //新增
        function add2ButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addDetail', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
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
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_29');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_30');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress("ProduceManage") = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_PackingBGTransferCard/RemovePM_PackingBGTransferCard';
                //var url = 'http://localhost:49849/' + 'PM_PackingBGTransferCard' + '/RemovePM_PackingBGTransferCard'; 
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
                    console.log("RemovePM_PackingBGTransferCard----------------------------" + JSON.stringify(res));
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
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_33'));
                    }
                }, function (error) {
                    console.log("RemovePM_PackingBGTransferCard--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_28'));
                });
            }, title);
        }
        function initGridOptions2Detail() {
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_4'),
                        width: 150
                    },
                    {
                        field: 'CustomerPO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'Customer',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_42'),
                        width: 100
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_13'),
                        width: 150
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_43'),
                        width: 150
                    },
                    {
                        field: 'Quantity',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_44'),
                        width: 150
                    },
                    {
                        field: 'PackTransferCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_45'),
                        width: 200
                    },
                    {
                        field: 'Mark',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_46'),
                        width: 110
                    },
                    {
                        field: 'PrintStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_47'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.PrintStatus==\'1\'"><span ng-cell-text>未打印</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.PrintStatus==\'2\'"><span ng-cell-text>可打印</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.PrintStatus==\'3\'"><span ng-cell-text>已打印</span></div>'
                    },
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApiDetail = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData2Detail();
                    });
                    //行选中事件
                    $scope.gridApiDetail.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItem2Detail = row.entity;
                                self.isDetail2ButtonVisible = true;
                                //console.log (self.selectedItemDetail);
                                //子表明细关联
                                //initGridDataDetail();
                            } else {
                                self.selectedItem2Detail = null;
                                self.isDetail2ButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        function initGridData2Detail() {
            self.selectedItem2Detail = null;
            self.isDetailButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsDetail2.paginationPageSize,
                page: self.gridOptionsDetail2.paginationCurrentPage,
                sidx: 'CreateTime',//报工时间
                sord: 'desc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams3.PackingRecordId = self.selectedItem.Id;
            }
            else {
                self.gridOptionsDetail2.data = [];
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams3
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_PackingPrintMark/PM_PackingPrintMarkPageDataTableList';

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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_28'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_PackingBG';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_PackingBG';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PackingBG';

        var state = {
            name: moduleStateName + '_PackingRecord',
            url: '/' + moduleStateUrl + '_PackingRecord',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/PackingRecord-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PackingRecord.JS.Tips_51'
            }
        };
        $stateProvider.state(state);
    }
}());
