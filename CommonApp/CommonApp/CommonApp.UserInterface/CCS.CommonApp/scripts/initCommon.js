//ERP 接口 与 跨域地址
var erpUrl = 'http://10.13.131.6/SAP_API/';
var erpUrl_Origin = 'http://10.13.131.6';
//ERP 接口 与 跨域地址  正式
//var erpUrl = 'http://10.13.132.8/SAP_API/';
//var erpUrl_Origin = 'http://10.13.132.8';
//隐藏用户首选项功能（自定义列）
try {
    var timer = setInterval(function () {
        if (document.getElementById("ButtonCommandUser Preference")) {
            document.getElementById("ButtonCommandUser Preference").setAttribute("style", "display:none");
        } else {
            //clearInterval(timer);
        }
    }, 500);
} catch{ }

//自定义添加快速搜索框
function AddSortSearch_txt(self, searchWhere, sortQueryfield, optionsStr) {
    var custom_sortSearch_txt = '<li> ' +
        '<div><div><sit-search index="8" sit-type="search" sit-placeholder="搜索条件 ' + self.sortQueryfieldName + '"><div class="command-hide"></div> ' +
        '<div class="contextual-search-container"><input id="sortQueryTxt" type="text" ' +
        'placeholder="搜索条件 ' + self.sortQueryfieldName + '" title="搜索条件 ' + self.sortQueryfieldName + '" class="form-control contextual-quick-search ng-pristine ng-valid  ' +
        'ng-empty ng-touched"><em id="search_ico" onclick="clickQuery()" sit-class="svg-icon" sit-mom-icon="searchCtrl.svgIcons.search" ' +
        'class="fa fa-search contextual-search-icon momIcon"><svg icon-name="cmdSearch24" version="1.1" id="Artwork" ' +
        'x="0px" y="0px" width="24px" height="24px" viewBox="0 0 24 24" enable-background="new 0 0 24 24" xml:space="preserve" class="svg-icon"> ' +
        '<path class="aw-theme-iconOutline" fill="#464646" d="M14.5,0C9.3,0,5,4.3,5,9.5c0,1.9,0.6,3.7,1.6,5.2l-5.9,5.9c-0.7,0.7-0.7,2,0,2.7C1,' +
        '23.8,1.5,24,2,24c0,0,0,0,0,0c0.5,0,1-0.2,1.3-0.6l5.9-6c1.5,1,3.3,1.6,5.2,1.6c5.2,0,9.5-4.3,9.5-9.5S19.7,0,14.5,0z M2.6,22.7 ' +
        'c-0.2,0.2-0.4,0.3-0.6,0.3h0c-0.2,0-0.5-0.1-0.7-0.3c-0.4-0.4-0.4-0.9,0-1.3l5.8-5.8C7.6,16,8,16.4,8.5,16.8L2.6,22.7z M14.5,18 ' +
        'C9.8,18,6,14.2,6,9.5S9.8,1,14.5,1S23,4.8,23,9.5S19.2,18,14.5,18z"></path> ' +
        '</svg></em></div></sit-search></div></li>';
    //return custom_sortSearch_txt;
    try {
        var timer = setInterval(function () {
            if (!document.getElementById("sortQueryTxt")) {
                $($('.commandBar-container-list-contextual')[0]).append(custom_sortSearch_txt);
                //自定义添加过滤显示隐藏按钮
                //Add_filter_txt(self.viewerOptions, "itemlist1");
            } else {
                clearInterval(timer);
                document.onkeydown = function (e) {
                    var ev = document.all ? window.event : e;
                    if (ev.keyCode == 13 && $("#sortQueryTxt").is(":focus")) {
                        var fil = self.optionsStr;
                        if ($('#sortQueryTxt').val() && $('#sortQueryTxt').val() != '' && $('#sortQueryTxt').val() != null) {
                            fil += " and contains(" + self.sortQueryfield + ",'" + $('#sortQueryTxt').val() + "')";
                        }
                        self.viewerOptions.serverDataOptions.optionsString = fil;
                        self.viewerOptions.refresh();
                    }
                }
            }
        }, 100);
    } catch{ }
}

function clickQuery() {
    $('#sortQueryTxt').focus();
    var f = jQuery.Event("keydown");//模拟一个键盘事件
    f.keyCode = 13;//keyCode=13是回车
    $('#sortQueryTxt').trigger(f);//模拟页码框按下回车
}

//拼接筛选条件
function GetQueryWhereStr(obj, optionsStr) {
    //obj[i].filterField.searchType == 'eq'//等于
    //obj[i].filterField.searchType == 'ne'//不等于
    //obj[i].filterField.searchType == 'gt'//大于
    //obj[i].filterField.searchType == 'ge'//大于等于
    //obj[i].filterField.searchType == 'lt'//小于
    //obj[i].filterField.searchType == 'le'//小于等于
    //obj[i].filterField.searchType == 'co'//包含  DateTimeOffset
    var fil = optionsStr;
    for (var i = 0; i < obj.length; i++) {
        if (obj[i].value && obj[i].value != '') {
            if (obj[i].widget == 'sit-datepicker' || obj[i].widget == 'sit-date-time-picker') {
                if (obj[i].filterField.type == 'string') {
                    if ($.inArray(obj[i].filterField.searchType, ['eq', 'ne', 'gt', 'ge', 'lt', 'le']) >= 0) {//
                        var dd = GetDateStr(obj[i].value + '', obj[i].filterField.dateType, '');
                        fil += " and " + obj[i].filterField.field + " " + obj[i].filterField.searchType + " '" + dd + "' ";
                    }
                } else if (obj[i].filterField.type.toLowerCase() == 'date' || obj[i].filterField.type.toLowerCase() == 'datetime' || obj[i].filterField.type.toLowerCase() == 'datetimeoffset') {
                    var startTime = convertJSONDate(obj[i].value + '', obj[i].filterField.searchType, obj[i].filterField.type);
                    fil += " and " + obj[i].filterField.field + " " + obj[i].filterField.searchType + " " + startTime + " ";
                }
            } else if (obj[i].widget == 'sit-typeahead') {
                fil += " and " + obj[i].filterField.field + "  eq '" + obj[i].value.value + "' ";
            } else if (obj[i].widget == 'sit-time-picker') {

            } else if (obj[i].widget == 'sit-multi-select') {

            } else if (obj[i].widget == 'sit-numeric') {
                if ($.inArray(obj[i].filterField.searchType, ['eq', 'ne', 'gt', 'ge', 'lt', 'le'])) {//
                    fil += " and " + obj[i].filterField.field + " " + obj[i].filterField.searchType + " " + obj[i].value + " ";
                }
            } else if (obj[i].widget == 'sit-radio') {
                if (obj[i].filterField.type == 'string') {
                    fil += " and " + obj[i].filterField.field + "  eq '" + obj[i].value + "' ";
                }
                else {
                    fil += " and " + obj[i].filterField.field + "  eq " + obj[i].value + " ";
                }
            } else {
                if (obj[i].filterField.searchType == 'eq') {//等于
                    if (obj[i].filterField.type == 'bool') {
                        fil += " and " + obj[i].filterField.field + "  eq " + obj[i].value + " ";
                    } else {
                        fil += " and " + obj[i].filterField.field + "  eq '" + obj[i].value + "' ";
                    }
                } else if (obj[i].filterField.searchType == 'co') {//包含
                    fil += " and contains(" + obj[i].filterField.field + ",'" + obj[i].value + "')";
                } else {//默认是等于
                    fil += " and " + obj[i].filterField.field + "  eq '" + obj[i].value + "' ";
                }
            }
        }
    }
    // if (sortQueryTxt != '' && sortQueryTxt != null && sortQueryTxt != undefined) {
    //     fil += " and contains(" + storQueryField + ",'" + sortQueryTxt + "')";
    // }
    return fil;
}

function GetMonthStr(mon) {
    var re = '';
    switch (mon) {
        case 'Jan':
            re = '01';
            break;
        case '01':
            re = '01';
            break;
        case 'Feb':
            re = '02';
            break;
        case '02':
            re = '02';
            break;
        case 'Mar':
            re = '03';
            break;
        case '03':
            re = '03';
            break;
        case 'Apr':
            re = '04';
            break;
        case '04':
            re = '04';
            break;
        case 'May':
            re = '05';
            break;
        case '05':
            re = '05';
            break;
        case 'Jun':
            re = '06';
            break;
        case '06':
            re = '06';
            break;
        case 'Jul':
            re = '07';
            break;
        case '07':
            re = '07';
            break;
        case 'Aug':
            re = '08';
            break;
        case '08':
            re = '08';
            break;
        case 'Sep':
            re = '09';
            break;
        case '09':
            re = '09';
            break;
        case 'Oct':
            re = '10';
            break;
        case '10':
            re = '10';
            break;
        case 'Nov':
            re = '11';
            break;
        case '11':
            re = '11';
            break;
        case 'Dec':
            re = '12';
            break;
        case '12':
            re = '12';
            break;
        default:
            re = '';
            break;
    }
    return re;
}

function GetIsFebDay(year, mon) {
    var re = '';
    if (mon == '01' || mon == '03' || mon == '05' || mon == '07' || mon == '08' || mon == '10' || mon == '12') {
        re = '31';
    } else if (mon == '04' || mon == '06' || mon == '09' || mon == '11') {
        re = '30';
    } else if (mon == '02') {
        if ((year % 4 == 0 && year % 100 != 0) || (year % 100 == 0 && year % 400 == 0)) {
            re = '29';
        } else {
            re = '28';
        }
    }
    return re;
}

function convertJSONDate(jsonDate, searchType, dateType) {
    var tz = 8;//国内默认为+08时区，暂不做计算
    var DateArray = (jsonDate + '').split(' ');
    var returnFormat = '';
    var dt;
    var m = GetMonthStr(DateArray[1]);
    if (dateType == 'date') {
        if (searchType == 'gt' || searchType == 'ge') {
            dt = DateArray[3] + '-' + m + '-' + DateArray[2] + 'T00:00:00.0000000';
            returnFormat = 'yyyy-MM-ddTHH:00:00.0000000Z';
        } else if (searchType == 'lt' || searchType == 'le') {
            dt = DateArray[3] + '-' + m + '-' + DateArray[2] + 'T23:59:59.9999999';
            returnFormat = 'yyyy-MM-ddTHH:59:59.9999999Z';
        }
    } else if (dateType == 'datetimeoffset' || dateType == 'datetime') {
        if (searchType == 'gt' || searchType == 'ge') {
            dt = DateArray[3] + '-' + m + '-' + DateArray[2] + 'T' + DateArray[4] + '.0000000';
            returnFormat = 'yyyy-MM-ddTHH:mm:ss.0000000Z';
        } else if (searchType == 'lt' || searchType == 'le') {
            dt = DateArray[3] + '-' + m + '-' + DateArray[2] + 'T' + DateArray[4] + '.9999999';
            returnFormat = 'yyyy-MM-ddTHH:mm:ss.9999999Z';
        }
    }
    var myDate = new Date(dt);
    myDate.setHours(myDate.getHours() - tz);
    return myDate.format(returnFormat);
}

function GetDateStr(str, ty, t) {
    var re = '';
    var DateArray = str.split(' ');
    var m = GetMonthStr(DateArray[1]);
    if (ty == 'D') {
        re = DateArray[3] + '-' + m + '-' + DateArray[2];
    } else if (ty == 'M') {
        // re = DateArray[3] + '-' + GetMonthStr(DateArray[1]);
        if (t == 'S') {
            re = DateArray[3] + '-' + m + '-01';
        } else if (t == 'E') {
            re = DateArray[3] + '-' + m + '-' + GetIsFebDay(parseInt(DateArray[3]), m);
        }
    } else if (ty == 'Y') {
        if (t == 'S') {
            re = DateArray[3] + '-01-01';
        } else if (t == 'E') {
            re = DateArray[3] + '-12-31';
        }
    } else {
        re = DateArray[3] + '-' + m + '-' + DateArray[2];
    }
    return re;
}

//拼接数据字典获取查询条件
function GetDictionaryFilter(obj) {
    var fil = "$filter=(IsDeleted eq 0 and IsValid eq true) ";
    if (obj.length > 0) {
        fil += " and (1 eq 2 ";
        for (var i = 0; i < obj.length; i++) {
            fil += " or CategoryCode eq '" + obj[i] + "'";
        }
        fil += ")&$orderby=SortNum asc ";
    }
    return fil;
}


/**
* 功能描述: 自定义添加过滤显示隐藏按钮
* 创    建: 刘万军
* 创建时间: 2021-4-27 16:30:40
*@param viewerOptions  原生grid控件变量self.viewerOptions
*/
function Add_filter_txt(viewerOptions, itemlistID) {
    console.log(viewerOptions.filterBarOptions);
    //如果有sf 就是存在过滤器 就不显示这个按钮
    if (viewerOptions.filterBarOptions == 'sf') {
        return;
    }
    var custom_sortSearch_txt = '<li> '
        + '<div ng-switch="cmdCtrl.showas" class="" style="">'
        + ' <div ng-switch-when="Button" id="ButtontoggleFilter" class="contextual-button">'
        + '<button  data-internal-type="command-button-command-bar" title="过滤" class="contextual-command-button">'
        + ' <span data-internal-type="image-container"  class="fa-lg MainCommandImage">'
        + '   <em onclick="clickfilterShowHide(\'' + itemlistID + '\')" class="fa  momIcon contextual-command-image contextual-command-label-image"'
        + '      sit-mom-icon="cmdCtrl.command.displayIcon" sit-class="command-mom-icon">'
        + '         <svg icon-name="cmdFilter24" version="1.1" id="Artwork" xmlns="http://www.w3.org/2000/svg"'
        + '         xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="24px"'
        + '         height="24px" viewBox="0 0 24 24" enable-background="new 0 0 24 24" xml:space="preserve"'
        + '         class="command-mom-icon">'
        + '             <path class="aw-theme-iconOutline" fill="#464646" d="M23,3c0-2.1-5.7-3-11-3S1,0.9,1,3c0,0.2,0.1,0.5,0.2,0.6L9,15.5v8c0,0.2,0.1,0.3,0.3,0.4C9.3'
        + '             ,24,9.4,24,9.5,24c0.1,0,0.2,0,0.3-0.1l5-3c0.2-0.1,0.2-0.3,0.2-0.4v-5l7.8-11.9C22.9,3.5,23,3.2,23,3z M12,1c6.5,0,10,1.3,10,2s-3.5'
        + '             ,2-10,2S2,3.7,2,3S5.5,1,12,1z M14.1,15.1C14,15.2,14,15.3,14,15.4v4.8l-4,2.4v-7.2c0-0.1,0-0.2-0.1-0.3L3.2,4.9C5.4'
        + '             ,5.6,8.8,6,12,6c3.3,0,6.6-0.4,8.8-1.1L14.1,15.1z">'
        + '             </path>'
        + '         </svg>'
        + '     </em>'
        + ' </span>'
        + '</button>'
        + '</div>'
        + '</div></li>';
    try {
        var timer = setInterval(function () {
            //console.log($('.' + itemlistID + ' ' + '.commandBar-container-list-contextual')[0]);
            //插入到最前部
            $($('.' + itemlistID + ' ' + '.commandBar-container-list-contextual')[0]).prepend($(custom_sortSearch_txt));
            clearInterval(timer);
        }, 100);
    } catch { }
}

/**
* 功能描述: 显示隐藏查询框
* 创    建: 刘万军
* 创建时间: 2021-4-27 16:30:40
*/
function clickfilterShowHide(itemlistID) {
    if (itemlistID == undefined || itemlistID == null || itemlistID == '') {
        $('.property-grid-container').toggle();
    }
    else {
        $('#' + itemlistID).toggle();
    }
}

/**
* 功能描述: 对象克隆方法
* 创    建: 刘万军
* 创建时间: 2021-6-02 16:30:40
* @param obj 被复制的对象实体
*/
var clone = function (obj) {
    function Temp() { };  //新建空构造函数
    Temp.prototype = obj;  //把参数对象赋值给该构造函数的原型方法
    return new Temp();  //返回实例化后的对象
}