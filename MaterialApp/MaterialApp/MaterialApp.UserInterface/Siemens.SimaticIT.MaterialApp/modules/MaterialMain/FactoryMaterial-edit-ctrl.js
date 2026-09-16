(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MaterialMain').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterial.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;


        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.typeFactoryChange = typeFactoryChange;
            self.materialChange = materialChange;
            self.processRouteModal = processRouteModal;//工艺路线弹窗
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function initDictionary() {
            //初始化 物料分类
            self.MaterialClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ItemValue: "" }]
            };

            //初始化 物料小类
            self.SmallClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ItemValue: "" }]
            };
            self.typeIsExemption = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_3'), ItemValue: "0" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_3'), ItemValue: "0" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_4'), ItemValue: "1" },
                ]
            };
            self.typeIsExemption.value = self.typeIsExemption.options.find(t => t.ItemValue == self.currentItem.IsExemption);
            self.typeUnit = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ItemValue: "" }]
            };


            self.Warehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ResourceCode: "" }]
            };

            self.ProcureType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ItemValue: "" }]
            };

            //初始化 物料小类
            self.ProcessRoute = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ProcessCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ProcessCode: "" }]
            };
            //初始化 物料小类
            self.IsEnabled = {
                value: { ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2') },
                options: [
                    { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_4') },
                    { ItemValue: false, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_3') }
                ]
            };
            self.IsEnabled.value = self.IsEnabled.options.find(t => t.ItemValue == self.currentItem.IsUsed);
            self.Factory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ResourceCode: "" }]
            };
            commonService.getDataItemDuatil("MaterialType").then(function (res) {
                if (res && res.data.success) {
                    self.MaterialClass.options = res.data.resultData;
                    // self.MaterialClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                    self.MaterialClass.value = self.MaterialClass.options.find(t => t.ItemValue == self.currentItem.MaterialClass);
                }
            })
            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.SmallClass.options = res.data.resultData;
                    // self.SmallClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                    self.SmallClass.value = self.SmallClass.options.find(t => t.ItemValue == self.currentItem.SmallClass);
                }
            })
            commonService.getDataItemDuatil("Unit").then(function (res) {
                if (res && res.data.success) {
                    self.typeUnit.options = res.data.resultData;
                    self.typeUnit.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                    self.typeUnit.value = self.typeUnit.options.find(t => t.ItemValue == self.currentItem.Unit);
                }
            })
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2')
                    });
                    self.Factory.value = self.Factory.options.find(t => t.ResourceCode == self.currentItem.FactoryCode);
                }
            });
            commonService.getDataItemDuatil("ProcureType").then(function (res) {
                if (res && res.data.success) {
                    self.ProcureType.options = res.data.resultData;
                    self.ProcureType.value = self.ProcureType.options.find(t => t.ItemValue == self.currentItem.ProcureType);
                }
            })
            //物料属性模板
            self.typeAttr = {
                value: null,
                options: []
            }
            //获取属性模板

            var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTemp/GetBase_MaterialBindTempList?checkType=';
            var req = commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.typeAttr.options = res.data.resultData;
                    self.typeAttr.options.splice(0, 0, {
                        TempCode: "",
                        TempName: "--请选择--"
                    });
                    self.typeAttr.value = self.typeAttr.options.find(t => t.TempCode == self.currentItem.TemplateCode);
                } else {
                    self.typeAttr.options = [];
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
                            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2')
                        });
                        self.Warehouse.value = self.Warehouse.options.find(t => t.ResourceCode == self.currentItem.Warehouse);
                    }
                });
                //工艺路线
                var url = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=" + newItem.ResourceCode;
                commonService.callWebApiGet(url, null).then(function (res) {
                    if (res && res.data.success) {
                        self.ProcessRoute.options = res.data.resultData;
                        self.ProcessRoute.options.splice('0', '0', {
                            ProcessCode: "",
                            ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2')
                        });
                        self.ProcessRoute.value = self.ProcessRoute.options.find(t => t.ProcessCode == self.currentItem.ProcessRoute);
                    }
                })
            } else {
                self.Warehouse = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ResourceCode: "" }]
                };
                self.ProcessRoute = {
                    value: { ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ProcessCode: "" },
                    options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ProcessCode: "" }]
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
                    self.currentItem.Spec = item.Spec;
                    self.MaterialClass.value = self.MaterialClass.options.find(t => t.ItemValue == item.MaterialClass);
                    self.SmallClass.value = self.SmallClass.options.find(t => t.ItemValue == item.SmallClass);
                    self.typeUnit.value = self.typeUnit.options.find(t => t.ItemValue == item.Unit);

                } else {
                    self.currentItem.MaterialName = "";
                    self.currentItem.Spec = "";
                    self.MaterialClass.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ItemValue: "" };
                    self.SmallClass.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ItemValue: "" };
                    self.typeUnit.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_2'), ItemValue: "" };
                }
            });
        }
        //选择工艺路线
        function processRouteModal() {
            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=" + self.currentItem.FactoryCode,
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
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ProcessCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_6'),
                                    width: 200
                                },
                                {
                                    field: 'ProcessName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_7'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_8'));
                } else {
                    self.currentItem.ProcessRoute = data[0].ProcessCode;
                    self.currentItem.ProcessRouteName = data[0].ProcessName;
                }
            });

        }

        //保存
        function save() {

            if (!self.currentItem.MaterialName) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_9'));
                return;
            }

            self.currentItem.FactoryCode = self.Factory.value.ResourceCode;
            if (!!self.Warehouse.value) self.currentItem.Warehouse = self.Warehouse.value.ResourceCode;
            if (!!self.ProcureType.value) self.currentItem.ProcureType = self.ProcureType.value.ItemValue;
            // if (!!self.ProcessRoute.value) self.currentItem.ProcessRoute = self.ProcessRoute.value.ProcessCode;
            self.currentItem.IsUsed = self.IsEnabled.value.ItemValue;
            self.currentItem.IsExemption = self.typeIsExemption.value.ItemValue;
            self.currentItem.TemplateCode = self.typeAttr.value.TempCode;

            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_10') });

            var url = commonService.getMesApiAddress("material") + 'Base_MaterialFactory/SaveBase_MaterialFactory';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_11'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_12'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_12'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MaterialMain_FactoryMaterial';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MaterialMain';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/FactoryMaterial-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
