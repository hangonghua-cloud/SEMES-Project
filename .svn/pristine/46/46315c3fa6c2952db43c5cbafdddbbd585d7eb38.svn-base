(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.OQCManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheck.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            init();
            initGridOptions()
            initGridData();

            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_1'));
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;
            self.currentItem = angular.copy($stateParams.selectedItem);

            self.validInputs = false;

            self.typeSelect = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_2'), ItemValue: "" }]
            }

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
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
                enableSelectAll: false,
                enableRowSelection: false,
                //enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    // {
                    //     name: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_3'), field: 'operation', enableFiltering: false, enableSorting: false, enableColumnMenu: false,
                    //     cellTemplate: '<div style="text-align:center;"><button ng-show="!row.entity.addrow" title="添加" ng-click="grid.appScope.addrow(row.entity)"><span class="glyphicon glyphicon-plus"></span></button>' + ' ' +
                    //         '<button ng-show="!row.entity.editrow" title="删除" ng-click="grid.appScope.delete(row.entity)"><span class="glyphicon glyphicon-trash"></span></button>' + ' ' +
                    //         '</div>', width: 80
                    // },
                    // {
                    //     field: 'MaterialName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_4'),
                    //     enableCellEdit: false,
                    //     cellTemplate: '<div><div ng-click="grid.appScope.cellClicked(row.entity,col)" class="ui-grid-cell-contents" style="height:35px" title="TOOLTIP">{{COL_FIELD CUSTOM_FILTERS}}</div></div>',
                    //     width: 90
                    // },
                    {
                        field: 'TestItemCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_5'),
                        width: 130
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_6'),
                        width: 130
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_7'),
                        width: 180
                    },
                    {
                        field: 'CheckResult',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_8'),
                        cellTemplate: '<div ng-if="row.entity.DataType==1"><sit-numeric  sit-value="row.entity.CheckResult" ></sit-numeric></div>' +
                            '<div ng-if="row.entity.DataType==2"><sit-text sit-value="row.entity.CheckResult" ></sit-text></div>' +
                            '<div ng-if="row.entity.DataType==3"><sit-date-time-picker sit-value="row.entity.CheckResult"' +
                            'sit-format="\'yyyy-MM-dd HH:mm:ss\'"' +
                            'sit-show-button-bar="true"' +
                            'sit-show-weeks="false"' +
                            'sit-validation="{required: false}"></sit-date-time-picker></div>' +
                            '<div ng-if="row.entity.DataType>3"><sit-select sit-value="row.entity.typeResultSelect.value"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.typeResultSelect.options"' +

                            'sit-to-display="\'ItemName\'"' +
                            'sit-to-keep="\'ItemValue\'">' +
                            '</sit-select></div>',
                        width: 230
                    },
                    {
                        field: 'Distinguish',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_9'),
                        width: 140,
                        cellTemplate: '<div><sit-text  sit-value="row.entity.Distinguish" ></sit-text></div>'
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
                data: []
            };
        }

        function initGridData() {

            if (!self.currentItem.OQCCheckConfigId) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_10'), commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_11'));
                return
            }

            var postdata = {
                queryJson: {
                    OQCCheckConfigId: self.currentItem.OQCCheckConfigId,
                    TestDepartment: "1"
                }
            }

            var url = commonService.getMesApiAddress("quality") + 'QC_OQCCheckConfigItem/QC_OQCCheckConfigItemPageDataTableList';
            var req = commonService.callWebApiPost(url, postdata).then(function (res) {
                if (res && res.data.success) {

                    var data = res.data.resultData.rows;

                    var postdata2 = {
                        queryJson: {
                            OQCQualityCheckId: self.currentItem.Id,
                            TestDepartment: "1"
                        }
                    }
                    var url1 = commonService.getMesApiAddress("quality") + 'QC_OQCQualityCheckItem/QC_OQCQualityCheckItemPageDataTableList';

                    commonService.callWebApiPost(url1, postdata2).then(function (result) {

                        if (result && result.data.success) {
                            data.forEach(item => {
                                //实验室赋值
                                var ent = result.data.resultData.rows.find(t => t.TestItemCoading == item.TestItemCoading);
                                if (ent != null) {
                                    item.Distinguish = ent.Distinguish;
                                    if (item.DataType == "1") item.CheckResult = parseFloat(ent.CheckResult);
                                    else item.CheckResult = ent.CheckResult;
                                }
                                //实验室的不能录入
                                if (item.DataType > 3) {
                                    self.typeSelect.options = [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_2'), ItemValue: "" }];
                                    var arr = item.DataTypeName.split("/");
                                    for (var i = 0; i < arr.length; i++) {
                                        self.typeSelect.options.push({
                                            ItemName: arr[i], ItemValue: i
                                        })
                                    }
                                    if (!!item.CheckResult) {
                                        self.typeSelect.value = self.typeSelect.options.find(t => t.ItemName == item.CheckResult);
                                    }
                                    item.typeResultSelect = angular.copy(self.typeSelect);
                                }
                            });
                        } else {
                            data.forEach(item => {
                                if (item.DataType > 3) {
                                    self.typeSelect.options = [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_2'), ItemValue: "" }];
                                    var arr = item.DataTypeName.split("/");
                                    for (var i = 0; i < arr.length; i++) {
                                        self.typeSelect.options.push({
                                            ItemName: arr[i], ItemValue: i
                                        })
                                    }
                                    item.typeResultSelect = angular.copy(self.typeSelect);
                                }
                            });
                        }
                        self.gridOptionsItem.data = data;
                    })
                } else {
                    self.gridOptionsItem.data = []
                }
            });
        }


        //保存
        function save() {

            var data = self.gridOptionsItem.data;
            data.forEach(item => {

                if (item.DataType == "3") { //时间
                    item.CheckResult = commonService.ConvertToLocalTime(item.CheckResult);
                }
                // 下拉框
                if (item.DataType > 3 && item.typeResultSelect.value.ItemValue >= 0) {
                    item.CheckResult = item.typeResultSelect.value.ItemName;
                }
                item.FactoryCode = self.currentItem.FactoryCode;
                item.FactoryName = self.currentItem.FactoryName;
            })


            var postData = {
                TestDepartment: "1",
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem,
                data: data
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_12') });
            var url = commonService.getMesApiAddress("quality") + 'QC_OQCQualityCheckItem/SaveBatchQC_OQCQualityCheckItem';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_13'));
                //刷新局部
                let callData = {
                    dept: self.currentItem.TestDepartment,
                    testResult: self.currentItem.TestResult
                };
                //$rootScope.$emit('to-detail', callData);
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_11'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_11'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_OQCManage_OQCQualityCheck';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/OQCManage';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/OQCQualityCheck-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.OQCManage.OQCQualityCheckaddctrl.Tips_14'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
