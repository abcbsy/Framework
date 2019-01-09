using System;
using System.Data;
using System.Collections.Generic;
using UsersCenter.Models;

namespace UsersCenter.IDAL
{
    ///<summary>
    ///接口名：IUsersDAL
    ///公司名称：V-Life
    ///作者：曾璐（abcbsy@163.com）
    ///创建日期：2018/12/04 13:49:02
    ///用途说明：数据表Users的访问类借口
    ///修改记录：
    ///</summary>
    public interface IUsersDAL
	{
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
        DataTable Search(UsersInfo objWhere, string order, int curPage, int pageSize, out int recordCount, out int pageCount);
	}
}