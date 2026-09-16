(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BasicDataManageFBApp.AppRoleManage')
        .constant('Siemens.SimaticIT.BasicDataManageFBApp.AppRoleManage.AppRoleScreen.constants', ScreenServiceConstants())
        .service('Siemens.SimaticIT.BasicDataManageFBApp.AppRoleManage.AppRoleScreen.service', ScreenService)
        .run(ScreenServiceRun);

    function ScreenServiceConstants() {
        return {
            data: {
                //appName: 'BasicDataManageFBApp',
                appName: 'BasicDataManageFBApp', // The name of App: in case of extended app, it is the name of AppBase
                appPrefix: 'Siemens.SimaticIT',
                // TODO: Customize the entityName with the name of the entity defined in the App you want to manage within the UI Module.
                //       Customize the command name with the name of the command defined in the App you want to manage within the UI Module
                entityName: "V_UARoles",
                readPublicName: "GetAppRolesRF",
                createPublicName: null,
                updatePublicName: "SaveAppRolesConfiCmd",
                deletePublicName: "DelAppModelCmd"
            }
        };
    }

    ScreenService.$inject = ['$q', '$state', 'common.base', 'Siemens.SimaticIT.BasicDataManageFBApp.AppRoleManage.AppRoleScreen.constants', 'common.services.logger.service'];
    function ScreenService($q, $state, base, context, loggerService) {
        var self = this;
        var logger, backendService;

        activate();
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.BasicDataManageFBApp.AppRoleManage.AppRoleScreen.service');
            backendService = base.services.runtime.backendService;
            exposeApi();
        }

        function exposeApi() {
            self.getAll = getAll;
            self.create = createEntity;
            self.update = updateEntity;
            self.delete = deleteEntity;
            self.readRF = readRF;
        }

        function readRF(obj) {
            return execReadingFunction(context.data.readPublicName, obj);
        }

        function getAll(options) {
            return execGetAll(options, context.data.entityName);
        }

        function createEntity(data) {
            // TODO: Customize the mapping between "UI entity" and the "DB entity" that will create 
            var obj = {
                'Id': data.Id
            };
            return execCommand(context.data.createPublicName, obj);
        }

        function updateEntity(data) {
            // TODO: Customize the mapping between "UI entity" and the "DB entity" that will create
            return execCommand(context.data.updatePublicName, data);
        }

        function deleteEntity(data) {
            return execCommand(context.data.deletePublicName, data);
        }

        function execReadingFunction(publicName, params) {
            logger.logDebug('Executing RF.......', publicName);
            return backendService.read({
                'appName': context.data.appName,
                'functionName': publicName,
                'params': params
            });
        }

        function execGetAll(options, entityName) {
            return backendService.findAll({
                'appName': context.data.appName,
                'entityName': entityName,
                'options': options
            });
        }

        function execCommand(publicName, params) {
            logger.logDebug('Executing command.......', publicName);
            return backendService.invoke({
                'appName': context.data.appName,
                'commandName': publicName,
                'params': params
            });
        }
    }

    ScreenServiceRun.$inject = ['Siemens.SimaticIT.BasicDataManageFBApp.AppRoleManage.AppRoleScreen.constants', 'common.base'];
    function ScreenServiceRun(context, common) {
        if (!context.data.entityName) {
            common.services.logger.service.logWarning('Configure the entityName');
        };
        if (!context.data.createPublicName) {
            common.services.logger.service.logWarning('Configure the createPublicName');
        };
        if (!context.data.deletePublicName) {
            common.services.logger.service.logWarning('Configure the deletePublicName');
        };
        if (!context.data.updatePublicName) {
            common.services.logger.service.logWarning('Configure the updatePublicName');
        };
    }
}());
