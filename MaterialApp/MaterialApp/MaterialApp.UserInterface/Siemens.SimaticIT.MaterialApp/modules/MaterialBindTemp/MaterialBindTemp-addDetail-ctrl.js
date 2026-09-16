/**
*  0. 代码生成： UA主子表一键生成前后端html、JS、API接口代码生成器 Ver 1.11 更新日期：2021-07-14  设计者：刘万军
*  1. 功能描述： 物料属性模板维护
*  2. 创建人员： liyongguo
*  3. 创建日期： 2021-07-21
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MaterialBindTemp').config(AddDetailScreenStateConfig);

    AddDetailScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemp.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal','$rootScope'];
    function AddDetailScreenController(dataService, $state, $stateParams, common, $filter,
        $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            //初始化
            init();
            //注册事件
            registerEvents();

            //self.currentItem.MateriaBindTempId = commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddDetailctrl.Tips_1');//如果新增有系统自动生成编号 可以在这写一个初始值, 这个控件要只读状态,后台代码生成编号+流水号

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddDetailctrl.Tips_2'));
            sidePanelManager.open('e');//使用窄弹窗
            //使用宽右侧弹窗
            // sidePanelManager.open({
            //     mode: 'e',
            //     size: 'wide'
            // });
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
            self.MainselectedItem = angular.copy($stateParams.selectedItem);
            self.currentItem = {};
            self.currentItem.MateriaBindTempId = self.MainselectedItem.Id;//关联字段
            self.validInputs = false;


            self.typeEnabledSelect = {
                options: [
                    { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddDetailctrl.Tips_3') },
                    { ItemValue: false, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddDetailctrl.Tips_4') },
                ],
                selectedOption: { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddDetailctrl.Tips_3') }
            }
            self.typeAttrTypeSelect = {
                options: [],
                selectedOption: {}
            }

            commonService.getDataItemDuatil("AttrType").then(function (res) {
                if (res && res.data.success) {
                    self.typeAttrTypeSelect.options = res.data.resultData;
                    self.typeAttrTypeSelect.selectedOption = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddDetailctrl.Tips_5') });

            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            // self.currentItem.CreatedByName = self.UserCode + '-' + self.UserName;
            // self.currentItem.CreatedByCode = self.UserCode;
            // if(self.UserName == null || self.UserName == '' || self.UserName == undefined){
            //     self.currentItem.CreatedByName = self.UserCode;
            // }

            //self.currentItem.IsEnabled = self.typeEnabledSelect.selectedOption.ItemValue;
            self.currentItem.IsEnabled = true;
            if (self.typeAttrTypeSelect.selectedOption && self.typeAttrTypeSelect.selectedOption != "") {
                self.currentItem.AttrType = self.typeAttrTypeSelect.selectedOption.ItemValue;
            } else {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddDetailctrl.Tips_6'), commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddDetailctrl.Tips_7'));
                busyIndicatorService.hide();
                return;
            }


            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };


            var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTempFacet/SaveBase_MaterialBindTempFacet';

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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddDetailctrl.Tips_8'));
                //刷新局部
                 $rootScope.$emit('to-parentdetail', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddDetailctrl.Tips_9'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddDetailctrl.Tips_9'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddDetailScreenStateConfig.$inject = ['$stateProvider'];
    function AddDetailScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MaterialBindTemp_MaterialBindTemp';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MaterialBindTemp';

        var state = {
            name: screenStateName + '.addDetail',
            url: '/addDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/MaterialBindTemp-addDetail.html',
                    controller: AddDetailScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddDetailctrl.Tips_2'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
