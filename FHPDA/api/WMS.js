import request from '@/utils/request'
import {
	erphttp
} from '@/utils/request'

//成品入库-唛头码扫描
export function ProductInMarkScan(data) {
	return request({
		url: '/Material/ProductInMarkScan',
		method: 'POST',
		data: data
	})
}
//成品入库-库位扫描
export function ProductInLocationScan(data) {
	return request({
		url: '/Material/ProductInLocationScan',
		method: 'POST',
		data: data
	})
}

//成品入库-保存
export function ProductInSave(data) {
	return request({
		url: '/Material/ProductInSave',
		method: 'POST',
		data: data
	})
}
//成品入库-记录查询
export function ProductInQuery(data) {
	return request({
		url: '/Material/ProductInQuery',
		method: 'POST',
		data: data
	})
}
// 成品发货-唛头码扫描
export function ProductDispatchMarkScan(data) {
	return request({
		url: '/Material/ProductDispatchMarkScan',
		method: 'POST',
		data: data
	})
}
// 成品发货-发货详情展示
export function ProductDispatchDetailShow(data) {
	return request({
		url: '/Material/ProductDispatchDetailShow',
		method: 'POST',
		data: data
	})
}
// 成品发货-保存
export function ProductDispatchSave(data) {
	return request({
		url: '/Material/ProductDispatchSave',
		method: 'POST',
		data: data
	})
}
// 成品发货-记录查询
export function ProductDispatchQuery(data) {
	return request({
		url: '/Material/ProductDispatchQuery',
		method: 'POST',
		data: data
	})
}
// 成品发货-获取订单
export function ProductDispatchProductOrder(data) {
	return request({
		url: '/Material/ProductDispatchProductOrder',
		method: 'POST',
		data: data
	})
}
// 成品发货-获取柜号
export function ProductDispatchContainerNO(data) {
	return request({
		url: '/Material/ProductDispatchContainerNO',
		method: 'POST',
		data: data
	})
}
// 成品发货-发货单详情
export function ProductDispatchInfo(data) {
	return request({
		url: '/Material/ProductDispatchInfo',
		method: 'POST',
		data: data
	})
}
//成品发货-唛头标记
export function ProductDispatchMarkBook(data) {
	return request({
		url: '/Material/ProductDispatchMarkBook',
		method: 'POST',
		data: data
	})
}

//半成品移库-流转卡扫描
export function SemiProductMoveCardScan(data) {
	return request({
		url: '/Material/SemiProductMoveCardScan',
		method: 'POST',
		data: data
	})
}
//半成品移库-库位扫描
export function SemiProductMoveLocationScan(data) {
	return request({
		url: '/Material/SemiProductMoveLocationScan',
		method: 'POST',
		data: data
	})
}
//半成品移库-保存
export function SemiProductMoveSave(data) {
	return request({
		url: '/Material/SemiProductMoveSave',
		method: 'POST',
		data: data
	})
}
//半成品移库-记录查询
export function SemiProductMoveQuery(data) {
	return request({
		url: '/Material/SemiProductMoveQuery',
		method: 'POST',
		data: data
	})
}

//成品移库-唛头码扫描
export function ProductMoveMarkScan(data) {
	return request({
		url: '/Material/ProductMoveMarkScan',
		method: 'POST',
		data: data
	})
}
//成品移库-库位扫描
export function ProductMoveLocationScan(data) {
	return request({
		url: '/Material/ProductMoveLocationScan',
		method: 'POST',
		data: data
	})
}
//成品移库-保存
export function ProductMoveSave(data) {
	return request({
		url: '/Material/ProductMoveSave',
		method: 'POST',
		data: data
	})
}
//成品移库-记录查询
export function ProductMoveQuery(data) {
	return request({
		url: '/Material/ProductMoveQuery',
		method: 'POST',
		data: data
	})
}
//唛头更换-唛头码扫描
export function MarkChangeMarkScan(data) {
	return request({
		url: '/Material/MarkChangeMarkScan',
		method: 'POST',
		data: data
	})
}
//唛头更换-保存
export function MarkChangeSave(data) {
	return request({
		url: '/Material/MarkChangeSave',
		method: 'POST',
		data: data
	})
}
//唛头更换-记录查询
export function MarkChangeQuery(data) {
	return request({
		url: '/Material/MarkChangeQuery',
		method: 'POST',
		data: data
	})
}
//创建成品返工单-唛头码扫描
export function ProductReWorkMarkScan(data) {
	return request({
		url: '/Material/ProductReWorkMarkScan',
		method: 'POST',
		data: data
	})
}
//创建成品返工单-保存
export function ProductReWorkSave(data) {
	return request({
		url: '/Material/ProductReWorkSave',
		method: 'POST',
		data: data
	})
}
//成品返工报工-返工单查询
export function ProductReworkBGOrderQuery(data) {
	return request({
		url: '/Material/ProductReworkBGOrderQuery',
		method: 'POST',
		data: data
	})
}
//成品返工报工-返工单选择页面跳转后显示
export function ProductReworkBGShow(data) {
	return request({
		url: '/Material/ProductReworkBGShow',
		method: 'POST',
		data: data
	})
}
//成品返工报工-生产小组扫描
export function ProductReworkBGPTeamScan(data) {
	return request({
		url: '/Material/ProductReworkBGPTeamScan',
		method: 'POST',
		data: data
	})
}
//成品返工报工-保存
export function ProductReworkBGSave(data) {
	return request({
		url: '/Material/ProductReworkBGSave',
		method: 'POST',
		data: data
	})
}
//成品返工报工-查询
export function ProductReworkBGQuery(data) {
	return request({
		url: '/Material/ProductReworkBGQuery',
		method: 'POST',
		data: data
	})
}
//原材料调拨-库存查询
export function RawMaterialTransferStockQuery(data) {
	return request({
		url: '/Material/RawMaterialTransferStockQuery',
		method: 'POST',
		data: data
	})
}
//原材料调拨-保存
export function RawMaterialTransferSave(data) {
	return request({
		url: '/Material/RawMaterialTransferSave',
		method: 'POST',
		data: data
	})
}