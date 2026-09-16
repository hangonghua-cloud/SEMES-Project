using Lib.Bll;
using Lib.Model;
using Lib.Model.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MESClient.TransferCard
{
    public partial class Frm_TransferCardAdd : Form
    {
        public dynamic main = new ExpandoObject();
        public TransferCardEntity newCard = new TransferCardEntity();//流转卡

        TransferCardBll bll = new TransferCardBll();
        BaseDataBll _baseBll = new BaseDataBll();//基础数据

        public Frm_TransferCardAdd()
        {
            InitializeComponent();

        }

        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_TransferCardAdd_Load(object sender, EventArgs e)
        {
            try
            {
                Init();
                Assignment();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 确认生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region 数据验证
                //if (string.IsNullOrEmpty(cmbCardType.SelectedValue.ToString()))
                //{
                //    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardAdd.MessageTips_3", "请选择流转卡类型"));
                //    return;
                //}
                if (string.IsNullOrEmpty(cmbNewType.SelectedValue.ToString()))
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardAdd.MessageTips_4", "请选择新建类型"));
                    return;
                }
                //if (string.IsNullOrEmpty(cmbStartProcess.SelectedValue.ToString()))
                //{
                //    MessageBox.Show("请选择起始工序");
                //    return;
                //}
                if (string.IsNullOrEmpty(txtPieceQty.Text.Trim()))
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardAdd.MessageTips_5", "请填写新建片数"));
                    return;
                }
                Regex rex = new Regex(@"^\d+$");//^开始，\d匹配一个数字字符，+出现至少一次，$结尾
                if (!rex.IsMatch(txtPieceQty.Text))
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardAdd.MessageTips_6", "数量格式不正确"));
                    return;
                }
                if (string.IsNullOrEmpty(cmbCard.SelectedValue.ToString()))
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardAdd.MessageTips_7", "请选择要继承的流转卡"));
                    return;
                }
                if (cmbNewType.SelectedValue.ToString() == "1") //生产拆托
                {
                    if (string.IsNullOrEmpty(txtPerPalletPieceQty.Text.Trim()))
                    {
                        MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardAdd.MessageTips_8", "请填写单托片数"));
                        return;
                    }
                    if (!rex.IsMatch(txtPerPalletPieceQty.Text))
                    {
                        MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardAdd.MessageTips_9", "单托片数格式不正确"));
                        return;
                    }
                }
                #endregion

                var cardCode = cmbCard.SelectedValue.ToString();
                var cardEntity = bll.GetCardEntity(cardCode);
                var resumeEntity = bll.GetResumeEntity(cardCode);
                dynamic resumeFlag = new ExpandoObject();
                if (resumeEntity != null && resumeEntity.BusinessType == "2")//完工
                {
                    resumeFlag = bll.GetCardResumeByFlag(cardCode);
                    //MessageBox.Show("当前流转卡已完工，无法新建");
                    //return;
                }

                #region 流转卡
                decimal palletCount = 1;
                List<dynamic> cardList = new List<dynamic>();
                List<dynamic> resumeList = new List<dynamic>();
                if (cmbNewType.SelectedValue.ToString() == "1")
                {
                    palletCount = Math.Ceiling(Convert.ToDecimal(txtPieceQty.Text.Trim()) / Convert.ToDecimal(txtPerPalletPieceQty.Text.Trim()));
                }
                var oldCardCount = bll.GetCardList(cardEntity.ExeWorkOrder).Count;
                string newCardCode = cardCode;
                string newCardName = cardEntity.CardName;
                int startNo = oldCardCount;
                for (int i = 1; i <= palletCount; i++)
                {
                    var newCardEntity = Common.Clone(cardEntity);
                    newCardEntity.Id = Guid.NewGuid().ToString();
                    newCardEntity.CardCode = newCardCode.Substring(0, newCardCode.Length - 2) + (startNo + i).ToString().PadLeft(2, '0');
                    newCardEntity.CardName = newCardName.Substring(0, newCardName.Length - 2) + (startNo + i).ToString().PadLeft(2, '0');
                    //newCardEntity.CardType = cmbCardType.SelectedValue.ToString();
                    newCardEntity.CardType = cardEntity.CardType;// dragon 2023-05-09 改为继承流转卡的类型
                    newCardEntity.NewType = cmbNewType.SelectedValue.ToString();
                    if (resumeEntity != null)
                    {
                        newCardEntity.StartProcess = resumeEntity.ProcessCode;
                    }
                    else
                    {
                        if (cardEntity.CardStatus != "6")//6：包装
                        {
                            newCardEntity.StartProcess = cardEntity.StartProcess;
                        }
                        else
                        {
                            newCardEntity.StartProcess = "";//说明流转卡已到包装工序
                        }
                    }
                    //newCardEntity.StartProcess = resumeEntity == null ? cardEntity.StartProcess : resumeEntity.ProcessCode;
                    newCardEntity.SplitProcess = cardEntity.SplitProcess;
                    if (i == palletCount)
                    {
                        newCardEntity.PieceQty = Convert.ToDecimal(txtPieceQty.Text.Trim()) - Convert.ToDecimal(txtPerPalletPieceQty.Text.Trim()) * (i - 1);
                    }
                    else
                    {
                        newCardEntity.PieceQty = Convert.ToDecimal(txtPerPalletPieceQty.Text.Trim());
                    }
                    newCardEntity.PalletQty = Math.Ceiling(newCardEntity.PieceQty.Value / cardEntity.DXZH.Value);
                    newCardEntity.ShowPalletQty = newCardEntity.PalletQty.ToString() + newCardEntity.ShowPalletQty.Substring(newCardEntity.ShowPalletQty.IndexOf('/'));
                    newCardEntity.Creator = CurrentUser.UserCode;
                    newCardEntity.CreateTime = DateTime.Now;
                    newCardEntity.Creator = CurrentUser.UserCode;
                    newCardEntity.ModifyBy = CurrentUser.UserCode;
                    newCardEntity.ModifyTime = DateTime.Now;
                    cardList.Add(newCardEntity);

                    if (resumeEntity != null)
                    {
                        dynamic newResumeEntity = new ExpandoObject();
                        newResumeEntity.Id = Guid.NewGuid().ToString();
                        newResumeEntity.FactoryCode = resumeEntity.FactoryCode;
                        newResumeEntity.FactoryName = resumeEntity.FactoryName;
                        newResumeEntity.ProcessCode = resumeEntity.ProcessCode;
                        newResumeEntity.CardCode = newCardEntity.CardCode;
                        newResumeEntity.Flag = "1";
                        newResumeEntity.SheetQty = 0;
                        newResumeEntity.PieceQty = 0;
                        newResumeEntity.Creator = CurrentUser.UserCode;
                        if (resumeEntity.BusinessType == "2")
                        {
                            newResumeEntity.BusinessType = "8";//完工时新建流转卡为开工状态
                            newResumeEntity.WhsCode = resumeFlag.WhsCode;
                            newResumeEntity.LocationCode = resumeFlag.LocationCode;
                        }
                        else
                        {
                            newResumeEntity.BusinessType = resumeEntity.BusinessType;
                            newResumeEntity.WhsCode = resumeEntity.WhsCode;
                            newResumeEntity.LocationCode = resumeEntity.LocationCode;
                        }
                        resumeList.Add(newResumeEntity);
                    }
                }
                #endregion

                #region 流转履历



                #endregion
                int result = 0;
                result = bll.SaveBatchTransferCard(cardList, resumeList);
                if (result > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardAdd.MessageTips_1", "新建成功"));
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardAdd.MessageTips_2", "新建失败"));
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 新建类型改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbNewType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbNewType.SelectedValue.ToString()))
                {
                    if (cmbNewType.SelectedValue.ToString() == "1")//生产拆托
                    {
                        lblPerPalletPieceQty.Visible = true;
                        txtPerPalletPieceQty.Visible = true;
                    }
                    else
                    {
                        lblPerPalletPieceQty.Visible = false;
                        txtPerPalletPieceQty.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            #region 流转卡类型
            var lstCardType = _baseBll.GetDictionarySelect("CirculationCardType");
            lstCardType.Insert(0, new Select() { ItemCode = "", ItemName = Lib.Common.Language.GetText("Common.DropDownAll", "-请选择-") });
            cmbCardType.DataSource = lstCardType;
            cmbCardType.ValueMember = "ItemCode";
            cmbCardType.DisplayMember = "ItemName";
            #endregion

            #region 新建类型
            var lstNewType = _baseBll.GetDictionarySelect("NewType");
            lstNewType.Insert(0, new Select() { ItemCode = "", ItemName = Lib.Common.Language.GetText("Common.DropDownAll", "-请选择-") });
            cmbNewType.DataSource = lstNewType;
            cmbNewType.ValueMember = "ItemCode";
            cmbNewType.DisplayMember = "ItemName";
            #endregion

            #region 起始工序
            var factoryCode = main.FactoryCode;
            var lstStartProcess = _baseBll.GetProcessSelectByFactory(factoryCode);
            lstStartProcess.Insert(0, new Select() { ItemCode = "", ItemName = Lib.Common.Language.GetText("Common.DropDownAll", "-请选择-") });
            //cmbStartProcess.DataSource = lstStartProcess;
            //cmbStartProcess.ValueMember = "ItemCode";
            //cmbStartProcess.DisplayMember = "ItemName";
            #endregion

            #region 流转卡列表
            var exeWorkOrder = main.ExeWorkOrder;
            var lstCard = _baseBll.GetCardSelect(exeWorkOrder);
            lstCard.Insert(0, new Select() { ItemCode = "", ItemName = Lib.Common.Language.GetText("Common.DropDownAll", "-请选择-") });
            this.cmbCard.DataSource = lstCard;
            this.cmbCard.ValueMember = "ItemCode";
            this.cmbCard.DisplayMember = "ItemName";

            string selectedCardCode = main.SelectedCardCode;
            this.cmbCard.SelectedValue = selectedCardCode;
            #endregion

            cmbNewType.SelectedValue = "1";
        }

        /// <summary>
        /// 赋值
        /// </summary>
        public void Assignment()
        {
            newCard = bll.GetNewCard(main.ExeWorkOrder);

            txtProductOrder.Text = newCard.ProductOrder;
            txtWorkOrder.Text = newCard.WorkOrder;
            txtExeWorkOrder.Text = newCard.ExeWorkOrder;
            //txtCardCode.Text = newCard.CardCode;
            //txtCardName.Text = newCard.CardName;
            txtContainerNO.Text = newCard.ContainerNO;
            txtSpec.Text = newCard.Spec;
            txtMMXH.Text = newCard.MMXH;
            txtMaterialCode.Text = newCard.MaterialCode;
            txtBWXH.Text = newCard.BWXH;
            txtUV.Text = newCard.UV;
            txtJCGG.Text = newCard.JCGG;
            txtMMCH.Text = main.MMCJ;
            txtKCKX.Text = newCard.KCKX;

        }
        #endregion

    }
}
