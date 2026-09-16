(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.IPQCManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenance.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;


            //Initialize Model Data
            self.currentItem = {};

            self.validInputs = false;
            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.SelectwlModal = SelectwlModal;



        }
        function initDictionary() {
            //初始化 物料分类
            self.typeTest = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_2'), ItemValue: "" }]
            };

            self.typeEnabled = {
                value: { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_3') },
                options: [
                    { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_3') },
                    { ItemValue: false, ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_4') }
                ]
            };
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_2'), ResourceCode: "" }]
            };


            commonService.getDataItemDuatil("PQCInspection").then(function (res) {
                if (res && res.data.success) {
                    self.typeTest.options = res.data.resultData;
                    self.typeTest.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })


            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_2')
                    });
                }
            });


        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function SelectwlModal() {

            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'GroupCode',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_6'),
                    width: 200
                },
                {
                    field: 'GroupName',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_7'),
                    width: 200
                }
            ];
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_8'), commonService.getMesApiAddress("quality") + "QC_TestMaintenance/GetwlZList", [{ 'FieldCode': 'GroupName', 'FileldName': commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_7'), 'FiledType': 'Text' }], "GroupCode", "asc", columnDefs, Select_SingleChoiceModalEquipment_callback);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalEquipment_callback(res) {
            //alert(JSON.stringify(res));
            debugger

            self.currentItem.GroupCode = res.GroupCode;
            self.currentItem.GroupName = res.GroupName;
        }




        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_9') });

            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.TestType = self.typeTest.value.ItemValue;
            self.currentItem.IsEnabled = self.typeEnabled.value.ItemValue;
            self.currentItem.SmallClass = self.currentItem.GroupName;

            var postData = {
                KeyValue: '',
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("quality") + 'QC_TestMaintenance/SaveQC_TestMaintenance';

            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

            busyIndicatorService.hide();
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
            // console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_10'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_11'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_11'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_IPQCManage_TestMaintenance';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/IPQCManage';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/TestMaintenance-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.IPQCManage.TestMaintenanceaddctrl.Tips_12'
            }
        };
        $stateProvider.state(state);
    }
}());
