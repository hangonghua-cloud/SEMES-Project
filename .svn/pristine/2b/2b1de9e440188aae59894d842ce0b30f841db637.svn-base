(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.TraitBOM').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOM.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope,
        commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            //初始化 是否启用
            self.IsEnabled = {
                value: '1',
                options: [{
                    label: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_1'),
                    value: '1'
                }, {
                    label: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_2'),
                    value: '0'
                }]
            };
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_3'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            if (self.currentItem.isDefault == true) {
                self.IsEnabled.value = "1";
            } else {
                self.IsEnabled.value = "0";
            }

            initDictionary();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.materialClick = materialClick;
            self.materialChange = materialChange;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function materialChange(oldvalue, newvalue) {

            var url = commonService.getMesApiAddress("material") + 'Base_Material/GetDataTable_TestOtherEntity?checkType=' + newvalue;
            commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success && res.data.resultData.length > 0) {
                    self.currentItem.MaterialClass = res.data.resultData[0].MaterialClassName;
                    self.currentItem.MaterialName = res.data.resultData[0].MaterialName;
                } else {
                    self.currentItem.MaterialClass = "";
                    self.currentItem.MaterialName = "";
                }
            });
        }

        function initDictionary() {

            self.ProcessRoute = {
                value: { ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_4'), ProcessCode: "" },
                options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_4'), ProcessCode: "" }]
            };
            self.typeFactory = {
                value: null,
                options: []
            };
            self.TypeOrder = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_4'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_4'), ItemValue: "" }]
            }
            self.typeUnit = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_4'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_4'), ItemValue: "" }]
            };

            commonService.getDataItemDuatil("Unit").then(function (res) {
                if (res && res.data.success) {
                    self.typeUnit.options = res.data.resultData;
                    // self.typeUnit.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                    self.typeUnit.value = self.typeUnit.options.find(t => t.ItemName == self.currentItem.TraitUnitName);
                }
            })
            commonService.getDataItemDuatil("OrderType").then(function (res) {
                if (res && res.data.success) {
                    self.TypeOrder.options = res.data.resultData;
                    self.TypeOrder.value = self.TypeOrder.options.find(t => t.ItemValue == self.currentItem.OrderType);
                }
            })
            // var url1 = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=";
            // commonService.callWebApiGet(url1, null).then(function (res) {
            //     if (res && res.data.success) {
            //         self.ProcessRoute.options = res.data.resultData;
            //         self.ProcessRoute.options.splice('0', '0', {
            //             ProcessCode: "",
            //             ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_4')
            //         });
            //         self.ProcessRoute.value = self.ProcessRoute.options.find(t => t.ProcessCode == self.currentItem.Process);
            //     }
            // })
            //工艺路线
            var url = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=" + self.currentItem.FactoryCode;
            commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.ProcessRoute.options = res.data.resultData;
                    self.ProcessRoute.options.splice('0', '0', {
                        ProcessCode: "",
                        ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_4')
                    });
                    self.ProcessRoute.value = self.ProcessRoute.options.find(t => t.ProcessCode == self.currentItem.Process);
                }
            })

            var url = commonService.getMesApiAddress("factory") + 'level/Get_ModelResourceExtendInfo_ByLevelCode';
            commonService.callWebApiPost(url, { LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    self.typeFactory.value = self.TypeFactory.options.find(t => t.ResourceCode == self.currentItem.FactoryCode);
                } else {
                    self.typeFactory.options = [];
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_5'), commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_6'));
                }
            });
        }
        function materialClick() {
            //自定义查询字段
            let queryParmeters = [
                { 'FieldCode': 'TraitCode', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_7'), 'FiledType': 'Text' },
                { 'FieldCode': 'TraitName', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_8'), 'FiledType': 'Text' },
                { 'FieldCode': 'TraitValue', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_9'), 'FiledType': 'Text' }
            ];

            //grid显示字段列表
            let columnDefs = [
                {
                    field: 'TraitCode',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_7'),
                    width: 200
                },
                {
                    field: 'TraitName',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_8'),
                    width: 200
                },
                {
                    field: 'TraitValue',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_9'),
                    width: 200
                }
            ];

            /*
            * 功能描述: 单选弹窗方法(), 自定义查询条件,显示列表字段
            * 创    建: jpf
            * 创建时间: 2022-11-15
            * 参    数:
            *       title    标题(最后生成如: 选择产品型号)
            *       PostUrl API接口
            *       queryParmeters  查询条件 参考 [{'FieldCode': 'purchaseOrderNo', 'FileldName':commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_10'),'FiledType':'Text'},{'FieldCode': 'poDate', 'FileldName':commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_11'),'FiledType':'Date'}]
            *       sidx  排序字段
            *       sord  排序方式
            *       columnDefs   grid显示字段列表
            *       callback  回调方法
            */
            let query = {
                Factory: self.currentItem.FactoryCode
            }
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_12'), commonService.getMesApiAddress("material") + 'BS_TraitManage/BS_TraitManagerDetailDataTableList', queryParmeters, 'TraitCode', 'asc', columnDefs, Select_SingleChoiceModal_MaterialName_callback, query);
        }
        //公共弹窗回调方法
        function Select_SingleChoiceModal_MaterialName_callback(result_data) {
            console.info('Select_SingleChoiceModal_MaterialName_callback_data', result_data);
            self.currentItem.MaterialName = result_data.TraitName;
            self.currentItem.MaterialCode = result_data.TraitValue

        }

        function save() {

            if (!self.currentItem.MaterialName) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_13'));
                return;
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_14') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            //单位取值

            if (!!self.TypeOrder.value) self.currentItem.OrderType = self.TypeOrder.value.ItemValue;
            if (!!self.typeUnit.value) self.currentItem.UnitName = self.typeUnit.value.ItemName;

            if (self.IsEnabled.value == "1") {
                self.currentItem.IsDefault = true;

            } else {
                self.currentItem.IsDefault = false;
            }

            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            console.log("jpf123456" + JSON.stringify(postData));
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_15'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_6'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_TraitBOM_TraitBOM';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/TraitBOM';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/TraitBOM-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.TraitBOM.TraitBOMeditctrl.Tips_3'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
