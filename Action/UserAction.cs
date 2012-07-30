using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PersistenceLayer;
using Entity;

namespace Action
{
    public class UserAction
    {
        public string GetSessionKey(string nick)
        {
            RetrieveCriteria rc = new RetrieveCriteria(typeof(tb_UserEntity));
            Condition c = rc.GetNewCondition();
            c.AddEqualTo(tb_UserEntity.__NICK, nick);
            tb_UserEntity user = (tb_UserEntity)rc.AsEntity();
            return user.SessionKey;
        }

        public int GetUserIdByNick(string nick)
        {
            RetrieveCriteria rc = new RetrieveCriteria(typeof(tb_UserEntity));
            Condition c = rc.GetNewCondition();
            c.AddEqualTo(tb_UserEntity.__NICK,nick);
            tb_UserEntity ue = (tb_UserEntity)rc.AsEntity();
            if (ue != null)
            {
                return ue.id;
            }
            else
            {
                return 0;
            }
        }

        public string GetUserNick(int id)
        {
            tb_UserEntity ue = new tb_UserEntity();
            ue.id = id;
            try
            {
                ue.Retrieve();
            }
            catch (PlException plex)
            {
                //获取失败了，主键有误
            }
            if (ue.IsPersistent)
            {
                return ue.nick;
            }
            else
            {
                return "";
            }
        }

        public bool CheckIsOverTime(string nick)
        {
            RetrieveCriteria rc = new RetrieveCriteria(typeof(tb_UserEntity));
            Condition c = rc.GetNewCondition();
            c.AddEqualTo(tb_UserEntity.__NICK,nick);

            tb_UserEntity ue=(tb_UserEntity)rc.AsEntity();
            //体验版或者过期
            if (ue.syslevel == ((int)Util.Enum.UserSysLevel.Experience).ToString() || ue.authEndTime < DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
