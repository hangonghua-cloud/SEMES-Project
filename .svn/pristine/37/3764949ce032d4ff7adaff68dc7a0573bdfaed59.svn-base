(function ( {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch'.config(AddScreenStateConfig;
    
    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatch.service', '$state', '$stateParams',
         'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
         'common.widgets.busyIndicator.service', '$uibModal','$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, 
         common, $filter, $scope, commonService, auth, notificationService,
         busyIndicatorService, $modal,$rootScope {
         var self = this;
         var sidePanelManager, backendService, propertyGridHandler;
    
         activate(;
    
         // Initialization function
         function activate( {
             init(;
             registerEvents(;
             
             sidePanelManager.setTitle('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchaddctrl.Tips_1';
             sidePanelManager.open('e';
             //sidePanelManager.open({
             //  mode: "e",
             //   size: "wide"
             //};
         }
         
         function init( {
             sidePanelManager = common.services.sidePanel.service;
             backendService = common.services.runtime.backendService;
         
             //Initialize Model Data
             self.currentItem = null;
             self.validInputs = false;
             
              //Expose Model Methods
             self.save = save;
             self.cancel = cancel;
             
             //数据字典
             initDictionary(;
         }
         
         function initDictionary( {
             
             self.typeMaterialClass = {
                 value: { text: 'Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchaddctrl.Tips_2', value: "" },
                 options: { text: 'Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchaddctrl.Tips_2', value: "" },
             }
             
             commonService.getDataItemDuatil("".then(function (res {
                 if (res && res.data.success {
                     self.typeMaterialClass.options = res.data.resultData;
                     self.typeMaterialClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                 }
             }
         }
         
         function save( {
         
             //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
             var postData = {
                 KeyValue: '',
                 Entity: self.currentItem
             };
         
             busyIndicatorService.show({ message: 'Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchaddctrl.Tips_3' };
             var url = commonService.getMesApiAddress( + 'MMRawMaterialDispatch/SaveForm';
         var req = commonService.callWebApiPost(url, postData.then(onSaveSuccess, onSaveError;
         }
         
         //取消
         function cancel( {
             sidePanelManager.close(;//关闭侧边栏
             $state.go('^';//返回列表(父页面
         }
         
         //保存成功事件
         function onSaveSuccess(data {
             if (data.data.success {
                 busyIndicatorService.hide(;//关闭遮罩层
                 sidePanelManager.close(;//关闭侧边栏
                 commonService.showInfo('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchaddctrl.Tips_4';
                 $rootScope.$emit('to-parent', 'parent';//刷新局部
                 $state.go('^', {}, { reload: false };
             } else {
                 busyIndicatorService.hide(;
                 backendService.genericError(data.data.returnMsg, 'Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchaddctrl.Tips_5';
             }
         }
         
         //保存失败事件
         function onSaveError(error {
             busyIndicatorService.hide(;
             backendService.genericError('[' + error.status + '] - ' + error.statusText, 'Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchaddctrl.Tips_5';
         }
         
         function registerEvents( {
             $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange;
         }
         
         function onPropertyGridValidityChange(event, params {
             self.validInputs = params.validity;
         }
    }
         AddScreenStateConfig.$inject = ['$stateProvider'];
         function AddScreenStateConfig($stateProvider {
             var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_MMRawMaterialDispatch_MMRawMaterialDispatch';
             var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MMRawMaterialDispatch';
         
         var state = {
             name: moduleStateName + '.add',
             url: '/add',
             views: {
                 'property-area-container@': {
                     templateUrl: moduleFolder + '/MMRawMaterialDispatch-add.html',
                     controller: AddScreenController,
                     controllerAs: 'vm'
                 }
             },
             data: {
                 title: 'Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchaddctrl.Tips_6'
             }
         };
         $stateProvider.state(state;
     }
}(;
