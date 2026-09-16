export function formatDate(date, fmt) {
	if (/(y+)/.test(fmt)) {
		fmt = fmt.replace(RegExp.$1, (date.getFullYear() + '').substr(4 - RegExp.$1.length));
	}
	let o = {
		'M+': date.getMonth() + 1,
		'd+': date.getDate(),
		'h+': date.getHours(),
		'm+': date.getMinutes(),
		's+': date.getSeconds()
	};
	for (let k in o) {
		if (new RegExp(`(${k})`).test(fmt)) {
			let str = o[k] + '';
			fmt = fmt.replace(RegExp.$1, (RegExp.$1.length === 1) ? str : padLeftZero(str));
		}
	}
	return fmt;
};

function padLeftZero(str) {
	return ('00' + str).substr(str.length);
};

/**
 *  filters.js
 * 对Date的扩展，将 Date 转化为指定格式的String  默认是2019-11-25 14:00:00 需要格式则后续传值
 * 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符，
 * 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)
 * (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2019-11-25 08:09:04.423
 * (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2019-11-25 8:9:4.18
 * 使用格式,dom上 {{formatTime(time,'YYYY-MM-DD')}}
 * 在script中 this.formatTime(this.time,"hh:mm:ss")
 * 
 */
import Vue from 'vue'
Date.prototype.Format = function(fmt) {
	var o = {
		"M+": this.getMonth() + 1, //月份
		"D+": this.getDate(), //日
		"h+": this.getHours(), //小时
		"m+": this.getMinutes(), //分
		"s+": this.getSeconds(), //秒
		"q+": Math.floor((this.getMonth() + 3) / 3), //季度
		"S": this.getMilliseconds() //毫秒
	};
	if (/(Y+)/.test(fmt))
		fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
	for (var k in o)
		if (new RegExp("(" + k + ")").test(fmt))
			fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k])
				.length)));
	return fmt;
}
const formatTime = function(times, pattern) {
	if (!times)
		return "";
	var d = new Date(new Date(times).setHours(new Date(times).getHours() - 8)).Format("YYYY-MM-DD hh:mm:ss");
	if (pattern) {
		d = new Date(new Date(times).setHours(new Date(times).getHours() - 8)).Format(pattern);
	}
	return d.toLocaleString();
}
export default formatTime
