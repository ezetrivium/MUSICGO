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
        Guid Add(TViewModel viewModel);

        bool Delete(Guid id);

        IList<TViewModel> Get();

        TViewModel GetById(Guid id);

        bool Update(TViewModel viewModel);

    }
}
