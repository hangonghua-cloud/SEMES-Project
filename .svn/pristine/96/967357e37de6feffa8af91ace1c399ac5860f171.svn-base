/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 设备保养项目详情
*  2. 创建人员： 王坤
*  3. 创建日期： 2021-08-05
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask').config(AddSpareScreenStateConfig);

    AddSpareScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTask.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', '$rootScope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddSpareScreenController(dataService, $state, $stateParams, common, $filter, $scope, $rootScope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            //初始化
            init();
            //注册事件
            registerEvents();

            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskaddsparectrl.Tips_1'));
            //sidePanelManager.open('e');//使用窄弹窗
            //使用宽右侧弹窗
            sidePanelManager.open({
                mode: 'e',
                //size: 'wide'
            });
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
            self.currentSpareItem = {};
            self.validInputs = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            self.SelectSpareModal = SelectSpareModal;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            GetUnitDictionary();

            self.UnitConfig = {
                value: null,
                selectedOption: null,
                options: []
            };
        }

        function GetUnitDictionary() {
            var url = commonService.getDataItemDuatil("Unit").then(function (res) {
                self.UnitConfig.options = res.data.resultData;;
            });
        }

        //选择设备  使用公用方法
        function SelectSpareModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskaddsparectrl.Tips_2'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'MaterialCode',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskaddsparectrl.Tips_3'),
                    width: 200
                },
                {
                    field: 'MaterialName',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskaddsparectrl.Tips_4'),
                    width: 200
                },
                {
                    field: 'Spec',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskaddsparectrl.Tips_5'),
                    width: 200
                },
                {
                    field: 'UnitName',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskaddsparectrl.Tips_6'),
                    width: 200
                }
            ];

            var postData = {
                MaterialClassName: "'BPBJ','DQYP'"
            }
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskaddsparectrl.Tips_7'), commonService.getMesApiAddress("material") + "Base_Material/GetBaseMaterialList", [{ 'FieldCode': 'queryCode', 'FileldName': ' 备件编码', 'FiledType': 'Text' }, { 'FieldCode': 'queryName', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskaddsparectrl.Tips_4'), 'FiledType': 'Text' }], "MaterialCode", "asc", columnDefs, Select_SingleChoiceModalSpare_callback, postData);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalSpare_callback(res) {
            //alert(JSON.stringify(res));
            self.currentSpareItem.SparePartsName = res.MaterialName;
            self.currentSpareItem.SparePartsId = res.MaterialCode;
            self.currentSpareItem.SpecificationsModels = res.Spec;
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {

            if (self.currentSpareItem.SparePartsId == "" || self.currentSpareItem.SparePartsId == null) {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskaddsparectrl.Tips_8'));
                return;
            }
            if (self.currentSpareItem.Num == "" || self.currentSpareItem.Num == null) {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskaddsparectrl.Tips_9'));
                return;
            }

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskaddsparectrl.Tips_10') });


            self.currentSpareItem.RepairId = self.currentItem.Id;
            self.currentItem.UseType = "2";


            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentSpareItem
            };

            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentSpareParts/SaveEP_EquipmentSpareParts';

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
                //sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskaddsparectrl.Tips_11'));
                self.currentSpareItem.SparePartsId = "";
                self.currentSpareItem.SparePartsName = "";
                self.currentSpareItem.SpecificationsModels = "";
                self.currentSpareItem.Num = null;
                //刷新局部
                $rootScope.$emit('to-addChildSpare', self.currentSpareItem);
                //$state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskaddsparectrl.Tips_12'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskaddsparectrl.Tips_12'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddSpareScreenStateConfig.$inject = ['$stateProvider'];
    function AddSpareScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMaintainTask_EP_EquipmentMaintainTask';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentMaintainTask';

        var state = {
            name: screenStateName + '.addspare',
            url: '/addspare',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EP_EquipmentMaintainTask-addspare.html',
                    controller: AddSpareScreenController,
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
