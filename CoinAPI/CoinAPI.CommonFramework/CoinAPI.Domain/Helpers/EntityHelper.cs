using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using MobiSage.AdsAPI.Domain.Business;
using MySql.Data.Types;

namespace MobiSage.AdsAPI.Domain.Helpers
{
    public static class EntityHelper
    {
        #region Methods

        public static T BuildModel<T>(DataTable table)
        {
            T item = default(T);
            Type itemType = typeof(T);
            if (table.Rows.Count > 0)
            {
                item = (T)Activator.CreateInstance(itemType, true);
                MappingRowHandler(item, table, itemType, 0);
            }
            return item;
        }

        public static Campaign BuildCampaign(DataTable table)
        {
            //Campaign campaign  = Campaign.Create(table.Rows[0]["CampaignID"], )
            //return campaign;
            return null;
        }

        public static IList<T> BuildModelList<T>(DataTable table)
        {
            IList<T> list = new List<T>();

            T item;
            Type listItemType = typeof(T);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                item = (T)Activator.CreateInstance(listItemType);
                MappingRowHandler(item, table, listItemType, i);
                list.Add(item);
            }
            return list;
        }

        private static void MappingRowHandler(object obj, DataTable table, Type type, int row)
        {
            MySqlDateTime temp;
            for (int col = 0; col < table.Columns.Count; col++)
            {
                string columnName = table.Columns[col].ColumnName;
                PropertyInfo prop = type.GetProperty(columnName) ??
                                    type.GetProperty(
                                        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                                            columnName.ToLower()));
                if (prop == null)
                    continue;
                if (table.Rows[row][col] == DBNull.Value)
                {
                    prop.SetValue(obj, null, null);
                    continue;
                }
                object data = table.Rows[row][col];
                if (!Convert.IsDBNull(data))
                {
                    if (Nullable.GetUnderlyingType(prop.PropertyType) == null)
                        data = Convert.ChangeType(data, prop.PropertyType);
                    else if (data is MySqlDateTime) data = ((MySqlDateTime)data).GetDateTime();
                    prop.SetValue(obj, data, null);
                }
            }
        }

        #endregion
    }
}