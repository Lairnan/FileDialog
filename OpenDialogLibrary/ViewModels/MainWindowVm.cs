using MvvmBuilder.Notifies;
using System.Windows.Controls;
using OpenDialogLibrary.Services.Interface;
using OpenDialogLibrary.Views.Pages;

namespace OpenDialogLibrary.ViewModels
{
    internal class MainWindowVm : NotifyBase
    {
        private readonly IPageService _pageService;

        internal MainWindowVm(IPageService pageService)
        {
            _pageService = pageService;
            _pageService.PageChanged += page => this.CurrentPage = page;
            _pageService.Navigate(new MainPage());
        }

        public Page CurrentPage
        {
            get => GetProperty<Page>();
            private set => SetProperty(value);
        }
    }
}