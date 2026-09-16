(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.Departments').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.SystemApp.Departments.Departments.service',
        '$state', '$stateParams', '$rootScope', '$scope', 'common.base', 'common.services.logger.service',
        'commonService', 'common.widgets.notificationTile.globalService', '$timeout'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, commonService, notification, $timeout) {
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.SystemApp.Departments.Departments');

            init();
            initGridOptions();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_SystemApp_Departments_Departments';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;
            self.searchButtonHandler = searchButtonHandler;
            self.doneButtonHandler = doneButtonHandler;

            //数据字典
            initDictionary();
        }

        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })

        function initDictionary() {

            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_1'), ResourceCode: "" },
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_1'), ResourceCode: "" },
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_1')
                    });
                    initGridData();
                }
            }); 
        }

        function initGridOptions() {
            self.gridOptions = commonService.uiGridOptionsConfig;
            self.gridOptions.columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                }, {
                    field: 'FactoryName',
                    displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_3'),
                    width: 150
                },
                {
                    field: 'Code',
                    displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_4'),
                    width: 150
                }
                , {
                    field: 'Name',
                    displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_5'),
                    width: 200
                }, {
                    field: 'UpdateTime',
                    displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_6'),
                    width: 200,
                    type: 'date',
                    cellFilter: 'alpDatetimeFilter'
                }
            ]
            self.gridOptions.onRegisterApi = function (gridApi) {
                $scope.gridApi = gridApi;
                //分页按钮事件
                gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                    //调用查询方法
                    initGridData();
                });
                //行选中事件
                $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                    if (row) {
                        if (row.isSelected) {
                            self.selectedItem = row.entity;
                            self.isButtonVisible = true;
                            console.log(self.selectedItem);
                        } else {
                            self.selectedItem = null;
                            self.isButtonVisible = false;
                        }
                    }
                });
            }
        }
        //查询方法,数据绑定
        function initGridData() {
            var Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'Name',
                sord: 'asc'
            };

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_7'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress() + 'Base/GetList_Department';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据            
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {

                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_8'));
            });
        }
        //查询
        function searchButtonHandler() {
            initGridData();
        }
        //U9同步
        function doneButtonHandler() {
            commonService.showLoading();
            let url = commonService.apiAdress_mes_integration + "DepartmentsCall/do";
            // let url = commonService.apiAdress_mes_integration + "PersonCall/test";
            commonService.callWebApiGet(url, null).then(function (res) {
                commonService.hideLoading();
                if (res.data == "success") {
                    commonService.showWarning(commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_9'));
                }
                else {
                    commonService.showError(res.data);
                }
            }, function (error) {
                commonService.hideLoading();
                commonService.showError('[' + error.status + '] - ' + error.statusText + '-' + error.data);
            });
        }
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function deleteButtonHandler(clickedCommand) {
            if (!self.selectedItem) {
                notification.warning(commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_10'));
                return;
            }

            var title = commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_11');
            // TODO: Put here the properties of the entity managed by the service
            var text = commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_12');

            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress() + "Base/DeleteDepartment?ID=" + self.selectedItem.ID;
                var res = commonService.callWebApiGet(url).then(onDeleteSuccess, onDeleteError);
                self.isButtonVisible = false;
            }, title);
        }

        function onDeleteSuccess(data) {
            if (data.data.success) {

                $timeout(function () {
                    initGridData();
                }, 1500);
            }
            else {
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_13'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_14',
                    text: '操作失败 - ' + data.data.returnMsg
                });
                messageservice.show();
            }
        }

        function onDeleteError(error) {
            messageservice.set({
                buttons: [{
                    id: 'ok',
                    displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_13'),
                    onClickCallback: function () {
                        messageservice.hide();
                    }
                }],
                title: 'Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_14',
                text: '[' + error.status + '] - ' + error.data.returnMsg
            });
            messageservice.show();
        }



        function onGridItemSelectionChanged(items, item) {
            if (item && item.selected == true) {
                self.selectedItem = item;
                setButtonsVisibility(true);
            } else {
                self.selectedItem = null;
                setButtonsVisibility(false);
            }
        }

        // Internal function to make item-specific buttons visible
        function setButtonsVisibility(visible) {
            self.isButtonVisible = visible;
        }
    }

    ListScreenRouteConfig.$inject = ['$stateProvider'];
    function ListScreenRouteConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_SystemApp_Departments';
        var moduleStateUrl = 'Siemens.SimaticIT_SystemApp_Departments';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/Departments';

        var state = {
            name: moduleStateName + '_Departments',
            url: '/' + moduleStateUrl + '_Departments',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/Departments-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.Departments.Departmentslistctrl.Tips_16'
            }
        };
        $stateProvider.state(state);
    }
}());
