using System;
using System.Collections.Generic;

namespace HRSystem.Models
{
    /// <summary>
    /// 模擬資料集合(資料庫)，包括員工、部門及考勤資料
    /// </summary>
    public class Data
    {
        /// <summary>
        /// 員工列表
        /// </summary>
        public List<Employee> Employees { get; set; }

        /// <summary>
        /// 部門列表
        /// </summary>
        public List<Department> Departments { get; set; }

        /// <summary>
        /// 考勤資料列表
        /// </summary>
        public List<Attendance> Attendances { get; set; }

        /// <summary>
        /// 建構子：初始化模擬部門、員工及考勤資料
        /// </summary>
        public Data()
        {
            // 初始化模擬部門
            Departments = new List<Department>
            {
                new Department { Id = 1, Name = "HR" },
                new Department { Id = 2, Name = "IT" },
                new Department { Id = 3, Name = "Finance" }
            };

            // 初始化模擬員工
            Employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Alice", DepartmentId = 1 },
                new Employee { Id = 2, Name = "Bob", DepartmentId = 2 },
                new Employee { Id = 3, Name = "Charlie", DepartmentId = 3 }
            };

            // 初始化考勤資料（總共 60 筆記錄）
            Attendances = new List<Attendance>();

            var startDate = DateTime.Today.AddDays(-20);

            // 固定工作時段，避免重疊
            var workSchedules = new Dictionary<int, (int start, int end)>
            {
                { 1, (9, 18) },   // Alice
                { 2, (9, 17) },   // Bob
                { 3, (10, 19) }   // Charlie
            };

            int id = 0;
            for (int day = 0; day < 20; day++)
            {
                var currentDate = startDate.AddDays(day);

                foreach (var schedule in workSchedules)
                {
                    Attendances.Add(new Attendance
                    {
                        Id = id++,
                        EmployeeId = schedule.Key,
                        StartTime = currentDate.AddHours(schedule.Value.start),
                        EndTime = currentDate.AddHours(schedule.Value.end),
                        Note = "",
                        IsHolidayWork = "N"
                    });
                }
            }
        }
    }
}
