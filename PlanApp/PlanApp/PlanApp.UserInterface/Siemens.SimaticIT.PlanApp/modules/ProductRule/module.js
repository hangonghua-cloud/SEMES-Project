(function(){
    'use strict';

    angular.module('Siemens.SimaticIT.PlanApp.ProductRule', []).config(StateConfig);

    StateConfig.$inject = ['$stateProvider'];
    function StateConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_PlanApp_ProductRule';
        var moduleStateUrl = 'Siemens.SimaticIT_PlanApp_ProductRule';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/ProductRule';

        //Add new states under the root state to be unique. Below is an example code for reference
        //var state1 = {
        //    name: moduleStateName + '_state_name',
        //    url: '/' + moduleStateUrl + '_state_url',
        //    views: {
        //        'Canvas@': {
        //            templateUrl: moduleFolder + '/state_template.html',
        //            controller: 'state_controller',
        //            controllerAs: 'vm'
        //        }
        //    },
        //    data: {
        //        title: 'state_title'
        //    },
        //    params: {}
        //};
        //$stateProvider.state(state1);
    }
}());
