/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 设备保养项目维护
*  2. 创建人员： 王坤
*  3. 创建日期： 2021-08-05
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain').config(AddStandardScreenStateConfig);

    AddStandardScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintain.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', '$rootScope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddStandardScreenController(dataService, $state, $stateParams, common, $filter, $scope, $rootScope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            //初始化
            init();
            //注册事件
            registerEvents();

            ///self.currentItem = {};
            //self.currentItem.EquipmentType = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainaddstandardctrl.Tips_1');//如果新增有系统自动生成编号 可以在这写一个初始值, 这个控件要只读状态,后台代码生成编号+流水号

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainaddstandardctrl.Tips_2'));
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
            self.currentItemDetail = {};
            self.validInputs = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();


            //数据类型
            self.DataTypeConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            GetDataTypeDictionary();
        }

        //设备类型
        function GetDataTypeDictionary() {
            var url = commonService.getDataItemDuatil("DataType").then(function (res) {
                self.DataTypeConfig.options = res.data.resultData;;
            });
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {

            if (self.DataTypeConfig.selectedOption.ItemValue != "") {
                self.currentItemDetail.DataType = self.DataTypeConfig.selectedOption.ItemValue;
            }
            else {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainaddstandardctrl.Tips_3'));
                return;
            }

            // self.currentItemDetail.FactoryCode = self.currentItem.FactoryCode;
            // self.currentItemDetail.FactoryName = self.currentItem.FactoryName;
            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItemDetail.Creator = self.UserCode;
            self.currentItemDetail.EquipmentTaskId = self.currentItem.EquipmentTaskId;
            //self.currentItemDetail.DataType = self.DataTypeConfig.selectedOption.ItemValue;
            console.log("username------------------------------------" + self.UserName);
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItemDetail
            };

            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentMaintainDetail/SaveEP_EquipmentMaintainDetail';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainaddstandardctrl.Tips_4') });
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
            console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                //sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainaddstandardctrl.Tips_5'));
                self.currentItemDetail.EquipmentMaintainId = "";
                self.currentItemDetail.EquipmentMaintainName = "";
                self.currentItemDetail.EquipmentMaintainStandard = "";
                self.DataTypeConfig.selectedOption = { ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainaddstandardctrl.Tips_6'), ItemValue: "" };
                //刷新局部
                $rootScope.$emit('to-addChildItem', self.currentItemDetail);
                //$state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainaddstandardctrl.Tips_7'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintainaddstandardctrl.Tips_7'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddStandardScreenStateConfig.$inject = ['$stateProvider'];
    function AddStandardScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMaintain_EP_EquipmentMaintain';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentMaintain';

        var state = {
            name: screenStateName + '.addstandard',
            url: '/addstandard',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EP_EquipmentMaintain-addstandard.html',
                    controller: AddStandardScreenController,
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
