/**
*  0. 代码生成： UA主子表一键生成前后端html、JS、API接口代码生成器 Ver 1.11 更新日期：2021-07-14  设计者：刘万军
*  1. 功能描述： 物料属性模板维护
*  2. 创建人员： liyongguo
*  3. 创建日期： 2021-07-21
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MaterialBindTemp').config(EditDetailScreenStateConfig);

    EditDetailScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemp.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal','$rootScope'];
    function EditDetailScreenController(dataService, $state, $stateParams, common, $filter, $scope,
        commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
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

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempeditDetailctrl.Tips_1'));
            sidePanelManager.open('e');//使用窄弹窗
            //使用宽右侧弹窗
            // sidePanelManager.open({
            //     mode: 'e',
            //     size: 'wide'
            // });
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


            self.typeEnabledSelect = {
                options: [
                    { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempeditDetailctrl.Tips_2') },
                    { ItemValue: false, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempeditDetailctrl.Tips_3') },
                ],
                selectedOption: { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempeditDetailctrl.Tips_2') }
            }
            self.typeAttrTypeSelect = {
                options: [],
                selectedOption: {}
            }

            commonService.getDataItemDuatil("AttrType").then(function (res) {
                if (res && res.data.success) {
                    self.typeAttrTypeSelect.options = res.data.resultData;
                    self.typeAttrTypeSelect.selectedOption = res.data.resultData.find(t => t.ItemValue == self.currentItem.AttrType);
                }
            })

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
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempeditDetailctrl.Tips_4') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;

           // self.currentItem.IsEnabled = self.typeEnabledSelect.selectedOption.ItemValue;
            self.currentItem.IsEnabled = true;
            if (self.typeAttrTypeSelect.selectedOption && self.typeAttrTypeSelect.selectedOption != "") {
                self.currentItem.AttrType = self.typeAttrTypeSelect.selectedOption.ItemValue;
            } else {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempeditDetailctrl.Tips_5'), commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempeditDetailctrl.Tips_6'));
                busyIndicatorService.hide();
                return;
            }

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTempFacet/SaveBase_MaterialBindTempFacet';

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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempeditDetailctrl.Tips_7'));
                //刷新局部
                 $rootScope.$emit('to-parentdetail', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempeditDetailctrl.Tips_8'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempeditDetailctrl.Tips_8'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditDetailScreenStateConfig.$inject = ['$stateProvider'];
    function EditDetailScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MaterialBindTemp_MaterialBindTemp';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MaterialBindTemp';

        var state = {
            name: screenStateName + '.editDetail',
            url: '/editDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/MaterialBindTemp-editDetail.html',
                    controller: EditDetailScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTempeditDetailctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
