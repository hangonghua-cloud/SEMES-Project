(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.Plan').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.PlanApp.Plan.ProductionOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $interval) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;


        activate();
        function activate() {
            init();

            initGridOptions();
            initGridData();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_1'));
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
            self.currentItem = angular.copy($stateParams.selectedItem)
            self.validInputs = false;
            self.selectedItem = null;
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.edit = edit;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function initGridOptions() {
            self.gridOptionsItem = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
                rowHeight: 35,
                multiSelect: true,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: false,
                enableRowSelection: false,
                //enableFullRowSelection: true,
                enableMultiSelection: true,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_2'),
                        width: 110
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_3'),
                        width: 110
                    },
                    {
                        field: 'CustomerPO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_4'),
                        width: 110
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_5'),
                        width: 80
                    },
                    {
                        field: 'OrderPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_6'),
                        width: 100
                    },

                    {
                        field: 'LoadingDate',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_7'),
                        width: 120,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd"'
                    },
                    {
                        field: 'InvoiceNO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_8'),
                        width: 100
                    },
                    {
                        field: 'LoadingBill',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_9'),
                        width: 100
                    },
                    {
                        field: 'BoxType',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_10'),
                        width: 80
                    },
                    {
                        field: 'GrossWeight',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_11'),
                        width: 100
                    },
                    {
                        field: 'Volume',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_12'),
                        width: 100
                    },

                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem = row.entity;
                        } else {
                            self.selectedItem = null;
                        }
                    });
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }

        function initGridData() {

            var postData = {
                queryJson: {
                    ProductOrder: self.currentItem.ProductOrder,
                    workOrderString: self.currentItem.workOrderString
                }
            }
            var url = commonService.getMesApiAddress("plan") + "PL_LoadingSchedule/PL_LoadingSchedulePageDataTableList";
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem.data = res.data.resultData.rows;
                }
            });
        }

        function edit() {
            var rows = $scope.gridApi.selection.getSelectedRows();
            if (rows.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_13'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_14'));
                return;
            }
            rows.forEach((item, index, arr) => {
                item.LoadingDate = angular.copy(self.currentItem.LoadingDate),
                    item.InvoiceNO = angular.copy(self.currentItem.InvoiceNO),
                    item.LoadingBill = angular.copy(self.currentItem.LoadingBill),
                    item.BoxType = angular.copy(self.currentItem.BoxType),
                    item.GrossWeight = angular.copy(self.currentItem.GrossWeight),
                    item.Volume = angular.copy(self.currentItem.Volume)
            });

        }
        //编辑保存
        function save() {

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_15') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            var rows = $scope.gridApi.selection.getSelectedRows();
            if (rows.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_13'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_16'));
                busyIndicatorService.hide();
                return
            }
            var postData = {
                ProductOrder: self.currentItem.ProductOrder,
                data: rows
            };

            var url = commonService.getMesApiAddress('plan') + 'PL_LoadingSchedule/SaveBatchPL_LoadingSchedule';
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

            //console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_18'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_16'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_16'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_Plan_ProductionOrder';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/Plan';

        var state = {
            name: screenStateName + '.stuffing',
            url: '/stuffing/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductionOrder-stuffing.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.Plan.stuffingJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
