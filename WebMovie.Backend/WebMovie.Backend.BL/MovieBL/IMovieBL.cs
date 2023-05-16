using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;

namespace WebMovie.Backend.BL.MovieBL
{
    public interface IMovieBL : IBasaBL<Movie>
    {
        ServiceResult InsertMovie(Movie movie);

        Movie GetMovieById(Guid movieId);

        IEnumerable<Movie> GetAllMovies();

        //IEnumerable<Movie> GetAllMovieByType(int typeMovie, int? limit);

        PagingResult<Movie> GetAllMovieByTypeAndFilter(int pageNumber, int pageSize, Guid? categoryId, int? typeMovie, int columnFilter, string? keyword,
            Guid? categorySearchImproveId, int? startYear, int? endYear, int? columnSort, int? filterAndSortReview);

        PagingResult<Movie> GetSimilarMovieByFilter(int pageNumber, int pageSize, Guid? movieId);

        ServiceResult UpdateMovie(Guid movieId, Movie movie);

        int UpdateReview(Guid movieId);

        int UpdateMediumScore(Guid movieId, decimal mediumScore);

        ServiceResult DeleteMovie(Guid movieId);
    }
}
