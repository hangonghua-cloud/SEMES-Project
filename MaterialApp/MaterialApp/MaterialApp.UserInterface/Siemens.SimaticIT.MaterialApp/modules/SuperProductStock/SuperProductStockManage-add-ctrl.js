(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.SuperProductStock').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_1'));
            //sidePanelManager.open('e');
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            self.currentItem = {};
            self.validInputs = false;
            self.selectedItem = null;
            self.searchParams = {};

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.search = search;

            initDictionary();
            self.typeFactoryChange = typeFactoryChange;
            self.typeWarehouseChange = typeWarehouseChange;
            self.searchButtonHandler = searchButtonHandler;


        }

        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2'), ResourceCode: "" }]
            };
            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2'), ResourceCode: "" }]
            };
            //库位
            self.typeLocation = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2'), ResourceCode: "" }]
            };

            //工厂
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2')
                    });
                    // initGridData();
                }
            });

            //仓库
            self.typeWarehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2'), ResourceCode: "" }]
            };
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                //工序
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeProcess.options = res.data.resultData;
                        self.typeProcess.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2')
                        });
                    }
                });
                //仓库
                commonService.getWarehouseByFactoryExtendInfo({
                    factoryCode: newItem.ResourceCode,
                    fieldCode: "CKLX",
                    fieldValue: "4"
                }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeWarehouse.options = res.data.resultData;
                        self.typeWarehouse.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2')
                        });
                    }
                });
            }
            else {
                self.typeProcess = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2'), ResourceCode: "" }]
                };
                self.typeWarehouse = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2'), ResourceCode: "" }]
                };
            }
        }

        //仓库change事件
        function typeWarehouseChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeLocation.options = res.data.resultData;
                        self.typeLocation.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2')
                        });
                    }
                });
            }
            else {
                self.typeLocation = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_2'), ResourceCode: "" }]
                };
            }
        }

        //查询
        function searchButtonHandler() {
            initGridData();
        }
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
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
                enablePagination: false, //是否分页,default为true
                enablePaginationControls: false, //使用默认的底部分页
                paginationPageSizes: [20, 30, 50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 20, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: false,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: false, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_3'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    // {
                    //     field: 'FactoryName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_4'),
                    //     width: 120
                    // },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_6'),
                        width: 100
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_21'),
                        width: 130
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_7'),
                        width: 150
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_8'),
                        width: 320
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_9'),
                        width: 200
                    },

                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'WorkOrderInQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_11'),
                        width: 120
                    },
                    {
                        field: 'OrderStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_22'),
                        width: 120
                    },
                    {
                        field: 'POStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_23'),
                        width: 120
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
            // let Pagination = {
            //     rows: self.gridOptions.paginationPageSize,
            //     page: self.gridOptions.paginationCurrentPage,
            //     sidx: 'MaterialCode',//物料编码
            //     sord: 'asc'
            // };
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            // self.searchParams.ProcessCode = self.typeProcess.value.ResourceCode;
            let queryParmeters = {
                // pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'MM_SuperProductStock/GetDZCCPList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //数据
                    self.gridOptions.data = res.data.resultData;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_12'));
            });
        }
        function search() {
            initGridData();
        }

        function save() {

            //字典类型 取值参考
            if (!self.selectedItem) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_13'), commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_14'));
                return false;
            }
            if (!self.typeProcess.value.ResourceCode) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_15'), commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_14'));
                return false;
            }
            if (!self.currentItem.BatchNo) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_16'), commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_14'));
                return false;
            }
            if (!self.currentItem.Qty) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_17'), commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_14'));
                return false;
            }
            if (!self.currentItem.Remark) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_18'), commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_14'));
                return false;
            }
            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.typeFactory.value.ResourceName;
            self.currentItem.ProcessCode = self.typeProcess.value.ResourceCode;
            self.currentItem.BusinessType = "1";//转入
            self.currentItem.Spec = self.selectedItem.Spec;
            self.currentItem.MaterialCode = self.selectedItem.MaterialCode;
            self.currentItem.MaterialName = self.selectedItem.MaterialName;
            self.currentItem.MMXH = self.selectedItem.MMXH;
            self.currentItem.DXZH = self.selectedItem.DXZH;
            self.currentItem.ProductOrder = self.selectedItem.ProductOrder;
            self.currentItem.ContainerNO = self.selectedItem.ContainerNO;
            self.currentItem.WorkOrder = self.selectedItem.WorkOrder;
            self.currentItem.WhsCode = self.typeWarehouse.value.ResourceCode;
            self.currentItem.WhsName = self.typeWarehouse.value.ResourceName;
            self.currentItem.LocationCode = self.typeLocation.value.ResourceCode;
            self.currentItem.LocationName = self.typeLocation.value.ResourceName;

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                Entity: self.currentItem
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_19') });
            var url = commonService.getMesApiAddress("material") + 'MM_SuperProductStock/SuperProductStockIn';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        //取消
        function cancel() {
            //关闭侧边栏
            sidePanelManager.close();
            //返回列表(父页面)
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {

            if (data.data.success) {
                debugger;
                busyIndicatorService.hide();
                //关闭侧边栏
                //sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_20'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                // $state.go('^', {}, { reload: false });
                self.selectedItem = null;
                self.currentItem.BatchNo = "";
                self.currentItem.Qty = "";
                self.currentItem.Remark = "";
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_14'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_14'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_SuperProductStock_SuperProductStockManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/SuperProductStock';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/SuperProductStockManage-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
