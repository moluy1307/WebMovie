﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
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

        public int InsertMovie(Movie movie)
        {
            var record = _movieDL.InsertMovie(movie);
            return record;
        }

        //public IEnumerable<Movie> GetAllMovieByType(int typeMovie, int? limit)
        //{
        //    var numberEffectedRow = _movieDL.GetAllMovieByType(typeMovie, limit);
        //    return numberEffectedRow;
        //}

        public PagingResult<Movie> GetAllMovieByTypeAndFilter(int pageNumber, int pageSize, Guid? categoryId, int? typeMovie, int columnFilter)
        {
            var numberEffectedRow = _movieDL.GetAllMovieByTypeAndFilter(pageNumber, pageSize, categoryId, typeMovie, columnFilter);
            return numberEffectedRow;
        }

        public PagingResult<Movie> GetSimilarMovieByFilter(int pageNumber, int pageSize, Guid? movieId)
        {
            var numberEffectedRow = _movieDL.GetSimilarMovieByFilter(pageNumber, pageSize, movieId);
            return numberEffectedRow;
        }

        public int UpdateMovie(Guid movieId, Movie movie)
        {
            var res = _movieDL.UpdateMovie(movieId, movie);
            return res;
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
        #endregion
    }
}
