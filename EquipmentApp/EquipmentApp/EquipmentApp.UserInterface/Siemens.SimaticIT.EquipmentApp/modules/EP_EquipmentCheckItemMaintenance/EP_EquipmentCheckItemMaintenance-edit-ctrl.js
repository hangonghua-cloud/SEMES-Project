(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenance.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', '$rootScope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, $rootScope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditctrl.Tips_1'));
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
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            //工厂
            self.CodadConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            //设备类型
            self.TypeConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            self.IsUsed = [
                {
                    label: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditctrl.Tips_2'),
                    checked: true
                }
            ];

            if (self.currentItem.IsUsed == "1")
                self.IsUsed[0].checked = true;
            else
                self.IsUsed[0].checked = false;

            GetCodadList();
            GetTypeDictionary();
        }
        //工厂
        function GetCodadList() {
            var postData = {
                "queryJson": {
                    "ResourceCode": "",
                    "ResourceName": "",
                    "ModelLevel": "Factory"
                }
            };
            var url = commonService.getMesApiAddress("factory") + "LevelManage/BsModelWithResource/GetListJson";
            console.log(JSON.stringify(postData));
            commonService.callWebApiPost(url, postData).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    self.CodadConfig.options = jsonData;
                    self.CodadConfig.selectedOption = { ResourceCode: self.currentItem.Factory, ResourceName: self.currentItem.FactoryName };
                } else {
                    console.log(self.ParentResourceCtrl.options);
                }
                //self.SecondValue = jsonData[0];
                self.CodadConfig.options.splice(0, 0, { ResourceCode: "", ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditctrl.Tips_3') })
            }, function (error) {
                // console.log('-----------error------------');
                console.log(error);
            });
        }
        //设备类型
        function GetTypeDictionary() {
            var url = commonService.getDataItemDuatil("EquipmentTypes").then(function (res) {
                self.TypeConfig.options = res.data.resultData;;
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditctrl.Tips_4') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;

            // if(self.CodadConfig.selectedOption != null){
            //     self.currentItem.Factory = self.CodadConfig.selectedOption.ResourceCode;
            // }
            if (self.TypeConfig.selectedOption != null) {
                self.currentItem.EquipmentType = self.TypeConfig.selectedOption.ItemValue;
            }
            self.currentItem.IsUsed = self.IsUsed[0].checked ? "1" : "2";

            console.log("postData------------------------------------" + JSON.stringify(self.currentItem));

            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItem.ModifyBy = self.UserCode;
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            console.log("postData------------------------------------" + JSON.stringify(postData));
            // commonService.getMesApiAddress() = '/sitSrvApi/'
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentCheckItemMaintenance/SaveEP_EquipmentCheckItemMaintenance';
            console.log("url----------------" + url);
            //提交数据
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            console.log("SaveEP_EquipmentCheckItemMaintenance----------------------" + JSON.stringify(req));
            busyIndicatorService.hide();
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditctrl.Tips_5'));
                $rootScope.$emit('to-editItem', self.currentItem);
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditctrl.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditctrl.Tips_6'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentCheckItemMaintenance_EP_EquipmentCheckItemMaintenance';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentCheckItemMaintenance';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EP_EquipmentCheckItemMaintenance-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckItemMaintenance.EP_EquipmentCheckItemMaintenanceeditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
