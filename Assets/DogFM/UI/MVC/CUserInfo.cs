using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CUserInfo
{
    public CUserInfo()
    {
        MUserInfo.Instance.userName.OnValueChanged += (old, value) => { UIMgr.Instance.GetView<VUserInfo>("VUserInfo").OnNameUpdate(value); };

        UIMgr.Instance.GetView<VUserInfo>("VUserInfo").OnAlterName += (name) =>
        {
            MUserInfo.Instance.userName.Value = name;
        };
    }
}
