(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BasicDataManageFBApp.AppUserManage')
        .constant('Siemens.SimaticIT.BasicDataManageFBApp.AppUserManage.AppUserScreen.constants', ScreenServiceConstants())
        .service('Siemens.SimaticIT.BasicDataManageFBApp.AppUserManage.AppUserScreen.service', ScreenService)
        .run(ScreenServiceRun);

    function ScreenServiceConstants() {
        return {
            data: {
                //appName: 'BasicDataManageFBApp',
                appName: 'BasicDataManageFBApp', // The name of App: in case of extended app, it is the name of AppBase
                appPrefix: 'Siemens.SimaticIT',
                // TODO: Customize the entityName with the name of the entity defined in the App you want to manage within the UI Module.
                //       Customize the command name with the name of the command defined in the App you want to manage within the UI Module
                entityName: null,
                createPublicName: null,
                updatePublicName: "UpdateAppUserCmd",
                resetPwdPublicName: "ResetAppPwdCmd",
                getUserPublicName: "GetUAUsersCmd",
                deletePublicName: null
            }
        };
    }

    ScreenService.$inject = ['$q', '$state', 'common.base', 'Siemens.SimaticIT.BasicDataManageFBApp.AppUserManage.AppUserScreen.constants', 'common.services.logger.service'];
    function ScreenService($q, $state, base, context, loggerService) {
        var self = this;
        var logger, backendService;

        activate();
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.BasicDataManageFBApp.AppUserManage.AppUserScreen.service');
            backendService = base.services.runtime.backendService;
            exposeApi();
        }

        function exposeApi() {
            self.getAll = getAll;
            self.create = createEntity;
            self.update = updateEntity;
            self.resetPwd = resetPwd;
            self.delete = deleteEntity;
            self.getUsers = getUsers;
        }

        function getUsers() {
            var obj = {};
            return execCommand(context.data.getUserPublicName, obj);
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

        function resetPwd(data) {
            // TODO: Customize the mapping between "UI entity" and the "DB entity" that will create
            var obj = {
                'Id': data.Id,
                'Pwd': "123"
            };
            return execCommand(context.data.resetPwdPublicName, obj);
        }

        function deleteEntity(data) {
            return execCommand(context.data.deletePublicName, data);
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

    ScreenServiceRun.$inject = ['Siemens.SimaticIT.BasicDataManageFBApp.AppUserManage.AppUserScreen.constants', 'common.base'];
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
