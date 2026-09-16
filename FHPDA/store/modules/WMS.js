import {
	ProductInMarkScan, //成品入库-唛头码扫描
	ProductInLocationScan, //成品入库-库位扫描
	ProductInSave, //成品入库-保存
	ProductInQuery, //成品入库-记录查询
	ProductDispatchMarkScan, //成品发货-唛头码扫描
	ProductDispatchDetailShow, //成品发货-发货详情展示
	ProductDispatchSave, //成品发货-保存
	ProductDispatchQuery, //成品发货-记录查询
	SemiProductMoveCardScan, //半成品移库-流转卡扫描
	SemiProductMoveLocationScan, //半成品移库-库位扫描
	SemiProductMoveSave, //半成品移库-保存
	SemiProductMoveQuery, //半成品移库-记录查询
	ProductMoveMarkScan, //成品移库-唛头码扫描
	ProductMoveLocationScan, //成品移库-库位扫描
	ProductMoveSave, //成品移库-保存
	ProductMoveQuery, //成品移库-记录查询
	MarkChangeMarkScan, //唛头更换-唛头码扫描
	MarkChangeSave, //唛头更换-保存
	MarkChangeQuery, //唛头更换-记录查询
	ProductReWorkMarkScan, //创建成品返工单-唛头码扫描
	ProductReWorkSave, //创建成品返工单-保存
	ProductReworkBGOrderQuery, //成品返工报工-返工单查询
	ProductReworkBGShow, //成品返工报工-返工单选择页面跳转后显示
	ProductReworkBGPTeamScan, //成品返工报工-生产小组扫描
	ProductReworkBGSave, //成品返工报工-保存
	ProductReworkBGQuery, //成品返工报工-查询
	ProductDispatchProductOrder, // 成品发货-获取订单
	ProductDispatchContainerNO, // 成品发货-获取柜号
	ProductDispatchInfo, // 成品发货-发货单详情
	ProductDispatchMarkBook ,// 成品发货-唛头标记
	RawMaterialTransferStockQuery,//原材料调拨-库存查询
	RawMaterialTransferSave//原材料调拨-保存
} from '@/api/WMS'
import {} from './../mutations_type'

const WMS = {
	state: {},

	mutations: {},

	actions: {
		ProductInMarkScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductInMarkScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductInLocationScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductInLocationScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductInSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductInSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductInQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductInQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductDispatchMarkScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductDispatchMarkScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductDispatchDetailShow({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductDispatchDetailShow(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

		ProductDispatchSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductDispatchSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductDispatchQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductDispatchQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		SemiProductMoveCardScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SemiProductMoveCardScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		SemiProductMoveLocationScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SemiProductMoveLocationScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		SemiProductMoveSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SemiProductMoveSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		SemiProductMoveQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SemiProductMoveQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductMoveMarkScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductMoveMarkScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductMoveLocationScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductMoveLocationScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductMoveSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductMoveSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductMoveQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductMoveQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		MarkChangeMarkScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				MarkChangeMarkScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		MarkChangeSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				MarkChangeSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		MarkChangeQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				MarkChangeQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductReWorkMarkScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductReWorkMarkScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductReWorkSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductReWorkSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductReworkBGOrderQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductReworkBGOrderQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductReworkBGShow({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductReworkBGShow(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductReworkBGPTeamScan({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductReworkBGPTeamScan(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductReworkBGSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductReworkBGSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductReworkBGQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductReworkBGQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductDispatchProductOrder({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductDispatchProductOrder(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductDispatchContainerNO({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductDispatchContainerNO(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductDispatchInfo({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductDispatchInfo(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		ProductDispatchMarkBook({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				ProductDispatchMarkBook(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		RawMaterialTransferStockQuery({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				RawMaterialTransferStockQuery(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		RawMaterialTransferSave({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				RawMaterialTransferSave(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
	}
}

export default WMS