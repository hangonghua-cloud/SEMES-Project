/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 设备故障报修
*  2. 创建人员： 王坤
*  3. 创建日期： 2021-08-05
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair').config(AddRepairScreenStateConfig);

    AddRepairScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepair.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddRepairScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            //初始化
            init();
            //注册事件
            registerEvents();

            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddrepairctrl.Tips_1'));
            sidePanelManager.open('e');
            //使用宽右侧弹窗
            //sidePanelManager.open({
            //mode: 'e',
            //size: 'wide'
            //});
        }

        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        //初始化
        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //初始化前端变量数据
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.currentItem.FinishTime2 = "aaa";//self.currentItem.FinishTime;
            self.validInputs = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            self.SelectPeopleModal = SelectPeopleModal;

        }

        //选择设备  使用公用方法
        function SelectPeopleModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddrepairctrl.Tips_2'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'Code',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddrepairctrl.Tips_3'),
                    width: 200
                },
                {
                    field: 'Name',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddrepairctrl.Tips_4'),
                    width: 200
                }
            ];
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddrepairctrl.Tips_5'),commonService.getMesApiAddress() + "Base/GetEmployeePageList", [{ 'FieldCode': 'queryCode', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddrepairctrl.Tips_3'), 'FiledType': 'Text' }, { 'FieldCode': 'queryName', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddrepairctrl.Tips_6'), 'FiledType': 'Text' }],"Code", "asc", columnDefs, Select_SingleChoiceModalPeople_callback);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalPeople_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItem.RepairingPersonName = res.Name;
            self.currentItem.RepairingPerson = res.Code;
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {
            if (self.currentItem.curFinishTime != null) {
                self.currentItem.FinishTime = moment(self.currentItem.curFinishTime).format("YYYY-MM-DD HH:mm:ss");
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddrepairctrl.Tips_7') });

            self.currentItem.RepairingStatus = "2";

            var postData = {
                KeyValue: self.currentItem.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };


            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentMalfunctionRepair/SaveEP_EquipmentMalfunctionRepair';

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

            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddrepairctrl.Tips_8'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddrepairctrl.Tips_9'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddrepairctrl.Tips_9'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddRepairScreenStateConfig.$inject = ['$stateProvider'];
    function AddRepairScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMalfunctionRepair_EP_EquipmentMalfunctionRepair';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentMalfunctionRepair';

        var state = {
            name: screenStateName + '.addrepair',
            url: '/addrepair',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EP_EquipmentMalfunctionRepair-addrepair.html',
                    controller: AddRepairScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Add'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
