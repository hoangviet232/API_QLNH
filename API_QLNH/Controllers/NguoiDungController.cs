using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using API_QLNH.Model;

namespace API_QLNH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public NguoiDungController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public JsonResult DangNhap(NguoiDung NguoiDung)
        {
            string query = "select count(*) from nguoidung where TenDangNhap='" + NguoiDung.TenDangNhap + "' and MatKhau ='" + NguoiDung.MatKhau + "'";
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

        /*    [HttpPost]
            public JsonResult Post(NguoiDung thucDon)
            {
                string query = "INSERT INTO NguoiDung (TenNguoiDung) VALUES (@TenNguoiDung)";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("QLNH");

                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@TenNguoiDung", thucDon.TenNguoiDung);
                        myCommand.ExecuteNonQuery();
                    }
                }

                return new JsonResult("Thêm mới thành công");
            }

            [HttpPut]
            public JsonResult Put(NguoiDung thucDon)
            {
                string query = @"Update NguoiDung set
                TenNguoiDung =  '" + thucDon.TenNguoiDung + "'" +
                "where MaNguoiDung = " + thucDon.MaNguoiDung;
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
            public JsonResult Delete(NguoiDung thucDon)
            {
                string query = @"Delete From NguoiDung " +
                "where MaNguoiDung = " + thucDon.MaNguoiDung;
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
            }*/
    }
}