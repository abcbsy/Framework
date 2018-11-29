using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DALFactory
{
    public interface IDALClass<T>
    {
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="where">条件实体</param>
        /// <returns>如果存在返回true,否则返回false</returns>
        bool Exists(T where);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender">数据实体</param>
        void ExecuteInsertCommand(T sender);
        /// <summary>
        /// 新增并且获取Identity值
        /// </summary>
        /// <param name="sender">数据实体</param>
        /// <returns>如果存在Identity列返回Identity值,否则返回-1</returns>
        int ExecuteInsertGetIdentity(T sender);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender">数据实体</param>
        /// <param name="where">条件实体</param>
        void ExecuteUpdateCommand(T sender, T where);
        /// <summary>
        /// 修改，必须有唯一整型字段“ID”
        /// </summary>
        /// <param name="sender">数据实体</param>
        /// <param name="id">ID</param>
        void ExecuteUpdateCommand(T sender, int id);
        /// <summary>
        /// 修改并且获取修改数据行数
        /// </summary>
        /// <param name="sender">数据实体</param>
        /// <param name="where">条件实体</param>
        /// <returns>返回影响行数</returns>
        int ExecuteUpdateGetEffect(T sender, T where);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">条件实体</param>
        void ExecuteDeleteCommand(T where);
        /// <summary>
        /// 删除，必须有唯一整型字段“ID”
        /// </summary>
        /// <param name="id">ID</param>
        void ExecuteDeleteCommand(int id);
        /// <summary>
        /// 删除并且获取修改数据行数
        /// </summary>
        /// <param name="where">条件实体</param>
        /// <returns>返回影响行数</returns>
        int ExecuteDeleteGetEffect(T where);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="where">条件实体</param>
        /// <returns></returns>
        IDataReader ExecuteReader(T where);
        /// <summary>
        /// 获取数据，必须有唯一整型字段“ID”
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        IDataReader ExecuteReader(int id);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="where">条件实体</param>
        /// <returns></returns>
        DataTable ExecuteDataSet(T where);
    }
}
