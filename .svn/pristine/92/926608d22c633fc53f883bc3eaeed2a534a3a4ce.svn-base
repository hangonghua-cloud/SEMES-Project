(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder').config(OutScreenStateConfig);

    OutScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.PMOwnSemiProductOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function OutScreenController(dataService, $state, $stateParams,
        common, $filter, $scope, commonService, auth, notificationService,
        busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();

        // Initialization function
        function activate() {
            init();
            registerEvents();
            initGridOptions1();
            initGridOptions2();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_1'));
            // sidePanelManager.open('e');
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;
            self.selectedItem1 = null;
            self.selectedItem2 = null;
            self.searchParams1 = {};
            self.searchParams2 = {};

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.search = search;
            self.addForm = addForm;
            self.resetForm = resetForm;
            self.typeFactoryChange = typeFactoryChange;
            self.typeWarehouseChange = typeWarehouseChange;
            self.search2 = search2;

            //数据字典
            initDictionary();
        }

        function initDictionary() {

            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_2')
                    });
                    initGridData1();
                }
            });
            //仓库
            self.typeWarehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_2'), ResourceCode: "" }]
            };
            //库位
            self.typeLocation = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_2'), ResourceCode: "" }]
            };
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                let query = {
                    factoryCode: newItem.ResourceCode,
                    fieldCode: "CKLX",
                    fieldValue: "1"
                };
                commonService.getWarehouseByFactoryExtendInfo(query).then(function (res) {
                    if (res && res.data.success) {
                        self.typeWarehouse.options = res.data.resultData;
                        self.typeWarehouse.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_2')
                        });
                    }
                });
            } else {
                self.typeWarehouse = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_2'), ResourceCode: "" }]
                };
            }
        }
        //仓库改变事件
        function typeWarehouseChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeLocation.options = res.data.resultData;
                    self.typeLocation.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_2')
                    });
                }
            });
        }

        function initGridOptions1() {
            self.gridOptionsItem1 = {
                //分页属性
                enablePagination: true, //是否分页,default为true
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
                paginationPageSize: 100, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                rowHeight: 35,
                multiSelect: false,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: false,
                enableRowSelection: false,
                enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_3'),
                        width: 100
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_4'),
                        width: 100
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_5'),
                        width: 130
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_6'),
                        width: 80
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_7'),
                        width: 130
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_8'),
                        width: 130
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_9'),
                        width: 200
                    },
                    {
                        field: 'ProductQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'DeliveryQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_11'),
                        width: 120
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_12'),
                        width: 80
                    },

                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData1();
                    });
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem1 = row.entity;
                        } else {
                            self.selectedItem1 = null;
                        }
                        initGridData2();
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }

        function initGridData1() {

            self.selectedItem1 = null;
            self.selectedItem2 = null;
            self.gridOptionsItem2.data = [];

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_13'))
                return;
            }
            let Pagination = {
                rows: self.gridOptionsItem1.paginationPageSize,
                page: self.gridOptionsItem1.paginationCurrentPage,
                sidx: 'CreateTime',//订单号、柜号
                sord: 'asc'
            };

            self.searchParams1.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams1.queryCode = "1,2";//订单状态新建、进行中
            var postData = {
                pagination: Pagination,
                queryJson: self.searchParams1
            };
            var url = commonService.getMesApiAddress("ProduceManage") + 'PMOwnSemiProductOrder/GetPageDataTableList';
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    //总条数
                    self.gridOptionsItem1.totalItems = res.data.resultData.records;
                    self.gridOptionsItem1.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem1.data = []
                }
            })
        }

        function search() {
            initGridData1();
        }

        function initGridOptions2() {
            self.gridOptionsItem2 = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
                rowHeight: 35,
                multiSelect: false,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: false,
                enableRowSelection: false,
                //enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_14'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_15'),
                        width: 100
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_16'),
                        width: 100
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_17'),
                        width: 110
                    },
                    {
                        field: 'OutQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_18'),
                        width: 110
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi2 = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem2 = row.entity;
                        } else {
                            self.selectedItem2 = null;
                        }
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi2.core.handleWindowResize();
                        $scope.gridApi2.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }

        function initGridData2() {

            self.selectedItem2 = null;
            if (!self.selectedItem1) {
                return;
            }

            self.searchParams2.FactoryCode = self.selectedItem1.FactoryCode;
            self.searchParams2.MaterialCode = self.selectedItem1.MaterialCode;
            self.searchParams2.WhsCode = self.typeWarehouse.value.ResourceCode;
            self.searchParams2.LocationCode = self.typeLocation.value.ResourceCode;
            var postData = {
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialStock/MM_RawMaterialStockPageDataTableList';
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem2.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem2.data = []
                }
            })
        }

        function search2() {
            initGridData2();
        }

        function addForm() {
            if (!self.selectedItem1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_19'), commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_20'));
                return false;
            }
            if (!self.selectedItem2) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_21'), commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_20'));
                return false;
            }
            if (!self.currentItem.Qty < 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_22'), commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_20'));
                return false;
            }
            if (self.currentItem.Qty > self.selectedItem2.Qty) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_23'));
                return;
            }

            self.selectedItem2.OutQty = self.currentItem.Qty;
        }

        function resetForm() {
            if (!self.selectedItem2) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_21'));
                return;
            }

            self.selectedItem2.OutQty = "";
        }

        function save() {

            //字典类型 取值参考
            let data = self.gridOptionsItem2.data.filter(item => {
                return !!item.OutQty;
            });
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_24'), commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_20'));
                return false;
            }
            let totalOutQty = data.reduce((c, item) => c + item.OutQty, 0);
            if (totalOutQty > self.selectedItem1.ProductQty - self.selectedItem2.DeliveryQty) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_25'), commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_20'));
                return false;
            }

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                keyValue: self.selectedItem1.Id,
                data: data
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_26') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PMOwnSemiProductOrder/PMOwnSemiProductOrderOut';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        //取消
        function cancel() {
            sidePanelManager.close();//关闭侧边栏
            $state.go('^');//返回列表(父页面)
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();//关闭遮罩层
                // sidePanelManager.close();//关闭侧边栏
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_27'));
                $rootScope.$emit('to-parent', 'parent');//刷新局部
                initGridData1();
                // $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_20'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_20'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }
    OutScreenStateConfig.$inject = ['$stateProvider'];
    function OutScreenStateConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_PMOwnSemiProductOrder_PMOwnSemiProductOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PMOwnSemiProductOrder';

        var state = {
            name: moduleStateName + '.out',
            url: '/out',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PMOwnSemiProductOrder-out.html',
                    controller: OutScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PMOwnSemiProductOrder.outJS.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
