(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.FactoryModelApp.ModelManage').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.FactoryModelApp.ModelManage.ModelManage.service', '$state', '$stateParams', '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'commonService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, commonService) {
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.FactoryModelApp.ModelManage.ModelManage');

            init();
            initGridOptions();
            initGridData();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_FactoryModelApp_ModelManage_ModelManage';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.SearchParams={};

            self.selectedDataItemId = null;
            self.CurrentItem={};
            self.DataItemClass = commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelManage.ModelManagelistctrl.Tips_1')
            $scope.changedCB = function (e, data) {
                var i, j, r = [];
                for (i = 0, j = data.selected.length; i < j; i++) {
                    r.push(data.instance.get_node(data.selected[i]).id);
                }
                if (data.selected.length > 0) {
                    getDataItemDetailList(data.selected[0]);
                }
                if (!!data.node) {
                    self.selectedDataItemId = data.node.id;
                    console.log('data.node.id-------------: ' + data.node.id);
                    self.selectedDataItemIsTree = data.node.original.isTree;
                    self.DataItemClass = data.node.text;
                }
            };

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.selectButtonHandler = selectButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;

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
                console.log('changedCB');
                var i, j, r = [];
                for (i = 0, j = data.selected.length; i < j; i++) {
                    r.push(data.instance.get_node(data.selected[i]).id);
                }
                // if (data.selected.length > 0) {

                //     getDataItemDetailList(data.selected[0]);
                // }
                if (!!data.node) {
                    self.selectedDataItemId = data.node.id;
                    console.log('data.node.id-------------: ' + data.node.id);
               
                    self.CurrentItem.ResourceCode=data.node.id
                    self.CurrentItem.LevelCode=data.node.original.isTree
                    console.log(self.CurrentItem);

                    self.selectedDataItemIsTree = data.node.original.isTree;
                    self.DataItemClass = data.node.text;
                    getDataItemDetailList(data.selected[0]);
                }
            };
        }

        function initGridOptions() {
            self.gridOptions = {
              
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelManage.ModelManagelistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    }, {
                        field: 'FieldName',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelManage.ModelManagelistctrl.Tips_3'),
                        width: 200
                    }, {
                        field: 'FieldValue',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelManage.ModelManagelistctrl.Tips_4'),
                        width: 200
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row.isSelected) {
                            self.selectedDetail = row.entity;
                            console.log(self.selectedDetail);
                        }
                    });
                },
                data: []
            }
        }

        function initGridData() {
            //var url = commonService.getMesApiAddress() + "/SystemManage/DataItem/GetTreeJson";
            //debugger;
            var url = commonService.getMesApiAddress('factory') + "level/GetTreeJson";

            commonService.callWebApiGet(url, null).then(function (data) {
               if ((data) && (data.data.success)) {
                   var jsonData = data.data.resultData;
                   console.log(jsonData);
                   var treeData = [];
                   for (var i = 0; i < jsonData.length; i++) {
                       var opened = false;
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
                       displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelManage.ModelManagelistctrl.Tips_5'),
                       onClickCallback: function () {
                           messageservice.hide();
                       }
                   }],
                   title: 'Siemens.SimaticIT.FactoryModelApp.ModelManage.ModelManagelistctrl.Tips_6',
                   text: '[' + error.status + '] - ' + error.data.returnMsg
               });
               messageservice.show();
            });
        }

        function getDataItemDetailList() {
            console.log(self.CurrentItem);
              var SearchParams=
             {
                "ResourceCode":self.CurrentItem.ResourceCode,
                "LevelCode":self.CurrentItem.LevelCode
            };
            console.log("---------------------"+JSON.stringify(SearchParams));

            var url = commonService.getMesApiAddress("factory") + 'level/Get_FieldData';
            commonService.callWebApiPost(url, SearchParams).then(function (res) {
                if ((res) && (res.data.success)) {
                    var resultData = res.data.resultData;
                    self.gridOptions.data = resultData;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelManage.ModelManagelistctrl.Tips_5'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.FactoryModelApp.ModelManage.ModelManagelistctrl.Tips_6',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });
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
        var moduleStateName = 'home.Siemens_SimaticIT_FactoryModelApp_ModelManage';
        var moduleStateUrl = 'Siemens.SimaticIT_FactoryModelApp_ModelManage';
        var moduleFolder = 'Siemens.SimaticIT.FactoryModelApp/modules/ModelManage';

        var state = {
            name: moduleStateName + '_ModelManage',
            url: '/' + moduleStateUrl + '_ModelManage',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ModelManage-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.FactoryModelApp.ModelManage.ModelManagelistctrl.Tips_7'
            }
        };
        $stateProvider.state(state);
    }
}());
