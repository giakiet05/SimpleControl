using QRCoder;
using SimpleControlDesktop.Commands;
using SimpleControlDesktop.Stores;
using SimpleControlDesktop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using SimpleControlDesktop.Server;
using SimpleControlDesktop.Models;

namespace SimpleControlDesktop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly GenericStore<ConnectedDeviceViewModel> _deviceStore;

        public IEnumerable<ConnectedDeviceViewModel> ConnectedDevices => _deviceStore.Items;

        public Task InitializationTask { get; }
        public BaseViewModel CurrentModalViewModel => ModalNavigationStore.Instance.CurrentModalViewModel;
        public bool IsOpen => ModalNavigationStore.Instance.IsOpen;
        public ObservableCollection<ToastMessageView> Toasts => ToastMessageViewModel.Toasts;


        public ICommand GenerateQRCodeCommand { get; }


        public MainViewModel()
        {

            ModalNavigationStore.Instance.CurrentModalViewModelChanged += OnCurrentModalViewModelChanged;
            _deviceStore = GenericStore<ConnectedDeviceViewModel>.Instance;

            _deviceStore.Add(new ConnectedDeviceViewModel(new Phone
            {
                Name = "Samsung",
                OSVersion = "Android",

            }));
            _deviceStore.Add(new ConnectedDeviceViewModel(new Phone
            {
                Name = "Oppo",
                OSVersion = "Android",

            }));
            _deviceStore.Add(new ConnectedDeviceViewModel(new Phone
            {
                Name = "Samsung 2",
                OSVersion = "Android",

            }));

            InitializationTask = InitializeAsync();
        }

        private void GenerateQRCode(string data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            // Create QR code data
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);

            // Generate QR code
            QRCode qrCode = new QRCode(qrCodeData);

            // Render the QR code as a Bitmap
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            // Save the QR code as a PNG file
            qrCodeImage.Save("QRCode.png", System.Drawing.Imaging.ImageFormat.Png);


        }


        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsOpen));
        }

        private async Task InitializeAsync()
        {
            string serverUri = "http://localhost:8080/";
            await WebSocketServer.StartServerAsync(serverUri);
        }

    }
}
