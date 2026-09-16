(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BaseDataApp.DataDictionary').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionary.service', '$state',
        '$stateParams', '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'uiGridConstants', 'i18nService', '$http',
        'common.widgets.notificationTile.globalService', 'common.services.authentication', 'commonService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base,
        loggerService, uiGridConstants, i18nService, $http, notification, $auth, commonService) {
        var self = this;
        var logger, rootstate, messageservice, backendService, rootstateDataItemDetail;

        activate();

        // Initialization function
        function activate() {

            logger = loggerService.getModuleLogger('home.Siemens_SimaticIT_BaseDataApp_DataDictionary_DataDictionary');

            init();
            initGridOptions();
            initGridData();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_BaseDataApp_DataDictionary_DataDictionary';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;
            self.dataselected = "";
            //Initialize Model Data
            self.selectedItem = null;
            //左-新增
            self.is_addButtonHandler = true;
            //左-编辑
            self.is_editButtonHandler = false;
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
            self.DataItemClass = commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_1')
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
                    //左-编辑
                    self.is_editButtonHandler = true;
                    //左-删除
                    self.is_deleteButtonHandler = true;
                    //右-新增
                    self.is_addDataItemDetailButtonHandler = true;
                    //右-编辑
                    self.is_editDataItemDetailButtonHandler = false;
                    //右-删除
                    self.is_deleteDataItemDetailButtonHandler = false;
                    self.dataselected = data.selected[0];
                    getDataItemDetailList(data.selected[0]);
                }
                if (!!data.node) {

                    self.selectedDataItemId = data.node.id;

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
            self.editDataItemDetailButtonHandler = editDataItemDetailButtonHandler;
            self.selectDataItemDetailButtonHandler = selectDataItemDetailButtonHandler;
            self.deleteDataItemDetailButtonHandler = deleteDataItemDetailButtonHandler;
        }

        function initGridOptions() {
            i18nService.setCurrentLang("zh-cn");
            self.gridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                //paginationPageSizes: [10, 20, 50, 100, 200, 500],
                //paginationPageSize: 100,
                //minRowsToShow: 23,
                useExternalPagination: false,//true:使用外部分页方式；false:使用UI Grid内部分页方式
                useExternalSorting: false,//true:使用外部排序方式，false:使用UI Gird内部排序方式
                multiSelect: false,
                enableRowSelection: true,
                enableRowHeaderSelection: false,
                enableColumnResizing: true,//允许调整列宽
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_2'), minWidth: 70, width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'entityJson.ItemValue',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_3'),
                        width: 100
                    },
                    {
                        field: 'entityJson.ItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_4'),
                        width: 200
                    },
                    {
                        field: 'entityJson.Description',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_5'),
                        width: 250
                    },
                    {
                        field: 'entityJson.Remark1',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_6'),
                        width: 250
                    },
                    {
                        field: 'entityJson.SortCode',
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_7'),
                        width: 80
                    },
                    {
                        field: 'entityJson.EnabledMark', displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_8'),
                        width: 80,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.entityJson.EnabledMark==1"><span ng-cell-text class="">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.entityJson.EnabledMark!=1"><span ng-cell-text class="green">否</span></div>'
                    }
                    //{
                    //    field: 'entityJson.Description',
                    //    displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_5')
                    //},
                    //{
                    //    field: 'entityJson.CreateDate',
                    //    displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_9'),
                    //    cellFilter: 'date:\'yyyy-MM-dd HH:mm\'',
                    //    width: 180
                    //}
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {

                        if (row && row.isSelected == true) {
                            self.selectedItem = row;
                            setButtonsVisibility(true);
                            //左-新增
                            self.is_addButtonHandler = true;
                            //左-编辑
                            self.is_editButtonHandler = false;
                            //左-删除
                            self.is_deleteButtonHandler = false;

                            //右-新增
                            self.is_addDataItemDetailButtonHandler = true;

                            if (row.entity.entityJson["IsDefault"] != undefined && row.entity.entityJson["IsDefault"] == 1) {
                                //右-编辑
                                self.is_editDataItemDetailButtonHandler = false;
                                //右-删除
                                self.is_deleteDataItemDetailButtonHandler = false;
                            }
                            else {
                                //右-编辑
                                self.is_editDataItemDetailButtonHandler = true;
                                //右-删除
                                self.is_deleteDataItemDetailButtonHandler = true;
                            }
                        } else {
                            self.selectedItem = null;
                            setButtonsVisibility(false);
                        }
                    });
                },
                data: []
            }
        }

        function initGridData() {
            //var url = commonService.getMesApiAddress() + "/SystemManage/DataItem/GetTreeJson";
            var url = commonService.getMesApiAddress() + "/SystemManage/DataItem/GetTreeJson";

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
                backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_10'));
            });
        }

        function getDataItemDetailList(itemId) {
            var url = commonService.getMesApiAddress() + "/SystemManage/DataItemDetail/GetTreeListJson?itemId=" + itemId;
            commonService.callWebApiGet(url, null).then(function (res) {
                if ((res) && (res.data.success)) {
                    var resultData = res.data.resultData;
                    self.gridOptions.data = resultData;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_10'));
            });
        }

        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            //$state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
            if (!self.selectedDataItemId) {
                notification.warning(commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_11'));
                return;
            }

            var url = commonService.getMesApiAddress() + "/SystemManage/DataItem/GetFormJson?keyValue=" + self.selectedDataItemId;
            commonService.callWebApiGet(url, null).then(function (res) {
                if ((res) && (res.data.success)) {
                    $state.go(rootstate + '.edit', { id: self.selectedDataItemId, selectedItem: res.data.resultData });
                }
            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_10'));
            });
        }

        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function deleteButtonHandler(clickedCommand) {
            if (!self.selectedDataItemId) {
                notification.warning(commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_11'));
                return;
            }

            var title = commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_12');
            // TODO: Put here the properties of the entity managed by the service
            var text = commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_13') + self.DataItemClass + commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_14');

            backendService.confirm(text, function () {
                var postData = {
                    username: $auth.getUser().unique_name,
                    keyValue: self.selectedDataItemId
                };

                var url = commonService.getMesApiAddress() + "/SystemManage/DataItem/RemoveForm";
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
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_10'));
            }
        }

        function onDeleteError(error) {
            backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_10'));
        }

        function addDataItemDetailButtonHandler(clickedCommand) {

            if (!self.selectedDataItemId) {
                notification.warning(commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_11'));
                return;
            }

            var parentId = self.selectedDataItemId;
            // if (!!self.selectedItem) {
            //     parentId = self.selectedItem.Id;
            // }
            // if (self.selectedDataItemIsTree != "1") {
            //     parentId = "0";
            // }

            $state.go(rootstate + '.dataItemAdd', { parentId: parentId, itemId: self.selectedDataItemId });
        }

        function editDataItemDetailButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service

            $state.go(rootstate + '.editDataItemDetail', { id: self.selectedItem.entity.entityJson.ItemDetailId, selectedItem: self.selectedItem.entity.entityJson });
        }

        function selectDataItemDetailButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function deleteDataItemDetailButtonHandler(clickedCommand) {

            var title = commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_12');
            // TODO: Put here the properties of the entity managed by the service
            var text = commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_15') + self.selectedItem.entity.entityJson.ItemName + commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_14');

            backendService.confirm(text, function () {
                var postData = {
                    username: $auth.getUser().unique_name,
                    keyValue: self.selectedItem.entity.entityJson.ItemDetailId
                };

                var url = commonService.getMesApiAddress() + "/SystemManage/DataItemDetail/RemoveForm";
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
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_10'));
            }
        }

        function onDeleteDataItemDetailError(error) {
            backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_10'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_BaseDataApp_DataDictionary';
        var moduleStateUrl = 'Siemens.SimaticIT_BaseDataApp_DataDictionary';
        var moduleFolder = 'Siemens.SimaticIT.BaseDataApp/modules/DataDictionary';

        var state = {
            name: moduleStateName + '_DataDictionary',
            url: '/' + moduleStateUrl + '_DataDictionary',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/DataDictionary-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionarylistctrl.Tips_16'
            }
        };
        $stateProvider.state(state);
    }
}());
