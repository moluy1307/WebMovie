using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMovie.Backend.Common.Constants
{
    /// <summary>
    /// Tên stored procedure
    /// </summary>
    public static class ProcedureName
    {
        /// <summary>
        /// Procedure Get
        /// </summary>
        public static string Get = "Proc_{0}_Get{1}";

        /// <summary>
        /// Procedure Pagination
        /// </summary>
        public static string Pagination = "Proc_{0}_Pagination";

        /// <summary>
        /// Procedure Insert
        /// </summary>
        public static string Insert = "Proc_{0}_Insert";

        /// <summary>
        /// Procedure Update
        /// </summary>
        public static string Update = "Proc_{0}_Update";

        /// <summary>
        /// Procedure Update TreeStructure
        /// </summary>
        public static string UpdateTree = "Proc_{0}_Update{1}";

        /// <summary>
        /// Procedure Delete
        /// </summary>
        public static string Delete = "Proc_{0}_Delete";
    }
}
