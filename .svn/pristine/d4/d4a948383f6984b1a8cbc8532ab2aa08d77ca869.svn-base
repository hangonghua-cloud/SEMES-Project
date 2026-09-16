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
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintain.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', '$rootScope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, $rootScope, commonService, auth, notificationService, busyIndicatorService, $modal) {
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

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditctrl.Tips_1'));
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
            self.currentItem.Period = parseFloat(self.currentItem.Period);
            self.validInputs = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            //工厂
            self.CodadConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            self.IsUsed = [
                {
                    label: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditctrl.Tips_2'),
                    checked: true
                }
            ];

            if (self.currentItem.IsUsed == "1")
                self.IsUsed[0].checked = true;
            else
                self.IsUsed[0].checked = false;

            GetCodadList();
        }
        //工厂
        function GetCodadList() {
            var postData = {
                "queryJson": {
                    "ResourceCode": "",
                    "ResourceName": "",
                    "ModelLevel": "Factory"
                }
            };
            var url = commonService.getMesApiAddress("factory") + "LevelManage/BsModelWithResource/GetListJson";
            console.log(JSON.stringify(postData));
            commonService.callWebApiPost(url, postData).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    self.CodadConfig.options = jsonData;
                    self.CodadConfig.selectedOption = { ResourceCode: self.currentItem.Factory, ResourceName: self.currentItem.FactoryName };
                } else {
                    console.log(self.ParentResourceCtrl.options);
                }
                //self.SecondValue = jsonData[0];
                self.CodadConfig.options.splice(0, 0, { ResourceCode: "", ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditctrl.Tips_3') })
            }, function (error) {
                // console.log('-----------error------------');
                console.log(error);
            });
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //编辑保存
        function save() {

            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItem.ModifyBy = self.UserCode;
            // self.currentItem.Factory = self.CodadConfig.selectedOption.ResourceCode;
            self.currentItem.IsUsed = self.IsUsed[0].checked;
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditctrl.Tips_4') });
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentMaintain/SaveEP_EquipmentMaintain';
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
            console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditctrl.Tips_5'));
                //刷新局部
                $rootScope.$emit('to-editItem', self.currentItem);
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditctrl.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditctrl.Tips_6'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMaintain_EP_EquipmentMaintain';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentMaintain';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EP_EquipmentMaintain-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Edit'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
