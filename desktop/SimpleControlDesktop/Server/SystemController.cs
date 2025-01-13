using Microsoft.Win32;
using NAudio.CoreAudioApi;
using System.Diagnostics;
using System.Drawing;
using System.Management;
using System.Windows.Forms;

namespace SimpleControlDesktop.Server
{
    public static class SystemController
    {
        public static void Shutdown()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "shutdown",
                Arguments = "/s /t 0", // /s for shutdown, /t 0 for immediate
                CreateNoWindow = true,
                UseShellExecute = false,
            });
        }

        public static void Restart()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "shutdown",
                Arguments = "/r /t 0", // /r for restart, /t 0 for immediate
                CreateNoWindow = true,
                UseShellExecute = false,
            });
        }

        public static void Sleep()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "rundll32.exe",
                Arguments = "powrprof.dll,SetSuspendState 0,1,0", // Command for sleep
                CreateNoWindow = true,
                UseShellExecute = false,
            });
        }


        public static void SetVolume(int volume)
        {
            // Ensure the volume is within the valid range (0 to 100)
            volume = Math.Clamp(volume, 0, 100);

            // Convert the integer volume to a float range (0.0 to 1.0)
            float volumeScalar = volume / 100f;

            using (var enumerator = new MMDeviceEnumerator())
            {
                var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                device.AudioEndpointVolume.MasterVolumeLevelScalar = volumeScalar;
            }
        }

        public static float GetVolume()
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                return device.AudioEndpointVolume.MasterVolumeLevelScalar;
            }
        }

        public static void ShutdownWithTimer(int seconds)
        {
            if (seconds > 0)
            {

                Process.Start(new ProcessStartInfo
                {
                    FileName = "shutdown",
                    Arguments = $"/s /t {seconds}", // /s for shutdown, /t for time in seconds
                    CreateNoWindow = true,
                    UseShellExecute = false,
                });
            }
        }
        public static void CancelShutdown()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "shutdown",
                Arguments = "/a", // /a aborts a scheduled shutdown
                CreateNoWindow = true,
                UseShellExecute = false,
            });
        }

        public static void PrintScreen(string filePath = "screenshot.png")
        {
            try
            {
                // Define the bounds of the screen (full screen)
                Rectangle bounds = Screen.GetBounds(Point.Empty);

                // Create a bitmap object with the same size as the screen
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    // Create a graphics object to capture the screen
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        // Capture the screenshot and store it in the bitmap
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                    }

                    // Save the screenshot to the specified file path
                    bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                }

                Console.WriteLine($"Screenshot saved to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error capturing screenshot: {ex.Message}");
            }
        }

        public static string GetDesktopName()
        {
            return Environment.MachineName;
        }

    }
}

