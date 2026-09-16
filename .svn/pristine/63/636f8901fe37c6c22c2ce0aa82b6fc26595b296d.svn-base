import {
    GetQCTransferCardEntity,//fuhua 质量管理  巡检/过程检验根据流转卡获取信息 by 刘万军
    GetQCTestMethodList,//fuhua 质量管理  根据工厂,物料小类和检验工序 检验分类( TestType巡检:1 过程检验:2) 获取检验方法 by 刘万军
    GetQCTestItemList,//fuhua 质量管理  根据物料小类,检验工序  检验方法  获取检测项目 by 刘万军
    SaveQCTestItemForm,//fuhua 质量管理  保存巡检结果和保存过程检验结果  TestType巡检:1 过程检验:2 by 刘万军
    GetQCTestRecordList,//fuhua 质量管理  查询检测结果  TestType巡检:1 过程检验:2 by 刘万军
    GetQCTestResultRecordList,//fuhua 质量管理  查询检测明细结果  TestType巡检:1 过程检验:2 by 刘万军
    SavePollingDetailForm,//fuhua 质量管理  巡检质量判定   by 刘万军
    GetOrderByTransferCode,//fuhua 质量管理  成品检验 OQC根据唛头号查询订单信息   by 刘万军
    GetOQCCheckConfigItem,//fuhua 质量管理  成品检验 OQC根据检验方法获取检验项目   by 刘万军
    SaveOQCCheckConfigForm,//fuhua 质量管理  成品检验 OQC 保存检验记录   by 刘万军
    GetOQCQualityCheckRecord,//fuhua 质量管理  成品检验 OQC 查询检测记录  by 刘万军
    SaveOQCQualityChecInspectNo,//fuhua 质量管理  成品检验 OQC 合并批次  by 刘万军
    GetOQCQualityCheckItemRecord,//fuhua 质量管理  成品检验 OQC 成品检验项目记录  by 刘万军
    SaveOQCQualityCheckResult,//fuhua 质量管理  成品检验 OQC 成品检验项目最终判定  by 刘万军
	CallSaveDemoData, //保存Demo数据
	SaveQC_IsolateManage,
	GetQC_IsolateManageList,
	BS_MaterielPageDataTableList,
	SaveQC_RemarkMaterialRecord,
	GetQC_CheckRecordProcess,
	SaveQC_CheckRecordProcess,
	GetQC_CheckRecordJQList,
	GetQC_MainlineQualityControlRecordResultList,
	GetQC_MainlineQualityControlRecordResultDetailList,
	SaveBatchQC_MainlineQualityControlRecordResultDetail,
	GetQC_MainlineQualityControlRecordResultDetailListLa,
	GetListForReview,
	UpdateRecordResult,
	SaveQC_ProcessWaste,
	GetScrapsTotalList,
	GetCheckRecordProcessDetail,
	GetQC_CheckRecordJFList,
	GetQC_CheckRecordProcessDetailJFList,
	SaveBatchQC_CheckRecordProcessDetailForApp,
	GetQC_CheckRecordJFReviewList,
	GetQC_RollSeal2InspectionItemListByOrderNo,
	GetQC_RollSeal2ProductByOrderNo,
	SaveQC_RollSeal2InspectionItem,
	GetQC_RollSeal2InspectionList,
	GetQC_RollSeal2InspectionItemList,
	SaveQC_RollSeal2InspectionForApp,
	GetprocessList,
} from '@/api/Quality'
import {} from './../mutations_type'


const Quality = {
	state: {

	},

	mutations: {

	},

	actions: {
        //fuhua 质量管理  巡检/过程检验根据流转卡获取信息 by 刘万军
        GetQCTransferCardEntity({
        	commit
        }, data) {
        	return new Promise((resolve, reject) => {
        		GetQCTransferCardEntity(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
        //fuhua 质量管理  根据工厂,物料小类和检验工序 检验分类( TestType巡检:1 过程检验:2) 获取检验方法 by 刘万军
        GetQCTestMethodList({
        	commit
        }, data) {
        	return new Promise((resolve, reject) => {
        		GetQCTestMethodList(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
        //fuhua 质量管理  根据物料小类,检验工序  检验方法 获取检测项目 by 刘万军
        GetQCTestItemList({
        	commit
        }, data) {
        	return new Promise((resolve, reject) => {
        		GetQCTestItemList(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
        //fuhua 质量管理  保存巡检结果 by 刘万军
        SaveQCTestItemForm({
        	commit
        }, data) {
        	return new Promise((resolve, reject) => {
        		SaveQCTestItemForm(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
        //fuhua 质量管理  查询检测结果  TestType巡检:1 过程检验:2 by 刘万军
        GetQCTestRecordList({
        	commit
        }, data) {
        	return new Promise((resolve, reject) => {
        		GetQCTestRecordList(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
        //fuhua 质量管理  查询检测明细结果  TestType巡检:1 过程检验:2 by 刘万军
        GetQCTestResultRecordList({
        	commit
        }, data) {
        	return new Promise((resolve, reject) => {
        		GetQCTestResultRecordList(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
        //fuhua 质量管理  质量管理  巡检质量判定   by 刘万军
        SavePollingDetailForm({
        	commit
        }, data) {
        	return new Promise((resolve, reject) => {
        		SavePollingDetailForm(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
        //fuhua 质量管理  成品检验 OQC根据唛头号查询订单信息   by 刘万军
        GetOrderByTransferCode({
        	commit
        }, data) {
        	return new Promise((resolve, reject) => {
        		GetOrderByTransferCode(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
        //fuhua 质量管理  成品检验 OQC根据检验方法获取检验项目   by 刘万军
        GetOQCCheckConfigItem({
        	commit
        }, data) {
        	return new Promise((resolve, reject) => {
        		GetOQCCheckConfigItem(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
        //fuhua 质量管理  成品检验 OQC 保存检验记录   by 刘万军
        SaveOQCCheckConfigForm({
        	commit
        }, data) {
        	return new Promise((resolve, reject) => {
        		SaveOQCCheckConfigForm(data).then(res => {
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
		
		
        //fuhua 质量管理  成品检验 OQC 查询检测记录  by 刘万军
        GetOQCQualityCheckRecord({
        	commit
        }, data) {
        	return new Promise((resolve, reject) => {
        		GetOQCQualityCheckRecord(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
        //fuhua 质量管理  成品检验 OQC 合并批次  by 刘万军
        SaveOQCQualityChecInspectNo({
        	commit
        }, data) {
        	return new Promise((resolve, reject) => {
        		SaveOQCQualityChecInspectNo(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
        //fuhua 质量管理  成品检验 OQC 成品检验项目记录  by 刘万军
        GetOQCQualityCheckItemRecord({
        	commit
        }, data) {
        	return new Promise((resolve, reject) => {
        		GetOQCQualityCheckItemRecord(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
        //fuhua 质量管理  成品检验 OQC 成品检验项目最终判定  by 刘万军
        SaveOQCQualityCheckResult({
        	commit
        }, data) {
        	return new Promise((resolve, reject) => {
        		SaveOQCQualityCheckResult(data).then(res => {
        			resolve(res)
        		}).catch(error => {
        			reject(error)
        		})
        	})
        },
		CallSaveDemoData({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				CallSaveDemoData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		SaveQC_IsolateManage({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SaveQC_IsolateManage(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

		GetQC_IsolateManageList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetQC_IsolateManageList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

		BS_MaterielPageDataTableList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				BS_MaterielPageDataTableList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

		SaveQC_RemarkMaterialRecord({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SaveQC_RemarkMaterialRecord(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

		GetQC_CheckRecordProcess({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetQC_CheckRecordProcess(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

		SaveQC_CheckRecordProcess({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SaveQC_CheckRecordProcess(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

		GetQC_CheckRecordJQList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetQC_CheckRecordJQList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		///////////////////////////////
		GetQC_MainlineQualityControlRecordResultList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetQC_MainlineQualityControlRecordResultList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

		GetQC_MainlineQualityControlRecordResultDetailList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetQC_MainlineQualityControlRecordResultDetailList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

		SaveBatchQC_MainlineQualityControlRecordResultDetail({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SaveBatchQC_MainlineQualityControlRecordResultDetail(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		///////////////////////////////////////////
	GetQC_MainlineQualityControlRecordResultDetailListLa({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetQC_MainlineQualityControlRecordResultDetailListLa(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetListForReview({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetListForReview(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

		UpdateRecordResult({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				UpdateRecordResult(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		///////////////////////////////////////////
		SaveQC_ProcessWaste({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SaveQC_ProcessWaste(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

		GetScrapsTotalList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetScrapsTotalList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},


		GetQC_CheckRecordJFList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetQC_CheckRecordJFList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

	GetCheckRecordProcessDetail({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetCheckRecordProcessDetail(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},



		GetQC_CheckRecordProcessDetailJFList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetQC_CheckRecordProcessDetailJFList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

		SaveBatchQC_CheckRecordProcessDetailForApp({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SaveBatchQC_CheckRecordProcessDetailForApp(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetQC_CheckRecordJFReviewList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetQC_CheckRecordJFReviewList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetQC_RollSeal2InspectionItemListByOrderNo({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetQC_RollSeal2InspectionItemListByOrderNo(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetQC_RollSeal2ProductByOrderNo({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetQC_RollSeal2ProductByOrderNo(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		SaveQC_RollSeal2InspectionItem({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SaveQC_RollSeal2InspectionItem(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetQC_RollSeal2InspectionList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetQC_RollSeal2InspectionList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetQC_RollSeal2InspectionItemList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetQC_RollSeal2InspectionItemList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		SaveQC_RollSeal2InspectionForApp({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SaveQC_RollSeal2InspectionForApp(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
        
	}
}

export default Quality
