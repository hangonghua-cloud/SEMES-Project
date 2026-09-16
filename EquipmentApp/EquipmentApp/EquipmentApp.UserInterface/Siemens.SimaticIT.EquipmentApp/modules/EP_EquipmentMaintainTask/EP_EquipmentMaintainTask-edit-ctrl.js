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
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTask.service', '$state', '$stateParams',
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

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskeditctrl.Tips_1'));
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
            self.curPlanDate = new Date(self.currentItem.PlanDate);
            self.validInputs = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            self.SelectEquipmentModal = SelectEquipmentModal;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            GetTaskList();

            //保养任务
            self.TaskConfig = {
                value: null,
                selectedOption: null,
                options: []
            };
        }

        //保养任务列表
        function GetTaskList() {
            var url = commonService.getMesApiAddress("equipment") + "EP_EquipmentMaintain/GetEP_EquipmentMaintainList?checkType=";
            //console.log(JSON.stringify(postData));
            commonService.callWebApiGet(url, null).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    self.TaskConfig.options = jsonData;
                    self.TaskConfig.selectedOption = { EquipmentTaskId: self.currentItem.EquipmentMaintainTaskId, EquipmentTaskName: self.currentItem.EquipmentMaintainTaskName };
                } else {
                    console.log(self.TaskConfig.options);
                }
                //self.SecondValue = jsonData[0];
                self.TaskConfig.options.splice(0, 0, { EquipmentTaskId: "", EquipmentTaskName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskeditctrl.Tips_2') })
            }, function (error) {
                // console.log('-----------error------------');
                console.log(error);
            });
        }

        //选择设备  使用公用方法
        function SelectEquipmentModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskeditctrl.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'EquipmentId',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskeditctrl.Tips_4'),
                    width: 200
                },
                {
                    field: 'EquipmentName',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskeditctrl.Tips_5'),
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
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskeditctrl.Tips_4'), commonService.getMesApiAddress("equipment") + "EquipmentManage/GetPage_Equipment", [{ 'FieldCode': 'EquipmentId', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskeditctrl.Tips_4'), 'FiledType': 'Text' }, { 'FieldCode': 'EquipmentName', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskeditctrl.Tips_5'), 'FiledType': 'Text' }], "EquipmentId", "asc", columnDefs, Select_SingleChoiceModalEquipment_callback);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalEquipment_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItem.EquipmentName = res.EquipmentName;
            self.currentItem.EquipmentId = res.EquipmentId;
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //编辑保存
        function save() {


            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItem.ModifyBy = self.UserCode;
            self.currentItem.EquipmentMaintainTaskId = self.TaskConfig.selectedOption.EquipmentTaskId;
            self.currentItem.PlanDate = moment(self.currentItem.curPlanDate).format("YYYY-MM-DD HH:mm:ss");
            console.log("username------------------------------------" + self.UserName);
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentMaintainTask/SaveEP_EquipmentMaintainTask';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskeditctrl.Tips_6') });
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
            console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskeditctrl.Tips_7'));
                //刷新局部
                $rootScope.$emit('to-editItem', self.currentItem);
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskeditctrl.Tips_8'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintainTask.EP_EquipmentMaintainTaskeditctrl.Tips_8'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMaintainTask_EP_EquipmentMaintainTask';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentMaintainTask';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EP_EquipmentMaintainTask-edit.html',
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
