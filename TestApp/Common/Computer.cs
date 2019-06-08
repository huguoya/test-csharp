using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Common
{
    /// <summary>
    /// Author ZZH
    /// 
    /// 获取计算机硬件信息
    /// 
    /// 可根据以下信息比对
    /// 
    /// CPU（CPU序列号）
    /// 网卡（Mac地址）
    /// 硬盘（硬盘ID）
    /// 系统（系统名称，系统型号）
    /// 内存（内存大小）
    /// </summary>
    public class Computer
    {
        /// <summary>
        /// CPU序列号
        /// </summary>
        public string CpuID;
        /// <summary>
        /// 网卡/Mac地址
        /// </summary>
        public string MacAddress;
        /// <summary>
        /// 硬盘ID
        /// </summary>
        public string DiskID;
        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress;
        /// <summary>
        /// 系统登录用户名
        /// </summary>
        public string LoginUserName;
        /// <summary>
        /// 系统名称
        /// </summary>
        public string ComputerName;
        /// <summary>
        /// 系统型号
        /// </summary>
        public string SystemType;
        /// <summary>
        /// 物理内存（单位b）
        /// </summary>
        public string TotalPhysicalMemory;
        private static Computer _instance;
        public static Computer Instance()
        {
            if (_instance == null)
                _instance = new Computer();
            return _instance;
        }
        protected Computer()
        {
            CpuID = GetCpuID();
            MacAddress = GetMacAddress();
            DiskID = GetDiskID();
            IpAddress = GetIPAddress();
            LoginUserName = GetUserName();
            SystemType = GetSystemType();
            TotalPhysicalMemory = GetTotalPhysicalMemory();
            ComputerName = GetComputerName();
        }
        string GetCpuID()
        {
            try
            {
                //获取CPU序列号代码
                string cpuInfo = "";//cpu序列号
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
                return cpuInfo;
            }
            catch
            {
                return "unknow";
            }
        }
        string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "unknow";
            }
        }
        string GetIPAddress()
        {
            try
            {
                //获取IP地址
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        //st=mo["IpAddress"].ToString();
                        System.Array ar;
                        ar = (System.Array)(mo.Properties["IpAddress"].Value);
                        st = ar.GetValue(0).ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
        }
        string GetDiskID()
        {
            try
            {
                //获取硬盘ID
                String HDid = "";
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    HDid = (string)mo.Properties["Model"].Value;
                }
                moc = null;
                mc = null;
                return HDid;
            }
            catch
            {
                return "unknow";
            }
        }

        /// <summary>
        /// 操作系统的登录用户名
        /// </summary>
        /// <returns></returns>
        string GetUserName()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    st = mo["UserName"].ToString();
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
        }


        /// <summary>
        /// PC类型
        /// </summary>
        /// <returns></returns>
        string GetSystemType()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    st = mo["SystemType"].ToString();
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }

        }

        /// <summary>
        /// 物理内存
        /// </summary>
        /// <returns></returns>
        string GetTotalPhysicalMemory()
        {
            try
            {

                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    st = mo["TotalPhysicalMemory"].ToString();
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
        }
        /// <summary>
        /// 系统名称
        /// </summary>
        /// <returns></returns>
        string GetComputerName()
        {
            try
            {
                return System.Environment.GetEnvironmentVariable("ComputerName");
            }
            catch
            {
                return "unknow";
            }
        }
    }
}
