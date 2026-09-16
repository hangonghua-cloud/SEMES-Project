/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 设备刀具更换记录
*  2. 创建人员： 王坤
*  3. 创建日期： 2021-08-05
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTool.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            //初始化
            init();
            //注册事件
            registerEvents();

            self.currentItem = {};
            //self.currentItem.EquipmentId = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_1');//如果新增有系统自动生成编号 可以在这写一个初始值, 这个控件要只读状态,后台代码生成编号+流水号

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_2'));
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
            self.currentItem = {};
            self.validInputs = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            self.SelectResourceModal = SelectResourceModal;
            self.SelectPeopleModal = SelectPeopleModal;
            self.SelectToolsModal = SelectToolsModal;
            self.selectCodad = selectCodad;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            initDictionary();
        }

        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_3'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_3'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_3')
                    });
                }
            });

            self.IsUsed = [
                {
                    label: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_4'),
                    checked: true
                }
            ];
        }

        function selectCodad(oldValue, newValue) {
            self.currentItem.LineName = "";
            self.currentItem.LineCode = "";
        }
        //选择设备  使用公用方法
        function SelectResourceModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'LineCode',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_6'),
                    width: 200
                },
                {
                    field: 'LineName',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_7'),
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
            if (!self.typeFactory.value.ResourceCode) {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_8'));
                return;
            }
            var query = {
                Factory: self.typeFactory.value.ResourceCode
            }

            commonService.Select_SingleChoiceModalByValue(commonService.getMesApiAddress("equipment") + "EP_EquipmentTool/GetLinePageListJson", "LineCode", "asc", columnDefs, Select_SingleChoiceModalLine_callback, query);
            //commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_9'),commonService.getMesApiAddress("equipment") + "EP_EquipmentTool/GetLinePageListJson",[{'FieldCode': 'Factory', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_10'), 'FiledType': 'Select'},{ 'FieldCode': 'queryCode', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_6'), 'FiledType': 'Text' }, { 'FieldCode': 'queryName', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_7'), 'FiledType': 'Text' }], "LineCode", "asc", columnDefs, Select_SingleChoiceModalLine_callback, query); 
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalLine_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItem.LineName = res.LineName;
            self.currentItem.LineCode = res.LineCode;
        }

        //选择设备  使用公用方法
        function SelectPeopleModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'Code',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_11'),
                    width: 200
                },
                {
                    field: 'Name',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_12'),
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
            if (!self.typeFactory.value.ResourceCode) {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_8'));
                return;
            }
            var query = {
                FactoryCode: self.typeFactory.value.ResourceCode
            }

            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_13'), commonService.getMesApiAddress() + "Base/GetEmployeePageList", [{ 'FieldCode': 'queryCode', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_11'), 'FiledType': 'Text' }, { 'FieldCode': 'queryName', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_14'), 'FiledType': 'Text' }], "Code", "asc", columnDefs, Select_SingleChoiceModalPeople_callback, query);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalPeople_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItem.PersonOfReplaceName = res.Name;
            self.currentItem.PersonOfReplace = res.Code;
        }

        //选择设备  使用公用方法
        function SelectToolsModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'MaterialCode',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_15'),
                    width: 200
                },
                {
                    field: 'MaterialName',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_16'),
                    width: 200
                },
                {
                    field: 'Spec',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_17'),
                    width: 200
                },
                {
                    field: 'UnitName',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_18'),
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
            var query = {
                MaterialClass: ""
            };
            debugger
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_19'), commonService.getMesApiAddress("equipment") + "EP_EquipmentMalfunctionRepair/GetMaterialPageListWithClassJson", [{ 'FieldCode': 'queryCode', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_20'), 'FiledType': 'Text' }, { 'FieldCode': 'queryName', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_16'), 'FiledType': 'Text' }], "MaterialCode", "asc", columnDefs, Select_SingleChoiceModalTools_callback);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalTools_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItem.ToolsName = res.MaterialName;
            self.currentItem.ToolId = res.MaterialCode;
            self.currentItem.SpecificationsModels = res.Spec;
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {

            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.typeFactory.value.ResourceName;
            
            if (self.currentItem.DateOfReplaceStr != null)
                self.currentItem.DateOfReplace = moment(self.currentItem.DateOfReplaceStr).format("YYYY-MM-DD");
            self.currentItem.IsUsed = true;//self.IsUsed[0].checked;

            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItem.ModifyBy = self.UserCode;
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_21') });
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentTool/SaveEP_EquipmentTool';
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
            console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_22'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_23'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentTool.EP_EquipmentTooladdctrl.Tips_23'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentTool_EP_EquipmentTool';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentTool';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EP_EquipmentTool-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Add'
            }
        };
        $stateProvider.state(state);
    }
}());
