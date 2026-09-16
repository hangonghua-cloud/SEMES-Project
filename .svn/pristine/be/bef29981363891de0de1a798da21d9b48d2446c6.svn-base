(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.WorkOrderBatch').config(ViewScreenStateConfig);

    ViewScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.WorkOrderBatch.WorkOrderBatch.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function ViewScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService,
        busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();

            initGridOptions1();
            initGridData1();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.selectedItem1 = null;
            self.searchParams1 = {};
            //Expose Model Methods
            self.cancel = cancel;
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_3'),
                        width: 120,
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_4'),
                        width: 110
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_5'),
                        width: 160
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_6'),
                        width: 80
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_7'),
                        width: 110
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_9'),
                        width: 140
                    },
                    {
                        field: 'KCPieceQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'DemandMaterial',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_11'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.DemandMaterial==true"><span ng-cell-text class="green">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.DemandMaterial!=true"><span ng-cell-text class="red">否</span></div>'
                    },
                    {
                        field: 'TotalSheets',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_14'),
                        width: 110
                    },
                    {
                        field: 'ActualSheets',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_15'),
                        width: 110
                    },
                    {
                        field: 'BWXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_16'),
                        width: 110
                    },
                    {
                        field: 'KCKX',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_17'),
                        width: 110
                    },
                    {
                        field: 'FirstInspectionConfirm',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_18'),
                        width: 140,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.FirstInspectionConfirm==true"><span ng-cell-text class="green">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.FirstInspectionConfirm!=true"><span ng-cell-text class="red">否</span></div>'
                    },
                    {
                        field: 'AvoidProduce',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_19'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.AvoidProduce==true"><span ng-cell-text class="green">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.AvoidProduce!=true"><span ng-cell-text class="red">否</span></div>'
                    },

                    {
                        field: 'CustomerPO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_20'),
                        width: 120
                    },
                    {
                        field: 'OrderStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_21'),
                        width: 110,
                    },
                    {
                        field: 'WorkOrderTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_22'),
                        width: 110,
                    },

                    {
                        field: 'POStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_23'),
                        width: 130,
                    },
                    {
                        field: 'MMCJ',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_24'),
                        width: 120
                    },
                    {
                        field: 'OrderPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_25'),
                        width: 120
                    },
                    {
                        field: 'OrderBox',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_26'),
                        width: 140
                    },
                    {
                        field: 'OrderPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_27'),
                        width: 140
                    },
                    {
                        field: 'OrderStartPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_28'),
                        width: 140
                    },
                    {
                        field: 'DeliveryPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_29'),
                        width: 140
                    },
                    {
                        field: 'DeliveryBox',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_30'),
                        width: 140
                    },
                    {
                        field: 'DeliveryPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_31'),
                        width: 140
                    },
                    {
                        field: 'DeliveryStartPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_32'),
                        width: 140
                    },

                    {
                        field: 'Yield',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_33'),
                        width: 100
                    },
                    {
                        field: 'DXZH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_34'),
                        width: 130
                    },

                    {
                        field: 'HD',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_35'),
                        width: 120
                    },

                    {
                        field: 'UV',
                        displayName: 'UV',
                        width: 100
                    },

                    {
                        field: 'OrderDate',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_36'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'
                    },
                    {
                        field: 'DeliveryDate',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_37'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'
                    },
                    {
                        field: 'PackingEndTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_38'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_39'),
                        width: 110
                    },
                    {
                        field: 'StartOperationName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_40'),
                        width: 110,
                    },
                    {
                        field: 'TransferBy',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_41'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TransferBy==\'1\'"><span ng-cell-text>按柜</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TransferBy==\'2\'"><span ng-cell-text>按托</span></div>'
                    },
                    {
                        field: 'FirstInspectionOperation',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_44'),
                        width: 160
                    },

                    {
                        field: 'FreezeFlag',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_45'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.FreezeFlag==true"><span ng-cell-text class="green">已冻结</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.FreezeFlag!=true"><span ng-cell-text class="red">未冻结</span></div>'
                    },
                    // {
                    //     field: 'IsEnabled',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_48'),
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.IsEnabled!=true"><span ng-cell-text class="green">已删除</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.IsEnabled==true"><span ng-cell-text class="red">未删除</span></div>'
                    // },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_51'),
                        width: 160
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

            let Pagination = {
                rows: self.gridOptionsItem1.paginationPageSize,
                page: self.gridOptionsItem1.paginationCurrentPage,
                sidx: 'CreateTime',//排序
                sord: 'asc'
            };
            self.searchParams1.BatchWorkOrder = self.currentItem.WorkOrder;
            var postData = {
                pagination: Pagination,
                queryJson: self.searchParams1
            };
            var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/PL_WorkOrderPageDataTableList';
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

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            sidePanelManager.close();
            $state.go('^', {}, { reload: true });
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    ViewScreenStateConfig.$inject = ['$stateProvider'];
    function ViewScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_WorkOrderBatch_WorkOrderBatch';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/WorkOrderBatch';

        var state = {
            name: screenStateName + '.select',
            url: '/select/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrderBatch-select.html',
                    controller: ViewScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.WorkOrderBatch.selectJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
