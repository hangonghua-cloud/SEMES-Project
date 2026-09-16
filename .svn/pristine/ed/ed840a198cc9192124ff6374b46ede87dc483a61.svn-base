(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.ProductionFirstInspection.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();

            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeDetailJS.Tips_1'));
            sidePanelManager.open('e');
            // sidePanelManager.open(
            //     {
            //         mode: 'e',
            //         size: 'wide'
            //     }
            // );
        }


        //初始化
        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //初始化前端变量数据
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.isQualityButtonVisible = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;

            initDictionary();


        }
        function initDictionary() {

        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //编辑保存
        function save() {

            var postData = {
                KeyValue: self.currentItem.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeDetailJS.Tips_2') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ProductionFirstInspectionDetail/SavePM_ProductionFirstInspectionDetail';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeDetailJS.Tips_3'));

                $rootScope.$emit('to-parentDetail', '');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeDetailJS.Tips_4'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeDetailJS.Tips_4'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_ProductionFirstInspection_ProductionFirstInspection';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/ProductionFirstInspection';

        var state = {
            name: screenStateName + '.editDetail',
            url: '/editDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductionFirstInspection-editDetail.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeDetailJS.Tips_5'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
