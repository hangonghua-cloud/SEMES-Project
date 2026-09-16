(function(){
    'use strict';

    angular.module('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel', []).config(StateConfig);

    StateConfig.$inject = ['$stateProvider'];
    function StateConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_SystemApp_Sys_DataSegregateLabel';
        var moduleStateUrl = 'Siemens.SimaticIT_SystemApp_Sys_DataSegregateLabel';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/Sys_DataSegregateLabel';

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
