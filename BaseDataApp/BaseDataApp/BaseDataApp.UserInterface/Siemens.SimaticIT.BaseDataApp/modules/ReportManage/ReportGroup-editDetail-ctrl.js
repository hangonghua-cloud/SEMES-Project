(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BaseDataApp.ReportManage').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroup.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope', '$interval'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope,
        commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope, $interval) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions1();
            initGridData1();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupeditDetailctrl.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.searchParams = {
                GroupId: self.currentItem.Id
            }
            //Expose Model Methods
            self.search = search;
            self.save = save;
            self.cancel = cancel;
        }

        function initGridOptions1() {
            self.gridOptionsItem1 = {
                //分页属性
                enablePagination: true, //是否分页,default为true
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
                paginationPageSize: 100, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                rowHeight: 35,
                multiSelect: true,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: true,
                enableRowSelection: true,
                enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'UserCode',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupeditDetailctrl.Tips_2'),
                        width: 150
                    }
                    , {
                        field: 'Name',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupeditDetailctrl.Tips_3'),
                        width: 150
                    }, {
                        field: 'Sex',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupeditDetailctrl.Tips_4'),
                        width: 100
                    }, {
                        field: 'DepartName',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupeditDetailctrl.Tips_5'),
                        width: 300
                    },
                    {
                        field: 'PositionName',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupeditDetailctrl.Tips_6'),
                        width: 200
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData1();
                    });
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem1 = row.entity;
                            //setButtonsVisibility(true);
                            //GetWorkOrderBomUnitConsome();
                            self.IsShowButten = true;
                        } else {
                            self.selectedItem1 = null;
                            self.IsShowButten = false;
                            //setButtonsVisibility(false);
                        }
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }

        function initGridData1() {
            let pagination = {
                rows: self.gridOptionsItem1.paginationPageSize,
                page: self.gridOptionsItem1.paginationCurrentPage,
                sidx: 'UserCode',//执行工单号
                sord: 'desc'
            };
            var postData = {
                pagination: pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress() + "BaseReportRole/GetUserNotInGroup";
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem1.data = res.data.resultData.rows;
                    self.gridOptionsItem1.totalItems = res.data.resultData.records;
                } else {
                    self.gridOptionsItem1.data = []
                }
            })
        }
        function search() {
            initGridData1();
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {

            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;

            var data = $scope.gridApi.selection.getSelectedRows();
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupeditDetailctrl.Tips_7'), commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupeditDetailctrl.Tips_8'));
                return;
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupeditDetailctrl.Tips_9') });
            var postData = {
                KeyValue: self.currentItem.Id,
                data: data
            };
            var url = commonService.getMesApiAddress() + 'BaseReportRole/SaveBatchRoleUser';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

        }

        //取消
        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupeditDetailctrl.Tips_10'));
                //刷新局部
                $rootScope.$emit('to-ParentDetail', 'ParentDetail');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupeditDetailctrl.Tips_8'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupeditDetailctrl.Tips_8'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BaseDataApp_ReportManage_ReportGroup';
        var moduleFolder = 'Siemens.SimaticIT.BaseDataApp/modules/ReportManage';

        var state = {
            name: screenStateName + '.editDetail',
            url: '/editDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ReportGroup-editDetail.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupeditDetailctrl.Tips_11'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
