using CoinAPI.CommonFramework.DbAccess;
using CoinAPI.WebAPI.DataRepository;
using System;
using System.Collections.Generic;
using System.Data;

namespace CoinAPI.WebAPI.DataHelper
{
    /// <summary>
    ///     MySql数据库操作类
    /// </summary>
    public class DataBaseHelper
    {
        #region Members

        /// <summary>
        ///     主数据库
        /// </summary>
        private static Database _masterDB;
        /// <summary>
        ///     存储数据库
        /// </summary>
        private static Database _slaveDB;

        #endregion

        #region Methods

        #region WebApi_BaseInfo Operation

        /// <summary>
        ///     获取Api列表信息
        /// </summary>
        /// <returns>返回列表对象</returns>
        public IList<WebApi_BaseInfo> GetApiList()
        {
            DataSet ds = _slaveDB.SpExecuteDataSet("spGetWebApiList", null, null);
            if (ds != null && ds.Tables.Count > 0) return LoadTableToList(ds.Tables[0]);
            return null;
        }

        /// <summary>
        ///     根据Id获取实体对象
        /// </summary>
        /// <param name="baseInfoId">主键Id</param>
        /// <returns>获取到的实体对象</returns>
        public WebApi_BaseInfo GetWebApi_BaseInfoById(int baseInfoId)
        {
            string[] parameters = new[]
            {
                "BaseInfoId"
            };
            object[] parameterValues = new object[] { baseInfoId };
            DataSet ds = _slaveDB.SpExecuteDataSet("spGetWebApiById", parameters, parameterValues);
            WebApi_BaseInfo model = new WebApi_BaseInfo();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        LoadWebApi_BaseInfoEntity(dr, model);
                    }
                    return model;
                }
                return null;
            }
            return null;
        }

        /// <summary>
        ///     根据GroupId获取实体对象
        /// </summary>
        /// <param name="groupId">主键Id</param>
        /// <returns>获取到的实体对象</returns>
        public IList<WebApi_BaseInfo> GetWebApi_BaseInfoByGroupId(int groupId)
        {
            string[] parameters = new[]
            {
                "GroupId"
            };
            object[] parameterValues = new object[] { groupId };
            DataSet ds = _slaveDB.SpExecuteDataSet("spGetApiListByGroupId", parameters, parameterValues);
            if (ds != null && ds.Tables.Count > 0)
                return LoadTableToList(ds.Tables[0]);
            return null;
        }

        /// <summary>
        ///     更新WebApi_BaseInfo信息
        /// </summary>
        /// <param name="data">实体对象</param>
        /// <returns>更新成功返回true，更新失败返回false</returns>
        public bool UpdateWebApi(WebApi_BaseInfo data)
        {
            if (data == null)
                return false;
            string[] parameters = new[]
            {
                "BaseInfoId",
                "Name",
                "Url",
                "Description",
                "OpenTime",
                "IsAuthorize",
                "HttpType",
                "IsDelete",
                "UpdateTime",
                "RequestHeader",
                "RequestBody",
                "ReturnResult",
                "GroupId"
            };
            Object[] parameterValues = new object[]
            {
                data.BaseInfoId,
                data.Name,
                data.Url,
                data.Description,
                data.OpenTime,
                data.IsAuthorize,
                data.HttpType,
                data.IsDelete,
                data.UpdateTime,
                data.RequestHeader,
                data.RequestBody,
                data.ReturnResult,
                data.GroupId
            };
            if (_masterDB.SpExecuteNonQuery("spUpdateWebApiBaseInfo", parameters, parameterValues) > 0)
                return true;
            return false;
        }

        /// <summary>
        ///     新增WebApi_BaseInfo信息
        /// </summary>
        /// <param name="data">实体对象</param>
        /// <returns>新增成功返回新增Api主键Id，新增失败返回0</returns>
        public int AddWebApi(WebApi_BaseInfo data)
        {
            int id = 0;
            if (data == null)
                return id;
            string[] parameters = new[]
            {
                "Name",
                "Url",
                "Description",
                "OpenTime",
                "IsAuthorize",
                "HttpType",
                "CreateTime",
                "IsDelete",
                "UpdateTime",
                "RequestHeader",
                "RequestBody",
                "ReturnResult",
                "GroupId"
            };
            Object[] parameterValues = new object[]
            {
                data.Name,
                data.Url,
                data.Description,
                data.OpenTime,
                data.IsAuthorize,
                data.HttpType,
                data.CreateTime,
                data.IsDelete,
                data.UpdateTime,
                data.RequestHeader,
                data.RequestBody,
                data.ReturnResult,
                data.GroupId
            };
            DataTable dt = _masterDB.SpExecuteTable("spAddWebApiBaseInfo", parameters, parameterValues);
            if (dt != null && dt.Rows.Count > 0)
                id = Convert.ToInt32(dt.Rows[0]["ApiId"]);
            return id;
        }

        /// <summary>
        ///     将DataTable中的数据装载到List列表
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>返回列表对象</returns>
        private static List<WebApi_BaseInfo> LoadTableToList(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                List<WebApi_BaseInfo> list = new List<WebApi_BaseInfo>();
                //循环遍历数据表中的每一行
                foreach (DataRow dr in dt.Rows)
                {
                    WebApi_BaseInfo model = new WebApi_BaseInfo();
                    LoadWebApi_BaseInfoEntity(dr, model);
                    list.Add(model);
                }
                return list;
            }
            return null;
        }

        /// <summary>
        ///     将DataRow数据装载到实体对象
        /// </summary>
        /// <param name="dr">DataRow对象</param>
        /// <param name="data">实体对象</param>
        private static void LoadWebApi_BaseInfoEntity(DataRow dr, WebApi_BaseInfo data)
        {
            if (dr.Table.Columns.Contains("BaseInfoId"))
            {
                if (!string.IsNullOrEmpty(dr["BaseInfoId"].ToString()))
                {
                    data.BaseInfoId = int.Parse(dr["BaseInfoId"].ToString());
                }
            }
            if (dr.Table.Columns.Contains("GroupId"))
            {
                if (!string.IsNullOrEmpty(dr["GroupId"].ToString()))
                {
                    data.GroupId = int.Parse(dr["GroupId"].ToString());
                }
            }
            if (dr.Table.Columns.Contains("Name"))
            {
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                {
                    data.Name = dr["Name"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("DemoStr"))
            {
                if (!string.IsNullOrEmpty(dr["DemoStr"].ToString()))
                {
                    data.DemoStr = dr["DemoStr"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("Url"))
            {
                if (!string.IsNullOrEmpty(dr["Url"].ToString()))
                {
                    data.Url = dr["Url"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("Description"))
            {
                if (!string.IsNullOrEmpty(dr["Description"].ToString()))
                {
                    data.Description = dr["Description"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("ReturnResult"))
            {
                if (!string.IsNullOrEmpty(dr["ReturnResult"].ToString()))
                {
                    data.ReturnResult = dr["ReturnResult"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("ActionUrl"))
            {
                if (!string.IsNullOrEmpty(dr["ActionUrl"].ToString()))
                {
                    data.ActionUrl = dr["ActionUrl"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("RequestHeader"))
            {
                if (!string.IsNullOrEmpty(dr["RequestHeader"].ToString()))
                {
                    data.RequestHeader = dr["RequestHeader"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("RequestBody"))
            {
                if (!string.IsNullOrEmpty(dr["RequestBody"].ToString()))
                {
                    data.RequestBody = dr["RequestBody"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("OpenTime"))
            {
                if (dr["OpenTime"] != null)
                {
                    data.OpenTime = Convert.ToDateTime(dr["OpenTime"]);
                }
            }
            if (dr.Table.Columns.Contains("UpdateTime"))
            {
                if (dr["UpdateTime"] != null)
                {
                    data.UpdateTime = Convert.ToDateTime(dr["UpdateTime"]);
                }
            }
            if (dr.Table.Columns.Contains("CreateTime"))
            {
                if (dr["CreateTime"] != null)
                {
                    data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                }
            }
            if (dr.Table.Columns.Contains("IsDelete"))
            {
                if (dr["IsDelete"] != null)
                {
                    data.IsDelete = Convert.ToBoolean(dr["IsDelete"]);
                }
            }
            if (dr.Table.Columns.Contains("IsAuthorize"))
            {
                if (dr["IsAuthorize"] != null)
                {
                    data.IsAuthorize = Convert.ToBoolean(dr["IsAuthorize"]);
                }
            }
            if (dr.Table.Columns.Contains("HttpType"))
            {
                if (dr["HttpType"] != null)
                {
                    data.HttpType = Convert.ToByte(dr["HttpType"]);
                }
            }
            if (dr.Table.Columns.Contains("ApiGroupName"))
            {
                if (!string.IsNullOrEmpty(dr["ApiGroupName"].ToString()))
                {
                    data.ApiGroupName = dr["ApiGroupName"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("ApiGroupDesc"))
            {
                if (!string.IsNullOrEmpty(dr["ApiGroupDesc"].ToString()))
                {
                    data.GroupDesc = dr["ApiGroupDesc"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("GroupUrl"))
            {
                if (!string.IsNullOrEmpty(dr["GroupUrl"].ToString()))
                {
                    data.GroupUrl = dr["GroupUrl"].ToString();
                }
            }
        }

        #endregion

        #region WebApi_Group Operation

        /// <summary>
        ///     获取Api业务组信息
        /// </summary>
        /// <returns>返回列表对象</returns>
        public IList<WebApi_Group> GetApiCroupList()
        {
            DataSet ds = _slaveDB.SpExecuteDataSet("spGetWebApiGroup", null, null);
            if (ds != null && ds.Tables.Count > 0)
                return LoadWebApiGroupToList(ds.Tables[0]);
            return null;
        }

        /// <summary>
        ///     将DataTable中的数据装载到List列表
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>返回列表对象</returns>
        private static List<WebApi_Group> LoadWebApiGroupToList(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                List<WebApi_Group> list = new List<WebApi_Group>();
                //循环遍历数据表中的每一行
                foreach (DataRow dr in dt.Rows)
                {
                    WebApi_Group model = new WebApi_Group();
                    LoadWebApi_GroupEntity(dr, model);
                    list.Add(model);
                }
                return list;
            }
            return null;
        }

        /// <summary>
        ///     将DataRow数据装载到实体对象
        /// </summary>
        /// <param name="dr">DataRow对象</param>
        /// <param name="data">实体对象</param>
        private static void LoadWebApi_GroupEntity(DataRow dr, WebApi_Group data)
        {
            if (dr.Table.Columns.Contains("GroupId"))
            {
                if (!string.IsNullOrEmpty(dr["GroupId"].ToString()))
                {
                    data.GroupId = int.Parse(dr["GroupId"].ToString());
                }
            }
            if (dr.Table.Columns.Contains("UpdateTime"))
            {
                if (dr["UpdateTime"] != null)
                {
                    data.UpdateTime = Convert.ToDateTime(dr["UpdateTime"]);
                }
            }
            if (dr.Table.Columns.Contains("CreateTime"))
            {
                if (dr["CreateTime"] != null)
                {
                    data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                }
            }
            if (dr.Table.Columns.Contains("NAME"))
            {
                if (dr["NAME"] != null)
                {
                    data.Name = dr["NAME"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("Description"))
            {
                if (dr["Description"] != null)
                {
                    data.Description = dr["Description"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("ActionUrl"))
            {
                if (dr["ActionUrl"] != null)
                {
                    data.ActionUrl = dr["ActionUrl"].ToString();
                }
            }
        }

        #endregion

        #region WebApi_DataStruct Operation

        /// <summary>
        ///     更具Api组编号获取数据结构列表数据
        /// </summary>
        /// <param name="groupId">Api组编号</param>
        /// <returns>返回获取到的列表对象</returns>
        public IList<WebApi_DataStruct> GetWebApiDataStructByGroupId(int groupId)
        {
            string[] parameters = new[]
            {
                "GroupId"
            };
            object[] parameterValues = new object[] { groupId };
            DataSet ds = _slaveDB.SpExecuteDataSet("spGetWebApiDataStructByGroupId", parameters, parameterValues);
            if (ds != null && ds.Tables.Count > 0)
                return LoadWebApi_DataStructToList(ds.Tables[0]);
            return null;
        }

        /// <summary>
        ///     将DataTable中的数据装载到List列表
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>返回列表对象</returns>
        private static List<WebApi_DataStruct> LoadWebApi_DataStructToList(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                List<WebApi_DataStruct> list = new List<WebApi_DataStruct>();
                //循环遍历数据表中的每一行
                foreach (DataRow dr in dt.Rows)
                {
                    WebApi_DataStruct model = new WebApi_DataStruct();
                    LoadWebApi_DataStructEntity(dr, model);
                    list.Add(model);
                }
                return list;
            }
            return null;
        }

        /// <summary>
        ///     将DataRow数据装载到实体对象
        /// </summary>
        /// <param name="dr">DataRow对象</param>
        /// <param name="data">实体对象</param>
        private static void LoadWebApi_DataStructEntity(DataRow dr, WebApi_DataStruct data)
        {
            if (dr.Table.Columns.Contains("DataStructId"))
            {
                if (!string.IsNullOrEmpty(dr["DataStructId"].ToString()))
                {
                    data.DataStructId = int.Parse(dr["DataStructId"].ToString());
                }
            }
            if (dr.Table.Columns.Contains("GroupId"))
            {
                if (!string.IsNullOrEmpty(dr["GroupId"].ToString()))
                {
                    data.GroupId = int.Parse(dr["GroupId"].ToString());
                }
            }
            if (dr.Table.Columns.Contains("DataStructName"))
            {
                if (!string.IsNullOrEmpty(dr["DataStructName"].ToString()))
                {
                    data.Name = dr["DataStructName"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("ActionUrl"))
            {
                if (!string.IsNullOrEmpty(dr["ActionUrl"].ToString()))
                {
                    data.ActionUrl = dr["ActionUrl"].ToString();
                }
            }

            if (dr.Table.Columns.Contains("Description"))
            {
                if (!string.IsNullOrEmpty(dr["Description"].ToString()))
                {
                    data.Description = dr["Description"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("UpdateTime"))
            {
                if (dr["UpdateTime"] != null)
                {
                    data.UpdateTime = Convert.ToDateTime(dr["UpdateTime"]);
                }
            }
            if (dr.Table.Columns.Contains("CreateTime"))
            {
                if (dr["CreateTime"] != null)
                {
                    data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                }
            }
            if (dr.Table.Columns.Contains("IsDelete"))
            {
                if (dr["IsDelete"] != null)
                {
                    data.IsDelete = Convert.ToBoolean(dr["IsDelete"]);
                }
            }
        }

        #endregion

        #region WebApi_DataStruct_Details Operation

        /// <summary>
        ///     更具数据结构编号获取数据结构属性列表数据
        /// </summary>
        /// <param name="dataStructId">数据结构编号</param>
        /// <returns>返回获取到的列表对象</returns>
        public IList<WebApi_DataStruct_Details> GetWebApiDataStructDetailsByDataStructId(int dataStructId)
        {
            string[] parameters = new[]
            {
                "DataStructId"
            };
            object[] parameterValues = new object[] { dataStructId };
            DataSet ds = _slaveDB.SpExecuteDataSet("spGetWebApi_DataStructByDataStructId", parameters, parameterValues);
            if (ds != null && ds.Tables.Count > 0)
                return LoadWebApi_DataStructDetailsToList(ds.Tables[0]);
            return null;
        }

        /// <summary>
        ///     将DataTable中的数据装载到List列表
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>返回列表对象</returns>
        private static List<WebApi_DataStruct_Details> LoadWebApi_DataStructDetailsToList(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                List<WebApi_DataStruct_Details> list = new List<WebApi_DataStruct_Details>();
                //循环遍历数据表中的每一行
                foreach (DataRow dr in dt.Rows)
                {
                    WebApi_DataStruct_Details model = new WebApi_DataStruct_Details();
                    LoadWebApi_DataStructDetailsEntity(dr, model);
                    list.Add(model);
                }
                return list;
            }
            return null;
        }

        /// <summary>
        ///     将DataRow数据装载到实体对象
        /// </summary>
        /// <param name="dr">DataRow对象</param>
        /// <param name="data">实体对象</param>
        private static void LoadWebApi_DataStructDetailsEntity(DataRow dr, WebApi_DataStruct_Details data)
        {
            if (dr.Table.Columns.Contains("DataStructId"))
            {
                if (!string.IsNullOrEmpty(dr["DataStructId"].ToString()))
                {
                    data.DataStructId = int.Parse(dr["DataStructId"].ToString());
                }
            }
            if (dr.Table.Columns.Contains("GroupId"))
            {
                if (!string.IsNullOrEmpty(dr["GroupId"].ToString()))
                {
                    data.GroupId = int.Parse(dr["GroupId"].ToString());
                }
            }
            if (dr.Table.Columns.Contains("DetailsId"))
            {
                if (!string.IsNullOrEmpty(dr["DetailsId"].ToString()))
                {
                    data.DetailsId = int.Parse(dr["DetailsId"].ToString());
                }
            }
            if (dr.Table.Columns.Contains("DetailsName"))
            {
                if (!string.IsNullOrEmpty(dr["DetailsName"].ToString()))
                {
                    data.DetailsName = dr["DetailsName"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("ParentActionUrl"))
            {
                if (!string.IsNullOrEmpty(dr["ParentActionUrl"].ToString()))
                {
                    data.ParentActionUrl = dr["ParentActionUrl"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("ActionUrl"))
            {
                if (!string.IsNullOrEmpty(dr["ActionUrl"].ToString()))
                {
                    data.ActionUrl = dr["ActionUrl"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("DataStructName"))
            {
                if (!string.IsNullOrEmpty(dr["DataStructName"].ToString()))
                {
                    data.DataStructName = dr["DataStructName"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("Description"))
            {
                if (!string.IsNullOrEmpty(dr["Description"].ToString()))
                {
                    data.Description = dr["Description"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("StructDesc"))
            {
                if (!string.IsNullOrEmpty(dr["StructDesc"].ToString()))
                {
                    data.StructDesc = dr["StructDesc"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("DataType"))
            {
                if (!string.IsNullOrEmpty(dr["DataType"].ToString()))
                {
                    data.DataType = dr["DataType"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("DataDemo"))
            {
                if (!string.IsNullOrEmpty(dr["DataDemo"].ToString()))
                {
                    data.DataDemo = dr["DataDemo"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("UpdateTime"))
            {
                if (dr["UpdateTime"] != null)
                {
                    data.UpdateTime = Convert.ToDateTime(dr["UpdateTime"]);
                }
            }
            if (dr.Table.Columns.Contains("CreateTime"))
            {
                if (dr["CreateTime"] != null)
                {
                    data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                }
            }
            if (dr.Table.Columns.Contains("OpenTime"))
            {
                if (dr["OpenTime"] != null)
                {
                    data.OpenTime = Convert.ToDateTime(dr["OpenTime"]);
                }
            }
            if (dr.Table.Columns.Contains("IsDelete"))
            {
                if (dr["IsDelete"] != null)
                {
                    data.IsDelete = Convert.ToBoolean(dr["IsDelete"]);
                }
            }
        }

        #endregion

        #region WebApi_Parameter Operation

        /// <summary>
        ///     根据Api基础信息编号获取Querystring参数列表数据
        /// </summary>
        /// <param name="baseInfoId">Api基础信息编号</param>
        /// <returns>返回获取到的列表对象</returns>
        public IList<WebApi_Parameter> GetWebApiParameterByBaseInfoId(int baseInfoId)
        {
            string[] parameters = new[]
            {
                "BaseInfoId"
            };
            object[] parameterValues = new object[] { baseInfoId };
            DataSet ds = _slaveDB.SpExecuteDataSet("spGetWebApi_ParameterByBaseInfoId", parameters, parameterValues);
            if (ds != null && ds.Tables.Count > 0)
                return LoadWebApi_ParameterToList(ds.Tables[0]);
            return null;
        }

        /// <summary>
        ///     将DataTable中的数据装载到List列表
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>返回列表对象</returns>
        private static List<WebApi_Parameter> LoadWebApi_ParameterToList(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                List<WebApi_Parameter> list = new List<WebApi_Parameter>();
                //循环遍历数据表中的每一行
                foreach (DataRow dr in dt.Rows)
                {
                    WebApi_Parameter model = new WebApi_Parameter();
                    LoadWebApi_ParameterEntity(dr, model);
                    list.Add(model);
                }
                return list;
            }
            return null;
        }

        /// <summary>
        ///     将DataRow数据装载到实体对象
        /// </summary>
        /// <param name="dr">DataRow对象</param>
        /// <param name="data">实体对象</param>
        private static void LoadWebApi_ParameterEntity(DataRow dr, WebApi_Parameter data)
        {
            if (dr.Table.Columns.Contains("ParameterId"))
            {
                if (!string.IsNullOrEmpty(dr["ParameterId"].ToString()))
                {
                    data.ParameterId = int.Parse(dr["ParameterId"].ToString());
                }
            }
            if (dr.Table.Columns.Contains("BaseInfoId"))
            {
                if (!string.IsNullOrEmpty(dr["BaseInfoId"].ToString()))
                {
                    data.BaseInfoId = int.Parse(dr["BaseInfoId"].ToString());
                }
            }
            if (dr.Table.Columns.Contains("ParameterName"))
            {
                if (!string.IsNullOrEmpty(dr["ParameterName"].ToString()))
                {
                    data.Name = dr["ParameterName"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("Description"))
            {
                if (!string.IsNullOrEmpty(dr["Description"].ToString()))
                {
                    data.Description = dr["Description"].ToString();
                }
            }
            if (dr.Table.Columns.Contains("UpdateTime"))
            {
                if (dr["UpdateTime"] != null)
                {
                    data.UpdateTime = Convert.ToDateTime(dr["UpdateTime"]);
                }
            }
            if (dr.Table.Columns.Contains("CreateTime"))
            {
                if (dr["CreateTime"] != null)
                {
                    data.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                }
            }
            if (dr.Table.Columns.Contains("IsDelete"))
            {
                if (dr["IsDelete"] != null)
                {
                    data.IsDelete = Convert.ToBoolean(dr["IsDelete"]);
                }
            }
        }

        #endregion

        /// <summary>
        ///     静态注入db实例
        /// </summary>
        /// <param name="masterDB">主DB</param>
        /// <param name="slaveDB">从DB</param>
        public static void Inject(Database masterDB, Database slaveDB)
        {
            if (null == masterDB) throw new ArgumentNullException("masterDB");
            _masterDB = masterDB;
            _slaveDB = slaveDB;
        }

        #endregion
    }
}