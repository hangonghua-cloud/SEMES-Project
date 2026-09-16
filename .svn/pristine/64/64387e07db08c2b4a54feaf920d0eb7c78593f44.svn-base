(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum').config(AddVCScreenStateConfig);

    AddVCScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.PMOperationPalletNum.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddVCScreenController(dataService, $state, $stateParams,
        common, $filter, $scope, commonService, auth, notificationService,
        busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();

        // Initialization function
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_1'));
            sidePanelManager.open('e');
            //sidePanelManager.open({
            //  mode: "e",
            //   size: "wide"
            //});
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

            self.typeFactoryChange = typeFactoryChange;
            self.typeProcessChange = typeProcessChange;
            self.selectSpecModal = selectSpecModal;//选择规格

            //数据字典
            initDictionary();
        }

        function initDictionary() {

            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_2')
                    });
                }
            });

            //工序
            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_2'), ResourceCode: "" }]
            };

            //单位
            self.typeUnit = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_2'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("Unit").then(function (res) {
                if (res && res.data.success) {
                    self.typeUnit.options = res.data.resultData;
                    self.typeUnit.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeProcess.options = res.data.resultData;
                        self.typeProcess.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_2')
                        });
                    }
                });
            } else {
                self.typeProcess = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_2'), ResourceCode: "" }]
                };
            }
        }
        //工序改变事件
        function typeProcessChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                let postData = {
                    resourceCode: newItem.ResourceCode,
                    fieldCode: "DW"
                };
                let url = commonService.getMesApiAddress("factory") + 'LevelManage/BsModelResourceExtendInfo/GetResourceExtendEntity';
                commonService.callWebApiPost(url, postData).then(function (data) {
                    if (data.data.success && data.data.resultData) {
                        self.currentItem.UnitName = data.data.resultData.FieldValue;
                    }
                    else
                        self.currentItem.UnitName = "";
                });
            }
            else
                self.currentItem.UnitName = "";
        }
        //选择规格
        function selectSpecModal() {

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_3'));
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
                            url: commonService.getMesApiAddress("material") + 'BS_TraitDetails/BS_TraitDetailsPageDataTableList',
                            queryParmeters: {
                                Name: "",
                                FactoryCode: self.typeFactory.value.ResourceCode,
                                TraitCode: "SPEC"
                            },
                            multiple: false,
                            method: "Post",
                            pagination: {},
                            sidx: "TraitValue",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_4'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'TraitValue',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_5'),
                                    width: 300
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_6'));
                } else {
                    self.currentItem.Spec = data[0].TraitValue;
                }
            });
        }

        function save() {

            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.typeFactory.value.ResourceName;
            self.currentItem.ProcessCode = self.typeProcess.value.ResourceCode;
            self.currentItem.ProcessName = self.typeProcess.value.ResourceName;
            self.currentItem.DocType = "2";//VC
            self.currentItem.UnitName = self.typeUnit.value.ItemName;
            var postData = {
                KeyValue: '',
                Entity: self.currentItem
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_7') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PMOperationPalletNum/SaveForm';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        //取消
        function cancel() {
            sidePanelManager.close();//关闭侧边栏
            $state.go('^');//返回列表(父页面)
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();//关闭遮罩层
                sidePanelManager.close();//关闭侧边栏
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_8'));
                $rootScope.$emit('to-parent', 'parent');//刷新局部
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_9'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_9'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }
    AddVCScreenStateConfig.$inject = ['$stateProvider'];
    function AddVCScreenStateConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_PMOperationPalletNum_PMOperationPalletNum';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PMOperationPalletNum';

        var state = {
            name: moduleStateName + '.addVC',
            url: '/addVC',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PMOperationPalletNum-addVC.html',
                    controller: AddVCScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addVCJS.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
