import request from '@/utils/request'

// 获取表结构
export function CallGetDemoData(data) {
	return request({
		url: '/api/Test/CallGetDemoData',
		method: 'POST',
		data: data
	})
}
// 获取设备保养工单
export function CallGetEquipmentMatitainOrderData(data) {
	return request({
		url: '/EP_EquipmentMatitainOrder/EP_EquipmentMatitainOrderPageList',
		method: 'POST',
		data: data
	})
}

//获取设备保养工单明细
export function CallGetEP_EquipmentMaintainDetailData(data) {
	return request({
		url: '/EP_EquipmentMaintainDetail/EP_EquipmentMaintainDetailPageList',
		method: 'POST',
		data: data
	})
}
//FuHua 设备点检-根据设备编码获取点检任务
export function GetEquipmentCheckByCode(data) {
	return request({
		url: '/Equipment/GetEquipmentCheckByCode',
		method: 'POST',
		data: data
	})
}
//FuHua 设备点检-保存设备点检记录
export function SaveEquipmentTackResult(data) {
	return request({
		url: '/Equipment/SaveEquipmentTackResult',
		method: 'POST',
		data: data
	})
}
//FuHua 设备点检-保存设备点检记录查询
export function GetDataTableCheckResult(data) {
	return request({
		url: '/Equipment/GetDataTableCheckResult',
		method: 'POST',
		data: data
	})
}

//FuHua 设备故障报修-根据设备编码获取设备信息
export function GetEquipmentManageByCode(data) {
	return request({
		url: '/Equipment/GetEquipmentManageByCode',
		method: 'POST',
		data: data
	})
}
//FuHua 设备故障报修-设备故障报修保存
export function SaveEquipmentMalfunctionRepair(data) {
	return request({
		url: '/Equipment/SaveEquipmentMalfunctionRepair',
		method: 'POST',
		data: data
	})
}
//FuHua 设备维修-获取设备报修记录 -未修理
export function GetEquipmentMalfunctionRepair(data) {
	return request({
		url: '/Equipment/GetEquipmentMalfunctionRepair',
		method: 'POST',
		data: data
	})
}

//FuHua 设备故障维修- 设备维修记录保存
export function SaveEquipmentRepairRecord(data) {
	return request({
		url: '/Equipment/SaveEquipmentRepairRecord',
		method: 'POST',
		data: data
	})
}


//FuHua 根据时间范围和设备类别 获取未保养的任务 -未保养 by 刘万军
export function GetEquipmentMaintainTaskByDateAndType(data) {
	return request({
		url: '/EP_EquipmentMaintainTask/GetEquipmentMaintainTaskByDateAndType',
		method: 'POST',
		data: data
	})
}

//FuHua 根据保养任务编码查询保养项目 by 刘万军
export function GetEP_EquipmentMaintainDetailList(data) {
	return request({
		url: '/EP_EquipmentMaintainTask/GetEP_EquipmentMaintainDetailList',
		method: 'POST',
		data: data
	})
}

//FuHua 设备点检-保存设备点检记录 by 刘万军
export function SaveEP_EquipmentMaintainTask(data) {
	return request({
		url: '/EP_EquipmentMaintainTask/SaveEP_EquipmentMaintainTask',
		method: 'POST',
		data: data
	})
}

//更新工单状态为待保养
export function UpdateMatitainOrderStateByWorkOrderNumber(data) {
	return request({
		url: '/EP_EquipmentMatitainOrder/UpdateMatitainOrderStateByWorkOrderNumber',
		method: 'POST',
		data: data
	})
}

//推迟保养
export function UpdateMatitainOrderNextMaintainTimeByID(data) {
	return request({
		url: '/EP_EquipmentMatitainOrder/UpdateMatitainOrderNextMaintainTimeByID',
		method: 'POST',
		data: data
	})
}
//获取转办人列表
export function CallGetZBPeopleListData(data) {
	return request({
		url: '/BS_People/BS_PeoplePageListButOwn',
		method: 'POST',
		data: data
	})
}
//获取设备盘点工单列表
export function CallGetEquipmentCheckOrderData(data) {
	return request({
		url: '/EP_EquipmentCheckOrder/EP_EquipmentCheckOrderPageList',
		method: 'POST',
		data: data
	})
}
//获取设备盘点列表
export function CallGetEquipmentCheckOrderDetailData(data) {
	return request({
		url: '/EP_EquipmentCheckOrderDetail/EP_EquipmentCheckOrderDetailPageList',
		method: 'POST',
		data: data
	})
}
//提交盘点结果
export function SubmitCheckOrderDetailResultData(data) {
	return request({
		url: '/EP_EquipmentCheckOrderDetail/SubmitCheckOrderDetailResultData',
		method: 'POST',
		data: data
	})
}
//获取设备列表
export function CallGetEquipmentListData(data) {
	return request({
		url: '/EP_EquipmentFile/EP_EquipmentFilePageListForApp',
		method: 'POST',
		data: data
	})
}

//提交设备故障上报信息
export function SubmitEquipmentRepairFormInfo(data) {
	return request({
		url: '/EP_EquipmentRepair/SubmitEquipmentRepairFormInfo',
		method: 'POST',
		data: data
	})
}
//获取设备故障上报列表
export function CallGetEquipmentRepairListInfo(data) {
	return request({
		url: '/EP_EquipmentRepair/CallGetEquipmentRepairListInfo',
		method: 'POST',
		data: data
	})
}
//更新设备故障工单状态
export function UpdateEquipmentRepairFormInfo(data) {
	return request({
		url: '/EP_EquipmentRepair/UpdateEquipmentRepairFormInfo',
		method: 'POST',
		data: data
	})
}
//获取转办人列表
export function CallGetAllZBPeopleListData(data) {
	return request({
		url: '/BS_People/BS_PeoplePageList',
		method: 'POST',
		data: data
	})
}
//获取转办人列表
export function CallGetRoleListData(data) {
	return request({
		url: '/Base/GetRolePageListJson',
		method: 'POST',
		data: data
	})
}
//上传图片
export function UploadPic(data) {
	return request({
		url: '/UploadFile/Upload',
		method: 'POST',
		data: data
	})
}
// 获取设备复检列表
export function CallGetEquipmentReCheckOrderData(data) {
	return request({
		url: '/EP_EquipmentReCheckOrder/EP_EquipmentReCheckOrderPageList',
		method: 'POST',
		data: data
	})
}
// 获取设备复检列表
export function CallGetEquipmentReCheckDetailData(data) {
	return request({
		url: '/EP_EquipmentReCheckDetail/EP_EquipmentReCheckDetailPageList',
		method: 'POST',
		data: data
	})
}

// 提交设备复检结果信息
export function UpdateEquipmentReCheckState(data) {
	return request({
		url: '/EP_EquipmentReCheckOrder/UpdateEquipmentReCheckState',
		method: 'POST',
		data: data
	})
}
// 获取备件的信息
export function CallGetEP_SAP_PAPARE_MATInfo(data) {
	return request({
		url: '/EP_SAP_PAPARE_MAT/EP_SAP_PAPARE_MATPageListForApp',
		method: 'POST',
		data: data
	})
}

// 提交备件领料
export function SubmitMATReceivingData(data) {
	return request({
		url: '/EP_SpareReceiving/SubmitMATReceivingData',
		method: 'POST',
		data: data
	})
}
// 获取备件列表，根据单据的id
export function CallGetMATListDataByOrderID(data) {
	return request({
		url: '/EP_SpareReceiving/CallGetMATListDataByOrderID',
		method: 'POST',
		data: data
	})
}
// 获取备件列表，根据单据的id
export function DeleteOrderChooseMat(data) {
	return request({
		url: '/EP_SpareReceiving/DeleteOrderChooseMat',
		method: 'POST',
		data: data
	})
}