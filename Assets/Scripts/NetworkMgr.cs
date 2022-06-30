using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogFM
{
    public partial class NetworkMgr
    {
        private void Awake()
        {
            AddResponseListener(ProtocolDefine.SendChatMessage, (message) =>
            {
                Session session = (Session)message;
                EventCenter.Call<Session>(EventDefine.ReceivedChatMessage, session);
            });
        }

        public void OnLogin(string userName, string password)
        {

        }

        public void OnSendChatMessage(string message)
        {
            Session session = new Session();
            session.Id = this.localPlayerId;
            session.Message = message;
            byte[] data = Pack(ProtocolDefine.SendChatMessage, session);
            this.tcp.Send(data);
        }
    }
}
