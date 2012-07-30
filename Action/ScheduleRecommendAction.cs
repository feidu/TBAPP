using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PersistenceLayer;
using Entity;

namespace Action
{
    public class ScheduleRecommendAction
    {
        UserAction userAction=new UserAction();
        public void EnQueueByScheduleRelist(IList<tb_ScheduleRecommendQueueEntity> list)
        {
            Transaction t = new Transaction();
            foreach (tb_ScheduleRecommendQueueEntity srqe in list)
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
            RetrieveCriteria rc = new RetrieveCriteria(typeof(tb_ScheduleRecommendQueueEntity));
            Condition c = rc.GetNewCondition();
            c.AddEqualTo(tb_ScheduleRecommendQueueEntity.__STATE, false);
            c.AddLessThanOrEqualTo(tb_ScheduleRecommendQueueEntity.__SCHEDULE, DateTime.Now);

            EntityContainer ec=rc.AsEntityContainer();

            IList<ScheduleEntity> list = new List<ScheduleEntity>();
            if (ec.Count > 0)
            {
                foreach (EntityObject srq in ec)
                {
                    ScheduleEntity sre = new ScheduleEntity();
                    sre.Id = ((tb_ScheduleRecommendQueueEntity)srq).id;
                    sre.Nick = userAction.GetUserNick(((tb_ScheduleRecommendQueueEntity)srq).user_id);
                    sre.Num_iid = ((tb_ScheduleRecommendQueueEntity)srq).num_iid;
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
            tb_ScheduleRecommendQueueEntity srqe = new tb_ScheduleRecommendQueueEntity();
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

        public void ResultWrite(tb_RecommendResultEntity rre)
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
