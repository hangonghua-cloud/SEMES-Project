(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BasicDataManageFBApp.AppRoleManage').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.BasicDataManageFBApp.AppRoleManage.AppRoleScreen.service', '$state', '$stateParams', '$rootScope', '$scope', 
        'common.base', 'common.services.logger.service', 'common.services.authentication', 'common.widgets.notificationTile.globalService', 'i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, auth, notificationService, i18nService) {
        //国际化 
        i18nService.setCurrentLang('zh-cn');
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.BasicDataManageFBApp.AppRoleManage.AppRoleScreen');

            init();
            GetUserInfo();
            initGridOptions();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_BasicDataManageFBApp_AppRoleManage_AppRoleScreen';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;
            
            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];

            self.UserId = '';
            self.UserCode = '';
            self.UserName = '';

            self.optionsStr = "$filter=IsDeleted eq 0 and Name ne 'AccessControlAdmin' and Name ne 'AccessControlViewer' and " + 
                              "Name ne 'SystemMonitoring' and Name ne 'UserPreferences' and Name ne 'Siemens.SimaticIT.System.System.SAPOMModel.Security.UserPreferences' ";//初始化查询条件
            self.sortQueryfield = 'Name';//快速查询字段
            self.sortQueryfieldName = '角色编号';//快速查询字段描述
            self.RowIcon = 'common/icons/typeDocumentViewer48.svg';//行图标

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;

            AddSortSearch_txt(self);//自定义添加快速查询框
        }

        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        function initGridOptions() {
            self.viewerOptions = {
                containerID: 'itemlist',
                selectionMode: 'single',
                viewOptions: 'l',
                serverDataOptions: {
                    dataService: backendService,
                    dataEntity: 'V_UARoles',
                    optionsString: self.optionsStr,
                    appName: 'BasicDataManageFBApp'
                },
                enablePaging: true,
                pagingOptions: {
                    pageSizes: [10, 25, 50, 100, 500],
                    pageSize: 25,
                    currentPage: 1
                },
                // TODO: Put here the properties of the entity managed by the service

                sortInfo: {
                    field: 'Name',
                    direction: 'asc',
                    fields: [
                        { field: 'Name', displayName: '用户编号' },
                    ]
                },
                //image: 'fa-cube',
                //tileConfig: {
                //    titleField: 'Id'
                //},
                filterBarOptions: 'sf',
                filterFields: {
                    type: 'filterPanel',
                    onApplyCallback: function (obj) {
                        //查询
                        self.viewerOptions.serverDataOptions.optionsString = GetQueryWhereStr(obj, self.optionsStr);
                        self.viewerOptions.refresh();
                    },
                    onResetCallback: function () {
                        //重置
                        self.viewerOptions.serverDataOptions.optionsString = self.optionsStr;
                        self.viewerOptions.refresh();
                    },
                    fields: [
                        {
                            field: 'Description',
                            displayName: '用户名称',
                            searchType: 'co',
                            type: 'string',
                            default: true,
                            values: '',
                            widget: 'sit-text',
                            allowedCompareOperators: [],
                            validation: {},
                            useEntityPicker: false,
                            pagingMode: 'client'
                        },
                    ]
                },
                svgIcon: self.RowIcon,
                gridConfig: {
                    // TODO: Put here the properties of the entity managed by the service
                    columnDefs: [
                        { field: 'Name', displayName: '角色编号' },
                        { field: 'Description', displayName: '角色名称' }
                    ],
                    showSelectionCheckbox: true,
                },
                onSelectionChangeCallback: onGridItemSelectionChanged
            }
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
            var title = "Delete";
            // TODO: Put here the properties of the entity managed by the service
            var text = "Do you want to delete '" + self.selectedItem.Id + "'?";

            backendService.confirm(text, function () {
                dataService.delete(self.selectedItem).then(function () {
                    $state.go(rootstate, {}, { reload: true });
                }, backendService.backendError);
            }, title);
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
        var moduleStateName = 'home.Siemens_SimaticIT_BasicDataManageFBApp_AppRoleManage';
        var moduleStateUrl = 'Siemens.SimaticIT_BasicDataManageFBApp_AppRoleManage';
        var moduleFolder = 'Siemens.SimaticIT.BasicDataManageFBApp/modules/AppRoleManage';

        var state = {
            name: moduleStateName + '_AppRoleScreen',
            url: '/' + moduleStateUrl + '_AppRoleScreen',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/AppRoleScreen-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'PDA角色权限管理'
            }
        };
        $stateProvider.state(state);
    }
}());
