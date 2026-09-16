(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatch.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams,
        common, $filter, $scope, commonService, auth, notificationService,
        busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();

        // Initialization function
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatcheditctrl.Tips_1'));
            //sidePanelManager.open('e');
            sidePanelManager.open({
                mode: "e",
                size: "small"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            //Expose Model Methods
            self.cancel = cancel;
            self.save = save;

        }

        function save() {

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatcheditctrl.Tips_3') });
            var url = commonService.getMesApiAddress("material") + 'MMRawMaterialDispatch/SaveForm';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatcheditctrl.Tips_4'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatcheditctrl.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatcheditctrl.Tips_5'));
        }

        //取消
        function cancel() {
            sidePanelManager.close();//关闭侧边栏
            $state.go('^');//返回列表(父页面)
        }
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }
    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_MMRawMaterialDispatch_MMRawMaterialDispatch';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MMRawMaterialDispatch';

        var state = {
            name: moduleStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/MMRawMaterialDispatch-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatcheditctrl.Tips_1'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
