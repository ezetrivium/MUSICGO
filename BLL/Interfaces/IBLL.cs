using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace BLL.Interfaces
{
    public interface IBLL<TViewModel>
        where TViewModel : BaseViewModel
    {
        bool Add(TViewModel viewModel);

        bool Delete(Guid id);

        IEnumerable<TViewModel> Get();

        TViewModel GetById(Guid id);

        TViewModel Update(TViewModel viewModel);

    }
}
