<template>
	<view class="container">
		<view class="left-bottom-sign"></view>
		<image src="../../static/back.png" style="width: 48rpx; height: 48rpx;" @click="navBack"></image>
		<!-- <view icon="user.png" @click="navBack"></view> -->
		<view class="right-top-sign"></view>
		<!-- 设置白色背景防止软键盘把下部绝对定位元素顶上来盖住输入框等 -->
		<view class="wrapper">
			<view class="left-top-sign">register</view>
			<!-- 			<mescroll-body ref="mescrollRef" :down="downOption" :up="upOption" @init="mescrollInit" 
			:auto="false"></mescroll-body> -->
			<view class="welcome">
				{{$t('register.updatePassword')}}
			</view>
			<u-toast ref="uToast"></u-toast>
			<view class="input-content">
				<view class="input-item">
					<text class="tit">{{$t('register.oldPassword')}}</text>
					<input type="number" v-model="form.OldPassword" password data-key="password"
						:placeholder="$t('register.oldPassword_placeholder')" maxlength="20" @input="inputChange" />
				</view>
				<view class="input-item">
					<text class="tit">{{$t('register.newPassword')}}</text>
					<input type="number" v-model="form.UserPWD" password data-key="password"
						:placeholder="$t('register.newPassword_placeholder')" maxlength="20" @input="inputChange" />
				</view>
				<view class="input-item">
					<text class="tit">{{$t('register.newPassword2')}}</text>
					<input type="number" v-model="form.NewPwdTwo" password data-key="password"
						:placeholder="$t('register.newPassword_placeholder2')" maxlength="20" @input="inputChange" />
				</view>
			</view>
			<button class="confirm-btn" type="primary" :ripple="true" ripple-bg-color="#138087" @click="toRegist"
				:disabled="logining">{{$t('register.update')}}</button>
		</view>
		<!-- <view class="register-section">
			已有账号?
			<text @click="navToLogin">马上登录</text>
		</view> -->
	</view>
</template>

<script>
	import MescrollBody from "@/components/mescroll-uni/mescroll-body.vue";
	import MescrollMixin from "@/components/mescroll-uni/mescroll-mixins.js";
	import {
		mapState,
		mapActions
	} from 'vuex'
	import {
		commonMixin
	} from '@/common/mixin/mixin.js'
	import {
		isMobile,
		isPassword
	} from '../../utils/validate'
	import _config from '../../utils/config'; // 导入私有配置
	export default {
		mixins: [MescrollMixin, commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			MescrollBody
		},
		data() {
			return {
				form: {
					UserCode: '123',
					UserPWD: '',
					OldPassword: '',
				},
				mobile: '',
				password: '',

				logining: false
			}
		},
		onLoad() {
			if (!this.loginInfo.hasLogin) {
				uni.navigateTo({
					url: '/pages/public/login'
				})
				return;
			}
		},
		methods: {
			...mapActions('user', ['UpdateAppPassword']),
			inputChange(e) {
				const key = e.currentTarget.dataset.key;
				this[key] = e.detail.value;
			},
			navBack() {
				uni.navigateBack();
			},
			navToLogin() {

			},
			toRegist() {
				this.logining = true;
				if (this.form.OldPassword == "") {
					this.$refs.uToast.show({
						title: this.$t('register.oldPassword_placeholder_Tips'),
						type: 'warning'
					});
					return;
				}
				if (this.form.UserPWD == "") {
					this.$refs.uToast.show({
						title: this.$t('register.newPassword_placeholder_Tips'),
						type: 'warning'
					});
					return;
				}
				if (this.form.NewPwdTwo == "") {
					this.$refs.uToast.show({
						title: this.$t('register.newPassword_placeholder2_Tips'),
						type: 'warning'
					});
					return;
				}
				if (this.form.NewPwdTwo != this.form.UserPWD) {
					this.$refs.uToast.show({
						title: this.$t('register.passwordError'),
						type: 'warning'
					});
					return;
				}
				var obj = {
					UserCode: this.loginInfo.result.UserCode,
					OldPassword: this.form.OldPassword,
					NewPassword: this.form.NewPwdTwo,
				}
				this.UpdateAppPassword(obj).then(res => {
					if (res.code == "100") {
						this.$refs.uToast.show({
							title: this.$t('register.success'),
							type: 'success'
						});
						setTimeout(function() {
							uni.switchTab({
								url: "/pages/index/index"
							});
						}, 1000)
					} else {
						this.$refs.uToast.show({
							title: this.$t('register.error') + res.Message,
							type: 'warning'
						});
						this.logining = false;
						return;
					}
				}).catch(error => {
					this.$refs.uToast.show({
						title: this.$t("common.requestError"),
						type: 'warning'
					});
					this.logining = false;
					return;
				})
			}
		},

	}
</script>

<style lang='scss' scoped>
	page {
		background: #fff;
	}

	.container {
		padding-top: 65px;
		position: relative;
		width: 100vw;
		height: 100vh;
		overflow: hidden;
		background: #fff;
	}

	.wrapper {
		position: relative;
		z-index: 90;
		background: #fff;
		padding-bottom: 40upx;
	}

	.back-btn {
		position: absolute;
		left: 40upx;
		z-index: 9999;
		padding-top: var(--status-bar-height);
		top: 40upx;
		font-size: 40upx;
		color: $font-color-dark;
	}

	.left-top-sign {
		font-size: 120upx;
		color: $page-color-base;
		position: relative;
		left: -16upx;
	}

	.right-top-sign {
		position: absolute;
		top: 80upx;
		right: -30upx;
		z-index: 95;

		&:before,
		&:after {
			display: block;
			content: "";
			width: 400upx;
			height: 80upx;
			background: #b4f3e2;
		}

		&:before {
			transform: rotate(50deg);
			border-radius: 0 50px 0 0;
		}

		&:after {
			position: absolute;
			right: -198upx;
			top: 0;
			transform: rotate(-50deg);
			border-radius: 50px 0 0 0;
			/* background: pink; */
		}
	}

	.left-bottom-sign {
		position: absolute;
		left: -270upx;
		bottom: -320upx;
		border: 100upx solid #d0d1fd;
		border-radius: 50%;
		padding: 180upx;
	}

	.welcome {
		position: relative;
		left: 50upx;
		top: -90upx;
		font-size: 46upx;
		color: #555;
		text-shadow: 1px 0px 1px rgba(0, 0, 0, .3);
	}

	.input-content {
		padding: 0 60upx;
	}

	.input-item {
		display: flex;
		flex-direction: column;
		align-items: flex-start;
		justify-content: center;
		padding: 0 30upx;
		background: $page-color-light;
		height: 120upx;
		border-radius: 4px;
		margin-bottom: 50upx;

		&:last-child {
			margin-bottom: 0;
		}

		.tit {
			height: 50upx;
			line-height: 56upx;
			font-size: $font-sm+2upx;
			color: $font-color-base;
		}

		input {
			height: 60upx;
			font-size: $font-base + 2upx;
			color: $font-color-dark;
			width: 100%;
		}
	}

	.confirm-btn {
		width: 630upx;
		height: 76upx;
		line-height: 76upx;
		margin-top: 70upx;
		background: $uni-color-primary;
		color: #fff;
		font-size: $font-lg;

		&:after {
			border-radius: 100px;
		}
	}

	.forget-section {
		font-size: $font-sm+2upx;
		color: $font-color-spec;
		text-align: center;
		margin-top: 40upx;
	}

	.register-section {
		position: absolute;
		left: 0;
		bottom: 50upx;
		width: 100%;
		font-size: $font-sm+2upx;
		color: $font-color-base;
		text-align: center;

		text {
			color: $font-color-spec;
			margin-left: 10upx;
		}
	}
</style>