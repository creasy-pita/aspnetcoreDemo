using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace CustomModelBindingSample.Data
{
    /// <summary>
    /// tb_task Data Structure.
    /// </summary>
    public partial class tb_task_model
    {
	/*�����Զ����ɹ����Զ�����,��Ҫ������д�Լ��Ĵ��룬����ᱻ�Զ�����Ŷ - ����*/
        
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string taskname { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int categoryid { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int nodeid { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime taskcreatetime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime taskupdatetime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime tasklaststarttime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime tasklastendtime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime tasklasterrortime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int taskerrorcount { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int taskruncount { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int taskcreateuserid { get; set; }
        
        /// <summary>
        /// 0 ֹͣ  1 ������
        /// </summary>
        public Byte taskstate { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int taskversion { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string taskappconfigjson { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string taskcron { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string taskmainclassnamespace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string taskmainclassdllfilename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string taskremark { get; set; }
        
    }
}