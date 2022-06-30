using DogFM;
using DogFM.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ViewModelChat : BaseViewModel
{
    public BindableProperty<string> worldRecord = new BindableProperty<string>();

    public ViewModelChat()
    {
        EventCenter.AddListener<Session>(EventDefine.ReceivedChatMessage, OnReceivedChatMessage);
    }

    public void Update(CharRecordModel charRecord)
    {
        this.worldRecord.Value = charRecord.worldRecord;
    }

    private void OnReceivedChatMessage(Session session)
    {
        string message = session.Id + ": " + session.Message;
        worldRecord.Value = message + "\n";
    }
}
