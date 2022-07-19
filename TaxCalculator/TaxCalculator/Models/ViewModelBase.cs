using System;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace TaxCalculator.Models
{
    public class ViewModelBase  : ReactiveObject, INavigationAware
    {
        protected IPageDialogService PageDialogService { get; }
        protected INavigationService NavigationService { get; }

        public ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            PageDialogService = pageDialogService;
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
        }
    }
}

