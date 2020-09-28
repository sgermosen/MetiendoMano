using LineDietXF.Interfaces;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading;

namespace LineDietXF.ViewModels
{
    /// <summary>
    /// Base view model that all other view models derive from. It includes common services which all VMs need access to as well as common logic
    /// for keeping track of if the app is busy waiting for a pending request
    /// </summary>
    public class BaseViewModel : BindableBase
    {
        protected INavigationService NavigationService { get; set; }
        protected ISettingsService SettingsService { get; set; }
        protected IAnalyticsService AnalyticsService { get; set; }
        protected IPageDialogService DialogService { get; set; }

        int _pendingRequestsCounter;
        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }

            // This setter is private. Deriving VM's should call IncreasePendingRequestCount and DecreasePendingRequestCount which in turn updates the IsBusy flag internally.
            private set
            {
                if (_isBusy == value)
                    return;

                SetProperty(ref _isBusy, value);

                if (IsBusyChanged != null)
                    IsBusyChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler IsBusyChanged;

        public BaseViewModel(INavigationService navigationService, ISettingsService settingsService, IAnalyticsService analyticsService, IPageDialogService dialogService)
        {
            // Store off injected services
            NavigationService = navigationService;
            SettingsService = settingsService;
            AnalyticsService = analyticsService;
            DialogService = dialogService;
        }

        void ToggleBusyIndicator(int requestCounter)
        {
            this.IsBusy = requestCounter > 0;
        }

        protected void IncrementPendingRequestCount()
        {
            this.ToggleBusyIndicator(Interlocked.Increment(ref _pendingRequestsCounter));
        }

        protected void DecrementPendingRequestCount()
        {
            if (_pendingRequestsCounter == 0)
            {
                AnalyticsService.TrackError($"{nameof(DecrementPendingRequestCount)} - Asked to decrement active request counter, but already 0.");
                return;
            }

            this.ToggleBusyIndicator(Interlocked.Decrement(ref _pendingRequestsCounter));
        }
    }
}
