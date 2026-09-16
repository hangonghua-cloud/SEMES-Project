(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.BSTraitManage').config(AddDetailScreenStateConfig);

    AddDetailScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.BSTraitManage.BS_TraitManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function AddDetailScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth,
        notificationService, busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddAttrctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            self.validInputs = false;
            self.selectedItem = angular.copy($stateParams.selectedItem);
            self.currentItem = {};

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.AttrChange = AttrChange;

            initDictionary();
            GetUserInfo();
        }

        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        function initDictionary() {
            // 属性模板 
            self.typeAttr = {
                options: [
                    { AttrCode: "", AttrName: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddAttrctrl.Tips_2') }
                ],
                value: { AttrCode: "", AttrName: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddAttrctrl.Tips_2') }
            }
            let queryParmeters = {
                queryJson: {
                    TraitDetailId: self.selectedItem.Id
                }
            };

            var url = commonService.getMesApiAddress("material") + 'BS_TraitDetailsAttr/BS_TraitDetailsAttrPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (resFacet) {
                if ((resFacet) && (resFacet.data.success)) {
                    var facetData = resFacet.data.resultData.rows;
                    // debugger
                    var TempCode = self.selectedItem.AttrModleCode;

                    var url1 = commonService.getMesApiAddress("material") + 'Base_MaterialBindTempFacet/GetBase_CodeMaterialBindTempFacetList?checkType=' + TempCode;
                    var req = commonService.callWebApiGet(url1, null).then(function (res) {
                        if (res && res.data.success) {
                            res.data.resultData.forEach(item => {
                                if (!facetData.find(t => t.AttrCode == item.AttrCode)) {
                                    self.typeAttr.options.push({
                                        AttrCode: item.AttrCode,
                                        AttrName: item.AttrName,
                                        AttrType: item.AttrType
                                    })
                                }
                            });

                        } else {
                            self.gridOptionsItem.data = []
                        }
                    });
                } else {
                    self.gridOptionsDetail.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddAttrctrl.Tips_3'));
            });


            self.typeDataType = {
                options: [],
                value: {}
            }
            commonService.getDataItemDuatil("AttrType").then(function (res) {
                if (res && res.data.success) {
                    self.typeDataType.options = res.data.resultData;
                }
            })
        }

        function AttrChange(oldval, newval) {
            if (!!newval) {
                self.typeDataType.value = self.typeDataType.options.find(t => t.ItemValue == newval.AttrType);
                self.currentItem.AttrType = newval.AttrType;
            }
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        //保存
        function save() {
            self.currentItem.CreatorName = self.UserName;
            self.currentItem.Creator = self.UserCode;
            if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                self.currentItem.CreatorName = self.UserCode;
            }
            if (self.currentItem.AttrType == "3") {
                var dete = $filter('date')(new Date(self.currentItem.AttrValue), 'yyyy-MM-dd HH:mm:ss');
                self.currentItem.AttrValue = dete;
            }

            self.currentItem.AttrCode = self.typeAttr.value.AttrCode;
            self.currentItem.AttrName = self.typeAttr.value.AttrName;
            self.currentItem.AttrType = self.typeDataType.value.ItemValue;
            self.currentItem.TraitDetailId = self.selectedItem.Id;

            var postData = {
                KeyValue: null,
                Entity: self.currentItem
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddAttrctrl.Tips_4') });
            var url = commonService.getMesApiAddress("material") + 'BS_TraitDetailsAttr/SaveBS_TraitDetailsAttr';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddAttrctrl.Tips_5'));
                //刷新局部
                $rootScope.$emit('to-parentAttr', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddAttrctrl.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddAttrctrl.Tips_6'));
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddDetailScreenStateConfig.$inject = ['$stateProvider'];
    function AddDetailScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_BSTraitManage_BS_TraitManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/BSTraitManage';

        var state = {
            name: screenStateName + '.addAttr',
            url: '/addAttr',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/BSTraitManage-addAttr.html',
                    controller: AddDetailScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddAttrctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
