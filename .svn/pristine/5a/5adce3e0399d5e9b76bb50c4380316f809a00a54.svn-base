(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MaterialMain').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMain.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth,
        notificationService, busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        activate();
        function activate() {
            init();
            // initGridOptions();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_1'));
            sidePanelManager.open('e');//使用窄弹窗
            // sidePanelManager.open({
            //     mode: 'e',
            //     size: 'wide'
            // });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;


            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            //初始化 物料分类
            self.MaterialClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_2'), ItemValue: "" }]
            };

            //初始化 物料小类
            self.SmallClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_2'), ItemValue: "" }]
            };

            self.typeUnit = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_2'), ItemValue: "" }]
            };


            self.Warehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_2'), ResourceCode: "" }]
            };

            self.ProcureType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_2'), ItemValue: "" }]
            };

            //初始化 物料小类
            self.ProcessRoute = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_2'), ItemValue: "" }]
            };
            //初始化 物料小类
            self.IsEnabled = {
                value: { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_3') },
                options: [
                    { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_3') },
                    { ItemValue: false, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_4') }
                ]
            };
            self.Attr = {
                value: null,
                options: []
            }


            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.AttrChange = AttrChange;
            //初始化数据字典
            initDictionary();
            GetUserInfo();
            // initGridData();
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        function initDictionary() {

            commonService.getDataItemDuatil("MaterialType").then(function (res) {
                if (res && res.data.success) {
                    self.MaterialClass.options = res.data.resultData;
                    self.MaterialClass.value = res.data.resultData.find(t => t.ItemValue == self.currentItem.MaterialClass);
                }
            })
            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.SmallClass.options = res.data.resultData;
                    self.SmallClass.value = res.data.resultData.find(t => t.ItemValue == self.currentItem.SmallClass);
                }
            })
            commonService.getDataItemDuatil("Unit").then(function (res) {
                if (res && res.data.success) {
                    self.typeUnit.options = res.data.resultData;
                    self.typeUnit.value = res.data.resultData.find(t => t.ItemValue == self.currentItem.Unit);
                }
            })
            commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
                if (res && res.data.success) {
                    self.Warehouse.options = res.data.resultData;
                    self.Warehouse.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_2')
                    });
                    self.Warehouse.value = self.Warehouse.options.find(t => t.ResourceCode == self.currentItem.Warehouse)
                }
            });
            commonService.getDataItemDuatil("ProcureType").then(function (res) {
                if (res && res.data.success) {
                    self.ProcureType.options = res.data.resultData;
                    self.ProcureType.value = res.data.resultData.find(t => t.ItemValue == self.currentItem.ProcureType);
                }
            })


            commonService.getDataItemDuatil("ProcessRoute").then(function (res) {
                if (res && res.data.success) {
                    self.ProcessRoute.options = res.data.resultData;
                    self.ProcessRoute.value = res.data.resultData.find(t => t.ItemValue == self.currentItem.ProcessRoute);
                }
            })

            //获取属性模板

            var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTemp/GetBase_MaterialBindTempList?checkType=';
            var req = commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.Attr.options = res.data.resultData;
                    self.Attr.options.splice(0, 0, {
                        TempCode: "",
                        TempName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_2')
                    });
                    //self.Attr.value = { TempCode: res.data.resultData[0].TempCode, TempName: res.data.resultData[0].TempName };
                } else {
                    self.Attr.options = [];
                }
            });

        }


        function AttrChange(oldval, newval) {
            if (newval) initGridDataAttr(newval.Id);
        }
        function initGridDataAttr(id) {
            debugger
            var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTempFacet/GetBase_MaterialBindTempFacetList?checkType=' + id;
            var req = commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem.data = res.data.resultData;
                } else {
                    self.gridOptionsItem.data = []
                }
            });
        }

        function initGridData() {
            var url = commonService.getMesApiAddress("material") + 'Base_MaterialFacet/GetBase_MaterialFacetList?checkType=' + self.currentItem.Id;
            commonService.callWebApiGet(url, null).then(function (res) {
                if ((res) && (res.data.success)) {
                    //需要转换数据
                    res.data.resultData.forEach((item, index, arr) => {
                        if (item.AttrType == "1") {
                            item.AttrValue = parseFloat(item.AttrValue);
                        }
                        self.gridOptionsItem.data.push(item);
                    });
                } else {
                    self.gridOptionsItem.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_5'));
            });
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
                    //     name: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_6'), field: 'operation', enableFiltering: false, enableSorting: false, enableColumnMenu: false,
                    //     cellTemplate: '<div style="text-align:center;"><button ng-show="!row.entity.addrow" title="添加" ng-click="grid.appScope.addrow(row.entity)"><span class="glyphicon glyphicon-plus"></span></button>' + ' ' +
                    //         '<button ng-show="!row.entity.editrow" title="删除" ng-click="grid.appScope.delete(row.entity)"><span class="glyphicon glyphicon-trash"></span></button>' + ' ' +
                    //         '</div>', width: 80
                    // },
                    // {
                    //     field: 'MaterialName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_7'),
                    //     enableCellEdit: false,
                    //     cellTemplate: '<div><div ng-click="grid.appScope.cellClicked(row.entity,col)" class="ui-grid-cell-contents" style="height:35px" title="TOOLTIP">{{COL_FIELD CUSTOM_FILTERS}}</div></div>',
                    //     width: 90
                    // },
                    {
                        field: 'AttrCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_8'),
                        width: 200,
                    },
                    {
                        field: 'AttrName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_9'),
                        width: 200
                    },

                    {
                        field: 'AttrTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_10'),
                        width: 200
                    },

                    {
                        field: 'AttrValue',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_11'),
                        cellTemplate: '<div ng-if="row.entity.AttrType==1"><sit-numeric sit-value="row.entity.AttrValue" ></sit-numeric></div>' +
                            '<div ng-if="row.entity.AttrType==2"><sit-text sit-value="row.entity.AttrValue" ></sit-text></div>' +
                            '<div ng-if="row.entity.AttrType==3"><sit-date-time-picker sit-value="row.entity.AttrValue"' +
                            'sit-format="\'yyyy-MM-dd HH:mm:ss\'"' +
                            'sit-show-button-bar="true"' +
                            'sit-show-weeks="false"' +
                            'sit-validation="{required: false}"></sit-date-time-picker></div>',
                        width: 300
                    },
                    // {
                    //     field: 'MixTime',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_12'),
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
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem = row.entity;
                            //setButtonsVisibility(true);
                        } else {
                            self.selectedItem = null;
                            //setButtonsVisibility(false);
                        }
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }

        //保存
        function save() {

            self.currentItem.MaterialClass = self.MaterialClass.value.ItemValue;
            self.currentItem.SmallClass = self.SmallClass.value.ItemValue;
            self.currentItem.Unit = self.typeUnit.value.ItemValue;
            self.currentItem.UnitName = self.typeUnit.value.ItemName;
            //self.currentItem.Warehouse = self.Warehouse.value.ResourceCode;
            //self.currentItem.ProcureType = self.ProcureType.value.ItemValue;
            // self.currentItem.ProcessRoute = self.ProcessRoute.value.ItemValue;
            self.currentItem.IsEnabled = self.IsEnabled.value.ItemValue;

            // var itemData = angular.copy(self.gridOptionsItem.data);

            // //处理时间转换为字符串
            // itemData.forEach((item, index, arr) => {
            //     if (item.AttrType == "3" && item.AttrValue) {
            //         item.AttrValue = $filter('date')(new Date(item.AttrValue), 'yyyy-MM-dd HH:mm:ss');

            //     }
            // });

            self.currentItem.creator = self.UserCode;
            if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                self.currentItem.creator = self.UserCode;
            }


            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem
                // Data: itemData
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_13') });
            var url = commonService.getMesApiAddress("material") + 'Base_Material/SaveBase_Material';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

        }

        //取消
        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_14'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_15'));
            }
        }
        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_15'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MaterialMain_MaterialMain';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MaterialMain';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/MaterialMain-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMaineditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
