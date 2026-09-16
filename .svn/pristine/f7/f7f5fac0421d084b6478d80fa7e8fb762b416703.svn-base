(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BasicDataManageFBApp.BaseManage').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.BasicDataManageFBApp.BaseManage.BaseManageScreen.service', '$state', '$stateParams', '$rootScope', '$scope',
        'common.base', 'common.services.logger.service', 'common.services.authentication', 'common.widgets.notificationTile.globalService', 'i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, auth, notificationService, i18nService) {
        //国际化 
        i18nService.setCurrentLang('zh-cn');
        var self = this;
        var logger, rootstate, messageservice, backendService;
        //var treeNodeData = [];
        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.BasicDataManageFBApp.BaseManage.BaseManageScreen');

            init();
            initGridOptions();
            initTreeData();
            // initGridData();
        }

        function init() {
            $rootScope.$on('to-parent', function (event, OnData) {
                var options = "$filter=CategoryCode eq '" + self.treeNodeData.CategoryCode + "'";
                setTimeout(() => {
                    initGridData(options);
                }, 100);
            });

            $rootScope.$on('to-parent1', function (event, OnData) {
				setTimeout(() => {
                    initTreeData();
                }, 100);
            });

            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_BasicDataManageFBApp_BaseManage_BaseManageScreen';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            self.RowIcon = 'common/icons/typeQueryDataBase48.svg';//行图标

            //Initialize Model Data
            self.selectCategoryCode = null;
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.isAddButtonVisible = false;
            self.isParentButtonVisible = true;
            self.viewerOptions = {};
            self.viewerData = [];
            self.treeNodeData = [];

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.addParentButtonHandler = addParentButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.editParentButtonHandler = editParentButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;
            self.deleteParentButtonHandler = deleteParentButtonHandler;
            //self.menuId = '00000000-0000-0000-0000-000000000000';
            self.treeNodeData.menuId = self.treeNodeData.ParentId = '00000000-0000-0000-0000-000000000000';
            self.treeOptions = {
                enableMultiselection: false,
                $init: function () {
                    //tree选中节点事件
                    this.$subscribe('onNodeSelected', function (eventArgs) {
                        if (eventArgs.node.id == '00000000-0000-0000-0000-000000000000') {//一级菜单
                            if (self.treeNodeData.menuId == eventArgs.node.id) {
                                //取消选中
                                self.treeNodeData.menuId = null;
                                self.isParentButtonVisible = false;
                                return;
                            } else {
                                self.treeNodeData.menuId = eventArgs.node.id;
                                self.isParentButtonVisible = true;
                            }
                            self.treeNodeData.Id = '00000000-0000-0000-0000-000000000000';
                            self.treeNodeData.ParentId = '00000000-0000-0000-0000-000000000000';
                        } else {
                            if (eventArgs.node.ParentId == '00000000-0000-0000-0000-000000000000') {//二级菜单
                                if (self.treeNodeData.menuId == eventArgs.node.id) {
                                    //取消选中
                                    self.treeNodeData.menuId = null;
                                    self.isParentButtonVisible = false;
                                    return;
                                } else {
                                    self.treeNodeData.menuId = eventArgs.node.id;
                                    self.isParentButtonVisible = true;
                                }
                                self.treeNodeData.ParentId = eventArgs.node.id;//只新增用
                                self.treeNodeData.Id = eventArgs.node.id;
                                self.treeNodeData.CategoryCode = eventArgs.node.CategoryCode;
                                self.treeNodeData.CategoryName = eventArgs.node.CategoryName;
                                self.treeNodeData.SortNum = eventArgs.node.SortNum;
                            } else {//三级菜单
                                if (eventArgs.node.ParentId != null && eventArgs.node.ParentId != undefined) {
                                    if (self.treeNodeData.menuId == eventArgs.node.id) {
                                        //取消选中
                                        self.treeNodeData.menuId = null;
                                        self.isParentButtonVisible = false;
                                        self.isAddButtonVisible = false;
                                        return;
                                    } else {
                                        self.treeNodeData.menuId = eventArgs.node.id;
                                        self.isParentButtonVisible = true;
                                        self.isAddButtonVisible = true;
                                    }
                                    self.treeNodeData.Id = eventArgs.node.id;
                                    self.treeNodeData.CategoryCode = eventArgs.node.CategoryCode;
                                    self.treeNodeData.CategoryName = eventArgs.node.CategoryName;
                                    self.treeNodeData.SortNum = eventArgs.node.SortNum;
                                    //根据选中三级菜单重新加载右边表格
                                    var options = "$filter=CategoryCode eq '" + self.treeNodeData.CategoryCode + "'";
                                    initGridData(options);
                                }
                            }
                        }
                    })
                },
                data: [
                    {
                        id: '00000000-0000-0000-0000-000000000000',
                        label: '数据字典',
                        icon: 'Library',
                        expanded: false,
                        selected: true,
                        children: []
                    }
                ]
            };
        }

        function initGridOptions() {
            self.viewerOptions = {
                containerID: 'itemlist',
                selectionMode: 'single',
                viewOptions: 'l',
                height: '840',
                // TODO: Put here the properties of the entity managed by the service
                quickSearchOptions: { enabled: true, field: 'ItemName' },
                sortInfo: {
                    field: 'SortNum',
                    direction: 'asc',
                    fields: [
                        { field: 'SortNum', displayName: '排序号' },
                        { field: 'ItemCode', displayName: '编号' },
                    ]
                },
                // image: 'fa-cube',
                // tileConfig: {
                //     titleField: 'Id'
                // },
                filterBarOptions: 'sf',
                svgIcon: self.RowIcon,
                gridConfig: {
                    // TODO: Put here the properties of the entity managed by the service
                    columnDefs: [
                        // { field: 'Id', displayName: 'Id'},
                        { field: 'CategoryCode', displayName: '类别编号' },
                        { field: 'ItemCode', displayName: '编号' },
                        { field: 'ItemName', displayName: '名称' },
                        { field: 'ItemValue', displayName: '值' },
                        { field: 'SortNum', displayName: '排序值' },
                        // { field: 'IsValid', displayName: '是否启用' }
                        {
                            field: 'IsValid', displayName: '是否启用',
                            cellTemplate: '<sit-mdtoggle ng-disabled="true" sit-row="row" sit-field="IsValid"></sit-mdtoggle>'
                            // cellTemplate: '<div><input type="checkbox" ng-disabled="true" ng-model="row.entity.IsValid"/></div>'
                        }
                    ],
                    showSelectionCheckbox: true,
                },
                onSelectionChangeCallback: onGridItemSelectionChanged
            }
        }

        function initGridData(options) {
            dataService.getAll(options).then(function (data) {
                if ((data) && (data.succeeded)) {
                    self.viewerData = data.value;
                } else {
                    self.viewerData = [];
                }
            }, backendService.backendError);
        }

        function initTreeData(){
            dataService.selectTree().then(function (data) {
                self.treeOptions.data = [
                    {
                        id: '00000000-0000-0000-0000-000000000000',
                        label: '数据字典',
                        icon: 'Library',
                        expanded: false,
                        selected: true,
                        children: data.data.TreeData
                    }
                ]
                //self.treeOptions.api.refresh();
                self.treeOptions.data[0].expanded = true;
                self.treeOptions.api.refresh();
            }, backendService.backendError);
        }

        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add', { CategoryCode: self.treeNodeData.CategoryCode });
        }

        function addParentButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addParent', { ParentId: self.treeNodeData.ParentId });
        }

        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function editParentButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            if (self.treeNodeData.menuId == '00000000-0000-0000-0000-000000000000') {
                notificationService.info('该项不允许修改！');
            } else {
                $state.go(rootstate + '.editParent', { id: self.treeNodeData.Id, selectedItem: self.treeNodeData });
            }
        }

        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function deleteButtonHandler(clickedCommand) {
            var title = "删除";
            // TODO: Put here the properties of the entity managed by the service
            var text = "是否删除 '" + self.selectedItem.ItemName + "'?";

            // backendService.confirm(text, function () {
            //     dataService.delete(self.selectedItem).then(function () {
            //         $state.go(rootstate, {}, { reload: true });
            //     }, backendService.backendError);
            // }, title);
            backendService.confirm(text, function () {
                dataService.delete(self.selectedItem).then(function (data) {
                    if ((data) && (data.succeeded)) {
                        notificationService.info(data.data.ReturnVal);
                        // $state.go(rootstate, {}, { reload: true });
                        var options = "$filter=CategoryCode eq '" + self.treeNodeData.CategoryCode + "'";
                        initGridData(options);
                    } else {
                        notificationService.warning(data.data.ReturnVal);
                    }
                }, backendService.backendError);
            }, title);
        }

        function deleteParentButtonHandler(clickedCommand) {
            if (self.treeNodeData.menuId == '00000000-0000-0000-0000-000000000000') {
                notificationService.info('请选择要删除的项！');
            } else {
                var title = "删除";
                // TODO: Put here the properties of the entity managed by the service
                var text = "是否删除 '" + self.treeNodeData.CategoryName + "'?";
                // backendService.confirm(text, function () {
                //     dataService.deleteParent(self.treeNodeData).then(function () {
                //         $state.go(rootstate, {}, { reload: true });
                //     }, backendService.backendError);
                // }, title);
                backendService.confirm(text, function () {
                    dataService.deleteParent(self.treeNodeData).then(function (data) {
                        if ((data) && (data.succeeded)) {
                            notificationService.info(data.data.ReturnVal);
                            // $state.go(rootstate, {}, { reload: true });
                            initTreeData();
                        } else {
                            notificationService.warning(data.data.ReturnVal);
                        }
                    }, backendService.backendError);
                }, title);
            }
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
        var moduleStateName = 'home.Siemens_SimaticIT_BasicDataManageFBApp_BaseManage';
        var moduleStateUrl = 'Siemens.SimaticIT_BasicDataManageFBApp_BaseManage';
        var moduleFolder = 'Siemens.SimaticIT.BasicDataManageFBApp/modules/BaseManage';

        var state = {
            name: moduleStateName + '_BaseManageScreen',
            url: '/' + moduleStateUrl + '_BaseManageScreen',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/BaseManageScreen-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: '数据字典'
            }
        };
        $stateProvider.state(state);
    }
}());
