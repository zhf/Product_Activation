using System;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

public class MachineID
{
    // Drive Total Size
    protected static long GetDriveTotalSize(string driveName)
    {
        foreach (DriveInfo drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady && drive.Name == driveName)
            {
                return drive.TotalSize;
            }
        }
        return 0;
    }

    // MAC Address of First/Default Network Interface
    public static string GetMACAddress()
    {
        return (from nic in NetworkInterface.GetAllNetworkInterfaces() where nic.OperationalStatus == OperationalStatus.Up select nic.GetPhysicalAddress().ToString()).FirstOrDefault();
    }

    public static string GetSystemDriveSize()
    {
        return GetDriveTotalSize("C:\\").ToString();
    }

    // Attribute information of current machine in a long string for hashing
    protected static string GetMachineAttrStr()
    {
        return GetMACAddress() + GetSystemDriveSize();
    }

    // Get a digest in form of 8-digital code for offline transferring easily (via telephone)
    public static byte[] ComputeEasyMachineID()
    {
        byte[] hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(GetMachineAttrStr()));
        byte[] ID = new byte[8];
        for (int i = 0; i < 8; i++)
        {
            ID[i] = (byte)(hash[i] % 10);
        }
        return ID;
    }
}

// Include information of this installation in addition to MachineID
public class InstallationID : MachineID
{
    public static string GetSelfSize()
    {
        FileInfo fi = new FileInfo(Process.GetCurrentProcess().MainModule.FileName);
        return fi.Length.ToString();
    }

    public const string LicenseTo = "Name of Your Client";
    public const string ValidThru = "2020-12-31";

    protected new static string GetMachineAttrStr()
    {
        return MachineID.GetMachineAttrStr() + GetSelfSize() + LicenseTo + ValidThru;
    }
}
