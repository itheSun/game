using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DogFM.Net
{
    /// <summary>
    /// TCP连接管理类
    /// 负责与服务器建立连接保存socket对象
    /// 异步接收消息，存入消息对象
    /// 提供发送消息接口
    /// </summary>
    public class UdpConnection
    {
        /// <summary>
        /// 套接字对象
        /// </summary>
        private Socket socket;
        /// <summary>
        /// 服务器ip、端口
        /// </summary>
        private IPEndPoint iPEndPoint;
        /// <summary>
        /// 接收缓冲区
        /// </summary>
        private byte[] buffer = new byte[Constant.MaxBufferSize];
        /// <summary>
        /// 已用缓冲区大小指针
        /// </summary>
        private int bufferCount = 0;
        private int packetLengthCount = 4;

    }
}
