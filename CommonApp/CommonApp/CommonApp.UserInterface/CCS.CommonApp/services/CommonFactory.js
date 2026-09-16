(function () {
    "use strict";

    angular.module('CCS.CommonApp').
        factory("CommonFactory", ["$q", 'common.base', 'commonService',
            function ($q, base, commonService) {
                var backendService = base.services.runtime.backendService;

                var results = {};
                //var logger  = loggerService.getModuleLogger('CCS.CommonApp.common.service');
                function err(err) {
                    console.log("******************CommonFactory.err******************");
                    //  logger.logError(err);
                }

                //部门及数据隔离标记
                var getLoginUseConfig = commonService.getLoginUseConfig();

                var deferred = $q.defer();
                var p = $q.all([
                    getLoginUseConfig.then(
                        function (res) {

                            //console.log("******************resresresres******************" + JSON.stringify(res.data.resultData));
                            results['LoginUseConfig'] = res.data.resultData;

                        }
                    ).catch(err)
                ]).then(function () {
                    //console.log(results);
                    deferred.resolve(results);
                    return deferred.promise;
                })
                return p;

            }]);
}());