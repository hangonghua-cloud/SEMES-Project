(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.ProductDispatch').config(TakeOffScreenStateConfig);

    TakeOffScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatch.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function TakeOffScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchtakeoffctrl.Tips_1'));
            sidePanelManager.open('e');
            // sidePanelManager.open({
            //     mode: "e",
            //     size: "small"
            // });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.DeliveryDate = new Date(self.currentItem.DeliveryDate);
            self.Off_PostDate = new Date();
            self.validInputs = false;


            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //编辑保存
        function save() {

            self.currentItem.DeliveryDate = commonService.ConvertToLocalDate(self.DeliveryDate);
            self.currentItem.Off_PostDate = commonService.ConvertToLocalDate(self.Off_PostDate);

            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchtakeoffctrl.Tips_2') });
            var url = commonService.getMesApiAddress("material") + 'MM_ProductDispatchItem/ProductDispatchItemTakOff';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchtakeoffctrl.Tips_3'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchtakeoffctrl.Tips_4'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchtakeoffctrl.Tips_4'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    TakeOffScreenStateConfig.$inject = ['$stateProvider'];
    function TakeOffScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_ProductDispatch_ProductDispatch';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/ProductDispatch';

        var state = {
            name: screenStateName + '.takeoff',
            url: '/takeoff/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductDispatch-takeoff.html',
                    controller: TakeOffScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchtakeoffctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
