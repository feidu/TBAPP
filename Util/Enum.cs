using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public class Enum
    {
        public enum UserSysLevel
        {
            Experience=0,
            Member=1
        }

        public enum AutoSwitchType
        {
            AutoRelist,
            AutoTraderate,
            AutoRecommend
        }

        public enum AutoTraderateType
        {
            AutoTraderateBuyerPay,
            AutoTraderateBuyerRated
        }

        public enum AutoRecommendType
        {
            DelistFirst
        }

        public enum ObserverType
        {
            AutoRelistTasker,
            AutoTraderateBuyerPayTasker,
            AutoTraderateBuyerRatedTasker,
            AutoRecommendTasker
        }
    }
}
