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

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_1'));
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

            self.materialChange = materialChange;
            self.processRouteModal = processRouteModal;//工艺路线
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function materialChange(oldvalue, newvalue) {

            var url = commonService.getMesApiAddress("material") + 'Base_Material/GetDataTable_TestOtherEntity?checkType=' + newvalue;
            commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success && res.data.resultData.length > 0) {
                    self.currentItem.MaterialClass = res.data.resultData[0].MaterialClassName;
                    self.currentItem.MaterialName = res.data.resultData[0].MaterialName;
                } else {
                    self.currentItem.MaterialClass = "";
                    self.currentItem.MaterialName = "";
                }
            });
        }

        function initDictionary() {

            self.ProcessRoute = {
                value: { ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_2'), ProcessCode: "" },
                options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_2'), ProcessCode: "" }]
            };
            self.typeFactory = {
                value: null,
                options: []
            };
            self.TypeOrder = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_2'), ItemValue: "" }]
            }
            commonService.getDataItemDuatil("OrderType").then(function (res) {
                if (res && res.data.success) {
                    self.TypeOrder.options = res.data.resultData;
                    self.TypeOrder.value = self.TypeOrder.options.find(t => t.ItemValue == self.currentItem.OrderType);
                }
            })
            // var url1 = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=";
            // commonService.callWebApiGet(url1, null).then(function (res) {
            //     if (res && res.data.success) {
            //         self.ProcessRoute.options = res.data.resultData;
            //         self.ProcessRoute.options.splice('0', '0', {
            //             ProcessCode: "",
            //             ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_2')
            //         });
            //         self.ProcessRoute.value = self.ProcessRoute.options.find(t => t.ProcessCode == self.currentItem.Process);
            //     }
            // })
            //工艺路线
            var url = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=" + self.currentItem.FactoryCode;
            commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.ProcessRoute.options = res.data.resultData;
                    self.ProcessRoute.options.splice('0', '0', {
                        ProcessCode: "",
                        ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_2')
                    });
                    self.ProcessRoute.value = self.ProcessRoute.options.find(t => t.ProcessCode == self.currentItem.Process);
                }
            })

            var url = commonService.getMesApiAddress("factory") + 'level/Get_ModelResourceExtendInfo_ByLevelCode';
            commonService.callWebApiPost(url, { LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    self.typeFactory.value = self.TypeFactory.options.find(t => t.ResourceCode == self.currentItem.FactoryCode);
                } else {
                    self.typeFactory.options = [];
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_3'), commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_4'));
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
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ProcessCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_6'),
                                    width: 200
                                },
                                {
                                    field: 'ProcessName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_7'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_8'));
                } else {
                    self.currentItem.Process = data[0].ProcessCode;
                    self.currentItem.ProcessName = data[0].ProcessName;
                }
            });
        }

        function save() {

            if (!self.currentItem.MaterialName) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_9'));
                return;
            }

            // //字典类型 取值参考
            // if (!!self.ProcessRoute.value && self.ProcessRoute.value.ProcessCode != "") {
            //     self.currentItem.Process = self.ProcessRoute.value.ProcessCode;
            // }
            // self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            // self.currentItem.FactoryName = self.typeFactory.value.ResourceName;
            if (!!self.TypeOrder.value) self.currentItem.OrderType = self.TypeOrder.value.ItemValue;

            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_10') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_11'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_4'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_4'));
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
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/BOM-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.BOM.BOMeditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
