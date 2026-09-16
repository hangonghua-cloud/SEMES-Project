/**
*  0. 代码生成： UA主子表一键生成前后端html、JS、API接口代码生成器 Ver 1.13 更新日期：2021-08-16  设计者：刘万军
*  1. 功能描述： 特征维护
*  2. 创建人员： jpf
*  3. 创建日期： 2022-11-02
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.BSTraitManage').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.BSTraitManage.BS_TraitManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope', 'i18nService'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope, i18nService) {
        // 国际化；
        i18nService.setCurrentLang('zh-cn');
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {


            //初始化
            init();
            //注册事件
            registerEvents();

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageeditctrl.Tips_1'));
            sidePanelManager.open('e');//使用窄弹窗
            //使用宽右侧弹窗
            //sidePanelManager.open({
            //mode: 'e',
            //size: 'wide'
            //});
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
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageeditctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageeditctrl.Tips_2'), ResourceCode: "" }]
            };
            //初始化 是否启用
            self.IsEnabled = {
                value: '1',
                options: [{
                    label: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageeditctrl.Tips_3'),
                    value: '1'
                }, {
                    label: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageeditctrl.Tips_4'),
                    value: '0'
                }]
            };
            //传参实体定义赋值
            self.currentItem = angular.copy($stateParams.selectedItem);
            // if (self.currentItem.IsEnable) {
            //     self.IsEnabled.value = '1';
            // } else {
            //     self.IsEnabled.value = '0';
            // }
            self.typeFactory.value = { "ResourceName": self.currentItem.FactoryName, "ResourceCode": self.currentItem.FactoryCode };

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
            //工厂

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    var Positions = [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageeditctrl.Tips_2'), ResourceCode: "" }]
                    var retData = res.data.resultData
                    for (var i = 0; i < retData.length; i++) {
                        Positions.push({ ResourceName: retData[i].ResourceName, ResourceCode: retData[i].ResourceCode })
                    }
                    self.typeFactory.options = Positions


                }
            });
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //编辑保存
        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageeditctrl.Tips_5') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;


            console.log("postData------------------------------------" + JSON.stringify(self.currentItem));

            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItem.ModifyName = self.UserCode + '-' + self.UserName;
            self.currentItem.ModifyBy = self.UserCode;
            if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                self.currentItem.ModifyName = self.UserCode;
            }
            if (self.IsEnabled.value == "1") {
                self.currentItem.IsEnable = true;

            } else {
                self.currentItem.IsEnable = false;
            }
            //工厂 下拉取值
            if (self.typeFactory.value != null && self.typeFactory.value != '') {
                self.currentItem.FactoryName = self.typeFactory.value.ResourceName;
            }
            console.log("username------------------------------------" + self.UserName);
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            console.log("postData------------------------------------" + JSON.stringify(postData));
            // commonService.getMesApiAddress() = '/sitSrvApi/'
            var url = commonService.getMesApiAddress("material") + 'BS_TraitManage/SaveBS_TraitManage';

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
            console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageeditctrl.Tips_6'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageeditctrl.Tips_7'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageeditctrl.Tips_7'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_BSTraitManage_BS_TraitManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/BSTraitManage';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/BSTraitManage-edit.html',
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
