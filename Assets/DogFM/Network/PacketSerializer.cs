using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace DogFM.Net
{
    /// <summary>
    /// 包协议解析类
    /// </summary>
    public class PacketSerializer
    {
        /// <summary>
        /// 解析协议委托
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public delegate IMessage ParseProtocol(byte[] buffer);

        /// <summary>
        /// 协议字典
        /// </summary>
        private static Dictionary<ProtocolDefine, ParseProtocol> protocolMap = new Dictionary<ProtocolDefine, ParseProtocol>();
        private const int PROTOCOL_SIZE = 4;

        static PacketSerializer()
        {
            RegisterProtocol(ProtocolDefine.SendChatMessage, (buffer) =>
            {
                return PraseIMessage<Session>(buffer);
            });
        }

        public static void RegisterProtocol(ProtocolDefine protocolDefine, ParseProtocol praseProtocol)
        {
            if (protocolMap == null)
            {
                protocolMap = new Dictionary<ProtocolDefine, ParseProtocol>();
            }
            if (protocolMap.ContainsKey(protocolDefine))
            {
                protocolMap[protocolDefine] = praseProtocol;
            }
            else
            {
                protocolMap.Add(protocolDefine, praseProtocol);
            }
        }

        /// <summary>
        /// 解析协议
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="start">协议包起始下标</param>
        /// <param name="len">协议包长度</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static KeyValuePair<ProtocolDefine, IMessage> Decode(byte[] buffer, int start, int len)
        {
            // 解析协议号
            byte[] id = new byte[PROTOCOL_SIZE];
            Array.Copy(buffer, start, id, 0, PROTOCOL_SIZE);
            int protocolID = BitConverter.ToInt32(id, 0);
            ProtocolDefine protocol = (ProtocolDefine)protocolID;
            Bug.Log("接收到{0}类型消息", (ProtocolDefine)protocolID);
            // 检查协议号是否存在
            if (!protocolMap.ContainsKey(protocol))
            {
                throw new Exception(string.Format("Protocol ID {0} is not existed in Protocol Map", protocolID));
            }
            // 读取协议
            byte[] protocolBuf = new byte[len - PROTOCOL_SIZE];
            Array.Copy(buffer, start + PROTOCOL_SIZE, protocolBuf, 0, len - PROTOCOL_SIZE);
            IMessage message = protocolMap[protocol].Invoke(protocolBuf);

            return new KeyValuePair<ProtocolDefine, IMessage>((ProtocolDefine)protocolID, message);
        }

        private static T PraseIMessage<T>(byte[] buffer) where T : IMessage, new()
        {
            IMessage loginResponse = new T();
            T response = (T)loginResponse.Descriptor.Parser.ParseFrom(buffer);
            return response;
        }
    }
}