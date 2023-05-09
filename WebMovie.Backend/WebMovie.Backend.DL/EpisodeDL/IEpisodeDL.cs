using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL.BaseDL;

namespace WebMovie.Backend.DL.EpisodeDL
{
    public interface IEpisodeDL : IBaseDL<Episode>
    {
        //IEnumerable<Episode> GetEpisodesByMovieId(Guid movieId);
        PagingResult<Episode> GetEpisodesByMovieId(Guid movieId, int pageNumber, int pageSize);
    }
}
