(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MaterialMain').config(AddDetailScreenStateConfig);

    AddDetailScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterial.service', '$state', '$stateParams',
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

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddDetailctrl.Tips_1'));
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
        }


        function initDictionary() {
            // 属性模板 
            self.typeAttr = {
                options: [
                    { AttrCode: "", AttrName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddDetailctrl.Tips_2') }
                ],
                value: { AttrCode: "", AttrName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddDetailctrl.Tips_2') }
            }
            let queryParmeters = {
                queryJson: {
                    MaterialFactoryId: self.selectedItem.Id
                }
            };

            var url = commonService.getMesApiAddress("material") + 'Base_MaterialFacet/Base_MaterialFacetPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (resFacet) {
                if ((resFacet) && (resFacet.data.success)) {
                    var facetData = resFacet.data.resultData.rows;

                    let templateCode = self.selectedItem.TemplateCode;
                    var url1 = commonService.getMesApiAddress("material") + 'Base_MaterialBindTempFacet/GetBase_MaterialBindTempFacetList?checkType=' + templateCode;
                    var req = commonService.callWebApiGet(url1, null).then(function (res) {
                        if (res && res.data.success) {

                            res.data.resultData.forEach(item => {
                                if (facetData.find(t => t.AttrCode == item.AttrCode) == null) {
                                    self.typeAttr.options.push({
                                        AttrCode: item.AttrCode,
                                        AttrName: item.AttrName,
                                        AttrType: item.AttrType
                                    })
                                }
                            });
                        }
                    });
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddDetailctrl.Tips_3'));
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
            if (self.currentItem.AttrType == "3") {
                var dete = $filter('date')(new Date(self.currentItem.AttrValue), 'yyyy-MM-dd HH:mm:ss');
                self.currentItem.AttrValue = dete;
            }

            self.currentItem.AttrCode = self.typeAttr.value.AttrCode;
            self.currentItem.AttrName = self.typeAttr.value.AttrName;
            self.currentItem.MaterialId = self.selectedItem.MaterialId;
            self.currentItem.MaterialFactoryId = self.selectedItem.Id;

            var postData = {
                KeyValue: null,
                Entity: self.currentItem
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddDetailctrl.Tips_4') });
            var url = commonService.getMesApiAddress("material") + 'Base_MaterialFacet/SaveBase_MaterialFacet';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddDetailctrl.Tips_5'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddDetailctrl.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddDetailctrl.Tips_6'));
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddDetailScreenStateConfig.$inject = ['$stateProvider'];
    function AddDetailScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MaterialMain_FactoryMaterial';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MaterialMain';

        var state = {
            name: screenStateName + '.addDetail',
            url: '/addDetail',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/FactoryMaterial-addDetail.html',
                    controller: AddDetailScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MaterialMain.FactoryMaterialaddDetailctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
