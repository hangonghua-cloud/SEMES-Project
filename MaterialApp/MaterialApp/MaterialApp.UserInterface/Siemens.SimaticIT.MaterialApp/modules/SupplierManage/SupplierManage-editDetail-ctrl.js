(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.SupplierManage').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', "common"];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, c) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;


        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);

            self.validInputs = false;
            self.organizeTreeData = [];
            self.selectedOrganize = [];

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.openSelectOrganizeDialog = openSelectOrganizeDialog;
            GetUserInfo();
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }


        function openSelectOrganizeDialog() {
            getOrganizeTreeData();
        }
        function showSelectOrganizeDialog() {
            var globalDialogService = c.globalDialog;//common.globalDialog;
            self.dialogButtons = [{
                id: $scope.$id + "_okButton",
                displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditDetailctrl.Tips_2'),
                onClickCallback: function () {
                    if (self.selectedOrganize.length > 0) {
                        // self.currentItem.ParentId = self.selectedOrganize[0].id;
                        self.currentItem.GroupCode = self.selectedOrganize[0].value;
                        self.currentItem.GroupName = self.selectedOrganize[0].text;
                    }
                    globalDialogService.hide();
                },
                disabled: false
            }, {
                id: $scope.$id + "_cancelButton",
                displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditDetailctrl.Tips_3'),
                onClickCallback: function () {
                    globalDialogService.hide();
                },
                disabled: false
            }];

            var changedCB = function (e, data) {
                self.selectedOrganize.length = 0;
                self.selectedOrganize.push({
                    id: data.node.original.id,
                    text: data.node.text,
                    value: data.node.original.value
                });
            };

            var dialogData = {
                title: 'Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditDetailctrl.Tips_4',
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
                                "value": jsonData[i].value,
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
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditDetailctrl.Tips_2'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditDetailctrl.Tips_5',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });
        }

        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        //保存
        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditDetailctrl.Tips_6') });

            if (!self.currentItem.GroupCode || self.currentItem.GroupCode.length < 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditDetailctrl.Tips_7'), commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditDetailctrl.Tips_8'));
                busyIndicatorService.hide();
                return;
            }

            var postData = {
                KeyValue: self.currentItem.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("material") + 'Base_SupplierBindMaterialGroup/SaveBase_SupplierBindMaterialGroup';

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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditDetailctrl.Tips_9'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditDetailctrl.Tips_8'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditDetailctrl.Tips_8'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_SupplierManage_SupplierManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/SupplierManage';

        var state = {
            name: screenStateName + '.editDetail',
            url: '/editDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/SupplierManage-editDetail.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditDetailctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
