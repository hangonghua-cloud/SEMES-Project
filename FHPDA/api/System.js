import request from '@/utils/request'

// 获取表结构
export function CallGetDemoData(data) {
	return request({
		url: '/api/Test/CallGetDemoData',
		method: 'POST',
		data: data
	})
}