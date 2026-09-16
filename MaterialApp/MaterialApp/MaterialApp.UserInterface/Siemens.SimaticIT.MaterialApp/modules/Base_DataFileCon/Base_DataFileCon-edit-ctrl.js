/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.14 更新日期：2021-07-21  设计者：刘万军
*  1. 功能描述： 仓库安全库存
*  2. 创建人员： jpf
*  3. 创建日期： 2023-02-28
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.Base_DataFileCon').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.Base_DataFileCon.Base_DataFileCon.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            //初始化 是否归档
            self.isFile = {

                value: '1',
                options: [{
                    label: commonService.$t('Siemens.SimaticIT.MaterialApp.Base_DataFileCon.Base_DataFileConeditctrl.Tips_1'),
                    value: '1'
                }, {
                    label: commonService.$t('Siemens.SimaticIT.MaterialApp.Base_DataFileCon.Base_DataFileConeditctrl.Tips_2'),
                    value: '0'
                }]
            };

            //传参实体定义赋值
            self.currentItem = angular.copy($stateParams.selectedItem);



            //初始化
            init();
            //注册事件
            registerEvents();

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.Base_DataFileCon.Base_DataFileConeditctrl.Tips_3'));
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
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //编辑保存
        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.Base_DataFileCon.Base_DataFileConeditctrl.Tips_4') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            //是否归档 //单选取值
            self.currentItem.isFile = self.isFile.value;


            console.log("postData------------------------------------" + JSON.stringify(self.currentItem));

            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItem.ModifyName = self.UserName;
            self.currentItem.ModifyBy = self.UserCode;
            if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                self.currentItem.ModifyName = self.UserCode;
            }
            if (self.isFile.value == "1") {
                self.currentItem.isFile = true;

            } else {
                self.currentItem.isFile = false;
            }
            console.log("username------------------------------------" + self.UserName);
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            console.log("postData------------------------------------" + JSON.stringify(postData));
            // commonService.getMesApiAddress() = '/sitSrvApi/'
            var url = commonService.getMesApiAddress("material") + 'Base_DataFileCon/SaveBase_DataFileCon';
            console.log("url----------------" + url);
            //提交数据
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            console.log("SaveBase_DataFileCon----------------------" + JSON.stringify(req));
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.Base_DataFileCon.Base_DataFileConeditctrl.Tips_5'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.Base_DataFileCon.Base_DataFileConeditctrl.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.Base_DataFileCon.Base_DataFileConeditctrl.Tips_6'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_Base_DataFileCon_Base_DataFileCon';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/Base_DataFileCon';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/Base_DataFileCon-edit.html',
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
