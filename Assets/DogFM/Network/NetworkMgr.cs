using DogFM;
using DogFM.Net;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DogFM
{
    /// <summary>
    /// 网络管理器
    /// </summary>
    public partial class NetworkMgr : DntdMonoSingleton<NetworkMgr>
    {
        /// <summary>
        /// tcp连接
        /// </summary>
        private TcpConnection tcp;

        /// <summary>
        /// udp连接
        /// </summary>
        private UdpConnection udp;

        /// <summary>
        /// http连接
        /// </summary>
        private HttpController http;

        /// <summary>
        /// 服务器应答回调
        /// </summary>
        private Dictionary<ProtocolDefine, Action<IMessage>> responseMap = new Dictionary<ProtocolDefine, Action<IMessage>>();

        /// <summary>
        /// 本地玩家
        /// </summary>
        private string localPlayerId;

        /// <summary>
        /// 网络玩家列表
        /// </summary>
        private Dictionary<string, NetworkPlayer> networkPlayerMap = new Dictionary<string, NetworkPlayer>();

        public void AddResponseListener(ProtocolDefine protocolDefine, Action<IMessage> action)
        {
            if (this.responseMap == null)
                this.responseMap = new Dictionary<ProtocolDefine, Action<IMessage>>();
            this.responseMap[protocolDefine] = action;
        }

        public void Init()
        {
            this.localPlayerId = "127.0.0.1";
            ConnectToServer();
        }

        public void ConnectToServer()
        {
            tcp = new TcpConnection(Constant.Server_IPAddress, Constant.Server_Port);
            tcp.Connect();
            MonoLoop.Instance.AddUpdateListener(OnUpdate);
        }

        private void OnUpdate()
        {
            if (tcp == null)
                return;
            if (tcp.IsConnected)
            {
                if (tcp.ReceivedQueue.Count > 0)
                {
                    KeyValuePair<ProtocolDefine, IMessage> msg;
                    lock (tcp.ReceivedQueue)
                    {
                        msg = tcp.ReceivedQueue.Dequeue();
                    }
                    DispenseMsg(msg.Key, msg.Value);
                }
            }
        }

        /// <summary>
        /// 分发消息
        /// </summary>
        /// <param name="message"></param>
        private void DispenseMsg(ProtocolDefine protocol, IMessage message)
        {
            if (!responseMap.ContainsKey(protocol))
            {
                throw new Exception(string.Format("不存在{}协议的回调", protocol));
            }
            responseMap[protocol].Invoke(message);
        }

        /// <summary>
        /// 打包协议
        /// 添加包长度、协议号
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private byte[] Pack(ProtocolDefine protocol, IMessage message)
        {
            List<byte> buffer = new List<byte>();
            byte[] id = BitConverter.GetBytes((int)protocol);
            byte[] proto = message.ToByteArray();
            byte[] length = BitConverter.GetBytes(id.Length + proto.Length);
            buffer.AddRange(length);
            buffer.AddRange(id);
            buffer.AddRange(proto);
            return buffer.ToArray();
        }
    }
}