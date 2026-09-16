import {
	GetPMProcessBadItem, //工序报工不良项目配置
	GetPMStartInfo, //生产开工扫描
	SavePMStartInfo, //生产开工保存
	RawMBatchUpMachineScan, //原料批次上机-机台扫描
	RawMBatchUpCodeBarScan, //原料批次上机-关键件扫描
	RawMBatchUpCodeBarSave, //原料批次上机-保存
	RawMBatchUpMachineRemove,//原料批次上机-取消绑定
	TransferCardBGMachineScan, //流转卡报工-生产机台扫描
	TransferCardBGCardScan, //流转卡报工-流转卡扫描
	TransferCardBGPTeamScan, //流转卡报工-生产小组扫描
	TransferCardBGSave, //流转卡卡报工-保存
	OwnProductBGCardScan, //自制半成品扫描
	OwnProductBGSave, //自制半成品保存
	TransferCardScrapScan, //流转卡报废-流转卡扫描
	TransferCardScrapSave, //流转卡报废-保存
	PTeamCodeScan, //生产小组人员绑定-生产小组扫描
	PTeamSave, //生产小组人员绑定-保存
	ReworkCardScan, //返工任务-流转卡扫描
	ReworkSave, //返工任务-保存
	ReworkQuery, //返工任务-查询
	ReworkBGTaskQuery, //返工报工-返工任务查询
	ReworkBGTaskDetailQuery, //返工报工-返工任务明细查询
	ReworkBGCardScan, //返工报工-流转卡扫描
	ReworkBGPTeamScan, //返工报工-生产小组扫描
	ReworkBGSave, //返工报工-保存
	FirstInspectionCardScan, //生产首检-流转卡扫描
	FirstInspectionConfirmMachieScan,//质量首检-机台扫描
	FirstInspectionConfirmSave,//质量首检-保存
	FirstInspectionQueryResult,
	
	FirstInspectionLastSave,
	FirstInspectionMachineScan, //生产首检-机台扫描
	FirstInspectionSave, //生产首检-保存
	FirstInspectionQuery, //生产首检-查询
	FirstInspectionConfirmQuery,//生产首检-查询
	FirstInspectionReCheck, //生产首检-质量复检（录入界面呈现）
	FirstInspectionReCheckSave, //生产首检-质量复检保存
	GetProductOrderSelect, //包装报工-订单号选择
	GetContainerNOSelect, //包装报工-柜号选择
	PackingBGCardScan, //包装报工-流转卡扫描
	PackingBGPTeamScan, //包装报工-生产小组扫描
	PackingBGSave, //包装报工-保存
	PackingBGQuery, //包装报工-查询
	CodeBarScan, //生产管理-扫一扫
	GetReworkRecordDetail,//质量确认-扫描流转卡
	ReworkRecordQualityConfirmSave,//质量确认-确认
    GetprocessList,
	GetUserBGRecord,//生产管理-查一查
	PerPalletStartMachineScan,//单托开工-机台扫描
	PerPalletStartCardScan,//单托开工-流转卡扫描
	PerPalletStartSave,//单托开工-保存
	SortingBGMachineScan,//分拣报工-生产机台扫描
	SortingBGCardScan,//分拣报工-流转卡扫描
	SortingBGPTeamScan,//分拣报工-生产小组扫描
	SortingBGSave,//分拣报工-保存
	InMachineCardQuery,//在机流转卡查询
	BOMQuery//BOM查询

} from '@/api/Produce'
import {} from './../mutations_type'


const Produce = {
	state: {

	},

	mutations: {

	},

	actions: {
		GetPMProcessBadItem({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetPMProcessBadItem(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetPMStartInfo({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetPMStartInfo(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		SavePMStartInfo({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SavePMStartInfo(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		RawMBatchUpMachineScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				RawMBatchUpMachineScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		RawMBatchUpCodeBarScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				RawMBatchUpCodeBarScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		RawMBatchUpCodeBarSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				RawMBatchUpCodeBarSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		RawMBatchUpMachineRemove({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				RawMBatchUpMachineRemove(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetReworkRecordDetail({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetReworkRecordDetail(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ReworkRecordQualityConfirmSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ReworkRecordQualityConfirmSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
 
		TransferCardBGMachineScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				TransferCardBGMachineScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		TransferCardBGCardScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				TransferCardBGCardScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		TransferCardBGPTeamScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				TransferCardBGPTeamScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		TransferCardBGSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				TransferCardBGSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		OwnProductBGCardScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				OwnProductBGCardScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		OwnProductBGSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				OwnProductBGSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

		TransferCardScrapScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				TransferCardScrapScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		TransferCardScrapSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				TransferCardScrapSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		PTeamCodeScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				PTeamCodeScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		PTeamSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				PTeamSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ReworkCardScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ReworkCardScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ReworkSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ReworkSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ReworkQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ReworkQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ReworkBGTaskQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ReworkBGTaskQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ReworkBGTaskDetailQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ReworkBGTaskDetailQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ReworkBGCardScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ReworkBGCardScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ReworkBGPTeamScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ReworkBGPTeamScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ReworkBGSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ReworkBGSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		FirstInspectionCardScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				FirstInspectionCardScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
				
		FirstInspectionConfirmMachieScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				FirstInspectionConfirmMachieScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
				
		FirstInspectionConfirmSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				FirstInspectionConfirmSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
			
		FirstInspectionQueryResult({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				FirstInspectionQueryResult(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
	
		
		
	FirstInspectionLastSave({
		commit
	}, data) {
		return new Promise((resolve, reject) => {
			FirstInspectionLastSave(data).then(res => {
				resolve(res)
			}).catch(error => {
				reject(error)
			})
		})
	},
			
		
		
		FirstInspectionMachineScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				FirstInspectionMachineScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		FirstInspectionSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				FirstInspectionSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		FirstInspectionQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				FirstInspectionQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		
		FirstInspectionConfirmQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				FirstInspectionConfirmQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		//fuhua 质量管理  成品检验 OQC 保存检验记录   by 刘万军
		GetprocessList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetprocessList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		
		FirstInspectionReCheck({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				FirstInspectionReCheck(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		FirstInspectionReCheckSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				FirstInspectionReCheckSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetProductOrderSelect({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetProductOrderSelect(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetContainerNOSelect({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetContainerNOSelect(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		PackingBGCardScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				PackingBGCardScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		PackingBGPTeamScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				PackingBGPTeamScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		PackingBGSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				PackingBGSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		PackingBGQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				PackingBGQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		CodeBarScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				CodeBarScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetUserBGRecord({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetUserBGRecord(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//单托开工-机台扫描
		PerPalletStartMachineScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				PerPalletStartMachineScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		PerPalletStartCardScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				PerPalletStartCardScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		PerPalletStartSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				PerPalletStartSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		SortingBGMachineScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SortingBGMachineScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		SortingBGCardScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SortingBGCardScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		SortingBGPTeamScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SortingBGPTeamScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		SortingBGSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SortingBGSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//在机流转卡查询
		InMachineCardQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				InMachineCardQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		//BOM查询
		BOMQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				BOMQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
	}
}

export default Produce
