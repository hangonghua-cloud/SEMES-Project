(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance').config(ViewScreenStateConfig);

    ViewScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenance.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$timeout'];
    function ViewScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $timeout) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();

            initGrid();
            initGridData();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceselectctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            // TODO: Put here the properties of the entity managed by the service
            debugger
            self.currentItem = angular.copy($stateParams.selectedItem);

            //Expose Model Methods
            self.cancel = cancel;
            self.save = save;
        }

        function initGrid() {
            self.gridOptions = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                useExternalPagination: false,//true:使用外部分页方式；false:使用UI Grid内部分页方式
                useExternalSorting: false,//true:使用外部排序方式，false:使用UI Gird内部排序方式
                multiSelect: true,
                enableRowSelection: false,
                enableRowHeaderSelection: true,
                enableColumnResizing: true,//允许调整列宽
                appScopeProvider: self,
                enableFiltering: false,
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceselectctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    }
                    , {
                        field: 'ItemDetailId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceselectctrl.Tips_3'),
                        width: 200,
                        visible: false
                    }
                    , {
                        field: 'EquipmentType',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceselectctrl.Tips_4'),
                        width: 200
                    }
                    , {
                        field: 'EquipmentTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceselectctrl.Tips_5'),
                        width: 200
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //行选中事件
                    //$scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                    //    if (row) {
                    //        if (row.isSelected) {
                    //            self.selectedItemDetail = row.entity;
                    //            console.log(self.selectedItemDetail);
                    //            self.isButtonVisible = true;

                    //            $scope.gridApi.selection.toggleRowSelection($scope.gridOptions.data[2]);
                    //        } else {
                    //            self.selectedItemDetail = null;

                    //        }

                    //    }
                    //});

                },
                data: []
            }
        }
        function initGridData() {
            var url = commonService.getMesApiAddress("equipment") + "EP_EquipmentCheckItemMaintenance/GetTypePageList?id=" + self.currentItem.Id;
            commonService.callWebApiGet(url).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridOptions.data = res.data.resultData;
                    $timeout(function () {
                        _.each(self.gridOptions.data, function (item, index) {

                            if (self.gridOptions.data[index].Id != null) {
                                // console.log(self.gridOptions.data[i]);
                                $scope.gridApi.selection.selectRow(self.gridOptions.data[index]);
                            }
                        })


                    })

                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {

                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceselectctrl.Tips_6'));
            });
        }

        function save() {
            debugger
            let selectRows = $scope.gridApi.selection.getSelectedRows();
            let Id = self.currentItem.Id;
            var rowArr = new Array();
            if (Id == "" || Id.length == 0) {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceselectctrl.Tips_7'));
                return;
            }
            if (selectRows == null || selectRows.length == 0) {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceselectctrl.Tips_8'));
                return;
            }
            for (var icount = 0; icount < selectRows.length; icount++) {
                //selectRows[icount].EquipmentType = selectRows[icount].ItemDetailId;
                var item = {
                    // FactoryCode: self.currentItem.FactoryCode,
                    // FactoryName: self.currentItem.FactoryName,
                    EquipmentMaintenanceId: selectRows[icount].EquipmentMaintenanceId,
                    EquipmentType: selectRows[icount].ItemDetailId,
                    EquipmentTypeName: selectRows[icount].EquipmentTypeName,
                    Id: selectRows[icount].Id,
                    ItemDetailId: selectRows[icount].ItemDetailId
                }
                rowArr.push(item);
            }
            //console.log('save.......' + JSON.stringify(selectRows));
            var postData = {
                EquipmentMaintenanceId: Id,
                SaveRows: rowArr
            };
            console.log("savesavesavesavesavesavesavesave       " + JSON.stringify(postData));
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentCheckItemMaintenance/SaveChildForm';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            debugger
            busyIndicatorService.hide();
            sidePanelManager.close();
            commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceselectctrl.Tips_9'));
            $state.go('^', {}, { reload: false });
        }
        function onSaveError(error) {
            busyIndicatorService.hide();
            console.log(123);
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceselectctrl.Tips_10'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    ViewScreenStateConfig.$inject = ['$stateProvider'];
    function ViewScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentCheckItemMaintenance_EP_EquipmentCheckItemMaintenance';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentCheckItemMaintenance';

        var state = {
            name: screenStateName + '.select',
            url: '/select/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EP_EquipmentCheckItemMaintenance-select.html',
                    controller: ViewScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceselectctrl.Tips_11'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
