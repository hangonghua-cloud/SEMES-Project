/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 原材料基础检验配置信息
*  2. 创建人员： 丁零
*  3. 创建日期： 2021-08-31
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenance.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            //初始化
            init();
            //注册事件
            registerEvents();

            self.currentItem = {};
            //self.currentItem.Factory = commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddctrl.Tips_1');//如果新增有系统自动生成编号 可以在这写一个初始值, 这个控件要只读状态,后台代码生成编号+流水号

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddctrl.Tips_2'));
            //sidePanelManager.open('e');//使用窄弹窗
            //使用宽右侧弹窗
            sidePanelManager.open({
                mode: 'e',
                //size: 'wide'
            });
        }

        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        //初始化
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
            //GetStatusDictionary();
        }

        //工厂
        function initDictionary() {
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddctrl.Tips_3'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddctrl.Tips_3'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddctrl.Tips_3')
                    });
                }
            });

            //检测类型
            self.TypeConfig = {
                value: null,
                selectedOption: null,
                options: []
            };
            var url = commonService.getDataItemDuatil("RawMaterialInspection").then(function (res) {//AssayType
                self.TypeConfig.options = res.data.resultData;;
            });

            //生效状态
            self.StatusConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            self.IsUsed = [
                {
                    label: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddctrl.Tips_4'),
                    checked: true
                }
            ];

        }

        //状态
        function GetStatusDictionary() {
            var url = commonService.getDataItemDuatil("EffectiveState").then(function (res) {
                self.StatusConfig.options = res.data.resultData;;
            });
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {

            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            if (self.TypeConfig.selectedOption != null) {
                self.currentItem.TestType = self.TypeConfig.selectedOption.ItemValue;
            }

            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItem.Creator = self.UserCode;
            self.currentItem.IsEnabled = self.IsUsed[0].checked;
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddctrl.Tips_5') });
            var url = commonService.getMesApiAddress("quality") + 'QC_TestMethodMaintenance/SaveQC_TestMethodMaintenance';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddctrl.Tips_6'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddctrl.Tips_7'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddctrl.Tips_7'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_QC_TestMethodMaintenance_QC_TestMethodMaintenance';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_TestMethodMaintenance';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/QC_TestMethodMaintenance-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Add'
            }
        };
        $stateProvider.state(state);
    }
}());
