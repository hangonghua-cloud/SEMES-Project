(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.FactoryModelApp.ModelResource').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResource.service',
        '$state', '$stateParams', '$rootScope', '$scope', 'common.base',
        'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService', '$timeout'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notification, commonService, $timeout) {
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResource');

            init();
            initGridOptions();
            initGridData();
            initGridOptions2();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_FactoryModelApp_ModelResource_ModelResource';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            self.inputCode = "";

            //Initialize Model Data
            self.selectedItem = null;
            self.selectedDetail = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};
            self.selectedDataItemId = null;
            self.currentLevlel = null;
            self.DataItemClass = commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_1')
            if (!!$stateParams.itemId) {

                self.selectedDataItemId = $stateParams.itemId;
                getDataItemDetailList(self.selectedDataItemId);
            }

            $scope.changedCB = function (e, data) {
                console.log('changedCB');
                var i, j, r = [];
                for (i = 0, j = data.selected.length; i < j; i++) {
                    r.push(data.instance.get_node(data.selected[i]).id);
                }
                if (data.selected.length > 0) {
                    self.currentLevlel = data.selected[0];
                    getDataItemDetailList(self.currentLevlel);
                    self.DataItemDetail.data = [];
                }
                if (!!data.node) {
                    self.selectedDataItemId = data.node.id;
                    console.log('data.node.id-------------: ' + data.node.id);
                    self.selectedDataItemIsTree = data.node.original.isTree;
                    self.DataItemClass = data.node.text;
                }
            };

            //Expose Model Methods
            self.addDataItemDetailButtonHandler = addButtonHandler;
            self.editDataItemDetailButtonHandler = editButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteDataItemDetailButtonHandler = deleteButtonHandler;
            self.editValueButtonHandler = editValueButtonHandler;
            self.deleteValueButtonHandler = deleteValueButtonHandler;
            self.searchButtonHandler = searchButtonHandler;
            $rootScope.$on('to-parent', function (event, OnData) {

                $timeout(function () {
                    getDetailList(self.selectedItem.ResourceCode, self.selectedItem.ModelLeve);
                }, 1000);
            });
            $rootScope.$on('to-addparent', function (event, OnData) {
                $timeout(function () {
                    getDataItemDetailList(self.currentLevlel);
                }, 1000);

            });
        }
        function initGridOptions() {
            self.gridOptions = {

                fastWatch: true,
                rowHeight: 35,
                minimumColumnSize: 100,
                enableMultiSelection: false,
                enableFiltering: true,
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
                paginationPageSizes: [200, 250, 300], //每页显示个数选项
                paginationPageSize: 200, //每页显示个数
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
                        name: 'Index', displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_2'), enableFiltering: false, width: 75,
                        cellTemplate: '<div calss="ui-grid-cell-contents" style="padding:5px">{{rowRenderIndex+1}}</div>'
                    }, , {
                        field: 'ResourceCode',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_3'),
                        width: 140
                    }, {
                        field: 'ResourceName',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_4'),
                        width: 140
                    }, {
                        field: 'ModelLeve',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_5'),
                        width: 140
                    }, {
                        field: 'ParentResource',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_6'),
                        width: 140
                    }, {
                        field: 'SortCode',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_7'),
                        width: 140
                    }, {
                        field: 'Describe',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_8'),
                        width: 200
                    }, {
                        field: 'CreateUser',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_9'),
                        width: 140
                    }, {
                        field: 'CreateDate',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_10'),
                        width: 200,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'
                    }, {
                        field: 'ModifyUser',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_11'),
                        width: 140
                    }, {
                        field: 'ModifyDate',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_12'),
                        width: 200,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        getList(self.inputCode);
                    });
                    //行选中事件
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row.isSelected) {
                            self.selectedItem = row.entity;

                            getDetailList(self.selectedItem.ResourceCode, self.selectedItem.ModelLeve);
                            self.is_addButtonHandler = true;
                        }
                    });
                },
                data: []
            }
        }

        function initGridOptions2() {
            self.DataItemDetail = {
                enableRowSelection: true,
                fastWatch: true,
                enableSelectAll: false,
                selectionRowHeaderWidth: 35,
                rowHeight: 35,
                paginationPageSizes: [20, 50, 75],
                paginationPageSize: 20,
                paginationCurrentPage: 1,
                minimumColumnSize: 100,
                enableFullRowSelection: true,
                multiSelect: false,
                useExternalPagination: true,
                enablePagination: true,
                enableMultiSelection: false,
                enableFiltering: true,
                appScopeProvider: self,
                totalItems: self.totalItems,
                columnDefs: [
                    /*
                    FieldCode: "WorkshopNumber"
                    FieldName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_13')
                    FieldType: "string"
                    FieldValue: null
                    ResourceCode: "DZH"
                    */
                    {
                        name: 'Index', displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_2'), enableFiltering: false, width: 75,
                        cellTemplate: '<div calss="ui-grid-cell-contents" style="padding:5px">{{rowRenderIndex+1}}</div>'
                    }, {
                        field: 'ResourceCode',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_14'),
                        visible: false,
                        width: 200
                    }, {
                        field: 'FieldCode',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_15'),
                        visible: false,
                        width: 200
                    }, {
                        field: 'FieldName',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_16'),
                        width: 200
                    }, {
                        field: 'FieldType',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_17'),
                        width: 200,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.FieldType==\'string\'"><span ng-cell-text class="">字符类型</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.FieldType==\'int\'"><span ng-cell-text class="green">数字类型</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.FieldType==\'datetime\'"><span ng-cell-text class="green">日期类型</span></div>'
                    }, {
                        field: 'FieldValue',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_18'),
                        width: 200
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row.isSelected) {
                            self.selectedDetail = row.entity;
                            console.log(self.selectedDetail);
                            //alert(self.selectedDetail.DocNo);
                            //initGridDataBOMGrder(self.selectedDetail.ProductionOrderNO);
                            //self.is_addButtonHandler = true;
                            //initGridDataGrder(self.selectedDetail.DocNo);
                            // console.log(row.entity);
                        }
                    });
                },
                data: []
            };
        }

    function searchButtonHandler()
      {
        getList(self.inputCode);

       }       


        function initGridData() {
            //var url = commonService.getMesApiAddress() + "/SystemManage/DataItem/GetTreeJson";
            var url = commonService.getMesApiAddress("factory") + "/LevelManage/BsModelLevelExtendFields/GetTreeJson";
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
                                "isTree": jsonData[i].AttributeValue,
                                'state': { 'opened': opened, 'selected': selected }
                            }
                        );
                    }
                    // console.log('-----------------------');
                    // console.log(treeData);
                    $scope.treeModel = treeData;
                } else {
                    //self.gridOptions.data = [];
                }
            }, function (error) {
                // console.log('-----------error------------');
                // console.log(error);
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_19'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_20',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });

            getList("");
        }

        function getList(code) {

             var aa =self.searchParams.ResourceCode;
            var url = commonService.getMesApiAddress("factory") + '/LevelManage/BsModelWithResource/GetPageListJson';
            var data = {
                pagination: {
                    rows: self.gridOptions.paginationPageSize,
                    page: self.gridOptions.paginationCurrentPage,
                    sidx: 'SortCode',
                    sord: 'asc'
                },
                queryJson: {
                    Level: code,
                 ResourceCode:aa
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
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_19'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_20',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });

        }
        function getDetailList(recCode, level) {
            var url = commonService.getMesApiAddress("factory") + '/level/Get_FieldData2';
            var queryJson = {
                ResourceCode: recCode,
                LevelCode: level
            };
            commonService.callWebApiPost(url, queryJson).then(function (res) {
                if ((res) && (res.data.success)) {

                    var resultData = res.data.resultData;

                    self.DataItemDetail.data = resultData;

                } else {
                    self.DataItemDetail.data = [];
                }
                console.log(self.DataItemDetail.data);
            }, function (error) {
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_19'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_20',
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
                notification.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_21'));
                return;
            }
            $state.go(rootstate + '.add', { code: self.inputCode });
        }

        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            if (!self.selectedItem) {
                notification.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_22'));
                return;
            }

            $state.go(rootstate + '.edit', { id: self.selectedItem.ResourceCode, selectedItem: self.selectedItem });
        }

        function editValueButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            if (!self.selectedDetail) {
                notification.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_23'));
                return;
            }

            //$state.go(rootstate + '.editDetail', { id: self.selectedDetail.Id, selectedItem: self.selectedDetail });
            $state.go(rootstate + '.editDetail', { id: self.selectedDetail.FieldCode, selectedItem: self.selectedDetail });
        }

        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function deleteButtonHandler(clickedCommand) {
            if (!self.selectedItem) {
                notification.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_22'));
                return;
            }
            var title = commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_24');
            // TODO: Put here the properties of the entity managed by the service
            var text = commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_25') + self.selectedItem.ResourceCode + commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_26');

            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("factory") + "LevelManage/BsModelWithResource/DeleteForm?code=" + self.selectedItem.ResourceCode;
                commonService.callWebApiGet(url).then(onDeleteSuccess, onDeleteError);

            }, title);
        }

        function deleteValueButtonHandler(clickedCommand) {
            if (!self.selectedDetail) {
                notification.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_23'));
                return;
            }
            var title = commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_24');
            // TODO: Put here the properties of the entity managed by the service
            var text = commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_25') + self.selectedDetail.Id + commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_26');

            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress("factory") + "LevelManage/BsModelResourceExtendInfo/DeleteForm?code=" + self.selectedDetail.Id;
                commonService.callWebApiGet(url).then(onDeleteSuccess, onDeleteError);

            }, title);
        }
        function onDeleteSuccess(data) {
            // alert('onDeleteSuccess1'); alert(self.selectedItem.ModelLeve);
            $timeout(function () {
                getDataItemDetailList(self.currentLevlel);
            }, 1500);

        }
        function onDeleteError(data) {
            getDetailList(self.selectedItem.ResourceCode, self.selectedItem.ModelLeve);

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
        var moduleStateName = 'home.Siemens_SimaticIT_FactoryModelApp_ModelResource';
        var moduleStateUrl = 'Siemens.SimaticIT_FactoryModelApp_ModelResource';
        var moduleFolder = 'Siemens.SimaticIT.FactoryModelApp/modules/ModelResource';

        var state = {
            name: moduleStateName + '_ModelResource',
            url: '/' + moduleStateUrl + '_ModelResource',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ModelResource-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourcelistctrl.Tips_27'
            }
        };
        $stateProvider.state(state);
    }
}());
