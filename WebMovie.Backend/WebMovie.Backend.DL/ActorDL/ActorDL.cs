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

namespace WebMovie.Backend.DL.ActorDL
{
    public class ActorDL : BaseDL<Actor>, IActorDL
    {
        public int InsertActor(Actor actor, Guid? parent)
        {
            int res = InsertRecord(actor, parent);
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
    }
}
