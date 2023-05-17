using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.API.Controllers;
using Newtonsoft.Json;
using WebMovie.Backend.BL.ActorBL;
using WebMovie.Backend.Common;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL.ActorDL;

namespace WebMovie.Backend.API.Controllers
{
    public class ActorsController : BasesController<Actor>
    {
        #region Field

        private IActorBL _actorBL;
        public IWebHostEnvironment _webHostEnvironment;

        #endregion

        #region Constructor

        public ActorsController(IActorBL actorBL, IWebHostEnvironment webHostEnvironment) : base(actorBL)
        {
            _actorBL = actorBL;
            _webHostEnvironment = webHostEnvironment;
        }

        #endregion

        #region Method

        [HttpGet]
        [Route("GetActorById")]
        public IActionResult GetActorById(Guid actorId)
        {
            try
            {

                var result = _actorBL.GetActorById(actorId);

                string imagePath = $"{result.Fullname}.png";
                string imageSavePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images\\Actors", imagePath);
                result.ImgByte = System.IO.File.ReadAllBytes(imageSavePath);

                //var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                //result.ImagePath = Path.Combine(uploadsFolder, result.ImagePath);

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
        [Route("InsertActor")]
        public IActionResult InsertActor([FromForm] Actor actor, Guid? parent)
        {
            try
            {
                string imagePath = null;
                if (actor.Image != null)
                {
                    imagePath = $"{actor.Fullname}{Path.GetExtension(actor.Image.FileName)}";
                    string imageSavePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images\\Actors", imagePath);
                    using (var stream = new FileStream(imageSavePath, FileMode.Create))
                    {
                        actor.Image.CopyTo(stream);
                        stream.Flush();
                    }
                }

                //movieId.Categories = JsonConvert.DeserializeObject<List<MovieCategory>>(movieId.CateMovie);
                //movieId.Categories = details;

                var newActor = new Actor
                {
                    ActorCode = actor.ActorCode,
                    Fullname = actor.Fullname,
                    ImagePath = imagePath,
                    DateOfBirth = actor.DateOfBirth,
                    Biography = actor.Biography,
                    Country = actor.Country,
                    Gender = actor.Gender,
                };

                var result = _actorBL.InsertActor(newActor, parent);

                //Xử lý kết quả trả về
                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (!result.IsSuccess && result.ErrorCode == WebMovie.Backend.Common.Enums.ErrorCode.InvalidData)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        MoreInfo = result.Data,
                        TradeId = HttpContext.TraceIdentifier
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        MoreInfo = result.Data,
                        TradeId = HttpContext.TraceIdentifier
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
        [Route("UpdateActor")]
        public IActionResult UpdateActor(Guid actorId, [FromForm] Actor? actor)
        {
            try
            {
                string imagePath = null;
                if (actor.Image != null)
                {
                    imagePath = $"{actor.Fullname}{Path.GetExtension(actor.Image.FileName)}";
                    string imageSavePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images\\Actors", imagePath);
                    using (var stream = new FileStream(imageSavePath, FileMode.Create))
                    {
                        actor.Image.CopyTo(stream);
                        stream.Flush();
                    }
                }

                //movieId.Categories = JsonConvert.DeserializeObject<List<MovieCategory>>(movieId.CateMovie);
                //movieId.Categories = details;

                var newActor = new Actor
                {
                    ActorCode = actor.ActorCode,
                    Fullname = actor.Fullname,
                    ImagePath = imagePath,
                    DateOfBirth = actor.DateOfBirth,
                    Biography = actor.Biography,
                    Country = actor.Country,
                    Gender = actor.Gender,
                };

                var result = _actorBL.UpdateActor(actorId, newActor);

                //Xử lý kết quả trả về
                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (!result.IsSuccess && result.ErrorCode == WebMovie.Backend.Common.Enums.ErrorCode.InvalidData)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        MoreInfo = result.Data,
                        TradeId = HttpContext.TraceIdentifier
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = result.Message,
                        MoreInfo = result.Data,
                        TradeId = HttpContext.TraceIdentifier
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
        [Route("GetActorByMovieId")]
        public IActionResult GetActorByMovieId(Guid movieId)
        {
            try
            {

                var result = _actorBL.GetActorByMovieId(movieId);

                string imagePath = string.Empty;
                string imageSavePath = string.Empty;

                foreach (var actorItem in result)
                {
                    imagePath = $"{actorItem.Fullname}.png";
                    imageSavePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images\\Actors", imagePath);
                    actorItem.ImgByte = System.IO.File.ReadAllBytes(imageSavePath);
                }

                //var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                //result.ImagePath = Path.Combine(uploadsFolder, result.ImagePath);

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
        [Route("GetActorBySearchingImprove")]
        public IActionResult GetActorBySearchingImprove(int pageNumber, int pageSize, string? keyword, int? gender, int? startYear, int? endYear, int? columnSort)
        {
            try
            {

                var result = _actorBL.GetActorBySearchingImprove(pageNumber, pageSize, keyword, gender, startYear, endYear, columnSort);

                string imagePath = string.Empty;
                string imageSavePath = string.Empty;

                foreach (var actorItem in result.Data)
                {
                    imagePath = $"{actorItem.Fullname}.png";
                    imageSavePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images\\Actors", imagePath);
                    actorItem.ImgByte = System.IO.File.ReadAllBytes(imageSavePath);
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
        #endregion
    }
}
