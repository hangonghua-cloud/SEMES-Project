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
    angular.module('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance').config(AddDetailScreenStateConfig);

    AddDetailScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenance.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', '$rootScope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddDetailScreenController(dataService, $state, $stateParams, common, $filter, $scope, $rootScope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            //初始化
            init();
            //注册事件
            registerEvents();

            //self.currentItem = {};
            //self.currentItem.Factory = commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceadddetailctrl.Tips_1');//如果新增有系统自动生成编号 可以在这写一个初始值, 这个控件要只读状态,后台代码生成编号+流水号

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceadddetailctrl.Tips_2'));
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
            self.currentItem = angular.copy($stateParams.selectedItem);

            self.validInputs = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            //部门
            self.DepartmentConfig = {
                value: null,
                selectedOption: null,
                options: []
            };


            self.DataTypeConfig = {
                selectedOption: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceadddetailctrl.Tips_3'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceadddetailctrl.Tips_3'), ItemValue: "" }]
            };

            self.IsUsed = [
                {
                    label: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceadddetailctrl.Tips_4'),
                    checked: true
                }
            ];

            commonService.getDataItemDuatil("AssayDepartment").then(function (res) {
                self.DepartmentConfig.options = res.data.resultData;;
            });
            commonService.getDataItemDuatil("DataType").then(function (res) {
                self.DataTypeConfig.options = res.data.resultData;;
            });
        }



        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {
            debugger
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceadddetailctrl.Tips_5') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;


            if (self.DepartmentConfig.selectedOption != null) {
                self.currentItem.TestDepartment = self.DepartmentConfig.selectedOption.ItemValue;
            }
            if (self.DataTypeConfig.selectedOption != null) {
                self.currentItem.DataType = self.DataTypeConfig.selectedOption.ItemValue;
                self.currentItem.DataTypeName = self.DataTypeConfig.selectedOption.ItemName;
            }

            self.currentItem.TestMethodId = self.currentItem.Id;
            self.currentItem.IsEnabled = self.IsUsed[0].checked;

            var postData = {
                KeyValue: '',
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("quality") + 'QC_TestMethodItemMaintenance/SaveQC_TestMethodItemMaintenance';

            //提交数据
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

            busyIndicatorService.hide();
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
                // sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceadddetailctrl.Tips_6'));
                //刷新局部
                $rootScope.$emit('to-DetailItem', self.currentItem);
                // $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceadddetailctrl.Tips_7'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceadddetailctrl.Tips_7'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddDetailScreenStateConfig.$inject = ['$stateProvider'];
    function AddDetailScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_QC_TestMethodMaintenance_QC_TestMethodMaintenance';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_TestMethodMaintenance';

        var state = {
            name: screenStateName + '.adddetail',
            url: '/adddetail',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/QC_TestMethodMaintenance-adddetail.html',
                    controller: AddDetailScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Add'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
