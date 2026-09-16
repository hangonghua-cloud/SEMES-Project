/* SIMATIC IT Unified Architecture Foundation V4.1 | Copyright (C) Siemens AG 2020. All Rights Reserved. */
/*
 * add new states to $stateProvider
 */
(function () {
    'use strict';

    var unityApp = angular.module('siemens.simaticit.common.services.layout');



    unityApp.config(['$stateProvider', function ($stateProvider) {
        var ChangeLocation = {
            name: 'home.ChangeLocation',
            url: '/ChangeLocation',
            views: {
                'Canvas@': {
                    templateUrl: 'CCS.CommonApp/layout/ChangeLocation/ChangeLocation.tpl.html'
                }
            },
            data: {
                title: 'customCommon.ChangeLocation.Title'
            }
        };
        $stateProvider.state(ChangeLocation);
    }]);


    function ChangeLocationCtrl(commonService, $rootScope, $scope, $filter, CONFIG, APPCONFIG, common, notificationTileService, LOG_LEVELS, loggerConfig, $modal, RESOURCE, THEMES) {
        /*jshint validthis: true */
        var sc = this;
        // var ChangeLocation = common.loadChangeLocation();

        function whileLoading(callback) {
            angular.element('html').addClass('sit-ui-loading');
            callback();
            setTimeout(function () {
                angular.element('html').removeClass('sit-ui-loading');
            }, 1000);
        }

        sc.tiles = [];
        sc.sampleTileData = {
            title: '',
            description: '',
            count: '',
            image: 'sit sit-log',
            selected: false
        };
        sc.tileLayout = { size: 'medium' };
        sc.tileType = {
            theme: 0,
            language: 1,
            log: 2,
            debug: 3
        };


        sc.notificationTileTypes = { warning: 'warning', info: 'info' };
        sc.notificationTile = {
            id: 'ChangeLocationNotificationTile',
            title: '',
            content: '',
            type: sc.notificationTileTypes.info,
            counter: '',
            clickCallback: null,
            position: 'bottomRight',
            popup: true
        };

        /*THEMES*/
        //init Themes data
        $rootScope.themes.forEach(function (item, idx) {
            var dataElem = angular.extend({}, sc.sampleTileData, sc.tileLayout, { datatype: sc.tileType.theme });
            dataElem.title = item.name;
            dataElem.description = item.name + ' Theme';
            dataElem.count = idx;
            dataElem.id = item.id;
            if ($rootScope.currentTheme.id === item.id) {
                dataElem.selected = true;
            }
            sc.tiles.push(dataElem);
        });
        sc.isCurrentTheme = function (id) {
            return $rootScope.currentTheme.id === id;
        };

        sc.themeSelected = function (themeId) {
            whileLoading(function () {
                sc.tiles.forEach(function (item) {
                    if (item.id === themeId) {
                        item.selected = true;
                        $rootScope.currentTheme = $rootScope.themes.filter(function (item) {
                            return item.id === themeId;
                        })[0];
                    } else {
                        item.selected = false;
                    }
                });
            });
        };

        /*LOG*/
        sc.loglevel = [];
        var Type = commonService.locationTypes;

        Object.keys(Type).forEach(function (item, idx) {
            var dataElem = angular.extend({}, sc.sampleTileData, sc.tileLayout, { datatype: sc.tileType.log }, { id: item, value: Type[item] });
            dataElem.count = idx;
            dataElem.selected = Type[item] === window.localStorage.getItem("locationType");

            switch (dataElem.value) {
                case Type.CN:
                    dataElem.description = commonService.$t('customCommon.ChangeLocation.Country.cn_remark');
                    dataElem.title = commonService.$t('customCommon.ChangeLocation.Country.cn');
                    break;
                case Type.VN:
                    dataElem.description = commonService.$t('customCommon.ChangeLocation.Country.vn_remark');
                    dataElem.title = commonService.$t('customCommon.ChangeLocation.Country.vn');
                    break;
                case Type.TH:
                    dataElem.description = commonService.$t('customCommon.ChangeLocation.Country.th_remark');
                    dataElem.title = commonService.$t('customCommon.ChangeLocation.Country.th_remark');
                    break;
                default: break;

            }
            sc.loglevel.push(dataElem);
        });

        sc.logSelected = function (tileContent) {
            window.localStorage.setItem("locationType", tileContent.value);
            sc.loglevel.forEach(function (item) {
                item.selected = item.value === window.localStorage.getItem("locationType");
            });
        };

        sc.openModal = function (themeId) {
            switch (themeId) {
                case 1:
                    $modal.open({
                        template: '<img class="ui-ChangeLocation-theme-image" src="common/images/ThemeDark.png">'

                    });
                    break;
                case 0:
                    $modal.open({
                        template: '<img class="ui-ChangeLocation-theme-image" src="common/images/ThemeLight.png">'
                    });
                    break;
                default: break;
            }
        };

        sc.saveChangeLocation = function () {
            var ChangeLocation = { 'location': "CN" };
            // ChangeLocation.theme = $rootScope.currentTheme;
            ChangeLocation.location = window.localStorage.getItem("locationType");

            common.saveSettings(ChangeLocation);

            sc.notificationTile.title = $filter('translate')('ChangeLocation.notificationTile.title');
            sc.notificationTile.content = $filter('translate')('ChangeLocation.notificationTile.saveText');
            notificationTileService.shownotificationTile(sc.notificationTile.id);
            commonService.changeLocation();
        };

        sc.restoreChangeLocation = function () {
            whileLoading(function () {
                common.removeChangeLocation();
                common.shell.setEnvironment(RESOURCE, THEMES);
                ChangeLocation = null;
                loggerConfig.config.currentLogLevel = LOG_LEVELS.LOG_VERBOSE;
                sc.loglevel.forEach(function (item) {
                    item.selected = item.value === loggerConfig.config.currentLogLevel;
                });
                sc.notificationTile.title = $filter('translate')('ChangeLocation.notificationTile.title');
                sc.notificationTile.content = $filter('translate')('ChangeLocation.notificationTile.restoreText');
                notificationTileService.shownotificationTile(sc.notificationTile.id);
            });
        };

        /*Events*/
        $scope.$on('sit-tile.clicked', function (event, tileContent) {
            switch (tileContent.datatype) {
                case sc.tileType.log:
                    sc.logSelected(tileContent);
                    break;
                default: break;
            }
        });


    }

    unityApp.controller('ChangeLocationCtrl', ['commonService', '$rootScope', '$scope', '$filter', 'CONFIG', 'APPCONFIG', 'common', 'common.widgets.notificationTile.service', 'LOG_LEVELS',
        'common.services.logger.config', '$uibModal', 'RESOURCE', 'THEMES', ChangeLocationCtrl]);

})();
