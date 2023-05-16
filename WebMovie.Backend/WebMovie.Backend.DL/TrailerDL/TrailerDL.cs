using Dapper;
using Misa.Amis.DL.BaseDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.Common.Constants;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.DL.BaseDL;

namespace WebMovie.Backend.DL.TrailerDL
{
    public class TrailerDL : BaseDL<Trailer>, ITrailerDL
    {
        public IEnumerable<Trailer> GetAllTrailerByMovieId(Guid movieId)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(Trailer).Name, "AllByMovieId");

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();
            parameters.Add($"@p_{typeof(Movie).Name}Id", movieId);

            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                var recordById = mySqlConnection.Query<Trailer>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);

                return recordById;
            }
        }
    }
}
