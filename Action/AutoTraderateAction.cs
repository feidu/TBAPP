using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PersistenceLayer;
using Entity;

namespace Action
{
    public class AutoTraderateAction
    {
        public string GetTraderContext(string nick)
        {
            if (nick == "")
            {
                return "";
            }
            Query qU=new Query(typeof(tb_UserEntity));
            Condition c=qU.GetQueryCondition();
            c.AddEqualTo(tb_UserEntity.__NICK,nick);

            Query qT=new Query(typeof(tb_TraderateContextEntity));
            qT.AddAttribute(tb_TraderateContextEntity.__CONTEXT);

            qT.AddJoinQuery(tb_TraderateContextEntity.__USER_ID,qU,tb_UserEntity.__ID);

            object o = qT.ExecuteScalar();
            if (o != null)
            {
                return o.ToString();
            }
            else
            {
                return "";
            }
        }

        public void ResultWrite(tb_TraderateResultEntity tce)
        {
            Transaction t = new Transaction();
            t.AddSaveObject(tce);
            try
            {
                t.Process();
            }
            catch (PlException plex)
            { }
        }

        public DataTable GetContext(string nick)
        {
            DataTable dt = new DataTable();
            if (nick == "")
            {
                return dt;
            }
            Query qU = new Query(typeof(tb_UserEntity));
            Condition cU = qU.GetQueryCondition();
            cU.AddEqualTo(tb_UserEntity.__NICK,nick);

            Query qT = new Query(typeof(tb_TraderateContextEntity));
            qT.AddAttribute(AttributeType.All);
            qT.AddJoinQuery(tb_TraderateContextEntity.__USER_ID, qU, tb_UserEntity.__ID);
            dt=qT.Execute();
            return dt;
        }

        public void UpdateContext(int id, string context)
        {
            tb_TraderateContextEntity tce = new tb_TraderateContextEntity();
            tce.id = id;
            tce.Retrieve();
            if (tce.IsPersistent)
            {
                tce.Context = context;
                try
                {
                    tce.Save();
                }
                catch (PlException plex)
                { }
            }
        }

        public void DeleteContext(int id)
        {
            tb_TraderateContextEntity tce = new tb_TraderateContextEntity();
            tce.id = id;
            tce.Retrieve();
            if (tce.IsPersistent)
            {
                try
                {
                    tce.Delete();
                }
                catch (PlException plex)
                { }
            }
        }

        public void SetUseContext(int id,string nick)
        {
            string strSql = "update dbo.tb_TraderateContext set [state] ='1' where [id]=" + id
                                + " and [user_id]=(select [id] from dbo.tb_User where nick='" + nick.Trim() + "');"
                                + "update dbo.tb_TraderateContext set [state] ='0' where [id]<>" + id
                                + " and [user_id]=(select [id] from dbo.tb_User where nick='" + nick.Trim() + "');";
            PersistenceLayer.Query.ProcessSqlNonQuery(strSql,Util.DB.DbName);
        }

        public void AddContext(string context,string nick)
        {
            string strSql = "insert into dbo.tb_TraderateContext([Context],[state],[user_id]) "
                                + " values ('" + context .Trim()+ "','0',(select [id] from dbo.tb_User where nick='" + nick.Trim() + "'))";
            PersistenceLayer.Query.ProcessSqlNonQuery(strSql, Util.DB.DbName);
        }

        public int GetContextTotalByUser(string nick)
        {
            Query qU = new Query(typeof(tb_UserEntity));
            Condition cU = qU.GetQueryCondition();
            cU.AddEqualTo(tb_UserEntity.__NICK, nick);

            Query qT = new Query(typeof(tb_TraderateContextEntity));
            qT.SelectCount(tb_TraderateContextEntity.__ID,"total");
            qT.AddJoinQuery(tb_TraderateContextEntity.__USER_ID, qU, tb_UserEntity.__ID);
            object o=qT.ExecuteScalar();

            return (int)o;
        }
    }
}
