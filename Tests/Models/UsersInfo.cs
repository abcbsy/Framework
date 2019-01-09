using System;
using System.Data;
using System.Collections.Generic;
using Common;

namespace UsersCenter.Models
{
	///<summary>
	///类名：UsersInfo
	///公司名称：V-Life
	///作者：曾璐（abcbsy@163.com）
	///创建日期：2018/11/28 15:10:26
	///用途说明：数据表Users的实体类
	///修改记录：
	///</summary>
	[Table("Users")]
	public class UsersInfo
	{
		#region Properties
		private int? m_UserID;
		/// <summary>
		/// 
		/// </summary>
		[Field("ID", FieldDescription="", FieldType=DbType.Int32, IsIdentity=false, IsPrimaryKey=true, Length=19, Scale=0, AllowNull=false, DefaultValue="")]
		public int? ID
        {
			get
			{
				return m_UserID;
			}
			set
			{
				m_UserID = value;
			}
		}
		private string m_UserAccount;
		/// <summary>
		/// 
		/// </summary>
		[Field("UserAccount", FieldDescription="", FieldType=DbType.String, IsIdentity=false, IsPrimaryKey=false, Length=32, Scale=0, AllowNull=false, DefaultValue="")]
		public string UserAccount
		{
			get
			{
				return m_UserAccount;
			}
			set
			{
				m_UserAccount = value;
			}
		}
		private string m_UserName;
		/// <summary>
		/// 
		/// </summary>
		[Field("UserName", FieldDescription="", FieldType=DbType.String, IsIdentity=false, IsPrimaryKey=false, Length=32, Scale=0, AllowNull=true, DefaultValue="")]
		public string UserName
		{
			get
			{
				return m_UserName;
			}
			set
			{
				m_UserName = value;
			}
		}
		private string m_Password;
		/// <summary>
		/// 
		/// </summary>
		[Field("Password", FieldDescription="", FieldType=DbType.String, IsIdentity=false, IsPrimaryKey=false, Length=128, Scale=0, AllowNull=false, DefaultValue="")]
		public string Password
		{
			get
			{
				return m_Password;
			}
			set
			{
				m_Password = value;
			}
		}
		private int? m_UserType;
		/// <summary>
		/// 
		/// </summary>
		[Field("UserType", FieldDescription="", FieldType=DbType.Int32, IsIdentity=false, IsPrimaryKey=false, Length=10, Scale=0, AllowNull=false, DefaultValue="")]
		public int? UserType
		{
			get
			{
				return m_UserType;
			}
			set
			{
				m_UserType = value;
			}
		}
		private string m_Mobile;
		/// <summary>
		/// 
		/// </summary>
		[Field("Mobile", FieldDescription="", FieldType=DbType.String, IsIdentity=false, IsPrimaryKey=false, Length=32, Scale=0, AllowNull=true, DefaultValue="")]
		public string Mobile
		{
			get
			{
				return m_Mobile;
			}
			set
			{
				m_Mobile = value;
			}
		}
		private string m_Email;
		/// <summary>
		/// 
		/// </summary>
		[Field("Email", FieldDescription="", FieldType=DbType.String, IsIdentity=false, IsPrimaryKey=false, Length=128, Scale=0, AllowNull=true, DefaultValue="")]
		public string Email
		{
			get
			{
				return m_Email;
			}
			set
			{
				m_Email = value;
			}
		}
		private string m_PhotoUrl;
		/// <summary>
		/// 
		/// </summary>
		[Field("PhotoUrl", FieldDescription="", FieldType=DbType.String, IsIdentity=false, IsPrimaryKey=false, Length=128, Scale=0, AllowNull=true, DefaultValue="")]
		public string PhotoUrl
		{
			get
			{
				return m_PhotoUrl;
			}
			set
			{
				m_PhotoUrl = value;
			}
		}
		private string m_CreateTime;
		/// <summary>
		/// 
		/// </summary>
		[Field("CreateTime", FieldDescription="", FieldType=DbType.String, IsIdentity=false, IsPrimaryKey=false, Length=23, Scale=3, AllowNull=false, DefaultValue="getdate")]
		public string CreateTime
		{
			get
			{
				return m_CreateTime;
			}
			set
			{
				m_CreateTime = value;
			}
		}
		private string m_ModifyTime;
		/// <summary>
		/// 
		/// </summary>
		[Field("ModifyTime", FieldDescription="", FieldType=DbType.String, IsIdentity=false, IsPrimaryKey=false, Length=23, Scale=3, AllowNull=true, DefaultValue="getdate")]
		public string ModifyTime
		{
			get
			{
				return m_ModifyTime;
			}
			set
			{
				m_ModifyTime = value;
			}
		}
		private int? m_IsDeleted;
		/// <summary>
		/// 
		/// </summary>
		[Field("IsDeleted", FieldDescription="", FieldType=DbType.Int32, IsIdentity=false, IsPrimaryKey=false, Length=1, Scale=0, AllowNull=false, DefaultValue="0")]
		public int? IsDeleted
		{
			get
			{
				return m_IsDeleted;
			}
			set
			{
				m_IsDeleted = value;
			}
		}

		#endregion
	}
}