(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MaterialMain').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterial.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterial');

            init();
            initGridOptions();
            initGridOptionsDetail();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_MaterialMain_FactoryMaterial';
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

            initDictionary();
            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;
            self.searchButtonHandler = searchButtonHandler;//查看//子表/明细//关联
            self.typeFactoryChange = typeFactoryChange;//工厂change事件

            self.add2ButtonHandler = add2ButtonHandler;
            self.edit2ButtonHandler = edit2ButtonHandler;
        }


        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })

        $rootScope.$on("to-parentDetail", function (event, data) {
            initGridDataDetail();
        })


        function initDictionary() {
            //初始化 物料分类
            self.MaterialClass = {
                value: { text: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_1'), value: "" },
                options: [{ text: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_1'), value: "" }]
            };

            //初始化 物料小类
            self.SmallClass = {
                value: { text: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_1'), value: "" },
                options: [{ text: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_1'), value: "" }]
            };

            //初始化 采购类型
            self.ProcureType = {
                value: { text: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_1'), value: "" },
                options: [{ text: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_1'), value: "" }]
            };

            self.Warehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_1'), ResourceCode: "" }]
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
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_1')
                    });
                    initGridData();
                }
            });

            commonService.getDataItemDuatil("ProcureType").then(function (res) {
                if (res && res.data.success) {
                    self.ProcureType.options = res.data.resultData;
                    self.ProcureType.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getWarehouseByFactory({ factoryCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.Warehouse.options = res.data.resultData;
                        self.Warehouse.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_1')
                        });
                    }
                });
            } else {
                self.Warehouse = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_1'), ResourceCode: "" }]
                };
            }
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_6'),
                        width: 200
                    },
                    {
                        field: 'MaterialClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_9'),
                        width: 100
                    },
                    {
                        field: 'WarehouseName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'ProcureTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_11'),
                        width: 120
                    },
                    {
                        field: 'ProcessRouteName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_12'),
                        width: 140
                    },
                    {
                        field: 'IsUsed',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_13'),
                        width: 100,
                        cellTemplate:
                            '<div class="ngCellText" style="padding-left:5px;height:30px;line-height:30px;" ng-if="row.entity.IsUsed==1">是</div>' +
                            '<div class="ngCellText" style="padding-left:5px;height:30px;line-height:30px;" ng-if="row.entity.IsUsed==0">否</div>'
                    },
                    {
                        field: 'IsExemption',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_14'),
                        width: 100,
                        cellTemplate:
                            '<div class="ngCellText" style="padding-left:5px;height:30px;line-height:30px;" ng-if="row.entity.IsExemption==1">是</div>' +
                            '<div class="ngCellText" style="padding-left:5px;height:30px;line-height:30px;" ng-if="row.entity.IsExemption==0">否</div>'
                    },
                    {
                        field: 'SafeStock',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_15'),
                        width: 120
                    },
                    {
                        field: 'TemplateCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_16'),
                        width: 120
                    },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_17'),
                        width: 120
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_18'),
                        width: 180,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'ModifyBy',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_19'),
                        width: 140
                    },
                    {
                        field: 'ModifyTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_20'),
                        width: 180,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
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

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'MaterialCode',//物料编码
                sord: 'asc'
            };

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_21'))
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            //_shuoming 下拉查询赋值
            self.searchParams.MaterialClass = self.MaterialClass.value.ItemValue;
            //_shuoming 下拉查询赋值
            self.searchParams.SmallClass = self.SmallClass.value.ItemValue;
            //_shuoming 下拉查询赋值
            self.searchParams.ProcureType = self.ProcureType.value.ItemValue;
            //_shuoming 下拉查询赋值
            self.searchParams.Warehouse = self.Warehouse.value.ResourceCode;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("material") + 'Base_MaterialFactory/Base_MaterialFactoryPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_22'));
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
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_23');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_24');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress("material") = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("material") + 'Base_MaterialFactory/RemoveBase_MaterialFactory';
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
                commonService.callWebApiPost(url, postData).then(function (res) {
                    console.log("RemoveBase_Material----------------------------" + JSON.stringify(res));
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
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_25'));
                    }
                }, function (error) {

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_22'));
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    // {
                    //     field: 'MaterialId',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_26'),
                    //     width: 200
                    // },
                    {
                        field: 'AttrCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_27'),
                        width: 320
                    },
                    {
                        field: 'AttrName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_28'),
                        width: 320
                    },
                    {
                        field: 'AttrTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_29'),
                        width: 200
                    },
                    {
                        field: 'AttrValue',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_30'),
                        width: 320
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApiDetail = gridApi;
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        initGridDataDetail();
                    });
                    $scope.gridApiDetail.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailButtonVisible = true;

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


        function initGridDataDetail() {
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsDetail.paginationPageSize,
                page: self.gridOptionsDetail.paginationCurrentPage,
                sidx: 'Sort',//物料主键
                sord: 'asc'
            };

            if (self.selectedItem != null) {
                self.searchParams2.MaterialFactoryId = self.selectedItem.Id;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };

            var url = commonService.getMesApiAddress("material") + 'Base_MaterialFacet/Base_MaterialFacetPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_22'));
            });
        }
        //新增属性
        function add2ButtonHandler(clickedCommand) {

            $state.go(rootstate + '.addDetail', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //编辑属性
        function edit2ButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.editDetail', { id: self.selectedItemDetail.Id, selectedItem: self.selectedItemDetail });
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
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_MaterialMain';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_MaterialMain';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MaterialMain';

        var state = {
            name: moduleStateName + '_FactoryMaterial',
            url: '/' + moduleStateUrl + '_FactoryMaterial',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/FactoryMaterial-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMateriallistctrl.Tips_31'
            }
        };
        $stateProvider.state(state);
    }
}());
