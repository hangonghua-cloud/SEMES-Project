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
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair').config(EditSpareScreenStateConfig);
    
    EditSpareScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepair.service', '$state', '$stateParams',
      'common.base', '$filter', '$scope', '$rootScope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService', 
      'common.widgets.busyIndicator.service', '$uibModal'];
    function EditSpareScreenController(dataService, $state, $stateParams, common, $filter, $scope, $rootScope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        
        activate();
        function activate() {
            //传参实体定义赋值
            //self.currentItem = angular.copy($stateParams.selectedItem);
            
            
            //初始化
            init();
            //注册事件
            registerEvents();
            
            //获取登录用户信息
            GetUserInfo();
            
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_1'));
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
            self.currentItemDetail = angular.copy($stateParams.selectedItemDetail);
            self.validInputs = false;
            
            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            GetTypeDictionary();
        }

        //类型
        function GetTypeDictionary() {
            var url = commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                self.TypeConfig.options = res.data.resultData;;
            });
        }

        //选择设备  使用公用方法
        function SelectEquipmentModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_2'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'EquipmentId',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_3'),
                    width: 200
                },
                {
                    field: 'EquipmentName',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_4'),
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
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_3'),commonService.getMesApiAddress("equipment") + "EquipmentManage/GetPage_Equipment", [{ 'FieldCode': 'EquipmentId', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_3'), 'FiledType': 'Text' }, { 'FieldCode': 'EquipmentName', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_4'), 'FiledType': 'Text' }], "EquipmentId", "asc", columnDefs, Select_SingleChoiceModalEquipment_callback);
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
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_2'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'Code',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_5'),
                    width: 200
                },
                {
                    field: 'Name',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_6'),
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
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_7'),commonService.getMesApiAddress() + "Base/GetEmployeePageList", [{ 'FieldCode': 'queryCode', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_5'), 'FiledType': 'Text' }, { 'FieldCode': 'queryName', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_8'), 'FiledType': 'Text' }],"Code", "asc", columnDefs, Select_SingleChoiceModalPeople_callback);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalPeople_callback(res) {
            //alert(JSON.stringify(res));
            self.currentItem.RepairingPersonName = res.Name;
            self.currentItem.RepairingPerson = res.Code;
        }
        
        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        
        //初始化grid选项
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
                enablePagination: true, //是否分页,default为true
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [20, 30, 50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 20, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
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
                            name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                                '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                        },
                        {
                            field: 'SparePartsId', 
                            displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_9'),
                            width: 200
                        },
                        {
                            field: 'SparePartsName', 
                            displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_10'),
                            width: 200
                        },
                        {
                            field: 'SpecificationsModels', 
                            displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_11'),
                            width: 200
                        },
                        {
                            field: 'UnitName', 
                            displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_12'),
                            width: 200
                        },
                        {
                            field: 'Num', 
                            displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_13'),
                            width: 200
                        },
                        {
                            field: 'PersonOfReplaceName', 
                            displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_14'),
                            width: 200
                        }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData();
                    });
                    //行选中事件
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        /*if (row) {
                            if (row.isSelected) {
                                self.selectedItem = row.entity;
                                self.isButtonVisible1 = true;
                                initGridDataDetail();
                                //console.log (self.selectedItem);
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible1 = false;
                                self.gridOptionsItem.data = {};
                            }
                        }*/
                    });
                },
                data: []
            }
        }
        
        //查询方法,数据绑定
        function initGridData() {
            self.selectedItem = null;
            self.isButtonVisible = false;
            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'DateOfReplace',//设备编码
                sord: 'desc'
            };
            
            let queryParmeters = {
                pagination: Pagination,
                queryJson: {
                    "ParentId": self.currentItemDetail.ParentId
                }
            };
            console.log('queryParmeters-----' + JSON.stringify(queryParmeters));
            var url = commonService.getMesApiAddress('equipment') + 'EP_EquipmentSpareParts/EP_EquipmentSparePartsPageDataTableList';
            //var url = 'http://localhost:49849/' + 'EP_EquipmentMalfunctionRepair' + '/EP_EquipmentMalfunctionRepairPageDataTableList'; 
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                //console.log ('-self.Post_ResultData----------------------' + JSON.stringify(res));
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_15'));
            });
        }
        //编辑保存
        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_16') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            
            
            console.log("postData------------------------------------" + JSON.stringify(self.currentItem));
            
            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItemDetail.ModifyBy = self.UserCode;
            self.currentItemDetail.DateOfReplace = self.currentItem.FinishTime;
            self.currentItemDetail.PersonOfReplace = self.currentItem.RepairingPerson2;
            console.log("username------------------------------------" + self.UserName);
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItemDetail.Id,// ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItemDetail//self.currentItem
            };
            
            console.log("postData------------------------------------" + JSON.stringify(postData));
            // commonService.getMesApiAddress() = '/sitSrvApi/'
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentSpareParts/SaveEP_EquipmentSpareParts';
            console.log("url----------------" + url);
            //提交数据
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            console.log("SaveEP_EquipmentMalfunctionRepair----------------------" + JSON.stringify(req));
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_17'));
                //刷新局部
                $rootScope.$emit('to-editChildItem', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_18'));
            }
        }
        
        //保存失败事件
        function onSaveError(error) {
                busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMalfunctionRepair.EP_EquipmentMalfunctionRepaireditsparectrl.Tips_18'));
        }
        
        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }
    
    EditSpareScreenStateConfig.$inject = ['$stateProvider'];
    function EditSpareScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMalfunctionRepair_EP_EquipmentMalfunctionRepair';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentMalfunctionRepair';
        
        var state = {
            name: screenStateName + '.editspare',
            url: '/editspare/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EP_EquipmentMalfunctionRepair-editspare.html',
                    controller: EditSpareScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Edit'
            },
            params: {
                selectedItem: null,
                selectedItemDetail: null
            }
        };
        $stateProvider.state(state);
    }
}());
