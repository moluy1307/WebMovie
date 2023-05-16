using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL.BaseDL;

namespace WebMovie.Backend.DL.MovieDL
{
    public interface IMovieDL : IBaseDL<Movie>
    {
        int InsertMovie (Movie movie);

        Movie GetMovieById(Guid movieId);

        IEnumerable<Movie> GetAllMovies();

        //IEnumerable<Movie> GetAllMovieByType (int typeMovie, int? limit);

        PagingResult<Movie> GetAllMovieByTypeAndFilter(int pageNumber, int pageSize, Guid? categoryId, int? typeMovie, int columnFilter, 
            string? keyword, Guid? categorySearchImproveId, int? startYear, int? endYear, int? columnSort, int? filterAndSortReview);

        PagingResult<Movie> GetSimilarMovieByFilter(int pageNumber, int pageSize, Guid? movieId);

        int UpdateMovie(Guid movieId, Movie movie);

        int UpdateReview(Guid movieId);

        int UpdateMediumScore(Guid movieId, decimal mediumScore);

        int DeleteMovie(Guid movieId);
    }
}
