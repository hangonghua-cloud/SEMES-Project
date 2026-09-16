(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MaterialGroup').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroup.service', '$state', '$stateParams',
        'common.base', '$filter', '$rootScope', '$scope', 'common', 'common.widgets.notificationTile.globalService', '$http', 'common.services.authentication', 'commonService'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $rootScope, $scope, c, notification, $http, $auth, commonService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler, messageservice;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            self.currentItem = {};
            self.validInputs = false;

            self.organizeTreeData = [];
            self.selectedOrganize = [];


            self.IsEnabled = [
                {
                    label: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddctrl.Tips_2'),
                    checked: true
                }
            ];

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.openSelectOrganizeDialog = openSelectOrganizeDialog;
        }



        function openSelectOrganizeDialog() {
            getOrganizeTreeData();
        }
        function showSelectOrganizeDialog() {
            var globalDialogService = c.globalDialog;//common.globalDialog;
            self.dialogButtons = [{
                id: $scope.$id + "_okButton",
                displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddctrl.Tips_3'),
                onClickCallback: function () {
                    if (self.selectedOrganize.length > 0) {
                        self.currentItem.ParentId = self.selectedOrganize[0].id;
                        self.currentItem.ParentName = self.selectedOrganize[0].text;
                    }
                    globalDialogService.hide();
                },
                disabled: false
            }, {
                id: $scope.$id + "_cancelButton",
                displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddctrl.Tips_4'),
                onClickCallback: function () {
                    globalDialogService.hide();
                },
                disabled: false
            }];

            var changedCB = function (e, data) {
                self.selectedOrganize.length = 0;
                self.selectedOrganize.push({
                    id: data.node.original.id,
                    text: data.node.text
                });
            };

            var dialogData = {
                title: 'Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddctrl.Tips_5',
                templatedata: {
                    treeModel: self.organizeTreeData,
                    treeTypes: {
                        "#": {
                            "valid_children": ["root"]
                        },
                        "root": {
                            "icon": "fa fa-folder",
                            "valid_children": ["default"]
                        },
                        "default": {
                            "icon": 'fa fa-folder',
                            "valid_children": ["default", "file"]
                        },
                        "file": {
                            "icon": 'fa fa-file-o',
                            "valid_children": []
                        }
                    },
                    changedCB: changedCB
                },
                templateuri: 'CCS.CommonApp/widgets/jstree/tree-dialog-template.html',
                buttons: self.dialogButtons
            }
            globalDialogService.set(dialogData);
            globalDialogService.show();

        }

        function getOrganizeTreeData() {
            var url = commonService.getMesApiAddress("material") + "Base_MaterialGroup/GetTreeJson";

            commonService.callWebApiGet(url, null).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    var treeData = [];
                    for (var i = 0; i < jsonData.length; i++) {
                        var opened = true;
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
                        treeData.push(
                            {
                                "id": jsonData[i].id,
                                "parent": parentId,
                                "text": jsonData[i].text,
                                "type": nodeType,
                                'state': { 'opened': opened, 'selected': false }
                            }
                        );
                    }
                    // console.log('-----------------------');
                    // console.log(treeData);
                    self.organizeTreeData = treeData;
                } else {
                    self.organizeTreeData = [];
                }
                showSelectOrganizeDialog();
            }, function (error) {
                // console.log('-----------error------------');
                // console.log(error);
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddctrl.Tips_3'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddctrl.Tips_6',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });

        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {

            self.currentItem.IsTree = 0;
            self.currentItem.IsEnabled = self.IsEnabled[0].checked ? 1 : 0;
            if (typeof self.currentItem.ParentId == "undefined" || !self.currentItem.ParentName) {
                self.currentItem.ParentId = "0";
            }
            // console.log('-----------self.currentItem------------');
            // console.log(self.currentItem);
            //dataService.create(self.currentItem).then(onSaveSuccess, backendService.backendError);
            var postData = {
                username: $auth.getUser().unique_name,
                keyValue: "",
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("material") + "Base_MaterialGroup/SaveForm";
            commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            if (data.data.success) {
                $rootScope.$emit('to-parent', 'parent');
                sidePanelManager.close();
                notification.warning(data.data.returnMsg);
                $state.go('^', {}, { reload: false });
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddctrl.Tips_7'));
            }
        }

        function onSaveError(error) {
            messageservice.set({
                buttons: [{
                    id: 'ok',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddctrl.Tips_3'),
                    onClickCallback: function () {
                        messageservice.hide();
                    }
                }],
                title: 'Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddctrl.Tips_6',
                text: '[' + error.status + '] - ' + error.data.returnMsg
            });
            messageservice.show();
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MaterialGroup_MaterialGroup';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MaterialGroup';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/MaterialGroup-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
