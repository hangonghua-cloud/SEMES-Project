/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.14 更新日期：2021-07-21  设计者：刘万军
*  1. 功能描述： 仓库安全库存
*  2. 创建人员： jpf
*  3. 创建日期： 2022-12-05
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStock.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            //初始化 工厂名称
            self.FactoryName = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_1'), ResourceCode: "" }]
            };
            // self.typeUnit = {
            //     value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_1'), ItemValue: "" },
            //     options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_1'), ItemValue: "" }]
            // };
            //初始化
            init();



            //初始化数据字典
            initDictionary();


            //注册事件
            registerEvents();

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_2'));
            sidePanelManager.open('e');//使用窄弹窗
            //使用宽右侧弹窗
            // sidePanelManager.open({
            //     mode: 'e',
            //     size: 'wide'
            // });
        }

        //获取数据字典数据
        function initDictionary() {

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.FactoryName.options = res.data.resultData;
                    // if (res.data.resultData.length > 0) {
                    //     self.FactoryName.value = res.data.resultData[0];
                    // }
                    self.FactoryName.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_1')
                    });
                }
            });
            // commonService.getDataItemDuatil("Unit").then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeUnit.options = res.data.resultData;
            //        // self.typeUnit.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
            //     }
            // })
        }

        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        //初始化
        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;


            //初始化前端变量数据
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.FactoryName.value = { "ResourceName": self.currentItem.FactoryName, "ResourceCode": self.currentItem.FactoryCode };
            //self.typeUnit.value={"ItemName":self.currentItem.UnitName,"ItemValue":self.currentItem.Unit}
            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            self.SelectMaterialCodeModal = SelectMaterialCodeModal;//选择物料编码
            self.selectClick1 = selectClick1;
            self.typeFactoryChange = typeFactoryChange;

        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function typeFactoryChange(oldItem, newItem) {
            debugger;
            if (newItem.ResourceCode) {
                self.currentItem.FactoryCode = newItem.ResourceCode;
            }
        }
        function selectClick1() {
            if (!self.FactoryName.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_3'));
                return;
            }
            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("factory") + 'level/GetWarehouseByFactoryExtendInfo',
                            queryParmeters: {
                                factoryCode: self.FactoryName.value.ResourceCode,
                                fieldCode: "CKLX",
                                fieldValue: "1"
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Post",
                            sidx: "ResourceCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_4'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ResourceCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_5'),
                                    width: 200
                                },
                                {
                                    field: 'ResourceName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_6'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_7'));
                } else {
                    debugger
                    self.currentItem.WhsCode = data[0].ResourceCode;
                    self.currentItem.WhsName = data[0].ResourceName;
                }
            });

        }





        //弹窗选择物料编码
        function SelectMaterialCodeModal() {
            //自定义查询字段
            let queryParmeters = [
                { 'FieldCode': 'MaterialCode', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_8'), 'FiledType': 'Text' },
                { 'FieldCode': 'MaterialName', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_9'), 'FiledType': 'Text' },
                { 'FieldCode': 'Spec', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_10'), 'FiledType': 'Text' },
            ];

            //grid显示字段列表
            let columnDefs = [
                {
                    field: 'MaterialCode',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_8'),
                    width: 170
                },
                {
                    field: 'MaterialName',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_9'),
                    width: 170
                },
                {
                    field: 'Spec',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_10'),
                    width: 170
                },
                {
                    field: 'MaterialClassName',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_11'),
                    width: 170
                }
            ];
            let query = {
                FactoryCode: self.currentItem.FactoryCode
            }
            /*
            * 功能描述: 单选弹窗方法(精工 ui-grid列表), 自定义查询条件,显示列表字段
            * 创    建: 刘万军
            * 创建时间: 2021-1-22
            * 参    数:
            *       title    标题(最后生成如: 选择产品型号)
            *       PostUrl API接口
            *       queryParmeters  查询条件 参考 [{'FieldCode': 'purchaseOrderNo', 'FileldName':commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_12'),'FiledType':'Text'},{'FieldCode': 'poDate', 'FileldName':commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_13'),'FiledType':'Date'}]
            *       sidx  排序字段
            *       sord  排序方式
            *       columnDefs   grid显示字段列表
            *       callback  回调方法
            */

            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_8'), commonService.getMesApiAddress("material") + 'Base_MaterialFactory/Base_MaterialFactoryPageDataTableList', queryParmeters, 'MaterialCode', 'asc', columnDefs, Select_SingleChoiceModal_MaterialName_callback, query);
        }

        //公共弹窗回调方法
        function Select_SingleChoiceModal_MaterialName_callback(result_data) {
            console.info('Select_SingleChoiceModal_MaterialCode_callback_data', result_data);
            //物料编码编码
            //self.currentItem.MaterialCode = result_data.Materiel_Code;
            //物料编码
            self.currentItem.MaterialCode = result_data.MaterialCode;
            self.currentItem.MaterialName = result_data.MaterialName;
            self.currentItem.MaterialSpc = result_data.Spec;
            self.currentItem.UnitName = result_data.UnitName;
            self.currentItem.Unit = result_data.Unit;
        }

        //编辑保存
        function save() {

            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            //工厂名称 下拉取值
            self.currentItem.FactoryName = self.FactoryName.value.ResourceName;
            //仓库名称 下拉取值
            //self.currentItem.WhsName = self.WhsName.value.ItemCode;
            //安全库存数 数字最小验证
            if (self.currentItem.SafetyQty < 0 || self.currentItem.SafetyQty == null) {
                busyIndicatorService.hide();
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_14'));
                return;
            }
            // if (!!self.typeUnit.value) {
            //     self.currentItem.Unit = self.typeUnit.value.ItemValue;
            //     self.currentItem.UnitName=self.typeUnit.value.ItemName;
            // }


            console.log("postData------------------------------------" + JSON.stringify(self.currentItem));

            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItem.ModifyName = self.UserName;
            self.currentItem.ModifyBy = self.UserCode;
            if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                self.currentItem.ModifyName = self.UserCode;
            }
            console.log("username------------------------------------" + self.UserName);
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            console.log("postData------------------------------------" + JSON.stringify(postData));
            // commonService.getMesApiAddress() = '/sitSrvApi/'
            var url = commonService.getMesApiAddress("material") + 'MM_WarehouseSafetyStock/SaveMM_WarehouseSafetyStock';
            console.log("url----------------" + url);
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_15') });
            //提交数据
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            console.log("SaveMM_WarehouseSafetyStock----------------------" + JSON.stringify(req));
            busyIndicatorService.hide();
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
            console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_16'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_17'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MM_WarehouseSafetyStock.MM_WarehouseSafetyStockeditctrl.Tips_17'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MM_WarehouseSafetyStock_MM_WarehouseSafetyStock';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MM_WarehouseSafetyStock';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/MM_WarehouseSafetyStock-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Edit'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
