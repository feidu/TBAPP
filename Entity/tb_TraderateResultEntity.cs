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
    public class tb_TraderateResultEntity : EntityObject
    {
        
        /// <summary>id</summary>
        public const string @__ID = "id";
        
        /// <summary>nick</summary>
        public const string @__NICK = "nick";
        
        /// <summary>isSuccess</summary>
        public const string @__ISSUCCESS = "isSuccess";
        
        /// <summary>Result</summary>
        public const string @__RESULT = "Result";
        
        /// <summary>operatTime</summary>
        public const string @__OPERATTIME = "operatTime";
        
        /// <summary>type</summary>
        public const string @__TYPE = "type";
        
        private int m_id;
        
        private string m_nick;
        
        private bool m_isSuccess;
        
        private string m_Result;
        
        private System.DateTime m_operatTime = DateTime.MinValue;
        
        private string m_type;
        
        /// <summary>构造函数</summary>
        public tb_TraderateResultEntity()
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
        
        /// <summary>属性isSuccess </summary>
        public bool isSuccess
        {
            get
            {
                return this.m_isSuccess;
            }
            set
            {
                this.m_isSuccess = value;
            }
        }
        
        /// <summary>属性Result </summary>
        public string Result
        {
            get
            {
                return this.m_Result;
            }
            set
            {
                this.m_Result = value;
            }
        }
        
        /// <summary>属性operatTime </summary>
        public System.DateTime operatTime
        {
            get
            {
                return this.m_operatTime;
            }
            set
            {
                this.m_operatTime = value;
            }
        }
        
        /// <summary>属性type </summary>
        public string type
        {
            get
            {
                return this.m_type;
            }
            set
            {
                this.m_type = value;
            }
        }
    }
    
    /// tb_TraderateResultEntity执行类
    public abstract class tb_TraderateResultEntityAction
    {
        
        private tb_TraderateResultEntityAction()
        {
        }
        
        public static void Save(tb_TraderateResultEntity obj)
        {
            if (obj!=null)
            {
                obj.Save();
            }
        }
        
        /// <summary>根据主键获取一个实体</summary>
        public static tb_TraderateResultEntity RetrieveAtb_TraderateResultEntity(int id)
        {
            tb_TraderateResultEntity obj=new tb_TraderateResultEntity();
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
        public static EntityContainer Retrievetb_TraderateResultEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(tb_TraderateResultEntity));
            return rc.AsEntityContainer();
        }
        
        /// <summary>获取所有实体(EntityContainer)</summary>
        public static DataTable Gettb_TraderateResultEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(tb_TraderateResultEntity));
            return rc.AsDataTable();
        }
    }
}
