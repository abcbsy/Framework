using System;
using System.Data;
using System.Collections.Generic;
using IDAL;
using Microsoft.Extensions.Configuration;

namespace SqlServerDAL
{
	///<summary>
	///������Users
	///��˾���ƣ�V-Life
	///���ߣ���责�abcbsy@163.com��
	///�������ڣ�2018/11/28 15:10:26
	///��;˵�������ݱ�Users�ķ�����
	///�޸ļ�¼��
	///</summary>
    public class Users : DbHelper, IUsers
    {
        #region ���캯��&��ܵı�Ҫ����
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