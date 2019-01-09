using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DALFactory;
using UsersCenter.IDAL;
using UsersCenter.Models;

namespace UsersCenter.Services
{
	///<summary>
	///������Users
	///��˾���ƣ�V-Life
	///���ߣ���责�abcbsy@163.com��
	///�������ڣ�2018/11/28 15:10:26
	///��;˵�������ݱ�Users��ҵ����
	///�޸ļ�¼��
	///</summary>
    public class UsersService: DALFactory.BLLClass<UsersInfo>
    {
        #region ��ܵı�Ҫ����
        private readonly string DbConnectionName = "DefaultConnection";
        private IUsersDAL dal;
        public UsersService()
        {
            base.InitDAL(this.DbConnectionName);
            dal = DataAccess.CreateExtendDALClass<IUsersDAL>(this.DbConnectionName);

        }
        #endregion

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="objWhere"></param>
        /// <param name="order"></param>
        /// <param name="curPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable Search(UsersInfo objWhere, string order, int curPage, int pageSize, out int recordCount, out int pageCount)
        {
            return dal.Search(objWhere, order, curPage, pageSize, out recordCount, out pageCount);
        }
        
    }
}