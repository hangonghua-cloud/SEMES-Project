(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BasicDataManageFBApp.AppModelManage').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.BasicDataManageFBApp.AppModelManage.AppModelScreen.service', '$state', '$stateParams', '$rootScope', '$scope', 
        'common.base', 'common.services.logger.service', 'common.services.authentication', 'common.widgets.notificationTile.globalService', 'i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, auth, notificationService, i18nService) {
        //国际化 
        i18nService.setCurrentLang('zh-cn');
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.BasicDataManageFBApp.AppModelManage.AppModelScreen');

            init();
            GetUserInfo();
            GetDectionary();
            initGridOptions();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_BasicDataManageFBApp_AppModelManage_AppModelScreen';
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
            self.sortQueryfield = 'ModelName';//快速查询字段
            self.sortQueryfieldName = '功能名称';//快速查询字段描述
            self.RowIcon = 'common/icons/typeBuffer48.svg';//行图标

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

        //获取数据字典数据
        function GetDectionary() {
            var obj = ['PDAFunctionType'];//可以一次查询多个
            var options = GetDictionaryFilter(obj);
            dataService.getDectionary(options).then(function (data) {
                if ((data) && (data.succeeded)) {
                    //如果只查询一项则不需要再进行下面的jinq查询
                    self.ModelListList = new jinqJs()
                        .from(data.value)
                        .where(function (item) { return (item.CategoryCode == 'PDAFunctionType'); })
                        .orderBy({ field: 'SortNum', sort: 'asc' })
                        .select([{ field: 'ItemCode' }, { field: 'ItemName' }]);
                    self.ModelListList.unshift({ ItemCode: '', ItemName: '' });
                    for (var i = 0; i < self.viewerOptions.filterFields.fields.length; i++) {
                        if (self.viewerOptions.filterFields.fields[i].field == 'ModelType') {
                            self.viewerOptions.filterFields.fields[i].options = self.ModelListList;
                            self.viewerOptions.filterFields.fields[i].value = '';
                            break;
                        }
                    }
                } else {
                    self.ModelListList = [];
                }
            }, backendService.backendError);
        }

        function initGridOptions() {
            self.viewerOptions = {
                containerID: 'itemlist',
                selectionMode: 'single',
                viewOptions: 'l',
                serverDataOptions: {
                    dataService: backendService,
                    dataEntity: 'AppModelEntity',
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
                    field: 'OrderByNum',
                    direction: 'asc',
                    fields: [
                        { field: 'ModelCode', displayName: '功能编号' },
                        { field: 'ModelName', displayName: '功能名称' },
                        { field: 'OrderByNum', displayName: '排序值' },
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
                            field: 'ModelType',
                            displayName: '功能类型',
                            type: 'string',
                            widget: 'sit-select',
                            options: [],
                            toDisplay: 'ItemName',
                            toKeep: 'ItemName',
                            // onChange: self.ProcessChange,
                            default: true,
                            values: '',
                            allowedCompareOperators: [],
                            validation: {},
                            useEntityPicker: false,
                            pagingMode: 'client'
                        },
                        {
                            field: 'ModelCode',
                            displayName: '功能编号',
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
                        {
                            field: 'ModelName',
                            displayName: '功能名称',
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
                        { field: 'ModelCode', displayName: '功能编码' },
                        { field: 'ModelName', displayName: '功能名称' },
                        { field: 'ModelType', displayName: '功能类型' },
                        { field: 'OrderByNum', displayName: '排序值' },
                        { field: 'OwnedPageName', displayName: '所属页面' },
                        { field: 'ModelIco', displayName: '功能图标' },
                        // { field: 'ModelColor', displayName: '功能颜色' },
                        {
                            field: 'ModelColor', displayName: '功能颜色',
                            cellTemplate: '<div><input type="label" style="background:{{row.entity.ModelColor}};margin-top:5px;" ng-disabled="true" /></div>'
                        },
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
            var text = "是否删除 '" + self.selectedItem.ModelCode + "'?";
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
        var moduleStateName = 'home.Siemens_SimaticIT_BasicDataManageFBApp_AppModelManage';
        var moduleStateUrl = 'Siemens.SimaticIT_BasicDataManageFBApp_AppModelManage';
        var moduleFolder = 'Siemens.SimaticIT.BasicDataManageFBApp/modules/AppModelManage';

        var state = {
            name: moduleStateName + '_AppModelScreen',
            url: '/' + moduleStateUrl + '_AppModelScreen',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/AppModelScreen-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'PDA功能管理'
            }
        };
        $stateProvider.state(state);
    }
}());
