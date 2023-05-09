using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common.Entities;

namespace WebMovie.Backend.BL.ActorBL
{
    public interface IActorBL : IBasaBL<Actor>
    {
        int InsertActor(Actor actor, Guid? parent);

        Actor GetActorById(Guid actorId);

        IEnumerable<Actor> GetActorByMovieId(Guid movieId);
    }
}
