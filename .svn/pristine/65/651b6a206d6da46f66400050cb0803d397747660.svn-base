(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.Process').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.Process.ProcessOperation.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope', '$interval'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope, $interval) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();

            initGridOptions();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.isReadOnly = false;
            console.log(self.currentItem)

            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.FactoryChange = FactoryChange;
            self.ProcessChange = ProcessChange;
            self.AttrChange = AttrChange;

        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function initDictionary() {
            self.typeSelect = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_2'), ItemValue: "" }]
            }
            self.Factory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_2'), ResourceCode: "" }]
            };
            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_2'), ResourceCode: "" }]
            };
            self.Attr = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_2'), ItemCode: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_2'), ItemCode: "" }]
            }
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_2')
                    });
                    self.Factory.value = self.Factory.options.find(t => t.ResourceCode == self.currentItem.FactoryCode);
                }
            });
        }
        function FactoryChange(oldItem, newItem) {
            commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.Process.options = res.data.resultData;
                    self.Process.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_2')
                    });
                    self.Process.value = self.Process.options.find(t => t.ResourceCode == self.currentItem.Process);
                }

            });
        }
        function ProcessChange(oldval, newval) {
            if (!!newval) {
                AttrSelect(self.Factory.value.ResourceCode, newval.ResourceCode);
                self.isReadOnly = true;
            }
        }

        function AttrSelect(factoryCode, process) {
            console.log(factoryCode, process)
            if (!factoryCode || !process) {
                return false;
            }
            var params = {
                FactoryCode: factoryCode,
                Process: process
            }

            var url = commonService.getMesApiAddress("material") + "Base_ProcessAttr/GetProcessAttrPage";
            commonService.callWebApiPost(url, params).then(function (res) {

                if (res && res.data.success) {
                    self.Attr.options = res.data.resultData.rows;
                    self.Attr.options.splice(0, 0, {
                        ItemCode: "",
                        ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_2')
                    });
                }
            })
        }



        function initGridOptions() {
            self.gridOptionsItem = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
                rowHeight: 35,
                multiSelect: false,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: false,
                enableRowSelection: false,
                //enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    // {
                    //     name: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_3'), field: 'operation', enableFiltering: false, enableSorting: false, enableColumnMenu: false,
                    //     cellTemplate: '<div style="text-align:center;"><button ng-show="!row.entity.addrow" title="添加" ng-click="grid.appScope.addrow(row.entity)"><span class="glyphicon glyphicon-plus"></span></button>' + ' ' +
                    //         '<button ng-show="!row.entity.editrow" title="删除" ng-click="grid.appScope.delete(row.entity)"><span class="glyphicon glyphicon-trash"></span></button>' + ' ' +
                    //         '</div>', width: 80
                    // },
                    // {
                    //     field: 'MaterialName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_4'),
                    //     enableCellEdit: false,
                    //     cellTemplate: '<div><div ng-click="grid.appScope.cellClicked(row.entity,col)" class="ui-grid-cell-contents" style="height:35px" title="TOOLTIP">{{COL_FIELD CUSTOM_FILTERS}}</div></div>',
                    //     width: 90
                    // },
                    {
                        field: 'AttrCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_5'),
                        width: 110,
                    },
                    {
                        field: 'AttrName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_6'),
                        width: 110
                    },

                    {
                        field: 'AttrTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_7'),
                        width: 110
                    },

                    {
                        field: 'AttrValue',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_8'),
                        cellTemplate: '<div ng-show="row.entity.AttrType==1"><sit-numeric  sit-value="row.entity.AttrValue" ></sit-numeric></div>' +
                            '<div ng-show="row.entity.AttrType==2"><sit-text sit-value="row.entity.AttrValue" ></sit-text></div>' +
                            '<div ng-show="row.entity.AttrType>3"><sit-select sit-value="row.entity.typeAttrSelect.value"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.typeAttrSelect.options"' +

                            'sit-to-display="\'ItemName\'"' +
                            'sit-to-keep="\'ItemValue\'">' +
                            '</sit-select></div>',
                        width: 100
                    },


                    // {
                    //     field: 'MixTime',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_9'),
                    //     enableCellEdit: false,
                    //     cellFilter: 'date:\'yyyy-MM-dd HH:mm:ss\'',
                    //     width: 150,
                    //     cellTemplate:
                    //         '<div ng-show="row.entity.AttrType==3"><sit-date-time-picker sit-value="row.entity.MixTime"' +
                    //         'sit-format="\'yyyy-MM-dd HH:mm:ss\'"' +
                    //         'sit-show-button-bar="true"' +
                    //         'sit-show-weeks="false"' +
                    //         'sit-validation="{required: false}"></sit-date-time-picker></div>',
                    // },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    // gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                    //     if (row && row.isSelected == true) {
                    //         self.selectedItem = row.entity;
                    //         //setButtonsVisibility(true);
                    //     } else {
                    //         self.selectedItem = null;
                    //         //setButtonsVisibility(false);
                    //     }
                    // });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }

        function AttrChange(oldval, newval) {
            if (!newval.ItemCode) {
                self.gridOptionsItem.data = [];
                return false;
            }
            let queryParmeters = {
                queryJson: {
                    ItemCode: newval.ItemCode
                }
            };
            var url = commonService.getMesApiAddress("material") + "Base_ProcessAttr/GetProcessAttrItemPage";
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {

                var data = res.data.resultData.rows;
                if ((res) && (res.data.success)) {

                    var url1 = commonService.getMesApiAddress("material") + "BS_ProcessOfOperations/BS_ProcessOfOperationsAttrPage"

                    commonService.callWebApiPost(url1, { queryJson: { OperationsId: self.currentItem.Id } }).then(function (resAttr) {
                        if (resAttr && resAttr.data.success && resAttr.data.resultData.rows.length > 0) {

                            data.forEach((item, index, arr) => {
                                //赋值
                                var ent = resAttr.data.resultData.rows.find(t => t.AttrCode == item.AttrCode);

                                if (item.AttrType > 3) {
                                    self.typeSelect.options = [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_2'), ItemValue: "" }];
                                    var arr = item.AttrTypeName.split("/");
                                    for (var i = 0; i < arr.length; i++) {
                                        self.typeSelect.options.push({
                                            ItemName: arr[i], ItemValue: i
                                        })
                                    }
                                    if (!!item.AttrValue) {
                                        self.typeSelect.value = self.typeSelect.options.find(t => t.ItemName == item.AttrValue);
                                    }
                                    item.typeAttrSelect = angular.copy(self.typeSelect)
                                }
                                if (ent != null) {
                                    if (item.AttrType == "1") {
                                        if (ent.AttrValue != "") {
                                            item.AttrValue = parseFloat(ent.AttrValue);
                                        } else {
                                            item.AttrValue = "";
                                        }
                                    }
                                    else if (item.AttrType > "3") {
                                        if (ent.AttrValue != "") {
                                            item.typeAttrSelect.value = item.typeAttrSelect.options.find(t => t.ItemName == ent.AttrValue);
                                        } else {
                                            item.typeAttrSelect.value = item.typeAttrSelect.options.find(t => t.ItemValue == "");
                                        }
                                    }
                                    else item.AttrValue = ent.AttrValue;
                                }

                            });
                        }
                        else {
                            data.forEach((item, index, arr) => {
                                if (item.AttrType > 3) {
                                    self.typeSelect.options = [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_2'), ItemValue: "" }];
                                    var arr = item.AttrTypeName.split("/");
                                    for (var i = 0; i < arr.length; i++) {
                                        self.typeSelect.options.push({
                                            ItemName: arr[i], ItemValue: i
                                        })
                                    }
                                    item.typeAttrSelect = angular.copy(self.typeSelect)
                                }

                            });
                        }

                        self.gridOptionsItem.data = data;
                    });

                    // self.gridOptionsItem.totalItems = res.data.resultData.records;

                } else {
                    self.gridOptionsItem.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_10'));
            });
        }

        function save() {

            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            var flag = false;
            // self.currentItem.IsEnabled = true;
            // self.currentItem.FactoryCode = self.Factory.value.ResourceCode;
            // self.currentItem.FactoryName = self.Factory.value.ResourceName;
            // self.currentItem.Process = self.Process.value.ResourceCode;
            // self.currentItem.ProcessName = self.Process.value.ResourceName;
            var rows = self.gridOptionsItem.data;

            rows.forEach((item, index, arr) => {
                if (item.AttrType > "3") {
                    item.AttrValue = item.typeAttrSelect.value.ItemName;
                    if (item.AttrValue == commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_2')) {
                        item.AttrValue = "";
                    }
                } else if (item.AttrType == "1") {
                    if (!item.AttrValue) {
                        item.AttrValue = "";
                    }
                }

                item.OperationsId = self.currentItem.Id;
                item.AttrValue = item.AttrValue + "";
            });
            if (rows.length == 0 || flag) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_11'), commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_12'));
                return false;
            }

            var postData = {
                KeyValue: self.currentItem.Id,
                data: rows
            };

            var url = commonService.getMesApiAddress("material") + 'BS_ProcessOfOperations/SaveProcessOfOperationsAttrList';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_13') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_14'));
                //刷新局部
                $rootScope.$emit('to-parentAttr', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_12'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_12'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_Process_ProcessOperation';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/Process';

        var state = {
            name: screenStateName + '.addAttr',
            url: '/addAttr',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProcessOperation-addAttr.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.Process.ProcessOperationaddAttrctrl.Tips_1'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
