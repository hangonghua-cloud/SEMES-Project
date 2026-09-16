(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MaterialMain').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MaterialMain.MaterialMain.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth,
        notificationService, busyIndicatorService, $modal, $interval) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            //初始化
            init();
            initGridOptions();
            //注册事件
            registerEvents();

            sidePanelManager.setTitle('导入');
            // sidePanelManager.open('e');//使用窄弹窗
            //使用宽右侧弹窗
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }


        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.AttrChange = AttrChange;

            //初始化数据字典
            initDictionary();
            //获取登录用户信息
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
            //初始化 物料分类
            self.MaterialClass = {
                value: { ItemName: "--请选择--", ItemValue: "" },
                options: [{ ItemName: "--请选择--", ItemValue: "" }]
            };

            //初始化 物料小类
            self.SmallClass = {
                value: { ItemName: "--请选择--", ItemValue: "" },
                options: [{ ItemName: "--请选择--", ItemValue: "" }]
            };

            self.typeUnit = {
                value: { ItemName: "--请选择--", ItemValue: "" },
                options: [{ ItemName: "--请选择--", ItemValue: "" }]
            };


            self.Warehouse = {
                value: { ResourceName: "--请选择--", ResourceCode: "" },
                options: [{ ResourceName: "--请选择--", ResourceCode: "" }]
            };

            self.ProcureType = {
                value: { ItemName: "--请选择--", ItemValue: "" },
                options: [{ ItemName: "--请选择--", ItemValue: "" }]
            };

            //初始化 物料小类
            self.ProcessRoute = {
                value: { ItemName: "--请选择--", ItemValue: "" },
                options: [{ ItemName: "--请选择--", ItemValue: "" }]
            };
            //初始化 物料小类
            self.IsEnabled = {
                value: { ItemValue: true, ItemName: "是" },
                options: [
                    { ItemValue: true, ItemName: "是" },
                    { ItemValue: false, ItemName: "否" }
                ]
            };
            self.Attr = {
                value: null,
                options: []
            }
            commonService.getDataItemDuatil("MaterialType").then(function (res) {
                if (res && res.data.success) {
                    self.MaterialClass.options = res.data.resultData;
                    self.MaterialClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.SmallClass.options = res.data.resultData;
                    self.SmallClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("Unit").then(function (res) {
                if (res && res.data.success) {
                    self.typeUnit.options = res.data.resultData;
                    self.typeUnit.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
                if (res && res.data.success) {
                    self.Warehouse.options = res.data.resultData;
                    self.Warehouse.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: "--请选择--"
                    });
                }
            });

            commonService.getDataItemDuatil("ProcureType").then(function (res) {
                if (res && res.data.success) {
                    self.ProcureType.options = res.data.resultData;
                    self.ProcureType.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })


            commonService.getDataItemDuatil("ProcessRoute").then(function (res) {
                if (res && res.data.success) {
                    self.ProcessRoute.options = res.data.resultData;
                    self.ProcessRoute.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })

            //获取属性模板

            var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTemp/GetBase_MaterialBindTempList?checkType=';
            var req = commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.Attr.options = res.data.resultData;
                    self.Attr.options.splice(0, 0, {
                        TempCode: "",
                        TempName: "--请选择--"
                    });
                    //self.Attr.value = { TempCode: res.data.resultData[0].TempCode, TempName: res.data.resultData[0].TempName };
                } else {
                    self.Attr.options = [];
                }
            });

        }


        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }


        //下拉框改变事件
        function AttrChange(oldval, newval) {
            if (newval) initGridData(newval.Id);
        }

        //加载table
        function initGridData(id) {
            var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTempFacet/GetBase_MaterialBindTempFacetList?checkType=' + id;
            var req = commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem.data = res.data.resultData;
                } else {
                    self.gridOptionsItem.data = []
                }
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
                    //     name: '操作', field: 'operation', enableFiltering: false, enableSorting: false, enableColumnMenu: false,
                    //     cellTemplate: '<div style="text-align:center;"><button ng-show="!row.entity.addrow" title="添加" ng-click="grid.appScope.addrow(row.entity)"><span class="glyphicon glyphicon-plus"></span></button>' + ' ' +
                    //         '<button ng-show="!row.entity.editrow" title="删除" ng-click="grid.appScope.delete(row.entity)"><span class="glyphicon glyphicon-trash"></span></button>' + ' ' +
                    //         '</div>', width: 80
                    // },
                    // {
                    //     field: 'MaterialName',
                    //     displayName: '原料名称',
                    //     enableCellEdit: false,
                    //     cellTemplate: '<div><div ng-click="grid.appScope.cellClicked(row.entity,col)" class="ui-grid-cell-contents" style="height:35px" title="TOOLTIP">{{COL_FIELD CUSTOM_FILTERS}}</div></div>',
                    //     width: 90
                    // },
                    {
                        field: 'AttrCode',
                        displayName: '属性编码',
                        width: 200,
                    },
                    {
                        field: 'AttrName',
                        displayName: '属性名称',
                        width: 200
                    },

                    {
                        field: 'AttrTypeName',
                        displayName: '属性类型',
                        width: 200
                    },

                    {
                        field: 'AttrValue',
                        displayName: '录入值',
                        cellTemplate: '<div ng-show="row.entity.AttrType==1"><sit-numeric  sit-value="row.entity.AttrValue" ></sit-numeric></div>' +
                            '<div ng-show="row.entity.AttrType==2"><sit-text sit-value="row.entity.AttrValue" ></sit-text></div>' +
                            '<div ng-show="row.entity.AttrType==3"><sit-date-time-picker sit-value="row.entity.AttrValue"' +
                            'sit-format="\'yyyy-MM-dd HH:mm:ss\'"' +
                            'sit-show-button-bar="true"' +
                            'sit-show-weeks="false"' +
                            'sit-validation="{required: false}"></sit-date-time-picker></div>',
                        width: 300
                    },
                    // {
                    //     field: 'MixTime',
                    //     displayName: '配制时间',
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
            busyIndicatorService.show({ message: "保存中，请稍后……" });

            self.currentItem.MaterialClass = self.MaterialClass.value.ItemValue;
            self.currentItem.SmallClass = self.SmallClass.value.ItemValue;
            self.currentItem.Unit = self.typeUnit.value.ItemValue;
            //self.currentItem.Warehouse = self.Warehouse.value.ResourceCode;
            //self.currentItem.ProcureType = self.ProcureType.value.ItemValue;
            //self.currentItem.ProcessRoute = self.ProcessRoute.value.ItemValue;
            self.currentItem.IsEnabled = self.IsEnabled.value.ItemValue;

            var itemData = angular.copy(self.gridOptionsItem.data);

            // 校验
            //var flag=false;
            // var reg1=/^(-?\d+)(\.\d+)?$/
            // var reg2=/^((([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29))\s([0-1][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$/

            itemData.forEach((item, index, arr) => {
                if (item.AttrType == "3" && item.AttrValue) {//处理时间
                    item.AttrValue = $filter('date')(new Date(item.AttrValue), 'yyyy-MM-dd HH:mm:ss');
                }
            });


            self.currentItem.creator = self.UserCode;
            if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                self.currentItem.creator = self.UserCode;
            }

            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem,
                Data: itemData
            };

            var url = commonService.getMesApiAddress("material") + 'Base_Material/SaveBase_Material';
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
                commonService.showInfo('保存成功！');
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, "操作出错");
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, "操作出错");
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MaterialMain_MaterialMain';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MaterialMain';

        var state = {
            name: screenStateName + '.import',
            url: '/import',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/MaterialMain-import.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: '添加'
            }
        };
        $stateProvider.state(state);
    }
}());
