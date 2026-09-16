(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.Process').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.Process.ProcessOperation.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddctrl.Tips_1'));
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
            initDictionary();
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function initDictionary() {
            //初始化 物料分类
            self.MaterialClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddctrl.Tips_2'), ItemValue: "" }]
            };

            //初始化 
            self.SmallClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddctrl.Tips_2'), ItemValue: "" }]
            };
            self.TypeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddctrl.Tips_2'), ResourceCode: "" }]
            }

            commonService.getDataItemDuatil("MaterialType").then(function (res) {
                if (res && res.data.success) {
                    self.MaterialClass.options = res.data.resultData;
                    self.MaterialClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.SmallClass.options = res.data.resultData;
                    self.SmallClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.TypeFactory.options = res.data.resultData;
                    self.TypeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddctrl.Tips_2')
                    });
                }
            });


        }

        //保存
        function save() {
            // 下拉取值
            self.currentItem.MaterialClass = self.MaterialClass.value.ItemValue;
            self.currentItem.SmallClass = self.SmallClass.value.ItemValue;
            self.currentItem.FactoryCode = self.TypeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.TypeFactory.value.ResourceName;
            self.currentItem.ProcessType = "1";
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            var url = commonService.getMesApiAddress("material") + 'BS_Process/SaveBS_Process';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddctrl.Tips_3') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddctrl.Tips_4'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddctrl.Tips_5'));
            }
        }
        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddctrl.Tips_5'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_Process_ProcessOperation';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/Process';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProcessOperation-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
