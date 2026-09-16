/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 设备保养项目详情
*  2. 创建人员： 王坤
*  3. 创建日期： 2021-08-05
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTask.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTask');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMaintainTask_EP_EquipmentMaintainTask';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.selectedItemDetail = null;
            self.selectedSpareItem = null;
            self.isButtonVisible = false;
            self.isResultButtonVisible1 = false;
            self.isResultButtonVisible2 = false;
            self.isSpareButtonVisible = false;
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

            self.addResultButtonHandler = addResultButtonHandler;//新增
            self.editResultButtonHandler = editResultButtonHandler;//编辑
            self.deleteResultButtonHandler = deleteResultButtonHandler;//删除

            self.addSpareButtonHandler = addSpareButtonHandler;//新增
            self.deleteSpareButtonHandler = deleteSpareButtonHandler;//删除

            initDictionary();

            $rootScope.$on('to-editItem', function (event, editData) {
                self.selectedItem.PlanDate = editData.PlanDate;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].PlanDate = editData.PlanDate;
            });

            $rootScope.$on('to-addChildItem', function (event, addData) {
                /*initGridData();
                initGridDataDetail();*/
                var main = {};
                //var child = addData.child;
                if (addData.main == undefined)
                    main = addData;
                else
                    main = addData.main;
                self.selectedItem.ActiveDate = main.ActiveDate;
                self.selectedItem.MaintainPerson = main.MaintainPerson;
                self.selectedItem.MaintainPersonName = main.MaintainPersonName
                self.selectedItem.MaintenanceStatus = main.MaintenanceStatus;
                self.selectedItem.MaintenanceStatusName = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_1');
                self.selectedItem.Remark = main.Remark;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].PlanDate = main.PlanDate;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].MaintainPerson = main.MaintainPerson;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].MaintainPersonName = main.MaintainPersonName;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].MaintenanceStatus = main.MaintenanceStatus;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].MaintenanceStatusName = main.MaintenanceStatusName;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].Remark = main.Remark;

                self.isResultButtonVisible2 = false;

                initGridDataDetail();
                //self.selectedItemDetail.EquipmentMaintainResultValue = child.EquipmentMaintainResultValue;
                //self.gridItemOptions.data.filter(item=>item.Id == self.selectedItemDetail.Id)[0].EquipmentMaintainResultValue = child.EquipmentMaintainResultValue;
            });

            $rootScope.$on('to-addChildSpare', function (event, addData) {
                initGridSpareDatal();
            });

            $rootScope.$on('to-editChildItem', function (event, editData) {
                //self.selectedItemDetail.EquipmentMaintainId = editData.EquipmentMaintainId;
                self.selectedItemDetail.EquipmentMaintainName = editData.EquipmentMaintainName;
                self.selectedItemDetail.EquipmentMaintainStandard = editData.EquipmentMaintainStandard;
                self.selectedItemDetail.EquipmentMaintainResultValue = editData.EquipmentMaintainResultValue;
                //self.selectedItemDetail.DataTypeName = editData.DataTypeName;
                //self.gridItemOptions.data.filter(item=>item.Id == self.selectedItemDetail.Id)[0].EquipmentMaintainId = editData.EquipmentMaintainId;
                self.gridItemOptions.data.filter(item => item.Id == self.selectedItemDetail.Id)[0].EquipmentMaintainName = editData.EquipmentMaintainName;
                self.gridItemOptions.data.filter(item => item.Id == self.selectedItemDetail.Id)[0].EquipmentMaintainStandard = editData.EquipmentMaintainStandard;
                self.gridItemOptions.data.filter(item => item.Id == self.selectedItemDetail.Id)[0].EquipmentMaintainResultValue = editData.EquipmentMaintainResultValue;
                //self.gridItemOptions.data.filter(item=>item.Id == self.selectedItemDetail.Id)[0].DataTypeName = editData.DataTypeName;
            });

            /*$rootScope.$on('to-editChildSpare', function (event, editData) {
                self.selectedSpareItem.SparePartsId = editData.SparePartsId;
                self.selectedSpareItem.SparePartsName = editData.SparePartsName;
                self.selectedSpareItem.SpecificationsModels = editData.SpecificationsModels;
                self.selectedSpareItem.Num = editData.Num;
                self.selectedSpareItem.UnitName = editData.UnitName;
                self.gridSpareOptions.data.filter(item=>item.Id == self.selectedSpareItem.Id)[0].SparePartsId = editData.SparePartsId;
                self.gridSpareOptions.data.filter(item=>item.Id == self.selectedSpareItem.Id)[0].SparePartsName = editData.SparePartsName;
                self.gridSpareOptions.data.filter(item=>item.Id == self.selectedSpareItem.Id)[0].SpecificationsModels = editData.SpecificationsModels;
                self.gridSpareOptions.data.filter(item=>item.Id == self.selectedSpareItem.Id)[0].Num = editData.Num;
                self.gridSpareOptions.data.filter(item=>item.Id == self.selectedSpareItem.Id)[0].UnitName = editData.UnitName;
            });*/
        }

        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_2')
                    });
                    initGridData();
                }
            });
            //保养状态
            self.StatusConfig = {
                value: null,
                selectedOption: null,
                options: []
            };
            var url = commonService.getDataItemDuatil("menuMaintenanceStatus").then(function (res) {
                self.StatusConfig.options = res.data.resultData;;
            });

            //保养任务
            self.TaskConfig = {
                value: null,
                selectedOption: null,
                options: []
            };
            var url = commonService.getMesApiAddress("equipment") + "EP_EquipmentMaintain/GetEP_EquipmentMaintainList?checkType=";
            commonService.callWebApiGet(url, null).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    self.TaskConfig.options = jsonData;
                    self.TaskConfig.selectedOption = { EquipmentTaskId: "", EquipmentTaskName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_2') };
                } else {
                    console.log(self.ParentResourceCtrl.options);
                }
                //self.SecondValue = jsonData[0];
                self.TaskConfig.options.splice(0, 0, { EquipmentTaskId: "", EquipmentTaskName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_2') })
            }, function (error) {

                console.log(error);
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_3'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'EquipmentId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'EquipmentName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'EquipmentMaintainTaskId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_7'),
                        width: 120,
                        visible: true
                    },
                    {
                        field: 'EquipmentMaintainTaskName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_8'),
                        width: 150,
                        visible: true
                    },
                    {
                        field: 'MaintenanceStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_9'),
                        width: 120,
                        visible: true
                    },
                    {
                        field: 'MaintainPersonName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'PlanDate',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_11'),
                        width: 160,
                        visible: true,
                        //type: 'date',
                        //cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'ActiveDate',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_12'),
                        width: 160,
                        //type: 'date',
                        //cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_13'),
                        width: 200
                    },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_14'),
                        width: 120
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_15'),
                        width: 150,
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
                                if (self.selectedItem.MaintainPersonName == null || self.selectedItem.MaintainPersonName == "") {
                                    self.isResultButtonVisible1 = true;
                                    self.isResultButtonVisible2 = false;
                                }
                                else {
                                    self.isResultButtonVisible1 = false;
                                    self.isResultButtonVisible2 = false;
                                }
                                initGridDataDetail();
                                initGridSpareDatal();
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;

                                self.gridItemOptions.data = [];
                                self.isResultButtonVisible1 = false;
                                self.isResultButtonVisible2 = false;

                                self.gridSpareOptions.data = [];
                                self.isSpareButtonVisible = false;

                            }
                        }
                    });
                },
                data: []
            }
            self.gridItemOptions = {
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_3'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'EquipmentMaintainName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_16'),
                        width: 200
                    },
                    {
                        field: 'EquipmentMaintainStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_17'),
                        width: 200
                    },
                    {
                        field: 'EquipmentMaintainResultValue',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_18'),
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
                                if (self.selectedItem.MaintainPersonName == null || self.selectedItem.MaintainPersonName == "") {
                                    self.isResultButtonVisible1 = true;
                                    self.isResultButtonVisible2 = false;
                                }
                                else {
                                    self.isResultButtonVisible1 = false;
                                    self.isResultButtonVisible2 = true;
                                }
                            } else {
                                self.selectedItemDetail = null;
                                self.isResultButtonVisible1 = false;
                                self.isResultButtonVisible2 = false;
                            }
                        }
                    });
                },
                data: []
            }

            self.gridSpareOptions = {
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_3'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'SparePartsId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_19'),
                        width: 200
                    },
                    {
                        field: 'SparePartsName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_20'),
                        width: 200
                    },
                    {
                        field: 'SpecificationsModels',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_21'),
                        width: 200
                    },
                    {
                        field: 'Num',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_22'),
                        width: 200
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_23'),
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
                                self.selectedSpareItem = row.entity;
                                self.isSpareButtonVisible = true;
                            } else {
                                self.selectedSpareItem = null;
                                self.isSpareButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //查询方法,数据绑定
        function initGridData() {
            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//保养工单号
                sord: 'desc'
            };

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_24'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            if (self.StatusConfig.selectedOption != null) {
                self.searchParams.MaintenanceStatus = self.StatusConfig.selectedOption.ItemValue;
            }
            else {
                self.searchParams.MaintenanceStatus = "";
            }
            if (self.TaskConfig.selectedOption != null) {
                self.searchParams.EquipmentMaintainTaskId = self.TaskConfig.selectedOption.EquipmentTaskId;
            }
            else {
                self.searchParams.EquipmentMaintainTaskId = "";
            }
            if (self.PlanStartDate != null && self.PlanStartDate != "") {
                self.searchParams.PlanStartDate = moment(self.PlanStartDate).format("YYYY-MM-DD");
            }
            else {
                self.searchParams.PlanStartDate = "";
            }
            if (self.PlanEndDate != null && self.PlanEndDate != "") {
                self.searchParams.PlanEndDate = moment(self.PlanEndDate).format("YYYY-MM-DD");
            }
            else {
                self.searchParams.PlanEndDate = "";
            }
            if (self.ActiveStartDate != null && self.ActiveStartDate != "") {
                self.searchParams.ActiveStartDate = moment(self.ActiveStartDate).format("YYYY-MM-DD");
            }
            else {
                self.searchParams.ActiveStartDate = "";
            }
            if (self.ActiveEndDate != null && self.ActiveEndDate != "") {
                self.searchParams.ActiveEndDate = moment(self.ActiveEndDate).format("YYYY-MM-DD");
            }
            else {
                self.searchParams.ActiveEndDate = "";
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentMaintainTask/EP_EquipmentMaintainTaskPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_25'));
            });
        }
        function initGridDataDetail() {
            let Pagination = {
                rows: self.gridItemOptions.paginationPageSize,
                page: self.gridItemOptions.paginationCurrentPage,
                sidx: 'EquipmentMaintainId',//工厂
                sord: 'adc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: { "Id": self.selectedItem.Id, "TaskId": self.selectedItem.EquipmentMaintainTaskId }
            };
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentMaintainTask/GetMaintenceResultPageList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridItemOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridItemOptions.data = res.data.resultData.rows;
                } else {
                    self.gridItemOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_25'));
            });
        }
        function initGridSpareDatal() {
            //self.selectedItem = null;
            let Pagination = {
                rows: self.gridSpareOptions.paginationPageSize,
                page: self.gridSpareOptions.paginationCurrentPage,
                sidx: 'CreateTime',//设备编码
                sord: 'desc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: {
                    "RepairId": self.selectedItem.Id
                }
            };
            var url = commonService.getMesApiAddress('equipment') + 'EP_EquipmentSpareParts/EP_EquipmentSparePartsPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridSpareOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridSpareOptions.data = res.data.resultData.rows;
                } else {
                    self.gridSpareOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_25'));
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

        //编辑
        function addResultButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.addresult', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //编辑
        function addSpareButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            if (self.selectedItem.MaintenanceStatus == "1")
                $state.go(rootstate + '.addspare', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
            else
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_26'));
        }

        //编辑
        function editResultButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.editresult', { id: self.selectedItem.Id, selectedItem: self.selectedItem, selectedItemDetail: self.selectedItemDetail });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_27');
            var text = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_28');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentMaintainTask/RemoveEP_EquipmentMaintainTask';
                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];
                self.UserCode = user.loginName;
                self.UserName = user.fullName;
                self.selectedItem.ModifyBy = self.UserCode;
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
                        self.isResultButtonVisible1 = false;
                        self.isResultButtonVisible2 = false;
                        self.isSpareButtonVisible = false;
                        self.gridSpareOptions.data = [];
                        self.gridItemOptions.data = [];
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg)
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_25'));
                });
            }, title);
        }

        //删除 事件
        function deleteResultButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_27');
            var text = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_28');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentMaintainResult/RemoveBatchEP_EquipmentMaintainResult';
                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];
                self.UserCode = user.loginName;
                self.UserName = user.fullName;
                self.selectedItem.ModifyBy = self.UserCode;
                //提交删除当前选择数据实体
                var postData = {
                    KeyValue: self.selectedItem.Id,   //self.selectedItemDetail
                    UpdateUser: self.UserCode
                };
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridDataDetail();
                        self.selectedItemDetail = null;
                        self.isResultButtonVisible1 = false;
                        self.isResultButtonVisible2 = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);

                    }
                }, function (error) {

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_25'));
                });
            }, title);
        }

        //删除 事件
        function deleteSpareButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_27');
            var text = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_28');
            backendService.confirm(text, function () {

                var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentSpareParts/RemoveEP_EquipmentSpareParts';

                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];
                self.UserCode = user.loginName;
                self.UserName = user.fullName;
                self.selectedSpareItem.ModifyBy = self.UserCode;
                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedSpareItem
                };

                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridSpareDatal();
                        self.selectedSpareItem = null;
                        self.isSpareButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);

                    }
                }, function (error) {

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_25'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMaintainTask';
        var moduleStateUrl = 'Siemens.SimaticIT_EquipmentApp_EP_EquipmentMaintainTask';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentMaintainTask';

        var state = {
            name: moduleStateName + '_EP_EquipmentMaintainTask',
            url: '/' + moduleStateUrl + '_EP_EquipmentMaintainTask',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/EP_EquipmentMaintainTask-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTasklistctrl.Tips_29'
            }
        };
        $stateProvider.state(state);
    }
}());
