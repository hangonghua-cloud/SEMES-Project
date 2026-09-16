/**
 * 对Date的扩展，将 Date 转化为指定格式的String
 * 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
 * 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
 * 例子： 
 * (new Date()).format("yyyy-MM-dd hh:mm:ss.S") ==> 2015-11-04 08:09:04.423 
 * (new Date()).format("yyyy-M-d h:m:s.S")      ==> 2015-11-4 8:9:4.18 
 */
Date.prototype.format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
		"h+": this.getHours()%12 == 0 ? 12 : this.getHours()%12,//12小时制
        "H+": this.getHours(), //24小时制
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
    if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

/**
 * 字符串转换为日期
 * 例：
 * strToDate("2015-11-04 12:00:00")
 * strToDate("2015-11-04")
 */
function strToDate(date){
	return new Date(date.replace(/-/g,"/"));
}

function addMonth(date, addMonth){  
	if(typeof(date)=="string"){
		date=strToDate(date);
	}
	return new Date(date.getFullYear(), (date.getMonth()) + addMonth, date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds());;  
}

/**
 * 日期加减
 * date可为Date对象或者"2015-11-04"、"2015-11-04 12:00:00"格式字符串
 * addDay 为整数（要加减的天数），减天数传入负数即可
 */
function addDate(date, addDay){  
	if(typeof(date)=="string"){
		date=strToDate(date);
	}
	date = date.valueOf();
	date = date + addDay * 24 * 60 * 60 * 1000;
	return new Date(date);  
}
function addHour(date, addHour){  
	if(typeof(date)=="string"){
		date=strToDate(date);
	}
	date = date.valueOf();
	date = date + addHour * 60 * 60 * 1000;
	return new Date(date);  
}

function addMinute(date, addMinute){  
	if(typeof(date)=="string"){
		date=strToDate(date);
	}
	date = date.valueOf();
	date = date + addMinute * 60 * 1000;
	return new Date(date);  
}

function addSecond(date, addSecond){  
	if(typeof(date)=="string"){
		date=strToDate(date);
	}
	date = date.valueOf();
	date = date + addSecond * 1000;
	return new Date(date);  
}

/**
 * 获取指定月份的天数
 * @param year
 * @param month
 * @returns
 */
function getDayCountOfMonth(year,month) {
	var day=new Date(year,month,0);
	return day.getDate();
}

/**
 * 返回月份最后一天
 * @param date，格式yyyy-MM
 * @returns
 */
function getLastDayOfMonth(date) {
	var dateArray=date.split("-");
	var year=dateArray[0];
	var month=dateArray[1];
	return date+"-"+getDayCountOfMonth(year,month);
}
