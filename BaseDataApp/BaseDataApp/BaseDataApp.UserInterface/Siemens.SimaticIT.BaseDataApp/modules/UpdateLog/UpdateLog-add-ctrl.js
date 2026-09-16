(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BaseDataApp.UpdateLog').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.BaseDataApp.UpdateLog.UpdateLog.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.BaseDataApp.UpdateLog.UpdateLogaddctrl.Tips_1'));
            sidePanelManager.open('e');
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
        }
        function initDictionary() {

            self.BusinessType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.BaseDataApp.UpdateLog.UpdateLogaddctrl.Tips_2'), ItemValue: "" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.BaseDataApp.UpdateLog.UpdateLogaddctrl.Tips_2'), ItemValue: "" },
                    { ItemName: "PDA", ItemValue: "1" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.BaseDataApp.UpdateLog.UpdateLogaddctrl.Tips_3'), ItemValue: "2" },
                ]
            };
        }


        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {

            self.currentItem.BusinessType = self.BusinessType.value.ItemValue;
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.BaseDataApp.UpdateLog.UpdateLogaddctrl.Tips_4') });
            var url = commonService.getMesApiAddress() + 'Base_UpdateLog/SaveBase_UpdateLog';
            //提交数据
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.BaseDataApp.UpdateLog.UpdateLogaddctrl.Tips_5'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.UpdateLog.UpdateLogaddctrl.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.BaseDataApp.UpdateLog.UpdateLogaddctrl.Tips_6'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BaseDataApp_UpdateLog_UpdateLog';
        var moduleFolder = 'Siemens.SimaticIT.BaseDataApp/modules/UpdateLog';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/UpdateLog-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.BaseDataApp.UpdateLog.UpdateLogaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
