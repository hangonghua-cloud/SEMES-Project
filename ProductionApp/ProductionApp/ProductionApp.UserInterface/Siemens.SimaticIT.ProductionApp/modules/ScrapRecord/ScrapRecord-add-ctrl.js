(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.ScrapRecord').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.ScrapRecord.ScrapRecord.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;
            self.typeFactoryChange = typeFactoryChange;
            self.typeScrapReasonChange = typeScrapReasonChange;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.typeSmallClassModal = typeSmallClassModal;

            initDictionary();
        }
        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_2'), ResourceCode: "" }]
            };
            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_2'), ResourceCode: "" }]
            };
            self.typeScrapReason = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_2'), ItemValue: "" }]
            };

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_2')
                    });
                }
            })
        }

        function typeFactoryChange(oldItem, newItem) {
            // debugger;
            commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                // debugger;
                if (res && res.data.success) {
                    self.typeProcess.options = res.data.resultData;
                    self.typeProcess.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_2')
                    });
                }
            });
            //报废原因
            commonService.getDataItemDuatil("PoorWorkReport").then(function (res) {
                // debugger;
                if (res && res.data.success) {
                    self.typeScrapReason.options = res.data.resultData.filter(t => t.Remark1 == newItem.ResourceCode);
                }
            })
        }

        function typeScrapReasonChange(oldItem, newItem) {
            debugger;
            let processCode = newItem.Description;
            self.typeProcess.value = self.typeProcess.options.find(t => t.ResourceCode == processCode);
        }

        //选择物料小类
        function typeSmallClassModal() {

            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress() + 'SystemManage/DataItemDetail/GetDataItemListJson?EnCode=MaterialSmall',
                            queryParmeters: {
                                Name: "",
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Get",
                            // sidx: "ProcessCode",
                            // sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ItemValue',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_4'),
                                    width: 200
                                },
                                {
                                    field: 'ItemName',
                                    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_5'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_6'));
                } else {
                    self.currentItem.SmallClassCode = data[0].ItemValue;
                    self.currentItem.SmallClassName = data[0].ItemName;
                }
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {

            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.typeFactory.value.ResourceName;
            self.currentItem.ProcessCode = self.typeProcess.value.ResourceCode;
            self.currentItem.ProcessName = self.typeProcess.value.ResourceName;
            self.currentItem.BadItemCode = self.typeScrapReason.value.ItemValue;
            self.currentItem.BadItemName = self.typeScrapReason.value.ItemName;

            self.currentItem.StartTime = commonService.ConvertToLocalTime(self.StartTime);
            self.currentItem.EndTime = commonService.ConvertToLocalTime(self.EndTime);

            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_7') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PMScrapRecord/SaveForm';
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
            //console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_9'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_10'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_10'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_ScrapRecord_ScrapRecord';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/ScrapRecord';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ScrapRecord-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.ScrapRecord.addJS.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
