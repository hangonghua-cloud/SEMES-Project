(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.TraitBOM').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOM.service', '$state', '$stateParams',
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
            //初始化 是否启用
            self.IsEnabled = {
                value: '1',
                options: [{
                    label: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_1'),
                    value: '1'
                }, {
                    label: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_2'),
                    value: '0'
                }]
            };

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_3'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.queryItem = {};
            self.validInputs = false;
            self.queryItem = angular.copy($stateParams.selectedItem);
            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.materialClick = materialClick;
            self.typeFactoryChange = typeFactoryChange;
        }
        function initDictionary() {
            self.ProcessRoute = {
                value: { ProcessCode: "", ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_4') },
                options: [{ ProcessCode: "", ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_4') }]
            };
            self.typeFactory = {
                value: null,
                options: []
            };
            self.TypeOrder = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_4'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_4'), ItemValue: "" }]
            }
            self.typeUnit = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_4'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_4'), ItemValue: "" }]
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
            // var url1 = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=";
            // commonService.callWebApiGet(url1, null).then(function (res) {
            //     if (res && res.data.success) {
            //         self.ProcessRoute.options = res.data.resultData;
            //         self.ProcessRoute.options.splice('0', '0', {
            //             ProcessCode: "",
            //             ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_4')
            //         });
            //     }
            // })

            var url = commonService.getMesApiAddress("factory") + 'level/Get_ModelResourceExtendInfo_ByLevelCode';
            commonService.callWebApiPost(url, { LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    // if (res.data.resultData.length > 0) {
                    //     self.typeFactory.value = res.data.resultData[0];
                    // }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_4')
                    });
                } else {
                    self.typeFactory.options = [];
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_5'), commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_6'));
                }
            });
        }

        function typeFactoryChange(oldItem, newItem) {
            // if (newItem.ResourceCode) {
            //     //工艺路线
            //     var url = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=" + newItem.ResourceCode;
            //     commonService.callWebApiGet(url, null).then(function (res) {
            //         if (res && res.data.success) {
            //             self.ProcessRoute.options = res.data.resultData;
            //             self.ProcessRoute.options.splice('0', '0', {
            //                 ProcessCode: "",
            //                 ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_4')
            //             });
            //         }
            //     })
            // } else {
            //     self.ProcessRoute = {
            //         value: { ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_4'), ProcessCode: "" },
            //         options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_4'), ProcessCode: "" }]
            //     };
            // }
            if (newItem.ResourceCode) {

                materialClick(newItem.ResourceCode);

            }
        }


        function materialClick(newvalue) {
            if (newvalue == "" || newvalue == undefined) {
                newvalue = self.typeFactory.value.ResourceCode;
            }
            //自定义查询字段
            let queryParmeters = [
                { 'FieldCode': 'TraitCode', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_7'), 'FiledType': 'Text' },
                { 'FieldCode': 'TraitName', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_8'), 'FiledType': 'Text' },
                { 'FieldCode': 'TraitValue', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_9'), 'FiledType': 'Text' }
            ];

            //grid显示字段列表
            let columnDefs = [
                {
                    field: 'FactoryName',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_10'),
                    width: 150
                },

                {
                    field: 'TraitCode',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_7'),
                    width: 150
                },
                {
                    field: 'TraitName',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_8'),
                    width: 160
                },
                {
                    field: 'TraitValue',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_9'),
                    width: 250
                }
            ];

            /*
            * 功能描述: 单选弹窗方法(), 自定义查询条件,显示列表字段
            * 创    建: jpf
            * 创建时间: 2022-11-15
            * 参    数:
            *       title    标题(最后生成如: 选择产品型号)
            *       PostUrl API接口
            *       queryParmeters  查询条件 参考 [{'FieldCode': 'purchaseOrderNo', 'FileldName':commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_11'),'FiledType':'Text'},{'FieldCode': 'poDate', 'FileldName':commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_12'),'FiledType':'Date'}]
            *       sidx  排序字段
            *       sord  排序方式
            *       columnDefs   grid显示字段列表
            *       callback  回调方法
            */
            let query = {
                Factory: newvalue
            }
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_13'), commonService.getMesApiAddress("material") + 'BS_TraitManage/BS_TraitManagerDetailDataTableList', queryParmeters, 'TraitCode', 'asc', columnDefs, Select_SingleChoiceModal_MaterialName_callback, query);
        }
        //公共弹窗回调方法
        function Select_SingleChoiceModal_MaterialName_callback(result_data) {
            console.info('Select_SingleChoiceModal_MaterialName_callback_data', result_data);
            self.currentItem.MaterialName = result_data.TraitName;
            self.currentItem.MaterialCode = result_data.TraitValue

        }
        //选择物料
        // function materialClick() {
        //     var modalInstance = commonService.openModel({
        //         templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
        //         controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
        //         controllerAs: 'vm',
        //         size: 'lg',
        //         resolve: {
        //             data: function () {
        //                 return {
        //                     url: commonService.getMesApiAddress("material") + 'BS_TraitManage/BS_TraitManagerDetailDataTableList',
        //                     method: "Post",
        //                     queryParmeters: {
        //                         Name: "",
        //                         FactoryCode: self.typeFactory.value.ResourceCode,
        //                         //SmallClassOwnProduct: "'GB','DC'"
        //                         //MaterialClass: "BCPL"
        //                     },
        //                     pagination: {},
        //                     multiple: false,
        //                     sidx: "TraitCode",
        //                     sord: "asc",
        //                     columnDefs: [
        //                         {
        //                             name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_14'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
        //                                 '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
        //                         },
        //                         {
        //                             field: 'MaterialCode',
        //                             displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_15'),
        //                             width: 110
        //                         },
        //                         {
        //                             field: 'MaterialName',
        //                             displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_16'),
        //                             width: 120
        //                         },
        //                         {
        //                             field: 'Spec',
        //                             displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_17'),
        //                             width: 120
        //                         },
        //                         {
        //                             field: 'MaterialClassName',
        //                             displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_18'),
        //                             width: 120
        //                         },
        //                         {
        //                             field: 'ProcessRouteName',
        //                             displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_19'),
        //                             width: 120
        //                         },
        //                     ],
        //                 };
        //             }
        //         }
        //     });
        //     modalInstance.result.then(function (data) {
        //         console.log(data);
        //         self.currentItem.MaterialCode = data[0].MaterialCode;
        //         self.currentItem.MaterialName = data[0].MaterialName;
        //         self.currentItem.Spec = data[0].Spec;
        //         self.currentItem.MaterialClass = data[0].MaterialClass;
        //         self.currentItem.MaterialClassName = data[0].MaterialClassName;
        //         // self.currentItem.ProcessRoute = data[0].ProcessRoute;
        //         //self.currentItem.ProcessRouteName = data[0].ProcessRouteName;
        //     });
        // }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {

            if (!self.currentItem.MaterialName) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_20'));
                return;
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_21') });
            //字典类型 取值参考
            if (!!self.ProcessRoute.value && self.ProcessRoute.value.ProcessCode != "") {
                self.currentItem.Process = self.ProcessRoute.value.ProcessCode;
            }
            //单位取值
            if (!!self.typeUnit.value && self.typeUnit.value.ItemName != "") {
                self.currentItem.UnitName = self.typeUnit.value.ItemName;
            }
            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.typeFactory.value.ResourceName;
            if (!!self.TypeOrder.value) self.currentItem.OrderType = self.TypeOrder.value.ItemValue;
            if (!!self.typeUnit.value) self.currentItem.Unit = self.typeUnit.value.ItemValue;
            self.currentItem.BOMType = "2";
            if (self.IsEnabled.value == "1") {
                self.currentItem.IsDefault = true;

            } else {
                self.currentItem.IsDefault = false;
            }
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            console.log("jpf11234567890" + JSON.stringify(postData));
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_22'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_6'));
            }
        }
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_6'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_TraitBOM_TraitBOM';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/TraitBOM';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/TraitBOM-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMaddctrl.Tips_3'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
