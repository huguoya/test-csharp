using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.WmOcr
{
    public class WmCode
    {
        /// <summary>
        /// 从文件中载入识别库文件
        /// </summary>
        /// <param name="FilePath">识别库文件所在全路径</param>
        /// <param name="Password">识别库调用密码</param>
        /// <returns>成功返回True，否则返回False</returns>
        [DllImport("WmCode.dll")]
        public static extern bool LoadWmFromFile(string FilePath, string Password);

        /// <summary>
        /// 从内存中载入识别库文件
        /// </summary>
        /// <param name="FileBuffer">一个记录了识别库文件的二进制数据的字节数组，或一块同样功能的内存区域。这里请提供数组第一个成员的地址，或内存区域的地址</param>
        /// <param name="FileBufLen">上述字节数组的数组成员数，或内存区域大小。</param>
        /// <param name="Password">识别库调用密码</param>
        /// <returns>成功返回True，否则返回False</returns>
        [DllImport("WmCode.dll")]
        public static extern bool LoadWmFromBuffer(byte[] FileBuffer, int FileBufLen, string Password);

        /// <summary>
        /// 识别一个图像文件
        /// </summary>
        /// <param name="FilePath">图像文件所在全路径</param>
        /// <param name="Vcode">返回的验证码字符串，使用该参数前需要将一个足够长的空白字符串赋值给它</param>
        /// <returns>成功返回True，否则返回False</returns>
        [DllImport("WmCode.dll")]
        public static extern bool GetImageFromFile(string FilePath, StringBuilder Vcode);

        /// <summary>
        /// 识别一个记录了图像文件的二进制数据的字节数组，或一块同样功能的内存区域
        /// </summary>
        /// <param name="FileBuffer">一个记录了图像文件的二进制数据的字节数组，或一块同样功能的内存区域。这里请提供数组第一个成员的地址，或内存区域的地址</param>
        /// <param name="ImgBufLen">上述字节数组的数组成员数，或内存区域大小。</param>
        /// <param name="Vcode">返回的验证码字符串，使用该参数前需要将一个足够长的空白字符串赋值给它。</param>
        /// <returns>成功返回True，否则返回False</returns>
        [DllImport("WmCode.dll")]
        public static extern bool GetImageFromBuffer(byte[] FileBuffer, int ImgBufLen, StringBuilder Vcode);

        /// <summary>
        /// 设定识别库选项
        /// </summary>
        /// <param name="OptionIndex">选项索引，取值范围1～10</param>
        /// <param name="OptionValue">选项数值</param>
        /// <returns>成功返回True，否则返回False</returns>
        [DllImport("WmCode.dll")]
        public static extern bool SetWmOption(int OptionIndex, int OptionValue);

        [DllImport("urlmon.dll", EntryPoint = "URLDownloadToFileA")]
        public static extern int URLDownloadToFile(int pCaller, string szURL, string szFileName, int dwReserved, int lpfnCB);
    }
}