using DogFM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class LoginScene : ISceneState
{
    public LoginScene(SceneStateController controller) : base(controller)
    {
        this.StateName = "Login";
    }

    public override void StateEnter()
    {
        base.StateEnter();
        UIMgr.Instance.Push<ViewLogin>(UILayer.Common, "UILogin");
    }
}
