(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFileds.service', '$state',
        '$stateParams', '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'uiGridConstants', 'i18nService', '$http',
        'common.widgets.notificationTile.globalService', 'common.services.authentication', 'commonService','$timeout'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base,
        loggerService, uiGridConstants, i18nService, $http, notification, $auth, commonService, $timeout) {
        var self = this;
        var logger, rootstate, messageservice, backendService, rootstateDataItemDetail;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFileds');

            init();
            initGridOptions();
            initGridData();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_FactoryModelApp_ModelLevelExtendFileds_ModelLevelExtendFileds';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;
            
            //Initialize Model Data
            /*self.selectedItem = null;
            self.viewerData = [];*/
            
            self.inputCode = "";

            //Initialize Model Data
            self.isButtonVisible = false;
            self.selectedItem = null;
            //左-新增
            self.is_addButtonHandler = true;
            //右-编辑
            //self.is_editButtonHandler = false;
            //左-删除
            //self.is_deleteButtonHandler = false;

            //右-新增
            /*self.is_addExtendInfoButtonHandler = false;
            
            //右-删除
            self.is_deleteExtendInfoButtonHandler = false;*/

            //Expose Model Methods
            self.viewerOptions = {};
            self.viewerData = [];
            self.selectedDataItemId = null;
            self.DataItemClass = commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_1')
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
               // console.log('changedCB');
                var i, j, r = [];
                for (i = 0, j = data.selected.length; i < j; i++) {
                    r.push(data.instance.get_node(data.selected[i]).id);
                }
                if (data.selected.length > 0) {
                    //左-新增
                    self.is_addButtonHandler = true;
                    //左-删除
                    //self.is_deleteButtonHandler = false;
                    //右-编辑
                    //self.is_editButtonHandler = false;

                    //右-新增
                    /*self.is_addExtendInfoButtonHandler = true;
                    
                    //右-删除
                    self.is_deleteExtendInfoButtonHandler = false;*/

                    getDataItemDetailList(data.selected[0]);
                }
                if (!!data.node) {
                    self.selectedDataItemId = data.node.id;
                    //console.log('data.node.id-------------: ' + data.node.id);
                    self.selectedDataItemIsTree = data.node.original.isTree;
                    self.DataItemClass = data.node.text;
                }
            };

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;
            $rootScope.$on('to-parent', function (event, OnData) {

                 $timeout(function () {
                    self.isButtonVisible = false;
                    getList(self.inputCode);
                }, 1500);                
            });
            $rootScope.$on('to-addparent', function (event, OnData) {
                $timeout(function () {
                    self.isButtonVisible = false;
                    getList(self.inputCode);
                }, 1500); 
               
            });
        }

        function initGridOptions() {
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
                paginationPageSizes: [20, 25, 30, 50, 75, 100], //每页显示个数选项
                paginationPageSize: 25, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮          
                //选中
                rowTemplate: "<div ng-dblclick=\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    }, {
                        field: 'LevelCode',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_3'),
                        width: 200
                    }, {
                        field: 'FieldCode',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_4'),
                        width: 200
                    }, {
                        field: 'FieldName',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_5'),
                        width: 200
                    }, {
                        field: 'FieldType',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_6'),
                        width: 200,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.FieldType==\'string\'"><span ng-cell-text class="">字符类型</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.FieldType==\'int\'"><span ng-cell-text class="green">整数类型</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.FieldType==\'datetime\'"><span ng-cell-text class="green">日期时间类型</span></div>'
                    }, {
                        field: 'IsReserve',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_7'),
                        width: 200,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.IsReserve==1"><span ng-cell-text class="">是</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.IsReserve!=1"><span ng-cell-text class="green">否</span></div>'
                    }, {
                        field: 'CreateUser',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_8'),
                        width: 200
                    }, {
                        field: 'CreateDate',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_9'),
                        width: 200,
                        cellFilter: 'date:\'yyyy-MM-dd\'',
                        filterCellFiltered: true
                    }, {
                        field: 'ModifyUser',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_10'),
                        width: 200
                    }, {
                        field: 'ModifyDate',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_11'),
                        width: 200,
                        cellFilter: 'date:\'yyyy-MM-dd\'',
                        filterCellFiltered: true
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData();
                    });
                    //行选中事件
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem = row;
                            setButtonsVisibility(true);
                            //左-新增
                            self.is_addButtonHandler = true;
                            //左-删除
                            //self.is_deleteButtonHandler = true;
                            //右-编辑
                            //self.is_editButtonHandler = true;
                            self.isButtonVisible = true;
                      } else {
                            self.selectedItem = null;
                            self.isButtonVisible = false;
                            //setButtonsVisibility(false);
                        }
                    });
                },
                data: []
            }
        }

        function initGridData() {
            var url = commonService.getMesApiAddress("factory") + "/LevelManage/BsModelLevelExtendFields/GetTreeJson";

            commonService.callWebApiGet(url, null).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    var treeData = [];
                    for (var i = 0; i < jsonData.length; i++) {
                        var opened = true;
                        var selected = false;
                        var nodeType = "default";
                        // if (jsonData[i].TreeLevel < 3) {
                        //     opened = true;
                        //     //nodeType="default";
                        // }
                        // else {
                        //     opened = false;
                        //     //nodeType="file";
                        // }
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
                                "isTree": jsonData[i].AttributeValue,
                                'state': { 'opened': opened, 'selected': selected }
                            }
                        );
                    }
                    $scope.treeModel = treeData;
                } else {
                    //self.gridOptions.data = [];
                }
            }, function (error) {;
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_12'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_13',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });



            getList(self.inputCode);
        }

        function getList(code){
            var url = commonService.getMesApiAddress("factory") + '/LevelManage/BsModelLevelExtendFields/GetPageListJson';
            var data = {
                pagination: {
                    rows: self.gridOptions.paginationPageSize,
                    page: self.gridOptions.paginationCurrentPage,
                    sidx: 'FieldCode',
                    sord: 'desc'
                },
                queryJson: {
                    LevelCode: code
                }
            };
            commonService.callWebApiPost(url, data).then(function (res) {
                if ((res) && (res.data.success)) {
                    /*var resultData = res.data.resultData.rows;
               
                    self.gridOptions.data = resultData;*/
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据            
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_12'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_13',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });

        }
        function getDataItemDetailList(itemId) {
            self.inputCode = itemId;
            getList(itemId);
        }


        function addButtonHandler(clickedCommand) {
            if (!self.inputCode) {
                notification.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_14'));
                return;
            }
            $state.go(rootstate + '.add', { code: self.inputCode });
        }

        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            if (!self.selectedItem) {
                notification.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_15'));
                return;
            }

            $state.go(rootstate + '.edit', { id: self.selectedItem, selectedItem: self.selectedItem.entity });
        }

        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function deleteButtonHandler(clickedCommand) {
            if (!self.selectedItem) {
                notification.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_15'));
                return;
            }

            var title = commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_16');
            // TODO: Put here the properties of the entity managed by the service
            var text = commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_17') + self.selectedItem.entity.FieldName + commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_18');

            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("factory") + "LevelManage/BsModelLevelExtendFields/DeleteForm?code=" + self.selectedItem.entity.id;
                var res = commonService.callWebApiGet(url).then(onDeleteSuccess, onDeleteError);
                self.isButtonVisible = false;
            }, title);
        }


        function onDeleteSuccess(data) {
            if (data.data.success) {
                //左-新增
                self.is_addButtonHandler = true;
                //左-删除
                //self.is_deleteButtonHandler = false;
                //右-编辑
                self.isButtonVisible = false;
                //$state.go(rootstate, {}, { reload: true });
                $timeout(function () {
                    getList(self.inputCode);
                }, 1500); 
            }
            else {
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_12'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_13',
                    text: '操作失败 - ' + data.data.returnMsg
                });
                messageservice.show();
            }
        }

        function onDeleteError(error) {
            messageservice.set({
                buttons: [{
                    id: 'ok',
                    displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_12'),
                    onClickCallback: function () {
                        messageservice.hide();
                    }
                }],
                title: 'Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_13',
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
        var moduleStateName = 'home.Siemens_SimaticIT_FactoryModelApp_ModelLevelExtendFileds';
        var moduleStateUrl = 'Siemens.SimaticIT_FactoryModelApp_ModelLevelExtendFileds';
        var moduleFolder = 'Siemens.SimaticIT.FactoryModelApp/modules/ModelLevelExtendFileds';

        var state = {
            name: moduleStateName + '_ModelLevelExtendFileds',
            url: '/' + moduleStateUrl + '_ModelLevelExtendFileds',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ModelLevelExtendFileds-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledslistctrl.Tips_20'
            }
        };
        $stateProvider.state(state);
    }
}());
