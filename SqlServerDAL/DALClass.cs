using Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace SqlServerDAL
{
    public class DALClass<T> : DbHelper, DALFactory.IDALClass<T>
    {

        #region 构造函数
        public DALClass() { }
        public DALClass(IConfigurationSection setting) : base(setting)
        {

        }
        #endregion

        /// <summary>
        /// 数据表名称
        /// </summary>
        public string TableName
        {
            get
            {
                TableAttribute table = (TableAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute));
                return table.TableName;
            }
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="where">条件实体</param>
        /// <returns>如果存在返回true,否则返回false</returns>
        public bool Exists(T where)
        {
            bool ret = false;
            StringBuilder sbWhere = new StringBuilder("1=1");
            string sql = "SELECT 1 FROM [{0}] WHERE {1}";
            List<SqlParameter> cmdParams = new List<SqlParameter>();
            if (where != null)
            {
                Type whereType = where.GetType();
                PropertyInfo[] whereInfos = whereType.GetProperties();
                foreach (PropertyInfo proInfo in whereInfos)
                {
                    FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                    if (attri != null)
                    {
                        object fieldValue = proInfo.GetValue(where, null);
                        if (fieldValue != null)
                        {
                            sbWhere.AppendFormat(" AND [{0}]=@{0}", attri.FieldName);
                            cmdParams.Add(new SqlParameter("@" + attri.FieldName, fieldValue));
                        }
                    }
                }
            }
            using (IDataReader dr = this.ExecuteReader(string.Format(sql, TableName, sbWhere.ToString()), cmdParams))
            {
                if (dr.Read())
                {
                    ret = true;
                }
                dr.Close();
            }
            return ret;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender">数据实体</param>
        public void ExecuteInsertCommand(T sender)
        {
            Type senderType = sender.GetType();
            PropertyInfo[] proInfos = senderType.GetProperties();
            if (proInfos.Length > 0)
            {
                string sql = "INSERT INTO [{0}] ({1}) VALUES({2})";
                StringBuilder sbFields = new StringBuilder();
                StringBuilder sbParams = new StringBuilder();
                List<SqlParameter> cmdParams = new List<SqlParameter>();
                foreach (PropertyInfo proInfo in proInfos)
                {
                    FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                    if (attri != null)
                    {
                        object fieldValue = proInfo.GetValue(sender, null);
                        if (!attri.IsIdentity && fieldValue != null)
                        {
                            sbFields.AppendFormat("[{0}],", attri.FieldName);
                            sbParams.AppendFormat("@{0},", attri.FieldName);
                            cmdParams.Add(new SqlParameter("@" + attri.FieldName, fieldValue));
                        }
                    }
                }
                this.ExecuteSql(string.Format(sql, TableName, sbFields.ToString().Trim(',', ' '), sbParams.ToString().Trim(',', ' ')), cmdParams);
            }
        }

        /// <summary>
        /// 新增并且获取Identity值
        /// </summary>
        /// <param name="sender">数据实体</param>
        /// <returns>如果存在Identity列返回Identity值,否则返回-1</returns>
        public int ExecuteInsertGetIdentity(T sender)
        {
            int ret = -1;
            Type senderType = sender.GetType();
            PropertyInfo[] proInfos = senderType.GetProperties();
            if (proInfos.Length > 0)
            {
                string sql = "INSERT INTO [{0}] ({1}) VALUES({2}) " + System.Environment.NewLine + "SELECT SCOPE_IDENTITY()";
                StringBuilder sbFields = new StringBuilder();
                StringBuilder sbParams = new StringBuilder();
                List<SqlParameter> cmdParams = new List<SqlParameter>();

                foreach (PropertyInfo proInfo in proInfos)
                {
                    FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                    if (attri != null)
                    {
                        object fieldValue = proInfo.GetValue(sender, null);
                        if (!attri.IsIdentity && fieldValue != null)
                        {
                            sbFields.AppendFormat("[{0}],", attri.FieldName);
                            sbParams.AppendFormat("@{0},", attri.FieldName);
                            cmdParams.Add(new SqlParameter("@" + attri.FieldName, fieldValue));
                        }
                    }
                }
                object objId = this.GetSingle(string.Format(sql, TableName, sbFields.ToString().Trim(',', ' '), sbParams.ToString().Trim(',', ' ')), cmdParams);
                if (!Convert.IsDBNull(objId))
                {
                    ret = Convert.ToInt32(objId);
                }
                
            }
            return ret;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender">数据实体</param>
        /// <param name="where">条件实体</param>
        public void ExecuteUpdateCommand(T sender, T where)
        {
            Type senderType = sender.GetType();
            PropertyInfo[] senderInfos = senderType.GetProperties();
            if (senderInfos.Length > 0)
            {
                StringBuilder sbSet = new StringBuilder();
                StringBuilder sbWhere = new StringBuilder("1=1");
                string sql = "UPDATE [{0}] SET {1} WHERE {2}";
                List<SqlParameter> cmdParams = new List<SqlParameter>();

                foreach (PropertyInfo proInfo in senderInfos)
                {
                    FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                    if (attri != null)
                    {
                        object fieldValue = proInfo.GetValue(sender, null);
                        if (!attri.IsIdentity && fieldValue != null)
                        {
                            sbSet.AppendFormat("[{0}]=@{0},", attri.FieldName);
                            cmdParams.Add(new SqlParameter("@" + attri.FieldName, fieldValue));
                        }
                    }
                }
                if (sbSet.Length > 0)
                {
                    if (where != null)
                    {
                        Type whereType = where.GetType();
                        PropertyInfo[] whereInfos = whereType.GetProperties();
                        foreach (PropertyInfo proInfo in whereInfos)
                        {
                            FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                            if (attri != null)
                            {
                                object fieldValue = proInfo.GetValue(where, null);
                                if (fieldValue != null)
                                {
                                    sbWhere.AppendFormat(" AND [{0}]=@W{0}", attri.FieldName);
                                    cmdParams.Add(new SqlParameter("@W" + attri.FieldName, fieldValue));
                                }
                            }
                        }
                    }
                    this.ExecuteSql(string.Format(sql, TableName, sbSet.ToString().Trim(',', ' '), sbWhere.ToString()), cmdParams);
                }
            }
        }

        /// <summary>
        /// 修改，必须有唯一整型字段“ID”
        /// </summary>
        /// <param name="sender">数据实体</param>
        /// <param name="id">ID</param>
        public void ExecuteUpdateCommand(T sender, int id)
        {
            Type senderType = sender.GetType();
            PropertyInfo[] senderInfos = senderType.GetProperties();
            if (senderInfos.Length > 0)
            {
                StringBuilder sbSet = new StringBuilder();
                string sql = "UPDATE [{0}] SET {1} WHERE ID = {2}";
                List<SqlParameter> cmdParams = new List<SqlParameter>();

                foreach (PropertyInfo proInfo in senderInfos)
                {
                    FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                    if (attri != null)
                    {
                        object fieldValue = proInfo.GetValue(sender, null);
                        if (!attri.IsIdentity && fieldValue != null)
                        {
                            sbSet.AppendFormat("[{0}]=@{0},", attri.FieldName);
                            cmdParams.Add(new SqlParameter("@" + attri.FieldName, fieldValue));
                        }
                    }
                }
                if (sbSet.Length > 0)
                {
                    this.ExecuteSql(string.Format(sql, TableName, sbSet.ToString().Trim(',', ' '), id), cmdParams);
                }
            }
        }

        /// <summary>
        /// 修改并且获取修改数据行数
        /// </summary>
        /// <param name="sender">数据实体</param>
        /// <param name="where">条件实体</param>
        /// <returns>返回影响行数</returns>
        public int ExecuteUpdateGetEffect(T sender, T where)
        {
            int ret = -1;
            Type senderType = sender.GetType();
            PropertyInfo[] senderInfos = senderType.GetProperties();
            if (senderInfos.Length > 0)
            {
                StringBuilder sbSet = new StringBuilder();
                StringBuilder sbWhere = new StringBuilder("1=1");
                string sql = "UPDATE [{0}] SET {1} WHERE {2} " + System.Environment.NewLine + "SELECT @@ROWCOUNT";
                List<SqlParameter> cmdParams = new List<SqlParameter>();

                foreach (PropertyInfo proInfo in senderInfos)
                {
                    FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                    if (attri != null)
                    {
                        object fieldValue = proInfo.GetValue(sender, null);
                        if (!attri.IsIdentity && fieldValue != null)
                        {
                            sbSet.AppendFormat("[{0}]=@{0},", attri.FieldName);
                            cmdParams.Add(new SqlParameter("@" + attri.FieldName, fieldValue));
                        }
                    }
                }
                if (sbSet.Length > 0)
                {
                    if (where != null)
                    {
                        Type whereType = where.GetType();
                        PropertyInfo[] whereInfos = whereType.GetProperties();
                        foreach (PropertyInfo proInfo in whereInfos)
                        {
                            FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                            if (attri != null)
                            {
                                object fieldValue = proInfo.GetValue(where, null);
                                if (fieldValue != null)
                                {
                                    sbWhere.AppendFormat(" AND [{0}]=@W{0}", attri.FieldName);
                                    cmdParams.Add(new SqlParameter("@W" + attri.FieldName, fieldValue));
                                }
                            }
                        }
                    }
                    object objId = this.GetSingle(string.Format(sql, TableName, sbSet.ToString().Trim(',', ' '), sbWhere.ToString()), cmdParams);
                    if (!Convert.IsDBNull(objId))
                    {
                        ret = Convert.ToInt32(objId);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">条件实体</param>
        public void ExecuteDeleteCommand(T where)
        {
            StringBuilder sbWhere = new StringBuilder("1=1");
            string sql = "DELETE FROM [{0}] WHERE {1}";
            List<SqlParameter> cmdParams = new List<SqlParameter>();
            if (where != null)
            {
                Type whereType = where.GetType();
                PropertyInfo[] whereInfos = whereType.GetProperties();
                foreach (PropertyInfo proInfo in whereInfos)
                {
                    FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                    if (attri != null)
                    {
                        object fieldValue = proInfo.GetValue(where, null);
                        if (fieldValue != null)
                        {
                            sbWhere.AppendFormat(" AND [{0}]=@{0}", attri.FieldName);
                            cmdParams.Add(new SqlParameter("@" + attri.FieldName, fieldValue));
                        }
                    }
                }
            }
            this.ExecuteSql(string.Format(sql, TableName, sbWhere.ToString()), cmdParams);
        }

        /// <summary>
        /// 删除，必须有唯一整型字段“ID”
        /// </summary>
        /// <param name="id">ID</param>
        public void ExecuteDeleteCommand(int id)
        {
            StringBuilder sbWhere = new StringBuilder("1=1");
            string sql = "DELETE FROM [{0}] WHERE ID = {1}";
            this.ExecuteSql(string.Format(sql, TableName, id));
        }

        /// <summary>
        /// 删除并且获取修改数据行数
        /// </summary>
        /// <param name="where">条件实体</param>
        /// <returns>返回影响行数</returns>
        public int ExecuteDeleteGetEffect(T where)
        {
            int ret = -1;
            StringBuilder sbWhere = new StringBuilder("1=1");
            string sql = "DELETE FROM [{0}] WHERE {1} " + System.Environment.NewLine + "SELECT @@ROWCOUNT";
            List<SqlParameter> cmdParams = new List<SqlParameter>();
            if (where != null)
            {
                Type whereType = where.GetType();
                PropertyInfo[] whereInfos = whereType.GetProperties();
                foreach (PropertyInfo proInfo in whereInfos)
                {
                    FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                    if (attri != null)
                    {
                        object fieldValue = proInfo.GetValue(where, null);
                        if (fieldValue != null)
                        {
                            sbWhere.AppendFormat(" AND [{0}]=@{0}", attri.FieldName);
                            cmdParams.Add(new SqlParameter("@" + attri.FieldName, fieldValue));
                        }
                    }
                }
            }
            object objId = this.GetSingle(string.Format(sql, TableName, sbWhere.ToString()), cmdParams);
            if (!Convert.IsDBNull(objId))
            {
                ret = Convert.ToInt32(objId);
            }
            return ret;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="where">条件实体</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(T where)
        {
            IDataReader dr = null;
            StringBuilder sbWhere = new StringBuilder("1=1");
            string sql = "SELECT * FROM [{0}] WHERE {1}";
            List<SqlParameter> cmdParams = new List<SqlParameter>();
            if (where != null)
            {
                Type whereType = where.GetType();
                PropertyInfo[] whereInfos = whereType.GetProperties();
                foreach (PropertyInfo proInfo in whereInfos)
                {
                    FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                    if (attri != null)
                    {
                        object fieldValue = proInfo.GetValue(where, null);
                        if (fieldValue != null)
                        {
                            sbWhere.AppendFormat(" AND [{0}]=@{0}", attri.FieldName);
                            cmdParams.Add(new SqlParameter("@" + attri.FieldName, fieldValue));
                        }
                    }
                }
            }
            dr = this.ExecuteReader(string.Format(sql, TableName, sbWhere.ToString()), cmdParams);
            return dr;
        }

        /// <summary>
        /// 获取数据，必须有唯一整型字段“ID”
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(int id)
        {
            IDataReader dr = null;
            string sql = "SELECT * FROM [{0}] WHERE ID = {1}";
            dr = this.ExecuteReader(string.Format(sql, TableName, id));
            return dr;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="where">条件实体</param>
        /// <returns></returns>
        public DataTable ExecuteDataSet(T where)
        {
            DataTable ret = null;
            StringBuilder sbWhere = new StringBuilder("1=1");
            string sql = "SELECT * FROM [{0}] WHERE {1}";
            List<SqlParameter> cmdParams = new List<SqlParameter>();
            if (where != null)
            {
                Type whereType = where.GetType();
                PropertyInfo[] whereInfos = whereType.GetProperties();
                foreach (PropertyInfo proInfo in whereInfos)
                {
                    FieldAttribute attri = (FieldAttribute)Attribute.GetCustomAttribute(proInfo, typeof(FieldAttribute));
                    if (attri != null)
                    {
                        object fieldValue = proInfo.GetValue(where, null);
                        if (fieldValue != null)
                        {
                            sbWhere.AppendFormat(" AND [{0}]=@{0}", attri.FieldName);
                            cmdParams.Add(new SqlParameter("@" + attri.FieldName, fieldValue));
                        }
                    }
                }
            }
            DataSet ds = this.Query(string.Format(sql, TableName, sbWhere.ToString()), cmdParams);
            if (ds != null && ds.Tables.Count == 1)
            {
                ret = ds.Tables[0];
            }
            return ret;
        }
    }
}
