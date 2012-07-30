using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PersistenceLayer;
using Entity;

namespace Action
{
    public class LoginAction
    {
        public void AddUserOrUpdateUser(tb_UserEntity user)
        {
            Transaction t = new Transaction();

            RetrieveCriteria rc = new RetrieveCriteria(typeof(tb_UserEntity));
            rc.AddSelect(tb_UserEntity.__ID);
            rc.Top = 1;
            Condition c = rc.GetNewCondition();
            c.AddEqualTo(tb_UserEntity.__NICK,user.nick);

            DataTable dt = t.DoRetrieveCriteria(rc);
            if (dt.Rows.Count > 0)
            {
                //存在 修改
                UpdateCriteria uc = new UpdateCriteria(typeof(tb_UserEntity));
                Condition cuc = uc.GetNewCondition();
                cuc.AddEqualTo(tb_UserEntity.__NICK, user.nick);
                uc.AddAttributeForUpdate(tb_UserEntity.__EMAIL, user.email);
                uc.AddAttributeForUpdate(tb_UserEntity.__TYPE, user.type);
                uc.AddAttributeForUpdate(tb_UserEntity.__SESSIONKEY,user.SessionKey);
                uc.AddAttributeForUpdate(tb_UserEntity.__AUTHENDTIME, user.authEndTime);
                uc.AddAttributeForUpdate(tb_UserEntity.__SYSLEVEL,user.syslevel);
                t.DoUpdateCriteria(uc);
            }
            else
            {
                //不存在 新增
                tb_UserEntity userE = new tb_UserEntity();
                userE.nick = user.nick;
                userE.email = user.email;
                userE.type = user.type;
                userE.syslevel = ((int)Util.Enum.UserSysLevel.Experience).ToString();
                userE.authEndTime = user.authEndTime;
                userE.SessionKey = user.SessionKey;
                t.DoSaveObject(userE);
            }
            t.Commit();
        }

        public tb_UserEntity GetUserByNick(string nick)
        {
            RetrieveCriteria rc = new RetrieveCriteria(typeof(tb_UserEntity));
            Condition c = rc.GetNewCondition();
            c.AddEqualTo(tb_UserEntity.__NICK, nick);
            return (tb_UserEntity)rc.AsEntity();
        }
    }
}
