 
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MaterialBindTemp').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemp.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter,
        $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            //初始化
            init();
            //注册事件
            registerEvents();

            self.currentItem = {};
            //self.currentItem.TempCode = commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddctrl.Tips_1');//如果新增有系统自动生成编号 可以在这写一个初始值, 这个控件要只读状态,后台代码生成编号+流水号

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddctrl.Tips_2'));
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

            self.typeEnabledSelect = {
                options: [
                    { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddctrl.Tips_3') },
                    { ItemValue: false, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddctrl.Tips_4') },
                ],
                selectedOption: { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddctrl.Tips_3') }
            }



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
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddctrl.Tips_5') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;

            self.currentItem.IsEnabled = self.typeEnabledSelect.selectedOption.ItemValue;

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };


            var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTemp/SaveBase_MaterialBindTemp';

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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddctrl.Tips_6'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddctrl.Tips_7'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddctrl.Tips_7'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MaterialBindTemp_MaterialBindTemp';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MaterialBindTemp';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/MaterialBindTemp-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempaddctrl.Tips_2'
            }
        };
        $stateProvider.state(state);
    }
}());
