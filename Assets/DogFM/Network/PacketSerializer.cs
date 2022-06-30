using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace DogFM.Net
{
    /// <summary>
    /// ��Э�������
    /// </summary>
    public class PacketSerializer
    {
        /// <summary>
        /// ����Э��ί��
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public delegate IMessage ParseProtocol(byte[] buffer);

        /// <summary>
        /// Э���ֵ�
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
        /// ����Э��
        /// </summary>
        /// <param name="buffer">������</param>
        /// <param name="start">Э�����ʼ�±�</param>
        /// <param name="len">Э�������</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static KeyValuePair<ProtocolDefine, IMessage> Decode(byte[] buffer, int start, int len)
        {
            // ����Э���
            byte[] id = new byte[PROTOCOL_SIZE];
            Array.Copy(buffer, start, id, 0, PROTOCOL_SIZE);
            int protocolID = BitConverter.ToInt32(id, 0);
            ProtocolDefine protocol = (ProtocolDefine)protocolID;
            Bug.Log("���յ�{0}������Ϣ", (ProtocolDefine)protocolID);
            // ���Э����Ƿ����
            if (!protocolMap.ContainsKey(protocol))
            {
                throw new Exception(string.Format("Protocol ID {0} is not existed in Protocol Map", protocolID));
            }
            // ��ȡЭ��
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