(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance').config(EditStandardScreenStateConfig);

    EditStandardScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenance.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', '$rootScope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditStandardScreenController(dataService, $state, $stateParams, common, $filter, $scope, $rootScope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditstandardctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.currentItemDetail = angular.copy($stateParams.selectedItemDetail);
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            //数据类型
            self.DataTypeConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            GetDataTypeDictionary();
        }

        //设备类型
        function GetDataTypeDictionary() {
            var url = commonService.getDataItemDuatil("DataType").then(function (res) {
                self.DataTypeConfig.options = res.data.resultData;;
                self.DataTypeConfig.selectedOption = { ItemValue: self.currentItemDetail.DataType, ItemName: self.currentItemDetail.DataTypeName };
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditstandardctrl.Tips_2') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;

            if (self.DataTypeConfig.selectedOption.ItemValue != "") {
                self.currentItemDetail.DataType = self.DataTypeConfig.selectedOption.ItemValue;
                self.currentItemDetail.DataTypeName = self.DataTypeConfig.selectedOption.ItemName;
            }
            else {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditstandardctrl.Tips_3'));
                return;
            }


            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItemDetail.ModifyBy = self.UserCode;
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItemDetail.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItemDetail
            };


            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentCheckItemDetail/SaveEP_EquipmentCheckItemDetail';

            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

            busyIndicatorService.hide();
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {

            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditstandardctrl.Tips_4'));
                //刷新局部
                $rootScope.$emit('to-editChildItem', self.currentItemDetail);
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditstandardctrl.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditstandardctrl.Tips_5'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditStandardScreenStateConfig.$inject = ['$stateProvider'];
    function EditStandardScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentCheckItemMaintenance_EP_EquipmentCheckItemMaintenance';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentCheckItemMaintenance';

        var state = {
            name: screenStateName + '.editstandard',
            url: '/editstandard/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EP_EquipmentCheckItemMaintenance-editstandard.html',
                    controller: EditStandardScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditstandardctrl.Tips_1'
            },
            params: {
                selectedItem: null,
                selectedItemDetail: null,
            }
        };
        $stateProvider.state(state);
    }
}());
