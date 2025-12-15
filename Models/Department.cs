using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.Models
{
    /// <summary>
    /// 部門資料
    /// </summary>
    public class Department
    {
        /// <summary>
        /// 部門編號
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 部門名稱
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 所屬員工列表（導航屬性）
        /// </summary>
        public List<Employee> Employees { get; set; }
    }
}
