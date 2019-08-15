using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbComparison.Model
{
    /// <summary>
    /// 表结构
    /// </summary>
    public class TableStruct
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string tableName { get; set; }

        /// <summary>
        /// 字段集合
        /// </summary>
        public List<Filed> filedList { get; set; }

        public class Filed
        {
            /// <summary>
            /// 名称
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 类型
            /// </summary>
            public string type { get; set; }

            /// <summary>
            /// 是否为空
            /// </summary>
            public bool isNull { get; set; }
        }
    }
}
