using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.DL.BaseDL;

namespace WebMovie.Backend.DL.MovieDL
{
    public interface IMovieDL : IBaseDL<Movie>
    {
        int InsertMovie (Movie movie);

        Movie GetMovieById(Guid movieId);

        IEnumerable<Movie> GetAllMovies();

        IEnumerable<Movie> GetAllMovieByType (int typeMovie, int? limit);

        int UpdateMovie(Guid movieId, Movie movie);

        int UpdateReview(Guid movieId);
    }
}
