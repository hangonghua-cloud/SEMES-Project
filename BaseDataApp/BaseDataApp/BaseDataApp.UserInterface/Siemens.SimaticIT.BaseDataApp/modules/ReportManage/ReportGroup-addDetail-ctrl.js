(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BaseDataApp.ReportManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroup.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            initTreeData();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupaddDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Expose Model Methods
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.save = save;
            self.cancel = cancel;
            self.selectValue = "";
            //Initialize Model Data
            self.selectedItem = null;
            self.nodes = [];


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
                self.nodes = [];
                var i, j, r = [];

                for (i = 0, j = data.selected.length; i < j; i++) {
                    r.push(data.instance.get_node(data.selected[i]).id);
                }
                if (data.selected.length > 0) {
                    self.nodes = data.selected;
                }
                if (!!data.node) {

                    // self.selectedDataItemId = data.node.id;

                    // self.selectedDataItemIsTree = data.node.original.isTree;

                    // self.DataItemClass = data.node.text;
                    // self.DataItemClassValue = data.node.original.value;

                }
            };


        }
        //Tree选择
        function treeSelectedNode(node, selected, event) {
            self.selectedItem = selected.node.original;

        };
        //加载Tree
        function initTreeData() {
            var postdata = {
                Id: self.currentItem.Id
            }
            var url = commonService.getMesApiAddress() + "/BaseReportRole/GetAllMenuWithChecked";
            commonService.callWebApiPost(url, postdata).then(function (res) {
                if ((res) && (res.data.success)) {
                    var jsonData = res.data.resultData;

                    var treeData = [];
                    for (var i = 0; i < jsonData.length; i++) {
                        var opened = false;
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
                        if (jsonData[i].checkstate == 1) {
                            selected = true;
                            //self.DataItemClass = jsonData[i].text;
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
                backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupaddDetailctrl.Tips_2'));
            });
        }


        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }


        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupaddDetailctrl.Tips_3') });
            //字典类型 取值参考

            if (self.nodes.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupaddDetailctrl.Tips_4'), commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupaddDetailctrl.Tips_2'));
                return
            }

            var array = []

            $.each(self.nodes, function (index, item) {
                array.push({
                    GroupId: self.currentItem.Id,
                    ReportId: item
                })
            })


            var postData = {
                KeyValue: self.currentItem.Id,
                data: array
            };

            var url = commonService.getMesApiAddress() + 'BaseReportRole/SaveBatchForm';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        //取消
        function cancel() {
            //关闭侧边栏
            sidePanelManager.close();
            //返回列表(父页面)
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {

            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupaddDetailctrl.Tips_5'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupaddDetailctrl.Tips_2'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupaddDetailctrl.Tips_2'));
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BaseDataApp_ReportManage_ReportGroup';
        var moduleFolder = 'Siemens.SimaticIT.BaseDataApp/modules/ReportManage';

        var state = {
            name: screenStateName + '.addDetail',
            url: '/addDetail',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ReportGroup-addDetail.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.BaseDataApp.ReportManage.ReportGroupaddDetailctrl.Tips_1'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
