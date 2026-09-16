(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PMPostCoefficient').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PMPostCoefficient.PMPostCoefficient.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams,
        common, $filter, $scope, commonService, auth, notificationService,
        busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();

        // Initialization function
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PMPostCoefficient.addJS.Tips_1'));
            sidePanelManager.open('e');
            //sidePanelManager.open({
            //  mode: "e",
            //   size: "wide"
            //});
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.main = angular.copy($stateParams.selectedItem);
            self.currentItem = {
                PostId: self.main.Id,
                ProcessCode: self.main.ProcessCode,
                PostCode: self.main.PostCode
            };
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            //数据字典
            initDictionary();
        }

        function initDictionary() {

        }

        function save() {

            var postData = {
                KeyValue: '',
                Entity: self.currentItem
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPostCoefficient.addJS.Tips_2') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PMPostCoefficient/SaveForm';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        //取消
        function cancel() {
            sidePanelManager.close();//关闭侧边栏
            $state.go('^');//返回列表(父页面)
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();//关闭遮罩层
                sidePanelManager.close();//关闭侧边栏
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.PMPostCoefficient.addJS.Tips_3'));
                $rootScope.$emit('to-parentDetail', "");//刷新局部
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PMPostCoefficient.addJS.Tips_4'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.PMPostCoefficient.addJS.Tips_4'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }
    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_PMPostCoefficient_PMPostCoefficient';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PMPostCoefficient';

        var state = {
            name: moduleStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PMPostCoefficient-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PMPostCoefficient.addJS.Tips_1'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
