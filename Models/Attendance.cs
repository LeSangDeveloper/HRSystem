using System;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.Models
{
    /// <summary>
    /// 考勤資料
    /// </summary>
    public class Attendance
    {
        /// <summary>
        /// 考勤編號(主鍵)
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 上班時間
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 下班時間
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 員工編號（外鍵）
        /// </summary>
        [Required]
        public int EmployeeId { get; set; }

        /// <summary>
        /// 員工資料（導航屬性）
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// 是否假日加班（"Y" 或 "N"）
        /// </summary>
        public string IsHolidayWork { get; set; }
    }
}
