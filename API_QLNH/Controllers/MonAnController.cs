using API_QLNH.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace API_QLNH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonAnController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public MonAnController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "Select MaMonAn,TenMonAn,ThucDon , NgayTao" + ", AnhMonAn  from MonAn";
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
        public JsonResult Post(MonAn monAn)
        {
            string query = @"Insert into MonAn values
            (
            '" + monAn.TenMonAn + "'" +
            ", '" + monAn.ThucDon + "'" +
            ", '" + monAn.NgayTao + "'" +
            ", '" + monAn.AnhMonAn + "'" +
            ")";
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
            return new JsonResult("Thêm mới thành công");
        }

        [HttpPut]
        public JsonResult Put(MonAn monAn)
        {
            string query = @"UPDATE MonAn
                    SET TenMonAn = @TenMonAn,
                        ThucDon = @ThucDon,
                        NgayTao = @NgayTao,
                        AnhMonAn = @AnhMonAn
                    WHERE MaMonAn = @MaMonAn";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("QLNH");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TenMonAn", monAn.TenMonAn);
                    myCommand.Parameters.AddWithValue("@ThucDon", monAn.ThucDon);
                    myCommand.Parameters.AddWithValue("@NgayTao", monAn.NgayTao);
                    myCommand.Parameters.AddWithValue("@AnhMonAn", monAn.AnhMonAn);
                    myCommand.Parameters.AddWithValue("@MaMonAn", monAn.MaMonAn);

                    myCommand.ExecuteNonQuery();
                }
            }

            return new JsonResult("Cập nhật thành công");
        }

        [HttpDelete]
        public JsonResult Delete(MonAn monAn)
        {
            string query = @"Delete From MonAn " +
            "where MaMonAn = " + monAn.MaMonAn;
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

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(fileName);
            }
            catch (Exception)
            {
                return new JsonResult("com.jnp");
            }
        }

        [Route("GetAllTenThucDon")]
        [HttpGet]
        public JsonResult GetAllTenThucDon()
        {
            string query = "Select TenThucDon from ThucDon";
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
    }
}