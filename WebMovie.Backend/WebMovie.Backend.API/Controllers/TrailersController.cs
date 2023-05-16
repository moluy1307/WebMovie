using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.API.Controllers;
using WebMovie.Backend.BL.TrailerBL;
using WebMovie.Backend.Common;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL.TrailerDL;

namespace WebMovie.Backend.API.Controllers
{
    public class TrailersController : BasesController<Trailer>
    {
        #region Field

        private ITrailerBL _trailerBL;

        #endregion

        #region Constructor

        public TrailersController(ITrailerBL trailerBL) : base(trailerBL)
        {
            _trailerBL = trailerBL;
        }

        #endregion

        #region Method

        [HttpGet]
        [Route("GetAllTrailerByMovieId")]
        public IActionResult GetAllTrailerByMovieId(Guid movieId)
        {
            try
            {
                var result = _trailerBL.GetAllTrailerByMovieId(movieId);

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
