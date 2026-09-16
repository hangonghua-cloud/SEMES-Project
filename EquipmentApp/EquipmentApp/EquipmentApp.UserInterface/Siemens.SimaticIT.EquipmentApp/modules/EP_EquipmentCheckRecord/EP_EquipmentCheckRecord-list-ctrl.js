/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 设备点检记录
*  2. 创建人员： 王坤
*  3. 创建日期： 2021-08-05
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecord.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecord');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentCheckRecord_EP_EquipmentCheckRecord';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible1 = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询

            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
            initDictionary();
        }

        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_1')
                    });
                    initGridData();
                }
            });

            //点检结论
            self.ConclusionConfig = {
                value: null,
                selectedOption: { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_1') },
                options: [
                    { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_1') },
                    { ItemCode: "1", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_2') },
                    { ItemCode: "2", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_3') },
                ]
            };
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_4'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_5'),
                        width: 200
                    },
                    {
                        field: 'EquipmentId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_6'),
                        width: 200
                    },
                    {
                        field: 'EquipmentName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_7'),
                        width: 200
                    },
                    {
                        field: 'CheckTaskId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_8'),
                        width: 200,
                        visible: false
                    },
                    // {
                    //     field: 'CheckTaskName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_9'),
                    //     width: 200
                    // },
                    {
                        field: 'CheckConclusionName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_10'),
                        width: 200
                    },
                    {
                        field: 'TypeInTeam',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_11'),
                        width: 200,
                        visible: false
                    },
                    {
                        field: 'TypeInPerson',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_12'),
                        width: 200
                    },
                    {
                        field: 'TypeInTime',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_13'),
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
                                self.isButtonVisible1 = true;
                                initGridDataDetail();
                                //console.log (self.selectedItem);
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible1 = false;
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_4'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'CheckTaskName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_9'),
                        width: 150
                    },
                    {
                        field: 'CheckItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_14'),
                        width: 200
                    },
                    {
                        field: 'CheckItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_15'),
                        width: 200
                    },
                    {
                        field: 'CheckResultValue',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_16'),
                        width: 200
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
                                //self.isButtonVisible2 = true;
                                //console.log (self.selectedItem);
                            } else {
                                self.selectedItemDetail = null;
                                //self.isButtonVisible2 = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //查询方法,数据绑定
        function initGridData() {
            // debugger
            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_17'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            if (self.ConclusionConfig.selectedOption != null) {
                self.searchParams.CheckConclusion = self.ConclusionConfig.selectedOption.ItemCode;
            }
            else {
                self.searchParams.CheckConclusion = "";
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
                sidx: 'TypeInTime',//设备编码
                sord: 'desc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentCheckRecord/EP_EquipmentCheckRecordPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据
                    // debugger
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_18'));
            });
        }

        //查询方法,数据绑定
        function initGridDataDetail() {
            self.isButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsItem.paginationPageSize,
                page: self.gridOptionsItem.paginationCurrentPage,
                sidx: 'CreateTime',//设备编码
                sord: 'desc'
            };
            // debugger
            let queryParmeters = {
                pagination: Pagination,
                queryJson: { "ParentId": self.selectedItem.Id }
            };
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentCheckResult/EP_EquipmentCheckResultPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsItem.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsItem.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_18'));
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
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_19');
            var text = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_20');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentCheckRecord/RemoveEP_EquipmentCheckRecord';
                console.log("删除----------------" + url);
                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];
                self.UserCode = user.loginName;
                self.UserName = user.fullName;
                self.selectedItem.ModifyBy = self.UserCode;
                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItem
                };
                console.log("new postData------------------------------------" + JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (res) {
                    console.log("RemoveEP_EquipmentCheckRecord----------------------------" + JSON.stringify(res));
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridData();
                        self.selectedItem = null;
                        self.isButtonVisible1 = false;
                        self.gridOptionsItem.data = [];
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                        console.log('删除数据出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_21'));
                    }
                }, function (error) {
                    console.log("RemoveEP_EquipmentCheckRecord--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_18'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentCheckRecord';
        var moduleStateUrl = 'Siemens.SimaticIT_EquipmentApp_EP_EquipmentCheckRecord';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentCheckRecord';

        var state = {
            name: moduleStateName + '_EP_EquipmentCheckRecord',
            url: '/' + moduleStateUrl + '_EP_EquipmentCheckRecord',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/EP_EquipmentCheckRecord-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordlistctrl.Tips_22'
            }
        };
        $stateProvider.state(state);
    }
}());
