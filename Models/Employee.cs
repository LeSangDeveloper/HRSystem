using System.ComponentModel.DataAnnotations;

namespace HRSystem.Models
{
    /// <summary>
    /// 員工資料
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 員工編號（主鍵）
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 員工姓名
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 部門編號（外鍵）
        /// </summary>
        [Required]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 所屬部門資料（導航屬性）
        /// </summary>
        public Department Deparment { get; set; }
    }
}
