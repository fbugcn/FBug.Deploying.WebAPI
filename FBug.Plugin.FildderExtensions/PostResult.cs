using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBug.Plugin.FildderExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public enum PostResult
    {
        /// <summary>
        /// 未知结果
        /// </summary>
        Unknow,
        /// <summary>
        /// 提交成功
        /// </summary>
        Ok,
        /// <summary>
        /// 部分提交成功
        /// </summary>
        Partial,
        /// <summary>
        /// 提交失败
        /// </summary>
        Error
    }
}
