(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGroup.service',
        '$state', '$stateParams', '$rootScope', 'common.base', '$filter', '$scope', 'common.widgets.notificationTile.globalService', 'commonService'];
    function AddScreenController(dataService, $state, $stateParams, $rootScope, common, $filter, $scope, notification, commonService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        var distribution = new Array();
        var undistribution = new Array();

        activate();
        function activate() {
            init();
            initGridOptions();

            initGridDataForUnDistribution();
            initGridDataForDistribution();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_1'));
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.selectedDetail = null;
            self.selectedItemDetail = null;
            self.arrObj = [];
            self.arrObj2 = [];
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = $stateParams.selectedItem;

            //Expose Model Methods
            self.save = save;
            self.disSave = disSave;
            self.cancel = cancel;
        }

        function initGridOptions() {
            //所有未分配的检验项目
            self.gridOptionsAll = {
                enableRowSelection: true,
                enableSelectAll: false,
                selectionRowHeaderWidth: 35,
                rowHeight: 35,
                multiSelect: true,// 是否可以选择多个,默认为true;
                paginationPageSizes: [20, 25, 30, 50, 75, 100], //每页显示个数选项
                paginationPageSize: 50, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                enableFullRowSelection: true,
                //multiSelect: false,
                ebablePagination: true,
                enableMultiSelection: true,
                enableFiltering: false,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'LabelName',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_2'),
                        enableSorting: true,
                        width: 120
                    }
                    , {
                        field: 'LabelColor',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_3'),
                        enableSorting: false,
                        width: 120,
                        cellTemplate:
                            '<div class="colorLabel" style=" background-color: {{row.entity.LabelColor}}"></div>'
                    }, {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_4'),
                        enableSorting: false,
                        width: 150
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi1 = gridApi;
                    // $scope.gridApi1.selection.on.rowSelectionChanged($scope, function (row, event) { 
                    //     debugger;
                    //     var dd = $scope.gridApi.selection.getSelectedRows(); 
                    //     if (row.isSelected) {
                    //         self.arrObj.push(self.selectedDetail);
                    //     }
                    //     else{
                    //         self.arrObj.pop(self.selectedDetail); 
                    //     }
                    // });
                },
                data: []
            }
            //已分配的检验项
            self.gridOptionsDistribution = {
                enableRowSelection: true,
                enableSelectAll: false,
                selectionRowHeaderWidth: 35,
                rowHeight: 35,
                multiSelect: true,// 是否可以选择多个,默认为true;
                paginationPageSizes: [20, 25, 30, 50, 75, 100], //每页显示个数选项
                paginationPageSize: 50, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                enableFullRowSelection: true,
                //multiSelect: false,
                ebablePagination: true,
                enableMultiSelection: true,
                enableFiltering: false,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'LabelName',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_2'),
                        enableSorting: true,
                        width: 120
                    }
                    , {
                        field: 'LabelColor',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_3'),
                        enableSorting: false,
                        width: 120,
                        cellTemplate:
                            '<div class="colorLabel" style=" background-color: {{row.entity.LabelColor}}"></div>'
                    }
                ],
                onRegisterApi: function (gridApi2) {
                    $scope.gridApi2 = gridApi2;
                    // $scope.gridApi2.selection.on.rowSelectionChanged($scope, function (row, event) { 
                    //     if (row.isSelected) {
                    //         self.arrObj2.push(self.selectedDetail);
                    //     }
                    //     else{
                    //         self.arrObj2.pop(self.selectedDetail); 
                    //     }
                    // });
                },
                data: []
            }
        }

        function initGridDataForUnDistribution() {
            var Pagination = {
                rows: self.gridOptionsAll.paginationPageSize,
                page: self.gridOptionsAll.paginationCurrentPage,
                sidx: 'LabelValue',
                sord: 'asc'
            };
            let queryParmeters = {
                pagination: Pagination,
                queryJson: {
                    GroupCode: self.currentItem.GroupCode
                }
            };

            var url = commonService.getMesApiAddress() + 'Sys_DataSegregate/Get_PageData_notGroup';
            //console.log("未选择的 queryParmeters------------------------------------" + JSON.stringify(queryParmeters));
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsAll.totalItems = res.data.resultData.records;
                    //数据            
                    self.gridOptionsAll.data = res.data.resultData.rows;

                    console.log(self.gridOptionsAll.data);
                } else {
                    self.gridOptionsAll.data = [];
                    notification.warning(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_5'));
                }
            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_6'));
            });
        }
        //已选择的
        function initGridDataForDistribution() {
            var Pagination = {
                rows: self.gridOptionsDistribution.paginationPageSize,
                page: self.gridOptionsDistribution.paginationCurrentPage,
                sidx: 'LabelCode',
                sord: 'asc'
            };
            let queryParmeters = {
                pagination: Pagination,
                queryJson: {
                    GroupCode: self.currentItem.GroupCode
                }
            };

            var url = commonService.getMesApiAddress() + 'SystemManage/Sys_DataSegregateGroupToLabel/Get_PageData';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsDistribution.totalItems = res.data.resultData.records;
                    //数据            
                    self.gridOptionsDistribution.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsDistribution.data = [];
                    notification.warning(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_7'));
                }
            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_6'));
            });
        }
        //保存指定数据
        function save() {
            let selectRows = $scope.gridApi1.selection.getSelectedRows();
            var dd = new Array();

            if (selectRows.length == 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_8'), commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_9'));
                return;
            }
            let params = {
                list: selectRows,
                GroupCode: self.currentItem.GroupCode
            };
            console.log("保存指定数据 params------------------------------------" + JSON.stringify(params));
            var url = commonService.getMesApiAddress() + 'SystemManage/Sys_DataSegregateGroupToLabel/Save_Data';
            var req = commonService.callWebApiPost(url, params).then(onSaveSuccess, donSaveError);
        }
        //删除指定数据
        function disSave() {
            let selectRows = $scope.gridApi2.selection.getSelectedRows();
            if (selectRows.length == 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_8'), commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_9'));
                return;
            }
            let params = {
                list: selectRows,
                GroupCode: self.currentItem.GroupCode
            };
           // console.log("删除指定数据 params------------------------------------" + JSON.stringify(params));
            var url = commonService.getMesApiAddress() + 'SystemManage/Sys_DataSegregateGroupToLabel/RemoveForm';
            var req = commonService.callWebApiPost(url, params).then(onSaveSuccess, donSaveError);
        }

        function cancel() {
            sidePanelManager.close();
            $rootScope.$emit('to-addparent', 'parent');
            $state.go('^', {}, { reload: false });
        }

        function onSaveSuccess(data) {
            initGridDataForUnDistribution();
            initGridDataForDistribution();
            notification.warning(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_10'));
        }
        function onDisSaveSuccess(data) {
            initGridDataForUnDistribution();
            initGridDataForDistribution();
            notification.warning(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_10'));
        }
        function donSaveError(error) {

            backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGrouplablectrl.Tips_6'));
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
            name: screenStateName + '.lable',
            url: '/lable',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/Sys_DataSegregateGroup-lable.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'lable'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
