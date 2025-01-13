using SimpleControlDesktop.Commands;
using SimpleControlDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleControlDesktop.ViewModels
{
    public class ConnectedDeviceViewModel : BaseViewModel
    {

        public Phone ConnectedDevice { get; private set; }

        public ICommand ShowDisconnectConfirmModalCommand { get; }

        public ConnectedDeviceViewModel(Phone connectedDevice)
        {
            ConnectedDevice = connectedDevice;
            ShowDisconnectConfirmModalCommand = new NavigateModalCommand(() => new DisconnectConfirmModalViewModel(this));
        }
    }
}
