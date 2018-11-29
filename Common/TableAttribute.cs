using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 数据表特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {
        private string tableName;
        /// <summary>
        /// 数据表名称
        /// </summary>
        public string TableName
        {
            get
            {
                return this.tableName;
            }
            set
            {
                this.tableName = value;
            }
        }
        private TableAttribute()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        public TableAttribute(string tableName)
        {
            this.tableName = tableName;
        }
    }
    /// <summary>
    /// 数据库字段特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FieldAttribute : Attribute
    {
        private string fieldName;
        private string fieldDescription;
        private DbType fieldType = DbType.String;
        private int length;
        private int scale;
        private string defaultValue;
        private bool isPrimaryKey = false;
        //private bool isUnique = false;
        private bool isIdentity = false;
        private bool allowNull = false;

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName
        {
            get
            {
                return this.fieldName;
            }
            set
            {
                this.fieldName = value;
            }
        }
        /// <summary>
        /// 字段描述
        /// </summary>
        public string FieldDescription
        {
            get
            {
                return this.fieldDescription;
            }
            set
            {
                this.fieldDescription = value;
            }
        }
        /// <summary>
        /// 字段数据类型
        /// </summary>
        public DbType FieldType
        {
            get
            {
                return this.fieldType;
            }
            set
            {
                this.fieldType = value;
            }
        }
        /// <summary>
        /// 字段长度
        /// </summary>
        public int Length
        {
            get
            {
                return this.length;
            }
            set
            {
                this.length = value;
            }
        }
        /// <summary>
        /// 小数位数
        /// </summary>
        public int Scale
        {
            get
            {
                return this.scale;
            }
            set
            {
                this.scale = value;
            }
        }
        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsPrimaryKey
        {
            get
            {
                return this.isPrimaryKey;
            }
            set
            {
                this.isPrimaryKey = value;
            }
        }
        /// <summary>
        /// 是否为标识字段
        /// </summary>
        public bool IsIdentity
        {
            get
            {
                return this.isIdentity;
            }
            set
            {
                this.isIdentity = value;
            }
        }
        /// <summary>
        /// 是否可以为空
        /// </summary>
        public bool AllowNull
        {
            get
            {
                return this.allowNull;
            }
            set
            {
                this.allowNull = value;
            }
        }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue
        {
            get
            {
                return this.defaultValue;
            }
            set
            {
                this.defaultValue = value;
            }
        }
        private FieldAttribute()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        public FieldAttribute(string fieldName)
        {
            this.fieldName = fieldName;
        }
    }
}
