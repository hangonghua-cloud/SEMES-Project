/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 原材料库存检验记录表
*  2. 创建人员： 丁零
*  3. 创建日期： 2021-08-27
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList')
        .constant('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckList.constants', ScreenServiceConstants())
        .service('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckList.service', ScreenService)
        .run(ScreenServiceRun);
        
    function ScreenServiceConstants() {
        return {
            data: {
                //appName: 'QualityApp',
                appName: 'QualityApp', // The name of App: in case of extended app, it is the name of AppBase
                appPrefix: 'Siemens.SimaticIT',
                // TODO: Customize the entityName with the name of the entity defined in the App you want to manage within the UI Module.
                //       Customize the command name with the name of the command defined in the App you want to manage within the UI Module
                entityName: null,
                createPublicName: null,
                updatePublicName: null,
                deletePublicName: null,
                dictionaryEntityName: 'DataItemEntity',//数据字典
                factoryModelPublicName: 'GetFactoryModelCmd'//工厂建模
                
            }
        };
    }
    
    ScreenService.$inject = ['$q', '$state', 'common.base', 'Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckList.constants', 'common.services.logger.service'];
    function ScreenService($q, $state, base, context, loggerService) {
        var self = this;
        var logger, backendService;
        
        activate();
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckList.service');
            backendService = base.services.runtime.backendService;
            exposeApi();
        }
        
        function exposeApi() {
            self.getAll = getAll;
            self.create = createEntity;
            self.update = updateEntity;
            self.delete = deleteEntity;
            //获取数据字典
            self.getDectionary = getDectionary;
            //获取工厂建模
            self.getFactoryModel = getFactoryModel;
        }
        
        //获取数据字典
        function getDectionary(options) {
            return execGetDectionary(options);
        }
        
        //获取工厂建模
        function getFactoryModel(data){
            return execCommand(context.data.factoryModelPublicName, data);
        }
        
        //方法 获取数据字典
        function execGetDectionary(options) {
            return backendService.findAll({
                'appName': context.data.appName,
                'entityName': context.data.dictionaryEntityName,
                'options': options
            });
        }
        
        function getAll(options) {
            return execGetAll(options);
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
            var obj = {
                'Id': data.Id
            };
            return execCommand(context.data.updatePublicName, obj);
        }
        
        function deleteEntity(data) {
            // TODO: Customize the mapping between "UI entity" and the "DB entity" that will delete
            var obj = {
                'Id': data.Id
            };
            return execCommand(context.data.deletePublicName, obj);
        }
        
        function execGetAll(options) {
            return backendService.findAll({
                'appName': context.data.appName,
                'entityName': context.data.entityName,
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
    
    ScreenServiceRun.$inject = ['Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckList.constants', 'common.base'];
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
