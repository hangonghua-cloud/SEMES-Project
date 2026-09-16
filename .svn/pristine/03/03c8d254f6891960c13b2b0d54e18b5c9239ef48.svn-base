(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.BOM').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.BOM.BOM.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter,
        $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_1'));
            sidePanelManager.open('e');
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

            self.materialClick = materialClick;
            self.typeFactoryChange = typeFactoryChange;
            self.processRouteModal = processRouteModal;//工艺路线
        }

        function initDictionary() {
            self.ProcessRoute = {
                value: { ProcessCode: "", ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_2') },
                options: [{ ProcessCode: "", ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_2') }]
            };
            self.typeFactory = {
                value: null,
                options: []
            };
            self.TypeOrder = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_2'), ItemValue: "" }]
            }

            commonService.getDataItemDuatil("OrderType").then(function (res) {
                if (res && res.data.success) {
                    self.TypeOrder.options = res.data.resultData;
                    self.TypeOrder.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            // var url1 = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=";
            // commonService.callWebApiGet(url1, null).then(function (res) {
            //     if (res && res.data.success) {
            //         self.ProcessRoute.options = res.data.resultData;
            //         self.ProcessRoute.options.splice('0', '0', {
            //             ProcessCode: "",
            //             ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_2')
            //         });
            //     }
            // })

            var url = commonService.getMesApiAddress("factory") + 'level/Get_ModelResourceExtendInfo_ByLevelCode';
            commonService.callWebApiPost(url, { LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_2')
                    });
                } else {
                    self.typeFactory.options = [];
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_3'), commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_4'));
                }
            });
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                //工艺路线
                var url = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=" + newItem.ResourceCode;
                commonService.callWebApiGet(url, null).then(function (res) {
                    if (res && res.data.success) {
                        self.ProcessRoute.options = res.data.resultData;
                        self.ProcessRoute.options.splice('0', '0', {
                            ProcessCode: "",
                            ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_2')
                        });
                    }
                })
            } else {
                self.ProcessRoute = {
                    value: { ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_2'), ProcessCode: "" },
                    options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_2'), ProcessCode: "" }]
                };
            }
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
                                FactoryCode: self.typeFactory.value.ResourceCode,
                                //SmallClassOwnProduct: "'GB','DC'"
                                //MaterialClass: "BCPL"
                            },
                            pagination: {},
                            multiple: false,
                            sidx: "MaterialCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'MaterialCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_6'),
                                    width: 110
                                },
                                {
                                    field: 'MaterialName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_7'),
                                    width: 120
                                },
                                {
                                    field: 'Spec',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_8'),
                                    width: 120
                                },
                                {
                                    field: 'MaterialClassName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_9'),
                                    width: 120
                                },
                                {
                                    field: 'ProcessRouteName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_10'),
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
                self.currentItem.MaterialClass = data[0].MaterialClass;
                self.currentItem.MaterialClassName = data[0].MaterialClassName;
                self.currentItem.UnitName = data[0].UnitName;
                //self.currentItem.ProcessRouteName = data[0].ProcessRouteName;
            });
        }

        //选择工艺路线
        function processRouteModal() {
            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_11'));
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
                            url: commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=" + self.typeFactory.value.ResourceCode,
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
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ProcessCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_12'),
                                    width: 200
                                },
                                {
                                    field: 'ProcessName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_13'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_14'));
                } else {
                    self.currentItem.Process = data[0].ProcessCode;
                    self.currentItem.ProcessName = data[0].ProcessName;
                }
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {

            if (!self.currentItem.MaterialName) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_15'));
                return;
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_16') });
            // //字典类型 取值参考
            // if (!!self.ProcessRoute.value && self.ProcessRoute.value.ProcessCode != "") {
            //     self.currentItem.Process = self.ProcessRoute.value.ProcessCode;
            // }
            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.typeFactory.value.ResourceName;
            if (!!self.TypeOrder.value) self.currentItem.OrderType = self.TypeOrder.value.ItemValue;

            self.currentItem.BOMType = "1";
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("material") + 'BS_BOM/SaveBS_BOM';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_17'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_4'));
            }
        }
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_4'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_BOM_BOM';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/BOM';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/BOM-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.BOM.BOMaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
