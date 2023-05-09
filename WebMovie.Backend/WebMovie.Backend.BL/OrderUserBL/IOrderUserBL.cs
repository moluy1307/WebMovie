using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;

namespace WebMovie.Backend.BL.OrderUserBL
{
    public interface IOrderUserBL : IBasaBL<OrderUser>
    {
        OrderUser GetOrderdById(Guid recordId);

        ServiceResult UpdateAmountMoney(Guid orderId);

        ServiceResult DeleteOrder(Guid recordId);

        OrderUser GetOrderByUserId(Guid userId);
    }
}
