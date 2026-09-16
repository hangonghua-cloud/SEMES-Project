(function () {
    "use strict";

    angular.module('CCS.CommonApp').
        factory("commonTopSwacService", ['commonService', "common.widgets.globalDialog.service", 'common.services.swac.SwacUiModuleManager', 'common.base', '$state', '$rootScope', '$q', 'common.services.logger.service', 'common.services.signalManager', '$timeout',
            u4dmNotificationBarService
        ]);


    function u4dmNotificationBarService(commonService, globalDialogSerice, swacManager, commonBase, $state, $rootScope, $q, $log, signalMgr, $timeout) {
        var _this = {
            init: init
        };
        var badges = [];
        var subscribedToSignals = [];
        var currentOperation;
        var woId;
        var lastWo;
        var lastProcess;
        var notificationBarCommandClickEventName = 'notificationBarCommandClickEventName';
        var isInitialize = false;//Bug 235331: Notification Badge - Number inside the Notification are set 0 on home Page after logout
        var toBeDestroyed = [];
        var eventBusService;
        var notes;
        var currentState;
        var context = {};
        var dereisterICVSelectionEvent;

        var runtimeNotesStateName;
        var isWorkOrderLevel = false;
        var AppName = "MeasuringToolFBApp"//"BasicDataManageFBApp"
        var backendService;

        var retrieveNumber = 100;
        var lastError = undefined;
        var mapper = {};
        var counter = [];
        var lastErrors = [];
        var sidePanelManager

        return _this;

        /**
         * @name init
         * */
        function init() {
            backendService = commonBase.services.runtime.backendService;
            var notificationEvent = 'mom.sit.loadComplete';
            return swacManager.eventBusServicePromise.promise.then(function (eventBusSvc) {
                eventBusService = eventBusSvc;
                eventBusSvc.register(notificationEvent);
                eventBusSvc.event.subscribe(function (event) {
                    if (event.data.topic === notificationEvent && !isInitialize) {
                        let i = 0;
                        var v = {
                            ConfigKey: "MeasuringToolEarlywarningYellow",
                            ConfigIcon: "common/icons/cmdNoteProperties24.svg",
                            ConfigToolTip: commonService.$t('customCommon.ChangeLocation.Title'),
                            ConfigEnable: true,
                            ConfigShow: true,
                            CallBackType: "readingfunction",
                            FunctionName: "init",
                            FunctionOptions: "“”",
                            FunctionParams: "{}",
                            FunctionAppName: "CommonApp",
                            NavigationUrl: "home.ChangeLocation",
                        };
                        try {
                            badges = [{
                                key: v.ConfigKey,
                                icon: getIcon(v.ConfigIcon),//图标
                                onClick: function () {
                                    //alert("点击");
                                    noteClick(i);
                                },//点击事件
                                toolTip: v.ConfigToolTip,//'提示信息ToolTip',
                                signalName: v.ConfigSignalName,//订阅的事件
                                securityString: v.ConfigSecurityString,//订阅的事件全路径
                                signalCallBack: function () {
                                    // noteCallback({}, i)
                                },//订阅事件的回调函数
                                enable: v.ConfigEnable,
                                show: v.ConfigShow,
                                CallBackType: v.CallBackType,
                                FunctionName: v.FunctionName,
                                FunctionOptions: v.FunctionOptions,
                                FunctionParams: v.FunctionParams,
                                FunctionAppName: v.FunctionAppName,
                                NavigationUrl: v.NavigationUrl
                            }]
                            registerNotificationBarContext()
                                .then(updateNotificationBarCommands, function (reason) { })
                                //.then(subscribeSignals)
                                .then(function () {
                                    isInitialize = true;
                                });

                        } catch (error) {
                            //u4dmSvc.logger.error("initializeBadges error retrieving data", error);
                        }

                    }
                    else if (event.data.topic === notificationEvent && isInitialize) {
                        registerNotificationBarContext()
                            .then(updateNotificationBarCommands);
                    }
                });
                toBeDestroyed[toBeDestroyed.length] = function () {
                    return swacManager.eventBusServicePromise.promise.then(function (eventBusSvc) {
                        eventBusSvc.unregister(notificationEvent);
                    });
                };
                eventBusSvc.publish(notificationEvent);
            });
        }

        function showSelectOrganizeDialog() {
            sidePanelManager = commonBase.services.sidePanel.service;

            sidePanelManager.setTitle("123");
            sidePanelManager.open('e');

        }
        function noteClick(i) {
            //$state.go(runtimeNotesStateName, params);
            //$state.go('home.Siemens_SimaticIT_BasicDataManageFBApp_AppRoleManage_AppRoleScreen_AppRoleScreen', {});
            $state.go(badges[i].NavigationUrl, {});
            //showSelectOrganizeDialog();
        }


        function getIcon(iconFullPath) {
            return 'cmd' + getCommandSvgName(iconFullPath);
        }

        /**
      * 
      * @param {string} icon
      * @returns from "common/icons/cmdOpen24.svg" return "Open"
      */
        function getCommandSvgName(icon) {
            if (icon.indexOf("common/icons/cmd") === 0) {
                var extensionIndex = icon.lastIndexOf(".svg");
                return icon.substring(16, extensionIndex - 2);
            }
            else return icon;
        }
        //end  Message


        //end signal

        /**
         * @name registerNotificationBarContext
         * */
        function registerNotificationBarContext() {
            if (swacManager.enabled) {
                return swacManager.contextServicePromise.promise.then(function (contextSvc) {
                    var context = {};
                    for (var i = 0; i < badges.length; i++) {
                        context[badges[i].key] = {
                            count: badges[i].count ? badges[i].count : 0,
                            blink: false
                        };
                    }
                    return contextSvc.registerCtx('notificationBarContext', context);
                });
            }
            else return $q.reject('swacManager not enabled');
        }

        /**
         * @name updateNotificationBarCommands
         * */
        function updateNotificationBarCommands() {
            var cmds = {
                commands: {},
                commandHandlers: {},
                commandPlacements: {},
                actions: {}
            };
            var priority = 0;
            badges.forEach(function (badge) {
                cmds.commands[badge.key] = {
                    iconId: badge.icon,
                    title: badge.toolTip,
                    isToggle: true,
                    //template: '<div ng-show="!ctx.notificationBarContext.' + badge.key + '.blink">{{ctx.notificationBarContext.' + badge.key + '.count}}</div>' +
                    //   '<div ng-show="ctx.notificationBarContext.' + badge.key + '.blink" style="background-color: crimson; color: white;">{{ctx.notificationBarContext.' + badge.key + '.count}}</div>'
                };
                cmds.commandHandlers[badge.key] = {
                    id: badge.key,
                    action: 'load' + badge.key,
                    activeWhen: { condition: 'conditions.true' },
                    enableWhen: badge.enable,
                    visibleWhen: badge.show
                };
                cmds.commandPlacements[badge.key] = {
                    id: badge.key,
                    uiAnchor: 'mom_headerBar',
                    priority: priority + 100
                };
                cmds.actions['load' + badge.key] = {
                    actionType: 'JSFunction',
                    method: 'publish',
                    inputData: {
                        name: notificationBarCommandClickEventName,
                        data: {
                            id: badge.key
                        }
                    },
                    deps: 'js/eventBus'
                };
            });
            return swacManager.eventBusServicePromise.promise.then(function (eventBusSvc) {
                eventBusService = eventBusSvc;
                eventBusSvc.register(notificationBarCommandClickEventName);//see http://bulacco.industrysoftware.automation.siemens.com/mom-ui/docs/module-_MOM.UI.EventBus_.html
                eventBusSvc.event.subscribe(function (event) {
                    if (event.data.topic === notificationBarCommandClickEventName) {
                        notificationBarCommandClickEventHandler(event);
                    }
                });

                toBeDestroyed[toBeDestroyed.length] = function () {
                    return swacManager.eventBusServicePromise.promise.then(function (eventBusSvc) {
                        eventBusSvc.unregister(notificationBarCommandClickEventName);
                    });
                };
                return eventBusSvc.publish('mom.commands.update', cmds);
            });
        }

        /**
         * @name notificationBarCommandClickEventHandler
         * @param {any} event
         */
        function notificationBarCommandClickEventHandler(event) {
            var badge = getBadge(event.data.data.id);
            if (badge) badge.onClick();
        }

        /**
         * @name getOpenNotes
         * */
        //function getOpenNotes() {
        //    var query = "";
        //    var select = "&$select=Id,CloseDateTime,CloseUser,IsWorkOrderLevel,Message,OpenDateTime,OpenUser,Status,WorkOrder,WorkOrderOperation";
        //    if (currentOperation && !woId)
        //        query = "$filter=Status eq 'Open' and WorkOrderOperation eq " + currentOperation + select;
        //    else if (!currentOperation && woId)
        //        query = "$filter=Status eq 'Open' and WorkOrder eq " + woId + select;
        //    else if (currentOperation && woId)
        //        query = "$filter=Status eq 'Open' and (WorkOrderOperation eq " + currentOperation + " or WorkOrder eq " + woId + ")" + select;

        //    return u4dmSvc.data.getAllBackground(u4dmSvc.api.entityList.SnagAndNote, query).then(function (result) {
        //        if (notes.count != result.value.length) {
        //            notes.count = result.value.length;
        //        }
        //        sendUpdateCountersEvent();
        //        updateNotificationBarCommands();

        //    });
        //}

        /**
         * @name getBadge
         * @param {any} key
         */
        function getBadge(key) {
            // @ts-ignore
            return badges.find(function (b) { return b.key === key; });
        }

    }
}());