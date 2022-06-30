using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum NetStat
{
    NotConnect = 0,
    StartConnect = 1,
    Connecting = 2,
    Connected = 3,
    FailedConnect = 4,
    BreakConnect = 5,
    ReConnect = 6,
}

public static class NetStatHandler
{
    private static NetStat stat;

    public static NetStat Stat { get => stat; set => stat = value; }

    static NetStatHandler()
    {
        stat = NetStat.NotConnect;
    }

    public static void Update(NetStat stat)
    {
        NetStatHandler.stat = stat;
        switch (stat)
        {
            case NetStat.NotConnect:
                break;
            case NetStat.StartConnect:
                break;
            case NetStat.Connecting:
                break;
            case NetStat.Connected:
                break;
            case NetStat.BreakConnect:
                break;
        }
    }
}
