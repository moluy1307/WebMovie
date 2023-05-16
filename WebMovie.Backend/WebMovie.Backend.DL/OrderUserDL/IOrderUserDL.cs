using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.DL.BaseDL;

namespace WebMovie.Backend.DL.OrderUserDL
{
    public interface IOrderUserDL : IBaseDL<OrderUser>
    {
        /// <summary>
        /// Hàm lấy bản ghi theo Id
        /// </summary>
        /// <param name="recordId">Khóa chính</param>
        /// <returns>
        /// 200 - Lấy thông tin bản ghi theo Id thành công
        /// 500 - Có exception
        /// </returns>
        /// CreatedBy: huynq (9/2/2023)
        OrderUser GetOrderdById(Guid recordId);
        int UpdateAmountMoney(Guid orderId);

        int DeleteOrder(Guid recordId);

        IEnumerable<OrderUser> GetOrderByUserId(Guid userId);

        IEnumerable<OrderUser> GetOrderRevenue(int? sortByYear);

        IEnumerable<OrderUser> GetUserIdBoughtMovie();

        Task SendEmail(string subjectMess, string accountName, string customerName, int moneyRecharge);
    }
}
