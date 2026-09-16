using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using MESClient.WorkOrderExcute;
using MESClient.TransferCard;
using MESClient.PackingBG;
using MESClient.BaseData;
using MESClient.SystemManage;
using Lib.Model;
using MESClient.ProduceManage;
using System.Reflection;
using Lib.Model.Role;
using System.Linq;

namespace MESClient
{
    public partial class Frm_Main : Form
    {
        //定义一个窗体客户区的私有变量
        System.Windows.Forms.MdiClient mdiClient;
        public Frm_Main()
        {
            InitializeComponent();
            //获取当前主窗体的子控件数量
            int iCnt = this.Controls.Count;
            for (int i = 0; i < this.Controls.Count; i++)
            {//遍历控件列表，找到MdiClient，也就是MDI客户区
                if (this.Controls[i].GetType().ToString() ==
                    "System.Windows.Forms.MdiClient")
                {//获取窗体客户区实例
                    this.mdiClient = (System.Windows.Forms.MdiClient)this.Controls[i];
                    break;
                }
            }
            ////加载图片
            //string fbImage = Application.StartupPath + "\\image\\background.bmp";
            //if (File.Exists(fbImage))
            //{//如果图片文件存在，则设为MDI客户区背景
            //    Bitmap bm = new Bitmap(fbImage);
            //    this.mdiClient.BackgroundImage = bm;
            //}
        }

        //查询子窗体是否存在
        public bool checkchildfrm(string childfrmname)
        {//遍历MDI子窗体
            foreach (Form childFrm in this.MdiChildren)
            {//查找与指定名称相匹配的子窗体
                if (childFrm.Name == childfrmname)
                {//如果存在则显示子窗体
                    if (childFrm.WindowState == FormWindowState.Minimized)
                        childFrm.WindowState = FormWindowState.Normal;
                    //给子窗体焦点
                    childFrm.Activate();
                    return true;//返回成功找到布尔值
                }
            }
            return false;//返回没有成功找到布尔值
        }
        private void Frm_Main_Load(object sender, EventArgs e)
        {

            tsl_Show.Text = Lib.Common.Language.GetTextWithParams("Common.LoginLeftMsg", "", CurrentUser.UserName, DateTime.Now.ToString());

            //调用角色权限
            this.LoadRole();

            Lib.Resources.FormLanguage.SetFormLanguage(this);
        }

        #region 角色权限
        /// <summary>
        /// 用户权限集合
        /// 暂时用这个结构，后续增加权限功能后，在更改结构
        /// 权限可以考虑用命名空间+form名称，例如 MESClient.Frm_Main
        /// </summary>
        List<RolePermissionEntity> roleList = new List<RolePermissionEntity>();

        Dictionary<string, List<string>> allButtonPermission = new Dictionary<string, List<string>>();
        RoleEntity Role = new RoleEntity();
        private void LoadRole()
        {
            //每次使用的时候重置一下
            Role = new RoleEntity();
            //清除菜单
            this.menuStrip1.Items.Clear();

            //foreach (ToolStripMenuItem item in this.menuStrip1.Items)
            //{
            //    item.DropDownItems.Clear();
            //}

            #region 调用接口获取权限

            #endregion

            this.roleList = Role.PermissionList;

            #region 角色测试 功能  正式启用的时候删除
            Role.RoleName = "admin";
            this.roleList = new List<RolePermissionEntity>() {
                    new RolePermissionEntity {
                        Name="tsm_SystemManage",
                        Text="系统管理",
                        Sort=30,
                        Children=new List<RolePermissionEntity> (){ new RolePermissionEntity {
                                Name="MESClient.SystemManage.Frm_UpdatePassword",
                                Text="修改密码",
                                Children=null,
                                ButtonList=new List<string> (){ "btnSave" }
                            },new RolePermissionEntity {
                                Name="MESClient.SystemManage.Frm_Version",
                                Text="关于MES客户端",
                                Children=null,
                                //ButtonList=new List<string> (){ "btnSave" }
                            },new RolePermissionEntity {
                                Name="MESClient.Frm_GenerateJson",
                                Text="生成JSON工具",
                                Children=null,
                                //ButtonList=new List<string> (){ "btnSave" }
                            }
                        }
                    },
                    new RolePermissionEntity {
                        Name="tsm_ProductionManage",
                        Text="生产管理",
                        Sort=20,
                        Children=new List<RolePermissionEntity> (){ new RolePermissionEntity {
                                Name="MESClient.TransferCard.Frm_TransferCard",
                                Text="流转卡打印",
                                Children=null,
                            },new RolePermissionEntity {
                                Name="MESClient.PackingBG.Frm_PackingBG",
                                Text="包装报工打印",
                                Children=null,
                            },new RolePermissionEntity {
                                Name="MESClient.ProduceManage.RawMaterialBatchUp",
                                Text="半成品上机报工",
                                Children=null,
                            },new RolePermissionEntity {
                                Name="MESClient.PackingBG.Frm_PrintLog",
                                Text="打印日志",
                                Children=null,
                            }
                        }
                    },
                    new RolePermissionEntity {
                        Name="tsm_BaseDataManage",
                        Text="基础数据",
                        Sort=10,
                        Children=new List<RolePermissionEntity> (){ new RolePermissionEntity {
                                Name="MESClient.BaseData.Frm_PTeam",
                                Text="生产小组打印",
                                Children=null,
                            },new RolePermissionEntity {
                                Name="MESClient.BaseData.Frm_People",
                                Text="人员打印",
                                Children=null,
                            },new RolePermissionEntity {
                                Name="MESClient.BaseData.Frm_MaterialBatch",
                                Text="物料批次",
                                Children=null,
                            },new RolePermissionEntity {
                                Name="MESClient.BaseData.Frm_Machine",
                                Text="机台打印",
                                Children=null,
                            },new RolePermissionEntity {
                                Name="MESClient.BaseData.Frm_Location",
                                Text="库位打印",
                                Children=null,
                            }
                        }
                    },
            };
            #endregion
            //创建菜单
            if (this.roleList != null && this.roleList.Count > 0)
            {
                //创建父级菜单 虽然内部结构和dropdown子级的菜单结构一样，因为需要创建父级菜单，不去做封装了。
                foreach (var fatherRole in this.roleList.OrderBy(x => x.Sort)?.ToList())
                {
                    ToolStripMenuItem menu = new ToolStripMenuItem();
                    menu.Name = fatherRole.Name;
                    menu.Text = fatherRole.Text;
                    if (fatherRole.Children != null && fatherRole.Children.Count > 0)
                    {
                        this.CreateItemMenu(menu, fatherRole.Children);//创建子级菜单
                    }
                    else
                    {
                        menu.Click += Menu_Click;
                        allButtonPermission.Add(fatherRole.Name, fatherRole.ButtonList);
                    }
                    this.menuStrip1.Items.Add(menu);
                }
            }
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                var meun = sender as ToolStripMenuItem;

                if (this.checkchildfrmNew(meun.Name) == true) return;
                OpenForm(meun.Name);
            }
        }

        /// <summary>
        /// 递归创建子级菜单
        /// </summary>
        /// <param name="fatherMenu"></param>
        /// <param name="items"></param>
        private void CreateItemMenu(ToolStripMenuItem fatherMenu, List<RolePermissionEntity> items)
        {

            foreach (var itemRole in items.OrderBy(x => x.Sort)?.ToList())
            {
                ToolStripMenuItem menu = new ToolStripMenuItem();
                menu.Name = itemRole.Name;
                menu.Text = itemRole.Text;
                fatherMenu.DropDownItems.Add(menu);
                if (itemRole.Children != null && itemRole.Children.Count > 0)
                {
                    this.CreateItemMenu(menu, itemRole.Children);
                }
                else
                {
                    menu.Click += Menu_Click;
                    allButtonPermission.Add(itemRole.Name, itemRole.ButtonList);
                }
            }
        }

        public bool checkchildfrmNew(string name)
        {
            var form = Assembly.GetExecutingAssembly().CreateInstance(name) as Form;//此方式name是命名空间+form名称
            if (form == null)
            {
                return false;
            }
            //遍历MDI子窗体
            foreach (Form childFrm in this.MdiChildren)
            {//查找与指定名称相匹配的子窗体
                if (childFrm.Name == form.Name)
                {//如果存在则显示子窗体
                    if (childFrm.WindowState == FormWindowState.Minimized)
                        childFrm.WindowState = FormWindowState.Normal;
                    //给子窗体焦点
                    childFrm.Activate();
                    return true;//返回成功找到布尔值
                }
            }
            return false;
        }
        /// <summary>
        /// 打开功能窗体 //后续根据权限配置的ID打开路径
        /// </summary>
        /// <param name="name"></param>
        private void OpenForm(string name)
        {
            var form = Assembly.GetExecutingAssembly().CreateInstance(name) as Form;//此方式name是命名空间+form名称
            if (form == null)
            {
                return;
            }
            //form.TopLevel = false;
            //form.MaximizeBox = false;
            //form.MinimizeBox = false;
            //form.FormBorderStyle = FormBorderStyle.FixedDialog;
            //form.WindowState = FormWindowState.Maximized;
            //form.Dock = DockStyle.Fill;
            //form.Icon = this.Icon;
            form.MdiParent = this;

            #region 多国语言

            Lib.Resources.FormLanguage.SetFormLanguage(form);

            #endregion

            #region 样式

            Util.FormLayout.SetFormLayout(form);

            #endregion

            if (Role.RoleName != "admin")//* 由于暂时没有实现【按钮权限功能分配】（已完成winform按钮展示功能），所以暂定不控制按钮权限，后续增加删除
            {
                #region 按钮权限
                var buttons = new List<string>();
                allButtonPermission.TryGetValue(form.GetType().Namespace + "." + form.Name, out buttons);
                ButtonVisible(form.Controls, buttons);
                #endregion
            }

            form.Show();
        }
        /// <summary>
        /// 按钮显示权限 
        /// </summary>
        /// <param name="sonControls"></param>
        /// <param name="buttons"></param>
        private void ButtonVisible(Control.ControlCollection sonControls, List<string> buttons)
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
                    ButtonVisible(control.Controls, buttons);
                }
                else
                {
                    if (control is Button)
                    {
                        var button = control as Button;
                        if (buttons.Contains(button.Name))
                        {
                            button.Visible = true;
                        }
                        else
                        {
                            button.Visible = false;
                        }
                    }
                }

            }
        }
        #endregion

        #region 清空父级中的菜单

        //protected override CreateParams CreateParams //防止界面闪烁
        //{

        //    get

        //    {

        //        CreateParams paras = base.CreateParams;

        //        paras.ExStyle |= 0x02000000;

        //        return paras;

        //    }

        //}
        private void ClearParentChildrenForm()
        {
            foreach (var form in this.MdiChildren)
            {
                form.Close();
                form.Dispose();
            }
        }
        #endregion

        private void Frm_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 派工执行工单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_WorkOrderExcute_Click(object sender, EventArgs e)
        {
            if (this.checkchildfrm("Frm_WorkOrderExcute") == true) return;
            //如果没有打开则创建新的窗口并打开
            Frm_WorkOrderExcute frm = new Frm_WorkOrderExcute();
            frm.MdiParent = this;//设置MdiParent
            Lib.Resources.FormLanguage.SetFormLanguage(frm);
            frm.Show();//显示窗口
        }
        /// <summary>
        /// 流转卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_TransferCard_Click(object sender, EventArgs e)
        {
            if (this.checkchildfrm("Frm_TransferCard") == true) return;
            //如果没有打开则创建新的窗口并打开
            Frm_TransferCard frm = new Frm_TransferCard();
            frm.MdiParent = this;//设置MdiParent
            frm.WindowState = FormWindowState.Maximized;
            Lib.Resources.FormLanguage.SetFormLanguage(frm);
            frm.Show();//显示窗口
        }
        /// <summary>
        /// 包装报工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_WorkReportManage_Click(object sender, EventArgs e)
        {
            if (this.checkchildfrm("Frm_PackingBG") == true) return;
            //如果没有打开则创建新的窗口并打开
            Frm_PackingBG frm = new Frm_PackingBG();
            frm.MdiParent = this;//设置MdiParent
            Lib.Resources.FormLanguage.SetFormLanguage(frm);
            frm.Show();//显示窗口
        }
        /// <summary>
        /// 生产小组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_PTeam_Click(object sender, EventArgs e)
        {
            if (this.checkchildfrm("Frm_PTeam") == true) return;
            //如果没有打开则创建新的窗口并打开
            Frm_PTeam frm = new Frm_PTeam();
            frm.MdiParent = this;//设置MdiParent
            Lib.Resources.FormLanguage.SetFormLanguage(frm);
            frm.Show();//显示窗口
        }
        /// <summary>
        /// 人员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_People_Click(object sender, EventArgs e)
        {
            if (this.checkchildfrm("Frm_People") == true) return;
            //如果没有打开则创建新的窗口并打开
            Frm_People frm = new Frm_People();
            frm.MdiParent = this;//设置MdiParent
            Lib.Resources.FormLanguage.SetFormLanguage(frm);
            frm.Show();//显示窗口
        }
        /// <summary>
        /// 物料批次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_MaterBatch_Click(object sender, EventArgs e)
        {
            if (this.checkchildfrm("Frm_MaterialBatch") == true) return;
            //如果没有打开则创建新的窗口并打开
            Frm_MaterialBatch frm = new Frm_MaterialBatch();
            frm.MdiParent = this;//设置MdiParent
            Lib.Resources.FormLanguage.SetFormLanguage(frm);
            frm.Show();//显示窗口
        }
        /// <summary>
        /// 机台打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_Machine_Click(object sender, EventArgs e)
        {
            if (this.checkchildfrm("Frm_Machine") == true) return;
            //如果没有打开则创建新的窗口并打开
            Frm_Machine frm = new Frm_Machine();
            frm.MdiParent = this;//设置MdiParent
            Lib.Resources.FormLanguage.SetFormLanguage(frm);
            frm.Show();//显示窗口
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_UpdatePassword_Click(object sender, EventArgs e)
        {
            if (this.checkchildfrm("Frm_UpdatePassword") == true) return;
            //如果没有打开则创建新的窗口并打开
            Frm_UpdatePassword frm = new Frm_UpdatePassword();
            frm.MdiParent = this;//设置MdiParent
            Lib.Resources.FormLanguage.SetFormLanguage(frm);
            frm.Show();//显示窗口
        }
        /// <summary>
        /// 库位打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_Location_Click(object sender, EventArgs e)
        {
            if (this.checkchildfrm("Frm_Location") == true) return;
            //如果没有打开则创建新的窗口并打开
            Frm_Location frm = new Frm_Location();
            frm.MdiParent = this;//设置MdiParent
            Lib.Resources.FormLanguage.SetFormLanguage(frm);
            frm.Show();//显示窗口
        }
        /// <summary>
        /// 打印日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_PrintLog_Click(object sender, EventArgs e)
        {
            if (this.checkchildfrm("Frm_PrintLog") == true) return;
            //如果没有打开则创建新的窗口并打开
            Frm_PrintLog frm = new Frm_PrintLog();
            frm.MdiParent = this;//设置MdiParent
            Lib.Resources.FormLanguage.SetFormLanguage(frm);
            frm.Show();//显示窗口
        }
        /// <summary>
        /// 关于MesClient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_Version_Click(object sender, EventArgs e)
        {
            if (this.checkchildfrm("Frm_Version") == true) return;
            //如果没有打开则创建新的窗口并打开
            Frm_Version frm = new Frm_Version();
            frm.MdiParent = this;//设置MdiParent
            Lib.Resources.FormLanguage.SetFormLanguage(frm);
            frm.Show();//显示窗口
        }

        /// <summary>
        /// 半成品上机报工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_rawBatchUp_Click(object sender, EventArgs e)
        {
            if (this.checkchildfrm("RawMaterialBatchUp") == true) return;
            //如果没有打开则创建新的窗口并打开
            RawMaterialBatchUp frm = new RawMaterialBatchUp();
            frm.MdiParent = this;//设置MdiParent
            Lib.Resources.FormLanguage.SetFormLanguage(frm);
            frm.Show();//显示窗口
        }

        private void 生成JSON工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_GenerateJson frm = new Frm_GenerateJson();
            //frm.MdiParent = this;//设置MdiParent
            frm.Show();//显示窗口
        }
    }
}