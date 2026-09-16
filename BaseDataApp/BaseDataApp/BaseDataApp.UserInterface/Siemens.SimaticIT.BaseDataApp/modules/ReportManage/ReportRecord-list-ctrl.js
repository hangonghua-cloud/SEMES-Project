(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BaseDataApp.ReportManage').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.BaseDataApp.ReportManage.ReportRecord.service', '$state',
        '$stateParams', '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'uiGridConstants', 'i18nService', '$http',
        'common.widgets.notificationTile.globalService', 'common.services.authentication', 'commonService', '$window', '$sce'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base,
        loggerService, uiGridConstants, i18nService, $http, notification, $auth, commonService, $window, $sce) {
        var self = this;
        var logger, rootstate, messageservice, backendService, rootstateDataItemDetail;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportRecord');

            init();
            initTreeData();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_BaseDataApp_ReportManage_ReportRecord';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.menuShow = true;
            //self.dataselected = "";
            self.selectValue = "";
            //Initialize Model Data
            self.selectedItem = null;
            self.hidden = hidden;
            self.hiddenName = commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportRecordlistctrl.Tips_1')

            self.viewerOptions = {};
            self.viewerData = [];
            self.selectedDataItemId = null;
            self.DataItemClass = commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportRecordlistctrl.Tips_2')
            self.DataItemClassValue = "";
            self.factoryCode = "";
            self.factoryName = "";

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

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.factoryCode = res.data.resultData.map(item => {
                        return item.ResourceCode
                    }).join(",");
                    self.factoryName = res.data.resultData.map(item => {
                        return item.ResourceName
                    }).join(",");
                }
                console.log(commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportRecordlistctrl.Tips_3'), self.factoryCode);
                console.log(commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportRecordlistctrl.Tips_4'), self.factoryName);
            });

            $scope.changedCB = function (e, data) {

                //self.src="http://172.168.11.133:9000/mi/#/statementPreview?sid=614"
                if (!!data.node && !!data.node.original.value) {
                    //console.log(data.node.original.value);
                    var url = data.node.original.value;
                    url += "&FactoryCode=" + self.factoryCode + "&FactoryName=" + self.factoryName;
                    console.log(url);
                    var ifr = document.getElementById("iframe");
                    ifr.innerHTML = "";
                    ifr.innerHTML = ' <iframe class="report-iframe" width="100%" height="100%" seamless frameBorder="0"' +
                        'ng-src="' + url + '"' +
                        'src="' + url + '"' +
                        '></iframe>'
                    //$scope.src = $sce.trustAsResourceUrl(data.node.original.value);

                }
            };
        }

        function hidden() {
            var doc = document.getElementById("itemClass");
            doc.removeAttribute("class");
            self.menuShow = !self.menuShow;
            if (self.menuShow == false) {
                //隐藏左菜单
                self.hiddenName = commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportRecordlistctrl.Tips_5')
                doc.setAttribute("class", "col-xs-40 col-md-40")
                document.getElementById("boxTree").setAttribute("class", "col-xs-0 col-md-0")
            } else {
                self.hiddenName = commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportRecordlistctrl.Tips_1')
                doc.setAttribute("class", "col-xs-34 col-md-34")
                document.getElementById("boxTree").setAttribute("class", "col-xs-6 col-md-6")
            }
        }

        // 加载树
        function initTreeData() {

            var url = commonService.getMesApiAddress() + "BaseReport/GetReportTree";
            var postData = {
                UserCode: "admin"
            }
            commonService.callWebApiPost(url, postData).then(function (res) {
                if ((res) && (res.data.success)) {
                    var jsonData = res.data.resultData;

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
                    backendService.genericError(res.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportRecordlistctrl.Tips_6'));
                }
            }, function (error) {
                backendService.genericError(res.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportRecordlistctrl.Tips_6'));
            });
        }
        self.gridStyleHeight = function () {
            return { height: ($window.innerHeight - 75) + "px" };
        }

        function getDataItemDetailList(url) {
            console.log(url);
        }

        function search() {
            if (self.searchCode.length > 0) {
                self.treeInstance.jstree(true).search(self.searchCode);
            }
        }
        function treeSelectedNode(node, selected, event) {
            console.log(selected);
            self.selectedItem = selected.node.original;
            initGridData();
        };



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
        var moduleStateName = 'home.Siemens_SimaticIT_BaseDataApp_ReportManage';
        var moduleStateUrl = 'Siemens.SimaticIT_BaseDataApp_ReportManage';
        var moduleFolder = 'Siemens.SimaticIT.BaseDataApp/modules/ReportManage';

        var state = {
            name: moduleStateName + '_ReportRecord',
            url: '/' + moduleStateUrl + '_ReportRecord',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ReportRecord-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.BaseDataApp.ReportManage.ReportRecordlistctrl.Tips_7'
            }
        };
        $stateProvider.state(state);
    }
}());
