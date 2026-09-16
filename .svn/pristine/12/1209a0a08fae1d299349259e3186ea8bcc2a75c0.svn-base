(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.SuperProductStock').config(AdjustScreenStateConfig);

    AdjustScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AdjustScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageadjustctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function initDictionary() {


        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //编辑保存
        function save() {
            if (self.currentItem.AdjustQty < 0) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageadjustctrl.Tips_2'));
                return;
            }
            if (self.currentItem.AdjustQty == self.currentItem.StockQty) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageadjustctrl.Tips_3'));
                return;
            }
            if (self.currentItem.AdjustQty < self.currentItem.LockedQty) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageadjustctrl.Tips_4'));
                return;
            }

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            var url = commonService.getMesApiAddress("material") + 'MM_SuperProductStock/SuperProductStockAdjust';
            //提交数据
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageadjustctrl.Tips_5') });
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
                // sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageadjustctrl.Tips_6'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageadjustctrl.Tips_7'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageadjustctrl.Tips_7'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AdjustScreenStateConfig.$inject = ['$stateProvider'];
    function AdjustScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_SuperProductStock_SuperProductStockManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/SuperProductStock';

        var state = {
            name: screenStateName + '.adjust',
            url: '/adjust/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/SuperProductStockManage-adjust.html',
                    controller: AdjustScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageadjustctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
