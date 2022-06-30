using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace DogFM.Net
{
    /// <summary>
    /// TCP连接管理类
    /// 负责与服务器建立连接保存socket对象
    /// 异步接收消息，存入消息对象
    /// 提供发送消息接口
    /// </summary>
    public class TcpConnection
    {
        /// <summary>
        /// 套接字对象
        /// </summary>
        private Socket socket;
        /// <summary>
        /// 服务器ip、端口
        /// </summary>
        private IPEndPoint remoteEndPoint;
        /// <summary>
        /// 接收缓冲区
        /// </summary>
        private byte[] buffer = new byte[Constant.MaxBufferSize];
        /// <summary>
        /// 已用缓冲区大小指针
        /// </summary>
        private int bufferCount = 0;
        private int packetLengthCount = 4;

        /// <summary>
        /// 消息队列
        /// </summary>
        private Queue<KeyValuePair<ProtocolDefine, IMessage>> receivedQueue = new Queue<KeyValuePair<ProtocolDefine, IMessage>>();

        public bool IsConnected { get => socket.Connected; }
        public Queue<KeyValuePair<ProtocolDefine, IMessage>> ReceivedQueue { get => receivedQueue; }

        public TcpConnection(string ip, int port)
        {
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        public static IList PortIsUsed()

        {

            //获取本地计算机的网络连接和通信统计数据的信息            

            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            //返回本地计算机上的所有Tcp监听程序            

            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();

            //返回本地计算机上的所有UDP监听程序            

            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();

            //返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。            

            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            IList allPorts = new ArrayList();

            foreach (IPEndPoint ep in ipsTCP)

            {

                allPorts.Add(ep.Port);

            }

            foreach (IPEndPoint ep in ipsUDP)

            {

                allPorts.Add(ep.Port);

            }

            foreach (TcpConnectionInformation conn in tcpConnInfoArray)

            {

                allPorts.Add(conn.LocalEndPoint.Port);

            }

            return allPorts;

        }

        private int GetRandomPort()
        {
            IList HasUsedPort = PortIsUsed();
            int port = 0;
            bool IsRandomOk = true;
            Random random = new Random((int)DateTime.Now.Ticks);
            while (IsRandomOk)
            {
                port = random.Next(1024, 65535);
                IsRandomOk = HasUsedPort.Contains(port);
            }
            return port;
        }

        public void Connect()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, GetRandomPort()));
            socket.BeginConnect(remoteEndPoint, OnConnected, socket);
            socket.BeginReceive(buffer, 0, Constant.MaxBufferSize, SocketFlags.None, OnReceived, buffer);
        }

        private void OnConnected(IAsyncResult ar)
        {
            try
            {
                socket.EndConnect(ar);
                if (socket.Connected)
                {
                    Bug.Log("端口{0}连接成功!", socket.LocalEndPoint);
                }
                else
                {
                    Bug.Log("连接失败！!");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void OnReceived(IAsyncResult ar)
        {
            try
            {
                int size = socket.EndReceive(ar);
                bufferCount += size;
                Bug.Log(String.Format("收到包长度：{0}", bufferCount));
                PraseData();

                socket.BeginReceive(buffer, 0, Constant.MaxBufferSize, SocketFlags.None, OnReceived, buffer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void PraseData()
        {
            // 当前缓冲区大小小于包大小位大小
            if (bufferCount < packetLengthCount)
            {
                return;
            }
            // 读取包大小
            byte[] packetLengthBuf = new byte[packetLengthCount];
            Array.Copy(buffer, packetLengthBuf, packetLengthCount);
            int packetLength = BitConverter.ToInt32(packetLengthBuf, 0);
            Bug.Log(string.Format("消息长度：{0}", packetLength));
            // 当前包还未接收完
            if (bufferCount - 4 < packetLength)
            {
                return;
            }
            // 解析包
            KeyValuePair<ProtocolDefine, IMessage> msg = PacketSerializer.Decode(buffer, packetLengthCount, packetLength);
            // 消息入队列
            receivedQueue.Enqueue(msg);
            // 更新缓冲区
            bufferCount -= packetLength + packetLengthCount;
            Array.Copy(buffer, packetLengthCount + packetLength, buffer, 0, bufferCount);
            if (bufferCount > 0)
            {
                PraseData();
            }
        }

        public void Send(byte[] buffer)
        {
            if (IsConnected)
            {
                socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, OnSended, buffer);
            }
            else
            {
                Bug.Log("网络未连接");
            }
        }

        private void OnSended(IAsyncResult ar)
        {
            try
            {
                int count = socket.EndSend(ar);
                Bug.Log("向服务器成功发送大小为{0}的数据", count);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Close()
        {
            if (socket == null)
                return;
            socket.Close();
        }
    }
}
