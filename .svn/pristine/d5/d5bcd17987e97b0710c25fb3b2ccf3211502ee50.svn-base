/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 巡检检验记录表
*  2. 创建人员： 丁零
*  3. 创建日期： 2021-08-23
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.QC_IPQCDetail').config(AddScreenStateConfig);
    
    AddScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetail.service', '$state', '$stateParams',
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
            //self.currentItem.FlowCardId = commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_1');//如果新增有系统自动生成编号 可以在这写一个初始值, 这个控件要只读状态,后台代码生成编号+流水号
            
            //获取登录用户信息
            GetUserInfo();
            
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_2'));
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
            //self.SelectResourceModal = SelectResourceModal;
            self.SelectPeopleModal = SelectPeopleModal;
            //self.SelectMethodModal = SelectMethodModal;
            self.GetMachine = GetMachine;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            //所属工序
            self.ProcessConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            //
            self.TestMachineConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            //方法
            self.MethodConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            GetProcess();
        }

        function GetProcess() {
            var postData={
                "queryJson":{},
                "pagination":null
            };
            var url = commonService.getMesApiAddress("quality") + "QC_IPQCDetail/GetProcessList";//+self.currentItem.CalibrationMethod;
            //console.log(JSON.stringify(postData));
            commonService.callWebApiPost(url, postData).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData.rows;
                    self.ProcessConfig.options = jsonData;
                    //self.ProcessConfig.selectedOption = {ResourceCode:self.currentItem.TestProcess,ResourceName:self.currentItem.TestProcessName};
                } else {
                    console.log(self.ProcessConfig.options);
                }
                //self.SecondValue = jsonData[0];
                self.ProcessConfig.options.splice(0, 0, {ResourceCode:"",ResourceName:commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_3') })
            }, function (error) {
                // console.log('-----------error------------');
                console.log(error);
            });
        }

        function GetMachine(oldValue, newValue) {
            var postData={
                "pagination":null,
                "queryJson":{"Factory": "3001","ParentCode":newValue.ResourceCode}
            };
            var url = commonService.getMesApiAddress("equipment") + "EP_EquipmentTool/GetLinePageListJson";
            console.log(JSON.stringify(postData));
            commonService.callWebApiPost(url, postData).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData.rows;
                    self.TestMachineConfig.options = jsonData;
                    //self.TestMachineConfig.selectedOption = {LineCode: self.currentItem.ProductionMachine, LineName: self.currentItem.ProductionMachineName};
                } else {
                    console.log(self.TestMachineConfig.options);
                }
                //self.SecondValue = jsonData[0];
                self.TestMachineConfig.options.splice(0, 0, {LineCode:"",LineName:commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_3') });
            }, function (error) {
                // console.log('-----------error------------');
                console.log(error);
            });
            postData={
                "pagination":null,
                "queryJson":{"ProcessCode":newValue.ResourceCode}
            };
            url = commonService.getMesApiAddress("quality") + "QC_IPQCDetail/GetMaintenanceList";
            commonService.callWebApiPost(url, postData).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData.rows;
                    self.MethodConfig.options = jsonData;
                    //self.MethodConfig.selectedOption = {TestMethodCoading: self.currentItem.CalibrationMethod, TestMethodName: self.currentItem.CalibrationMethodName};
                } else {
                    console.log(self.MethodConfig.options);
                }
                //self.SecondValue = jsonData[0];
                self.MethodConfig.options.splice(0, 0, {TestMethodCoading:"",TestMethodName:commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_3') });
            }, function (error) {
                // console.log('-----------error------------');
                console.log(error);
            });
        }

        //选择设备  使用公用方法
        function SelectResourceModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_4'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'LineCode',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_5'),
                    width: 200
                },
                {
                    field: 'LineName',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_6'),
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
                notificationService.warning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_7'));
                return;
            }*/   
           var query={
            Factory: "3001" //self.CodadConfig.selectedOption.ResourceCode
           }       
            commonService.Select_SingleChoiceModal(commonService.getMesApiAddress("equipment") + "EP_EquipmentTool/GetLinePageListJson", "LineCode", "asc", columnDefs, Select_SingleChoiceModalLine_callback, query); 
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalLine_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItem.TestMachineName = res.LineName;
            self.currentItem.TestMachine = res.LineCode;
        }

        //选择设备  使用公用方法
        function SelectMethodModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_4'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'TestMethodCoading',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_8'),
                    width: 200
                },
                {
                    field: 'TestMethodName',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_9'),
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
                notificationService.warning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_7'));
                return;
            }*/   
           var query={
            Factory: "3001" //self.CodadConfig.selectedOption.ResourceCode
           }       
            commonService.Select_SingleChoiceModal(commonService.getMesApiAddress("quality") + "QC_IPQCDetail/GetCalibrationMethodPageList", "TestMethodCoading", "asc", columnDefs, Select_SingleChoiceModalMethod_callback, query); 
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalMethod_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItem.CalibrationMethodName = res.TestMethodName;
            self.currentItem.CalibrationMethod = res.TestMethodCoading;
            GetProcess();
        }

        //选择设备  使用公用方法
        function SelectPeopleModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_4'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'Code',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_10'),
                    width: 200
                },
                {
                    field: 'Name',
                    displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_11'),
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
            commonService.Select_SingleChoiceModal(commonService.getMesApiAddress() + "Base/GetEmployeePageList", "Code", "asc", columnDefs, Select_SingleChoiceModalPeople_callback);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalPeople_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItem.InspectorName = res.Name;
            self.currentItem.Inspector = res.Code;
        }
        
        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        
        //保存
        function save() {
            if(self.MethodConfig.selectedOption == null){
                notificationService.warning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_12'));
                return;
            }
            else{
                self.currentItem.CalibrationMethod = self.MethodConfig.selectedOption.TestMethodCoading;
            }
            if(self.ProcessConfig.selectedOption == null){
                notificationService.warning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_13'));
                return;
            }
            else{
                self.currentItem.ProductionWorkshop = self.ProcessConfig.selectedOption.ResourceCode;
            }
            if(self.TestMachineConfig.selectedOption == null){
                notificationService.warning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_14'));
                return;
            }
            else{
                self.currentItem.TestMachine = self.TestMachineConfig.selectedOption.LineCode;
            }
            /*if(self.currentItem.InspectionTimeStr != null){
                self.currentItem.InspectionTime = moment(self.currentItem.InspectionTimeStr).format("YYYY-MM-DD HH:mm:ss");
            }*/
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_15') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            
            
            console.log("postData------------------------------------" + JSON.stringify(self.currentItem));
            
            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItem.Creator = self.UserCode;
            /*self.currentItem.CreatedByCode = self.UserCode;
            if(self.UserName == null || self.UserName == '' || self.UserName == undefined){
                self.currentItem.CreatedByName = self.UserCode;
            }*/

            console.log("username------------------------------------" + self.UserName);
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            
            console.log("postData------------------------------------" + JSON.stringify(postData));
            // commonService.getMesApiAddress() = '/sitSrvApi/'
            var url = commonService.getMesApiAddress("quality") + 'QC_IPQCDetail/SaveQC_IPQCDetail';
            console.log("url----------------" + url);
            //提交数据
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            console.log("SaveQC_IPQCDetail----------------------" + JSON.stringify(req));
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_16'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_17'));
            }
        }
        
        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetailaddctrl.Tips_17'));
        }
        
        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }
    
    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_QC_IPQCDetail_QC_IPQCDetail';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_IPQCDetail';
        
        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/QC_IPQCDetail-add.html',
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
