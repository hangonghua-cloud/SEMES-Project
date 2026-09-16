(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.Plan').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.PlanApp.Plan.ProductionOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;


        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);

            self.OrderDate = angular.copy(self.currentItem.OrderDate);
            self.DeliveryDate = angular.copy(self.currentItem.DeliveryDate);
            self.BoxDate = angular.copy(self.currentItem.BoxDate);
            self.validInputs = false;
            self.orderTypeReadonly = false;//订单类型
            if (self.currentItem.OrderStatus != "1") {  //只要不是创建状态，不可修改
                self.orderTypeReadonly = true;
            }

            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }
        function initDictionary() {

            //订单类型
            self.typeOrderTypeSelect = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editJS.Tips_2'), ItemValue: "" }]
            };
            //订单状态
            self.typeOrderStatusSelect = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editJS.Tips_2'), ItemValue: "" }]
            };
            self.typeClientSelect = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editJS.Tips_2'), ItemCode: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editJS.Tips_2'), ItemCode: "" }]
            };

            commonService.getDataItemDuatil("OrderType").then(function (res) {
                if (res && res.data.success) {
                    self.typeOrderTypeSelect.options = res.data.resultData;
                    self.typeOrderTypeSelect.value = self.typeOrderTypeSelect.options.find(t => t.ItemValue == self.currentItem.OrderType);
                }
            })
            commonService.getDataItemDuatil("OrderStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeOrderStatusSelect.options = res.data.resultData;
                    self.typeOrderStatusSelect.value = self.typeOrderStatusSelect.options.find(t => t.ItemValue == self.currentItem.OrderStatus);
                }
            })

            commonService.getKeyParameterItem({ EnCode: "client" }).then(function (res) {

                if (res && res.data.success) {
                    self.typeClientSelect.options = res.data.resultData;
                    self.typeClientSelect.value = self.typeClientSelect.options.find(t => t.ItemCode == self.currentItem.Customer);
                }
            })

        }

        //编辑保存
        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editJS.Tips_3') });
            //字典类型 取值参考

            self.currentItem.UpdateByName = self.UserCode + '-' + self.UserName;
            self.currentItem.UpdateByCode = self.UserCode;
            if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                self.currentItem.UpdateByName = self.UserCode;
            }
            self.currentItem.Customer = self.typeClientSelect.value.ItemCode;
            self.currentItem.OrderType = self.typeOrderTypeSelect.value.ItemValue;

            //处理时间
            self.currentItem.OrderDate = commonService.ConvertToLocalTime(self.OrderDate);
            self.currentItem.DeliveryDate = commonService.ConvertToLocalTime(self.DeliveryDate);
            self.currentItem.BoxDate = commonService.ConvertToLocalTime(self.BoxDate);

            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };


            var url = commonService.getMesApiAddress('plan') + 'PL_ProductionOrder/SavePL_ProductionOrder';

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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editJS.Tips_4'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editJS.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editJS.Tips_5'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_Plan_ProductionOrder';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/Plan';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductionOrder-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.Plan.editJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
