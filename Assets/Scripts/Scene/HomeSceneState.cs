using DogFM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HomeSceneState : ISceneState
{
    public HomeSceneState(SceneStateController controller) : base(controller)
    {
        this.StateName = "Home";
    }

    public override void StateEnter()
    {
        base.StateEnter();
        
    }
}
