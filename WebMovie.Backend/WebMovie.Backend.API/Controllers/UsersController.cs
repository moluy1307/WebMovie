using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.API.Controllers;
using WebMovie.Backend.BL.UserBL;
using WebMovie.Backend.Common;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;

namespace WebMovie.Backend.API.Controllers
{
    public class UsersController : BasesController<User>
    {
        #region Field

        private IUserBL _userBL;

        #endregion

        #region Constructor

        public UsersController(IUserBL userBL) : base(userBL)
        {
            _userBL = userBL;
        }

        #endregion

        #region Method

        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string username, string password)
        {
            try
            {
                var result = _userBL.Login(username, password);

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
        [Route("changePassword")]
        public IActionResult ChangePassword(Guid userId, string password)
        {
            try
            {
                var result = _userBL.ChangePassword(userId, password);

                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else if (!result.IsSuccess && result.ErrorCode == Common.Enums.ErrorCode.InvalidData)
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
        [Route("GetUserByEmail")]
        public IActionResult GetUserByEmail(string emailUser)
        {
            try
            {
                var result = _userBL.GetUserByEmail(emailUser);

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

        [HttpPost("SendConfirmCode")]
        public async Task<IActionResult> SendConfirmCode(string emailMess)
        {
            try
            {
                await _userBL.SendConfirmCode(emailMess);

                return Ok();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }

        }

        [HttpPost("SendNewPassword")]
        public async Task<IActionResult> SendNewPassword(string emailMess)
        {
            try
            {
                await _userBL.SendNewPassword(emailMess);

                return Ok();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return HandleException(ex);
            }

        }

        [HttpPut("MinusMoneyUser")]
        public IActionResult MinusAmountMoney(Guid userId, float amountMoneyMovie)
        {
            try
            {
                var result = _userBL.MinusAmountMoney(userId, amountMoneyMovie);

                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else if (!result.IsSuccess && result.ErrorCode == Common.Enums.ErrorCode.InvalidData)
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
        #endregion
    }
}
