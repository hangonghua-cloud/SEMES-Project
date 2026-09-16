(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.Plan').config(ViewScreenStateConfig);

    ViewScreenController.$inject = ['Siemens.SimaticIT.PlanApp.Plan.ProductionOrder.service', '$state', '$stateParams', 'common.base', '$filter', '$scope', 'commonService', '$interval'];
    function ViewScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, $interval) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            initGridOptions();
            initGridData();
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.materialJS.Tips_1'));
            sidePanelManager.open({
                mode: 'e',
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = $stateParams.selectedItem;

            //Expose Model Methods
            self.cancel = cancel;
        }

        function initGridOptions() {
            self.gridOptionsItem = {
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
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.materialJS.Tips_2'),
                        width: 220,
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.materialJS.Tips_3'),
                        width: 220,
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.materialJS.Tips_4'),
                        width: 200,
                    },
                    {
                        field: 'Amount',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.materialJS.Tips_5'),
                        width: 200,
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            // self.selectedItem = row.entity;
                            //setButtonsVisibility(true);
                            // self.IsShowDelete = true;
                        } else {
                            // self.selectedItem = null;
                            // self.IsShowDelete = false;
                            //setButtonsVisibility(false);
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

        function initGridData() {
            var param = {
                queryJson: {
                    ProductOrder: self.currentItem.ProductOrder
                }
            }
            var url = commonService.getMesApiAddress("plan") + "PL_PrdOrderReqMaterials/PL_PrdOrderReqMaterialsPageDataTableList";
            commonService.callWebApiPost(url, param).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem.data = res.data.resultData.rows
                } else {
                    self.gridOptionsItem.data = []
                }
            });
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
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_Plan_ProductionOrder';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/Plan';

        var state = {
            name: screenStateName + '.material',
            url: '/material/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductionOrder-material.html',
                    controller: ViewScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.Plan.materialJS.Tips_6'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
