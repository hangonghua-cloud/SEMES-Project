(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderSort').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderSort.WorkOrderSort.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService,
        auth, notificationService, busyIndicatorService, $modal, $interval) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions();
            initGridData();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.IsShowDelete = false;
            self.isReadField = true;

            initDictionary();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.addForm = addForm;
            self.deleteForm = deleteForm;
        }
        function initDictionary() {
            self.IsAsc = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_2'), ItemCode: true },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_2'), ItemCode: true },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_3'), ItemCode: false },
                ]
            }
            self.FieldName = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_4'), ItemCode: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_4'), ItemCode: "" }]
            }


            commonService.getKeyParameterItem({ EnCode: "WorkOderSortColumn" }).then(function (res) {
                if (res && res.data.success) {
                    self.FieldName.options = res.data.resultData;
                    self.FieldName.options.splice(0, 0, {
                        ItemCode: "",
                        ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_4')
                    });

                }
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function addForm() {

            if (!self.currentItem || !self.currentItem.RuleCode || !self.currentItem.RuleName
                || self.FieldName.value.column == ""
            ) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_5'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_6'));
                return false
            }
            self.isReadField = true;
            var rows = angular.copy(self.gridOptionsItem.data);
            if (rows.length > 0 && rows.find(t => t.FieldCode == self.FieldName.value.ItemCode) != null) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_7'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_6'));
                return false
            }
            rows.push({
                RuleCode: self.currentItem.RuleCode,
                RuleName: self.currentItem.RuleName,
                FieldCode: self.FieldName.value.ItemCode,
                FieldName: self.FieldName.value.ItemName,
                IsAsc: self.IsAsc.value.ItemCode,
                SortCode: self.currentItem.SortCode

            });
            self.gridOptionsItem.data = rows;
        }
        function deleteForm() {

            if (self.selectedItem == null || self.gridOptionsItem.data.length == 1) {
                self.isReadField = false;
                self.IsShowDelete = false;
            };
            self.gridOptionsItem.data = _.filter(self.gridOptionsItem.data, function (item) {
                return item.FieldCode != self.selectedItem.FieldCode;
            })
        }
        function initGridData() {
            var param = {
                queryJson: {
                    RuleCode: self.currentItem.RuleCode
                }
            }
            var url = commonService.getMesApiAddress("plan") + "PL_WorkOrderSortRule/PL_WorkOrderSortRulePageList";
            commonService.callWebApiPost(url, param).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem.data = res.data.resultData.rows
                } else {
                    self.gridOptionsItem.data = []
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
                        field: 'FieldName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_8'),
                        width: 180,
                    },
                    {
                        field: 'IsAsc',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_9'),
                        width: 180,
                        cellTemplate:
                            '<div class="ngCellText" style="padding-left:5px;height:30px;line-height:30px;" ng-if="row.entity.IsAsc==true">正序</div>' +
                            '<div class="ngCellText" style="padding-left:5px;height:30px;line-height:30px;" ng-if="row.entity.IsAsc!=true">倒序</div>'
                    },
                    {
                        field: 'SortCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_12'),
                        width: 180,
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem = row.entity;
                            //setButtonsVisibility(true);
                            self.IsShowDelete = true;
                        } else {
                            self.selectedItem = null;
                            self.IsShowDelete = false;
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

        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_13') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;

            if (self.gridOptionsItem.data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_14'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_6'));
                return false
            }

            var rows = self.gridOptionsItem.data;
            // var sortCode = 1;
            // rows.forEach((item, index, arr) => {
            //     item.SortCode = sortCode++;
            // });

            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem,
                data: rows
            };

            var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrderSortRule/SaveBatchPL_WorkOrderSortRule';

            //提交数据
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_15'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_6'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderSort_WorkOrderSort';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderSort';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrderSort-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderSort.editJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
