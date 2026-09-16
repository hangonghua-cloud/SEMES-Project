(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PMPerformanceManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PMPerformanceManage.PMPerformanceManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams,
        common, $filter, $scope, commonService, auth, notificationService,
        busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();

        // Initialization function
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_1'));
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
            self.currentItem = null;
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.typeFactoryChange = typeFactoryChange;
            self.typeWorkShopChange = typeWorkShopChange;
            self.typeProcessChange = typeProcessChange;

            //数据字典
            initDictionary();
            initUserConfig();
        }

        function initDictionary() {
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2')
                    });
                    initGridData();
                }
            });

            //车间
            self.typeWorkShop = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2'), ResourceCode: "" }]
            };
            //工序
            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2'), ResourceCode: "" }]
            };
            //岗位
            self.typePost = {
                value: { Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2'), Col1: "" },
                options: [{ Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2'), Col1: "" }]
            };

        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeWorkShop.options = res.data.resultData;
                        self.typeWorkShop.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2')
                        });
                    }
                });
            } else {
                self.typeWorkShop = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2'), ResourceCode: "" }]
                };
            }
        }

        function typeWorkShopChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeProcess.options = res.data.resultData;
                        self.typeProcess.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2')
                        });
                    }
                });
            } else {
                self.typeProcess = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2'), ResourceCode: "" }]
                };
            }
        }

        function typeProcessChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getKeyParameterItem({ ItemCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typePost.options = res.data.resultData;
                        self.typePost.options.splice(0, 0, {
                            Col1: "",
                            Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2')
                        });
                    }
                });
            } else {
                self.typePost = {
                    value: { Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2'), Col1: "" },
                    options: [{ Col2: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_2'), Col1: "" }]
                };
            }
        }

        //子表操作区
        function initUserConfig() {
            self.UserConfig = {
                id: "userId",
                disableEP: false,
                datasource: [],
                selectedObject: {
                    Code: '',
                    Name: '',
                    ShowName: ''
                },
                limit: 5,
                waitTime: 500,
                placeholder: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_3'),
                attributetodisplay: "ShowName",
                editable: true,
                required: true,
                changeEvent: function (oldValue, newValue) {
                    if (self.UserConfig.selectedObject && self.UserConfig.selectedObject.Code) {
                        self.UserConfig.selectedObject.Code = null;
                    }
                    getUserData(newValue, self.UserConfig);
                }
            };
        }

        //获取用户名称
        function getUserData(name, obj) {

            console.log(name);
            if (!name) {
                obj.datasource = [];
            } else {
                commonService.getUserList(name).then(function (res) {
                    if (res && res.data.success) {
                        obj.datasource = res.data.resultData;

                    }
                }, function (error) {
                    commonService.showError('[' + error.status + '] - ' + '获取明细数据出错 ' + error.statusText);
                });
            }

        }

        function save() {

            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.typeFactory.value.ResourceName;
            self.currentItem.WorkShopCode = self.typeWorkShop.value.ResourceCode;
            self.currentItem.WorkShopName = self.typeWorkShop.value.ResourceName;
            self.currentItem.ProcessCode = self.typeProcess.value.ResourceCode;
            self.currentItem.ProcessName = self.typeProcess.value.ResourceName;
            self.currentItem.PostCode = self.typePost.value.Col1;
            self.currentItem.PostName = self.typePost.value.Col2;
            self.currentItem.UserCode = self.UserConfig.selectedObject.Code;
            self.currentItem.UserName = self.UserConfig.selectedObject.Name;
            self.currentItem.InfoSource = 0;//人工录入
            if (self.PayrollDate)
                self.currentItem.PayrollDate = commonService.ConvertToLocalDate(self.PayrollDate);
            else
                self.currentItem.PayrollDate = "";

            var postData = {
                KeyValue: '',
                Entity: self.currentItem
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_5') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PMPerformanceManage/SaveForm';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_6'));
                $rootScope.$emit('to-parent', 'parent');//刷新局部
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_7'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_7'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_PMPerformanceManage_PMPerformanceManage';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PMPerformanceManage';

        var state = {
            name: moduleStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PMPerformanceManage-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PMPerformanceManage.addJS.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
