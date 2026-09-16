using Lib.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MESClient.Util
{
    public class ControlLayout
    {
        public Control Control { get; set; }
        public string Move { get; set; } = "";
    }
    public class FormLayout
    {
        public static dynamic self;
        /// <summary>
        /// 获取配置文件信息
        /// </summary>
        /// <returns></returns>
        private static JObject GetJsonFile()
        {
            string currDir = System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\Style\";
            string JsonFile = currDir + @"zhStyle.json";
            switch (Language.GetLanguage())
            {
                //case 1066://越南语
                case "vi-VN"://越南语
                    JsonFile = currDir + @"viStyle.json";
                    break;
                //case 2052://简体中文
                case "zh-CN"://简体中文
                    JsonFile = currDir + @"zhStyle.json";
                    break;
                case "th-TH"://泰语
                    JsonFile = currDir + @"thStyle.json";
                    break;
            }

            string result = "";
            using (StreamReader r = new StreamReader(JsonFile, Encoding.Default))
            {
                result = r.ReadToEnd();
            }
            return JObject.Parse(result);
        }
        /// <summary>
        /// 设置布局
        /// 
        /// </summary>
        /// <param name="_this">可以使form 或者 usercontrol ，如果是usercontrol需要传递key值</param>
        /// <param name="key">JSON中的关键值  参考分页PagerControl用户组件</param>
        public static void SetFormLayout(dynamic _this, string key = "")
        {
            self = _this;
            JObject parmas = GetJsonFile()[string.IsNullOrEmpty(key) ? self.Name : key];
            if (parmas == null)
            {
                return;
            }
            foreach (var item in parmas)
            {
                SetValue(item.Key, CountCalc(item.Value.ToString()));
            }
        }

        #region 计算 保存 控件属性
        /// <summary>
        /// 计算
        /// 默认都是 【控件.参数】 的格式
        /// "-", "+", "*", "/", "%"
        /// </summary>
        /// <param name="countCalc">计算公式或者纯数字都可以</param>
        /// <returns></returns>
        public static int CountCalc(string countCalc)
        {

            //string exp = "txtUserCode.Left - label1.Width - 2";
            string exp = countCalc;
            var expList = exp.Split(new string[] { "-", "+", "*", "/", "%", "(", ")" }, StringSplitOptions.None);

            foreach (var item in expList.Where(x => !string.IsNullOrEmpty(x))?.ToList())
            {
                var key = item.Trim();
                if (isPureNum(key) || IsSpecialHandle(key))
                {
                    continue;
                }
                var strS = key.Split('.');
                string controlStr = strS[0];
                string propertyName = strS[1];
                if (controlStr == self.Name)
                {
                    object o = self.GetType().GetProperty(propertyName).GetValue(self, null);
                    exp = exp.Replace(key, o.ToString());
                }
                else
                {
                    if (self.Controls.Find(controlStr, true) == null)
                    {
                        continue;
                    }
                    Control control = self.Controls.Find(controlStr, true)[0];
                    object o = control.GetType().GetProperty(propertyName).GetValue(control, null);
                    exp = exp.Replace(key, o.ToString());
                }
            }
            return CalcByDataTable(exp);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="needCalc">需要保存的控件</param>
        /// <param name="value">值</param>
        public static void SetValue(string needCalc, int value)
        {
            //string needCalc = "label1.Left";
            var strS = needCalc.Split('.');
            string controlStr = strS[0];
            string propertyName = strS[1];
            if (controlStr == self.Name)
            {
                self.GetType().GetProperty(propertyName).SetValue(self, value);
            }
            else
            {
                if (self.Controls.Find(controlStr, true) == null)
                {
                    return;
                }
                Control control = self.Controls.Find(controlStr, true)[0];
                switch (FormEnumHelper.GetStringDesEnum<SpecialHandle>(propertyName))
                {
                    case SpecialHandle.LabelChangeLine://label换行
                        LabelChangeLine(control, value);
                        break;

                    default://只要不是特殊处理字符，直接赋值
                        control.GetType().GetProperty(propertyName).SetValue(control, value);
                        break;
                }
            }
        }
        #endregion

        #region 校验是否是特殊处理
        public static bool IsSpecialHandle(string key)
        {
            var isTrue = false;
            switch (FormEnumHelper.GetStringDesEnum<SpecialHandle>(key))
            {
                case SpecialHandle.LabelChangeLine:
                    isTrue = true;
                    break;
                default:
                    isTrue = false;
                    break;
            }
            return isTrue;
        }
        #endregion

        #region label换行
        public static void LabelChangeLine(Control control, int addWidth)
        {
            if (!(control is Label))
            {
                return;
            }
            var label = (Label)control;
            int LblNum = label.Text.Length; //Label内容长度
            int RowNum = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(LblNum / 2))); //每行显示的字数
            float FontWidth = label.Width / label.Text.Length; //每个字符的宽度
            int RowHeight = label.Height; //每行的高度
            int ColNum = (LblNum - (LblNum / RowNum) * RowNum) == 0 ? (LblNum / RowNum) : (LblNum / RowNum) + 1; //列数
            label.AutoSize = false; //设置AutoSize
            label.Width = (int)(FontWidth * 10.0) + addWidth; //设置显示宽度
            label.Height = RowHeight * ColNum; //设置显示高度
        }
        #endregion


        #region 对应函数

        /// <summary>
        /// 由DataTable计算公式
        /// </summary>
        /// <param name="expression">表达式</param>
        internal static int CalcByDataTable(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return 0;
            }
            object result = new DataTable().Compute(expression, "");
            return int.Parse(result + "");
        }
        public static bool isPureNum(string str)
        {
            if (str == null)//验证这个字符串是否为空 测试：字符串为空字符串不会发生异常，字符串为null会发生异常
            {
                return false;
            }
            bool isNumeric = System.Text.RegularExpressions.Regex.IsMatch(str, @"^\d+$");
            return isNumeric;
        }
        #endregion
    }
}
