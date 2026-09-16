namespace ALP.Util.Entitys
{
    /// <summary>
    /// 数据返回结果
    /// </summary>
    public class DataResultEntity
    {
        /// <summary>
        /// 操作是否成功，true为成功，false为失败
        /// </summary>
        public bool IsSuccess { get; set; } = true;
        /// <summary>
        /// 错误提示信息
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 请求返回结果
        /// </summary>
        public virtual object Result { get; set; }
        /// <summary>
        /// 请求返回辅助结果
        /// </summary>
        public virtual object OtherResult { get; set; }
    }
}
