using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL.OrderUserDL;

namespace WebMovie.Backend.BL.OrderUserBL
{
    public class OrderUserBL : BaseBL<OrderUser>, IOrderUserBL
    {
        #region Field

        private IOrderUserDL _orderUserDL;

        #endregion

        #region Constructor

        public OrderUserBL(IOrderUserDL orderUserDL) : base(orderUserDL)
        {
            _orderUserDL = orderUserDL;
        }

        #endregion

        #region Method

        public OrderUser GetOrderdById(Guid recordId)
        {
            var recordById = _orderUserDL.GetOrderdById(recordId);
            return recordById;
        }

        public ServiceResult UpdateAmountMoney(Guid orderId)
        {
            int numberOfAffectedRows = _orderUserDL.UpdateAmountMoney(orderId);

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
                    Message = ResourceVI.ChangePass_Failed
                };
            }
        }

        public ServiceResult DeleteOrder(Guid recordId)
        {
            var numberOfAffectedRows = _orderUserDL.DeleteOrder(recordId);

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

        public OrderUser GetOrderByUserId(Guid userId)
        {
            var res = _orderUserDL.GetOrderByUserId(userId);
            return res;
        }
        #endregion
    }
}
