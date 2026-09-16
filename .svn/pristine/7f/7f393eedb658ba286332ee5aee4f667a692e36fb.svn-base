(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.Process').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.Process.ProcessOperation.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationeditctrl.Tips_1'));
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
            self.MaterialClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationeditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationeditctrl.Tips_2'), ItemValue: "" }]
            };

            //初始化 
            self.SmallClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationeditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationeditctrl.Tips_2'), ItemValue: "" }]
            };
            self.TypeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationeditctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationeditctrl.Tips_2'), ResourceCode: "" }]
            }

            commonService.getDataItemDuatil("MaterialType").then(function (res) {
                if (res && res.data.success) {
                    self.MaterialClass.options = res.data.resultData;
                    self.MaterialClass.value = res.data.resultData.find(t => t.ItemValue == self.currentItem.MaterialClass)
                }
            })
            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.SmallClass.options = res.data.resultData;
                    self.SmallClass.value = res.data.resultData.find(t => t.ItemValue == self.currentItem.SmallClass)
                }
            })
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.TypeFactory.options = res.data.resultData;
                    self.TypeFactory.value = self.TypeFactory.options.find(t => t.ResourceCode == self.currentItem.FactoryCode);
                }
            });
        }

        //编辑保存
        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationeditctrl.Tips_3') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            //物料分类 下拉取值
            self.currentItem.MaterialClass = self.MaterialClass.value.ItemValue;
            // 下拉取值
            self.currentItem.SmallClass = self.SmallClass.value.ItemValue;
            self.currentItem.FactoryCode = self.TypeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.TypeFactory.value.ResourceName;
            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("material") + 'BS_Process/SaveBS_Process';

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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationeditctrl.Tips_4'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationeditctrl.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationeditctrl.Tips_5'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_Process_ProcessOperation';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/Process';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProcessOperation-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.Process.ProcessOperationeditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
