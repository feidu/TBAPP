using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PersistenceLayer;
using Entity;

namespace Action
{
    public class AutoRelistAction
    {
        #region old
        /*
        public IList<tb_MessageQueueEntity> GetAutoRelistMsg()
        {
            RetrieveCriteria rc = new RetrieveCriteria(typeof(tb_MessageQueueEntity));
            Condition c = rc.GetNewCondition();
            c.AddEqualTo(tb_MessageQueueEntity.__TOPIC, "item");
            c.AddEqualTo(tb_MessageQueueEntity.__STATE, false);
            c.AddEqualTo(tb_MessageQueueEntity.__STATUS, "ItemDownshelf");
            EntityContainer ec =rc.AsEntityContainer();
            IList<tb_MessageQueueEntity> list = new List<tb_MessageQueueEntity>();

            foreach (tb_MessageQueueEntity mqe in ec)
            {
                //检测用户开关
                DataTable dt = CheckUserAutoSwitch(mqe.nick, Util.Enum.AutoSwitchType.AutoRelist.ToString());
                if (dt.Rows.Count > 0)
                {
                    list.Add(mqe);
                }   
            }
            return list;//返回需要执行上架的任务
        }

        public DataTable CheckUserAutoSwitch(string nick, string switchtype)
        {
            Query qUS = new Query(typeof(tb_User_SwitchEntity));
            qUS.AddAttribute(tb_User_SwitchEntity.__USER_ID);
            qUS.AddAttribute(tb_User_SwitchEntity.__SWITCH_ID);
            Condition cUS = qUS.GetQueryCondition();
            cUS.AddEqualTo(tb_User_SwitchEntity.__STATE, true);

            Query qS = new Query(typeof(tb_AutoSwitchEntity));
            Condition cS = qS.GetQueryCondition();
            cS.AddEqualTo(tb_AutoSwitchEntity.__SWITCH, switchtype);

            Query qU = new Query(typeof(tb_UserEntity));
            Condition cU = qU.GetQueryCondition();
            cU.AddEqualTo(tb_UserEntity.__NICK, nick);
            qUS.AddJoinQuery(tb_User_SwitchEntity.__SWITCH_ID, qS, tb_AutoSwitchEntity.__ID);
            qUS.AddJoinQuery(tb_User_SwitchEntity.__USER_ID, qU, tb_UserEntity.__ID);
            DataTable dt = qUS.Execute();
            return dt;
        }
        */
        #endregion

        public void EndRelist(tb_MessageQueueEntity mqe,tb_RelistReslutEntity rre)
        {
            Transaction t = new Transaction();
            UpdateCriteria uc = new UpdateCriteria(typeof(tb_MessageQueueEntity));
            Condition c = uc.GetNewCondition();
            c.AddEqualTo(tb_MessageQueueEntity.__ID,mqe.id);
            uc.AddAttributeForUpdate(tb_MessageQueueEntity.__STATE,true);
            t.AddUpdateCriteria(uc);
            t.AddSaveObject(rre);
            try
            {
                t.Process();
            }
            catch (PlException plex)
            {
            }
        }

        public void ResultWrite(tb_RelistReslutEntity rre)
        {
            Transaction t = new Transaction();
            t.AddSaveObject(rre);
            try
            {
                t.Process();
            }
            catch (PlException plex)
            {
            }
        }
    }
}
