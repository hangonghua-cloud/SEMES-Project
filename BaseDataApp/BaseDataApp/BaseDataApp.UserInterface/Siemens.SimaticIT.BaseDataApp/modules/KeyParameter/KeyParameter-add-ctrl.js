(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BaseDataApp.KeyParameter').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameter.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', 'common'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope,
        commonService, auth, notificationService, busyIndicatorService, $modal, c) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {
                ParentId: "0"
            };
            self.validInputs = false;
            initDictionary();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.openSelectOrganizeDialog = openSelectOrganizeDialog;

        }
        function initDictionary() {
            var isEnabledMark = [
                {
                    label: commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddctrl.Tips_2'),
                    checked: true
                }
            ];
            self.IsEnabledMark = isEnabledMark;

            self.IsRepetition = [
                {
                    label: commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddctrl.Tips_3'),
                    checked: false
                }
            ];

        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddctrl.Tips_4') });
            self.currentItem.IsEnabled = self.IsEnabledMark[0].checked ? 1 : 0;
            self.currentItem.IsRepetition = self.IsRepetition[0].checked ? 1 : 0;


            var postData = {
                KeyValue: '',
                Entity: self.currentItem
            };
            var url = commonService.getMesApiAddress() + 'Base_KeyParameter/SaveBase_KeyParameter';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

        }
        function openSelectOrganizeDialog() {
            getOrganizeTreeData();
        }
        function showSelectOrganizeDialog() {
            var globalDialogService = c.globalDialog;//common.globalDialog;
            self.dialogButtons = [{
                id: $scope.$id + "_okButton",
                displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddctrl.Tips_5'),
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
                displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddctrl.Tips_6'),
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
                title: 'Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddctrl.Tips_7',
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
            var url = commonService.getMesApiAddress() + "/Base_KeyParameter/GetTreeJson";

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
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddctrl.Tips_5'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddctrl.Tips_8',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });

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
            console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddctrl.Tips_9'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddctrl.Tips_10'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddctrl.Tips_10'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BaseDataApp_KeyParameter_KeyParameter';
        var moduleFolder = 'Siemens.SimaticIT.BaseDataApp/modules/KeyParameter';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/KeyParameter-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddctrl.Tips_11'
            }
        };
        $stateProvider.state(state);
    }
}());
