using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.API.Controllers;
using WebMovie.Backend.BL.RoleUserBL;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.DL.RoleUserDL;

namespace WebMovie.Backend.API.Controllers
{
    public class RoleUsersController : BasesController<RoleUser>
    {
        #region Field

        private IRoleUserBL _roleUserBL;

        #endregion

        #region Constructor

        public RoleUsersController(IRoleUserBL roleUserBL) : base(roleUserBL)
        {
            _roleUserBL = roleUserBL;
        }

        #endregion
    }
}
