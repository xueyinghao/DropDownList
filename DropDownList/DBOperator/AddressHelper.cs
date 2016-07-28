using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using DropDownList.Models;
using System.Data;
using System.Data.SqlClient;

namespace DropDownList.DBOperator
{
    /// <summary>
    /// 这个类将用于数据库的操作的方法单独写出来
    /// </summary>
    public class AddressHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            }
        }
        public List<Province> GetAllProvince()
        {
            List<Province> lstProvince = new List<Province>();
            string sql = @"select * from dbo.Province";

            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand(sql,conn);
            conn.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lstProvince.Add(new Province()
                    {
                        ProvinceID = Convert.ToInt32(reader["ProvinceID"]),
                        ProvinceName = reader["ProvinceName"].ToString()
                    });
            }
            conn.Close();
            reader.Close();
            return lstProvince;
        }

        /// <summary>
        /// 通过ProvinceID来获取市的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<City> GetCityListByProvinceID(int id)
        {
            DataSet ds = new DataSet();
            string sql = @"SELECT CityID,CityName FROM dbo.City WHERE ProvinceID="+id;
            //创建链接对象
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            sda.SelectCommand.CommandType = CommandType.Text;
            conn.Open();
            sda.Fill(ds);
            conn.Close();
            return DatatableToList<City>.ConvertToModel(ds.Tables[0]).ToList<City>();

        }
    }
}