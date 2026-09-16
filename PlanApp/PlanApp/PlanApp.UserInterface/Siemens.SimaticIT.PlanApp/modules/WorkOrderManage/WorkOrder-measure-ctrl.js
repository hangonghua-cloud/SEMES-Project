(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderManage.WorkOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        
        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.measureJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = null;
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.measureJS.Tips_2') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;

            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItem.CreatedByName = self.UserCode + '-' + self.UserName;
            self.currentItem.CreatedByCode = self.UserCode;
            if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                self.currentItem.CreatedByName = self.UserCode;
            }
 
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };


            var url = commonService.getMesApiAddress() + 'PL_WorkOrder/SavePL_WorkOrder';
 
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.measureJS.Tips_3'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.measureJS.Tips_4'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.measureJS.Tips_4'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderManage_WorkOrder';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderManage';

        var state = {
            name: screenStateName + '.measure',
            url: '/measure',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrder-measure.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderManage.measureJS.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
