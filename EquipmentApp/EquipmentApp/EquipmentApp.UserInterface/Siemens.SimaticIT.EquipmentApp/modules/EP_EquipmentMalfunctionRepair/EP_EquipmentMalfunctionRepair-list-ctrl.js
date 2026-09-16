/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 设备故障报修
*  2. 创建人员： 王坤
*  3. 创建日期： 2021-08-05
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepair.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepair');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMalfunctionRepair_EP_EquipmentMalfunctionRepair';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.selectedItemDetail = null;
            self.isButtonVisible1 = false;
            self.isButtonVisible2 = false;
            self.isEdit = false;
            self.isNew = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            self.addSpareButtonHandler = addSpareButtonHandler;
            self.editSpareButtonHandler = editSpareButtonHandler;
            self.deleteSpareButtonHandler = deleteSpareButtonHandler;
            self.addRepairButtonHandler = addRepairButtonHandler;
            self.editRepairButtonHandler = editRepairButtonHandler;

            self.typeFactoryChange = typeFactoryChange;

            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
            $rootScope.$on('to-addparent', function (event, OnData) {

                initGridDataDetail();
            })

            initDictionary();
            GetTypeDictionary();

            $rootScope.$on('to-editItem', function (event, editData) {
                self.selectedItem.EquipmentId = editData.EquipmentId;
                self.selectedItem.EquipmentName = editData.EquipmentName;
                self.selectedItem.RepairingType = editData.RepairingType;
                self.selectedItem.RepairingTypeName = editData.RepairingTypeName;
                self.selectedItem.RepairingPerson = editData.RepairingPerson;
                self.selectedItem.RepairingPersonName = editData.RepairingPersonName;
                self.selectedItem.MalfunctionDescription = editData.MalfunctionDescription;
                //self.selectedItem.RepairingPersonName3 = editData.RepairingPersonName3;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].EquipmentId = editData.EquipmentId;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].EquipmentName = editData.EquipmentName;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].RepairingType = editData.RepairingType;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].RepairingTypeName = editData.RepairingTypeName;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].RepairingPerson = editData.RepairingPerson;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].RepairingPersonName = editData.RepairingPersonName;
                self.gridOptions.data.filter(item => item.Id == self.selectedItem.Id)[0].MalfunctionDescription = editData.MalfunctionDescription;
            });

            $rootScope.$on('to-addChildItem', function (event, addData) {
                initGridDataDetail();
            });

            $rootScope.$on('to-editChildItem', function (event, editData) {
                self.selectedItemDetail.SparePartsId = editData.SparePartsId;
                self.selectedItemDetail.SparePartsName = editData.SparePartsName;
                self.selectedItemDetail.SpecificationsModels = editData.SpecificationsModels;
                self.selectedItemDetail.Num = editData.Num;
                self.selectedItemDetail.UnitName = editData.UnitName;
                self.gridOptionsItem.data.filter(item => item.Id == self.selectedItemDetail.Id)[0].SparePartsId = editData.SparePartsId;
                self.gridOptionsItem.data.filter(item => item.Id == self.selectedItemDetail.Id)[0].SparePartsName = editData.SparePartsName;
                self.gridOptionsItem.data.filter(item => item.Id == self.selectedItemDetail.Id)[0].SpecificationsModels = editData.SpecificationsModels;
                self.gridOptionsItem.data.filter(item => item.Id == self.selectedItemDetail.Id)[0].Num = editData.Num;
                self.gridOptionsItem.data.filter(item => item.Id == self.selectedItemDetail.Id)[0].UnitName = editData.UnitName;
            });
        }

        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_1')
                    });
                    initGridData();
                }
            });

            //车间
            self.WorkshopConfig = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_1'), ResourceCode: "" }]
            };

            //报修类别
            self.TypeConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            //工单状态
            self.StatusConfig = {
                value: null,
                selectedOption: { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_1') },
                options: [
                    { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_1') },
                    { ItemCode: "1", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_2') },
                    { ItemCode: "2", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_3') }
                ]
            };

        }
        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.WorkshopConfig.options = res.data.resultData;
                        self.WorkshopConfig.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_1')
                        });
                    }
                });
            } else {
                self.WorkshopConfig = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_1'), ResourceCode: "" }]
                };
            }
        }


        // //车间
        // function GetWorkshopList() {
        //     var postData = {
        //         "queryJson": {
        //             "ResourceCode": "",
        //             "ResourceName": "",
        //             "ModelLevel": "Workshop"
        //         }
        //     };
        //     var url = commonService.getMesApiAddress("factory") + "LevelManage/BsModelWithResource/GetListJson";
        //     commonService.callWebApiPost(url, postData).then(function (data) {
        //         if ((data) && (data.data.success)) {
        //             var jsonData = data.data.resultData;
        //             self.WorkshopConfig.options = jsonData;
        //         } else {
        //         }
        //         //self.SecondValue = jsonData[0];
        //         self.WorkshopConfig.options.splice(0, 0, { ResourceCode: "", ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_1') })
        //     }, function (error) {
        //     });
        // }

        //报修类别
        function GetTypeDictionary() {
            var url = commonService.getDataItemDuatil("RepairsCategory").then(function (res) {
                self.TypeConfig.options = res.data.resultData;;
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_4'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_5'),
                        width: 110
                    },
                    {
                        field: 'WorkshopName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_6'),
                        width: 110
                    },
                    {
                        field: 'EquipmentId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_7'),
                        width: 110
                    },
                    {
                        field: 'EquipmentName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'RepairingType',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_9'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.RepairingType==\'1\'"><span ng-cell-text>电器故障</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.RepairingType==\'2\'"><span ng-cell-text>机器故障</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.RepairingType==\'3\'"><span ng-cell-text>日常维修</span></div>'
                    },
                    {
                        field: 'RepairingStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_10'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.RepairingStatus==\'1\'"><span ng-cell-text>未完成</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.RepairingStatus==\'2\'"><span ng-cell-text>已完成</span></div>'
                    },
                    {
                        field: 'MalfunctionDescription',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_11'),
                        width: 200
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_12'),
                        width: 110
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_13'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'RepairingContent',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_14'),
                        width: 200
                    },
                    {
                        field: 'TimeLength',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_15'),
                        width: 140
                    },
                    {
                        field: 'RepairingPersonName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_16'),
                        width: 110
                    },
                    {
                        field: 'FinishTime',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_17'),
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
                                self.isButtonVisible1 = true;
                                if (self.selectedItem.RepairingId != null) {
                                    self.isEdit = true;
                                    self.isNew = false;
                                }
                                else {
                                    self.isEdit = false;
                                    self.isNew = true;
                                }
                                initGridDataDetail();

                            } else {
                                self.selectedItem = null;
                                self.selectItemDetail = null;
                                self.isButtonVisible1 = false;
                                self.isEdit = false;
                                self.isNew = false;
                                self.gridOptionsItem.data = {};
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_4'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'SparePartsId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_18'),
                        width: 200
                    },
                    {
                        field: 'SparePartsName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_19'),
                        width: 200
                    },
                    {
                        field: 'SpecificationsModels',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_20'),
                        width: 200
                    },
                    {
                        field: 'Num',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_21'),
                        width: 200
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_22'),
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
                                self.isButtonVisible2 = true;
                            } else {
                                self.selectedItemDetail = null;
                                self.isButtonVisible2 = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //查询方法,数据绑定
        function initGridData() {
            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_23'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.Workshop = self.WorkshopConfig.value.ResourceCode;
            if (self.TypeConfig.selectedOption != null) {
                self.searchParams.RepairingType = self.TypeConfig.selectedOption.ItemValue;
            }
            else {
                self.searchParams.RepairingType = "";
            }
            if (self.StatusConfig.selectedOption != null) {
                self.searchParams.Status = self.StatusConfig.selectedOption.ItemCode;
            }
            else {
                self.searchParams.Status = "";
            }
            if (self.RepairStartDate != null && self.RepairStartDate != "") {
                self.searchParams.RepairStartTime = moment(self.RepairStartDate).format("YYYY-MM-DD");
            }
            else {
                self.searchParams.RepairStartTime = "";
            }
            if (self.RepairEndDate != null && self.RepairEndDate != "") {
                self.searchParams.RepairEndTime = moment(self.RepairEndDate).format("YYYY-MM-DD");
            }
            else {
                self.searchParams.RepairEndTime = "";
            }
            if (self.FinishStartDate != null && self.FinishStartDate != "") {
                self.searchParams.FinishStartTime = moment(self.FinishStartDate).format("YYYY-MM-DD");
            }
            else {
                self.searchParams.FinishStartTime = "";
            }
            if (self.FinishEndDate != null && self.FinishEndDate != "") {
                self.searchParams.FinishEndTime = moment(self.FinishEndDate).format("YYYY-MM-DD");
            }
            else {
                self.searchParams.FinishEndTime = "";
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//设备编码
                sord: 'desc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress('equipment') + 'EP_EquipmentMalfunctionRepair/EP_EquipmentMalfunctionRepairPageDataTableList';

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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_24'));
            });
        }

        //查询方法,数据绑定
        function initGridDataDetail() {
            //self.selectedItem = null;
            self.isButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsItem.paginationPageSize,
                page: self.gridOptionsItem.paginationCurrentPage,
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
                    self.gridOptionsItem.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsItem.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_24'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
            self.isButtonVisible1 = false;
            self.isButtonVisible2 = false;
            self.isNew = false;
            self.isEdit = false;
            self.gridOptionsItem.data = [];
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
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_25');
            var text = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_26');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentMalfunctionRepair/RemoveEP_EquipmentMalfunctionRepair';
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
                        self.isButtonVisible1 = false;
                        self.isButtonVisible2 = false;
                        self.isNew = false;
                        self.isEdit = false;
                        self.gridOptionsItem.data = [];
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_24'));
                });
            }, title);
        }

        //新增
        function addRepairButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addrepair', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //编辑
        function editRepairButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.editrepair', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //新增
        function addSpareButtonHandler(clickedCommand) {
            if (self.selectedItem.FinishTime != null)
                $state.go(rootstate + '.addspare', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
            else
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_27'));
        }

        //编辑
        function editSpareButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            if (self.selectedItem.FinishTime != null)
                $state.go(rootstate + '.editspare', { id: self.selectedItem.ID, selectedItem: self.selectedItem, selectItemDetail: self.selectedItemDetail });
            else
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_27'));
        }

        //删除 事件
        function deleteSpareButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_25');
            var text = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_26');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentSpareParts/RemoveEP_EquipmentSpareParts';
                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];
                self.UserCode = user.loginName;
                self.UserName = user.fullName;
                self.selectedItem.ModifyBy = self.UserCode;
                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItemDetail
                };
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridDataDetail();
                        self.selectedItemDetail = null;
                        //self.isButtonVisible1 = false;
                        self.isButtonVisible2 = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);

                    }
                }, function (error) {
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_24'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMalfunctionRepair';
        var moduleStateUrl = 'Siemens.SimaticIT_EquipmentApp_EP_EquipmentMalfunctionRepair';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentMalfunctionRepair';

        var state = {
            name: moduleStateName + '_EP_EquipmentMalfunctionRepair',
            url: '/' + moduleStateUrl + '_EP_EquipmentMalfunctionRepair',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/EP_EquipmentMalfunctionRepair-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairlistctrl.Tips_28'
            }
        };
        $stateProvider.state(state);
    }
}());
