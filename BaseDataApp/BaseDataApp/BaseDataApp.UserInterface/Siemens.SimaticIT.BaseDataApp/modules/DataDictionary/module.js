(function(){
    'use strict';

    angular.module('Siemens.SimaticIT.BaseDataApp.DataDictionary', [
        'ui.grid',
        'ui.grid.pagination',
        'ui.grid.selection',
        'ui.grid.resizeColumns',
        'jsTree.directive',
        'jsTreePlugin.directive']).config(StateConfig);

    StateConfig.$inject = ['$stateProvider'];
    function StateConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_BaseDataApp_DataDictionary';
        var moduleStateUrl = 'Siemens.SimaticIT_BaseDataApp_DataDictionary';
        var moduleFolder = 'Siemens.SimaticIT.BaseDataApp/modules/DataDictionary';

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
