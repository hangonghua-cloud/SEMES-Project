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
    angular.module('Siemens.SimaticIT.MaterialApp.BSTraitManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.BSTraitManage.BS_TraitManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope', 'i18nService'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope, i18nService) {
        // 国际化；
        i18nService.setCurrentLang('zh-cn');
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddctrl.Tips_1'), ResourceCode: "" }]
            };
            //初始化 是否启用
            self.IsEnabled = {
                value: '1',
                options: [{
                    label: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddctrl.Tips_2'),
                    value: '1'
                }, {
                    label: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddctrl.Tips_3'),
                    value: '0'
                }]
            };

            //初始化
            init();
            //注册事件
            registerEvents();

            self.currentItem = {};
            //self.currentItem.FactoryCode = commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddctrl.Tips_4');//如果新增有系统自动生成编号 可以在这写一个初始值, 这个控件要只读状态,后台代码生成编号+流水号


            //获取登录用户信息
            GetUserInfo();
            self.typeFactoryChange = typeFactoryChange;
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddctrl.Tips_5'));
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
            self.currentItem = {};
            self.validInputs = false;


            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
            //工厂

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    var Positions = [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddctrl.Tips_1'), ResourceCode: "" }]
                    var retData = res.data.resultData
                    for (var i = 0; i < retData.length; i++) {
                        Positions.push({ ResourceName: retData[i].ResourceName, ResourceCode: retData[i].ResourceCode })
                    }
                    self.typeFactory.options = Positions


                }
            });
        }

        function typeFactoryChange(oldItem, newItem) {
            // debugger;
            if (newItem.ResourceCode) {
                self.currentItem.FactoryCode = newItem.ResourceCode;
            }
        }
        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddctrl.Tips_6') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItem.CreateName = self.UserName;
            self.currentItem.Creator = self.UserCode;
            if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                self.currentItem.CreateName = self.UserCode;
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
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            console.log("postData------------------------------------" + JSON.stringify(postData));
            // commonService.getMesApiAddress() = '/sitSrvApi/'
            var url = commonService.getMesApiAddress("material") + 'BS_TraitManage/SaveBS_TraitManage';
            console.log("url----------------" + url);
            //提交数据
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            console.log("SaveBS_TraitManage----------------------" + JSON.stringify(req));
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddctrl.Tips_7'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddctrl.Tips_8'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddctrl.Tips_8'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_BSTraitManage_BS_TraitManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/BSTraitManage';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/BSTraitManage-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddctrl.Tips_9'
            }
        };
        $stateProvider.state(state);
    }
}());
