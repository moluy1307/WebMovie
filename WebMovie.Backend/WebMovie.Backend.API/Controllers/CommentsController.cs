using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.API.Controllers;
using WebMovie.Backend.BL.CommentBL;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.DL.CommentDL;

namespace WebMovie.Backend.API.Controllers
{
    public class CommentsController : BasesController<Comment>
    {
        #region Field

        private ICommentBL _commentBL;

        #endregion

        #region Constructor

        public CommentsController(ICommentBL commentBL) : base(commentBL)
        {
            _commentBL = commentBL;
        }

        #endregion
    }
}
