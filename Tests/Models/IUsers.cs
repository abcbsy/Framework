using System;
using System.Data;
using System.Collections.Generic;

namespace IDAL
{
	///<summary>
	///接口名：IUsers
	///公司名称：V-Life
	///作者：曾璐（abcbsy@163.com）
	///创建日期：2018/11/28 15:10:26
	///用途说明：数据表Users的访问类借口
	///修改记录：
	///</summary>
	public interface IUsers
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
        DataTable Search(Model.UsersInfo objWhere, string order, int curPage, int pageSize, out int recordCount, out int pageCount);
	}
}