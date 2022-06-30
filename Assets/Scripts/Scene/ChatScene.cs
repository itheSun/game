using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ChatScene : ISceneState
{
    public ChatScene(SceneStateController controller) : base(controller)
    {
        this.StateName = "Chat";
    }

    public override void StateEnter()
    {
        base.StateEnter();

        ViewChat viewChat = UIMgr.Instance.Push<ViewChat>(UILayer.Common, "ViewChat");
        viewChat.BindingContext = new ViewModelChat();
    }
}
