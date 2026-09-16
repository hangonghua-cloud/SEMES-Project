(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BasicDataManageFBApp.AppUserManage').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.BasicDataManageFBApp.AppUserManage.AppUserScreen.service', '$state', '$stateParams', '$rootScope', '$scope',
        'common.base', 'common.services.logger.service', 'common.services.authentication', 'common.widgets.notificationTile.globalService', 'i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, auth, notificationService, i18nService) {
        //国际化 
        i18nService.setCurrentLang('zh-cn');
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.BasicDataManageFBApp.AppUserManage.AppUserScreen');

            init();
            GetUserInfo();
            initGridOptions();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_BasicDataManageFBApp_AppUserManage_AppUserScreen';
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

            self.optionsStr = "$filter=IsDeleted eq 0 ";//初始化查询条件
            self.sortQueryfield = 'UserCode';//快速查询字段
            self.sortQueryfieldName = '用户编号';//快速查询字段描述
            self.RowIcon = 'common/icons/typeOperationCatalog48.svg';//行图标

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;

            self.playBtn = playBtn;
            self.stopBtn = stopBtn;
            self.getUsers = getUsers;

            self.resetPwd = resetPwd;

            AddSortSearch_txt(self);//自定义添加快速查询框
        }

        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        function getUsers() {
            dataService.getUsers().then(function (data) {
                if ((data) && (data.data.Result)) {
                    notificationService.info(data.data.Result);
                    self.viewerOptions.refresh();
                } else {
                    notificationService.error(data.Error.ErrorMessage);
                }
            }, backendService.backendError);
        }
        function resetPwd() {
            var title = "重置";
            // TODO: Put here the properties of the entity managed by the service
            var text = "是否重置： '" + self.selectedItem.UserCode + self.selectedItem.UserDesc + "'密码?";
            var obj = {
                'Id': self.selectedItem.Id
            };
            backendService.confirm(text, function () {
                dataService.resetPwd(obj).then(function () {
                    $state.go(rootstate, {}, { reload: true });
                }, backendService.backendError);
            }, title);

        }

        function playBtn() {
            if (self.selectedItem.IsEnabled) {
                return;
            }
            var title = "启用";
            // TODO: Put here the properties of the entity managed by the service
            var text = "是否启用用户： '" + self.selectedItem.UserCode + "'?";
            var obj = {
                'Id': self.selectedItem.Id,
                'IsEnabled': true
            };
            backendService.confirm(text, function () {
                dataService.update(obj).then(function () {
                    $state.go(rootstate, {}, { reload: true });
                }, backendService.backendError);
            }, title);
        }

        function stopBtn() {
            if (!self.selectedItem.IsEnabled) {
                return;
            }
            var title = "停用";
            // TODO: Put here the properties of the entity managed by the service
            var text = "是否停用用户： '" + self.selectedItem.UserCode + "'?";
            var obj = {
                'Id': self.selectedItem.Id,
                'IsEnabled': false
            };
            backendService.confirm(text, function () {
                dataService.update(obj).then(function () {
                    $state.go(rootstate, {}, { reload: true });
                }, backendService.backendError);
            }, title);
        }

        function initGridOptions() {
            self.viewerOptions = {
                containerID: 'itemlist',
                selectionMode: 'single',
                viewOptions: 'l',
                serverDataOptions: {
                    dataService: backendService,
                    dataEntity: 'AppUsersEntity',
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
                    field: 'UserCode',
                    direction: 'asc',
                    fields: [
                        { field: 'UserCode', displayName: '用户编号' },
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
                            field: 'UserDesc',
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
                        { field: 'UserId', displayName: '用户Id' },
                        { field: 'UserCode', displayName: '用户编号' },
                        { field: 'UserDesc', displayName: '用户名称' },
                        {
                            field: 'IsEnabled', displayName: '是否启用',
                            // cellTemplate: '<sit-mdtoggle ng-disabled="true" sit-row="row" sit-field="IsEnabled"></sit-mdtoggle>'
                            cellTemplate: '<div><input type="checkbox" ng-disabled="true" ng-model="row.entity.IsEnabled"/></div>'
                        }
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
            var title = "删除";
            // TODO: Put here the properties of the entity managed by the service
            var text = "是否删除 '" + self.selectedItem.UserCode + "'?";
            var obj = {
                Id: self.selectedItem.Id
            };
            backendService.confirm(text, function () {
                dataService.delete(obj).then(function (data) {
                    if ((data) && (data.succeeded)) {
                        notificationService.info(data.data.Result);
                        $state.go(rootstate, {}, { reload: true });
                    } else {
                        notificationService.warning(data.data.Result);
                    }
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
        var moduleStateName = 'home.Siemens_SimaticIT_BasicDataManageFBApp_AppUserManage';
        var moduleStateUrl = 'Siemens.SimaticIT_BasicDataManageFBApp_AppUserManage';
        var moduleFolder = 'Siemens.SimaticIT.BasicDataManageFBApp/modules/AppUserManage';

        var state = {
            name: moduleStateName + '_AppUserScreen',
            url: '/' + moduleStateUrl + '_AppUserScreen',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/AppUserScreen-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'PDA用户管理'
            }
        };
        $stateProvider.state(state);
    }
}());
