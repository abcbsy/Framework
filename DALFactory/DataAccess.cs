using System;
using System.Reflection;
using System.Configuration;
using Common;
using Microsoft.Extensions.Configuration;

namespace DALFactory
{
    public sealed class DataAccess
    {
        /// <summary>
        /// 创建对象或从缓存获取
        /// </summary>
        public static object CreateObject(IConfigurationSection setting, string typeName, string assemblyName)
        {
            string key = string.Format("{0}-{1}", setting.Key, typeName);
            object objType = key.FromMemoryCache<object>();//从缓存读取
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(assemblyName).CreateInstance(typeName, false, BindingFlags.Default, null, new object[] { setting }, System.Globalization.CultureInfo.CurrentCulture, null);//反射创建
                    objType.ToMemoryCache(key);// 写入缓存
                }
                catch
                { }
            }
            return objType;
        }
        /// <summary>
        /// 创建数据层接口（DALFactory中定义的）
        /// </summary>
        public static IDALClass<T> CreateDALClass<T>(string DbConnectionName)
        {
            Type type = typeof(T);
            var setting = ConfigurationManager.Configuration.GetSection($"ConnectionStrings:{DbConnectionName}");
            string assemblyName = "SqlServerDAL";
            string typeName = string.Format("{0}.DALClass`1[[{1}]]", assemblyName, type.AssemblyQualifiedName);
            Console.WriteLine("CreateDALClass:" + typeName);
            IDALClass<T> objType = (IDALClass<T>)CreateObject(setting, typeName, assemblyName);
            return objType;
        }
        /// <summary>
        /// 创建数据层扩展接口（开发具体项目时创建的接口）
        /// </summary>
        public static T CreateExtendDALClass<T>(string DbConnectionName)
        {
            Type type = typeof(T);
            var setting = ConfigurationManager.Configuration.GetSection($"ConnectionStrings:{DbConnectionName}");
            string assemblyName = setting["ProviderName"];
            string typeName = string.Format("{0}.{1}", assemblyName, type.Name.Substring(1));
            Console.WriteLine("CreateExtendDALClass:" + typeName);
            object objType = CreateObject(setting, typeName, assemblyName);
            return (T)objType;
        }
    }
}
