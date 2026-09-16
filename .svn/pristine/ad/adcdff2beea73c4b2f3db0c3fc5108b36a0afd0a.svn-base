(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.OwnProduct').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.OwnProduct.OwnProductOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_1'));
            sidePanelManager.open("e");
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            initDictionary();

            self.typeFactoryChange = typeFactoryChange;
            self.processOperationChange = processOperationChange;
            self.materialClick = materialClick;
        }
        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_2'), ResourceCode: "" }]
            };
            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_2'), ResourceCode: "" }]
            };

            self.typeProcessOperation = {
                value: { ProcessName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_2'), ProcessCode: "" },
                options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_2'), ProcessCode: "" }]
            };

            self.typeBom = {
                value: { BOMCode: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_2'), Id: "" },
                options: [{ BOMCode: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_2'), Id: "" }]
            };


            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_2')
                    });
                }
            });
            // commonService.getDataItemDuatil("WorkOrderType").then(function (res) {
            //     if (res && res.data.success) {
            //         self.WorkOrderType.options = res.data.resultData;
            //         self.WorkOrderType.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
            //     }
            // })
            // commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeMaterialSmall.options = res.data.resultData;
            //         self.typeMaterialSmall.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
            //     }
            // })

            // var url = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=";
            // commonService.callWebApiGet(url, null).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeProcessOperation.options = res.data.resultData;
            //         self.typeProcessOperation.options.splice('0', '0', {
            //             ProcessCode: "",
            //             ProcessName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_2')
            //         });
            //     }
            // })
        }

        function typeFactoryChange(oldItem, newItem) {
            commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeProcess.options = res.data.resultData;
                    self.typeProcess.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_2')
                    });
                }
            });
        }

        //选择物料
        function materialClick() {
            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("material") + 'Base_MaterialFactory/Base_MaterialFactoryPageDataTableList',
                            method: "Post",
                            queryParmeters: {
                                Name: "",
                                FactoryCode: self.typeFactory.value.ResourceCode
                                //SmallClassOwnProduct: "'GB','DC'"
                                // MaterialClass: "BCPL"
                            },
                            pagination: {},
                            multiple: false,
                            sidx: "MaterialCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'MaterialCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_4'),
                                    width: 110
                                },
                                {
                                    field: 'MaterialName',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_5'),
                                    width: 120
                                },
                                {
                                    field: 'Spec',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_6'),
                                    width: 120
                                },
                                {
                                    field: 'SmallClassName',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_7'),
                                    width: 120
                                },
                                {
                                    field: 'ProcessRouteName',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_8'),
                                    width: 120
                                },
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                console.log(data);
                self.currentItem.MaterialCode = data[0].MaterialCode;
                self.currentItem.MaterialName = data[0].MaterialName;
                self.currentItem.Spec = data[0].Spec;
                self.currentItem.SmallClass = data[0].SmallClass;
                self.currentItem.SmallClassName = data[0].SmallClassName;
                self.currentItem.ProcessRoute = data[0].ProcessRoute;
                self.currentItem.ProcessRouteName = data[0].ProcessRouteName;
                self.currentItem.Unit = data[0].Unit;
                self.currentItem.UnitName = data[0].UnitName;
                initTypeBom(self.currentItem.MaterialCode);
            });
        }

        //工艺改变
        function processOperationChange(oldval, newval) {
            console.log(newval);
        }

        function initTypeBom(materialCode) {
            if (!self.typeFactory.value.ResourceCode) {
                return;
            }
            let factoryCode = self.typeFactory.value.ResourceCode;
            var url = commonService.getMesApiAddress("material") + "BS_BOM/GetBS_BOMList?checkType=" + materialCode + "&factoryCode=" + factoryCode;
            commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.typeBom.options = res.data.resultData;
                    self.typeBom.options.splice('0', '0', {
                        Id: "",
                        BOMCode: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_2')
                    });
                }
            })
        }

        function save() {

            //字典类型 取值参考
            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.typeFactory.value.ResourceName;
            self.currentItem.ProcessCode = self.typeProcess.value.ResourceCode;
            if (self.typeBom.value.Id != "") {
                self.currentItem.BOMCode = self.typeBom.value.BOMCode;
            }
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_9') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_OwnProductOrder/SavePM_OwnProductOrder';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_10'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_11'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_11'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_OwnProduct_OwnProductOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/OwnProduct';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/OwnProductOrder-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.OwnProductOrder.addJS.Tips_12'
            }
        };
        $stateProvider.state(state);
    }
}());
