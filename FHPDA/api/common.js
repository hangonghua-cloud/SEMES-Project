import request from '@/utils/request'

// 获取表结构
export function CallGetEntity(data) {
	return request({
		url: '/api/Test/CallGetEntity',
		method: 'POST',
		data: data
	})
}

//FuHua 获取数据字典下拉框
export function GetDictionary(data) {
	return request({
		url: '/Base/GetList_DataItemByFather_PDA',
		method: 'POST',
		data: data
	})
}
//FuHua 取指定层级下的所有资源
export function GetModelResourceExtendInfoByLevelCode(data) {
	return request({
		url: '/BaseManage/BsModelLevel/GetModelResourceExtendInfoByLevelCode',
		method: 'POST',
		data: data
	})
}


//FuHua 根据子集获取父级
export function GetListByProductionMachine(data) {
	return request({
		url: '/BaseManage/BsModelLevel/GetListByProductionMachine',
		method: 'POST',
		data: data
	})
}



//FuHua 根据子集获取父级
export function GetModelResourceByChild(data) {
	return request({
		url: '/BaseManage/BsModelLevel/GetModelResourceByChild',
		method: 'POST',
		data: data
	})
}


//Fuhua 模糊查询人员信息
export function GetUserList(data) {
	return request({
		url: '/Base/GetUserList',
		method: 'POST',
		data: data
	})
}
//Fuhua 模糊查询物料信息
export function GetBaseMaterialList(data) {
	return request({
		url: '/Base/GetBaseMaterialList',
		method: 'POST',
		data: data
	})
}

// 获取工厂建模下拉框
export function GetFactoryModel(data) {
	return request({
		url: '/Base/GetFactoryModel',
		method: 'POST',
		data: data
	})
}


//自动打印功能控制
export function SelfAction(data) {
	return request({
		url: '/BS_IsOpen/SelfAction',
		method: 'POST',
		data: data
	})
}

//获取功能是否开启
export function GetOpenType(data) {
	return request({
		url: '/BS_IsOpen/GetOpenType',
		method: 'POST',
		data: data
	})
}

// 取指定层级下所有资源
export function GetResourceByLevelCode(data) {
	return request({
		url: '/BaseManage/BsModelLevel/GetModelResourceExtendInfoByLevelCode',
		method: 'POST',
		data: data
	})
}

// 根据工厂获取工序
export function GetProcessModel(data) {
	return request({
		url: '/BaseManage/BsModelLevel/GetProcessByFactory',
		method: 'POST',
		data: data
	})
}
// 根据工厂获取仓库
export function GetWarehouseByFactory(data) {
	return request({
		url: '/BaseManage/BsModelLevel/GetWarehouseByFactory',
		method: 'POST',
		data: data
	})
}
// 根据工厂、属性获取仓库
export function GetWarehouseByFactoryExtendInfo(data) {
	return request({
		url: '/BaseManage/BsModelLevel/GetWarehouseByFactoryExtendInfo',
		method: 'POST',
		data: data
	})
}

//FuHua 根据父级获取子集
export function GetListByParentResource(data) {
	return request({
		url: '/BaseManage/BsModelLevel/GetListByParentResource',
		method: 'POST',
		data: data
	})
}
