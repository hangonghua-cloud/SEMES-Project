(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGroup.service',
    '$state', '$stateParams', 'common.base', '$filter', '$scope', 'common.widgets.notificationTile.globalService', 'commonService'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, notification, commonService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        var distribution = new Array();
        var undistribution = new Array();

        activate();
        function activate() {
            init();

            LoadDept();
            initGridOptions();

            initGridDataForUnDistribution();
            initGridDataForDistribution();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouppersonctrl.Tips_1'));
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            //self.currentItem = angular.copy($stateParams.selectedItem);
            self.selectedItemUnDistribution = null;
            self.selectedItemDistribution = null;

            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = $stateParams.selectedItem;
            self.QueryPersonCode=null
            self.DeptCtrl={
                options:{},
                value:null,
                selectedOptions:{}
            };

            self.searchButtonHandler = initGridDataForUnDistribution;

            //Expose Model Methods
            self.save = save;
            self.disSave = disSave;
            self.cancel = cancel;
        }

        function initGridOptions() {
            //所有未分配的检验项目
            self.gridOptionsAll = {
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
                paginationPageSizes: [20, 25, 30, 50, 75, 100], //每页显示个数选项
                paginationPageSize: 25, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮          
                //选中
                // rowTemplate: "<div ng-dblclick=\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
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
                        field: 'PersonCode',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouppersonctrl.Tips_2'),
                        width: 180
                    }, {
                        field: 'PersonName',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouppersonctrl.Tips_3'),
                        width: 180
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataForUnDistribution();
                    });
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row.isSelected) {                            
                            self.selectedItemUnDistribution = row.entity;
                            if(distribution.indexOf(row.entity.PersonCode) == -1) {
                                distribution.push(row.entity.PersonCode);
                            }
                            //console.log(self.selectedItemUnDistribution);
                        }else{
                            //self.isButtonVisible=false;
                            distribution.remove(row.entity.PersonCode);
                        }
                    });
                },
                data: []
            }
            //已分配的检验项
            self.gridOptionsDistribution = {
                enableRowSelection: true,
                enableSelectAll: false,
                selectionRowHeaderWidth: 35,
                rowHeight: 35,
                multiSelect:true,
                paginationPageSizes: [20, 25, 30, 50, 75, 100], //每页显示个数选项
                paginationPageSize: 25, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                enableFullRowSelection: true,
                //multiSelect: false,
                ebablePagination: true,
                enableMultiSelection: false,
                enableFiltering: false,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'Id',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouppersonctrl.Tips_4'),
                        visible: false,
                        width: 180
                    }, {
                        field: 'PersonCode',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouppersonctrl.Tips_2'),
                        width: 180
                    }, {
                        field: 'PersonName',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouppersonctrl.Tips_3'),
                        width: 180
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row.isSelected) {                            
                            self.selectedItemDistribution = row.entity;
                            if(undistribution.indexOf(row.entity.Id) == -1) {
                                undistribution.push(row.entity.Id);
                            }
                            console.log(self.selectedItemDistribution); 
                        }else{
                            //self.isButtonVisible=false;
                            undistribution.remove(row.entity.PersonCode);
                        }
                    });
                },
                data: []
            }
        }

        function initGridDataForUnDistribution() {
            self.Pagination = {
                rows: self.gridOptionsAll.paginationPageSize,
                page: self.gridOptionsAll.paginationCurrentPage,
                sidx: 'PersonCode',
                sord: 'asc'
            };
            var deptId = "";
            if(self.DeptCtrl.value!=null){
                deptId = self.DeptCtrl.value.ID; 
            }
           
           
            var param = {
                pagination: self.Pagination,
                queryJson: {
                    GroupCode: self.currentItem.GroupCode,
                    DeptId: deptId,//self.searchParams.DeptId.ID
                    PersonCode: self.QueryPersonCode
                }
            };

            var url = commonService.getMesApiAddress() + 'SystemManage/Sys_DataSegregateGroupToPerson/GetPersonAllListJson';
            console.log("-self.initGridDataForUnDistributioninitGridDataForUnDistribution----------------------------" + JSON.stringify(param));
            commonService.callWebApiPost(url, param).then(function (res) {
                if ((res) && (res.data.success)) {
                    //console.log(res.data.resultData.rows);
                    var resultData = res.data.resultData.rows;
                    self.gridOptionsAll.data = resultData;
                    self.gridOptionsAll.totalItems = res.data.resultData.records;
                    $scope.totalPage = res.data.resultData.total;
                    //console.log(self.gridOptions.data);
                } else {
                    //self.gridOptions.data = [];
                }

            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouppersonctrl.Tips_5'));
            });
        }

        function initGridDataForDistribution() {
            self.Pagination = {
                rows: self.gridOptionsDistribution.paginationPageSize,
                page: self.gridOptionsDistribution.paginationCurrentPage,
                sidx: 'PersonCode',
                sord: 'asc'
            };
            var param = {
                pagination: self.Pagination,
                groupCode: self.currentItem.GroupCode
            };

            //self.queryParmeters.TheYear = self.YearParams.selectedOption.Id;
            var url = commonService.getMesApiAddress() + 'SystemManage/Sys_DataSegregateGroupToPerson/GetPersonSelectListJson';
            console.log("url----------------" + url);
            //console.log("-self.queryParmeters----------------------------" + JSON.stringify(self.queryParmeters));
            //console.log("-self.searchParam----------------------------" + JSON.stringify(params));
            commonService.callWebApiPost(url, param).then(function (res) {
                if ((res) && (res.data.success)) {
                    console.log(res.data.resultData);
                    var resultData = res.data.resultData;
                    self.gridOptionsDistribution.data = resultData.rows;
                    console.log(self.gridOptionsDistribution.data);
                } else {
                    self.gridOptionsDistribution.data = [];
                    notification.warning(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouppersonctrl.Tips_6'));
                }
            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouppersonctrl.Tips_5'));
            });
        }

        function LoadDept() {
            var url = commonService.getMesApiAddress() + "Base/GetList_Department_Control";

            commonService.callWebApiGet(url, null).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    self.DeptCtrl = jsonData;
                } else {
                    var jsonData = [{
                        ID: "None",
                        ItemName: ""
                    }];
                    self.ProcessCtrl = jsonData;
                    //console.log(self.ParentResourceCtrl.options);
                }
                //self.SecondValue = jsonData[0];
            }, function (error) {
                // console.log('-----------error------------');
                // console.log(error);
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouppersonctrl.Tips_7'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouppersonctrl.Tips_7',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });
        }

        function save() {
            //dataService.update(self.currentItem).then(onSaveSuccess, backendService.backendError);
            var param = {
                GroupCode: self.currentItem.GroupCode,
                PersonCodes: distribution.toString()
            };

            console.log("postData------------------------------------" + JSON.stringify(param));
            //console.log("postData------------------------------------" + JSON.stringify(postData));
            var url = commonService.getMesApiAddress() + 'SystemManage/Sys_DataSegregateGroupToPerson/SaveForm';
            console.log("----------------" + url);
            distribution.length = 0;
            var req = commonService.callWebApiPost(url, param).then(onSaveSuccess, backendService.backendError);
        }

        function disSave() {
            //dataService.update(self.currentItem).then(onSaveSuccess, backendService.backendError);
            //var materialCheckId = self.currentItem.Id;
            var param = {
                KeyValues: undistribution.toString()
            };
            console.log("postData------------------------------------" + JSON.stringify(param));
            //console.log("postData------------------------------------" + JSON.stringify(postData));
            var url = commonService.getMesApiAddress() + 'SystemManage/Sys_DataSegregateGroupToPerson/DeleteForm';
            console.log("----------------" + url);
            undistribution.length = 0;
            var req = commonService.callWebApiPost(url, param).then(onDisSaveSuccess, backendService.backendError);
        }

        Array.prototype.indexOf = function(val) { 
            for (var i = 0; i < this.length; i++) { 
                if (this[i] == val) return i; 
            } 
            return -1; 
        };
        Array.prototype.remove = function(val) { 
            var index = this.indexOf(val); 
            if (index > -1) { 
                this.splice(index, 1); 
            } 
        }; 

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            /*sidePanelManager.close();
            $state.go('^', {}, { reload: true });*/
            initGridDataForUnDistribution();
            initGridDataForDistribution();
            notification.warning(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouppersonctrl.Tips_8'));
        }
        function onDisSaveSuccess(data){
            initGridDataForUnDistribution();
            initGridDataForDistribution();
            notification.warning(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouppersonctrl.Tips_9'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_Sys_DataSegregateGroup_Sys_DataSegregateGroup';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/Sys_DataSegregateGroup';

        var state = {
            name: screenStateName + '.person',
            url: '/person',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/Sys_DataSegregateGroup-person.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'person'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
