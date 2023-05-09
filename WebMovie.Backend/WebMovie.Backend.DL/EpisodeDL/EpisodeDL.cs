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
using WebMovie.Backend.Common.Entities.DTO;

namespace WebMovie.Backend.DL.EpisodeDL
{
    public class EpisodeDL : BaseDL<Episode>, IEpisodeDL
    {
        public PagingResult<Episode> GetEpisodesByMovieId(Guid movieId, int pageNumber, int pageSize)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(Episode).Name, "ByMovieId");

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();
            parameters.Add($"@p_{typeof(Movie).Name}Id", movieId);
            parameters.Add("@p_PageNumber", pageNumber);
            parameters.Add("@p_PageSize", pageSize);

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                var reader = mySqlConnection.QueryMultiple(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);

                int count = reader.Read<int>().FirstOrDefault();
                var listRecordByFilterPaging = reader.Read<Episode>().ToList();
                var result = new PagingResult<Episode>(count, pageNumber, pageSize, listRecordByFilterPaging);

                return result;
            }
        }
    }
}
