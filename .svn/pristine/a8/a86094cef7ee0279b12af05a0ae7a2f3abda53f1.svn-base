(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.AbrasiveOrder').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AbrasiveOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //初始化前端变量数据
            self.currentItem = {};
            self.validInputs = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
            initDictionary();
            self.typeFactoryChange = typeFactoryChange;
            self.materialClick = materialClick;
        }

        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_2'), ResourceCode: "" }]
            };
            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_2'), ResourceCode: "" }]
            };

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    self.typeFactory.value = res.data.resultData[0];
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_2')
                    });
                }
            });
            //BOM编码
            self.typeBom = {
                value: { BOMCode: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_2'), Id: "" },
                options: [{ BOMCode: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_2'), Id: "" }]
            };

        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.Process.options = res.data.resultData;
                        self.Process.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_2')
                        });
                    }
                });
            }
            else {
                self.typeFactory = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_2'), ResourceCode: "" }]
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
                                // SmallClassOwnProduct: "'MFL'"
                            },
                            pagination: {},
                            multiple: false,
                            sidx: "MaterialCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'MaterialCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_4'),
                                    width: 110
                                },
                                {
                                    field: 'MaterialName',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_5'),
                                    width: 120
                                },
                                {
                                    field: 'Spec',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_6'),
                                    width: 120
                                },
                                {
                                    field: 'SmallClassName',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_7'),
                                    width: 120
                                },
                                {
                                    field: 'ProcessRouteName',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_8'),
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
                        BOMCode: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_2')
                    });
                }
            })
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {

            if (!self.currentItem.ProcessRoute) {
                //工艺路线不能为空
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_12'));
                return;
            }

            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.typeFactory.value.ResourceName;
            self.currentItem.ProcessCode = self.Process.value.ResourceCode;
            self.currentItem.BOMCode = self.typeBom.value.BOMCode;

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_AbrasiveOrder/SavePM_AbrasiveOrder';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_9') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_10'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_11'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_11'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_AbrasiveOrder_AbrasiveOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/AbrasiveOrder';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/AbrasiveOrder-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.AbrasiveOrder.AddJS.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
