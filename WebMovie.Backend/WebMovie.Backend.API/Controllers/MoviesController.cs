using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.API.Controllers;
using MySqlConnector;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.BL.MovieBL;
using WebMovie.Backend.BL.UserBL;
using WebMovie.Backend.Common;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL.MovieDL;
using WebMovie.Backend.DL;

namespace WebMovie.Backend.API.Controllers
{
    public class MoviesController : BasesController<Movie>
    {
        #region Field

        private IMovieBL _movieBL;
        public IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Constructor

        public MoviesController(IMovieBL movieBL, IWebHostEnvironment webHostEnvironment) : base(movieBL)
        {
            _movieBL = movieBL;
            _webHostEnvironment = webHostEnvironment;
        }

        #endregion

        #region Method

        [HttpGet]
        [Route("GetAllMovie")]
        public IActionResult GetAllMovies()
        {
            try
            {
                var result = _movieBL.GetAllMovies();

                //Xử lý kết quả trả về
                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.GetFailed,
                        DevMsg = ResourceVI.Error_DatabaseQuery,
                        UserMsg = ResourceVI.Login_Failed
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("GetMovieById")]
        public IActionResult GetMovieById(Guid movieId)
        {
            try
            {

                var result = _movieBL.GetMovieById(movieId);

                string imagePath = $"{result.MovieCode}.png";
                string imageSavePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", imagePath);
                result.ImgByte = System.IO.File.ReadAllBytes(imageSavePath);

                //Xử lý kết quả trả về
                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.GetFailed,
                        DevMsg = ResourceVI.Error_DatabaseQuery,
                        UserMsg = ResourceVI.Login_Failed
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        [HttpPost]
        [Route("PostMovie")]
        public IActionResult InsertMovie([FromForm] Movie movieId)
        {
            try
            {
                string imagePath = null;
                if (movieId.Image != null)
                {
                    imagePath = $"{movieId.MovieCode}{Path.GetExtension(movieId.Image.FileName)}";
                    string imageSavePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", imagePath);
                    using (var stream = new FileStream(imageSavePath, FileMode.Create))
                    {
                        movieId.Image.CopyTo(stream);
                        stream.Flush();
                    }
                }

                //movieId.Categories = JsonConvert.DeserializeObject<List<MovieCategory>>(movieId.CateMovie);
                //movieId.Categories = details;

                var newMovie = new Movie
                {
                    MovieCode = movieId.MovieCode,
                    MovieName = movieId.MovieName,
                    ImagePath = imagePath,
                    MovieInfor = movieId.MovieInfor,
                    MovieDirector = movieId.MovieDirector,
                    MovieReview = movieId.MovieReview,
                    MovieTime = movieId.MovieTime,
                    DateOfManufactor = movieId.DateOfManufactor,
                    MovieArea = movieId.MovieArea,
                    Amount = movieId.Amount,
                    TypeMovie = movieId.TypeMovie,
                    CateMovie = movieId.CateMovie,
                    EpisodeMovie = movieId.EpisodeMovie,
                    Categories = JsonConvert.DeserializeObject<List<MovieCategory>>(movieId.CateMovie),
                    ActorMovie = movieId.ActorMovie,
                    Actors = JsonConvert.DeserializeObject<List<MovieActor>>(movieId.ActorMovie),
                    Episodes = JsonConvert.DeserializeObject<List<Episode>>(movieId.EpisodeMovie),
                };

                var result = _movieBL.InsertMovie(newMovie);

                //Xử lý kết quả trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.GetFailed,
                        DevMsg = ResourceVI.Error_DatabaseQuery,
                        UserMsg = ResourceVI.Login_Failed
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        //[HttpGet]
        //[Route("GetAllMovieByType")]
        //public IActionResult GetAllMovieByType(int typeMovie, int? limit)
        //{
        //    try
        //    {
        //        var result = _movieBL.GetAllMovieByType(typeMovie, limit);

        //        string imagePath = string.Empty;
        //        string imageSavePath = string.Empty;

        //        foreach (var movieItem in result)
        //        {
        //            imagePath = $"{movieItem.MovieCode}.png";
        //            imageSavePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", imagePath);
        //            movieItem.ImgByte = System.IO.File.ReadAllBytes(imageSavePath);
        //        }

        //        //Xử lý kết quả trả về
        //        if (result != null)
        //        {
        //            return StatusCode(StatusCodes.Status200OK, result);
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
        //            {
        //                ErrorCode = Common.Enums.ErrorCode.GetFailed,
        //                DevMsg = ResourceVI.Error_DatabaseQuery,
        //                UserMsg = ResourceVI.Login_Failed
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return HandleException(ex);
        //    }
        //}

        [HttpGet]
        [Route("GetAllMovieByTypeAndFilter")]
        public IActionResult GetAllMovieByTypeAndFilter(int pageNumber, int pageSize, Guid? categoryId, int? typeMovie, int columnFilter)
        {
            try
            {
                var result = _movieBL.GetAllMovieByTypeAndFilter(pageNumber, pageSize, categoryId, typeMovie, columnFilter);

                //var result = _movieBL.GetMovieById(movieId);

                //string imagePath = $"{result.MovieCode}.png";
                //string imageSavePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", imagePath);
                //result.ImgByte = System.IO.File.ReadAllBytes(imageSavePath);

                string imagePath = string.Empty;
                string imageSavePath = string.Empty;

                foreach (var movieItem in result.Data)
                {
                    imagePath = $"{movieItem.MovieCode}.png";
                    imageSavePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", imagePath);
                    movieItem.ImgByte = System.IO.File.ReadAllBytes(imageSavePath);
                }

                //Xử lý kết quả trả về
                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.GetFailed,
                        DevMsg = ResourceVI.Error_DatabaseQuery,
                        UserMsg = ResourceVI.Login_Failed
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("GetSimilarMovieByFilter")]
        public IActionResult GetSimilarMovieByFilter(int pageNumber, int pageSize, Guid? movieId)
        {
            try
            {
                var result = _movieBL.GetSimilarMovieByFilter(pageNumber, pageSize, movieId);

                //var result = _movieBL.GetMovieById(movieId);

                //string imagePath = $"{result.MovieCode}.png";
                //string imageSavePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", imagePath);
                //result.ImgByte = System.IO.File.ReadAllBytes(imageSavePath);

                string imagePath = string.Empty;
                string imageSavePath = string.Empty;

                foreach (var movieItem in result.Data)
                {
                    imagePath = $"{movieItem.MovieCode}.png";
                    imageSavePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", imagePath);
                    movieItem.ImgByte = System.IO.File.ReadAllBytes(imageSavePath);
                }

                //Xử lý kết quả trả về
                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.GetFailed,
                        DevMsg = ResourceVI.Error_DatabaseQuery,
                        UserMsg = ResourceVI.Login_Failed
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        [HttpPut]
        [Route("UpdateMovie")]
        public IActionResult UpdateMovie(Guid movieId, [FromForm] Movie movie)
        {
            try
            {
                string imagePath = null;
                if (movie.Image != null)
                {
                    imagePath = $"{movie.MovieCode}{Path.GetExtension(movie.Image.FileName)}";
                    string imageSavePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", imagePath);
                    using (var stream = new FileStream(imageSavePath, FileMode.Create))
                    {
                        movie.Image.CopyTo(stream);
                        stream.Flush();
                    }
                }

                //movie.Categories = JsonConvert.DeserializeObject<List<MovieCategory>>(movie.CateMovie);
                //movie.Categories = details;

                var newMovie = new Movie
                {
                    MovieCode = movie.MovieCode,
                    MovieName = movie.MovieName,
                    ImagePath = imagePath,
                    MovieInfor = movie.MovieInfor,
                    MovieDirector = movie.MovieDirector,
                    MovieReview = movie.MovieReview,
                    MovieTime = movie.MovieTime,
                    DateOfManufactor = movie.DateOfManufactor,
                    MovieArea = movie.MovieArea,
                    Amount = movie.Amount,
                    TypeMovie = movie.TypeMovie,
                    CateMovie = movie.CateMovie,
                    EpisodeMovie = movie.EpisodeMovie,
                    Categories = JsonConvert.DeserializeObject<List<MovieCategory>>(movie.CateMovie),
                    ActorMovie = movie.ActorMovie,
                    Actors = JsonConvert.DeserializeObject<List<MovieActor>>(movie.ActorMovie),
                    Episodes = JsonConvert.DeserializeObject<List<Episode>>(movie.EpisodeMovie),
                };

                var result = _movieBL.UpdateMovie(movieId, newMovie);

                //Xử lý kết quả trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.GetFailed,
                        DevMsg = ResourceVI.Error_DatabaseQuery,
                        UserMsg = ResourceVI.Login_Failed
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        [HttpPut]
        [Route("UpdateMovieReview")]
        public IActionResult UpdateReview(Guid movieId)
        {
            try
            {

                var result = _movieBL.UpdateReview(movieId);

                //Xử lý kết quả trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.GetFailed,
                        DevMsg = ResourceVI.Error_DatabaseQuery,
                        UserMsg = ResourceVI.Login_Failed
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }

        [HttpPut]
        [Route("UpdateMediumScore")]
        public IActionResult UpdateMediumScore(Guid movieId, decimal mediumScore)
        {
            try
            {

                var result = _movieBL.UpdateMediumScore(movieId, mediumScore);

                //Xử lý kết quả trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.GetFailed,
                        DevMsg = ResourceVI.Error_DatabaseQuery,
                        UserMsg = ResourceVI.Login_Failed
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }
        }
        #endregion
    }
}
