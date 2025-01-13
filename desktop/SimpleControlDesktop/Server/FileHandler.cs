using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleControlDesktop.Server
{
    public static class FileHandler
    {
        public static async Task HandleFileTransferAsync(JsonElement value)
        {
            try
            {
                // Extract "FileName" and "Payload" from the JSON Value
                string fileName = value.GetProperty("FileName").GetString();
                byte[] payload = Convert.FromBase64String(value.GetProperty("Payload").GetString());

                if (string.IsNullOrEmpty(fileName))
                {
                    throw new InvalidOperationException("FileName is missing or empty.");
                }

                // Get the Downloads folder path
                string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                Directory.CreateDirectory(downloadsPath);

                // Save the file
                string filePath = Path.Combine(downloadsPath, fileName);
                await File.WriteAllBytesAsync(filePath, payload);

                Console.WriteLine($"File saved at: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling file transfer: {ex.Message}");
                throw;
            }
        }
    }
}
