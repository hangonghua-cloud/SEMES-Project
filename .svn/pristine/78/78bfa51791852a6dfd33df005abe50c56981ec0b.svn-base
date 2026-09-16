(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderManage').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderManage.WorkOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope,
        commonService, auth, notificationService, busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();

            initGridOptions();
            initGridOptionsItem2();
            initGridData();

            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_1'));
            sidePanelManager.open("e");
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.isVisble = false;

            self.searchParams2 = {};

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.deleteOperation = deleteOperation;
            self.saveAttr = saveAttr;
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
                multiSelect: false,
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
                        field: 'SN',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_2'),
                        width: 120,

                    },
                    {
                        field: 'OperationName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'CuringCycle',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_4'),
                        width: 120,
                        cellTemplate: '<sit-numeric sit-value="row.entity.CuringCycle" ></sit-numeric>'
                    },
                    {
                        field: 'WFMarkName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_18'),
                        width: 120
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem = row.entity;
                            //setButtonsVisibility(true);
                            self.isVisble = true;
                            initGridDataItem2();
                        } else {
                            self.selectedItem = null;
                            //setButtonsVisibility(false);
                            self.isVisble = false;
                            self.gridOptionsItem2.data = [];
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

        function initGridData() {
            var url = commonService.getMesApiAddress("plan") + 'PL_BOM/GetWorkOrderOperationsItem';

            commonService.callWebApiPost(url, { queryJson: { WorkOrder: self.currentItem.WorkOrder } }).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem.data = res.data.resultData.rows;
                } else {
                    backendService.genericError(res.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_5'));
                }
            })
        }

        //删除 事件
        function deleteOperation() {
            if (!self.selectedItem) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_6'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_5'));
                return;
            }
            var title = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_7');
            var text = commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_8');
            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/RemovePLOperationAndAttr';
                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItem
                };
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridData();
                        self.selectedItem = null;
                        self.isVisble = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                    }
                }, function (error) {

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_9'));
                });
            }, title);
        }

        function initGridOptionsItem2() {
            self.gridOptionsItem2 = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
                rowHeight: 35,
                multiSelect: false,
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
                        field: 'AttrCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_14'),
                        width: 120,

                    },
                    {
                        field: 'AttrName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_15'),
                        width: 120
                    },
                    {
                        field: 'AttrValue',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_16'),
                        width: 120,
                        cellTemplate: '<sit-text sit-value="row.entity.AttrValue" ></sit-text>'
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {

                        } else {

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

        function initGridDataItem2() {
            var url = commonService.getMesApiAddress("plan") + 'PL_ProcessOfOperationsAttr/GetPLProcessOfOperationsAttr';

            self.searchParams2.OperationsId = self.selectedItem.Id;

            let queryParams = {
                queryJson: self.searchParams2
            }
            commonService.callWebApiPost(url, queryParams).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem2.data = res.data.resultData;
                } else {
                    backendService.genericError(res.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_5'));
                }
            })
        }

        //保存属性
        function saveAttr() {

            var rows = self.gridOptionsItem2.data;
            if (rows.length == 0) {
                //没有要保存的行
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_17'));
                return;
            }
            var postData = {
                data: rows
            };
            var url = commonService.getMesApiAddress("plan") + 'PL_ProcessOfOperationsAttr/SaveBatchPLProcessOfOperationsAttr';
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    commonService.showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_13'));
                } else {
                    commonService.showWarning(res.data.returnMsg);
                }
            })
        }

        function save() {

            var rows = self.gridOptionsItem.data;
            if (rows.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_10'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_5'));
                busyIndicatorService.hide();
                return;
            }

            var arr = Array.from(new Set(rows.map(e => e["SN"])))

            if (arr.length != rows.length) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_11'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_5'));
                busyIndicatorService.hide();
                return;
            }
            var postData = {
                KeyValue: self.currentItem.Id,
                WorkOrder: self.currentItem.WorkOrder,
                data: rows
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_12') });
            var url = commonService.getMesApiAddress("plan") + 'PL_BOM/SaveWorkOrderOperations';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_13'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_5'));
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
            name: screenStateName + '.select',
            url: '/select/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrder-select.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderManage.selectJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
