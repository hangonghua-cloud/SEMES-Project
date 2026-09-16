import request from '@/utils/request'

//fuhua 质量管理  巡检/过程检验根据流转卡获取信息 by 刘万军
export function GetQCTransferCardEntity(data) {
	return request({
		url: '/Quality/GetQCTransferCardEntity',
		method: 'POST',
		data: data
	})
}
//fuhua 质量管理  根据工厂,物料小类和检验工序 检验分类( TestType巡检:1 过程检验:2) 获取检验方法 by 刘万军
export function GetQCTestMethodList(data) {
	return request({
		url: '/Quality/GetQCTestMethodList',
		method: 'POST',
		data: data
	})
}
//fuhua 质量管理  根据物料小类,检验工序  检验方法 获取检测项目 by 刘万军
export function GetQCTestItemList(data) {
	return request({
		url: '/Quality/GetQCTestItemList',
		method: 'POST',
		data: data
	})
}
//fuhua 质量管理  保存巡检结果和保存过程检验结果  TestType巡检:1 过程检验:2 by 刘万军
export function SaveQCTestItemForm(data) {
	return request({
		url: '/Quality/SaveQCTestItemForm',
		method: 'POST',
		data: data
	})
}
//fuhua 质量管理  查询检测结果  TestType巡检:1 过程检验:2 by 刘万军
export function GetQCTestRecordList(data) {
	return request({
		url: '/Quality/GetQCTestRecordList',
		method: 'POST',
		data: data
	})
}
//fuhua 质量管理  查询检测明细结果  TestType巡检:1 过程检验:2 by 刘万军
export function GetQCTestResultRecordList(data) {
	return request({
		url: '/Quality/GetQCTestResultRecordList',
		method: 'POST',
		data: data
	})
}
//fuhua 质量管理  巡检质量判定   by 刘万军
export function SavePollingDetailForm(data) {
	return request({
		url: '/Quality/SavePollingDetailForm',
		method: 'POST',
		data: data
	})
}
//fuhua 质量管理  成品检验 OQC根据唛头号查询订单信息   by 刘万军
export function GetOrderByTransferCode(data) {
	return request({
		url: '/Quality/GetOrderByTransferCode',
		method: 'POST',
		data: data
	})
}
//fuhua 质量管理  成品检验 OQC 根据检验方法获取检验项目    by 刘万军
export function GetOQCCheckConfigItem(data) {
	return request({
		url: '/Quality/GetOQCCheckConfigItem',
		method: 'POST',
		data: data
	})
}
//fuhua 质量管理  成品检验 OQC 保存检验记录  by 刘万军
export function SaveOQCCheckConfigForm(data) {
	return request({
		url: '/Quality/SaveOQCCheckConfigForm',
		method: 'POST',
		data: data
	})
}



//fuhua 质量管理  巡检 获取流转履历表数据
export function GetprocessList(data) {
	return request({
		url: '/Quality/GetprocessList',
		method: 'POST',
		data: data
	})
}




//fuhua 质量管理  成品检验 OQC 查询检测记录  by 刘万军
export function GetOQCQualityCheckRecord(data) {
	return request({
		url: '/Quality/GetOQCQualityCheckRecord',
		method: 'POST',
		data: data
	})
}
//fuhua 质量管理  成品检验 OQC 合并批次  by 刘万军
export function SaveOQCQualityChecInspectNo(data) {
	return request({
		url: '/Quality/SaveOQCQualityChecInspectNo',
		method: 'POST',
		data: data
	})
}
//fuhua 质量管理  成品检验 OQC 成品检验项目记录  by 刘万军
export function GetOQCQualityCheckItemRecord(data) {
	return request({
		url: '/Quality/GetOQCQualityCheckItemRecord',
		method: 'POST',
		data: data
	})
}
//fuhua 质量管理  成品检验 OQC 成品检验项目最终判定  by 刘万军
export function SaveOQCQualityCheckResult(data) {
	return request({
		url: '/Quality/SaveOQCQualityCheckResult',
		method: 'POST',
		data: data
	})
}
// 保存隔离品信息
export function SaveQC_IsolateManage(data) {
	return request({
		url: '/QC_IsolateManage/SaveQC_IsolateManage',
		method: 'POST',
		data: data
	})
}

export function GetQC_IsolateManageList(data) {
	return request({
		url: '/QC_IsolateManage/QC_IsolateManagePageDataTableList',
		method: 'POST',
		data: data
	})
}
//获取物料信息
export function BS_MaterielPageDataTableList(data) {
	return request({
		url: '/BS_Materiel/BS_MaterielPageDataTableList',
		method: 'POST',
		data: data
	})
}

//获取物料信息
export function SaveQC_RemarkMaterialRecord(data) {
	return request({
		url: '/QC_RemarkMaterialRecord/SaveQC_RemarkMaterialRecord',
		method: 'POST',
		data: data
	})
}

//获取主线自检剪切工序信息
export function GetQC_CheckRecordProcess(data) {
	return request({
		url: '/QC_CheckRecordProcess/GetQC_CheckRecordProcess',
		method: 'POST',
		data: data
	})
}

//保存主线自检剪切工序信息
export function SaveQC_CheckRecordProcess(data) {
	return request({
		url: '/QC_CheckRecordProcess/SaveQC_CheckRecordProcess',
		method: 'POST',
		data: data
	})
}



//主线自检剪切工序检验信息列表
export function GetQC_CheckRecordJQList(data) {
	return request({
		url: '/QC_CheckRecordProcess/GetQC_CheckRecordJQList',
		method: 'POST',
		data: data
	})
}

//生成主线自检检验项目详情
export function GetCheckRecordProcessDetail(data) {
	return request({
		url: '/QC_CheckRecordProcess/GetCheckRecordProcessDetail',
		method: 'POST',
		data: data
	})
}


// 获取表结构
export function CallGetDemoData(data) {
	return request({
		url: '/api/Test/CallGetDemoData',
		method: 'POST',
		data: data
	})
}

// 主线品控检查记录任务
export function GetQC_MainlineQualityControlRecordResultList(data) {
	return request({
		url: '/QC_MainlineQualityControlRecordResult/GetQC_MainlineQualityControlRecordResultList',
		method: 'GET',
		data: data
	})
}
// 主线品控检查记录任务
export function GetQC_MainlineQualityControlRecordResultDetailList(data) {
	return request({
		url: '/QC_MainlineQualityControlRecordResultDetail/GetQC_MainlineQualityControlRecordResultDetailList',
		method: 'GET',
		data: data
	})
}
export function GetQC_MainlineQualityControlRecordResultDetailListLa(data) {
	return request({
		url: '/QC_MainlineQualityControlRecordResultDetail/GetQC_MainlineQualityControlRecordResultDetailListLa',
		method: 'GET',
		data: data
	})
}


// 保存检验结果主线品控检查记录任务
export function SaveBatchQC_MainlineQualityControlRecordResultDetail(data) {
	return request({
		url: '/QC_MainlineQualityControlRecordResultDetail/SaveBatchQC_MainlineQualityControlRecordResultDetail',
		method: 'POST',
		data: data
	})
}

//保存报废数量填写数据
export function SaveQC_ProcessWaste(data) {
	return request({
		url: '/QC_ProcessWaste/SaveQC_ProcessWaste',
		method: 'POST',
		data: data
	})
}


//根据生产订单获取报废数量统计
export function GetScrapsTotalList(data) {
	return request({
		url: '/QC_ProcessWaste/GetScrapsTotalList',
		method: 'POST',
		data: data
	})
}

//获取主线自检卷封检验任务
export function GetQC_CheckRecordJFList(data) {
	return request({
		url: '/QC_CheckRecordProcess/GetQC_CheckRecordJFList',
		method: 'POST',
		data: data
	})
}

//获取主线自检卷封检验项目
export function GetQC_CheckRecordProcessDetailJFList(data) {
	return request({
		url: '/QC_CheckRecordProcessDetail/GetQC_CheckRecordProcessDetailJFList',
		method: 'GET',
		data: data
	})
}


//保存主线自检卷封项目填写记录
export function SaveBatchQC_CheckRecordProcessDetailForApp(data) {
	return request({
		url: '/QC_CheckRecordProcessDetail/SaveBatchQC_CheckRecordProcessDetailForApp',
		method: 'POST',
		data: data
	})
}


//获取线自检卷封复核任务列表
export function GetQC_CheckRecordJFReviewList(data) {
	return request({
		url: '/QC_CheckRecordProcess/GetQC_CheckRecordJFReviewList',
		method: 'POST',
		data: data
	})
}

//根据生产订单获取卷封工序2检验项目
export function GetQC_RollSeal2InspectionItemListByOrderNo(data) {
	return request({
		url: '/QC_RollSeal2InspectionItem/GetQC_RollSeal2InspectionItemListByOrderNo',
		method: 'GET',
		data: data
	})
}


//根据生产订单获取卷封工序2检验项目
export function GetQC_RollSeal2ProductByOrderNo(data) {
	return request({
		url: '/QC_RollSeal2InspectionItem/GetQC_RollSeal2ProductByOrderNo',
		method: 'GET',
		data: data
	})
}


export function SaveQC_RollSeal2InspectionItem(data) {
	return request({
		url: '/QC_RollSeal2InspectionItem/SaveQC_RollSeal2InspectionItem',
		method: 'POST',
		data: data
	})
}

export function GetQC_RollSeal2InspectionList(data) {
	return request({
		url: '/QC_RollSeal2Inspection/GetQC_RollSeal2InspectionList',
		method: 'GET',
		data: data
	})
}

export function GetQC_RollSeal2InspectionItemList(data) {
	return request({
		url: '/QC_RollSeal2InspectionItem/GetQC_RollSeal2InspectionItemList',
		method: 'GET',
		data: data
	})
}

export function SaveQC_RollSeal2InspectionForApp(data) {
	return request({
		url: '/QC_RollSeal2Inspection/SaveQC_RollSeal2InspectionForApp',
		method: 'POST',
		data: data
	})
}

export function GetListForReview(data) {
	return request({
		url: '/QC_MainlineQualityControlRecordResult/GetListForReview',
		method: 'POST',
		data: data
	})
}

export function UpdateRecordResult(data) {
	return request({
		url: '/QC_MainlineQualityControlRecordResult/UpdateRecordResult',
		method: 'POST',
		data: data
	})
}
