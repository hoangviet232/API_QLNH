using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using API_QLNH.Model;

namespace API_QLNH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThucDonController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ThucDonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "Select MaThucDon, TenThucDon from ThucDon";
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("QLNH");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(ThucDon thucDon)
        {
            string query = "INSERT INTO ThucDon (TenThucDon) VALUES (@TenThucDon)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QLNH");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TenThucDon", thucDon.TenThucDon);
                    myCommand.ExecuteNonQuery();
                }
            }

            return new JsonResult("Thêm mới thành công");
        }

        [HttpPut]
        public JsonResult Put(ThucDon thucDon)
        {
            string query = @"Update ThucDon set
            TenThucDon =  '" + thucDon.TenThucDon + "'" +
            "where MaThucDon = " + thucDon.MaThucDon;
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("QLNH");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Cập nhật thành công");
        }

        [HttpDelete]
        public JsonResult Delete(ThucDon thucDon)
        {
            string query = @"Delete From ThucDon " +
            "where MaThucDon = " + thucDon.MaThucDon;
            DataTable table = new DataTable();
            String sqlDataSource = _configuration.GetConnectionString("QLNH");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Xoá thành công");
        }
    }
}