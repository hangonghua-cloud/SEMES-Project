(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.TraitProcess').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperation.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationeditDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            initDictionary();
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function initDictionary() {
            //初始化 物料分类
            self.typeOperation = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationeditDetailctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationeditDetailctrl.Tips_2'), ResourceCode: "" }]
            }
            commonService.getProcessByFactory({ LevelCode: self.currentItem.FactoryCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeOperation.options = res.data.resultData;
                    self.typeOperation.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationeditDetailctrl.Tips_2')
                    });
                    self.typeOperation.value = self.typeOperation.options.find(t => t.ResourceCode == self.currentItem.OperationCode);
                }
            });

            self.typeOutWarehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationeditDetailctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationeditDetailctrl.Tips_2'), ResourceCode: "" }]
            }

            commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeOutWarehouse.options = res.data.resultData;
                    self.typeOutWarehouse.value = self.typeOutWarehouse.options.find(t => t.ResourceCode == self.currentItem.OutWarehouse);
                }
            });

        }
        //编辑保存
        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationeditDetailctrl.Tips_3') });
            self.currentItem.OperationCode = self.typeOperation.value.ResourceCode;
            self.currentItem.OperationName = self.typeOperation.value.ResourceName;
            //self.currentItem.OutWarehouse = self.typeOutWarehouse.value.ResourceCode;
            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("material") + 'BS_ProcessOfOperations/SaveBS_ProcessOfOperations';

            commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationeditDetailctrl.Tips_4'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationeditDetailctrl.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationeditDetailctrl.Tips_5'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_TraitProcess_TraitProcessOperation';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/TraitProcess';

        var state = {
            name: screenStateName + '.editDetail',
            url: '/editDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/TraitProcessOperation-editDetail.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationeditDetailctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
