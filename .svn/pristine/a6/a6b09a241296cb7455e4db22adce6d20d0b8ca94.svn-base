(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BasicDataManageFBApp.AppModelManage').
        controller('AppModelScreen-select',
            ['Siemens.SimaticIT.BasicDataManageFBApp.AppModelManage.AppModelScreen.service', '$state', '$stateParams',
                'common.base', '$filter', '$scope', '$uibModalInstance', 'data', 'common.services.authentication', 'commonService', '$rootScope', 'common.widgets.notificationTile.globalService',
                function (dataService, $state, $stateParams, common, $filter, $scope, $modalInstance, data, auth, commonService, $rootScope, notificationService) {
                    var self = this;
                    var sidePanelManager, backendService, propertyGridHandler;
                    activate();
                    function activate() {
                        init();
                        registerEvents();
                    }
                    function init() {
                        sidePanelManager = common.services.sidePanel.service;
                        backendService = common.services.runtime.backendService;

                        self.data = angular.copy(data).selectedItem;
                        setTimeout(() => {
                            $('#selectedName').html(self.data.selectedName);
                            $('#icoClass').attr('class', self.data.icoClass);
                            $('#icoName').html(self.data.icoName);
                        }, 100);

                        self.lang = 'zh-cn';
                        self.validInputs = false;

                        self.save = save;
                        self.cancel = cancel;
                    }

                    function registerEvents() {
                        $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
                    }

                    function onPropertyGridValidityChange(event, params) {
                        self.validInputs = params.validity;
                    }

                    function save() {
                        var obj = {
                            icoClass: $('#icoClass')[0].className,
                            icoName: $('#icoName').html(),
                            selectedName: $('#selectedName').html()
                        };
                        $modalInstance.close(obj);
                    }

                    function cancel() {
                        $modalInstance.dismiss();
                    }
                }
            ])
}());

