using System;
using System.Data;
using System.Collections.Generic;

namespace IDAL
{
	///<summary>
	///�ӿ�����IUsers
	///��˾���ƣ�V-Life
	///���ߣ���责�abcbsy@163.com��
	///�������ڣ�2018/11/28 15:10:26
	///��;˵�������ݱ�Users�ķ�������
	///�޸ļ�¼��
	///</summary>
	public interface IUsers
	{
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
        DataTable Search(Model.UsersInfo objWhere, string order, int curPage, int pageSize, out int recordCount, out int pageCount);
	}
}