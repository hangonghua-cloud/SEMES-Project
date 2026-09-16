(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.APPRole').config(ViewScreenStateConfig);

    ViewScreenController.$inject = ['Siemens.SimaticIT.SystemApp.APPRole.APPRole.service', '$state', '$stateParams',
        'commonService', 'common.base', '$filter', '$scope', '$timeout', 'common.widgets.notificationTile.globalService'];
    function ViewScreenController(dataService, $state, $stateParams, commonService, common, $filter, $scope, $timeout,notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            initGridData();
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleselectctrl.Tips_1'));
            sidePanelManager.open(
                {
                    mode: 'e',
                    size: 'wide'
                }
            );
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);

            //Expose Model Methods
            self.cancel = cancel;
            self.save = save;

        }
        self.gridOptions = {

            enablePagination: false,
            enablePaginationControls: false,   //是否显示分页
            useExternalPagination: false,//true:使用外部分页方式；false:使用UI Grid内部分页方式
            useExternalSorting: false,//true:使用外部排序方式，false:使用UI Gird内部排序方式
            multiSelect: true,
            enableRowSelection: false,
            enableRowHeaderSelection: true,
            enableColumnResizing: true,//允许调整列宽
            appScopeProvider: self,
            enableFiltering: false,
            appScopeProvider: self,
            columnDefs: [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleselectctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                }
                , {
                    field: 'ModuleName',
                    displayName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleselectctrl.Tips_3'),
                    width: 200
                }
                , {
                    field: 'FunctionCode',
                    displayName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleselectctrl.Tips_4'),
                    width: 200
                }
                , {
                    field: 'FunctionName',
                    displayName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleselectctrl.Tips_5'),
                    width: 200
                }
            ],
            //---------------api---------------------
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                //行选中事件
                //$scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                //    if (row) {
                //        if (row.isSelected) {
                //            self.selectedItemDetail = row.entity;
                //            console.log(self.selectedItemDetail);
                //            self.isButtonVisible = true;

                //            $scope.gridApi.selection.toggleRowSelection($scope.gridOptions.data[2]);
                //        } else {
                //            self.selectedItemDetail = null;

                //        }

                //    }
                //});

            },
            data: []
        }

        function save() {
            let selectRows = $scope.gridApi.selection.getSelectedRows();
            let role = self.currentItem.RoleCode;
            if (role == "" || role.length == 0) {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleselectctrl.Tips_6'));
                return;
            }
            //if (selectRows == null || selectRows.length == 0) {
            //    notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleselectctrl.Tips_7'));
            //    return;
            //}
            // console.log('save.......' + JSON.stringify(selectRows));
            var postData = {
                Role: role,
                UserId: commonService.getLoginUser().loginName,
                SaveRows: selectRows
            };
            //console.log("savesavesavesavesavesavesavesave       " + JSON.stringify(postData));
            var url = commonService.getMesApiAddress() + 'BSAppRole/SaveFunctions';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }
        //查询方法,数据绑定
        function initGridData() {
            var url = commonService.getMesApiAddress() + "BSAppRole/GetAuthorList?roleCode=" + self.currentItem.RoleCode;
            commonService.callWebApiGet(url, '').then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridOptions.data = res.data.resultData;
                    $timeout(function () {
                        _.each(self.gridOptions.data, function (item, index) {

                            if (self.gridOptions.data[index].ItemSelected == "SELECTED") {
                                // console.log(self.gridOptions.data[i]);
                                $scope.gridApi.selection.selectRow(self.gridOptions.data[index]);
                            }
                        })

                    })

                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {

                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleselectctrl.Tips_8'));
            });
        }
        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            console.log(data.data);
            if (data.data.success) {
                sidePanelManager.close();
                notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleselectctrl.Tips_9'));
                $state.go('^', {}, { reload: true });
            } else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleselectctrl.Tips_10'));
            }
        }
        function onSaveError(error) {
            console.log(123);
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleselectctrl.Tips_10'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    ViewScreenStateConfig.$inject = ['$stateProvider'];
    function ViewScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_APPRole_APPRole';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/APPRole';

        var state = {
            name: screenStateName + '.select',
            url: '/select/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/APPRole-select.html',
                    controller: ViewScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.APPRole.APPRoleselectctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
