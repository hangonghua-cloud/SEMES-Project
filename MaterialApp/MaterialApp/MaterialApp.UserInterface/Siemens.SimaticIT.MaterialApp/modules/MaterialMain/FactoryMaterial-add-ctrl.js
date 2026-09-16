(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MaterialMain').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterial.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth,
        notificationService, busyIndicatorService, $modal, $interval) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            initGridOptions();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_1'));
            // sidePanelManager.open('e');
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;

            initDictionary();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.typeFactoryChange = typeFactoryChange;
            self.materialChange = materialChange;
            self.AttrChange = AttrChange;
            self.processRouteModal = processRouteModal;//工艺路线
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function initDictionary() {
            //初始化 物料分类
            self.MaterialClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ItemValue: "" }]
            };

            //初始化 物料小类
            self.SmallClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ItemValue: "" }]
            };
            self.typeIsExemption = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_3'), ItemValue: "0" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_3'), ItemValue: "0" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_4'), ItemValue: "1" },
                ]
            };


            self.typeUnit = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ItemValue: "" }]
            };


            self.Warehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ResourceCode: "" }]
            };

            self.ProcureType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ItemValue: "" }]
            };


            //初始化 物料小类
            self.IsEnabled = {
                value: { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_4') },
                options: [
                    { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_4') },
                    { ItemValue: false, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_3') }
                ]
            };
            self.Factory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ResourceCode: "" }]
            };
            self.ProcessRoute = {
                value: { ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ProcessCode: "" },
                options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ProcessCode: "" }]
            };
            commonService.getDataItemDuatil("MaterialType").then(function (res) {
                if (res && res.data.success) {
                    self.MaterialClass.options = res.data.resultData;
                    self.MaterialClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.SmallClass.options = res.data.resultData;
                    self.SmallClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("Unit").then(function (res) {
                if (res && res.data.success) {
                    self.typeUnit.options = res.data.resultData;
                    self.typeUnit.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            // commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.Warehouse.options = res.data.resultData;
            //         self.Warehouse.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2')
            //         });
            //     }
            // });
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.Factory.value = res.data.resultData[0];
                    }
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2')
                    });
                }
            });
            commonService.getDataItemDuatil("ProcureType").then(function (res) {
                if (res && res.data.success) {
                    self.ProcureType.options = res.data.resultData;
                    self.ProcureType.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })

            //物料属性模板
            self.Attr = {
                value: null,
                options: []
            }
            //获取属性模板

            var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTemp/GetBase_MaterialBindTempList?checkType=';
            var req = commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.Attr.options = res.data.resultData;
                    self.Attr.options.splice(0, 0, {
                        TempCode: "",
                        TempName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2')
                    });
                    //self.Attr.value = { TempCode: res.data.resultData[0].TempCode, TempName: res.data.resultData[0].TempName };
                } else {
                    self.Attr.options = [];
                }
            });
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                //仓库
                commonService.getWarehouseByFactory({ factoryCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.Warehouse.options = res.data.resultData;
                        self.Warehouse.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2')
                        });
                    }
                });
                //工艺路线
                var url = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=" + newItem.ResourceCode;
                commonService.callWebApiGet(url, null).then(function (res) {
                    if (res && res.data.success) {
                        self.ProcessRoute.options = res.data.resultData;
                        self.ProcessRoute.options.splice('0', '0', {
                            ProcessCode: "",
                            ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2')
                        });
                    }
                })
            } else {
                self.Warehouse = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ResourceCode: "" }]
                };
                self.ProcessRoute = {
                    value: { ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ProcessCode: "" },
                    options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ProcessCode: "" }]
                };
            }
        }

        function materialChange(oldval, newval) {
            var url = commonService.getMesApiAddress("material") + 'Base_Material/GetDataTable_TestOtherEntity?checkType=' + newval;
            commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success && res.data.resultData.length > 0) {
                    var item = res.data.resultData[0];
                    //self.currentItem.MaterialClassName = res.data.resultData[0].MaterialClassName;
                    //self.currentItem.MaterialClass = res.data.resultData[0].MaterialClass;
                    self.currentItem.MaterialName = item.MaterialName;
                    self.currentItem.MaterialId = item.Id;
                    self.currentItem.Spec = item.Spec;
                    self.MaterialClass.value = self.MaterialClass.options.find(t => t.ItemValue == item.MaterialClass);
                    self.SmallClass.value = self.SmallClass.options.find(t => t.ItemValue == item.SmallClass);
                    self.typeUnit.value = self.typeUnit.options.find(t => t.ItemValue == item.Unit);
                } else {
                    self.currentItem.MaterialName = "";
                    self.currentItem.Spec = "";
                    self.MaterialClass.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ItemValue: "" };
                    self.SmallClass.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ItemValue: "" };
                    self.typeUnit.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_2'), ItemValue: "" };
                }
            });
        }

        //下拉框改变事件
        function AttrChange(oldval, newval) {
            if (newval) initGridData(newval.Id);
        }

        //选择工艺路线
        function processRouteModal() {
            if (!self.Factory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_5'));
                return;
            }

            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=" + self.Factory.value.ResourceCode,
                            queryParmeters: {
                                Name: "",
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Get",
                            sidx: "ProcessCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_6'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ProcessCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_7'),
                                    width: 200
                                },
                                {
                                    field: 'ProcessName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_8'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_9'));
                } else {
                    self.currentItem.ProcessRoute = data[0].ProcessCode;
                    self.currentItem.ProcessRouteName = data[0].ProcessName;
                }
            });

        }

        //加载table
        function initGridData(id) {
            var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTempFacet/GetBase_MaterialBindTempFacetList?checkType=' + id;
            var req = commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem.data = res.data.resultData;
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
                excessRows: 100,
                columnDefs: [
                    // {
                    //     name: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_10'), field: 'operation', enableFiltering: false, enableSorting: false, enableColumnMenu: false,
                    //     cellTemplate: '<div style="text-align:center;"><button ng-show="!row.entity.addrow" title="添加" ng-click="grid.appScope.addrow(row.entity)"><span class="glyphicon glyphicon-plus"></span></button>' + ' ' +
                    //         '<button ng-show="!row.entity.editrow" title="删除" ng-click="grid.appScope.delete(row.entity)"><span class="glyphicon glyphicon-trash"></span></button>' + ' ' +
                    //         '</div>', width: 80
                    // },
                    // {
                    //     field: 'MaterialName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_11'),
                    //     enableCellEdit: false,
                    //     cellTemplate: '<div><div ng-click="grid.appScope.cellClicked(row.entity,col)" class="ui-grid-cell-contents" style="height:35px" title="TOOLTIP">{{COL_FIELD CUSTOM_FILTERS}}</div></div>',
                    //     width: 90
                    // },
                    {
                        field: 'AttrCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_12'),
                        width: 200,
                    },
                    {
                        field: 'AttrName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_13'),
                        width: 200
                    },

                    {
                        field: 'AttrTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_14'),
                        width: 200
                    },

                    {
                        field: 'AttrValue',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_15'),
                        cellTemplate: '<div ng-show="row.entity.AttrType==1"><sit-numeric  sit-value="row.entity.AttrValue" ></sit-numeric></div>' +
                            '<div ng-show="row.entity.AttrType==2"><sit-text sit-value="row.entity.AttrValue" ></sit-text></div>' +
                            '<div ng-show="row.entity.AttrType==3"><sit-date-time-picker sit-value="row.entity.AttrValue"' +
                            'sit-format="\'yyyy-MM-dd HH:mm:ss\'"' +
                            'sit-show-button-bar="true"' +
                            'sit-show-weeks="false"' +
                            'sit-validation="{required: false}"></sit-date-time-picker></div>',
                        width: 300
                    },
                    // {
                    //     field: 'MixTime',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_16'),
                    //     enableCellEdit: false,
                    //     cellFilter: 'date:\'yyyy-MM-dd HH:mm:ss\'',
                    //     width: 150,
                    //     cellTemplate:
                    //         '<div ng-show="row.entity.AttrType==3"><sit-date-time-picker sit-value="row.entity.MixTime"' +
                    //         'sit-format="\'yyyy-MM-dd HH:mm:ss\'"' +
                    //         'sit-show-button-bar="true"' +
                    //         'sit-show-weeks="false"' +
                    //         'sit-validation="{required: false}"></sit-date-time-picker></div>',
                    // },
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

        //保存
        function save() {
            if (!self.currentItem.MaterialName) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_17'));
                return;
            }

            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            self.currentItem.FactoryCode = self.Factory.value.ResourceCode;
            self.currentItem.Warehouse = self.Warehouse.value.ResourceCode;
            self.currentItem.ProcureType = self.ProcureType.value.ItemValue;
            // self.currentItem.ProcessRoute = self.ProcessRoute.value.ProcessCode;
            self.currentItem.IsUsed = self.IsEnabled.value.ItemValue;
            self.currentItem.IsExemption = self.typeIsExemption.value.ItemValue;
            self.currentItem.TemplateCode = self.Attr.value.TempCode;

            let itemData = angular.copy(self.gridOptionsItem.data);
            itemData.forEach((item, index, arr) => {
                if (item.AttrType == "3" && item.AttrValue) {//处理时间
                    item.AttrValue = $filter('date')(new Date(item.AttrValue), 'yyyy-MM-dd HH:mm:ss');
                }
                //item.MaterialId = self.currentItem.MaterialId;
            });

            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem,
                data: itemData
            };

            var url = commonService.getMesApiAddress("material") + 'Base_MaterialFactory/SaveBase_MaterialFactory';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_18') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_19'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_20'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_20'));
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MaterialMain_FactoryMaterial';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MaterialMain';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/FactoryMaterial-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
