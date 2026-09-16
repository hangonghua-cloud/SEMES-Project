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
    angular.module('Siemens.SimaticIT.QualityApp.QC_PollingDetail').config(EditAuthorScreenStateConfig);
    
    EditAuthorScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetail.service', '$state', '$stateParams',
      'common.base', '$filter', '$scope', '$rootScope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService', 
      'common.widgets.busyIndicator.service', '$uibModal'];
    function EditAuthorScreenController(dataService, $state, $stateParams, common, $filter, $scope, $rootScope, commonService, auth, notificationService, busyIndicatorService, $modal) {
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
            
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_1'));
            //sidePanelManager.open('e');//使用窄弹窗
            //使用宽右侧弹窗
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
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
            self.validInputs = false;
            //self.isQualityButtonVisible = false;
            
            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            //判定状态
            self.DeterminationConfig = {
                value: null,
                selectedOption: {ItemValue: self.currentItem.Determination, ItemName: self.currentItem.DeterminationName},
                options: []
            };

            //点检结论
            self.ResultConfig = {
                value: null,
                selectedOption: { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_2') },
                options: [
                    { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_2') },
                    { ItemCode: "1", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_3') },
                    { ItemCode: "2", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_4') }
                ]
            };
            /*self.ResultConfig2 = {
                value: null,
                selectedOption: {ItemCode: "", ItemName:commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_5')},
                options: [
                    {ItemCode: "", ItemName:commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_5')},
                    {ItemCode: "1", ItemName:commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_3')},
                    {ItemCode: "2", ItemName:commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_4')}
                ]
            };*/
            self.ResultConfig3 = {
                value: null,
                selectedOption: { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_2') },
                options: [
                    { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_6') },
                    { ItemCode: "1", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_7') },
                    { ItemCode: "2", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_8') }
                ]
            };
            self.ResultConfig4 = {
                value: null,
                selectedOption: { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_6') },
                options: [
                    { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_6') },
                    { ItemCode: "1", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_9') },
                    { ItemCode: "2", ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_10') }
                ]
            };

            GetDeterminationDictionary();
            initGridOptions();
            getDataItemDetailList();
        }
        
        //判定状态
        function GetDeterminationDictionary() {
            var url = commonService.getDataItemDuatil("ComprehensiveJudgement").then(function (res) {
                self.DeterminationConfig.options = res.data.resultData;;
            });
        }

        function initGridOptions() {
            self.gridOptions = {
                fastWatch: true,
                rowHeight: 35,
                minimumColumnSize: 100,
                enableMultiSelection: false,
                enableFiltering: false,
                //基础属性
                enableSorting: true,//是否支持排序(列)
                useExternalSorting: false,//是否支持自定义的排序规则      
                enableGridMenu: false,//是否显示表格 菜单
                showGridFooter: false,//时候显示表格的footer
                enableHorizontalScrollbar: 1,//表格的水平滚动条
                enableVerticalScrollbar: 1,//表格的垂直滚动条 (两个都是 1-显示,0-不显示)
                selectionRowHeaderWidth: 30,
                enableCellEditOnFocus: false,//default为false,true的时候单击即可打开编辑(cellEdit为true的时候,需要引入'ui.grid.cellNav')
                //分页属性
                enablePagination: false, //是否分页,default为true
                enablePaginationControls: false, //使用默认的底部分页
                paginationPageSizes: [100, 200, 300], //每页显示个数选项
                paginationPageSize: 100, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮          
                //选中
                rowTemplate: "<div ng-dblclick=\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中         
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'Id',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_11'),
                        width: 90,
                        visible: false
                    },
                    {
                        field: 'TestMaintenanceId',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_12'),
                        width: 90,
                        visible: false
                    },
                    {
                        field: 'TestItemCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_13'),
                        width: 80,
                        visible: false
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_14'),
                        width: 200
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_15'),
                        width: 200
                    },
                    {
                        field: 'DataType',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_16'),
                        width: 80,
                        visible: false
                    },
                    {
                        field: 'TestItemResultValue',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_17'),
                        width: 200,
                        //enableCellEdit: true, pinnedLeft: true,
                        /*cellTemplate: '<div class="ui-grid-cell-contents" ng-if="!row.entity.editrow">' +
                            '{{row.entity.TestItemResult}}</div>' +
                            '<div ng-show="row.entity.DataType==\'2\'">' +
                            '<sit-text sit-value="row.entity.TestItemResult"> ' +
                            '</sit-text>' +
                            '</div>' +
                            '<div ng-show="row.entity.DataType==\'1\'">' +
                            '<sit-numeric sit-value="row.entity.TestItemResult"> ' +
                            '</sit-numeric>' +
                            '</div>' +
                            '<div ng-show="row.entity.DataType==\'4\'">' +
                            '<sit-select sit-value="row.entity.TestItemResult.selectedOption"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.TestItemResult.options"' +
                            'sit-to-display="\'ItemName\'"' +
                            'sit-to-keep="\'ItemCode\'">' +
                            '</sit-select>' +
                            '</div>' +
                            '<div ng-show="row.entity.DataType==\'5\'">' +
                            '<sit-select sit-value="row.entity.TestItemResult.selectedOption"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.TestItemResult.options"' +
                            'sit-to-display="\'ItemName\'"' +
                            'sit-to-keep="\'ItemCode\'">' +
                            '</sit-select>' +
                            '</div>' +
                            '<div ng-show="row.entity.DataType==\'6\'">' +
                            '<sit-select sit-value="row.entity.TestItemResult.selectedOption"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.TestItemResult.options"' +
                            'sit-to-display="\'ItemName\'"' +
                            'sit-to-keep="\'ItemCode\'">' +
                            '</sit-select>' +
                            '</div>'*/
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row.isSelected) {
                            //self.isDelDisplay=true;
                            self.selectedDetail = row.entity;
                            /*if(deleteArr.indexOf(row.entity.Id) == -1){
                                deleteArr.push(row.entity.Id);
                            }*/
                        }
                        else {
                            //self.isDelDisplay=false;
                            //deleteArr.remove(row.entity.Id);
                        }
                    });
                },
                data: []
            }

        }

        function getDataItemDetailList() {
            var url = commonService.getMesApiAddress("quality") + 'QC_PollingDetailResult/GetCheckDataList';
            var postCode = {
                pagination: null,
                queryJson: {
                    Id: self.currentItem.Id,
                    TestMaintenanceId: self.currentItem.CalibrationMethod,
                    Type: "1"
                }
            };
            console.log(JSON.stringify(postCode));
            commonService.callWebApiPost(url, postCode).then(function (res) {
                if ((res) && (res.data.success)) {
                    var resultData = res.data.resultData;
                    var arrLeng = 0;
                    self.gridOptions.data = resultData;
                    /*for (var icount = 0; icount < resultData.length; icount++) {
                        if (resultData[icount].DataType == "4") {
                            self.ResultConfig.selectedOption={ItemCode:resultData[icount].TestItemResult,ItemName:resultData[icount].TestItemResultValue};
                            self.gridOptions.data.push({
                                Id: resultData[icount].Id,
                                TestMaintenanceId: resultData[icount].TestMaintenanceId,
                                TestItemCoading: resultData[icount].TestItemCoading,
                                TestItemName: resultData[icount].TestItemName,
                                TestItemStandard: resultData[icount].TestItemStandard,
                                DataType: resultData[icount].DataType,
                                //CheckResult: resultData[icount].CheckResult,
                                TestItemResult: angular.copy(self.ResultConfig),
                                editrow: true
                            });
                        }
                        else if (resultData[icount].DataType == "5") {
                            self.ResultConfig.selectedOption={ItemCode:resultData[icount].TestItemResult,ItemName:resultData[icount].TestItemResultValue};
                            self.gridOptions.data.push({
                                Id: resultData[icount].Id,
                                TestMaintenanceId: resultData[icount].TestMaintenanceId,
                                TestItemCoading: resultData[icount].TestItemCoading,
                                TestItemName: resultData[icount].TestItemName,
                                TestItemStandard: resultData[icount].TestItemStandard,
                                DataType: resultData[icount].DataType,
                                //CheckResult: resultData[icount].CheckResult,
                                TestItemResult: angular.copy(self.ResultConfig3),
                                editrow: true
                            });
                        }
                        else if (resultData[icount].DataType == "6") {
                            self.ResultConfig.selectedOption={ItemCode:resultData[icount].TestItemResult,ItemName:resultData[icount].TestItemResultValue};
                            self.gridOptions.data.push({
                                Id: resultData[icount].Id,
                                TestMaintenanceId: resultData[icount].TestMaintenanceId,
                                TestItemCoading: resultData[icount].TestItemCoading,
                                TestItemName: resultData[icount].TestItemName,
                                TestItemStandard: resultData[icount].TestItemStandard,
                                DataType: resultData[icount].DataType,
                                //CheckResult: resultData[icount].CheckResult,
                                TestItemResult: angular.copy(self.ResultConfig4),
                                editrow: true
                            });
                        }
                        else if(resultData[icount].DataType=="1"){
                            //self.ResultConfig.selectedOption={ItemCode:resultData[icount].CheckResult,ItemName:resultData[icount].CheckResultValue};
                            self.gridOptions.data.push({
                                Id: resultData[icount].Id,
                                TestMaintenanceId: resultData[icount].TestMaintenanceId,
                                TestItemCoading: resultData[icount].TestItemCoading,
                                TestItemName: resultData[icount].TestItemName,
                                TestItemStandard: resultData[icount].TestItemStandard,
                                DataType: resultData[icount].DataType,
                                //CheckResult: resultData[icount].CheckResult,
                                TestItemResult: parseFloat(resultData[icount].TestItemResult),
                                editrow: true
                            });
                        }
                        else {
                            self.gridOptions.data.push({
                                Id: resultData[icount].Id,
                                TestMaintenanceId: resultData[icount].TestMaintenanceId,
                                TestItemCoading: resultData[icount].TestItemCoading,
                                TestItemName: resultData[icount].TestItemName,
                                TestItemStandard: resultData[icount].TestItemStandard,
                                DataType: resultData[icount].DataType,
                                TestItemResult: resultData[icount].TestItemResult,
                                //CheckResultValue: angular.copy(self.ResultConfig),
                                editrow: true
                            });
                        }
                        arrLeng++;
                    }*/

                } else {
                    self.gridOptions.data = [];

                }
            }, function (error) {

            });
        }
        
        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        
        //编辑保存
        function save() {
            
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_18') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            
            
            console.log("postData------------------------------------" + JSON.stringify(self.currentItem));
            
            //获取登录用户信息 要存在此函数 GetUserInfo()  方法   
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                DeterminationValue: self.DeterminationConfig.selectedOption.ItemValue,
                Inspector: self.UserCode
            };
            
            console.log("postData------------------------------------" + JSON.stringify(postData));
            var url = commonService.getMesApiAddress("quality") + 'QC_PollingDetail/SaveDetermination';
            console.log("url----------------" + url);
            //提交数据
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            console.log("SaveQC_PollingDetail----------------------" + JSON.stringify(req));
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_19'));
                //刷新局部
                if(self.DeterminationConfig.selectedOption != null){
                    var _date = new Date();
                    var year=_date.getFullYear();
                    var month=_date.getMonth()+1;
                    if(month<10)month="0"+month;
                    var day=_date.getDate();
                    if(day<10)day="0"+day;
                    self.currentItem.Determination = self.DeterminationConfig.selectedOption.ItemValue;
                    self.currentItem.DeterminationName = self.DeterminationConfig.selectedOption.ItemName;
                    self.currentItem.LaboratoryTestStatusName = commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_20');
                    self.currentItem.LaboratoryTestStatus = "3";
                    self.currentItem.Inspector = self.UserCode;
                    self.currentItem.InspectorName = self.UserName;
                    self.currentItem.InspectionTime = year + "-" + month + "-" + day;
                    self.currentItem.TestMachine = self.currentItem.ListType;
                    self.currentItem.TestMachineName = self.currentItem.ListTypeName;
                }
                $rootScope.$emit('to-editItem', self.currentItem);
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_21'));
            }
        }
        
        //保存失败事件
        function onSaveError(error) {
                busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetaileditauthorctrl.Tips_21'));
        }
        
        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }
    
    EditAuthorScreenStateConfig.$inject = ['$stateProvider'];
    function EditAuthorScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_QC_PollingDetail_QC_PollingDetail';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_PollingDetail';
        
        var state = {
            name: screenStateName + '.editauthor',
            url: '/editauthor/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/QC_PollingDetail-editauthor.html',
                    controller: EditAuthorScreenController,
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
