using System;
using System.Data;
using System.Collections.Generic;
using IDAL;
using Microsoft.Extensions.Configuration;

namespace SqlServerDAL
{
	///<summary>
	///类名：Users
	///公司名称：V-Life
	///作者：曾璐（abcbsy@163.com）
	///创建日期：2018/11/28 15:10:26
	///用途说明：数据表Users的访问类
	///修改记录：
	///</summary>
    public class Users : DbHelper, IUsers
    {
        #region 构造函数&框架的必要代码
        public Users() { }
        public Users(IConfigurationSection setting) : base(setting)
        {

        }
        #endregion
        public DataTable Search(Model.UsersInfo objWhere, string order, int curPage, int pageSize, out int recordCount, out int pageCount)
        {
            System.Text.StringBuilder sbWhere = new System.Text.StringBuilder("1 = 1");
            //TODO
//            if (!string.IsNullOrEmpty(objWhere.Name) && objWhere.Name.Trim() != "")
//            {
//                sbWhere.AppendFormat(" AND Name LIKE '%{0}%'", DbHelper.SqlAttackTrim(objWhere.Name));
//            }
            return this.GetPage(pageSize, curPage, "*", "Users", sbWhere.ToString(), order, out recordCount, out pageCount); 
        }
    }
}