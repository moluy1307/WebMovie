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

        IEnumerable<OrderUser> GetOrderByUserId(Guid userId);

        IEnumerable<OrderUser> GetOrderRevenue(int? sortByYear);

        IEnumerable<OrderUser> GetUserIdBoughtMovie();

        Task SendEmail(string subjectMess, string accountName, string customerName, int moneyRecharge);
    }
}
