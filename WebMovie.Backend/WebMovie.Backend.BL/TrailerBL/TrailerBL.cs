using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.DL.BaseDL;
using WebMovie.Backend.DL.TrailerDL;

namespace WebMovie.Backend.BL.TrailerBL
{
    public class TrailerBL : BaseBL<Trailer>, ITrailerBL
    {
        #region Field

        private ITrailerDL   _trailerDL;

        #endregion

        #region Constructor

        public TrailerBL(ITrailerDL trailerDL) : base(trailerDL)
        {
            _trailerDL = trailerDL;
        }

        #endregion

        #region Method

        public IEnumerable<Trailer> GetAllTrailerByMovieId(Guid movieId)
        {
            var res = _trailerDL.GetAllTrailerByMovieId(movieId);
            return res;
        }

        #endregion
    }
}
