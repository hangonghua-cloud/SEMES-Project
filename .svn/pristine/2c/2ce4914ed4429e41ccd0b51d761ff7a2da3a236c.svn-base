(function () {
    'use strict';

    var app = angular.module('CCS.CommonApp');

    /**
    * @usage
    * As an element:
    * ```
    * <sit-text-picker sit-value="value" sit-validation="validation" ng-readonly="ngReadonly" ng-blur="ngBlur" sit-change="sitChange"
    *       ng-disabled="ngDisabled" ng-focus="ngFocus">
    * </sit-text-picker>
    * ```
    * @restrict E
    *
    * @param {string} sit-value Value of the text widget.
    * @param {ValidationModel} sit-validation See {@link ValidationModel}.
    * @param {string} [ng-blur] _(Optional)_ An expression to evaluate on blur event.
    * @param {string} [sit-change] _(Optional)_ An expression to evaluate on change of value.
    * @param {string} [ng-disabled] _(Optional)_ If this expression is truthy, the element will be disabled.
    * @param {string} [ng-focus] _(Optional)_ An expression to evaluate on focus event.
    * @param {string} [ng-readonly] _(Optional)_ If this expression is truthy, the element will be set as read-only.
    *
    */
    AlpSelectPickerController.$inject = ['$scope'];
    function AlpSelectPickerController(scope) {
        var vm = this;
        let dataList = [];
        var newValue = [];
        var changeOldValue = [];
        function loadOptions() {
            $('#multiple_ID').empty();
            if (changeOldValue.length > 0 && vm.value.length > 0) {
                vm.value = null;
            }
            dataList = [];

            vm.options.forEach(element => {
                if (element[vm.toKeep]) {
                    let info = {
                        id: element[vm.toKeep],
                        text: element[vm.toDisplay],
                        selected: element.selected ? element.selected : false,
                        disabled: element.disabled ? element.disabled : false,
                        ...element
                    }
                    if (info.selected == true) {
                        newValue.push(info);
                    }
                    dataList.push(info);
                }
            });
            $('#multiple_ID').select2({
                // theme: "classic",
                data: dataList,
                language: 'zh-CN',
                placeholder: vm.placeholder,//默认文字提示
                closeOnSelect: false,
            });
            $('#multiple_ID').on("select2:select", (e) => {
                changeValue(e.params.data);
            });
            $('#multiple_ID').on("select2:unselect", (e) => {
                changeValue(e.params.data);
            });
            function changeValue(data) {
                if (data.selected) {
                    newValue.push(data);
                } else {
                    newValue = newValue.filter(x => x.id != data.id);
                }
                vm.sitChange(null, newValue);
            }
        }
        scope.LoadWaitHTML = () => {//页面初始化完成
            $('#multiple_ID').select2({
                // theme: "classic",
                data: [{ id: '请选择', text: "请选择", disabled: true }],
                language: 'zh-CN',
                placeholder: "",//默认文字提示
                closeOnSelect: false,
            });
            // alert("页面加载完成");
        }
        var optionListner = scope.$watchCollection(function () {
            return vm.options;
        }, function (newValue, oldValue) {
            changeOldValue = oldValue;
            if (JSON.stringify(newValue) !== JSON.stringify(oldValue)) {
                loadOptions();
                // if (vm.allowReset) {
                //     addResetValue();
                // }
            }
        });
        scope.$on('$destroy', function () {
            optionListner();
        });
    }
    app.controller('AlpSelectPickerController', AlpSelectPickerController);

    app.directive('sitAlpSelectPicker', function () {
        return {
            bindToController: {
                'readOnly': '=?sitReadOnly',
                'value': '=?sitValue',//集合
                'model': '=sitModel',
                'options': '=?sitOptions',
                'validation': '=?sitValidation',
                'ngBlur': '&?',
                'sitChange': '=?',
                'ngDisabled': '=?',
                'ngFocus': '&?',
                'ngReadonly': '=?',
                'placeholder': '=?sitPlaceholder',
                'toDisplay': '=sitToDisplay',//转换text
                'toKeep': '=sitToKeep',//转换id
            },

            scope: {},

            restrict: 'E',

            controller: 'AlpSelectPickerController',

            controllerAs: 'alpSelectPickerCtrl',

            templateUrl: 'CCS.CommonApp/widgets/sitAlpSelectPicker/sit-alp-select-picker.html'
        };
    });
})();