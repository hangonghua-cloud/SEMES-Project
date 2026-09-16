(function(){
    'use strict';

    angular.module('CCS.CommonApp.Common', []).config(StateConfig);

    StateConfig.$inject = ['$stateProvider'];
    function StateConfig($stateProvider) {
        var moduleStateName = 'home.CCS_CommonApp_Common';
        var moduleStateUrl = 'CCS_CommonApp_Common';
        var moduleFolder = 'CCS.CommonApp/modules/Common';

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
