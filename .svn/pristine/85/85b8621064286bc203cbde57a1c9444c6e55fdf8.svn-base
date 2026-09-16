(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.BOM').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.BOM.BOM.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', 'i18nService', 'common.services.security.securityService', 'common.services.security.functionRightModel'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, i18nService, securityService, FunctionRightModel) {
        var self = this;
        var logger, rootstate, messageservice, backendService;
        i18nService.setCurrentLang('zh-cn');

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.BOM.BOM');

            init();
            initGridOptions();
            initGridOptionsDetail();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_BOM_BOM';
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
            self.copyparams = {};
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
            self.CopyButtonHandler = CopyButtonHandler;//复制bom
            self.stickButtonHandler = stickButtonHandler;//粘贴bom

            initDictionary();


            //按钮权限
            ButtonAuthInitFalse();
            self.BaseBOMAdd = false;  //新增
            self.BaseBOMEdit = false; //修改
            self.BaseBOMDelete = false; //删除
            self.BaseBOMAddDetail = false;  //新增BOM明细
            self.BaseBOMEditDetail = false; //修改BOM明细
            self.BaseBOMDeleteDetail = false;//删除BOM明细
            self.BaseBOMCopy = false;//复制BOM
            self.BaseBOMPaste = false;//粘贴BOM
            ButtonAuthInit();
        }

        //按钮权限
        function ButtonAuthInitFalse() {
            //1.定义初始按钮
            self.isBaseBOMAdd = false;
            self.isBaseBOMEdit = false;
            self.isBaseBOMDelete = false;
            self.isBaseBOMAddDetail = false;
            self.isBaseBOMEditDetail = false;
            self.isBaseBOMDeleteDetail = false;
            self.isBaseBOMCopy = false;//复制BOM
            self.isBaseBOMPaste = false;//粘贴BOM
        }
        //按钮权限
        function ButtonAuthInit() {
            // debugger
            var PageName = "BaseBOM";
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

                                    if (data[i].objectName.split('.')[7] == PageName + "Add") {
                                        self.BaseBOMAdd = data[i].isAccessible;//新增
                                        self.isBaseBOMAdd = self.BaseBOMAdd;
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Edit") {
                                        self.BaseBOMEdit = data[i].isAccessible;
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Delete") {
                                        self.BaseBOMDelete = data[i].isAccessible;
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "AddDetail") {
                                        self.BaseBOMAddDetail = data[i].isAccessible;
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "EditDetail") {
                                        self.BaseBOMEditDetail = data[i].isAccessible;
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "DeleteDetail") {
                                        self.BaseBOMDeleteDetail = data[i].isAccessible;
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Copy") {
                                        self.BaseBOMCopy = data[i].isAccessible;
                                    }
                                    else if (data[i].objectName.split('.')[7] == PageName + "Paste") {
                                        self.BaseBOMPaste = data[i].isAccessible;
                                    }
                                }
                            }
                        }
                    }, function (resError) {
                        backendService.genericError('获取数据出错', resError);
                    });

                } else {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_2'), commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_3'));
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_4'));
            });
        }

        function initDictionary() {
            //初始化 物料分类
            self.MaterialClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_5'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_5'), ItemValue: "" }]
            };
            self.typeOrderType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_5'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_5'), ItemValue: "" }]
            };
            //初始化 物料小类
            self.SmallClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_5'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_5'), ItemValue: "" }]
            };

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
            commonService.getDataItemDuatil("OrderType").then(function (res) {
                if (res && res.data.success) {
                    self.typeOrderType.options = res.data.resultData;
                    self.typeOrderType.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_5'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_5'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_5')
                    });
                    initGridData();
                }
            });
        }
        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })
        $rootScope.$on("to-parentdetail", function (event, data) {
            initGridDataDetail();
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_6'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_36'),
                        width: 110
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'OrderType',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_8'),
                        width: 120,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'1\'"><span ng-cell-text>出口</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'2\'"><span ng-cell-text>内销</span></div>'
                    },
                    {
                        field: 'MaterialClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'BOMCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_10'),
                        width: 140
                    },
                    {
                        field: 'MaterialCode_BS',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_11'),
                        width: 140
                    },
                    {
                        field: 'MaterialName_BS',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_12'),
                        width: 140
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_13'),
                        width: 140
                    },
                    {
                        field: 'UnitNum',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_14'),
                        width: 140
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_15'),
                        width: 110
                    },
                    {
                        field: 'Process',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_34'),
                        width: 140
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_16'),
                        width: 140
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_37'),
                        width: 140
                    },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_17'),
                        width: 140
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_18'),
                        width: 180,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'ModifyBy',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_19'),
                        width: 140
                    },
                    {
                        field: 'ModifyTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_20'),
                        width: 180,
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

                                //按钮权限
                                self.isBaseBOMEdit = self.BaseBOMEdit;
                                self.isBaseBOMDelete = self.BaseBOMDelete;
                                self.isBaseBOMAddDetail = self.BaseBOMAddDetail;
                                self.isBaseBOMCopy = self.BaseBOMCopy;
                                self.isBaseBOMPaste = self.BaseBOMPaste;
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;

                                //按钮权限
                                ButtonAuthInitFalse();
                                self.isBaseBOMAdd = self.BaseBOMAdd;
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
            self.isBaseBOMAdd = self.BaseBOMAdd;
            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'BOMCode',//BOM编码
                sord: 'asc'
            };

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_21'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.MaterialClass = self.MaterialClass.value.ItemValue;
            self.searchParams.SmallClass = self.SmallClass.value.ItemValue;
            self.searchParams.OrderType = self.typeOrderType.value.ItemValue;
            self.searchParams.BOMType = "1";
            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'BS_BOM/BS_BOMPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
                self.gridOptionsDetail.data = [];
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_1'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
            initGridDataDetail();
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
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_22');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_23');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress("material") = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("material") + 'BS_BOM/RemoveBS_BOM';
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
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_1'));
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_6'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_11'),
                        width: 120
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_12'),
                        width: 120
                    },
                    {
                        field: 'BOMCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'Num',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_24'),
                        width: 100
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_15'),
                        width: 100
                    },
                    {
                        field: 'Warehouse',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_35'),
                        width: 140
                    },
                    {
                        field: 'WarehouseName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_25'),
                        width: 140
                    },
                    {
                        field: 'ProcessCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_26'),
                        width: 130
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_27'),
                        width: 130
                    },
                    {
                        field: 'IsUsed',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_28'),
                        width: 100,
                        cellTemplate:
                            '<div class="ngCellText" style="padding-left:5px;height:30px;line-height:30px;" ng-if="row.entity.IsUsed==1">是</div>' +
                            '<div class="ngCellText" style="padding-left:5px;height:30px;line-height:30px;" ng-if="row.entity.IsUsed==0">否</div>'
                    },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_17'),
                        width: 100
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_18'),
                        width: 180,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
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

                                //按钮权限
                                self.isBaseBOMEditDetail = self.BaseBOMEditDetail;
                                self.isBaseBOMDeleteDetail = self.BaseBOMDeleteDetail;
                            } else {
                                self.selectedItemDetail = null;
                                self.isDetailButtonVisible = false;

                                //按钮权限
                                ButtonAuthInitFalse();
                                self.isBaseBOMAdd = self.BaseBOMAdd;
                                self.isBaseBOMEdit = self.BaseBOMEdit;
                                self.isBaseBOMDelete = self.BaseBOMDelete;
                                self.isBaseBOMAddDetail = self.BaseBOMAddDetail;
                                self.isBaseBOMCopy = self.BaseBOMCopy;
                                self.isBaseBOMPaste = self.BaseBOMPaste;
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
                sidx: 'MaterialCode',//物料编码
                sord: 'asc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.BOMId = self.selectedItem.Id;
            }
            else {
                self.gridOptionsDetail.data = [];
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };

            var url = commonService.getMesApiAddress("material") + 'BS_BOMItems/BS_BOMItemsPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_1'));
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

        //复制bom       
        function CopyButtonHandler() {
            debugger

            if (self.gridOptionsDetail.data != null) {
                self.copyparams = self.gridOptionsDetail.data;
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_29'));
            } else {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_30'));
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
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_31'));
                return;
            }

            var postData = {
                KeyValue: self.selectedItem.Id,
                Entity: self.copyparams
            };
            var url = commonService.getMesApiAddress("material") + 'BS_BOMItems/SaveBS_BOMItemsCopy';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_32') });
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

                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_1'));
            });
        }


        //删除 事件
        function delete2ButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_22');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_23');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("material") + 'BS_BOMItems/RemoveBS_BOMItems';
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
                commonService.callWebApiPost(url, postData).then(function (res) {
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

                    }
                }, function (error) {

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_1'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_BOM';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_BOM';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/BOM';

        var state = {
            name: moduleStateName + '_BOM',
            url: '/' + moduleStateUrl + '_BOM',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/BOM-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.BOM.BOMlistctrl.Tips_33'
            }
        };
        $stateProvider.state(state);
    }
}());
