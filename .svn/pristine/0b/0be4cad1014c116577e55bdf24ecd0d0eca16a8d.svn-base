(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MaterialGroup').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroup.service', '$state',
        '$stateParams', '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'uiGridConstants', 'i18nService', '$http',
        'common.widgets.notificationTile.globalService', 'common.services.authentication', 'commonService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base,
        loggerService, uiGridConstants, i18nService, $http, notification, $auth, commonService) {
        var self = this;
        var logger, rootstate, messageservice, backendService, rootstateDataItemDetail;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroup');

            init();
            initGridOptions();
            initGridData();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_MaterialGroup_MaterialGroup';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            self.dataselected = "";
            //Initialize Model Data
            self.selectedItem = null;
            self.searchParams = {};
            //左-新增
            self.is_addButtonHandler = true;
            //左-删除
            self.is_deleteButtonHandler = false;

            //右-新增
            self.is_addDataItemDetailButtonHandler = false;
            //右-编辑
            self.is_editDataItemDetailButtonHandler = false;
            //右-删除
            self.is_deleteDataItemDetailButtonHandler = false;

            self.viewerOptions = {};
            self.viewerData = [];
            self.selectedDataItemId = null;
            self.DataItemClass = commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_1')
            self.DataItemClassValue = ""
            if (!!$stateParams.itemId) {
                self.selectedDataItemId = $stateParams.itemId;
                getDataItemDetailList(self.selectedDataItemId);
            }
            $scope.typesConfig = {
                "#": {
                    //   "max_children" : 1,
                    //   "max_depth" : 4,
                    "valid_children": ["root"]
                },
                "root": {
                    //"icon" : "CR.FactoryModelingApp/scripts/jstree/tree.png",
                    "icon": "fa fa-folder",
                    "valid_children": ["default"]
                },
                "default": {
                    //"icon" : 'jstree-folder',
                    "icon": 'fa fa-folder',
                    "valid_children": ["default", "file"]
                },
                "file": {
                    //"icon" : 'jstree-file',
                    "icon": 'fa fa-file-o',
                    "valid_children": []
                }
            }

            $scope.changedCB = function (e, data) {
                var i, j, r = [];
                for (i = 0, j = data.selected.length; i < j; i++) {
                    r.push(data.instance.get_node(data.selected[i]).id);
                }
                if (data.selected.length > 0) {
                    //左-新增
                    self.is_addButtonHandler = true;
                    //左-删除
                    self.is_deleteButtonHandler = true;
                    //右-新增
                    self.is_addDataItemDetailButtonHandler = true;
                    //右-编辑
                    self.is_editDataItemDetailButtonHandler = false;
                    //右-删除
                    self.is_deleteDataItemDetailButtonHandler = false;
                    //self.dataselected = data.selected[0];//id关联
                    self.dataselected = data.node.original.value;

                    getDataItemDetailList(data.node.original.value);
                }
                if (!!data.node) {

                    // self.selectedDataItemId = data.node.id;//子表关联Id
                    self.selectedData = data.node;
                    self.selectedDataItemId = data.node.original.value;//子表关联编码

                    self.selectedDataItemIsTree = data.node.original.isTree;

                    self.DataItemClass = data.node.text;
                    self.DataItemClassValue = data.node.original.value;

                }
            };

            $rootScope.$on('to-parent', function (event, OnData) {
                initGridData();
                if (self.dataselected != "") {
                    getDataItemDetailList(self.dataselected);
                }

            });
            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;

            self.addDataItemDetailButtonHandler = addDataItemDetailButtonHandler;
            self.addDataItemSpecButtonHandler = addDataItemSpecButtonHandler;
            self.editDataItemDetailButtonHandler = editDataItemDetailButtonHandler;
            self.selectDataItemDetailButtonHandler = selectDataItemDetailButtonHandler;
            self.deleteDataItemDetailButtonHandler = deleteDataItemDetailButtonHandler;

            self.searchButtonHandler = searchButtonHandler;
        }

        function initGridOptions() {
            i18nService.setCurrentLang("zh-cn");
            self.gridOptions = {
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
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
                paginationPageSize: 100, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: false, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_2'), minWidth: 80, width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_3'),
                        width: 300
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_4'),
                        width: 200
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_5'),
                        width: 200
                    },
                    {
                        field: 'MaterialClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_8'),
                        width: 100
                    },

                    //{
                    //    field: 'entityJson.Description',
                    //    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_9')
                    //},
                    //{
                    //    field: 'entityJson.CreateDate',
                    //    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_10'),
                    //    cellFilter: 'date:\'yyyy-MM-dd HH:mm\'',
                    //    width: 180
                    //}
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        getDataItemDetailList(self.DataItemClassValue);
                    });
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {

                        if (row && row.isSelected == true) {
                            self.selectedItem = row.entity;
                            setButtonsVisibility(true);
                            //左-新增
                            self.is_addButtonHandler = true;
                            //左-删除
                            self.is_deleteButtonHandler = false;

                            //右-新增
                            self.is_addDataItemDetailButtonHandler = true;
                            //右-编辑
                            self.is_editDataItemDetailButtonHandler = true;
                            //右-删除
                            self.is_deleteDataItemDetailButtonHandler = true;

                        } else {
                            self.selectedItem = null;
                            //右-编辑
                            self.is_editDataItemDetailButtonHandler = false;
                            //右-删除
                            self.is_deleteDataItemDetailButtonHandler = false;
                            setButtonsVisibility(false);
                        }
                    });
                },
                data: []
            }
        }

        function initGridData() {
            //var url = commonService.getMesApiAddress("material") + "/SystemManage/DataItem/GetTreeJson";
            var url = commonService.getMesApiAddress("material") + "/Base_MaterialGroup/GetTreeJson";

            commonService.callWebApiGet(url, null).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;

                    var treeData = [];
                    for (var i = 0; i < jsonData.length; i++) {
                        var opened = true;
                        var selected = false;
                        var nodeType = "default";

                        var parentId = jsonData[i].parentId;
                        if (parentId == "0") {
                            parentId = "#";
                        }
                        if (parentId == "#") {
                            nodeType = "root";
                        }
                        else if (!jsonData.some(item => item.parentId === jsonData[i].id)) {
                            nodeType = "file";
                        }
                        if (jsonData[i].id == self.selectedDataItemId) {
                            selected = true;
                            self.DataItemClass = jsonData[i].text;
                        }

                        treeData.push(
                            {
                                "id": jsonData[i].id,
                                "parent": parentId,
                                "text": jsonData[i].text,
                                "type": nodeType,
                                "value": jsonData[i].value,
                                "isTree": jsonData[i].AttributeValue,
                                'state': { 'opened': opened, 'selected': selected },
                                "isDefault": jsonData[i].isDefault
                            }
                        );
                    }
                    $scope.treeModel = treeData;
                } else {
                    //self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_11'));
            });
        }

        function searchButtonHandler(params) {
            getDataItemDetailList(self.DataItemClassValue);
        }

        function getDataItemDetailList(GroupCode) {

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//创建时间
                sord: 'desc'
            };
            self.searchParams.GroupCode = GroupCode
            var param = {
                pagination: Pagination,
                queryJson: self.searchParams
            }
            var url = commonService.getMesApiAddress("material") + "Base_MaterialGroupBindMaterial/GetDataTableWithPage";
            commonService.callWebApiPost(url, param).then(function (res) {
                if ((res) && (res.data.success)) {
                    var resultData = res.data.resultData.rows;
                    self.gridOptions.data = resultData;
                    self.gridOptions.totalItems = res.data.resultData.records;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_11'));
            });
        }

        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            //$state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
            if (!self.selectedDataItemId) {
                notification.warning(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_12'));
                return;
            }

            var url = commonService.getMesApiAddress("material") + "/Base_MaterialGroup/GetFormJson?keyValue=" + self.selectedDataItemId;
            commonService.callWebApiGet(url, null).then(function (res) {
                if ((res) && (res.data.success)) {
                    $state.go(rootstate + '.edit', { id: self.selectedDataItemId, selectedItem: res.data.resultData });
                }
            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_11'));
            });
        }

        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function deleteButtonHandler(clickedCommand) {
            if (!self.selectedData) {
                notification.warning(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_12'));
                return;
            }

            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_13');
            // TODO: Put here the properties of the entity managed by the service
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_14') + self.DataItemClass + commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_15');

            backendService.confirm(text, function () {
                var postData = {
                    username: $auth.getUser().unique_name,
                    keyValue: self.selectedData.id,
                    GroupCode: self.selectedData.original.value
                };

                var url = commonService.getMesApiAddress("material") + "/Base_MaterialGroup/RemoveForm";
                commonService.callWebApiPost(url, postData).then(onDeleteSuccess, onDeleteError);

            }, title);
        }

        function onDeleteSuccess(data) {
            if (data.data.success) {
                self.is_editDataItemDetailButtonHandler = false;
                self.is_deleteDataItemDetailButtonHandler = false;
                //self.is_addDataItemDetailButtonHandler = false;
                //self.is_deleteButtonHandler = false;
                $state.go(rootstate, {}, { reload: true });
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_11'));
            }
        }

        function onDeleteError(error) {
            backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_11'));
        }

        function addDataItemDetailButtonHandler(clickedCommand) {

            if (!self.selectedDataItemId) {
                notification.warning(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_12'));
                return;
            }
            var data = self.gridOptions.data;

            var parentId = self.selectedDataItemId;
            // if (!!self.selectedItem) {
            //     parentId = self.selectedItem.Id;
            // }
            // if (self.selectedDataItemIsTree != "1") {
            //     parentId = "0";
            // }

            $state.go(rootstate + '.addDetail', { Id: parentId, selectedItem: { GroupCode: self.selectedDataItemId, GridData: data } });
        }

        //关联规格
        function addDataItemSpecButtonHandler(clickedCommand) {

            if (!self.selectedDataItemId) {
                notification.warning(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_12'));
                return;
            }
            var data = self.gridOptions.data;

            var parentId = self.selectedDataItemId;

            $state.go(rootstate + '.addSpec', { Id: parentId, selectedItem: { GroupCode: self.selectedDataItemId, GridData: data } });
        }

        function editDataItemDetailButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service

            $state.go(rootstate + '.editDetail', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function selectDataItemDetailButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function deleteDataItemDetailButtonHandler(clickedCommand) {

            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_13');
            // TODO: Put here the properties of the entity managed by the service
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_16');

            backendService.confirm(text, function () {
                var postData = {
                    username: $auth.getUser().unique_name,
                    keyValue: self.selectedItem.Id
                };

                var url = commonService.getMesApiAddress("material") + "/Base_MaterialGroupBindMaterial/RemoveForm";
                commonService.callWebApiPost(url, postData).then(onDeleteDataItemDetailSuccess, onDeleteDataItemDetailError);

            }, title);
        }
        function onDeleteDataItemDetailSuccess(data) {
            if (data.data.success) {
                //$state.go(rootstate, {}, { reload: false });
                if (self.dataselected != "") {
                    getDataItemDetailList(self.dataselected);
                    self.is_editDataItemDetailButtonHandler = false;
                    self.is_deleteDataItemDetailButtonHandler = false;
                }
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_11'));
            }
        }

        function onDeleteDataItemDetailError(error) {
            backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_11'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_MaterialGroup';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_MaterialGroup';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MaterialGroup';

        var state = {
            name: moduleStateName + '_MaterialGroup',
            url: '/' + moduleStateUrl + '_MaterialGroup',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/MaterialGroup-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGrouplistctrl.Tips_17'
            }
        };
        $stateProvider.state(state);
    }
}());
