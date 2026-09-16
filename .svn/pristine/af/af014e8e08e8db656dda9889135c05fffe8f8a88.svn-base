(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PMProductPrice').config(AddDetailScreenStateConfig);

    AddSDetailcreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PMProductPrice.PMProductPrice.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddSDetailcreenController(dataService, $state, $stateParams,
        common, $filter, $scope, commonService, auth, notificationService,
        busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();

        // Initialization function
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_1'));
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
            self.IsDefault = [
                {
                    label: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_2'),
                    checked: false
                }
            ];

            self.typeFactoryChange = typeFactoryChange;
            self.typeProcessChange = typeProcessChange;
            self.specClick = specClick;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            //数据字典
            initDictionary();
        }

        function initDictionary() {

            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_3'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_3'), ResourceCode: "" }]
            };

            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_3'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_3'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_3')
                    });
                }
            });
            self.typeUnit = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_3'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_3'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("Unit").then(function (res) {
                if (res && res.data.success) {
                    self.typeUnit.options = res.data.resultData;
                    // self.typeOrderType.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            //岗位
            self.typePost = {
                value: { Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_3'), Col1: "" },
                options: [{ Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_3'), Col1: "" }]
            };
        }
        //工厂Change事件
        function typeFactoryChange(oldItem, newItem) {
            commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeProcess.options = res.data.resultData;
                    self.typeProcess.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_3')
                    });
                }
            });
        }
        //工序Change事件
        function typeProcessChange(oldItem, newItem) {
            commonService.getKeyParameterItem({ ItemCode: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typePost.options = res.data.resultData;
                    self.typePost.options.splice(0, 0, {
                        Col1: "",
                        Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_3')
                    });
                }
            });
        }

        //选择特征值
        function specClick() {
            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_4'));
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
                            url: commonService.getMesApiAddress("material") + 'BS_TraitDetails/GetTraitValue',
                            method: "Post",
                            queryParmeters: {
                                Name: "",
                                FactoryCode: self.typeFactory.value.ResourceCode,
                                TraitCode: "SPEC"
                            },
                            pagination: {},
                            multiple: false,
                            sidx: "TraitValue",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'TraitValue',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_6'),
                                    width: 400
                                },

                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                console.log(data);
                self.currentItem.Spec = data[0].TraitValue;
            });
        }

        function save() {

            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.typeFactory.value.ResourceName;
            self.currentItem.ProcessCode = self.typeProcess.value.ResourceCode;
            self.currentItem.ProcessName = self.typeProcess.value.ResourceName;
            self.currentItem.PostCode = self.typePost.value.Col1;
            self.currentItem.PostName = self.typePost.value.Col2;
            self.currentItem.PriceType = "2";//VC
            self.currentItem.IsDefault = self.IsDefault[0].checked ? 1 : 0;
            self.currentItem.UnitName = self.typeUnit.value.ItemName;
            var postData = {
                KeyValue: '',
                Entity: self.currentItem
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_7') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PMProductPrice/SaveForm';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_8'));
                $rootScope.$emit('to-parent', 'parent');//刷新局部
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_9'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_9'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }
    AddDetailScreenStateConfig.$inject = ['$stateProvider'];
    function AddDetailScreenStateConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_PMProductPrice_PMProductPrice';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PMProductPrice';

        var state = {
            name: moduleStateName + '.addDetail',
            url: '/addDetail',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PMProductPrice-addDetail.html',
                    controller: AddSDetailcreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PMProductPrice.addDetailJS.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
