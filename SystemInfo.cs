using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using System.IO;
using System.Management;

namespace Nockdev.Utilities
{
    public class SystemInfo
    {
        public static string GetSystemInfo()
        {
            ManagementObjectSearcher video = new ManagementObjectSearcher("select * from Win32_VideoController");
            StringWriter writer = new StringWriter();
            writer.WriteLine("------------------------ System Info ------------------------");
            ComputerInfo info = new ComputerInfo();
            writer.WriteLine("Operating System: " + info.OSFullName);
            writer.WriteLine("Operating System Platform: " + info.OSFullName);
            writer.WriteLine("Operating System Version: " + info.OSVersion);
            writer.WriteLine("Total Physical Memory: " + (info.TotalPhysicalMemory / 1048576).ToString() + " MB");
            writer.WriteLine("Available Physical Memory: " + (info.AvailablePhysicalMemory / 1048576).ToString() + " MB");
            writer.WriteLine("Total Virtual Memory: " + (info.TotalVirtualMemory / 1048576).ToString() + " MB");
            writer.WriteLine("Available Virtual Memory: " + (info.AvailableVirtualMemory / 1048576).ToString() + " MB");

            RegistryKey processorName = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);

            if (processorName != null)
            {
                if (processorName.GetValue("ProcessorNameString") != null)
                {
                    writer.WriteLine("Processor: " + processorName.GetValue("ProcessorNameString"));
                }
            }

            foreach (ManagementObject obj in video.Get())
            {
                writer.WriteLine("GPU: " + obj["Name"]);
                writer.WriteLine("GPU RAM: " + obj["AdapterRAM"]);
                writer.WriteLine("GPU Driver Version: " + obj["DriverVersion"]);
                writer.WriteLine("Video Processor: " + obj["VideoProcessor"]);
                writer.WriteLine("Video Architecture: " + TranslateArchitecture((ushort)obj["VideoArchitecture"]));
                writer.WriteLine("Video MemoryType: " + MemoryType[(ushort)obj["VideoMemoryType"] - 1]);
            }

            string output = writer.ToString();
            writer.Close();
            return output;
        }

        private static string TranslateArchitecture(ushort id)
        {
            if (id == 160)
                return "PC-98";
            else
                return Architectures[id - 1];
        }

        private static string[] Architectures = new string[]
        {
            "Other",
            "Unknown",
            "CGA",
            "EGA",
            "VGA",
            "SVGA",
            "MDA",
            "HGC",
            "MCGA",
            "8514A",
            "XGA",
            "Linear Frame Buffer"
        };

        private static string[] MemoryType = new string[]
        {
            "Other",
            "Unknown",
            "VRAM",
            "DRAM",
            "SRAM",
            "WRAM",
            "EDO RAM",
            "Burst Synchronous DRAM",
            "Pipelined Burst SRAM",
            "CDRAM",
            "3DRAM",
            "SDRAM",
            "SGRAM"
        };
    }
}
