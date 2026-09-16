/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： IQC品质检验记录表
*  2. 创建人员： 丁零
*  3. 创建日期： 2021-08-27
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList').config(EditScreenStateConfig);
    
    EditScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckList.service', '$state', '$stateParams',
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
            
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_1'));
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
            self.currentItem.ArriveNoticeQuantities = parseFloat(self.currentItem.ArriveNoticeQuantities);
            self.validInputs = false;
            
            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            self.selectClass = selectClass;
            self.SelectMaterialModal = SelectMaterialModal;
            self.SelectReceivingModal = SelectReceivingModal;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            //物料小类
            self.MaterialClassConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            //原材料单据类型
            self.MaterialCheckClassConfig = {
                value: null,
                selectedOption: null,
                options: []
            };
            //检测方法
            self.CheckMethoddConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            GetMaterialCheckClassDictionary();
            GetMaterialClassDictionary();
        }

        //原材料单据类型
        function GetMaterialCheckClassDictionary() {
            var url = commonService.getDataItemDuatil("MaterialCheckClass").then(function (res) {
                self.MaterialCheckClassConfig.options = res.data.resultData;;
                self.MaterialCheckClassConfig.selectedOption = {ItemValue: self.currentItem.ListType, ItemName: self.currentItem.ListTypeName};
            });
        }

        //物料小类
        function GetMaterialClassDictionary() {
            var url = commonService.getMesApiAddress("quality") + "QC_TestMethodMaintenance/GetMaterialClass";
            //console.log(JSON.stringify(postData));
            commonService.callWebApiGet(url).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    self.MaterialClassConfig.options = jsonData;
                } else {
                    console.log(self.MaterialClassConfig.options);
                }
                //self.SecondValue = jsonData[0];
                self.MaterialClassConfig.options.splice(0, 0, {ItemValue:"",ItemName:commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_2') });
                self.MaterialClassConfig.selectedOption = {ItemValue:self.currentItem.MaterialClass,ItemName: self.currentItem.MaterialClassName };
            }, function (error) {
                // console.log('-----------error------------');
                console.log(error);
            });
        }

        function selectClass(oldValue, newValue) {
            var postData={
                "pagination":null,
                "queryJson":{"queyCode": "","queyName": "","GroupId":newValue.ItemValue}
            };
            var url = commonService.getMesApiAddress("quality") + "QC_TestMethodMaintenance/GetListForControl";
            console.log(JSON.stringify(postData));
            commonService.callWebApiPost(url, postData).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData.rows;
                    self.CheckMethoddConfig.options = jsonData;
                } else {
                    console.log(self.CheckMethoddConfig.options);
                }
                //self.SecondValue = jsonData[0];
                self.CheckMethoddConfig.options.splice(0, 0, {TestMethodCoding:"",TestMethodName:commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_2') });
                self.CheckMethoddConfig.selectedOption = {Id:self.currentItem.CheckMethodId,TestMethodName:self.currentItem.CheckMethodName };
            }, function (error) {
                // console.log('-----------error------------');
                console.log(error);
            });
        }

        //选择设备  使用公用方法
        function SelectMaterialModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'MaterialCode',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_4'),
                    width: 200
                },
                {
                    field: 'MaterialName',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_5'),
                    width: 200
                },
                {
                    field: 'Spec',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_6'),
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
           /*if(self.CodadConfig.selectedOption == null){
                notificationService.warning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_7'));
                return;
            }*/   
            var query={
                //Factory: "3001" //self.CodadConfig.selectedOption.ResourceCode
            }       
            commonService.Select_SingleChoiceModal(commonService.getMesApiAddress("equipment") + "EP_EquipmentMalfunctionRepair/GetMaterialPageListWithClassJson", "MaterialCode", "asc", columnDefs, Select_SingleChoiceModalMethod_callback, query); 
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalMethod_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItem.MaterialCode = res.MaterialCode;
            self.currentItem.MaterialName = res.MaterialName;
        }

        //选择设备  使用公用方法
        function SelectReceivingModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'ReceiptCode',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_8'),
                    width: 200
                },
                {
                    field: 'MaterialCode',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_4'),
                    width: 200
                },
                {
                    field: 'MaterialName',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_5'),
                    width: 200
                },
                {
                    field: 'Spec',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_6'),
                    width: 200
                },
                {
                    field: 'SupplierName',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_9'),
                    width: 200
                },
                {
                    field: 'UnitName',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_10'),
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
           /*if(self.CodadConfig.selectedOption == null){
                notificationService.warning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_7'));
                return;
            }*/   
            var query={
                //Factory: "3001" //self.CodadConfig.selectedOption.ResourceCode
            }       
            commonService.Select_SingleChoiceModal(commonService.getMesApiAddress("quality") + "QC_IQCQualityCheckList/GetMaterialReceiptPageList", "MaterialCode", "asc", columnDefs, Select_SingleChoiceModalMethod_callback, query); 
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalMethod_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItem.ReceivingNotificationLineNumber = res.ReceiptCode;
            self.currentItem.MaterialCode = res.MaterialCode;
            self.currentItem.MaterialName = res.MaterialName;
        }
        
        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        
        //编辑保存
        function save() {
            if(self.MaterialClassConfig.selectedOption == null){
                notificationService.warning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_11'));
                return;
            }
            else{
                self.currentItem.MaterialClass = self.MaterialClassConfig.selectedOption.ItemValue;
            }
            if(self.CheckMethoddConfig.selectedOption == null){
                notificationService.warning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_12'));
                return;
            }
            else{
                self.currentItem.CheckMethodId = self.CheckMethoddConfig.selectedOption.Id;
            }
            if(self.MaterialCheckClassConfig.selectedOption == null){
                notificationService.warning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_13'));
                return;
            }
            else{
                self.currentItem.ListType = self.MaterialCheckClassConfig.selectedOption.ItemValue;
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_14') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            
            
            console.log("postData------------------------------------" + JSON.stringify(self.currentItem));
            
            if(self.currentItem.ListType == "1"){
                self.currentItem.Determination = "3";
                self.currentItem.DeterminationName = commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_15');
                self.currentItem.LaboratoryTestingStatus = "1";
                self.currentItem.LaboratoryTestingStatusName = commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_16');
                self.currentItem.QualityTestingStatus = "1";
                self.currentItem.QualityTestingStatusName = commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_17');
            }
            else{
                self.currentItem.Determination = "3";
                self.currentItem.DeterminationName = commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_15');
                self.currentItem.LaboratoryTestingStatus = "2";
                self.currentItem.LaboratoryTestingStatusName = commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_17');
                self.currentItem.QualityTestingStatus = "1";
                self.currentItem.QualityTestingStatusName = commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_17');
            }
            
            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItem.ModifyBy = self.UserCode;
            console.log("username------------------------------------" + self.UserName);
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            
            console.log("postData------------------------------------" + JSON.stringify(postData));
            // commonService.getMesApiAddress() = '/sitSrvApi/'
            var url = commonService.getMesApiAddress("quality") + 'QC_IQCQualityCheckList/SaveQC_IQCQualityCheckList';
            console.log("url----------------" + url);
            //提交数据
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            console.log("SaveQC_IQCQualityCheckList----------------------" + JSON.stringify(req));
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_18'));
                //刷新局部
                $rootScope.$emit('to-editItem', self.currentItem);
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_19'));
            }
        }
        
        //保存失败事件
        function onSaveError(error) {
                busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListeditctrl.Tips_19'));
        }
        
        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }
    
    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_QC_IQCQualityCheckList_QC_IQCQualityCheckList';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_IQCQualityCheckList';
        
        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/QC_IQCQualityCheckList-edit.html',
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
