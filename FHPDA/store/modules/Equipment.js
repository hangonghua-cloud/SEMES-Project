import {
	CallSaveDemoData,//保存Demo数据
	CallGetEquipmentMatitainOrderData,//获取保养工单列表
	CallGetEP_EquipmentMaintainDetailData,//获取设备保养工单明细
	UpdateMatitainOrderStateByWorkOrderNumber,//更新工单状态为待保养
	UpdateMatitainOrderNextMaintainTimeByID,//推迟保养
	CallGetZBPeopleListData,//获取转办人列表
	CallGetEquipmentCheckOrderData,//获取待盘点的单号列表
	CallGetEquipmentCheckOrderDetailData,//获取设备盘点列表
	SubmitCheckOrderDetailResultData,//提交盘点结果
	CallGetEquipmentListData,//获取设备列表
	SubmitEquipmentRepairFormInfo,//提交设备上报信息
	CallGetEquipmentRepairListInfo,//获取设备故障上报列表
	UpdateEquipmentRepairFormInfo,//更新设备故障工单状态
	CallGetAllZBPeopleListData,//获取所有人列表
	UploadPic,//上传图片
	CallGetEquipmentReCheckOrderData,//获取设备复检列表
	CallGetEquipmentReCheckDetailData,//获取设备复检项目
	UpdateEquipmentReCheckState,//提交设备复检结果信息
	CallGetEP_SAP_PAPARE_MATInfo,//获取备件的信息
	SubmitMATReceivingData,//提交备件领料
	CallGetMATListDataByOrderID,//获取备件列表，根据单据的id
	CallGetRoleListData,//获取所有的角色
	DeleteOrderChooseMat,//删除所选的备件
	GetEquipmentCheckByCode,//FuHua 设备点检-根据设备编码获取点检任务
	SaveEquipmentTackResult,//FuHua 设备点检-保存设备点检记录
	GetDataTableCheckResult,//FuHua 设备点检-设备点检记录查询
	GetEquipmentManageByCode,//FuHua 设备故障报修-根据设备编码获取设备信息
	SaveEquipmentMalfunctionRepair,//FuHua 设备故障报修-设备故障报修保存
	GetEquipmentMalfunctionRepair,//FuHua 设备故障报修- 获取设备报修记录
	SaveEquipmentRepairRecord,//FuHua 设备故障维修- 设备维修记录保存
    GetEquipmentMaintainTaskByDateAndType,//根据时间范围和设备类别 获取未保养的任务 -未保养  by 刘万军
    GetEP_EquipmentMaintainDetailList,//根据保养任务编码查询保养项目  by 刘万军
    SaveEP_EquipmentMaintainTask,//设备保养--保存保养执行数据\保养项目\备件更换  by 刘万军
} from '@/api/Equipment'
import {} from './../mutations_type'


const Equipment = {
	state: {
	},

	mutations: {
	},

	actions: {
		CallSaveDemoData({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallSaveDemoData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		//FuHua 设备点检-根据设备编码获取点检任务
		GetEquipmentCheckByCode({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				GetEquipmentCheckByCode(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
 
		//FuHua 设备点检-保存设备点检记录
		SaveEquipmentTackResult({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				SaveEquipmentTackResult(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//FuHua 设备点检-设备点检记录查询
		GetDataTableCheckResult({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				GetDataTableCheckResult(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//FuHua 设备故障报修-根据设备编码获取设备信息
		GetEquipmentManageByCode({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				GetEquipmentManageByCode(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//FuHua 设备故障报修-设备故障报修保存
		SaveEquipmentMalfunctionRepair({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				SaveEquipmentMalfunctionRepair(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//FuHua 设备故障报修- 获取设备报修记录
		GetEquipmentMalfunctionRepair({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				GetEquipmentMalfunctionRepair(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//FuHua 设备故障维修- 设备维修记录保存
		SaveEquipmentRepairRecord({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				SaveEquipmentRepairRecord(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		
		//获取保养工单列表
		CallGetEquipmentMatitainOrderData({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallGetEquipmentMatitainOrderData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//获取设备保养工单明细
		CallGetEP_EquipmentMaintainDetailData({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallGetEP_EquipmentMaintainDetailData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//更新工单状态为待保养
		UpdateMatitainOrderStateByWorkOrderNumber({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				UpdateMatitainOrderStateByWorkOrderNumber(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//更新工单状态为待保养
		UpdateMatitainOrderNextMaintainTimeByID({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				UpdateMatitainOrderNextMaintainTimeByID(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//获取转办人列表
		CallGetZBPeopleListData({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallGetZBPeopleListData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//获取待盘点的单号列表
		CallGetEquipmentCheckOrderData({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallGetEquipmentCheckOrderData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//获取设备盘点列表
		CallGetEquipmentCheckOrderDetailData({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallGetEquipmentCheckOrderDetailData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//提交盘点结果
		SubmitCheckOrderDetailResultData({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				SubmitCheckOrderDetailResultData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//获取设备列表
		CallGetEquipmentListData({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallGetEquipmentListData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//提交设备上报信息
		SubmitEquipmentRepairFormInfo({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				SubmitEquipmentRepairFormInfo(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//获取设备故障上报列表
		CallGetEquipmentRepairListInfo({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallGetEquipmentRepairListInfo(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//更新设备故障工单状态
		UpdateEquipmentRepairFormInfo({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				UpdateEquipmentRepairFormInfo(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//获取所有人列表
		CallGetAllZBPeopleListData({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallGetAllZBPeopleListData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//获取所有人列表
		UploadPic({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				UploadPic(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//获取设备复检列表
		CallGetEquipmentReCheckOrderData({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallGetEquipmentReCheckOrderData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//获取设备复检项目
		CallGetEquipmentReCheckDetailData({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallGetEquipmentReCheckDetailData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		//提交设备复检结果信息
		UpdateEquipmentReCheckState({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				UpdateEquipmentReCheckState(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		//获取备件的信息
		CallGetEP_SAP_PAPARE_MATInfo({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallGetEP_SAP_PAPARE_MATInfo(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//提交备件领料
		SubmitMATReceivingData({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				SubmitMATReceivingData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//获取备件列表，根据单据的id
		CallGetMATListDataByOrderID({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallGetMATListDataByOrderID(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//获取所有的角色
		CallGetRoleListData({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallGetRoleListData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//删除所选的备件
		DeleteOrderChooseMat({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				DeleteOrderChooseMat(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
        //根据时间范围和设备类别 获取未保养的任务
        GetEquipmentMaintainTaskByDateAndType({
        	commit
        },data) {
        	return new Promise((resolve, reject) => {
        		GetEquipmentMaintainTaskByDateAndType(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
        //根据保养任务编码查询保养项目
        GetEP_EquipmentMaintainDetailList({
        	commit
        },data) {
        	return new Promise((resolve, reject) => {
        		GetEP_EquipmentMaintainDetailList(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
        //根据保养任务编码查询保养项目
        SaveEP_EquipmentMaintainTask({
        	commit
        },data) {
        	return new Promise((resolve, reject) => {
        		SaveEP_EquipmentMaintainTask(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
	}
}

export default Equipment
