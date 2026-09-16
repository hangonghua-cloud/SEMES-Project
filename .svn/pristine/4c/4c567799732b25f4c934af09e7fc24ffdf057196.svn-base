/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 设备保养项目维护
*  2. 创建人员： 王坤
*  3. 创建日期： 2021-08-05
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintain.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintain');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();
            initGridData();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMaintain_EP_EquipmentMaintain';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.selectedDetailItem = null;
            self.selectedSpareItem = null;
            self.isButtonVisible = false;
            self.isMaintainButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.addStandardButtonHandler = addStandardButtonHandler;
            self.editButtonHandler = editButtonHandler;//编辑
            self.editStandardButtonHandler = editStandardButtonHandler;
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.deleteStandardButtonHandler = deleteStandardButtonHandler;
            self.searchButtonHandler = searchButtonHandler;//查询

            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();


            initDictionary();

            $rootScope.$on('to-editItem', function (event, editData) {
                // self.selectedItem.Factory = editData.Factory;
                // self.selectedItem.FactoryName = editData.FactoryName;
                self.selectedItem.EquipmentTaskId = editData.EquipmentTaskId;
                self.selectedItem.EquipmentTaskName = editData.EquipmentTaskName;
                self.selectedItem.Period = editData.Period;
                self.selectedItem.Remark = editData.Remark;
                self.selectedItem.IsUsed = editData.IsUsed;
                // self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].Factory = editData.Factory;
                // self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].FactoryName = editData.FactoryName;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].EquipmentTaskId = editData.EquipmentTaskId;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].EquipmentTaskName = editData.EquipmentTaskName;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].Period = editData.Period;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].Remark = editData.Remark;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].IsUsed = editData.IsUsed;
            });

            $rootScope.$on('to-addChildItem', function (event, addData) {
                initGridDetailData();
            });

            $rootScope.$on('to-editChildItem', function (event, editData) {
                self.selectedDetailItem.EquipmentMaintainId = editData.EquipmentMaintainId;
                self.selectedDetailItem.EquipmentMaintainName = editData.EquipmentMaintainName;
                self.selectedDetailItem.EquipmentMaintainStandard = editData.EquipmentMaintainStandard;
                self.selectedDetailItem.DataType = editData.DataType;
                self.selectedDetailItem.DataTypeName = editData.DataTypeName;
                self.gridStandardOptions.data.filter(item => item.Id == self.selectedDetailItem.Id)[0].EquipmentMaintainId = editData.EquipmentMaintainId;
                self.gridStandardOptions.data.filter(item => item.Id == self.selectedDetailItem.Id)[0].EquipmentMaintainName = editData.EquipmentMaintainName;
                self.gridStandardOptions.data.filter(item => item.Id == self.selectedDetailItem.Id)[0].EquipmentMaintainStandard = editData.EquipmentMaintainStandard;
                self.gridStandardOptions.data.filter(item => item.Id == self.selectedDetailItem.Id)[0].DataType = editData.DataType;
                self.gridStandardOptions.data.filter(item => item.Id == self.selectedDetailItem.Id)[0].DataTypeName = editData.DataTypeName;
            });
        }

        function initDictionary() {
            // self.typeFactory = {
            //     value: { ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_1'), ResourceCode: "" },
            //     options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_1'), ResourceCode: "" }]
            // };
            // commonService.getResourceExtendInfo({ LevelCode: "factory" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeFactory.options = res.data.resultData;
            //         if (res.data.resultData.length > 0) {
            //             self.typeFactory.value = res.data.resultData[0];
            //         }
            //         self.typeFactory.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_1')
            //         });
            //         initGridData();
            //     }
            // });

            //状态
            self.UsedConfig = {
                value: null,
                selectedOption: null,
                options: []
            };
            var url = commonService.getDataItemDuatil("EffectiveState").then(function (res) {
                self.UsedConfig.options = res.data.resultData;;
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
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    // {
                    //     field: 'FactoryName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_3'),
                    //     width: 120
                    // },
                    {
                        field: 'EquipmentTaskId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_4'),
                        width: 200
                    },
                    {
                        field: 'EquipmentTaskName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_5'),
                        width: 200
                    },
                    {
                        field: 'Period',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_6'),
                        width: 200
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_7'),
                        width: 200
                    },
                    {
                        field: 'IsUsed',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_8'),
                        width: 170,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.IsUsed==\' \'"><span ng-cell-text class="">失效</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.IsUsed==\'2\'"><span ng-cell-text class="0">失效</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.IsUsed==\'1\'"><span ng-cell-text class="1">有效</span></div>',
                    },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_9'),
                        width: 200
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_10'),
                        width: 200,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
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
                                initGridDetailData();
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                                self.gridStandardOptions.data = [];
                                self.selectedStandardItem = null;
                                self.isMaintainButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
            self.gridStandardOptions = {
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'EquipmentMaintainId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_11'),
                        width: 200
                    },
                    {
                        field: 'EquipmentMaintainName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_12'),
                        width: 200
                    },
                    {
                        field: 'EquipmentMaintainStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_13'),
                        width: 200
                    },
                    {
                        field: 'DataTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_14'),
                        width: 200
                    }/*,
                    {
                        field: 'Creator', 
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_9'),
                        width: 200
                    },
                    {
                        field: 'CreateTime', 
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_10'),
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
                                self.selectedDetailItem = row.entity;
                                self.isMaintainButtonVisible = true;
                                //console.log (self.selectedItem);
                            } else {
                                self.selectedDetailItem = null;
                                self.isMaintainButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //查询方法,数据绑定
        function initGridData() {

            // if (!self.typeFactory.value.ResourceCode) {
            //     commonService.showWarning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_15'));
            //     return;
            // }
            // self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            if (self.UsedConfig.selectedOption != null) {
                self.searchParams.IsUsed = self.UsedConfig.selectedOption.ItemValue;
            }
            else {
                self.searchParams.IsUsed = "";
            }
            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'EquipmentTaskId',//设备类别
                sord: 'desc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentMaintain/EP_EquipmentMaintainPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_16'));
            });
        }

        function initGridDetailData() {
            let Pagination = {
                rows: self.gridStandardOptions.paginationPageSize,
                page: self.gridStandardOptions.paginationCurrentPage,
                sidx: 'EquipmentMaintainId',//设备类别
                sord: 'desc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: {
                    "EquipmentTaskId": self.selectedItem.EquipmentTaskId
                }
            };
            console.log('queryParmeters-----' + JSON.stringify(queryParmeters));
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentMaintainDetail/EP_EquipmentMaintainDetailPageDataTableList';
            //var url = 'http://localhost:49849/' + 'EP_EquipmentMaintain' + '/EP_EquipmentMaintainPageDataTableList'; 
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                //console.log ('-self.Post_ResultData----------------------' + JSON.stringify(res));
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridStandardOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridStandardOptions.data = res.data.resultData.rows;
                } else {
                    self.gridStandardOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_16'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
            self.gridStandardOptions.data = [];
            self.isButtonVisible = false;
            self.isMaintainButtonVisible = false;
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

        //新增
        function addStandardButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addstandard', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //编辑
        function editStandardButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.editstandard', { id: self.selectedDetailItem.ID, selectItem: self.selectedItem, selectedItemDetail: self.selectedDetailItem });
        }
        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.editselect', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_17');
            var text = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_18');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentMaintain/RemoveEP_EquipmentMaintain';
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
                    console.log("RemoveEP_EquipmentMaintain----------------------------" + JSON.stringify(res));
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridData();
                        self.selectedItem = null;
                        self.isButtonVisible = false;
                        //self.gridSpareOptions.data = {};
                        self.gridStandardOptions.data = [];
                        self.selectedDetailItem = null;
                        //self.selectedSpareItem = null;
                        self.isMaintainButtonVisible = false;
                        //self.isSpareButtonVisiable = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                        console.log('删除数据出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_19'));
                    }
                }, function (error) {
                    console.log("RemoveEP_EquipmentMaintain--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_16'));
                });
            }, title);
        }

        //删除 事件
        function deleteStandardButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_17');
            var text = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_18');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentMaintainDetail/RemoveEP_EquipmentMaintainDetail';
                console.log("删除----------------" + url);
                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];
                self.UserCode = user.loginName;
                self.UserName = user.fullName;
                self.selectedDetailItem.ModifyBy = self.UserCode;

                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedDetailItem
                };
                console.log("new postData------------------------------------" + JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (res) {
                    console.log("RemoveEP_EquipmentMaintain----------------------------" + JSON.stringify(res));
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridDetailData();
                        self.selectedDetailItem = null;
                        self.isMaintainButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                        console.log('删除数据出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_19'));
                    }
                }, function (error) {
                    console.log("RemoveEP_EquipmentMaintain--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_16'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMaintain';
        var moduleStateUrl = 'Siemens.SimaticIT_EquipmentApp_EP_EquipmentMaintain';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentMaintain';

        var state = {
            name: moduleStateName + '_EP_EquipmentMaintain',
            url: '/' + moduleStateUrl + '_EP_EquipmentMaintain',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/EP_EquipmentMaintain-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainlistctrl.Tips_20'
            }
        };
        $stateProvider.state(state);
    }
}());
