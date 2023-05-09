using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMovie.Backend.BL.BaseBL;
using WebMovie.Backend.Common.Entities;
using WebMovie.Backend.DL.CategoryDL;

namespace WebMovie.Backend.BL.CategoryBL
{
    public class CategoryBL : BaseBL<Category>, ICategoryBL
    {
        #region Field

        private ICategoryDL _categoryDL;

        #endregion

        #region Constructor

        public CategoryBL(ICategoryDL categoryDL) : base(categoryDL)
        {
            _categoryDL = categoryDL;
        }

        #endregion
    }
}
