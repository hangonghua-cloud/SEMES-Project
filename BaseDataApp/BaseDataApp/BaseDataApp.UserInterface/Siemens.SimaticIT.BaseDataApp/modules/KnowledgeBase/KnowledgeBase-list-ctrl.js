(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BaseDataApp.KnowledgeBase').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.BaseDataApp.KnowledgeBase.KnowledgeBase.service', '$state',
        '$stateParams', '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'uiGridConstants', 'i18nService', '$http',
        'common.widgets.notificationTile.globalService', 'common.services.authentication', 'commonService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base,
        loggerService, uiGridConstants, i18nService, $http, notification, $auth, commonService) {
        var self = this;
        var logger, rootstate, messageservice, backendService, rootstateDataItemDetail;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.BaseDataApp.KnowledgeBase.KnowledgeBase');

            init();
            //initGridOptions();
            //initGridData();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_BaseDataApp_KnowledgeBase_KnowledgeBase';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};

            ////http://172.168.11.122/apk/JSFH-MES.apk
            self.clickModel = clickModel;
            self.downloadApi = "http://172.168.11.122";

        }

        function clickModel(obj) {
            if (obj == "AppDown") {
                window.location.href = self.downloadApi + "/apk/JSFH-MES.apk";
            } else if (obj == "UMC") {
                window.open(self.downloadApi + "/umc");
            } else if (obj == "Honeywell") {
                window.location.href = self.downloadApi + "/apk/Honeywell.zip";
            } else if (obj == "CS") {
                window.location.href = self.downloadApi + "/apk/MesSetup.exe";
            } else if (obj == "CS_TH") {
                window.location.href = self.downloadApi + "/apk/setup_TH.exe";
            } else if (obj == "FrameWork") {
                window.location.href = self.downloadApi + "/apk/FrameWork4.7.exe";
            } else if (obj == "worldDown") {
                window.location.href = self.downloadApi + "/apk/富华MES下载及扫码枪设置流程.docx";
            } else if (obj == "Google") {
                window.location.href = self.downloadApi + "/apk/Gboard.apk";
            }
        }



        // Internal function to make item-specific buttons visible
        function setButtonsVisibility(visible) {
            self.isButtonVisible = visible;
        }
    }

    ListScreenRouteConfig.$inject = ['$stateProvider'];
    function ListScreenRouteConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_BaseDataApp_KnowledgeBase';
        var moduleStateUrl = 'Siemens.SimaticIT_BaseDataApp_KnowledgeBase';
        var moduleFolder = 'Siemens.SimaticIT.BaseDataApp/modules/KnowledgeBase';

        var state = {
            name: moduleStateName + '_KnowledgeBase',
            url: '/' + moduleStateUrl + '_KnowledgeBase',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/KnowledgeBase-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.BaseDataApp.KnowledgeBase.KnowledgeBaselistctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
