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
    public class tb_LdleTimeslotEntity : EntityObject
    {
        
        /// <summary>id</summary>
        public const string @__ID = "id";
        
        /// <summary>startTime</summary>
        public const string @__STARTTIME = "startTime";
        
        /// <summary>endTime</summary>
        public const string @__ENDTIME = "endTime";
        
        /// <summary>isFull</summary>
        public const string @__ISFULL = "isFull";
        
        /// <summary>nick</summary>
        public const string @__NICK = "nick";
        
        private int m_id;
        
        private System.DateTime m_startTime = DateTime.MinValue;
        
        private System.DateTime m_endTime = DateTime.MinValue;
        
        private bool m_isFull;
        
        private string m_nick;
        
        /// <summary>构造函数</summary>
        public tb_LdleTimeslotEntity()
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
        
        /// <summary>属性startTime </summary>
        public System.DateTime startTime
        {
            get
            {
                return this.m_startTime;
            }
            set
            {
                this.m_startTime = value;
            }
        }
        
        /// <summary>属性endTime </summary>
        public System.DateTime endTime
        {
            get
            {
                return this.m_endTime;
            }
            set
            {
                this.m_endTime = value;
            }
        }
        
        /// <summary>属性isFull </summary>
        public bool isFull
        {
            get
            {
                return this.m_isFull;
            }
            set
            {
                this.m_isFull = value;
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
    }
    
    /// tb_LdleTimeslotEntity执行类
    public abstract class tb_LdleTimeslotEntityAction
    {
        
        private tb_LdleTimeslotEntityAction()
        {
        }
        
        public static void Save(tb_LdleTimeslotEntity obj)
        {
            if (obj!=null)
            {
                obj.Save();
            }
        }
        
        /// <summary>根据主键获取一个实体</summary>
        public static tb_LdleTimeslotEntity RetrieveAtb_LdleTimeslotEntity(int id)
        {
            tb_LdleTimeslotEntity obj=new tb_LdleTimeslotEntity();
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
        public static EntityContainer Retrievetb_LdleTimeslotEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(tb_LdleTimeslotEntity));
            return rc.AsEntityContainer();
        }
        
        /// <summary>获取所有实体(EntityContainer)</summary>
        public static DataTable Gettb_LdleTimeslotEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(tb_LdleTimeslotEntity));
            return rc.AsDataTable();
        }
    }
}
