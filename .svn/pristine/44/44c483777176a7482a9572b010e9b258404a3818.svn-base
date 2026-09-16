(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PMPostCoefficient').config(ViewScreenStateConfig);
    
    ViewScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PMPostCoefficient.PMPostCoefficient.service', '$state', '$stateParams',
         'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
         'common.widgets.busyIndicator.service', '$uibModal','$rootScope'];
    function ViewScreenController(dataService, $state, $stateParams, 
         common, $filter, $scope, commonService, auth, notificationService,
         busyIndicatorService, $modal,$rootScope) {
         var self = this;
         var sidePanelManager, backendService, propertyGridHandler;
    
         activate();
    
         // Initialization function
         function activate() {
             init();
             
             sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PMPostCoefficient.selectJS.Tips_1'));
             sidePanelManager.open('e');
             //sidePanelManager.open({
             //  mode: "e",
             //   size: "wide"
             //});
         }
         
         function init() {
             sidePanelManager = common.services.sidePanel.service;
             backendService = common.services.runtime.backendService;
         
             //Initialize Model Data
             self.currentItem = angular.copy($stateParams.selectedItem);
             self.validInputs = false;
             
              //Expose Model Methods
             self.cancel = cancel;
             
             //数据字典
             initDictionary();
         }
         
         function initDictionary() {
             
             self.typeMaterialClass = {
                 value: { text: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPostCoefficient.selectJS.Tips_2'), value: "" },
                 options: { text: commonService.$t('Siemens.SimaticIT.ProductionApp.PMPostCoefficient.selectJS.Tips_2'), value: "" },
             }
             
             commonService.getDataItemDuatil("").then(function (res) {
                 if (res && res.data.success) {
                     self.typeMaterialClass.options = res.data.resultData;
                     self.typeMaterialClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                 }
                 self.typeMaterialClass.value = self.typeMaterialClass.options.find(t => t.ItemValue == self.currentItem.MaterialClass);
             })
         }
         
         //取消
         function cancel() {
             sidePanelManager.close();//关闭侧边栏
             $state.go('^');//返回列表(父页面)
         }
    }
         ViewScreenStateConfig.$inject = ['$stateProvider'];
         function ViewScreenStateConfig($stateProvider) {
             var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_PMPostCoefficient_PMPostCoefficient';
             var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PMPostCoefficient';
         
         var state = {
             name: moduleStateName + '.select',
             url: '/select/:id',
             views: {
                 'property-area-container@': {
                     templateUrl: moduleFolder + '/PMPostCoefficient-select.html',
                     controller: ViewScreenController,
                     controllerAs: 'vm'
                 }
             },
             data: {
                 title: 'Siemens.SimaticIT.ProductionApp.PMPostCoefficient.selectJS.Tips_1'
             },
             params: {
                 selectedItem: null
             }
         };
         $stateProvider.state(state);
     }
}());
