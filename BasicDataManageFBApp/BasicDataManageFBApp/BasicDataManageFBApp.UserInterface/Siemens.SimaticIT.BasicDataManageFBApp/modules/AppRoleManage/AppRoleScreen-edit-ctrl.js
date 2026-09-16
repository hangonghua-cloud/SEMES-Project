(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BasicDataManageFBApp.AppRoleManage').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.BasicDataManageFBApp.AppRoleManage.AppRoleScreen.service', '$state', '$stateParams', 'common.base', '$filter',
        '$scope', 'common.services.authentication', 'common.widgets.notificationTile.globalService'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, auth, notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            GetUserInfo();
            initGridOptions();
            initGridData();
            registerEvents();

            sidePanelManager.setTitle('配置移动端角色权限');
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            self.UserId = '';
            self.UserCode = '';
            self.UserName = '';

            self.optionsStr = "@x={'RoleCode':'" + self.currentItem.RoleCode + "'}&$filter=1 eq 1 ";//初始化查询条件
            self.sortQueryfield = 'ModelCode';//快速查询字段
            self.sortQueryfieldName = '功能编码';//快速查询字段描述
            self.RowIcon = 'common/icons/typeDocumentViewer48.svg';//行图标

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

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
                enablePaging: true,
                pagingOptions: {
                    pageSizes: [10, 25, 50, 100, 500],
                    pageSize: 25,
                    currentPage: 1
                },
                // quickSearchOptions: { enabled: true, field: 'ModelName' },
                sortInfo: {
                    field: 'ModelCode',
                    direction: 'asc',
                    fields: [
                        { field: 'ModelCode', displayName: '功能编码' },
                        { field: 'ModelType', displayName: '类型' },
                    ]
                },
                gridConfig: {
                    columnDefs: [
                        { field: 'ModelCode', displayName: '模块编码' },
                        { field: 'ModelName', displayName: '模块名称' },
                        { field: 'ModelType', displayName: '类型' },
                        { field: 'OwnedPage', displayName: '所属页面' },
                        {
                            field: 'IsEnabled', displayName: '是否启用',
                            // cellTemplate: '<sit-mdtoggle ng-disabled="true" sit-row="row" sit-field="IsEnabled"></sit-mdtoggle>'
                            cellTemplate: '<div><input type="checkbox" ng-model="row.entity.IsEnabled"/></div>'
                        }
                    ]
                },
                onSelectionChangeCallback: onGridItemSelectionChanged
            }
            setTimeout(function () {
                self.viewerOptions.refresh();
            }, '0');
        }

        function initGridData() {
            var obj = {
                RoleCode: self.currentItem.Name
            };
            dataService.readRF(obj).then(function (data) {
                if ((data) && (data.succeeded)) {
                    self.viewerData = data.value;
                } else {
                    self.viewerData = [];
                }
            }, backendService.backendError);
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
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

        function save() {
            if (self.viewerData.length > 0) {
                var saveList = [];
                for (var i = 0; i < self.viewerData.length; i++) {
                    saveList.push({
                        ModelCode: self.viewerData[i].ModelCode,
                        IsEnabled: self.viewerData[i].IsEnabled,
                    });
                }
                var obj = {
                    RoleCode: self.currentItem.Name,
                    UserCode: self.UserCode,
                    UserName: self.UserCode + '-' + self.UserName,
                    ModelList: saveList
                };
                dataService.update(obj).then(function (data) {
                    if ((data) && (data.succeeded)) {
                        notificationService.info(data.data.Result);
                        onSaveSuccess();
                    } else {
                        notificationService.warning(data.data.Result);
                    }
                }, backendService.backendError);
                // dataService.update(obj).then(onSaveSuccess, backendService.backendError);
            } else {

            }
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            sidePanelManager.close();
            $state.go('^', {}, { reload: true });
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BasicDataManageFBApp_AppRoleManage_AppRoleScreen';
        var moduleFolder = 'Siemens.SimaticIT.BasicDataManageFBApp/modules/AppRoleManage';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/AppRoleScreen-edit.html',
                    controller: EditScreenController,
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
