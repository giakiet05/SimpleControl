using SimpleControlDesktop.Commands;
using SimpleControlDesktop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleControlDesktop.ViewModels
{
    public class DisconnectConfirmModalViewModel : BaseViewModel
    {
        private readonly ConnectedDeviceViewModel _viewModel;
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public DisconnectConfirmModalViewModel(ConnectedDeviceViewModel viewModel)
        {
            SubmitCommand = new RelayCommand(ExecuteSubmit);
            CancelCommand = new CancelCommand();
            _viewModel = viewModel;
        }

        private void ExecuteSubmit()
        {
            GenericStore<ConnectedDeviceViewModel>.Instance.Delete(e => e.ConnectedDevice.Id == _viewModel.ConnectedDevice.Id);
            ToastMessageViewModel.ShowSuccessToast($"Disconnected with {_viewModel.ConnectedDevice.Name}");
            ModalNavigationStore.Instance.Close();
        }
    }
}

