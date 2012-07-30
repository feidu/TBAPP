using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PersistenceLayer;
using Entity;

namespace Action
{
    public class SwitchAction
    {
        public bool GetSwitchState(string nick,string switchtype)
        {
            if (nick == null)
            {
                return false;
            }
            Query qUS = new Query(typeof(tb_User_SwitchEntity));
            qUS.AddAttribute(tb_User_SwitchEntity.__STATE);

            Query qS = new Query(typeof(tb_AutoSwitchEntity));
            Condition cS = qS.GetQueryCondition();
            cS.AddEqualTo(tb_AutoSwitchEntity.__SWITCH, switchtype);

            Query qU = new Query(typeof(tb_UserEntity));
            Condition cU = qU.GetQueryCondition();
            cU.AddEqualTo(tb_UserEntity.__NICK, nick);
            qUS.AddJoinQuery(tb_User_SwitchEntity.__SWITCH_ID, qS, tb_AutoSwitchEntity.__ID);
            qUS.AddJoinQuery(tb_User_SwitchEntity.__USER_ID, qU, tb_UserEntity.__ID);
            object state = qUS.ExecuteScalar();
            if (state == null)
            {
                return false;
            }
            return (bool)state;
        }

        public bool GetSwitchPropertyState(string nick, string switchpro)
        {
            Query qUS = new Query(typeof(tb_User_SwitchPropertyEntity));
            qUS.AddAttribute(tb_User_SwitchPropertyEntity.__STATE);

            Query qS = new Query(typeof(tb_SwitchPropertyEntity));
            Condition cS = qS.GetQueryCondition();
            cS.AddEqualTo(tb_SwitchPropertyEntity.__PRONAME, switchpro);

            Query qU = new Query(typeof(tb_UserEntity));
            Condition cU = qU.GetQueryCondition();
            cU.AddEqualTo(tb_UserEntity.__NICK, nick);
            qUS.AddJoinQuery(tb_User_SwitchPropertyEntity.__SWITCHPROERTY_ID, qS, tb_SwitchPropertyEntity.__ID);
            qUS.AddJoinQuery(tb_User_SwitchPropertyEntity.__USER_ID,qU,tb_UserEntity.__ID);
            object state = qUS.ExecuteScalar();
            if (state == null)
            {
                return false;
            }
            return (bool)state;
        }

        public void ChangSwitchState(string nick, string switchtype, bool state)
        {
            string onoff=string.Empty;
            if(state)
            {
                onoff="1";
            }else
            {
                onoff="0";
            }
            string strSql = "delete from dbo.tb_User_Switch where [user_id]=(select top 1 [id] from dbo.tb_User where nick='" + nick + "') "
                                + "and [switch_id]=(select top 1 [id] from dbo.tb_AutoSwitch where switch='" + switchtype + "');"
                                + "insert into dbo.tb_User_Switch([user_id],[switch_id],[state]) values("
                                + "(select top 1 [id] from dbo.tb_User where nick='" + nick + "'),"
                                + "(select top 1 [id] from dbo.tb_AutoSwitch where switch='" + switchtype + "')," + onoff + ");";
            PersistenceLayer.Query.ProcessSqlNonQuery(strSql, Util.DB.DbName);
        }

        public void SetSwitchProperty(string nick, string switchpro, string state)
        {
            string strSql = "delete from dbo.tb_User_SwitchProperty where [user_id]=(select top 1 [id] from dbo.tb_User where nick='" + nick + "') "
                                + "and [switchProerty_id]=(select top 1 [id] from dbo.tb_SwitchProperty where proName='" + switchpro + "');"
                                + "insert into dbo.tb_User_SwitchProperty([user_id],[switchProerty_id],[state]) values("
                                + "(select top 1 [id] from dbo.tb_User where nick='" + nick + "'),"
                                + "(select top 1 [id] from dbo.tb_SwitchProperty where proName='" + switchpro + "')," + state + ");";
            PersistenceLayer.Query.ProcessSqlNonQuery(strSql, Util.DB.DbName);
        }

        public void OffSwitchTraderate(string nick)
        {
            string strSql = "update dbo.tb_User_Switch set [state]='0' "
                + "  where [USER_ID] =(select [ID] from dbo.tb_User where nick='" + nick + "') "
                + " and [switch_id] in (select [id] from dbo.tb_AutoSwitch where [SWITCH]= 'AutoTraderateBuyerRated' or [SWITCH]='AutoTraderateBuyerPay')";
            PersistenceLayer.Query.ProcessSqlNonQuery(strSql, Util.DB.DbName);
        }
    }
}
