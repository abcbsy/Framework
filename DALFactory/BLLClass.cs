using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DALFactory
{
    public class BLLClass<T>
    {
        private IDALClass<T> dal;

        public void InitDAL(string DbConnectionName)
        {
            dal = DataAccess.CreateDALClass<T>(DbConnectionName);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="where">条件实体</param>
        /// <returns>如果存在返回true,否则返回false</returns>
        public bool Exists(T where)
        {
            return dal.Exists(where);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender">数据实体</param>
        public void ExecuteInsertCommand(T sender)
        {
            dal.ExecuteInsertCommand(sender);
        }

        /// <summary>
        /// 新增并且获取Identity值
        /// </summary>
        /// <param name="sender">数据实体</param>
        /// <returns>如果存在Identity列返回Identity值,否则返回-1</returns>
        public int ExecuteInsertGetIdentity(T sender)
        {
            return dal.ExecuteInsertGetIdentity(sender);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender">数据实体</param>
        /// <param name="where">条件实体</param>
        public void ExecuteUpdateCommand(T sender, T where)
        {
            dal.ExecuteUpdateCommand(sender, where);
        }

        /// <summary>
        /// 修改，必须有唯一整型字段“ID”
        /// </summary>
        /// <param name="sender">数据实体</param>
        /// <param name="id">ID</param>
        public void ExecuteUpdateCommand(T sender, int id)
        {
            dal.ExecuteUpdateCommand(sender, id);
        }

        /// <summary>
        /// 修改并且获取修改数据行数
        /// </summary>
        /// <param name="sender">数据实体</param>
        /// <param name="where">条件实体</param>
        /// <returns>返回影响行数</returns>
        public int ExecuteUpdateGetEffect(T sender, T where)
        {
            return dal.ExecuteUpdateGetEffect(sender, where);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">条件实体</param>
        public void ExecuteDeleteCommand(T where)
        {
            dal.ExecuteDeleteCommand(where);
        }

        /// <summary>
        /// 删除，必须有唯一整型字段“ID”
        /// </summary>
        /// <param name="id">ID</param>
        public void ExecuteDeleteCommand(int id)
        {
            dal.ExecuteDeleteCommand(id);
        }

        /// <summary>
        /// 删除并且获取修改数据行数
        /// </summary>
        /// <param name="where">条件实体</param>
        /// <returns>返回影响行数</returns>
        public int ExecuteDeleteGetEffect(T where)
        {
            return dal.ExecuteDeleteGetEffect(where);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="where">条件实体</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(T where)
        {
            return dal.ExecuteReader(where);
        }

        /// <summary>
        /// 获取数据，必须有唯一整型字段“ID”
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(int id)
        {
            return dal.ExecuteReader(id);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="where">条件实体</param>
        /// <returns></returns>
        public DataTable ExecuteDataSet(T where)
        {
            return dal.ExecuteDataSet(where);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender">数据实体</param>
        /// <param name="where">条件实体</param>
        public void ExecuteSaveCommand(T sender, T where)
        {
            bool exist = false;
            using (IDataReader dr = this.ExecuteReader(where))
            {
                if (dr.Read())
                {
                    exist = true;
                }
            }
            if (exist)
            {
                this.ExecuteUpdateCommand(sender, where);
            }
            else
            {
                this.ExecuteInsertCommand(sender);
            }
        }

        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="where">条件实体</param>
        /// <returns>存在则返回实体对象,否则返回条件实体</returns>
        public T GetObject(T where)
        {
            using (IDataReader dr = this.ExecuteReader(where))
            {
                if (dr.Read())
                {
                    Type whereType = where.GetType();
                    PropertyInfo[] proInfos = whereType.GetProperties();
                    foreach (PropertyInfo proInfo in proInfos)
                    {
                        FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                        if (attri != null)
                        {
                            string fieldName = attri.FieldName;
                            if (!Convert.IsDBNull(dr[fieldName]))
                            {
                                proInfo.SetValue(where, this.ChangeType(dr[fieldName], proInfo.PropertyType), null);
                            }
                        }
                    }
                }
                else
                {
                    where = default(T);
                }
            }
            return where;
        }

        /// <summary>
        /// 获取实体对象，必须有唯一整型字段“ID”
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>存在则返回实体对象,否则返回条件实体</returns>
        public T GetObject(int id)
        {
            T obj = Activator.CreateInstance<T>();
            using (IDataReader dr = this.ExecuteReader(id))
            {
                if (dr.Read())
                {
                    Type whereType = typeof(T);
                    PropertyInfo[] proInfos = whereType.GetProperties();
                    foreach (PropertyInfo proInfo in proInfos)
                    {
                        FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                        if (attri != null)
                        {
                            string fieldName = attri.FieldName;
                            if (!Convert.IsDBNull(dr[fieldName]))
                            {
                                proInfo.SetValue(obj, this.ChangeType(dr[fieldName], proInfo.PropertyType), null);
                            }
                        }
                    }
                }
                else
                {
                    obj = default(T);
                }
            }
            return obj;
        }

        /// <summary>
        /// 获取实体对象列表
        /// </summary>
        /// <param name="where">条件实体</param>
        /// <returns>返回实体对象列表</returns>
        public List<T> GetObjectList(T where)
        {
            List<T> list = new List<T>();
            using (IDataReader dr = this.ExecuteReader(where))
            {
                Type whereType = where.GetType();

                PropertyInfo[] proInfos = whereType.GetProperties();
                while (dr.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach (PropertyInfo proInfo in proInfos)
                    {
                        FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                        if (attri != null)
                        {
                            string fieldName = attri.FieldName;
                            if (!Convert.IsDBNull(dr[fieldName]))
                            {
                                proInfo.SetValue(obj, this.ChangeType(dr[fieldName], proInfo.PropertyType), null);
                            }
                        }
                    }
                    list.Add(obj);
                }
            }

            return list;
        }

        /// <summary>
        /// 类型转化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public object ChangeType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            if (conversionType.Name == "String")
            {
                return string.Format("{0}", value);
            }
            else
            {
                return Convert.ChangeType(value, conversionType);
            }
        }
    }
}
