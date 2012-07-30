using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PersistenceLayer;
using Entity;

namespace Action
{
    public class ScheduleRelistAction
    {
        UserAction userAction = new UserAction();
        public void EnQueueByScheduleRelist(IList<tb_ScheduleRelistQueueEntity> list)
        {
            Transaction t = new Transaction();
            foreach (tb_ScheduleRelistQueueEntity srqe in list)
            {
                t.AddSaveObject(srqe);
            }
            try
            {
                t.Process();
            }
            catch (PlException plex)
            {
                //执行失败。
            }
        }

        public IList<ScheduleEntity> GetCanExecute()
        {
            RetrieveCriteria rc = new RetrieveCriteria(typeof(tb_ScheduleRelistQueueEntity));
            Condition c = rc.GetNewCondition();
            c.AddEqualTo(tb_ScheduleRelistQueueEntity.__STATE, false);
            c.AddLessThanOrEqualTo(tb_ScheduleRelistQueueEntity.__SCHEDULE, DateTime.Now);

            EntityContainer ec=rc.AsEntityContainer();

            IList<ScheduleEntity> list = new List<ScheduleEntity>();
            if (ec.Count > 0)
            {
                foreach (EntityObject srq in ec)
                {
                    ScheduleEntity sre = new ScheduleEntity();
                    sre.Id = ((tb_ScheduleRelistQueueEntity)srq).id;
                    sre.Nick = userAction.GetUserNick(((tb_ScheduleRelistQueueEntity)srq).user_id);
                    sre.Num_iid = ((tb_ScheduleRelistQueueEntity)srq).num_iid;
                    if (sre.Nick != "")
                    {
                        list.Add(sre);
                    }
                }
            }
            return list;
        }

        public void DeQueue(int id)
        {
            tb_ScheduleRelistQueueEntity srqe = new tb_ScheduleRelistQueueEntity();
            srqe.id = id;
            try
            {
                srqe.Retrieve();
                if (srqe.IsPersistent)
                {
                    srqe.state = true;
                    srqe.Save();
                }
            }catch(PlException plex)
            {
                //获取失败了，主键有误
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
