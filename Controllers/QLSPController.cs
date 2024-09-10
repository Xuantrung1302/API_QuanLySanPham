using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectDataAPI.Controllers
{
    public class QLSPController : ApiController
    {
        DBConnect DBConnect = new DBConnect();
        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value)
        //{


        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //
        //test branch
        [HttpGet]
        [Route("api/QLSP/danhsachsanpham")]
        public object Getsanpham(string Id = null)
        {
            object result = new List<object>();
            DataTable dt  = new DataTable();
            if (Id != null)
            {
                SqlParameter[] searchParams = {
                        new SqlParameter("@IdProduct",Id)
                };  
                dt = DBConnect.ExecuteQuery("SP_SELECT_PRODUCT", searchParams);
            }
            else
            {
                dt = DBConnect.ExecuteQuery("SP_SELECT_PRODUCT");
            }

            if (dt.Rows.Count > 0)
                result = dt;
            return result;
        }

        [HttpPost]
        [Route("api/QLSP/capnhatsanpham")]
        public bool Updatesanpham([FromBody] JObject data)
        {
            bool result = false;
            string idproduct = data["IdProduct"].ToString();
            string productName = data["ProductName"].ToString();
            string idProductDetail = data["IdProductDetail"].ToString();
            string productType = data["ProductType"].ToString();
            int quantity = (int)data["Quantity"];
            string productStatus = data["ProductStatus"].ToString();

            SqlParameter[] updateparams = {
                    new SqlParameter("@IdProduct", idproduct),
                    new SqlParameter("@ProductName", productName),
                     new SqlParameter("@IdProductDetail", idProductDetail),
                    new SqlParameter("@ProductType", productType),
                     new SqlParameter("@Quantity", quantity),
                    new SqlParameter("@ProductStatus", productStatus)
            };

            result = DBConnect.ExecuteNonQuery("SP_UPDATE_PRODUCT", updateparams);
            return result;
        }

        [HttpPost]
        [Route("api/QLSP/xoaSanPham")]
        public bool Deletesanpham(string Id)
        {

            bool result = false;    
            if (Id != null)
            {
                SqlParameter[] deleteParams = {
                    new SqlParameter("@IdProduct", Id),
                    };

                 result = DBConnect.ExecuteNonQuery("SP_DELETE_PRODUCT", deleteParams);

            }
            return result;
        }
        [HttpPost]
        [Route("api/QLSP/themsanpham")]
        public bool Addsanpham([FromBody] JObject data)
        {
            bool result = false;
            string idproduct = data["IdProduct"].ToString();
            string productName = data["ProductName"].ToString();
            string idProductDetail = data["IdProductDetail"].ToString();
            string productType = data["ProductType"].ToString();
            int quantity = (int)data["Quantity"];
            string productStatus = data["ProductStatus"].ToString();

            SqlParameter[] insertparams = {
                    new SqlParameter("@IdProduct", idproduct),
                    new SqlParameter("@ProductName", productName),
                     new SqlParameter("@IdProductDetail", idProductDetail),
                    new SqlParameter("@ProductType", productType),
                     new SqlParameter("@Quantity", quantity),
                    new SqlParameter("@ProductStatus", productStatus)
            };

            result = DBConnect.ExecuteNonQuery("SP_INSERT_PRODUCT", insertparams);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}