(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.OwnProduct').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.OwnProduct.OwnProductOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addDetailJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.selectClick = selectClick;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        //选择人员
        function selectClick() {
            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress() + 'Base/GetListUser',
                            method: "Post",
                            queryParmeters: {
                                Name: "",
                                FactoryCode: self.currentItem.FactoryCode
                            },
                            pagination: {
                                rows: 80,//每页显示条数
                                page: 1,//页码
                            },
                            multiple: true,
                            sidx: "Code",
                            sord: "asc",
                            columnDefs: [
                                {
                                    field: 'Code',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addDetailJS.Tips_2'),
                                    width: 200
                                },
                                {
                                    field: 'Name',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addDetailJS.Tips_3'),
                                    width: 200
                                },
                                {
                                    field: 'DeptName',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addDetailJS.Tips_4'),
                                    width: 300
                                },
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                self.currentItem.UserCode = "";
                self.currentItem.UserNames = "";
                data.forEach(item => {
                    self.currentItem.UserCode += item.Code + ",";
                    self.currentItem.UserNames += item.Name + ",";
                })
                self.currentItem.UserNames = self.currentItem.UserNames.substring(0, self.currentItem.UserNames.length - 1);
            });
        }

        function save() {

            var postData = {
                KeyValue: '',
                PrintNum: self.currentItem.PrintNum,
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_OwnProductTransfer/SaveBatchPM_OwnProductTransfer';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addDetailJS.Tips_5') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addDetailJS.Tips_6'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', data.data.resultData);
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addDetailJS.Tips_7'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.OwnProductOrder.addDetailJS.Tips_7'));
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_OwnProduct_OwnProductOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/OwnProduct';

        var state = {
            name: screenStateName + '.addDetail',
            url: '/addDetail',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/OwnProductOrder-addDetail.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.OwnProductOrder.addDetailJS.Tips_8'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
