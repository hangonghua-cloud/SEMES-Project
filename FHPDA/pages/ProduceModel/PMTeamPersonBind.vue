<template>
	<view class="container">
		<view style="margin-bottom: 15%;">
			<u-form :model="form" label-width="auto">
				<u-form-item :label="$t('PMTeamPersonBind.PTeamCode')" required>
					<u-search v-model="form.PTeamCode" @custom="custom" @search="searchPTeamCode" @clear="clear"
						:placeholder="$t('PMTeamPersonBind.PTeamCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus1">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('PMTeamPersonBind.BindMsg')" style="height: auto;">
					<view style="border: 1px solid Gainsboro;">
						<view class="label">{{$t('PMTeamPersonBind.ProcessName')}}：{{form.ProcessName}}</view>
						<view class="label">{{$t('PMTeamPersonBind.PTeamName')}}：{{form.PTeamName}}</view>
					</view>
				</u-form-item>
			</u-form>
			<view style="margin-top:10rpx;">
				<u-divider halfWidth="100%">{{$t('PMTeamPersonBind.PersonInfo')}}</u-divider>
			</view>
			<u-form :model="form2" label-width="auto">

				<u-form-item :label="$t('PMTeamPersonBind.Code')" required>
					<u-search v-model="form.Code" @custom="custom" @search="searchCode" @clear="clear"
						:placeholder="$t('PMTeamPersonBind.Code_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus2">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR1"></u-icon>
				</u-form-item>

				<u-form-item :label="$t('PMTeamPersonBind.Name')">
					<u-input v-model="form2.Name" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('PMTeamPersonBind.PostName')">
					<u-input v-model="form2.PostName" @click="showSel('postItem')" type="text" disabled
						:placeholder="$t('PMTeamPersonBind.PostName_placeholder')" border />
					<u-icon name="plus-circle-fill" size="70rpx" color="#138087" @click="addBadItem"></u-icon>
					<u-icon name="trash-fill" size="70rpx" color="#138087" @click="deleteBadItem"></u-icon>
				</u-form-item>
			</u-form>
			<scroll-view scroll-y="true" style="height: 750rpx;border:1px solid Gainsboro;margin-top: 10rpx;">
				<view style="border-bottom:1px solid Gainsboro; padding-left: 10rpx;"
					v-for="(item, index) in personList">
					<u-checkbox v-model="item.Checked">
						<view class="label u-line-1">{{$t('PMTeamPersonBind.PostName')}}：{{item.PostName}}</view>
						<view class="label u-line-1">{{$t('PMTeamPersonBind.Code')}}：{{item.UserCode}}</view>
						<view class="label u-line-1">{{$t('PMTeamPersonBind.Name')}}：{{item.UserName}}</view>
					</u-checkbox>
				</view>
			</scroll-view>
		</view>

		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="save" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('PMTeamPersonBind.SaveBtn')}}</text>
			</u-button>
		</view>

		<!--岗位选择 -->
		<u-select v-model="showPostItem" @confirm="changePostItem" :list="postItemList"
			:confirm-text="$t('showModal.confirm')" :cancel-text="$t('showModal.cancel')"></u-select>
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
					PTeamCode: "", //小组编码
					PTeamName: "", //小组名称

					ProcessName: "", //工序名称
					ProcessCode: "", //工序编码

				},
				form2: {
					Code: "",
					Name: "",
					PostName: "",
					PostCode: "",

				},
				personList: [], //人员信息
				postItemList: [], //岗位信息
				showPostItem: false, //岗位弹窗
				//焦点
				focus1: false,
				focus2: false,
			}
		},
		//预加载
		onLoad() {
			_self = this;
			_self.setFocus("focus1");
		},
		onReady() {
			// this.$refs.uForm.setRules(this.rules);
			// this.mescroll.resetUpScroll()
			// this.mescroll.showNoMore()
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.ProduceModel/PMTeamPersonBind")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			...mapActions('Produce', ['PTeamCodeScan', 'PTeamSave']),
			...mapActions('common', ['GetUserList']),



			//小组编码扫描事件
			searchQR() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchPTeamCode(res.result);
					}
				});
			},

			//小组编码扫描查询
			searchPTeamCode(value) {
				this.form.PTeamCode = value; //小组编码
				if (this.form.PTeamCode != "") {
					this.getPTeamCodeScan();
				}
			},

			clear() {
				this.form.PTeamCode = ""; //小组编码
			},



			//小组编码扫描事件
			searchQR1() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchCode(res.result);
					}
				});
			},

			//人员编码扫描查询
			searchCode(value) {
				this.form.Code = value; //小组编码
				if (this.form.Code != "") {
					this.getUserList();
				}
			},

			clear() {
				this.form.Code = ""; //小组编码
			},

			//人员编码扫描方法
			getPTeamCodeScan() {
				this.postItemList = [];
				let query = {
					pTeamCode: this.form.PTeamCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.PTeamCodeScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.form.PTeamCode = res.resultData.PTeamCode;
						this.form.ProcessName = res.resultData.ProcessName;
						this.form.PTeamName = res.resultData.PTeamName;
						if (res.resultData.ItemList == null || res.resultData.ItemList.length == 0) {
							this.personList = [];
						} else {
							this.personList = [];
							res.resultData.ItemList.forEach((item, index) => {
								this.personList.push({
									Checked: false,
									PostCode: item.PostCode,
									PostName: item.PostName,
									UserCode: item.UserCode,
									UserName: item.UserName
								})
							});
						}
						if (res.resultData.PostList == null || res.resultData.PostList.length == 0) {
							this.postItemList = [{
								value: '',
								label: this.$t("PMTeamPersonBind.None")
							}];

						} else {
							res.resultData.PostList.forEach((item, index) => {
								this.postItemList.push({
									value: item.PostCode,
									label: item.PostName
								})
							})
						}
						_self.setFocus("focus2");
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			//人员扫描方法
			getUserList() {
				let queryJson = {
					UserCode: this.form.Code,
					NoLike: "1"
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.GetUserList(queryJson).then(res => {
					uni.hideLoading();
					this.UserList = [];
					if (res && res.success) {
						res.resultData.forEach((item, index) => {

							this.form2.Code = item.Code,
								this.form2.Name = item.Name

						});
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				})
			},
			//添加人员
			addBadItem() {
				if (!this.form2.Code) {
					this.$refs.uToast.show({
						title: this.$t("PMTeamPersonBind.MessageTips_1"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form2.PostName) {
					this.$refs.uToast.show({
						title: this.$t("PMTeamPersonBind.MessageTips_2"),
						type: 'warning',
						icon: true
					});
					return;
				}
				let filterList = this.personList.filter(item => item.UserCode == this.form2.Code);
				if (filterList.length > 0) {
					this.$refs.uToast.show({
						title: this.$t("PMTeamPersonBind.MessageTips_3"),
						type: 'warning',
						icon: true
					});
					return;
				}
				this.personList.push({
					Checked: false,
					UserCode: this.form2.Code,
					UserName: this.form2.Name,
					PostName: this.form2.PostName,
					PostCode: this.form2.PostCode,
				});
				this.form.Code = "";
				this.form2.Code = "";
				this.form2.Name = "";
				this.form2.PostName = "";
				_self.setFocus("focus2");

			},
			//删除不良
			deleteBadItem() {
				this.personList = this.personList.filter(item => {
					return item.Checked == false;
				});

			},
			//保存
			save() {

				if (!this.form.PTeamCode) {
					this.$refs.uToast.show({
						title: this.$t("PMTeamPersonBind.MessageTips_4"),
						type: 'warning',
						icon: true
					});
					return;
				}

				let data = {

					pTeamCode: this.form.PTeamCode,
					personList: this.personList,
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
				};
				this.PTeamSave(data).then(res => {
					console.log(JSON.stringify(res));
					if (res && res.success) {
						this.$refs.uToast.show({
							title: this.$t("PMTeamPersonBind.MessageTips_5"),
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
				this.form.PTeamCode = "";
				this.form.ProcessName = "";
				this.form.PTeamName = "";
				this.personList = [];
				this.postItemList = []; //岗位信息
			},
			//显示下拉框
			showSel(val, item) {
				if (val == "postItem") {
					this.showPostItem = true;
				}
			},

			//选择岗位
			changePostItem(val) {

				this.form2.PostCode = val[0].value; //val[0].label;				
				this.form2.PostName = val[0].label;
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
			},
		}
	}
</script>

<style>
	.container {
		padding: 20upx;
		/* background: #f9f9f9; */
		/* font-size: 32upx; */
		/* height: 100vh; */
	}

	.readonly {
		background-color: Gainsboro;
	}

	.label {
		line-height: 20px;
		width: 250px;
	}
</style>