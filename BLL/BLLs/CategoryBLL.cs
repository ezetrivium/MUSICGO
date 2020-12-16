using BE.Entities;
using DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ViewModels;

namespace BLL.BLLs
{
    public class CategoryBLL : BaseBLL<CategoryBE, CategoryViewModel>
    {
        public CategoryBLL()
        {
            this.Dal = new CategoryDAL();
        }
    }
}
