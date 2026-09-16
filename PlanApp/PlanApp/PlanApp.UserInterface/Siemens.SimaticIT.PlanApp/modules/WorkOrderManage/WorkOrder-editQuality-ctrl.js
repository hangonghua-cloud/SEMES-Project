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
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_1'));
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
            self.processData = [];

            initDictionary();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.ChildClick = ChildClick;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function initDictionary() {

            self.typeFirstCheck = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_2'), ItemValue: true },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_3'), ItemValue: true },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_4'), ItemValue: false },
                ]
            };
            self.typeConfirm = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_5'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_5'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("Process").then(function (res) {
                if (res && res.data.success) {
                    self.typeConfirm.options = res.data.resultData;
                    self.typeConfirm.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })

        }

        function ChildClick() {

            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("factory") + 'level/Get_ModelResourceExtendInfo_ByLevelCode',
                            method: "Post",
                            queryParmeters: {
                                Name: "",
                                LevelCode: "Process"
                            },
                            pagination: null,
                            multiple: true,
                            sidx: "ResourceCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_6'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ResourceCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_7'),
                                    width: 200
                                },
                                {
                                    field: 'ResourceName',
                                    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_8'),
                                    width: 200
                                },
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                var name = ""
                if ((!data || data.length <= 0)) {
                    yoti.warn(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_9'));
                } else {
                    console.log(data);
                    data.forEach((item, index, arr) => {
                        name += item.ResourceName + ",";
                    });
                    self.currentItem.FirstInspectionOperation = name.substring(0, name.length - 1);
                    self.processData = data;
                }
            });
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
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_11'),
                        width: 110
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_12'),
                        width: 110
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_13'),
                        width: 110
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_14'),
                        width: 110
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_15'),
                        width: 110
                    },
                    {
                        field: 'OrderPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_16'),
                        width: 110
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_17'),
                        width: 110
                    },
                    {
                        field: 'StartOperation',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_18'),
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.StartOperation==\'JC\'"><span ng-cell-text>挤出</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.StartOperation==\'BZ\'"><span ng-cell-text>包装</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.StartOperation==\'KC\'"><span ng-cell-text>开槽</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.StartOperation==\'4\'"><span ng-cell-text></span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.StartOperation==\'5\'"><span ng-cell-text></span></div>'
                    },
                    {
                        field: 'TransferBy',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_22'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TransferBy==\'1\'"><span ng-cell-text>按柜</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TransferBy==\'2\'"><span ng-cell-text>按托</span></div>'
                    },
                    {
                        field: 'Yield',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_25'),
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
            debugger;
            let rows = [];
            let inspctionData = [];
            //字典类型 取值参考
            self.currentItem.FirstInspectionConfirm = self.typeFirstCheck.value.ItemValue;

            let copyData = $scope.gridApi.selection.getSelectedRows();
            if (copyData.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_26'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_27'));
                busyIndicatorService.hide();
                return false;
            }
            //缺少确认工序
            copyData.forEach((item) => {
                rows.push({
                    Id: item.WorkOrderId,
                    FirstInspectionConfirm: self.currentItem.FirstInspectionConfirm,
                    FirstInspectionOperation: self.currentItem.FirstInspectionOperation
                })

                self.processData.forEach((item1, index1, arr1) => {
                    inspctionData.push({
                        ProductOrder: item.ProductOrder,
                        WorkOrder: item.WorkOrder,
                        ContainerNO: item.ContainerNO,
                        MaterialCode: item.MaterialCode,
                        Process: item1.ResourceCode,
                        MMXH: item.MMXH
                    });
                })

            });
            var postData = {
                KeyValue: self.currentItem.Id,
                data: rows,
                inspctionData: inspctionData
            };
            console.log(postData);
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_28') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_29'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_27'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_27'));
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
            name: screenStateName + '.editQuality',
            url: '/editQuality/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrder-editQuality.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderManage.editquaJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
