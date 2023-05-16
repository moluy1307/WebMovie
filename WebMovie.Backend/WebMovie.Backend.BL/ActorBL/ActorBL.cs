using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL.ActorDL;

namespace WebMovie.Backend.BL.ActorBL
{
    public class ActorBL : BaseBL<Actor>, IActorBL
    {
        #region Field

        private IActorDL _actorDL;

        #endregion

        #region Constructor

        public ActorBL(IActorDL actorDL) : base(actorDL)
        {
            _actorDL = actorDL;
        }

        #endregion

        #region Method

        public ServiceResult InsertActor(Actor actor, Guid? parent)
        {
            //Validate
            var validateResults = ValidateRequest(actor, parent);

            //Thất bại -> Return lỗi
            if (validateResults.Count > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.InvalidData,
                    Message = ResourceVI.Invalid_InputData,
                    Data = validateResults
                };
            }
            //var res = _actorDL.InsertActor(actor, parent);
            //return res;

            //Thành công -> Gọi vào DL để chạy stored
            var numberOfAffectedRows = _actorDL.InsertActor(actor, parent);

            //XỬ lý kết quả trả về
            if (numberOfAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.InsertFailed,
                    Message = ResourceVI.InsertFaild
                };
            }
        }

        public ServiceResult UpdateActor(Guid actorId, Actor? actor)
        {
            //Validate
            var validateResults = ValidateRequest(actorId, actor);

            //Thất bại -> Return lỗi
            if (validateResults.Count > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.InvalidData,
                    Message = ResourceVI.Invalid_InputData,
                    Data = validateResults
                };
            }

            //Thành công -> Gọi vào DL để chạy stored
            var numberOfAffectedRows = _actorDL.UpdateActor(actorId, actor);

            //XỬ lý kết quả trả về
            if (numberOfAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.InsertFailed,
                    Message = ResourceVI.UpdateFailed
                };
            }
        }

        public Actor GetActorById(Guid actorId)
        {
            var res = _actorDL.GetActorById(actorId);
            return res;
        }

        public IEnumerable<Actor> GetActorByMovieId(Guid movieId)
        {
            var res = _actorDL.GetActorByMovieId(movieId);
            return res;
        }

        public PagingResult<Actor> GetActorBySearchingImprove(int pageNumber, int pageSize, string? keyword, int? gender, int? startYear, int? endYear, int? columnSort)
        {
            var res = _actorDL.GetActorBySearchingImprove(pageNumber, pageSize, keyword, gender, startYear, endYear, columnSort);
            return res;
        }
        #endregion
    }
}
