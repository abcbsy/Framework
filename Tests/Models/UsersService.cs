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
	///类名：Users
	///公司名称：V-Life
	///作者：曾璐（abcbsy@163.com）
	///创建日期：2018/11/28 15:10:26
	///用途说明：数据表Users的业务类
	///修改记录：
	///</summary>
    public class UsersService: DALFactory.BLLClass<UsersInfo>
    {
        #region 框架的必要代码
        private readonly string DbConnectionName = "DefaultConnection";
        private IUsersDAL dal;
        public UsersService()
        {
            base.InitDAL(this.DbConnectionName);
            dal = DataAccess.CreateExtendDALClass<IUsersDAL>(this.DbConnectionName);

        }
        #endregion

        /// <summary>
        /// 搜索
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