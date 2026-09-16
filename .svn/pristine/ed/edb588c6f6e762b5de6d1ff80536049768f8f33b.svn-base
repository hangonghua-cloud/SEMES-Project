<script>
	/**
	 * vuex管理登陆状态，具体可以参考官方登陆模板示例
	 */
	import homeBtn from '@/components/homeBtn/homeBtn.vue'
	import {
		mapState,
		mapActions
	} from 'vuex'
	import global from './utils/global.js'
	export default {
		components: {
			homeBtn
		},
		methods: {
			...mapActions('common', ['coinList']),
		},
		created() {
			//全局事件订阅只要注册的页面都可以收到回调值
			uni.$on("RefreshStyle", (val, opts) => {
				console.log("=======外=======");
				if (opts.click && opts.change) {
					// this.$router.go(0)
					location.reload();
				}
				if (val == "zh-CN") {
					require('./common/style/CN.scss');
				}
			 	else if (val == "vi_VN") {
					console.log("=======**=======");
					console.log(val);
					require('./common/style/VN.scss');
				}
			})
		},
		onLaunch: function() {
			let $this = this
			this.$fire.$on("refreshCoin", () => {
				$this.coinList()
			});
			this.$store.dispatch('WEBSOCKET_INIT', 'wss://api.hadax.com/ws')
		},
		onShow: function() {
			console.log('App Show')
		},
		onHide: function() {
			console.log('App Hide');
		}
	}
</script>

<style lang='scss'>
	@import "uview-ui/index.scss";
	/* 
	uni-view.u-form-item--left__content__label {
		font-size: 14px;
		line-height: 20px;
		width: 100px;
	} */

	/*
		全局公共样式和字体图标
	*/
	
	@font-face {
		font-family: yticon;
		font-weight: normal;
		font-style: normal;
		src: url('./static/font/font_1078604_w4kpxh0rafi.ttf') format('truetype');
	}

	.yticon {
		font-family: "yticon" !important;
		font-size: 16px;
		font-style: normal;
		-webkit-font-smoothing: antialiased;
		-moz-osx-font-smoothing: grayscale;
	}

	.icon-yiguoqi1:before {
		content: "\e700";
	}

	.icon-iconfontshanchu1:before {
		content: "\e619";
	}

	.icon-iconfontweixin:before {
		content: "\e611";
	}

	.icon-alipay:before {
		content: "\e636";
	}

	.icon-shang:before {
		content: "\e624";
	}

	.icon-shouye:before {
		content: "\e626";
	}

	.icon-shanchu4:before {
		content: "\e622";
	}

	.icon-xiaoxi:before {
		content: "\e618";
	}

	.icon-jiantour-copy:before {
		content: "\e600";
	}

	.icon-fenxiang2:before {
		content: "\e61e";
	}

	.icon-pingjia:before {
		content: "\e67b";
	}

	.icon-daifukuan:before {
		content: "\e68f";
	}

	.icon-pinglun-copy:before {
		content: "\e612";
	}

	.icon-dianhua-copy:before {
		content: "\e621";
	}

	.icon-shoucang:before {
		content: "\e645";
	}

	.icon-xuanzhong2:before {
		content: "\e62f";
	}

	.icon-gouwuche_:before {
		content: "\e630";
	}

	.icon-icon-test:before {
		content: "\e60c";
	}

	.icon-icon-test1:before {
		content: "\e632";
	}

	.icon-bianji:before {
		content: "\e646";
	}

	.icon-jiazailoading-A:before {
		content: "\e8fc";
	}

	.icon-zuoshang:before {
		content: "\e613";
	}

	.icon-jia2:before {
		content: "\e60a";
	}

	.icon-huifu:before {
		content: "\e68b";
	}

	.icon-sousuo:before {
		content: "\e7ce";
	}

	.icon-arrow-fine-up:before {
		content: "\e601";
	}

	.icon-hot:before {
		content: "\e60e";
	}

	.icon-lishijilu:before {
		content: "\e6b9";
	}

	.icon-zhengxinchaxun-zhifubaoceping-:before {
		content: "\e616";
	}

	.icon-naozhong:before {
		content: "\e64a";
	}

	.icon-xiatubiao--copy:before {
		content: "\e608";
	}

	.icon-shoucang_xuanzhongzhuangtai:before {
		content: "\e6a9";
	}

	.icon-jia1:before {
		content: "\e61c";
	}

	.icon-bangzhu1:before {
		content: "\e63d";
	}

	.icon-arrow-left-bottom:before {
		content: "\e602";
	}

	.icon-arrow-right-bottom:before {
		content: "\e603";
	}

	.icon-arrow-left-top:before {
		content: "\e604";
	}

	.icon-icon--:before {
		content: "\e744";
	}

	.icon-xia:before {
		content: "\e62d";
	}

	.icon--jianhao:before {
		content: "\e60b";
	}

	.icon-weixinzhifu:before {
		content: "\e61a";
	}

	.icon-comment:before {
		content: "\e64f";
	}

	.icon-weixin:before {
		content: "\e61f";
	}

	.icon-fenlei1:before {
		content: "\e620";
	}

	.icon-erjiye-yucunkuan:before {
		content: "\e623";
	}

	.icon-Group-:before {
		content: "\e688";
	}

	.icon-you:before {
		content: "\e606";
	}

	.icon-forward:before {
		content: "\e607";
	}

	.icon-tuijian:before {
		content: "\e610";
	}

	.icon-bangzhu:before {
		content: "\e679";
	}

	.icon-share:before {
		content: "\e656";
	}

	.icon-yiguoqi:before {
		content: "\e997";
	}

	.icon-shezhi1:before {
		content: "\e61d";
	}

	.icon-fork:before {
		content: "\e61b";
	}

	.icon-kafei:before {
		content: "\e66a";
	}

	.icon-iLinkapp-:before {
		content: "\e654";
	}

	.icon-saomiao:before {
		content: "\e60d";
	}

	.icon-shezhi:before {
		content: "\e60f";
	}

	.icon-shouhoutuikuan:before {
		content: "\e631";
	}

	.icon-gouwuche:before {
		content: "\e609";
	}

	.icon-dizhi:before {
		content: "\e614";
	}

	.icon-fenlei:before {
		content: "\e706";
	}

	.icon-xingxing:before {
		content: "\e70b";
	}

	.icon-tuandui:before {
		content: "\e633";
	}

	.icon-zuanshi:before {
		content: "\e615";
	}

	.icon-zuo:before {
		content: "\e63c";
	}

	.icon-shoucang2:before {
		content: "\e62e";
	}

	.icon-shouhuodizhi:before {
		content: "\e712";
	}

	.icon-yishouhuo:before {
		content: "\e71a";
	}

	.icon-dianzan-ash:before {
		content: "\e617";
	}

	@font-face {
		font-family: fexfont;
		font-weight: normal;
		font-style: normal;
		src: url('./static/font/fexfont.ttf') format('truetype');
	}

	.fexfont {
		font-family: "fexfont" !important;
		font-size: 16px;
		font-style: normal;
		-webkit-font-smoothing: antialiased;
		-moz-osx-font-smoothing: grayscale;
	}

	.icon-UnionPay {
		font-size: 26px;
		margin-right: 8px;
		color: #f5a623;
	}

	.icon-UnionPay:before {
		content: "\e612";
	}

	.icon-Wechat {
		font-size: 26px;
		margin-right: 8px;
		color: #ff5500;
	}

	.icon-Wechat:before {
		content: "\e6d8";
	}

	.icon-Alipay {
		font-size: 26px;
		margin-right: 8px;
		color: #1296db;
	}

	.icon-Alipay:before {
		content: "\e760";
	}

	.icon-shaixuan:before {
		content: "\e671";
	}

	.icon-weixin:before {
		content: "\e63e";
	}

	.icon-yinhangqia:before {
		content: "\e873";
	}

	.icon-zhifubao:before {
		content: "\e68a";
	}

	.icon-paypal:before {
		content: "\e68b";
	}

	.icon-yinlian:before {
		content: "\e68c";
	}

	.icon-12:before {
		content: "\e63d";
	}

	.icon-work-copy:before {
		content: "\e611";
	}

	.icon-xitonggonggao:before {
		content: "\e673";
	}

	.icon-bangzhu:before {
		content: "\e656";
	}

	.icon-shequguanli:before {
		content: "\e628";
	}

	.icon-tuijian:before {
		content: "\e627";
	}

	.icon-anquan:before {
		content: "\e681";
	}

	.icon-fankui:before {
		content: "\e64f";
	}

	.icon-zijin:before {
		content: "\e62d";
	}

	.icon-shezhi:before {
		content: "\e62e";
	}

	.icon-bianminshenghuo:before {
		content: "\e62f";
	}

	.icon-fengxinpinggu:before {
		content: "\e630";
	}

	.icon-zhibo:before {
		content: "\e631";
	}

	.icon-qushi:before {
		content: "\e632";
	}

	.icon-zaixiankefu:before {
		content: "\e633";
	}

	.icon-xinxuanshangcheng:before {
		content: "\e634";
	}

	.icon-fulizhongxin:before {
		content: "\e635";
	}

	.icon-hudongshequ:before {
		content: "\e636";
	}

	.icon-licai:before {
		content: "\e637";
	}

	.icon-lingquhongbao:before {
		content: "\e638";
	}

	.icon-bangdan:before {
		content: "\e639";
	}

	.icon-gerenzhongxin:before {
		content: "\e63a";
	}

	.icon-qianbao:before {
		content: "\e63b";
	}

	.icon-xiaoxi:before {
		content: "\e63c";
	}

	view,
	scroll-view,
	swiper,
	swiper-item,
	cover-view,
	cover-image,
	icon,
	text,
	rich-text,
	progress,
	button,
	checkbox,
	form,
	input,
	label,
	radio,
	slider,
	switch,
	textarea,
	navigator,
	audio,
	camera,
	image,
	video {
		box-sizing: border-box;
	}

	/* 骨架屏替代方案 */
	.Skeleton {
		background: #f3f3f3;
		padding: 20upx 0;
		border-radius: 8upx;
	}

	/* 图片载入替代方案 */
	.image-wrapper {
		font-size: 0;
		background: #f3f3f3;
		border-radius: 4px;

		image {
			width: 100%;
			height: 100%;
			transition: .6s;
			opacity: 0;

			&.loaded {
				opacity: 1;
			}
		}
	}

	.clamp {
		overflow: hidden;
		text-overflow: ellipsis;
		white-space: nowrap;
		display: block;
	}

	.common-hover {
		background: #f5f5f5;
	}

	/*边框*/
	.b-b:after,
	.b-t:after {
		position: absolute;
		z-index: 3;
		left: 0;
		right: 0;
		height: 0;
		content: '';
		transform: scaleY(.5);
		border-bottom: 1px solid $border-color-base;
	}

	.b-b:after {
		bottom: 0;
	}

	.b-t:after {
		top: 0;
	}

	/* button样式改写 */
	uni-button,
	button {
		height: 80upx;
		line-height: 80upx;
		font-size: $font-lg + 2upx;
		font-weight: normal;

		&.no-border:before,
		&.no-border:after {
			border: 0;
		}
	}

	uni-button[type=default],
	button[type=default] {
		color: $font-color-dark;
	}

	/* input 样式 */
	.input-placeholder {
		color: #999999;
	}

	.placeholder {
		color: #999999;
	}


	.little-line {
		position: relative;
	}

	.little-line:after {
		content: " ";
		position: absolute;
		left: 0;
		bottom: 0;
		width: 100%;
		height: 1px;
		background-color: #DCDFE6;
		/* 如果不用 background-color, 使用 border-top:1px solid blue; */
		-webkit-transform: scaleY(.5);
		transform: scaleY(.5);
	}

	.line {
		width: 100%;
		height: 20upx;
		background: #EEF2F5;
	}

	.upper-text {
		color: $uni-color-upper
	}

	.lower-text {
		color: $uni-color-lower
	}

	/* 	 @font-face {
	 	font-family: yticon;
	 	font-weight: normal;
	 	font-style: normal;
	 	src: url('./static/font/font_1078604_w4kpxh0rafi.ttf') format('truetype');
	 }
	 
	 .yticon {
	 	font-family: "yticon" !important;
	 	font-size: 16px;
	 	font-style: normal;
	 	-webkit-font-smoothing: antialiased;
	 	-moz-osx-font-smoothing: grayscale;
	 }
	 */
	@font-face {
		font-family: 'customicon';
		font-weight: normal;
		font-style: normal;
		src: url('./static/font/iconfont/iconfont.ttf') format('truetype');
	}

	.customicon {
		font-family: customicon !important;
		font-size: 16px;
		font-style: normal;
		-webkit-font-smoothing: antialiased;
		-moz-osx-font-smoothing: grayscale;
	}

	//此处引用第三方图标库
	.iconshengqian:before {
		content: "\e6d8";
	}

	.iconcangku:before {
		content: "\e6ea";
	}

	.iconchengpinrukuyuchukuguanli:before {
		content: "\e6d9";
	}

	.iconchanchengpinrukuzhuancang:before {
		content: "\e6da";
	}

	.iconicon-p_wuliaozu:before {
		content: "\e6db";
	}

	.iconicon-p_wuliaozhushuju:before {
		content: "\e6dc";
	}

	.iconyiku:before {
		content: "\e6dd";
	}

	.iconwuliaocaigou:before {
		content: "\e6de";
	}

	.iconyuancailiaoshujuku:before {
		content: "\e6df";
	}

	.iconchuku:before {
		content: "\e6e0";
	}

	.iconruku6:before {
		content: "\e6e1";
	}

	.iconyuanpiancangku:before {
		content: "\e6e2";
	}

	.iconchengpincangku:before {
		content: "\e6e3";
	}

	.iconcangku_zhongzhuanchuzhan:before {
		content: "\eac9";
	}

	.iconcangku_daozhan:before {
		content: "\eaca";
	}

	.iconcangku_daozhan_o:before {
		content: "\eb51";
	}

	.iconyuancailiaojiagong:before {
		content: "\e6e4";
	}

	.iconwuliaoguanli:before {
		content: "\e6e5";
	}

	.iconwuliaoguanli-:before {
		content: "\e6eb";
	}

	.iconchuku1:before {
		content: "\eb1c";
	}

	.icontubiao_chengpinku:before {
		content: "\e6ec";
	}

	.iconcangkudajian:before {
		content: "\e6fd";
	}

	.iconicon_function_chuku:before {
		content: "\e889";
	}

	.iconwuliaotoufang:before {
		content: "\e6fe";
	}

	.iconicon-test2:before {
		content: "\e6ff";
	}

	.iconbiaoqian:before {
		content: "\e700";
	}

	.iconchuku2:before {
		content: "\e703";
	}

	.iconchuku3:before {
		content: "\e704";
	}

	.iconcangku-chu:before {
		content: "\e705";
	}

	.iconwuliaodangan:before {
		content: "\e706";
	}

	.icon-_zhongxincangku:before {
		content: "\e707";
	}

	.iconchukudan:before {
		content: "\e708";
	}

	.iconcangku1:before {
		content: "\e709";
	}

	.iconchukuguanli:before {
		content: "\e7fd";
	}

	.iconchuku4:before {
		content: "\e70a";
	}

	.iconruku7:before {
		content: "\e70b";
	}

	.iconbiaoqian1:before {
		content: "\ec0f";
	}

	.iconchukuguanli1:before {
		content: "\e70c";
	}

	.iconyuancailiaoxunjianQAquerenliebiao:before {
		content: "\e70d";
	}

	.iconyuancailiaoxunjianQAquerenliebiao1:before {
		content: "\e70e";
	}

	.iconyuancailiaochuku:before {
		content: "\e70f";
	}

	.iconyuancailiaolingliaoliebiao:before {
		content: "\e710";
	}

	.iconyuancailiaoyiku:before {
		content: "\e711";
	}

	.iconchengpinrukuqueren:before {
		content: "\e712";
	}

	.iconbiaoqian2:before {
		content: "\e713";
	}

	.iconotherWarehouses:before {
		content: "\e714";
	}

	.iconchukuout:before {
		content: "\e715";
	}

	.iconchengpinzuizhongjiancejilubiao:before {
		content: "\e716";
	}

	.icon01zhushuju_kehuwuliao:before {
		content: "\e717";
	}

	.iconicon-svg-07:before {
		content: "\e718";
	}

	.iconicon-svg-08:before {
		content: "\e719";
	}

	.iconchukuguanli2:before {
		content: "\e71c";
	}

	.icon39cangkuguanli:before {
		content: "\e71d";
	}

	.iconbiaoqian3:before {
		content: "\f194";
	}

	.iconchukudan1:before {
		content: "\e71e";
	}

	.iconwuliaoxinxi:before {
		content: "\e71f";
	}

	.iconwuliaobaobeidan:before {
		content: "\e720";
	}

	.iconcangkuguanli:before {
		content: "\e721";
	}

	.iconbiaoqian4:before {
		content: "\e722";
	}

	.icondun:before {
		content: "\e6ed";
	}

	.iconjinggao:before {
		content: "\e6ee";
	}

	.iconfenlei:before {
		content: "\e6f0";
	}

	.iconshexiangtou:before {
		content: "\e6f1";
	}

	.iconliulanqi:before {
		content: "\e6f2";
	}

	.iconyunsuo:before {
		content: "\e6f3";
	}

	.iconzhiwen:before {
		content: "\e6f4";
	}

	.icon008minus04:before {
		content: "\e79d";
	}

	.icon017check03:before {
		content: "\e79e";
	}

	.icon013cross02:before {
		content: "\e79f";
	}

	.icon029cancel:before {
		content: "\e7a0";
	}

	.icon025ban:before {
		content: "\e7a1";
	}

	.icon003plus01:before {
		content: "\e7a2";
	}

	.icon037trash:before {
		content: "\e7a3";
	}

	.icon052expand:before {
		content: "\e7a4";
	}

	.icon091under02:before {
		content: "\e7a5";
	}

	.icon103bookTX:before {
		content: "\e7a7";
	}

	.icon111image:before {
		content: "\e7a9";
	}

	.icon133follow02:before {
		content: "\e7aa";
	}

	.icon120menu02:before {
		content: "\e7ab";
	}

	.icon131keyboard:before {
		content: "\e7ac";
	}

	.icon130wifi:before {
		content: "\e7ad";
	}

	.icon141send:before {
		content: "\e7ae";
	}

	.icon144message02:before {
		content: "\e7af";
	}

	.icon197hourglass01:before {
		content: "\e7b0";
	}

	.icon174txt:before {
		content: "\e7b1";
	}

	.icon219shield05:before {
		content: "\e7b2";
	}

	.icon229circlePY:before {
		content: "\e7b3";
	}

	.icon259send01:before {
		content: "\e7b8";
	}

	.icon247arrowXX05:before {
		content: "\e7b4";
	}

	.icon246arrowXS06:before {
		content: "\e7b6";
	}

	.icon289folder01:before {
		content: "\e7ba";
	}

	.icon266pen01:before {
		content: "\e7bb";
	}

	.icon240zhome03:before {
		content: "\e7b9";
	}

	.icon244arrowXZ06:before {
		content: "\e7bc";
	}

	.icon258choose01:before {
		content: "\e7bd";
	}

	.icon297textleft:before {
		content: "\e7be";
	}

	.icon295rotate:before {
		content: "\e7bf";
	}

	.icon248arrowXZ07:before {
		content: "\e7c0";
	}

	.icon196calendar:before {
		content: "\e7c1";
	}

	.icon325suspend:before {
		content: "\e7c2";
	}

	.icon322annotation:before {
		content: "\e7c3";
	}

	.icon327paixu:before {
		content: "\e7c4";
	}

	.icon204phone:before {
		content: "\e7c5";
	}

	.icon344:before {
		content: "\e7c7";
	}

	.icon330:before {
		content: "\e7c9";
	}

	.icon336min02:before {
		content: "\e7ca";
	}

	.icon349:before {
		content: "\e7cb";
	}

	.icon365:before {
		content: "\e7cd";
	}

	.icon373:before {
		content: "\e7ce";
	}

	.icon124notice:before {
		content: "\e7c6";
	}

	.icon108share:before {
		content: "\e7c8";
	}

	.iconbaoyang:before {
		content: "\e602";
	}

	.iconrukuguanli:before {
		content: "\e659";
	}

	.iconruku:before {
		content: "\e60f";
	}

	.iconrukuguanli1:before {
		content: "\e61f";
	}

	.iconbeihuodan:before {
		content: "\e638";
	}

	.iconshengchanlingliaoyutuiliao:before {
		content: "\e641";
	}

	.iconlailiaopinzhijianyan:before {
		content: "\e642";
	}

	.iconjianyandan:before {
		content: "\e6e8";
	}

	.iconshouhuo:before {
		content: "\e636";
	}

	.iconcheliangweixiubaoyang:before {
		content: "\e617";
	}

	.iconrukuqingdan:before {
		content: "\e607";
	}

	.icontouliao:before {
		content: "\e64c";
	}

	.iconnavicon-rkyw:before {
		content: "\e657";
	}

	.iconzhiliangjianyan:before {
		content: "\e621";
	}

	.iconjianyanpizhiliangjianyanjilu:before {
		content: "\e626";
	}

	.iconzhiliangjianyan1:before {
		content: "\e632";
	}

	.iconshangliao:before {
		content: "\e8e6";
	}

	.iconrukuC:before {
		content: "\e608";
	}

	.iconbanchengpinjianyan:before {
		content: "\e622";
	}

	.iconbeihuochuku:before {
		content: "\e65b";
	}

	.iconshouhuoruku:before {
		content: "\e662";
	}

	.iconshouhuo1:before {
		content: "\e610";
	}

	.iconshouhuoshou--:before {
		content: "\e62d";
	}

	.iconrukuguanli2:before {
		content: "\e627";
	}

	.iconqichebaoyangweixiushenqing:before {
		content: "\e614";
	}

	.iconrukuguanli-:before {
		content: "\e634";
	}

	.iconRectangleCopy:before {
		content: "\e6c3";
	}

	.iconbiaoji:before {
		content: "\e604";
	}

	.iconchengpinjianyan:before {
		content: "\e63c";
	}

	.iconrukuguanli3:before {
		content: "\e644";
	}

	.icontouliao2:before {
		content: "\e600";
	}

	.iconshangliaoqueren:before {
		content: "\e6b8";
	}

	.iconguochengjianyanchejian:before {
		content: "\e7d6";
	}

	.iconicon_function_ruku:before {
		content: "\e88b";
	}

	.iconbanchengpinjianyan1:before {
		content: "\e6c1";
	}

	.iconruku1:before {
		content: "\e60c";
	}

	.iconguochengjianyan:before {
		content: "\e606";
	}

	.icontouliao3:before {
		content: "\e640";
	}

	.iconchengpinjianyan1:before {
		content: "\e615";
	}

	.iconruku2:before {
		content: "\e60a";
	}

	.iconzu-:before {
		content: "\e623";
	}

	.iconchengpinjianyanqingdandaoru:before {
		content: "\e7dc";
	}

	.icongoodswhinStock:before {
		content: "\e663";
	}

	.iconruku3:before {
		content: "\e628";
	}

	.iconlunkuohua22_ruku:before {
		content: "\e660";
	}

	.iconshebeidianjian:before {
		content: "\e609";
	}

	.iconshebeidianjian1:before {
		content: "\e666";
	}

	.iconruku4:before {
		content: "\e629";
	}

	.iconchejiantuiliaojiaojie:before {
		content: "\e601";
	}

	.iconchejiantuiliaojiaojie1:before {
		content: "\e603";
	}

	.iconchejiantuiliaojiaojie2:before {
		content: "\e605";
	}

	.icontouliao4:before {
		content: "\e693";
	}

	.iconchengpinjianyan2:before {
		content: "\e6ef";
	}

	.icontubiaozhizuomoban-68:before {
		content: "\e618";
	}

	.icontubiaozhizuomoban-133:before {
		content: "\e631";
	}

	.iconbiaoji1:before {
		content: "\e6a5";
	}

	.iconguochengjianyan1:before {
		content: "\e763";
	}

	.iconbiaozhu:before {
		content: "\e624";
	}

	.iconrukuguanli4:before {
		content: "\e619";
	}

	.iconrukuguanli5:before {
		content: "\e62a";
	}

	.iconguochengjianyan_huaban1:before {
		content: "\e680";
	}

	.iconshengchantuiliaobeifen2x:before {
		content: "\e60b";
	}

	.icongongdan:before {
		content: "\e60d";
	}

	.iconbill:before {
		content: "\e60e";
	}

	.iconsousuo:before {
		content: "\e7a6";
	}

	.icontuichu:before {
		content: "\e625";
	}

	.iconjihuashu:before {
		content: "\e62c";
	}

	.iconmima:before {
		content: "\e66f";
	}

	.iconsousuo1:before {
		content: "\e684";
	}

	.icontubiao7:before {
		content: "\e639";
	}

	.iconmima1:before {
		content: "\e62b";
	}

	.iconshengchanrenwudanguanli:before {
		content: "\e611";
	}

	.icontuichu1:before {
		content: "\e65d";
	}

	.iconjihuajindu:before {
		content: "\e612";
	}

	.iconyonghu:before {
		content: "\e633";
	}

	.iconmimaffffffpx:before {
		content: "\e613";
	}

	.icondianchi:before {
		content: "\e616";
	}

	.iconbattery:before {
		content: "\e761";
	}

	.iconicon-test:before {
		content: "\e62e";
	}

	.iconjihua:before {
		content: "\e63a";
	}

	.icondianchi1:before {
		content: "\e61a";
	}

	.iconyonghu1:before {
		content: "\e61b";
	}

	.iconmima2:before {
		content: "\e658";
	}

	.iconmima3:before {
		content: "\e61c";
	}

	.iconshanchu:before {
		content: "\e61d";
	}

	.iconshengchan-:before {
		content: "\e61e";
	}

	.iconshezhi:before {
		content: "\e620";
	}

	.iconyiliaozhiliangfenxi:before {
		content: "\e685";
	}

	.iconshanchu1:before {
		content: "\e62f";
	}

	.iconshaixuan:before {
		content: "\e630";
	}

	.iconcopy:before {
		content: "\e635";
	}

	.iconsousuo2:before {
		content: "\e637";
	}

	.iconnengliangzhi:before {
		content: "\e643";
	}

	.iconbianji:before {
		content: "\e63b";
	}

	.iconbianji1:before {
		content: "\e63d";
	}

	.iconbianji2:before {
		content: "\e63e";
	}

	.iconziyuan142:before {
		content: "\e6e6";
	}

	.iconzhiliang:before {
		content: "\e701";
	}

	.iconshengchanzhoubao:before {
		content: "\e63f";
	}

	.iconshengchanyuebao:before {
		content: "\e645";
	}

	.icontubiao5:before {
		content: "\e646";
	}

	.icon6:before {
		content: "\e647";
	}

	.iconshaixuan1:before {
		content: "\e68a";
	}

	.iconsousuo3:before {
		content: "\e648";
	}

	.icontubiao:before {
		content: "\e649";
	}

	.iconcangpeitubiao_shanchu:before {
		content: "\e64a";
	}

	.iconjihuashangchuan:before {
		content: "\e71a";
	}

	.iconmima4:before {
		content: "\e64b";
	}

	.iconmima5:before {
		content: "\e64d";
	}

	.iconshezhi01:before {
		content: "\e64e";
	}

	.iconshanchu11:before {
		content: "\e67d";
	}

	.iconjihuaxiazai:before {
		content: "\e67c";
	}

	.icontuichu2:before {
		content: "\e64f";
	}

	.iconshaixuan2:before {
		content: "\e6a8";
	}

	.iconshengchanguanli:before {
		content: "\e650";
	}

	.iconshezhi1:before {
		content: "\e686";
	}

	.iconzhiliangguanli:before {
		content: "\e651";
	}

	.iconshanchu2:before {
		content: "\e6cd";
	}

	.iconjihuashuomingshu:before {
		content: "\e652";
	}

	.iconshaixuan3:before {
		content: "\f030";
	}

	.iconjihua1:before {
		content: "\e83b";
	}

	.iconbianji3:before {
		content: "\e653";
	}
</style>