using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common.Entities;

namespace WebMovie.Backend.BL.MovieBL
{
    public interface IMovieBL : IBasaBL<Movie>
    {
        int InsertMovie(Movie movie);

        Movie GetMovieById(Guid movieId);

        IEnumerable<Movie> GetAllMovies();

        IEnumerable<Movie> GetAllMovieByType(int typeMovie, int? limit);

        int UpdateMovie(Guid movieId, Movie movie);

        int UpdateReview(Guid movieId);
    }
}
