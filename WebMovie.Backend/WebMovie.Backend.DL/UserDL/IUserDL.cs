using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.DL.BaseDL;

namespace WebMovie.Backend.DL.UserDL
{
    public interface IUserDL : IBaseDL<User>
    {
        User Login(string username, string password);

        int ChangePassword(Guid userId, string password);

        Task SendConfirmCode(string emailMess);

        Task SendNewPassword(string emailMess);

        User GetUserByEmail(string emailUser);

        int MinusAmountMoney(Guid userId, float amountMoneyMovie);

    }
}
