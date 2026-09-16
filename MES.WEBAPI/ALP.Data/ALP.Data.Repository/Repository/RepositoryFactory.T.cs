using ALP.Util.Extension;

namespace ALP.Data.Repository
{
    /// <summary>
    /// 描 述：定义仓储模型工厂
    /// </summary>
    /// <typeparam name="T">动态实体类型</typeparam>
    public class RepositoryFactory<T> where T : class,new()
    {

        /// <summary>
        /// 定义仓储
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns></returns>
        public IRepository<T> BaseRepository(string connString)
        {
            return new Repository<T>(DbFactory.Base(connString, DatabaseType.SqlServer));
        }
        /// <summary>
        /// 定义仓储（基础库）
        /// </summary>
        /// <returns></returns>
        public IRepository<T> BaseRepository()
        {
            return new Repository<T>(DbFactory.Base());
        }

        /// <summary>
        /// 定义UA仓储（基础库）
        /// </summary>
        /// <returns></returns>
        public IRepository<T> UABaseRepository()
        {
            return new Repository<T>(DbFactory.UABase());
        }


        /// <summary>
        /// 20191221 定义报表仓储（基础库）
        /// </summary>
        /// <returns></returns>
        public IRepository<T> ReportBaseRepository()
        {
            return new Repository<T>(DbFactory.ReportBase());
        }
    }
}
