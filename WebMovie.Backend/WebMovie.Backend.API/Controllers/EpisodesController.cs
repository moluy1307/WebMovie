using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.API.Controllers;
using WebMovie.Backend.BL.EpisodeBL;
using WebMovie.Backend.Common;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL.EpisodeDL;

namespace WebMovie.Backend.API.Controllers
{
    public class EpisodesController : BasesController<Episode>
    {
        #region Field

        private IEpisodeBL _episodeBL;

        #endregion

        #region Constructor

        public EpisodesController(IEpisodeBL episodeBL) : base(episodeBL)
        {
            _episodeBL = episodeBL;
        }

        #endregion

        #region Method

        [HttpGet]
        [Route("GetEpisodeByMovieId")]
        public IActionResult GetEpisodesByMovieId(Guid movieId, int pageNumber, int pageSize)
        {
            try
            {
                var result = _episodeBL.GetEpisodesByMovieId(movieId, pageNumber, pageSize);

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
