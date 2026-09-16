(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BasicDataManageFBApp.BaseManage')
        .constant('Siemens.SimaticIT.BasicDataManageFBApp.BaseManage.BaseManageScreen.constants', ScreenServiceConstants())
        .service('Siemens.SimaticIT.BasicDataManageFBApp.BaseManage.BaseManageScreen.service', ScreenService)
        .run(ScreenServiceRun);

    function ScreenServiceConstants() {
        return {
            data: {
                //appName: 'BasicDataManageFBApp',
                appName: 'BasicDataManageFBApp', // The name of App: in case of extended app, it is the name of AppBase
                appPrefix: 'Siemens.SimaticIT',
                // TODO: Customize the entityName with the name of the entity defined in the App you want to manage within the UI Module.
                //       Customize the command name with the name of the command defined in the App you want to manage within the UI Module
                entityName: 'DataItemEntity',
                createPublicName: 'DataItemAddCmd',
                createParentPublicName: 'DataItemCategoryAddCmd',
                updatePublicName: 'DataItemUpdateCmd',
                updateParentPublicName: 'DataItemCategoryUpdateCmd',
                selectTreePublicName: 'GetTreeDataCmd',
                deletePublicName: 'DataItemDelCmd',
                deleteParentPublicName: 'DataItemCategoryDelCmd'
            }
        };
    }

    ScreenService.$inject = ['$q', '$state', 'common.base', 'Siemens.SimaticIT.BasicDataManageFBApp.BaseManage.BaseManageScreen.constants', 'common.services.logger.service', 'common.widgets.notificationTile.globalService'];
    function ScreenService($q, $state, base, context, loggerService, notificationService) {
        var self = this;
        var logger, backendService;

        activate();
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.BasicDataManageFBApp.BaseManage.BaseManageScreen.service');
            backendService = base.services.runtime.backendService;
            exposeApi();
        }

        function exposeApi() {
            self.getAll = getAll;
            self.create = createEntity;
            self.createParent = createParentEntity;
            self.update = updateEntity;
            self.updateParent = updateParentEntity;
            self.selectTree = selectTreeEntity;
            self.delete = deleteEntity;
            self.deleteParent = deleteParentEntity;
        }

        function getAll(options) {
            return execGetAll(options);
        }

        function selectTreeEntity(options) {
            return execGetTreeAll(options);
        }

        function createEntity(data) {
            // TODO: Customize the mapping between "UI entity" and the "DB entity" that will create 
            var obj = {
                'CategoryCode': data.CategoryCode,
                'ItemCode': data.ItemCode,
                'ItemName': data.ItemName,
                'ItemValue': data.ItemValue,

                'SortNum': data.SortNum,
                'IsValid': data.IsValid
            };
            //return execCommand(context.data.createPublicName, obj);
            return execCommand(context.data.createPublicName, obj).then(function (data) {
                if ((data) && (data.succeeded)) {
                    notificationService.info(data.data.ReturnVal);
                } else {
                    notificationService.warning(data.data.ReturnVal);
                }
            }, backendService.backendError);
        }

        function createParentEntity(data) {
            // TODO: Customize the mapping between "UI entity" and the "DB entity" that will create 
            var obj = {
                'CategoryCode': data.CategoryCode,
                'CategoryName': data.CategoryName,
                'ParentId': data.ParentId,
                'SortNum': data.SortNum,
                'IsValid': true
            };
            //return execCommand(context.data.createParentPublicName, obj);
            return execCommand(context.data.createParentPublicName, obj).then(function (data) {
                if ((data) && (data.succeeded)) {
                    notificationService.info(data.data.ReturnVal);
                } else {
                    notificationService.warning(data.data.ReturnVal);
                }
            }, backendService.backendError);
        }

        function updateEntity(data) {
            // TODO: Customize the mapping between "UI entity" and the "DB entity" that will create
            var obj = {
                'Id': data.Id,
                //'CategoryCode': data.CategoryCode,
                //'ItemCode': data.ItemCode,
                'ItemName': data.ItemName,
                'ItemValue': data.ItemValue,
                'SortNum': data.SortNum,
                'IsValid': data.IsValid
            };
            //return execCommand(context.data.updatePublicName, obj);
            return execCommand(context.data.updatePublicName, obj).then(function (data) {
                if ((data) && (data.succeeded)) {
                    notificationService.info(data.data.ReturnVal);
                } else {
                    notificationService.warning(data.data.ReturnVal);
                }
            }, backendService.backendError);
        }

        function updateParentEntity(data) {
            // TODO: Customize the mapping between "UI entity" and the "DB entity" that will create
            var obj = {
                'Id': data.Id,
                //'CategoryCode': data.CategoryCode,
                'CategoryName': data.CategoryName,
                'ParentId': data.ParentId,
                'SortNum': data.SortNum,
                'IsValid': data.IsValid
            };
            //return execCommand(context.data.updateParentPublicName, obj);
            return execCommand(context.data.updateParentPublicName, obj).then(function (data) {
                if ((data) && (data.succeeded)) {
                    notificationService.info(data.data.ReturnVal);
                } else {
                    notificationService.warning(data.data.ReturnVal);
                }
            }, backendService.backendError);
        }

        function deleteEntity(data) {
            // TODO: Customize the mapping between "UI entity" and the "DB entity" that will delete
            var obj = {
                'Id': data.Id
            };
            return execCommand(context.data.deletePublicName, obj);
            // return execCommand(context.data.deletePublicName, obj).then(function (data) {
            //     if ((data) && (data.succeeded)) {
            //         notificationService.info(data.data.ReturnVal);
            //     } else {
            //         notificationService.warning(data.data.ReturnVal);
            //     }
            // }, backendService.backendError);
        }

        function deleteParentEntity(data) {
            // TODO: Customize the mapping between "UI entity" and the "DB entity" that will delete
            var obj = {
                'Id': data.Id
            };
            return execCommand(context.data.deleteParentPublicName, obj);
            // return execCommand(context.data.deleteParentPublicName, obj).then(function (data) {
            //     if ((data) && (data.succeeded)) {
            //         notificationService.info(data.data.ReturnVal);
            //     } else {
            //         notificationService.warning(data.data.ReturnVal);
            //     }
            // }, backendService.backendError);
        }

        function execGetAll(options) {
            return backendService.findAll({
                'appName': context.data.appName,
                'entityName': context.data.entityName,
                'options': options
            });
        }

        function execGetTreeAll(data) {
            // TODO: Customize the mapping between "UI entity" and the "DB entity" that will create
            return execCommand(context.data.selectTreePublicName, null);
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

    ScreenServiceRun.$inject = ['Siemens.SimaticIT.BasicDataManageFBApp.BaseManage.BaseManageScreen.constants', 'common.base'];
    function ScreenServiceRun(context, common) {
        if (!context.data.entityName) {
            common.services.logger.service.logWarning('Configure the entityName');
        };
        if (!context.data.createPublicName) {
            common.services.logger.service.logWarning('Configure the createPublicName');
        };
        if (!context.data.createParentPublicName) {
            common.services.logger.service.logWarning('Configure the createParentPublicName');
        };
        if (!context.data.deletePublicName) {
            common.services.logger.service.logWarning('Configure the deletePublicName');
        };
        if (!context.data.deleteParentPublicName) {
            common.services.logger.service.logWarning('Configure the deleteParentPublicName');
        };
        if (!context.data.updatePublicName) {
            common.services.logger.service.logWarning('Configure the updatePublicName');
        };
        if (!context.data.updateParentPublicName) {
            common.services.logger.service.logWarning('Configure the updateParentPublicName');
        };
    }
}());
