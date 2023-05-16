using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;

namespace WebMovie.Backend.BL.ActorBL
{
    public interface IActorBL : IBasaBL<Actor>
    {
        ServiceResult InsertActor(Actor actor, Guid? parent);

        ServiceResult UpdateActor(Guid actorId, Actor? actor);

        Actor GetActorById(Guid actorId);

        IEnumerable<Actor> GetActorByMovieId(Guid movieId);

        PagingResult<Actor> GetActorBySearchingImprove(int pageNumber, int pageSize, string? keyword, int? gender, int? startYear, int? endYear, int? columnSort);
    }
}
