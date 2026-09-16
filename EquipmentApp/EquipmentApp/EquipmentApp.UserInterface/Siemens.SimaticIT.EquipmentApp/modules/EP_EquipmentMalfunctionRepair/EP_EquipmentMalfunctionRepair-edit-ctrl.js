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
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepair.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', '$rootScope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, $rootScope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            //传参实体定义赋值
            self.currentItem = angular.copy($stateParams.selectedItem);


            //初始化
            init();
            //注册事件
            registerEvents();

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_1'));
            sidePanelManager.open('e');//使用窄弹窗
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
            self.validInputs = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            self.SelectEquipmentModal = SelectEquipmentModal;
            self.SelectPeopleModal = SelectPeopleModal;

            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            //工厂
            self.CodadConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            //车间
            self.WorkshopConfig = {
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

            GetTypeDictionary();
        }

        //报修类别
        function GetTypeDictionary() {
            var url = commonService.getDataItemDuatil("RepairsCategory").then(function (res) {
                self.TypeConfig.options = res.data.resultData;;
                self.TypeConfig.selectedOption = { ItemValue: self.currentItem.RepairingType, ItemName: self.currentItem.RepairingTypeName };
            });
        }

        //选择设备  使用公用方法
        function SelectEquipmentModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_2'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'EquipmentId',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_3'),
                    width: 200
                },
                {
                    field: 'EquipmentName',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_4'),
                    width: 200
                }
            ];
            //queryName: "",
            //queryCode: ""
            /*功能描述:单选弹窗方法
            *创    建:刘万军
            *创建时间:2021-1-22
            *参    数:PostUrl API接口
            *         sidx  排序字段
            *         sord  排序方式
            *         columnDefs   grid显示字段列表
            *         callback  回调方法
            */
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_3'),commonService.getMesApiAddress("equipment") + "EquipmentManage/GetPage_Equipment", [{ 'FieldCode': 'EquipmentId', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_3'), 'FiledType': 'Text' }, { 'FieldCode': 'EquipmentName', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_4'), 'FiledType': 'Text' }], "EquipmentId", "asc", columnDefs, Select_SingleChoiceModalEquipment_callback);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalEquipment_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItem.EquipmentName = res.EquipmentName;
            self.currentItem.EquipmentId = res.EquipmentId;
        }

        //选择设备  使用公用方法
        function SelectPeopleModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_2'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'Code',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_5'),
                    width: 200
                },
                {
                    field: 'Name',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_6'),
                    width: 200
                }
            ];
            //queryName: "",
            //queryCode: ""
            /*功能描述:单选弹窗方法
            *创    建:刘万军
            *创建时间:2021-1-22
            *参    数:PostUrl API接口
            *         sidx  排序字段
            *         sord  排序方式
            *         columnDefs   grid显示字段列表
            *         callback  回调方法
            */
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_7'),commonService.getMesApiAddress() + "Base/GetEmployeePageList", [{ 'FieldCode': 'queryCode', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_5'), 'FiledType': 'Text' }, { 'FieldCode': 'queryName', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_8'), 'FiledType': 'Text' }],"Code", "asc", columnDefs, Select_SingleChoiceModalPeople_callback);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalPeople_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItem.CreatorName = res.Name;
            self.currentItem.Creator = res.Code;
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //编辑保存
        function save() {
            debugger
            if (self.TypeConfig.selectedOption.ItemValue != "") {
                self.currentItem.RepairingType = self.TypeConfig.selectedOption.ItemValue;
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_9') });

            self.currentItem.ModifyBy = self.UserCode;

            var postData = {
                KeyValue: self.currentItem.Id,
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_10'));
                //刷新局部
               // $rootScope.$emit('to-editItem', self.currentItem);
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_11'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditctrl.Tips_11'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMalfunctionRepair_EP_EquipmentMalfunctionRepair';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentMalfunctionRepair';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EP_EquipmentMalfunctionRepair-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Edit'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
