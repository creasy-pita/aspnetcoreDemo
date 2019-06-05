using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomModelBindingSample.Data
{

    /// <summary>
    /// tb_error Data Structure.
    /// </summary>
    public partial class tb_task_config_model
    {
        /*代码自动生成工具自动生成,不要在这里写自己的代码，否则会被自动覆盖哦 - 车毅*/

        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 任务id  tb_task 或者tb_webtask 的任务id
        /// </summary>
        public int taskid { get; set; }
        /// <summary>
        /// 相对于应用程序根目录的路径
        /// </summary>
        public string relativePath { get; set; }

        /// <summary>
        /// 配置文件的内容
        /// </summary>
        public string filecontent { get; set; }
        /// <summary>
        /// 配置文件的名称
        /// </summary>
        public string filename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime lastupdatetime { get; set; }

    }
}
