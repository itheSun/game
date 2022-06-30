using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MUserInfo : Singleton<MUserInfo>
{
    public BindableProperty<string> userName = new BindableProperty<string>();

    public void Update()
    {

    }
}
