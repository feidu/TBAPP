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
    public class tb_TraderateContextEntity : EntityObject
    {
        
        /// <summary>id</summary>
        public const string @__ID = "id";
        
        /// <summary>Context</summary>
        public const string @__CONTEXT = "Context";
        
        /// <summary>state</summary>
        public const string @__STATE = "state";
        
        /// <summary>user_id</summary>
        public const string @__USER_ID = "user_id";
        
        /// <summary>created</summary>
        public const string @__CREATED = "created";
        
        private int m_id;
        
        private string m_Context;
        
        private bool m_state;
        
        private int m_user_id;
        
        private System.DateTime m_created = DateTime.MinValue;
        
        /// <summary>构造函数</summary>
        public tb_TraderateContextEntity()
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
        
        /// <summary>属性Context </summary>
        public string Context
        {
            get
            {
                return this.m_Context;
            }
            set
            {
                this.m_Context = value;
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
        
        /// <summary>属性user_id </summary>
        public int user_id
        {
            get
            {
                return this.m_user_id;
            }
            set
            {
                this.m_user_id = value;
            }
        }
        
        /// <summary>属性created </summary>
        public System.DateTime created
        {
            get
            {
                return this.m_created;
            }
            set
            {
                this.m_created = value;
            }
        }
    }
    
    /// tb_TraderateContextEntity执行类
    public abstract class tb_TraderateContextEntityAction
    {
        
        private tb_TraderateContextEntityAction()
        {
        }
        
        public static void Save(tb_TraderateContextEntity obj)
        {
            if (obj!=null)
            {
                obj.Save();
            }
        }
        
        /// <summary>根据主键获取一个实体</summary>
        public static tb_TraderateContextEntity RetrieveAtb_TraderateContextEntity(int id)
        {
            tb_TraderateContextEntity obj=new tb_TraderateContextEntity();
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
        public static EntityContainer Retrievetb_TraderateContextEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(tb_TraderateContextEntity));
            return rc.AsEntityContainer();
        }
        
        /// <summary>获取所有实体(EntityContainer)</summary>
        public static DataTable Gettb_TraderateContextEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(tb_TraderateContextEntity));
            return rc.AsDataTable();
        }
    }
}