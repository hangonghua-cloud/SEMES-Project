using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MESClient
{
    public partial class PagerControl : UserControl
    {
        private int record = 0;

        /// <summary>
        /// 总记录数
        /// </summary>
        public int Record
        {
            get { return record; }
            set
            {
                record = value;
                InitPageInfo();
            }
        }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize
        {
            get { return Convert.ToInt32(cmbPerRow.SelectedItem); }
            //get { return pageSize; }
            //set { pageSize = value; }

        }

        private int currentPage = 1;

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }

        public int pageNum = 0;

        /// <summary>
        /// 总页码
        /// </summary>
        public int PageNum
        {
            get
            {
                if (Record == 0)
                {
                    pageNum = 0;
                }
                else
                {
                    if (Record % PageSize > 0)
                    {
                        pageNum = Record / PageSize + 1;
                    }
                    else
                    {
                        pageNum = Record / PageSize;
                    }
                }
                return pageNum;
            }

        }

        //定义委托
        public delegate void BindHandle();

        /// <summary>
        /// 绑定数据源事件
        /// </summary>
        public event BindHandle BindSource;

        public PagerControl()
        {
            InitializeComponent();
            if (cmbPerRow.SelectedValue == null) cmbPerRow.SelectedIndex = 0;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (Record > 0)
            {
                if (CurrentPage == 1)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("PagerControl.FirstTip", "当前已经是首页"));
                    return;
                }
                else
                {
                    CurrentPage = 1;
                    if (BindSource != null)
                    {
                        BindSource();
                        InitPageInfo();
                    }
                }
            }

        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (Record > 0)
            {
                if (CurrentPage == 1)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("PagerControl.FirstTip", "当前已经是首页"));
                    return;
                }
                else
                {
                    CurrentPage = CurrentPage - 1;
                    if (BindSource != null)
                    {
                        BindSource();
                        InitPageInfo();
                    }
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Record > 0)
            {
                if (CurrentPage == PageNum)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("PagerControl.LastTip", "当前已经是末页"));
                    return;
                }
                else
                {
                    CurrentPage = CurrentPage + 1;
                    if (BindSource != null)
                    {
                        BindSource();
                        InitPageInfo();
                    }
                }
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (Record > 0)
            {
                if (CurrentPage == PageNum)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("PagerControl.LastTip", "当前已经是末页"));
                    return;
                }
                else
                {
                    CurrentPage = PageNum;
                    if (BindSource != null)
                    {
                        BindSource();
                        InitPageInfo();
                    }
                }
            }
        }

        private void InitPageInfo()
        {
            if (Record == 0 || (Record > 0 && CurrentPage > pageNum))
            {
                CurrentPage = 1;
            }
            lblInfo.Text = Lib.Common.Language.GetTextWithParams("PagerControl.Label.lblInfo", "", Record.ToString(), PageNum.ToString(), CurrentPage.ToString());
            txtPage.Text = CurrentPage.ToString();

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (Record > 0)
            {
                if (!string.IsNullOrEmpty(txtPage.Text) && !Regex.IsMatch(txtPage.Text, @"^[\d]*$"))
                {
                    MessageBox.Show(Lib.Common.Language.GetText("PagerControl.GoError", "请正确填写页码！"));
                    return;
                }
                int page = Convert.ToInt32(txtPage.Text);
                if (page == 0)
                {
                    page = 1;
                }
                if (page > PageNum)
                {
                    page = PageNum;
                }

                CurrentPage = page;
                if (BindSource != null)
                {
                    BindSource();
                    InitPageInfo();
                }
            }

        }

        private void PagerControl_Load(object sender, EventArgs e)
        {
            //if (BindSource != null)
            //{
            //    BindSource();
            //    InitPageInfo();
            //}

            //由于一个页面会有多个，单独每个form配置太费劲直接修改
            //Lib.Resources.FormLanguage.SetFormLanguage(this);
            this.SetFormLanguage();

            Util.FormLayout.SetFormLayout(this, "PagerControl");
            //this.label1.Left = this.cmbPerRow.Left - this.label1.Width - 10;
            //Lib.Common.Language.SetVoidLanguage(() =>
            //{
            //    this.label1.Left = this.cmbPerRow.Left - this.label1.Width - 10;
            //},
            //() =>
            //{
            //    this.label1.Left = this.cmbPerRow.Left - this.label1.Width - 10;
            //});
        }
        private void SetFormLanguage()
        {
            this.lbl_PageSize.Text = Lib.Common.Language.GetText("PagerControl.Label.label1", this.lbl_PageSize.Text);
            this.btnFirst.Text = Lib.Common.Language.GetText("PagerControl.Button.btnFirst", this.btnFirst.Text);
            this.btnPre.Text = Lib.Common.Language.GetText("PagerControl.Button.btnPre", this.btnPre.Text);
            this.btnNext.Text = Lib.Common.Language.GetText("PagerControl.Button.btnNext", this.btnNext.Text);
            this.btnLast.Text = Lib.Common.Language.GetText("PagerControl.Button.btnLast", this.btnLast.Text);
        }
        private void cmbPerRow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BindSource != null)
            {
                BindSource();
                InitPageInfo();
            }
        }
    }

}
