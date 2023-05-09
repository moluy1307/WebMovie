using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common.Entities;
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

        public int InsertActor(Actor actor, Guid? parent)
        {
            var res = _actorDL.InsertActor(actor, parent);
            return res;
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

        #endregion
    }
}
