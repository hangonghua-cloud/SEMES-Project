(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckList.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions1();
            initGridOptions2();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;
            self.selectedItem1 = null;
            self.selectedItem2 = null;
            self.IsShowButten = false;
            self.IsShowDeleteButten = false;
            self.searchParams = {};
            self.TestDepartment = "";


            initDictionary();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.searchButtonHandler = searchButtonHandler;
            self.testDepartmentChange = testDepartmentChange;
            self.typeFactoryChange = typeFactoryChange;

        }


        function initDictionary() {
            self.typeSelect = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ItemValue: "" }]
            }

            self.typeWhsCode = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ResourceCode: "" }]
            };
            self.typeTestDepartment = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ItemValue: "" }]
            };
            self.typeTestResult = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ItemValue: "" }]
            };
            self.typeIsFrozen = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ItemValue: "" }]
            };
            // commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeWhsCode.options = res.data.resultData;
            //         self.typeWhsCode.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2')
            //         });
            //     }
            // });
            commonService.getDataItemDuatil("AssayDepartment").then(function (res) {
                if (res && res.data.success) {
                    self.typeTestDepartment.options = res.data.resultData;
                    self.typeTestDepartment.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("QualityJudgement").then(function (res) {
                if (res && res.data.success) {
                    self.typeTestResult.options = res.data.resultData;
                    self.typeTestResult.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("FrozenStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeIsFrozen.options = res.data.resultData;
                    self.typeIsFrozen.value = self.typeIsFrozen.options.find(t => t.ItemValue == "0");//默认不冻结
                }
            })
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2')
                    });
                    initGridData1();
                }
            });

        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getWarehouseByFactory({ factoryCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeWhsCode.options = res.data.resultData;
                        self.typeWhsCode.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2')
                        });
                    }
                });
            } else {
                self.typeWhsCode = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ResourceCode: "" }]
                };
            }
        }

        function initGridOptions1() {
            self.gridOptionsItem1 = {
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
                enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_5'),
                        width: 110
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_6'),
                        width: 110
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_7'),
                        width: 110
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_8'),
                        width: 110
                    },


                    {
                        field: 'SupplierName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_9'),
                        width: 250
                    },

                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        debugger
                        if (row && row.isSelected == true) {
                            self.selectedItem1 = row.entity;
                            // initGirdData2();
                        } else {
                            self.selectedItem1 = null;
                        }
                        self.gridOptionsItem2.data = [];
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

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_10'))
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.WhsCode = self.typeWhsCode.value.ResourceCode;

            var postData = {
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + "MM_RawMaterialStock/GetPageDataTableCheck";
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem1.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem1.data = []
                }
            })
        }
        function searchButtonHandler() {
            initGridData1();
        }


        function initGridOptions2() {
            self.gridOptionsItem2 = {
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
                    //     name: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_11'), field: 'operation', enableFiltering: false, enableSorting: false, enableColumnMenu: false,
                    //     cellTemplate: '<div style="text-align:center;"><button ng-show="!row.entity.addrow" title="添加" ng-click="grid.appScope.addrow(row.entity)"><span class="glyphicon glyphicon-plus"></span></button>' + ' ' +
                    //         '<button ng-show="!row.entity.editrow" title="删除" ng-click="grid.appScope.delete(row.entity)"><span class="glyphicon glyphicon-trash"></span></button>' + ' ' +
                    //         '</div>', width: 80
                    // },
                    {
                        field: 'TestDepartment',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_12'),
                        width: 120,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'1\'"><span ng-cell-text>实验室</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'2\'"><span ng-cell-text>质量部</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'3\'"><span ng-cell-text>生产部</span></div>'
                    },
                    {
                        field: 'TestItemCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_13'),
                        width: 140
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_14'),
                        width: 140
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_15'),
                        width: 140
                    },
                    {
                        field: 'ItemValue',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_16'),
                        cellTemplate: '<div ng-if="row.entity.DataType==1"><sit-numeric sit-validation="{required: true}"  sit-value="row.entity.ItemValue" ></sit-numeric></div>' +
                            '<div ng-if="row.entity.DataType==2"><sit-text sit-value="row.entity.ItemValue" sit-validation="{required: true}"> </sit-text></div>' +
                            '<div ng-if="row.entity.DataType==3"><sit-date-time-picker sit-value="row.entity.ItemValue"' +
                            'sit-format="\'yyyy-MM-dd HH:mm:ss\'"' +
                            'sit-show-button-bar="true"' +
                            'sit-show-weeks="false"' +
                            'sit-validation="{required: true}"></sit-date-time-picker></div>' +
                            '<div ng-if="row.entity.DataType>3"><sit-select sit-value="row.entity.typeResultSelect.value"' +
                            'sit-validation="{required: true}"' +
                            'sit-options="row.entity.typeResultSelect.options"' +

                            'sit-to-display="\'ItemName\'"' +
                            'sit-to-keep="\'ItemValue\'">' +
                            '</sit-select></div>',
                        width: 300
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    // gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                    //     if (row && row.isSelected == true) {
                    //         self.selectedItem = row.entity;
                    //         //setButtonsVisibility(true);
                    //     } else {
                    //         self.selectedItem = null;
                    //         //setButtonsVisibility(false);
                    //     }
                    // });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }

        function initGirdData2(testDepartment) {

            //查询检验项目
            // debugger
            self.selectedItem1.TestDepartment = self.TestDepartment;

            var url = commonService.getMesApiAddress("quality") + "QC_TestMethodMaterial/QC_TestMethodItemMaintenanceMaterial";
            commonService.callWebApiPost(url, self.selectedItem1).then(function (res) {
                if (res && res.data.success) {
                    var data = res.data.resultData;
                    data.forEach(item => {
                        if (item.DataType > 3) {
                            self.typeSelect.options = [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2'), ItemValue: "" }];
                            var arr = item.DataTypeName.split("/");
                            for (var i = 0; i < arr.length; i++) {
                                self.typeSelect.options.push({
                                    ItemName: arr[i], ItemValue: i
                                })
                            }
                            item.typeResultSelect = angular.copy(self.typeSelect);
                        }
                        item.FactoryCode = self.selectedItem1.FactoryCode;
                        item.FactoryName = self.selectedItem1.FactoryName;
                    });
                    self.gridOptionsItem2.data = data;
                } else {
                    self.gridOptionsItem2.data = []
                }
            })

        }

        function testDepartmentChange(oldval, newval) {
            self.TestDepartment = newval.ItemValue;
            if (!!self.selectedItem1 && self.TestDepartment) {
                initGirdData2();
            } else {
                self.gridOptionsItem2.data = [];
            }

        }



        function save() {

            var itemvalueisnull = "";

            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            var data = self.gridOptionsItem2.data;

            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_17'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_18'));
                return false;
            }
            if (!!self.typeTestDepartment.value.ItemValue)

                data.filter(function (item, index, array) {
                    return item.TestDepartment == self.typeTestDepartment.value.ItemValue;
                })
            debugger
            data.forEach(item => {


                if (item.DataType == "3") {
                    item.ItemValue = commonService.ConvertToLocalTime(item.ItemValue);
                }
                if (item.DataType > 3 && item.typeResultSelect.value) {
                    item.ItemValue = item.typeResultSelect.value.ItemName;
                }

                if (item.ItemValue == undefined) {

                    backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_19'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_18'));
                    itemvalueisnull = 1;
                } else if (item.ItemValue == commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_2')) {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_19'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_18'));
                    itemvalueisnull = 1;
                }
            });

            if (itemvalueisnull == '1') {
                return false;
            }
            self.currentItem.FactoryCode = self.selectedItem1.FactoryCode;
            self.currentItem.FactoryName = self.selectedItem1.FactoryName;
            self.currentItem.TestMethodId = data[0].TestMethodId;
            self.currentItem.TestDepartment = self.typeTestDepartment.value.ItemValue;

            self.currentItem.TestResult = self.typeTestResult.value.ItemValue;
            self.currentItem.BatchNo = self.selectedItem1.BatchNo;
            self.currentItem.WhsCode = self.selectedItem1.WhsCode;
            self.currentItem.MaterialCode = self.selectedItem1.MaterialCode;
            self.currentItem.MaterialName = self.selectedItem1.MaterialName;
            self.currentItem.SmallClass = self.selectedItem1.SmallClass;


            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: "",
                IsFrozen: self.typeIsFrozen.value.ItemValue,
                Entity: self.currentItem,//检测方法Id
                data: data
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_20') });
            var url = commonService.getMesApiAddress("quality") + 'QC_MaterialInventoryCheck/SaveBatchQC_MaterialInventoryCheck';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

            busyIndicatorService.hide();
        }

        //取消
        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_21'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_18'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_18'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function onPropertyGridValidityChange(event, params) {
            if (params.id == "add_form") {
                self.validInputs = params.validity;
            }
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_QC_MaterialInventoryCheckList_QC_MaterialInventoryCheckList';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_MaterialInventoryCheckList';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/QC_MaterialInventoryCheckList-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListaddctrl.Tips_22'
            }
        };
        $stateProvider.state(state);
    }
}());
