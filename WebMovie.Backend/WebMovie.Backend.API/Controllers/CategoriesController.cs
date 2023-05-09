using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Amis.API.Controllers;
using WebMovie.Backend.BL.CategoryBL;
using WebMovie.Backend.Common.Entities;

namespace WebMovie.Backend.API.Controllers
{
    public class CategoriesController : BasesController<Category>
    {
        #region Field

        private ICategoryBL _categoryBL;

        #endregion

        #region Constructor

        public CategoriesController(ICategoryBL categoryBL) : base(categoryBL)
        {
            _categoryBL = categoryBL;
        }

        #endregion
    }
}
