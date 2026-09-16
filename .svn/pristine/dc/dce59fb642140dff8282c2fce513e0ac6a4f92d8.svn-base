(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PMPerformanceManage').config(ViewScreenStateConfig);

    ViewScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PMPerformanceManage.PMPerformanceManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function ViewScreenController(dataService, $state, $stateParams,
        common, $filter, $scope, commonService, auth, notificationService,
        busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();

        // Initialization function
        function activate() {
            init();

            initGridOptions1();
            initGridOptions2();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.selectedItem1 = null;
            self.selectedItem2 = null;
            self.searchParams1 = {};
            self.searchParams2 = {};

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.search = search;
            self.neglect = neglect;//忽略
            self.typeFactoryChagne = typeFactoryChagne;

            //数据字典
            initDictionary();
        }

        function initDictionary() {
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_2')
                    });
                    initGridData1();
                }
            });

            //车间
            self.typeWorkShop = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_2'), ResourceCode: "" }]
            };

            //错误原因
            self.typeErrorReason = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_2'), ItemValue: "" },
                options: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_2'), ItemValue: "" },
            }
            commonService.getDataItemDuatil("ErrorReason").then(function (res) {
                if (res && res.data.success) {
                    self.typeErrorReason.options = res.data.resultData;
                }
            })
        }

        //工厂change事件
        function typeFactoryChagne(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeWorkShop.options = res.data.resultData;
                        self.typeWorkShop.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_2')
                        });
                    }
                });
            }
            else {
                self.typeWorkShop = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_2'), ResourceCode: "" }]
                };
            }
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
                multiSelect: true,
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_3'), width: 75, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_4'),
                        width: 100
                    },
                    {
                        field: 'WorkShopName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'CardCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_6'),
                        width: 300
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_7'),
                        width: 130
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_8'),
                        width: 130
                    },
                    {
                        field: 'Price',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'EquipCoefficient',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'ErrorReasonName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_11'),
                        width: 130
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_12'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter',
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
                            initGridData2();
                            self.validInputs = true;
                        } else {
                            self.selectedItem1 = null;
                            self.gridOptionsItem2.data = [];
                            self.validInputs = false;
                        }
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
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_13'))
                return;
            }
            let Pagination = {
                rows: self.gridOptionsItem1.paginationPageSize,
                page: self.gridOptionsItem1.paginationCurrentPage,
                sidx: 'CreateTime',//排序
                sord: 'asc'
            };

            self.searchParams1.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams1.WorkShopCode = self.typeWorkShop.value.ResourceCode;
            self.searchParams1.ErrorReason = self.typeErrorReason.value.ItemValue;
            var postData = {
                pagination: Pagination,
                queryJson: self.searchParams1
            };
            var url = commonService.getMesApiAddress("ProduceManage") + 'PMPerformanceManage/GetPageDataTableListBySalary';
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
                        field: 'PTeamCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_14'),
                        width: 120
                    },
                    {
                        field: 'PTeamName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_15'),
                        width: 120
                    },
                    {
                        field: 'PostCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_16'),
                        width: 120
                    },
                    {
                        field: 'PostName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_17'),
                        width: 120
                    },
                    {
                        field: 'UserCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_18'),
                        width: 120
                    },
                    {
                        field: 'UserName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_19'),
                        width: 120
                    },
                    {
                        field: 'Coefficient',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_20'),
                        width: 120
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
            self.gridOptionsItem2.data = [];

            self.searchParams2.BGID = self.selectedItem1.Id;
            var postData = {
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_TransferBGPersonRecord/PM_TransferBGPersonRecordPageDataTableList';
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem2.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem2.data = []
                }
            })
        }

        //忽略
        function neglect() {
            let selectedRows = $scope.gridApi.selection.getSelectedRows();
            if (selectedRows.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_21'), commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_22'));
                return;
            }

            var postData = {
                data: selectedRows
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_23') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PMPerformanceManage/Neglect';
            var req = commonService.callWebApiPost(url, postData).then(function (res) {
                busyIndicatorService.hide();//关闭遮罩层
                if ((res) && (res.data.success)) {
                    commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_24'));
                    initGridData1();
                }
            });
        }

        function save() {

            //字典类型 取值参考
            let data = self.gridOptionsItem1.data;
            if (data.length == 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_25'), commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_22'));
                return false;
            }

            var postData = {
                data: data
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_23') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PMPerformanceManage/ReCalculation';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        //取消
        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                // sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_26'));
                initGridData1();
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                // $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_22'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_22'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }
    ViewScreenStateConfig.$inject = ['$stateProvider'];
    function ViewScreenStateConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_PMPerformanceManage_PMPerformanceManage';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PMPerformanceManage';

        var state = {
            name: moduleStateName + '.select',
            url: '/select',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PMPerformanceManage-select.html',
                    controller: ViewScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PMPerformanceManage.selectJS.Tips_1'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
