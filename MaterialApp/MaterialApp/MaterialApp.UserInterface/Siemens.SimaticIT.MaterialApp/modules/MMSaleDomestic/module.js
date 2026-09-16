(function(){
    'use strict';

    angular.module('Siemens.SimaticIT.MaterialApp.MMSaleDomestic', []).config(StateConfig);

    StateConfig.$inject = ['$stateProvider'];
    function StateConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_MMSaleDomestic';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_MMSaleDomestic';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MMSaleDomestic';

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
