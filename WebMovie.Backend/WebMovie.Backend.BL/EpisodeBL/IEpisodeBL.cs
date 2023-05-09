using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;

namespace WebMovie.Backend.BL.EpisodeBL
{
    public interface IEpisodeBL : IBasaBL<Episode>
    {
        //IEnumerable<Episode> GetEpisodesByMovieId(Guid movieId);
        PagingResult<Episode> GetEpisodesByMovieId(Guid movieId, int pageNumber, int pageSize);
    }
}
