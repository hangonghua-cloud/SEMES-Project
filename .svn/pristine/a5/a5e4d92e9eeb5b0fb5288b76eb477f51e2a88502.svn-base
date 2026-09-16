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
    angular.module('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance').config(EditDetailScreenStateConfig);

    EditDetailScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenance.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', '$rootScope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditDetailScreenController(dataService, $state, $stateParams, common, $filter, $scope, $rootScope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            //传参实体定义赋值
            self.currentItem = angular.copy($stateParams.selectedItem);


            //初始化
            init();
            //注册事件
            registerEvents();

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceeditdetailctrl.Tips_1'));
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
            self.selectDataType = selectDataType;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();


            //部门
            self.DepartmentConfig = {
                value: null,
                selectedOption: {},
                options: []
            };

            //数据类型
            self.DataTypeConfig = {
                value: null,
                selectedOption: {},
                options: []
            };

            self.IsUsed = [
                {
                    label: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceeditdetailctrl.Tips_2'),
                    checked: true
                }
            ];
            self.IsUsed[0].checked = self.currentItem.IsEnabled;

            GetDepartmentList();
            GetDataTypeDictionary();
        }

        function selectDataType(oldValue, newValue) {
            if (newValue.ItemValue != 1) {
                self.currentItem.UpperLimit = "";
                self.currentItem.LowerLimit = "";
            }
        }
        //部门
        function GetDepartmentList() {
            var url = commonService.getDataItemDuatil("AssayDepartment").then(function (res) {
                self.DepartmentConfig.options = res.data.resultData;;
                self.DepartmentConfig.selectedOption = { ItemValue: self.currentItem.TestDepartment, ItemName: self.currentItem.TestDepartmentName };
            });
        }

        //设备类型
        function GetDataTypeDictionary() {
            var url = commonService.getDataItemDuatil("DataType").then(function (res) {
                self.DataTypeConfig.options = res.data.resultData;;
                var aa = self.currentItem.DataType.toString();
                self.DataTypeConfig.selectedOption = { ItemValue: self.currentItem.DataType.toString(), ItemName: self.currentItem.DataTypeName };
            });
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //编辑保存
        function save() {
            if (self.currentItem.UpperLimit != null && self.currentItem.LowerLimit != null && parseFloat(self.currentItem.UpperLimit) < parseFloat(self.currentItem.LowerLimit)) {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceeditdetailctrl.Tips_3'));
                return;
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceeditdetailctrl.Tips_4') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;

            if (self.DepartmentConfig.selectedOption != null) {
                self.currentItem.TestDepartment = self.DepartmentConfig.selectedOption.ItemValue;
            }
            if (self.DataTypeConfig.selectedOption != null) {
                self.currentItem.DataType = self.DataTypeConfig.selectedOption.ItemValue;
                self.currentItem.DataTypeName = self.DataTypeConfig.selectedOption.ItemName;
            }


            self.currentItem.IsEnabled = self.IsUsed[0].checked;

            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("quality") + 'QC_TestMethodItemMaintenance/SaveQC_TestMethodItemMaintenance';
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
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceeditdetailctrl.Tips_5'));
                //刷新局部
                $rootScope.$emit('to-DetailItem', self.currentItem);
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceeditdetailctrl.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceeditdetailctrl.Tips_6'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditDetailScreenStateConfig.$inject = ['$stateProvider'];
    function EditDetailScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_QC_TestMethodMaintenance_QC_TestMethodMaintenance';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_TestMethodMaintenance';

        var state = {
            name: screenStateName + '.editdetail',
            url: '/editdetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/QC_TestMethodMaintenance-editdetail.html',
                    controller: EditDetailScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Edit'
            },
            params: {
                selectedItem: null,
                selectedItemDetail: null,
            }
        };
        $stateProvider.state(state);
    }
}());
