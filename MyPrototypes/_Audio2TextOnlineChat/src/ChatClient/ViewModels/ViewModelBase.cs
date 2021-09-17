using Ardalis.GuardClauses;
using Prism.Mvvm;
using Prism.Navigation;

namespace ChatClient.ViewModels
{
    public abstract class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected ViewModelBase(INavigationService navigationService)
        {
            Guard.Against.Null(navigationService, nameof(navigationService));

            NavigationService = navigationService;
        }

        #region Properties

        protected INavigationService NavigationService { get; private set; }

        public string Title { get; protected set; }

        #endregion

        #region Public Interface

        /// <inheritdoc/>
        public virtual void Initialize(INavigationParameters parameters) { }

        /// <inheritdoc/>
        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        /// <inheritdoc/>
        public virtual void OnNavigatedTo(INavigationParameters parameters) { }

        /// <inheritdoc/>
        public virtual void Destroy() { }

        #endregion
    }
}
