using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.DL.BaseDL;

namespace WebMovie.Backend.DL.TrailerDL
{
    public interface ITrailerDL : IBaseDL<Trailer>
    {
        IEnumerable<Trailer> GetAllTrailerByMovieId(Guid movieId);
    }
}
