using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Reflection;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.Entity.ModelConfiguration.Conventions;
using ALP.Data.Attributes;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace ALP.Data.EF
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.10.10
    /// 描 述：数据访问(SqlServer) 上下文
    /// </summary>
    public class SqlServerDbContext : DbContext, IDbContext
    {
        #region 构造函数
        /// <summary>
        /// 初始化一个 使用指定数据连接名称或连接串 的数据访问上下文类 的新实例
        /// </summary>
        /// <param name="connString"></param>
        public SqlServerDbContext(string connString)
            : base(connString)
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
        #endregion

        #region 重载
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string assembleFileName = Assembly.GetExecutingAssembly().CodeBase.Replace("ALP.Data.EF.DLL", "ALP.Application.Mapping.dll").Replace("file:///", "");
            Assembly asm = Assembly.LoadFile(assembleFileName);
            var typesToRegister = asm.GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            modelBuilder.Conventions.Add(new DecimalPrecisionAttributeConvention());
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        /// <summary>
        /// 用于modelBuilder全局设置自定义精度属性
        /// </summary>
        public class DecimalPrecisionAttributeConvention
            : PrimitivePropertyAttributeConfigurationConvention<DecimalPrecisionAttribute>
        {
            public override void Apply(ConventionPrimitivePropertyConfiguration configuration, DecimalPrecisionAttribute attribute)
            {
                if (attribute.Precision < 1 || attribute.Precision > 38)
                {
                    throw new InvalidOperationException("Precision must be between 1 and 38.");
                }
                if (attribute.Scale > attribute.Precision)
                {
                    throw new InvalidOperationException("Scale must be between 0 and the Precision value.");
                }
                configuration.HasPrecision(attribute.Precision, attribute.Scale);
            }
        }
    }
}
