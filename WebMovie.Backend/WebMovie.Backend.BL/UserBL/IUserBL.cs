using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;

namespace WebMovie.Backend.BL.UserBL
{
    public interface IUserBL : IBasaBL<User>
    {
        User Login(string username, string password);

        ServiceResult ChangePassword(Guid userId, string password);

        Task SendConfirmCode(string emailMess);

        Task SendNewPassword(string emailMess);

        User GetUserByEmail(string emailUser);

        ServiceResult MinusAmountMoney(Guid userId, float amountMoneyMovie);

    }
}
