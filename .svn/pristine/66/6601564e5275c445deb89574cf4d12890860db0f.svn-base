(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderManage').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderManage.WorkOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope,
        commonService, auth, notificationService, busyIndicatorService, $modal, $interval) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();

            initGridOptions();

            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_1'));
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
            self.isReadYield = self.currentItem.isReadYield

            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function initDictionary() {

            // self.typeProcessOperation = {
            //     value: { ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_2'), ProcessCode: "" },
            //     options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_2'), ProcessCode: "" }]
            // };

            self.typeTransfer = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_2'), ItemValue: "" }]
            };
            // self.typeStartProcess = {
            //     value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_2'), ResourceCode: "" },
            //     options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_2'), ResourceCode: "" }]
            // };

            // commonService.getResourceExtendInfo({ LevelCode: "Process" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeStartProcess.options = res.data.resultData;
            //         self.typeStartProcess.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_2')
            //         });
            //     }
            // });

            commonService.getDataItemDuatil("TransferMode").then(function (res) {
                if (res && res.data.success) {
                    self.typeTransfer.options = res.data.resultData;
                    self.typeTransfer.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            // var url = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=";
            // commonService.callWebApiGet(url, null).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeProcessOperation.options = res.data.resultData;
            //         self.typeProcessOperation.options.splice('0', '0', {
            //             ProcessCode: "",
            //             ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_2')
            //         });
            //     }
            // })
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
                enableSelectAll: true,
                enableRowSelection: false,
                //enableFullRowSelection: true,
                enableMultiSelection: true,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_3'),
                        width: 110
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_4'),
                        width: 110
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_5'),
                        width: 110
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_6'),
                        width: 110
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_7'),
                        width: 140
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_8'),
                        width: 140
                    },
                    {
                        field: 'OrderPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_9'),
                        width: 140
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'StartOperationName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_11'),
                        width: 110,
                    },
                    {
                        field: 'TransferBy',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_12'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TransferBy==\'1\'"><span ng-cell-text>按柜</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TransferBy==\'2\'"><span ng-cell-text>按托</span></div>'
                    },
                    {
                        field: 'Yield',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_15'),
                        width: 110
                    },

                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem = row.entity;
                            //setButtonsVisibility(true);
                        } else {
                            self.selectedItem = null;
                            //setButtonsVisibility(false);
                        }
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: self.currentItem.data
            };
        }


        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_16') });

            var rows = [];
            // self.currentItem.Process = self.typeProcessOperation.value.ProcessCode == "" ? null : self.typeProcessOperation.value.ProcessCode;
            //self.currentItem.StartOperation = self.typeStartProcess.value.ResourceCode == "" ? null : self.typeStartProcess.value.ResourceCode;
            self.currentItem.TransferBy = self.typeTransfer.value.ItemValue == "" ? null : self.typeTransfer.value.ItemValue;
            var copyData = $scope.gridApi.selection.getSelectedRows();
            if (copyData.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_17'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_18'));
                busyIndicatorService.hide();
                return false;
            }

            copyData.forEach((item, index, arr) => {
                rows.push({
                    Id: item.WorkOrderId,
                    Process: self.currentItem.Process,
                    StartOperation: self.currentItem.StartOperation,
                    TransferBy: self.currentItem.TransferBy,
                    Yield: self.currentItem.YieldNum,
                    ActualSheets: Math.ceil(self.currentItem.TotalSheets / self.currentItem.YieldNum)

                })
            });

            var postData = {
                KeyValue: self.currentItem.Id,
                data: rows
            };


            var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/SaveBatchPL_WorkOrder';

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
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_19'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_18'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_18'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderManage_WorkOrder';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderManage';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrder-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderManage.editJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
