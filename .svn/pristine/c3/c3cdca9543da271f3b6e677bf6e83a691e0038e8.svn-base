(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BaseDataApp.ReportManage').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayout.service', '$state',
        '$stateParams', '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'uiGridConstants', 'i18nService', '$http',
        'common.widgets.notificationTile.globalService', 'common.services.authentication', 'commonService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base,
        loggerService, uiGridConstants, i18nService, $http, notification, $auth, commonService) {
        var self = this;
        var logger, rootstate, messageservice, backendService, rootstateDataItemDetail;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayout');

            init();
            initGridOptions();
            initGridData();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_BaseDataApp_ReportManage_ReportLayout';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //self.dataselected = "";
            self.selectValue = "";
            //Initialize Model Data
            self.selectedItem = null;
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
            self.DataItemClass = commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_1')
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
                    //self.dataselected = data.selected[0];
                    self.selectValue = data.node.id;
                    getDataItemDetailList(self.selectValue);
                }
                if (!!data.node) {

                    self.selectedDataItemId = data.node.id;

                    self.selectedDataItemIsTree = data.node.original.isTree;

                    self.DataItemClass = data.node.text;
                    self.DataItemClassValue = data.node.original.value;

                }
            };


            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;

            self.addDataItemDetailButtonHandler = addDataItemDetailButtonHandler;
            self.editDataItemDetailButtonHandler = editDataItemDetailButtonHandler;
            self.selectDataItemDetailButtonHandler = selectDataItemDetailButtonHandler;
            self.deleteDataItemDetailButtonHandler = deleteDataItemDetailButtonHandler;
        }
        $rootScope.$on('to-parent', function (event, OnData) {
            initGridData();
            if (self.selectValue != "") {
                getDataItemDetailList(self.selectValue);
            }

        });
        $rootScope.$on("to-parentDetail", function (event, data) {

            getDataItemDetailList(self.selectValue);
        })
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
                paginationPageSizes: [50, 100, 200, 500], //每页显示个数选项
                paginationPageSize: 50, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: false, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'SortCode',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_2'),
                        width: 100
                    },

                    {
                        field: 'ReportName',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_3'),
                        width: 300
                    },
                    {
                        field: 'Address',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_4'),
                        width: 300
                    },

                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_5'),
                        width: 300
                    },

                    {
                        field: 'EnabledMark',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_6'),
                        cellTemplate: '<div class="ui-grid-cell-contents" ng-if="row.entity.EnabledMark==\'1\'">是</div>' +
                            '<div class="ui-grid-cell-contents" ng-if="row.entity.EnabledMark==\'0\'">否</div>',

                        width: 120
                    },
                    //{
                    //    field: 'entityJson.Description',
                    //    displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_5')
                    //},
                    //{
                    //    field: 'entityJson.CreateDate',
                    //    displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_7'),
                    //    cellFilter: 'date:\'yyyy-MM-dd HH:mm\'',
                    //    width: 180
                    //}
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        getDataItemDetailList(self.selectValue);
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

            var url = commonService.getMesApiAddress() + "/BaseReport/GetReportMeun";

            commonService.callWebApiPost(url, {}).then(function (data) {
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
                backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_8'));
            });
        }

        function getDataItemDetailList(parentId) {

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'SortCode',//工厂编码
                sord: 'asc'
            };

            if (!parentId) {
                self.gridOptions.data = [];
                return;
            }

            var param = {
                pagination: Pagination,
                queryJson: {
                    ParentId: parentId,
                }
            }
            var url = commonService.getMesApiAddress() + "BaseReport/GetListWithPage";
            commonService.callWebApiPost(url, param).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridOptions.data = res.data.resultData.rows;
                    self.gridOptions.totalItems = res.data.resultData.records;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_8'));
            });
        }

        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            //$state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
            if (!self.selectedDataItemId) {
                notification.warning(commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_9'));
                return;
            }

            var url = commonService.getMesApiAddress() + "/BaseReport/GetFormJson";
            commonService.callWebApiPost(url, { Id: self.selectedDataItemId }).then(function (res) {
                if ((res) && (res.data.success)) {
                    $state.go(rootstate + '.edit', { id: self.selectedDataItemId, selectedItem: res.data.resultData });
                }
            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_8'));
            });
        }

        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function deleteButtonHandler(clickedCommand) {
            if (!self.selectedDataItemId) {
                notification.warning(commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_9'));
                return;
            }

            var title = commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_10');
            // TODO: Put here the properties of the entity managed by the service
            var text = commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_11');

            backendService.confirm(text, function () {
                var postData = {
                    Id: self.selectedDataItemId,
                    ParentId: self.selectedDataItemId
                };
                var url = commonService.getMesApiAddress() + "/BaseReport/RemoveForm";
                commonService.callWebApiPost(url, postData).then(onDeleteSuccess, onDeleteError);

            }, title);
        }

        function onDeleteSuccess(data) {
            if (data.data.success) {
                self.is_editDataItemDetailButtonHandler = false;
                self.is_deleteDataItemDetailButtonHandler = false;
                $state.go(rootstate, {}, { reload: true });
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_8'));
            }
        }

        function onDeleteError(error) {
            backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_8'));
        }

        function addDataItemDetailButtonHandler(clickedCommand) {

            if (!self.selectedDataItemId) {
                notification.warning(commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_12'));
                return;
            }

            $state.go(rootstate + '.addDetail', { parentId: self.selectedDataItemId, selectedItem: self.selectedDataItemId });
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

            var title = commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_10');
            // TODO: Put here the properties of the entity managed by the service
            var text = commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_13');

            backendService.confirm(text, function () {
                var postData = {
                    username: $auth.getUser().unique_name,
                    Id: self.selectedItem.Id
                };

                var url = commonService.getMesApiAddress() + "/BaseReport/RemoveForm";
                commonService.callWebApiPost(url, postData).then(onDeleteDataItemDetailSuccess, onDeleteDataItemDetailError);

            }, title);
        }
        function onDeleteDataItemDetailSuccess(data) {
            if (data.data.success) {
                //$state.go(rootstate, {}, { reload: false });
                if (self.selectValue != "") {
                    getDataItemDetailList(self.selectValue);
                    self.is_editDataItemDetailButtonHandler = false;
                    self.is_deleteDataItemDetailButtonHandler = false;
                }
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_8'));
            }
        }

        function onDeleteDataItemDetailError(error) {
            backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_8'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_BaseDataApp_ReportManage';
        var moduleStateUrl = 'Siemens.SimaticIT_BaseDataApp_ReportManage';
        var moduleFolder = 'Siemens.SimaticIT.BaseDataApp/modules/ReportManage';

        var state = {
            name: moduleStateName + '_ReportLayout',
            url: '/' + moduleStateUrl + '_ReportLayout',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ReportLayout-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutlistctrl.Tips_14'
            }
        };
        $stateProvider.state(state);
    }
}());
