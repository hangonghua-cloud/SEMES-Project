(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.TeamPerson').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.TeamPerson.TeamPerson.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('customCommon.Title.Edit'));
            sidePanelManager.open('e');//使用窄弹窗
            // 使用宽右侧弹窗
            // sidePanelManager.open({
            //     mode: 'e',
            //     size: 'wide'
            // });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.IsReadOnly = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.FactoryChange = FactoryChange;

            initDictionary();
        }

        function initDictionary() {
            self.Factory = {
                value: { ResourceName: commonService.$t('customCommon.SelectTips'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('customCommon.SelectTips'), ResourceCode: "" }]
            };
            self.Process = {
                value: { ResourceName: commonService.$t('customCommon.SelectTips'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('customCommon.SelectTips'), ResourceCode: "" }]
            };

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('customCommon.SelectTips')
                    });
                    self.Factory.value = self.Factory.options.find(t => t.ResourceCode == self.currentItem.FactoryCode);
                }
            });
        }

        function FactoryChange(oldItem, newItem) {
            commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.Process.options = res.data.resultData;
                    self.Process.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('customCommon.SelectTips')
                    });
                    self.Process.value = self.Process.options.find(t => t.ResourceCode == self.currentItem.ProcessCode);
                    self.IsReadOnly = true;
                }
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //编辑保存
        function save() {

            self.currentItem.FactoryCode = self.Factory.value.ResourceCode;
            self.currentItem.FactoryName = self.Factory.value.ResourceName;
            self.currentItem.ProcessCode = self.Process.value.ResourceCode;
            //self.currentItem.ProcessName = self.Process.value.ResourceName;

            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_TeamPerson/SavePM_TeamPerson';
            busyIndicatorService.show({ message: commonService.$t('customCommon.Saveing') });
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
                commonService.showInfo(commonService.$t('customCommon.SaveSuccess'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('customCommon.ErrorOperate'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('customCommon.ErrorOperate'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_TeamPerson_TeamPerson';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/TeamPerson';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/TeamPerson-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'customCommon.Title.Edit'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
