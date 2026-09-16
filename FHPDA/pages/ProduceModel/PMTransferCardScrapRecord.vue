<template>
	<view class="container">
		<u-form :model="form" :rules="rules" ref="uForm" label-width="auto">
			<u-form-item :label="$t('PMTransferCardScrapRecord.CardCode')" required>
				<u-search v-model="form.CardCode" @custom="custom" @search="searchCardCode" @clear="clear"
					:placeholder="$t('PMTransferCardScrapRecord.CardCode_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus1">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('PMTransferCardScrapRecord.ProductOrder')">
				<u-input v-model="form.ProductOrder" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PMTransferCardScrapRecord.ContainerNO')">
				<u-input v-model="form.ContainerNO" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PMTransferCardScrapRecord.ProcessName')">
				<u-input v-model="form.ProcessName" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PMTransferCardScrapRecord.ScrapQty')">
				<u-input v-model="form.ScrapQty" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PMTransferCardScrapRecord.ScrapByName')" prop="ScarpReaon">
				<view @click="showSel('process')" style="width: 100%;">
					<u-input v-model="form.ScrapByName" type="text" disabled
						:placeholder="$t('PMTransferCardScrapRecord.ScrapByName_placeholder')" border
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('PMTransferCardScrapRecord.Remak')"
				style="height: auto;margin-bottom: -5px;margin-top: 1px;">
				<u-input v-model="form.Remak" type="textarea" placeholder="" border :focus="focus2" />
			</u-form-item>
		</u-form>

		<!-- 报废原因选择 -->
		<u-select v-model="showProcess" @confirm="changeProcess" :list="ScarpReaonList"></u-select>

		<!--确认按钮-->
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="save" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('PMTransferCardScrapRecord.SaveBtn')}}</text>
			</u-button>
		</view>
		<view>
			<!-- 弹出提示 -->
			<u-top-tips ref="uTips"></u-top-tips>
			<u-toast ref="uToast" />
		</view>

	</view>

</template>

<script>
	import {
		mapState,
		mapActions
	} from 'vuex'
	import {
		commonMixin
	} from '@/common/mixin/mixin.js'
	import scanCode from '@/components/scanCode/scanCode.vue'
	var _self;
	export default {
		mixins: [commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			scanCode
		},
		data() {
			return {
				form: {
					CardCode: "", //流转卡编码
					ProductOrder: "", //订单号
					ContainerNO: "", //柜号
					ProcessName: "", //工序名称
					ProcessCode: "", //工序编码

					ScrapQty: "", //报工数量
					ScarpReaon: "", //报废原因id
					ScarpReaonName: "", //报废原因name
					Remak: "" //备注

				},
				chkAll: false,
				//工序列表
				processList: [],
				//工序是否显示弹窗
				showProcess: false,
				seachTit: "",
				cardList: [],
				ScarpReaonList: [],
				rules: {
					code: [{
						required: true,
						message: '请输入姓名',
						trigger: 'blur,change'
					}],
					intro: [{
						min: 5,
						message: '简介不能少于5个字',
						trigger: 'change'
					}]
				},
				//焦点
				focus1: false,
				focus2: false,
			}
		},

		onReady() {
			// this.$refs.uForm.setRules(this.rules);
			// this.mescroll.resetUpScroll()
			// this.mescroll.showNoMore()
		},
		//预加载
		onLoad() {
			_self = this;
			_self.setFocus("focus1");
			this.getScarpReaonList();
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.ProduceModel/PMTransferCardScrapRecord")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			//查询入库单主表列表(过滤掉状态为已入库) pagination 分页json; queryJson 查询JSO
			...mapActions('Produce', ['TransferCardScrapScan', 'TransferCardScrapSave']),
			...mapActions('common', ['GetProcessModel', 'GetDictionary']),

			//初始化报废原因列表
			getScarpReaonList() {
				var data = {
					EnCode: "LZKScrapReason"
				}
				this.GetDictionary(data).then(res => {
					this.ScarpReaonList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.ScarpReaonList = [];
						} else {
							this.ScarpReaonList = res.resultData;

						}
					} else {
						this.ScarpReaonList = [{
							value: '',
							label: this.$t('common.None')
						}];
					}
				});
			},
			//显示下拉框
			showSel(val, item) {
				if (val == "process")
					this.showProcess = true;
			},
			//选择报废原因
			changeProcess(val) {
				this.form.ScarpReaon = val[0].value; //val[0].label;				
				this.form.ScrapByName = val[0].label;
				_self.setFocus("focus2");
			},

			//条码扫描事件
			searchQR() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchCardCode(res.result);
					}
				});
			},
			//条码查询
			searchCardCode(value) {
				this.form.CardCode = value; //流转卡
				if (this.form.CardCode != "") {
					this.getTransferCardScrapScan();
				}
			},
			clear() {
				this.form.CardCode = ""; //流转卡
			},
			getTransferCardScrapScan() {
				this.cardList = [];
				let query = {
					cardCode: this.form.CardCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.TransferCardScrapScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						console.log(JSON.stringify(res.resultData));
						this.form.ProductOrder = res.resultData.ProductOrder;
						this.form.ContainerNO = res.resultData.ContainerNO;
						this.form.ProcessCode = res.resultData.ProcessCode;
						this.form.ProcessName = res.resultData.ProcessName;
						this.form.ScrapQty = res.resultData.ScrapQty;

					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
						this.form.CardCode = "";
					}
				});
			},

			//全选change事件
			chkAllChange(e) {
				if (this.chkAll) {
					this.cardList.forEach(item => {
						item.Checked = true;
					})
				} else {
					this.cardList.forEach(item => {
						item.Checked = false;
					})
				}
			},
			save() {
				if (this.form.CardCode == "") {
					this.$refs.uToast.show({
						title: this.$t("PMTransferCardScrapRecord.MessageTips_1"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.form.ProcessName == "") {
					this.$refs.uToast.show({
						title: this.$t("PMTransferCardScrapRecord.MessageTips_2"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.ScarpReaon) {
					this.$refs.uToast.show({
						title: this.$t("PMTransferCardScrapRecord.MessageTips_3"),
						type: 'warning',
						icon: true
					});
					return;
				}

				let data = {
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
					entity: this.form
				};
				this.TransferCardScrapSave(data).then(res => {
					console.log(JSON.stringify(res));
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t("PMTransferCardScrapRecord.MessageTips_4"),
							type: 'success',
							icon: true
						});
						this.reset();
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			reset() {
				this.form.CardCode = "";
				this.form.ProcessCode = "";
				this.form.ProcessName = "";
				this.form.CardCode = "";
				this.form.ProductOrder = "";
				this.form.ContainerNO = "";
				this.form.ScrapQty = "";
				this.form.ScarpReaonID = "";
				this.form.ScrapByName = "";
				this.form.Remak = "";
			},
			initFocus() {
				this.focus1 = false
				this.focus2 = false
			},
			setFocus(focusName) {
				_self.initFocus();
				setTimeout(() => {
					this[focusName] = true;
				}, 0)
			}

		}
	}
</script>

<style lang='scss' scoped>
	.container {
		padding: 20upx;
		/* background: #f9f9f9; */
		/* font-size: 32upx; */
		/* height: 100vh; */
	}

	.readonly {
		background-color: Gainsboro;
	}

	.u-collapse-item {
		background-color: Gainsboro;
		width: 320%;

		view {
			background-color: #F0F3FA;
			font-size: 14px;
		}
	}
</style>