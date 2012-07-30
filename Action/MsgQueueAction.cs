using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PersistenceLayer;
using Entity;

namespace Action
{
    public class MsgQueueAction
    {
        public void MsgEnQ(tb_MessageQueueEntity mqe)
        {
            Transaction t = new Transaction();
            t.AddSaveObject(mqe);
            try
            {
                t.Process();
            }
            catch(PlException plex)
            {
                //错误日志plex.Message;
            }
        }
    }
}
