using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL.MovieDL;

namespace WebMovie.Backend.BL.MovieBL
{
    public class MovieBL : BaseBL<Movie>, IMovieBL
    {
        #region Field

        private IMovieDL _movieDL;

        #endregion

        #region Constructor

        public MovieBL(IMovieDL movieDL) : base(movieDL)
        {
            _movieDL = movieDL;
        }

        #endregion

        #region Method

        public IEnumerable<Movie> GetAllMovies()
        {
            var listRecords = _movieDL.GetAllMovies();
            return listRecords;
        }

        public Movie GetMovieById(Guid movieId)
        {
            var recordById = _movieDL.GetMovieById(movieId);
            return recordById;
        }

        public ServiceResult InsertMovie(Movie movie)
        {
            //var record = _movieDL.InsertMovie(movie);
            //return record;

            //Validate
            var validateResults = ValidateRequest(movie, null);

            //Thất bại -> Return lỗi
            if (validateResults.Count > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.InvalidData,
                    Message = ResourceVI.Invalid_InputData,
                    Data = validateResults
                };
            }

            //Thành công -> Gọi vào DL để chạy stored
            var numberOfAffectedRows = _movieDL.InsertMovie(movie);

            //XỬ lý kết quả trả về
            if (numberOfAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.InsertFailed,
                    Message = ResourceVI.InsertFaild
                };
            }
        }

        //public IEnumerable<Movie> GetAllMovieByType(int typeMovie, int? limit)
        //{
        //    var numberEffectedRow = _movieDL.GetAllMovieByType(typeMovie, limit);
        //    return numberEffectedRow;
        //}

        public PagingResult<Movie> GetAllMovieByTypeAndFilter(int pageNumber, int pageSize, Guid? categoryId, int? typeMovie, int columnFilter, string? keyword,
            Guid? categorySearchImproveId, int? startYear, int? endYear, int? columnSort, int? filterAndSortReview)
        {
            var numberEffectedRow = _movieDL.GetAllMovieByTypeAndFilter(pageNumber, pageSize, categoryId, typeMovie, columnFilter, keyword,
                categorySearchImproveId, startYear, endYear, columnSort, filterAndSortReview);
            return numberEffectedRow;
        }

        public PagingResult<Movie> GetSimilarMovieByFilter(int pageNumber, int pageSize, Guid? movieId)
        {
            var numberEffectedRow = _movieDL.GetSimilarMovieByFilter(pageNumber, pageSize, movieId);
            return numberEffectedRow;
        }

        public ServiceResult UpdateMovie(Guid movieId, Movie movie)
        {
            //var res = _movieDL.UpdateMovie(movieId, movie);
            //return res;

            //Validate
            var validateResults = ValidateRequest(movieId, movie);
            //Thất bại -> Return lỗi
            if (validateResults.Count > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.InvalidData,
                    Message = ResourceVI.Invalid_InputData,
                    Data = validateResults
                };
            }

            //Thành công -> Gọi vào DL để chạy stored
            var numberOfAffectedRows = _movieDL.UpdateMovie(movieId, movie);

            //XỬ lý kết quả trả về
            if (numberOfAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.PutFailed,
                    Message = ResourceVI.UpdateFailed
                };
            }
        }

        public int UpdateReview(Guid movieId)
        {
            var res = _movieDL.UpdateReview(movieId);

            return res;
        }

        public int UpdateMediumScore(Guid movieId, decimal mediumScore)
        {
            var res = _movieDL.UpdateMediumScore(movieId, mediumScore);

            return res;
        }

        public ServiceResult DeleteMovie(Guid movieId)
        {
            //Thành công -> Gọi vào DL để chạy stored
            var numberOfAffectedRows = _movieDL.DeleteMovie(movieId);

            //XỬ lý kết quả trả về
            if (numberOfAffectedRows > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.DeleteFailed,
                    Message = ResourceVI.DeleteFailed
                };
            }
        }
        #endregion
    }
}
