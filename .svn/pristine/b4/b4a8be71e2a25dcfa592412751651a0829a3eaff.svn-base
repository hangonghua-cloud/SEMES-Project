(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MaterialMain').config(EditDetailScreenStateConfig);

    EditDetailScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterial.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditDetailScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;


        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            self.currentItem = angular.copy($stateParams.selectedItem);
            if (self.currentItem.AttrType == "1") {
                self.currentItem.AttrValue = parseFloat(self.currentItem.AttrValue)
            }
            else if (self.currentItem.AttrType == "3") {
                self.currentItem.AttrValue = new Date(self.currentItem.AttrValue);
            }
            self.validInputs = false;

            self.AttrType = {
                options: [],
                selectedOption: {}
            }

            commonService.getDataItemDuatil("AttrType").then(function (res) {
                if (res && res.data.success) {
                    self.AttrType.options = res.data.resultData;
                    self.AttrType.value = res.data.resultData.find(t => t.ItemValue == self.currentItem.AttrType);
                }
            })
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {

            if (self.currentItem.AttrType == "3") {
                var dete = $filter('date')(new Date(self.currentItem.AttrValue), 'yyyy-MM-dd HH:mm:ss');
                self.currentItem.AttrValue = dete;
            }

            self.currentItem.AttrType = self.AttrType.value.ItemValue;
            //self.currentItem.AttrName=self.AttrType.value.ItemName;

            var postData = {
                KeyValue: self.currentItem.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("material") + 'Base_MaterialFacet/SaveBase_MaterialFacet';

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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditDetailctrl.Tips_2'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', 'parentDetail');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditDetailctrl.Tips_3'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditDetailctrl.Tips_3'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditDetailScreenStateConfig.$inject = ['$stateProvider'];
    function EditDetailScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MaterialMain_FactoryMaterial';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MaterialMain';

        var state = {
            name: screenStateName + '.editDetail',
            url: '/editDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/FactoryMaterial-editDetail.html',
                    controller: EditDetailScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialeditDetailctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
