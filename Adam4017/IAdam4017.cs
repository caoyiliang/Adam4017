using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adam4017
{
    public interface IAdam4017
    {
        /// <summary>
        /// 打开串口
        /// </summary>
        Task OpenAsync();

        /// <summary>
        /// 关闭串口
        /// </summary>
        Task CloseAsync();

        /// <summary>
        /// 对端掉线
        /// </summary>
        event DisconnectEventHandler? OnDisconnect;

        /// <summary>
        /// 对端连接成功
        /// </summary>
        event ConnectEventHandler? OnConnect;

        /// <summary>
        /// 读信号量
        /// </summary>
        /// <param name="timeOut">超时时间(-1则使用构造传入超时)</param>
        /// <returns>信号量值</returns>
        Task<List<decimal>> ReadSignalValueAsync(string address = "01", int timeOut = -1);
    }
}
