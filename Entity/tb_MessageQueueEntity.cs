//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.4971
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// -------------------------------------------------------------
// 
//             Powered By： SR3.1(SmartRobot For SmartPersistenceLayer 3.1) 听棠
//             Created By： John
//             Created Time： 2012/7/22 16:22:24
// 
// -------------------------------------------------------------
namespace Entity
{
    using System;
    using System.Collections;
    using System.Data;
    using PersistenceLayer;
    
    
    /// <summary>该类的摘要说明</summary>
    [Serializable()]
    public class tb_MessageQueueEntity : EntityObject
    {
        
        /// <summary>id</summary>
        public const string @__ID = "id";
        
        /// <summary>topic</summary>
        public const string @__TOPIC = "topic";
        
        /// <summary>status</summary>
        public const string @__STATUS = "status";
        
        /// <summary>nick</summary>
        public const string @__NICK = "nick";
        
        /// <summary>MsgJson</summary>
        public const string @__MSGJSON = "MsgJson";
        
        /// <summary>state</summary>
        public const string @__STATE = "state";
        
        private int m_id;
        
        private string m_topic;
        
        private string m_status;
        
        private string m_nick;
        
        private string m_MsgJson;
        
        private bool m_state;
        
        /// <summary>构造函数</summary>
        public tb_MessageQueueEntity()
        {
        }
        
        /// <summary>属性id </summary>
        public int id
        {
            get
            {
                return this.m_id;
            }
            set
            {
                this.m_id = value;
            }
        }
        
        /// <summary>属性topic </summary>
        public string topic
        {
            get
            {
                return this.m_topic;
            }
            set
            {
                this.m_topic = value;
            }
        }
        
        /// <summary>属性status </summary>
        public string status
        {
            get
            {
                return this.m_status;
            }
            set
            {
                this.m_status = value;
            }
        }
        
        /// <summary>属性nick </summary>
        public string nick
        {
            get
            {
                return this.m_nick;
            }
            set
            {
                this.m_nick = value;
            }
        }
        
        /// <summary>属性MsgJson </summary>
        public string MsgJson
        {
            get
            {
                return this.m_MsgJson;
            }
            set
            {
                this.m_MsgJson = value;
            }
        }
        
        /// <summary>属性state </summary>
        public bool state
        {
            get
            {
                return this.m_state;
            }
            set
            {
                this.m_state = value;
            }
        }
    }
    
    /// tb_MessageQueueEntity执行类
    public abstract class tb_MessageQueueEntityAction
    {
        
        private tb_MessageQueueEntityAction()
        {
        }
        
        public static void Save(tb_MessageQueueEntity obj)
        {
            if (obj!=null)
            {
                obj.Save();
            }
        }
        
        /// <summary>根据主键获取一个实体</summary>
        public static tb_MessageQueueEntity RetrieveAtb_MessageQueueEntity(int id)
        {
            tb_MessageQueueEntity obj=new tb_MessageQueueEntity();
            obj.id=id;
            obj.Retrieve();
            if (obj.IsPersistent)
            {
                return obj;
            }
            else
            {
                return null;
            }
        }
        
        /// <summary>获取所有实体(EntityContainer)</summary>
        public static EntityContainer Retrievetb_MessageQueueEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(tb_MessageQueueEntity));
            return rc.AsEntityContainer();
        }
        
        /// <summary>获取所有实体(EntityContainer)</summary>
        public static DataTable Gettb_MessageQueueEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(tb_MessageQueueEntity));
            return rc.AsDataTable();
        }
    }
}
