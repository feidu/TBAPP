using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class TradeRateMsgEntity
    {
        private string nick;

        public string Nick
        {
            get { return nick; }
            set { nick = value; }
        }

        private long tid;

        public long Tid
        {
            get { return tid; }
            set { tid = value; }
        }

        private Util.Enum.AutoTraderateType ast;

        public Util.Enum.AutoTraderateType Ast
        {
            get { return ast; }
            set { ast = value; }
        }
    }
}
