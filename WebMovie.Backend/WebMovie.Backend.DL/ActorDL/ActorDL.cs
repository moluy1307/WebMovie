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
using WebMovie.Backend.DL.CategoryDL;

namespace WebMovie.Backend.DL.ActorDL
{
    public class ActorDL : BaseDL<Actor>, IActorDL
    {
        public int InsertActor(Actor actor, Guid? parent)
        {
            int res = InsertRecord(actor, parent);
            return res;
        }

        public int UpdateActor(Guid actorId, Actor? actor)
        {
            int res = UpdateRecord(actorId, actor);
            return res;
        }

        public Actor GetActorById(Guid actorId)
        {
            var res = GetRecordById(actorId);
            return res;
        }

        public IEnumerable<Actor> GetActorByMovieId(Guid movieId)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Get, typeof(Actor).Name, "ByMovieId");

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();
            parameters.Add($"@p_{typeof(Movie).Name}Id", movieId);

            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                var numberOfAffectedRows = mySqlConnection.Query<Actor>(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);

                return numberOfAffectedRows;
            }
        }

        public PagingResult<Actor> GetActorBySearchingImprove(int pageNumber, int pageSize, string? keyword, int? gender, int? startYear, int? endYear, int? columnSort)
        {
            //Chuẩn bị tên stored procedure
            string storedProceduredName = String.Format(ProcedureName.Pagination, typeof(Actor).Name);

            //Chuẩn bị tham số đầu vào cho stored
            var parameters = new DynamicParameters();
            parameters.Add("@p_PageNumber", pageNumber);
            parameters.Add("@p_PageSize", pageSize);
            parameters.Add("p_TextSearch", keyword);
            parameters.Add("p_Gender", gender);
            parameters.Add("p_StartYear", startYear);
            parameters.Add("p_EndYear", endYear);
            parameters.Add("p_ColumnSort", columnSort);

            //Khởi tạo kết nối đến DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Gọi vào DB
                var reader = mySqlConnection.QueryMultiple(storedProceduredName, parameters, commandType: CommandType.StoredProcedure);
                //var multi = mySqlConnection.QueryMultiple(storedProceduredTotalRecords, commandType: CommandType.StoredProcedure);
                int count = reader.Read<int>().FirstOrDefault();
                var listRecordByFilterPaging = reader.Read<Actor>().ToList();
                var numberOfAffectedRows = new PagingResult<Actor>(count, pageNumber, pageSize, listRecordByFilterPaging);

                return numberOfAffectedRows;
            }
        }
    }
}
