using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL.EpisodeDL;
using WebMovie.Backend.DL.MovieDL;

namespace WebMovie.Backend.BL.EpisodeBL
{
    public class EpisodeBL : BaseBL<Episode>, IEpisodeBL
    {
        #region Field

        private IEpisodeDL _episodeDL;

        #endregion

        #region Constructor

        public EpisodeBL(IEpisodeDL episodeDL) : base(episodeDL)
        {
            _episodeDL = episodeDL;
        }

        #endregion

        #region Method

        public PagingResult<Episode> GetEpisodesByMovieId(Guid movieId, int pageNumber, int pageSize)
        {
            var listRecords = _episodeDL.GetEpisodesByMovieId(movieId, pageNumber, pageSize);
            return listRecords;
        }

        #endregion
    }
}
