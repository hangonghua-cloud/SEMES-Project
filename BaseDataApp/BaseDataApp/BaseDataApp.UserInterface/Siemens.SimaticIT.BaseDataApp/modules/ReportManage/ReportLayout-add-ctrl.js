(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BaseDataApp.ReportManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayout.service', '$state', '$stateParams',
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

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutaddctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.selectedOrganize = [];
            self.currentItem = {
                ParentId: "0",
                Superior: "0"//菜单
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
                    label: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutaddctrl.Tips_2'),
                    checked: true
                }
            ];
            self.IsEnabledMark = isEnabledMark;


        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutaddctrl.Tips_3') });
            self.currentItem.EnabledMark = self.IsEnabledMark[0].checked ? 0 : 1;


            var postData = {
                KeyValue: '',
                Entity: self.currentItem
            };
            var url = commonService.getMesApiAddress() + 'BaseReport/SaveForm';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

        }
        function openSelectOrganizeDialog() {
            getOrganizeTreeData();
        }
        function showSelectOrganizeDialog() {
            var globalDialogService = c.globalDialog;//common.globalDialog;
            self.dialogButtons = [{
                id: $scope.$id + "_okButton",
                displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutaddctrl.Tips_4'),
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
                displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutaddctrl.Tips_5'),
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
                title: 'Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutaddctrl.Tips_6',
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
            var url = commonService.getMesApiAddress() + "/BaseReport/GetReportMeun";
            commonService.callWebApiPost(url, {}).then(function (data) {
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
                        displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutaddctrl.Tips_4'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutaddctrl.Tips_7',
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
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutaddctrl.Tips_8'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutaddctrl.Tips_9'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutaddctrl.Tips_9'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BaseDataApp_ReportManage_ReportLayout';
        var moduleFolder = 'Siemens.SimaticIT.BaseDataApp/modules/ReportManage';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ReportLayout-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.BaseDataApp.ReportManage.ReportLayoutaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
