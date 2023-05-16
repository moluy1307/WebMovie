using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.BL.RoleUserBL;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.DL.RoleUserDL;

namespace WebMovie.Backend.BL.RoleUserBL
{
    public class RoleUserBL : BaseBL<RoleUser>, IRoleUserBL
    {
        #region Field

        private IRoleUserDL _roleUserDL;

        #endregion

        #region Constructor

        public RoleUserBL(IRoleUserDL roleUserDL) : base(roleUserDL)
        {
            _roleUserDL = roleUserDL;
        }

        #endregion
    }
}
