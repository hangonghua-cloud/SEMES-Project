(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.BOM').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.BOM.BOM.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope,
        commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.materialChange = materialChange;
            initDictionary();
            materialChange(null, self.currentItem.MaterialCode);
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function initDictionary() {
            //初始化 物料分类
            self.MaterialClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), ItemValue: "" }]
            };

            //初始化 物料小类
            self.SmallClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), ItemValue: "" }]
            };

            self.typeUnit = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), ItemValue: "" }]
            };


            self.Warehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), ResourceCode: "" }]
            };

            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), ResourceCode: "" }]
            };
            self.TypeBom = {
                value: { BOMCode: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), Id: "" },
                options: [{ BOMCode: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), Id: "" }]
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

            //根据工厂获取仓库
            commonService.getWarehouseByFactory({ factoryCode: self.currentItem.FactoryCode }).then(function (res) {
                if (res && res.data.success) {
                    self.Warehouse.options = res.data.resultData;
                    self.Warehouse.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2')
                    });
                    self.Warehouse.value = self.Warehouse.options.find(t => t.ResourceCode == self.currentItem.Warehouse);
                }
            });
            // 获取工序
            commonService.getProcessByFactory({ LevelCode: self.currentItem.FactoryCode }).then(function (res) {
                if (res && res.data.success) {
                    self.Process.options = res.data.resultData;
                    self.Process.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2')
                    });
                    self.Process.value = self.Process.options.find(t => t.ResourceCode == self.currentItem.ProcessCode);
                }
            });

        }
        function materialChange(oldvalu, newvalue) {
            //获取物料基本信息
            var url1 = commonService.getMesApiAddress("material") + 'Base_Material/GetDataTable_TestOtherEntity?checkType=' + newvalue;
            commonService.callWebApiGet(url1, null).then(function (res) {
                if (res && res.data.success && res.data.resultData.length > 0) {
                    var ent = res.data.resultData[0];
                    self.currentItem.MaterialName = ent.MaterialName;
                    //self.currentItem.Spec=ent.Spec;
                    self.MaterialClass.value = self.MaterialClass.options.find(t => t.ItemValue == ent.MaterialClass);
                    self.SmallClass.value = self.SmallClass.options.find(t => t.ItemValue == ent.SmallClass);
                    self.typeUnit.value = self.typeUnit.options.find(t => t.ItemValue == ent.Unit);
                } else {
                    self.currentItem.MaterialClass = "";
                    self.currentItem.MaterialName = "";
                    self.MaterialClass.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), ItemValue: "" };
                    self.SmallClass.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), ItemValue: "" };
                    self.typeUnit.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), ItemValue: "" };
                }
            });

            //获取物料BOM信息
            var url2 = commonService.getMesApiAddress("material") + 'BS_BOM/GetBS_BOMList?checkType=' + newvalue;
            commonService.callWebApiGet(url2, null).then(function (res) {
                if (res && res.data.success) {
                    self.TypeBom.options = res.data.resultData;
                    self.TypeBom.options.splice(0, 0, {
                        Id: "",
                        BOMCode: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2')
                    });
                    if (!!self.currentItem.BOMCode) {
                        self.TypeBom.value = self.TypeBom.options.find(t => t.BOMCode == self.currentItem.BOMCode);
                    }
                } else {
                    self.TypeBom.value = { BOMCode: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_2'), Id: "" };
                }
            });

        }


        function save() {

            if (!self.currentItem.MaterialName) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_3'));
                return;
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_4') });

            if (!!self.typeUnit.value) self.currentItem.Unit = self.typeUnit.value.ItemValue;
            if (!!self.Warehouse.value) self.currentItem.Warehouse = self.Warehouse.value.ResourceCode;
            if (!!self.Process.value) self.currentItem.ConsumeProcess = self.Process.value.ResourceCode;
            if (!!self.TypeBom.value && self.TypeBom.value.Id != "") {
                self.currentItem.BOMCode = self.TypeBom.value.BOMCode;
            }

            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("material") + 'BS_BOMItems/SaveBS_BOMItems';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_5'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_6'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_BOM_BOM';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/BOM';

        var state = {
            name: screenStateName + '.editDetail',
            url: '/editDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/BOM-editDetail.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.BOM.BOMeditDetailctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
