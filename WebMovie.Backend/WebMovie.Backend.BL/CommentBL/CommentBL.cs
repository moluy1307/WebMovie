using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.DL.CommentDL;

namespace WebMovie.Backend.BL.CommentBL
{
    public class CommentBL : BaseBL<Comment>, ICommentBL
    {
        #region Field

        private ICommentDL _commentDL;

        #endregion

        #region Constructor

        public CommentBL(ICommentDL commentDL) : base(commentDL)
        {
            _commentDL = commentDL;
        }

        #endregion
    }
}
