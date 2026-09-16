(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.BOM').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.BOM.BOM.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter,
        $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle('导入');
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;

            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.materialChange = materialChange;
            self.materialClick = materialClick;
        }

        function materialChange(oldvalue, newvalue) {

            var url = commonService.getMesApiAddress("material") + 'Base_Material/GetDataTable_TestOtherEntity?checkType=' + newvalue;
            commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success && res.data.resultData.length > 0) {
                    self.currentItem.MaterialClassName = res.data.resultData[0].MaterialClassName;
                    self.currentItem.MaterialClass = res.data.resultData[0].MaterialClass;
                    self.currentItem.MaterialName = res.data.resultData[0].MaterialName;
                } else {
                    self.currentItem.MaterialClassName = "";
                    self.currentItem.MaterialName = "";
                }
            });
        }
        //选择物料
        function materialClick() {
            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("material") + 'Base_MaterialFactory/Base_MaterialFactoryPageDataTableList',
                            method: "Post",
                            queryParmeters: {
                                Name: "",
                                FactoryCode: self.TypeFactory.value.ResourceCode,
                                //SmallClassOwnProduct: "'GB','DC'"
                                //MaterialClass: "BCPL"
                            },
                            pagination: {},
                            multiple: false,
                            sidx: "MaterialCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: '序号', minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'MaterialCode',
                                    displayName: '物料编码',
                                    width: 110
                                },
                                {
                                    field: 'MaterialName',
                                    displayName: '物料物料名称',
                                    width: 120
                                },
                                {
                                    field: 'Spec',
                                    displayName: '规格型号',
                                    width: 120
                                },
                                {
                                    field: 'MaterialClassName',
                                    displayName: '物料分类',
                                    width: 120
                                },
                                {
                                    field: 'ProcessRouteName',
                                    displayName: '工艺路线',
                                    width: 120
                                },
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                console.log(data);
                self.currentItem.MaterialCode = data[0].MaterialCode;
                self.currentItem.MaterialName = data[0].MaterialName;
                self.currentItem.Spec = data[0].Spec;
                self.currentItem.MaterialClass = data[0].MaterialClass;
                self.currentItem.MaterialClassName = data[0].MaterialClassName;
                // self.currentItem.ProcessRoute = data[0].ProcessRoute;
                //self.currentItem.ProcessRouteName = data[0].ProcessRouteName;
            });
        }

        function initDictionary() {
            self.ProcessRoute = {
                value: { ProcessCode: "", ProcessName: "--请选择--" },
                options: [{ ProcessCode: "", ProcessName: "--请选择--" }]
            };
            self.TypeFactory = {
                value: null,
                options: []
            };
            self.TypeOrder = {
                value: { ItemName: "--请选择--", ItemValue: "" },
                options: [{ ItemName: "--请选择--", ItemValue: "" }]
            }
            self.typeUnit = {
                value: { ItemName: "--请选择--", ItemValue: "" },
                options: [{ ItemName: "--请选择--", ItemValue: "" }]
            };

            commonService.getDataItemDuatil("Unit").then(function (res) {
                if (res && res.data.success) {
                    self.typeUnit.options = res.data.resultData;
                    self.typeUnit.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("OrderType").then(function (res) {
                if (res && res.data.success) {
                    self.TypeOrder.options = res.data.resultData;
                    self.TypeOrder.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            var url1 = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=";
            commonService.callWebApiGet(url1, null).then(function (res) {
                if (res && res.data.success) {
                    self.ProcessRoute.options = res.data.resultData;
                    self.ProcessRoute.options.splice('0', '0', {
                        ProcessCode: "",
                        ProcessName: "--请选择--"
                    });
                }
            })

            var url = commonService.getMesApiAddress("factory") + 'level/Get_ModelResourceExtendInfo_ByLevelCode';
            commonService.callWebApiPost(url, { LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.TypeFactory.options = res.data.resultData;
                    self.TypeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: "--请选择--"
                    });
                } else {
                    self.TypeFactory.options = [];
                    backendService.genericError("获取数据失败", "操作出错");
                }
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {

            if (!self.currentItem.MaterialName) {
                backendService.genericError("物料名称不能为空");
                return;
            }
            busyIndicatorService.show({ message: "保存中，请稍后……" });
            //字典类型 取值参考
            if (!!self.ProcessRoute.value && self.ProcessRoute.value.ProcessCode != "") {
                self.currentItem.Process = self.ProcessRoute.value.ProcessCode;
            }
            self.currentItem.FactoryCode = self.TypeFactory.value.ResourceCode;
            if (!!self.TypeOrder.value) self.currentItem.OrderType = self.TypeOrder.value.ItemValue;
            if (!!self.typeUnit.value) self.currentItem.Unit = self.typeUnit.value.ItemValue;

            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("material") + 'BS_BOM/SaveBS_BOM';
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
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, "操作出错");
            }
        }
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
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_BOM_BOM';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/BOM';

        var state = {
            name: screenStateName + '.import',
            url: '/import',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/BOM-import.html',
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
