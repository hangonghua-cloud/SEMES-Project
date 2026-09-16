(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EquipmentAccount').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccount.service', '$state',
        '$stateParams', 'common.base', '$filter', '$scope', 'FileUploader', 'commonService','$rootScope', 'common.widgets.notificationTile.globalService'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, FileUploader, commonService,$rootScope, notificationService) {

        var self = this;
        var sidePanelManager, backendService, propertyGridHandler, uploader;
        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountarchivesctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = angular.copy($stateParams.selectedItem);
            console.log("self.currentItem -------------------- " + JSON.stringify(self.currentItem));
            self.validInputs = false;
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.addButtonHandler = addButtonHandler;
            //文件类型
            self.archives_fileType = {
                options: [],
                value: {},
                keep: 'ItemValue',//ItemValue
                label: 'ItemName'

            };
            archives_fileType_dic();
            uploader = $scope.uploader = new FileUploader({
                url: commonService.getMesApiAddress("equipment") + 'UploadFile/Upload'
              });

            // FILTERS
            // uploader.filters.push({
            //     name: 'imageFilter',
            //     fn: function (item /*{File|FileLikeObject}*/, options) {
            //         var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
            //         return '|jpg|png|jpeg|bmp|gif|doc|docx|xls|xlsx|ppt|pptx|pdf|'.indexOf(type) !== -1;
            //     }
            // });

            // uploader.filters.push({
            //     name: 'syncFilter',
            //     fn: function(item /*{File|FileLikeObject}*/, options) {
            //         console.log('syncFilter');
            //         return this.queue.length < 10;
            //     }
            // });

            // an async filter
            uploader.filters.push({
                name: 'asyncFilter',
                fn: function (item /*{File|FileLikeObject}*/, options, deferred) {
                    console.log('asyncFilter');
                    setTimeout(deferred.resolve, 1e3);
                }
            });

            // CALLBACKS
            uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {
                console.info('onWhenAddingFileFailed', item, filter, options);
            };
            uploader.onAfterAddingFile = function (fileItem) {

                if (self.currentItem.archives_fileType) {
                    var myPostDatas = {
                        EquipmentCategoryCode: self.currentItem.EquipmentCategoryCode,
                        FileType: self.currentItem.archives_fileType.value.ItemValue,
                        EquipmentName: self.currentItem.EquipmentName,
                        EquipmentCode: self.currentItem.EquipmentCode,
                        Note: self.currentItem.Note,
                        userName:commonService.getLoginUser().loginName
                    };
                    fileItem.formData = [myPostDatas];
                    console.log("formData-------------" + JSON.stringify(fileItem.formData));
                }
                else {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountarchivesctrl.Tips_2'), commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountarchivesctrl.Tips_3'));
                } 
            };
            uploader.onAfterAddingAll = function (addedFileItems) {
                console.info('onAfterAddingAll', addedFileItems);
            };
            uploader.onBeforeUploadItem = function (item) {
                console.info('onBeforeUploadItem', item);
            };
            uploader.onProgressItem = function (fileItem, progress) {
                console.info('onProgressItem', fileItem, progress);
            };
            uploader.onProgressAll = function (progress) {
                console.info('onProgressAll', progress);
            };
            uploader.onSuccessItem = function (fileItem, response, status, headers) {
                debugger;
                console.info('onSuccessItem------------------', fileItem, response, status, headers);
                if (response && response.success) {
                    fileItem._file.filePath = response.ResultData;
                    //触发父窗口的事件
                    $rootScope.$emit('to-parent', 'parent'); 
                    notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountarchivesctrl.Tips_4'));
                }
                else { 
                    //backendService.genericError(response.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountarchivesctrl.Tips_3'));
                    //alert(response.returnMsg);
                    $rootScope.$emit('to-parent', 'parent'); 
                    notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountarchivesctrl.Tips_5') + response.returnMsg);
                }
            };
            uploader.onErrorItem = function (fileItem, response, status, headers) { 
                console.info('onErrorItem-----------------', fileItem, response, status, headers);
            };
            uploader.onCancelItem = function (fileItem, response, status, headers) {
                console.info('onCancelItem', fileItem, response, status, headers);
            };
            uploader.onCompleteItem = function (fileItem, response, status, headers) {
                console.info('onCompleteItem', fileItem, response, status, headers);
            };
            uploader.onCompleteAll = function () {
                console.info('onCompleteAll');
            }; 
        }
        //文件类型
        function archives_fileType_dic() {
            var url = commonService.getDataItemDuatil("archives_fileType_dic").then(function (res) {
                var resultData = res.data.resultData;
                self.archives_fileType.options = resultData;
            });
        }
        function addButtonHandler() { }
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            var queueCount = uploader.queue.length;
            if (queueCount > 0) {
                //提交所有文档
                uploader.uploadAll();
                sidePanelManager.close();
                $state.go('^', {}, { reload: true });
            }
            else {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountarchivesctrl.Tips_6'), commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountarchivesctrl.Tips_3'));
            }
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            sidePanelManager.close();
            $state.go('^', {}, { reload: true });
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EquipmentAccount_EquipmentAccount';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EquipmentAccount'; 
        var state = {
            name: screenStateName + '.archives',
            url: '/archives',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EquipmentAccount-archives.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'add'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
