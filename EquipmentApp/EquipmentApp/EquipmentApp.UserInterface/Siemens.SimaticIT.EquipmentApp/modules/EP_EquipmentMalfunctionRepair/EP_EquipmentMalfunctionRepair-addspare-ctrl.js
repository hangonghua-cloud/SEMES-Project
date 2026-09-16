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
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair').config(AddSpareScreenStateConfig);

    AddSpareScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepair.service', '$state', '$stateParams',
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


            self.currentItem = angular.copy($stateParams.selectedItem);
            //self.currentItem.EquipmentId = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_1');//如果新增有系统自动生成编号 可以在这写一个初始值, 这个控件要只读状态,后台代码生成编号+流水号

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_2'));
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
            self.currentItemDetail = {};
            self.validInputs = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            self.SelectSpareModal = SelectSpareModal;
            self.SelectPeopleModal = SelectPeopleModal;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            GetTypeDictionary();
            GetUnitDictionary();


            //类型
            self.TypeConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            //类型
            self.UnitConfig = {
                value: null,
                selectedOption: null,
                options: []
            };
        }

        //类型
        function GetTypeDictionary() {
            var url = commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                self.TypeConfig.options = res.data.resultData;;
            });
        }

        //类型
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
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'MaterialCode',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_4'),
                    width: 200
                },
                {
                    field: 'MaterialName',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_5'),
                    width: 200
                },
                {
                    field: 'Spec',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_6'),
                    width: 200
                },
                {
                    field: 'UnitName',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_7'),
                    width: 200
                }
            ];
            var postData = {
                MaterialClassName: "'BPBJ','DQYP'"
            }
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_8'), commonService.getMesApiAddress("material") + "Base_Material/GetBaseMaterialList", [{ 'FieldCode': 'queryCode', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_4'), 'FiledType': 'Text' }, { 'FieldCode': 'queryName', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_5'), 'FiledType': 'Text' }], "MaterialCode", "asc", columnDefs, Select_SingleChoiceModalSpare_callback, postData);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalSpare_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItemDetail.SparePartsName = res.MaterialName;
            self.currentItemDetail.SparePartsId = res.MaterialCode;
            self.currentItemDetail.SpecificationsModels = res.Spec;
        }

        //选择设备  使用公用方法
        function SelectPeopleModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'Code',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_9'),
                    width: 200
                },
                {
                    field: 'Name',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_10'),
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
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_11'), commonService.getMesApiAddress() + "Base/GetEmployeePageList", [{ 'FieldCode': 'queryCode', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_9'), 'FiledType': 'Text' }, { 'FieldCode': 'queryName', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_12'), 'FiledType': 'Text' }], "Code", "asc", columnDefs, Select_SingleChoiceModalPeople_callback);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalPeople_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItemDetail.CreatorName = res.Name;
            self.currentItemDetail.Creator = res.Code;
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {


            if (self.currentItemDetail.SparePartsId == "" || self.currentItemDetail.SparePartsId == null) {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_13'));
                return;
            }
            if (self.currentItemDetail.Num == "" || self.currentItemDetail.Num == null) {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_14'));
                return;
            }

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_15') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;

            self.currentItemDetail.FactoryCode = self.currentItem.FactoryCode;
            self.currentItemDetail.FactoryName = self.currentItem.FactoryName;
            self.currentItemDetail.RepairId = self.currentItem.Id;
            self.currentItem.UseType = "1";

            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItemDetail
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_16'));
                $rootScope.$emit('to-addChildItem', self.currentItemDetail);
                self.currentItemDetail.SparePartsId = "";
                self.currentItemDetail.SparePartsName = "";
                self.currentItemDetail.Num = null;
                self.currentItemDetail.SpecificationsModels = "";

                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                //$state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_17'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepairaddsparectrl.Tips_17'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddSpareScreenStateConfig.$inject = ['$stateProvider'];
    function AddSpareScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMalfunctionRepair_EP_EquipmentMalfunctionRepair';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentMalfunctionRepair';

        var state = {
            name: screenStateName + '.addspare',
            url: '/addspare',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EP_EquipmentMalfunctionRepair-addspare.html',
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
