﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MVCMgr : Singleton<MVCMgr>
{
    public void Init()
    {
        CUserInfo cUserInfo = new CUserInfo();
    }
}
