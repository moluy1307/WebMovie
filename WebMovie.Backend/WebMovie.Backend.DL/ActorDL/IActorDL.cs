using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.DL.BaseDL;

namespace WebMovie.Backend.DL.ActorDL
{
    public interface IActorDL : IBaseDL<Actor>
    {
        int InsertActor(Actor actor, Guid? parent);

        Actor GetActorById(Guid actorId);

        IEnumerable<Actor> GetActorByMovieId(Guid movieId);
    }
}
