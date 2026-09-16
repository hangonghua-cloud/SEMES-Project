(function () {
    'use strict';
    var app = angular.module('CCS.CommonApp', ['angularjs-dropdown-multiselect']);
    app.run(['commonTopSwacService', '$rootScope', '$state', '$stateParams', 'RESOURCE', 'THEMES', 'common',
        function (commonTopSwacService, $rootScope, $state, $stateParams, RESOURCE, THEMES, common) {
            commonTopSwacService.init();
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;
            common.shell.setEnvironment(RESOURCE, THEMES);

            $rootScope.globalOverlayData = {
                text: '',
                title: '',
                buttons: {}
            };

            $rootScope.globalBusyIndicator = {
                id: 'globalBusyIndicatorId',
                message: 'Working...',
                icon: 'fa fa-spinner fa-2x fa-spin'
            };

            common.authentication.init();


        }]);
})();
