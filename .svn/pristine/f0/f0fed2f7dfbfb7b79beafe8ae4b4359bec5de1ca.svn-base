(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderDismantle').config(PrintScreenStateConfig);

    PrintScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderDismantle.WorkOrderDismantle.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function PrintScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth,
        notificationService, busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions();
            setTimeout(function () {
                initGridData();
            }, 100);//如果查询条件有下拉参数，请调整此值到1000

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_1'));
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

            self.selectedItem = {};
            self.validInputs = false;
            initDictionary();
            //Expose Model Methods
            self.print = print;
            self.cancel = cancel;
        }

        function initDictionary() {


        }


        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function initGridOptions() {
            self.gridOptions = {
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
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_2'),
                        width: 110
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_3'),
                        width: 150
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_4'),
                        width: 110
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_5'),
                        width: 110
                    },
                    {
                        field: 'LocationCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_6'),
                        width: 110
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_7'),
                        width: 100
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_8'),
                        width: 70
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
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: self.currentItem.data
            };
        }

        //子表查询方法,数据绑定
        function initGridData() {
            self.selectedItem = null;
            self.isButtonVisible = false;

            let data = {
                exeWorkOrder: self.currentItem.ExeWorkOrder
            };
            var url = commonService.getMesApiAddress("plan") + 'PL_PlanStoreIssue/GetPrintInfo';
            commonService.callWebApiPost(url, data).then(function (res) {
                if ((res) && (res.data.success)) {
                    //数据
                    self.gridOptions.data = res.data.resultData;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_9'));
            });
        }

        //打印
        function print() {

            let selectedRows = $scope.gridApi.selection.getSelectedRows();
            if (selectedRows.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_10'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_11'));
                return false;
            }

            let arrId = selectedRows.map(item => {
                return item.Id;
            })

            var postData = {
                arrId: arrId,
                exeWorkOrder: self.currentItem.ExeWorkOrder
            }
            // busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_12') });
            var url = commonService.getMesApiAddress("plan") + 'PL_PlanStoreIssue/Print';
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
                // commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_13'));
                //刷新局部
                // debugger;
                $rootScope.$emit('to-parentPrint', data.data.resultData);
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_11'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_11'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    PrintScreenStateConfig.$inject = ['$stateProvider'];
    function PrintScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderDismantle_WorkOrderDismantle';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderDismantle';

        var state = {
            name: screenStateName + '.print',
            url: '/print',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrderDismantle-print.html',
                    controller: PrintScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderDismantle.printJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
