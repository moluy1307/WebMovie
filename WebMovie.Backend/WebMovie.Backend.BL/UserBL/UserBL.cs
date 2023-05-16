using Misa.Amis.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.Common.Entities.DTO;
using WebMovie.Backend.DL.BaseDL;
using WebMovie.Backend.DL.UserDL;

namespace WebMovie.Backend.BL.UserBL
{
    public class UserBL : BaseBL<User>, IUserBL
    {
        #region Field

        private IUserDL _userDL;

        #endregion

        #region Constructor

        public UserBL(IUserDL userDL) : base(userDL)
        {
            _userDL = userDL;
        }

        #endregion

        #region Method
        public User Login(string username, string password)
        {
            var res = _userDL.Login(username, password);

            return res;
        }

        public ServiceResult ChangePassword(Guid userId, string password)
        {
            int numberOfAffectedRows = _userDL.ChangePassword(userId, password);

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

        public User GetUserByEmail(string emailUser)
        {
            var res = _userDL.GetUserByEmail(emailUser);

            return res;
        }

        public async Task SendConfirmCode(string emailMess)
        {
            await _userDL.SendConfirmCode(emailMess);
        }

        public async Task SendNewPassword(string emailMess)
        {
            await _userDL.SendNewPassword(emailMess);
        }

        //public async Task SendEmail(string emailMess, string subjectMess, string bodyMess)
        //{
        //    await _userDL.SendEmail(emailMess, subjectMess, bodyMess);
        //}

        public ServiceResult MinusAmountMoney(Guid userId, float amountMoneyMovie)
        {
            int res = _userDL.MinusAmountMoney(userId, amountMoneyMovie);
            if (res > 0)
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
        #endregion
    }
}
