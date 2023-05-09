using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.API.Controllers;
using WebMovie.Backend.BL.OrderUserBL;
using WebMovie.Backend.Common;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL.OrderUserDL;

namespace WebMovie.Backend.API.Controllers
{
    public class OrderUsersController : BasesController<OrderUser>
    {
        #region Field

        private IOrderUserBL _orderUserBL;

        #endregion

        #region Constructor

        public OrderUsersController(IOrderUserBL orderUserBL) : base(orderUserBL)
        {
            _orderUserBL = orderUserBL;
        }

        #endregion

        #region Method

        [HttpGet]
        [Route("GetOrderById")]
        public IActionResult GetOrderdById(Guid recordId)
        {
            try
            {
                var recordById = _orderUserBL.GetOrderdById(recordId);

                //Xử lý kết quả trả về
                if (recordById != null)
                {
                    return StatusCode(StatusCodes.Status200OK, recordById);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.GetFailed,
                        DevMsg = ResourceVI.Error_DatabaseQuery,
                        UserMsg = ResourceVI.Error_Exception
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HandleException(ex);
            }

        }

        [HttpGet]
        [Route("UpdateMoney")]
        public IActionResult UpdateAmountMoney(Guid orderId)
        {
            try
            {
                var result = _orderUserBL.UpdateAmountMoney(orderId);

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

        /// <summary>
        /// Hàm xóa thông tin bản ghi
        /// </summary>
        /// <param name="recordId">Khóa chính</param>
        /// <param name="record">Bản ghi có Id trên</param>
        /// <returns>
        /// 200 - Sửa thông tin bản ghi thành công
        /// 400 - Đầu vào không hợp lệ
        /// 500 - Có exception
        /// </returns>
        /// CreatedBy: huynq (9/2/2023)
        [HttpDelete]
        [Route("DeleteOrder")]
        public IActionResult DeleteOrder(Guid recordId)
        {
            try
            {
                var result = _orderUserBL.DeleteOrder(recordId);

                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status200OK);
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
        [Route("GetByUserId")]
        public IActionResult GetOrderByUserId(Guid userId)
        {
            try
            {
                var recordById = _orderUserBL.GetOrderByUserId(userId);

                //Xử lý kết quả trả về
                if (recordById != null)
                {
                    return StatusCode(StatusCodes.Status200OK, recordById);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.GetFailed,
                        DevMsg = ResourceVI.Error_DatabaseQuery,
                        UserMsg = ResourceVI.Error_Exception
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HandleException(ex);
            }
        }

        #endregion
    }
}
