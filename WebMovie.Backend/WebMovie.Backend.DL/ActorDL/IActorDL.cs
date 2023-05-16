using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL.BaseDL;

namespace WebMovie.Backend.DL.ActorDL
{
    public interface IActorDL : IBaseDL<Actor>
    {
        int InsertActor(Actor actor, Guid? parent);

        int UpdateActor(Guid actorId, Actor? actor);

        Actor GetActorById(Guid actorId);

        IEnumerable<Actor> GetActorByMovieId(Guid movieId);

        PagingResult<Actor> GetActorBySearchingImprove(int pageNumber, int pageSize, string? keyword, int? gender, int? startYear, int? endYear, int? columnSort);
    }
}
