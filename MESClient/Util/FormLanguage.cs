using Lib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lib.Resources
{
    public static class FormLanguage
    {
        /// <summary>
        /// 菜单相关的语言格式化
        /// </summary>
        public static void MenuLanguage()
        {

        }
        #region 初始化系统字段，系统语言

        /// <summary>
        /// 
        /// 初始化系统字段，系统语言
        /// 如果有特殊需要，自己写节点名称
        /// </summary>
        /// <param name="_this">用动态参数是因为form和usercontrol都用</param>
        public static void SetFormLanguage(dynamic _this)
        {
            #region 窗体 Form
            _this.Text = Lib.Common.Language.GetText(_this.Name + ".Title", _this.Text);
            #endregion

            //#region 其他类型 Other
            //this.groupBox1.Text = Lib.Common.Language.GetText(this.Name + ".Other.test");
            //#endregion

            SetAllControls(_this, _this.Controls);

        }
        #endregion




        #region 循环控件上所有的组件
        /// <summary>
        /// 循环控件上所有的组件
        /// 如果存在类似于box容器之类的控件，递归修改
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="sonControls"></param>
        private static void SetAllControls(dynamic _this, Control.ControlCollection sonControls)
        {
            //遍历所有控件
            foreach (Control control in sonControls)
            {
                /*
                control 中有HasChildren 可以判断组件是否含有其他组件，
                但是datagridview也会进入判断，不确定其他的控件会不会有此类问题，
                此项目暂时只考虑用到的容器控件。
                */

                if (control is GroupBox || control is Panel || control is TabControl || control is SplitContainer)
                {
                    control.Text = Lib.Common.Language.GetText(_this.Name + ".Other." + control.Name, control.Text);
                    SetAllControls(_this, control.Controls);
                }
                else
                {
                    SetFormText(_this, control);
                }

            }
        }
        #endregion

        #region 替换组件

        /// <summary>
        /// 替换组件Text的展示值
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="control"></param>
        private static void SetFormText(dynamic _this, Control control)
        {
            #region 按钮 button
            if (control is Button)
            {
                control.Text = Lib.Common.Language.GetText(_this.Name + ".Button." + control.Name, control.Text);
            }
            else
            #endregion

            #region 标签 label
            if (control is Label)
            {
                control.Text = Lib.Common.Language.GetText(_this.Name + ".Label." + control.Name, control.Text);
            }
            else
            #endregion

            #region 表格 dataGridView
            if (control is DataGridView)
            {
                DataGridView table = (DataGridView)control;
                foreach (var column in table.Columns)
                {
                    if (column is DataGridViewTextBoxColumn)
                    {
                        var item = (DataGridViewTextBoxColumn)column;
                        item.HeaderText = Lib.Common.Language.GetText(_this.Name + ".Table." + table.Name + "." + item.Name, item.HeaderText);

                    }
                }
                table.Refresh();
            }
            else
            #endregion

            #region 菜单 Menu
            if (control is MenuStrip)
            {
                MenuStrip menu = (MenuStrip)control;
                foreach (ToolStripMenuItem item in menu.Items)
                {
                    item.Text = Lib.Common.Language.GetText(_this.Name + ".Menu." + item.Name, item.Text);
                    DropDownMenus(_this, item);
                }
            }
            else
            #endregion

            #region 其他 Other
            {
                control.Text = Lib.Common.Language.GetText(_this.Name + ".Other." + control.Name, control.Text);
            }
            #endregion
        }
        /// <summary>
        /// 菜单递归
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="DropDownItems"></param>
        private static void DropDownMenus(dynamic _this, ToolStripDropDownItem DropDownItems)
        {
            foreach (ToolStripDropDownItem dropDownItem in DropDownItems.DropDownItems)
            {
                //如果没有子菜单，就按照form的title获取标题
                dropDownItem.Text = Lib.Common.Language.GetText(dropDownItem.Name.Split('.').LastOrDefault() + ".Title", dropDownItem.Text);

                if (dropDownItem.HasDropDownItems)
                {
                    //如果有子菜单，就在menu设置对应控件Name
                    dropDownItem.Text = Lib.Common.Language.GetText(_this.Name + ".Menu." + dropDownItem.Name, dropDownItem.Text);
                    DropDownMenus(_this, dropDownItem);
                }
            }
        }
        #endregion
    }
}
