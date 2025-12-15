using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRSystem.Models
{
    /// <summary>
    /// 模擬資料上下文，提供對員工、部門及考勤資料的操作
    /// </summary>
    public class DataContext
    {
        /// <summary>
        /// 單例資料來源
        /// </summary>
        private readonly Data _data;

        /// <summary>
        /// 員工列表（可操作的本地副本）
        /// </summary>
        public List<Employee> Employees { get; set; }

        /// <summary>
        /// 部門列表（可操作的本地副本）
        /// </summary>
        public List<Department> Departments { get; set; }

        /// <summary>
        /// 考勤資料列表（可操作的本地副本）
        /// </summary>
        public List<Attendance> Attendances { get; set; }

        /// <summary>
        /// 建構子：初始化資料上下文，建立對單例資料的淺層複本
        /// </summary>
        /// <param name="data">單例資料</param>
        public DataContext(Data data)
        {
            _data = data;

            // 淺層複製引用
            Employees = _data.Employees;
            Departments = _data.Departments;
            Attendances = _data.Attendances;
        }

        /// <summary>
        /// 模擬 Include 功能，將 Attendance 的 Employee 與 Department 導入
        /// </summary>
        /// <returns>包含 Employee 與 Department 導航屬性的考勤列表</returns>
        public IEnumerable<Attendance> AttendancesWithEmployeeAndDepartment()
        {
            return Attendances.Select(a =>
            {
                a.Employee = Employees.FirstOrDefault(e => e.Id == a.EmployeeId);
                if (a.Employee != null)
                {
                    a.Employee.Deparment = Departments.FirstOrDefault(d => d.Id == a.Employee.DepartmentId);
                }
                return a;
            });
        }

        /// <summary>
        /// 將本地操作同步回單例資料
        /// </summary>
        public async Task SaveAsync()
        {
            // 更新員工資料
            foreach (var emp in Employees)
            {
                var existingEmp = _data.Employees.FirstOrDefault(e => e.Id == emp.Id);
                if (existingEmp != null)
                {
                    existingEmp.Name = emp.Name;
                    existingEmp.DepartmentId = emp.DepartmentId;
                    existingEmp.Deparment = emp.Deparment; // 如果需要
                }
                else
                {
                    _data.Employees.Add(emp); // 新增員工
                }
            }

            // 更新部門資料
            foreach (var dept in Departments)
            {
                var existingDept = _data.Departments.FirstOrDefault(d => d.Id == dept.Id);
                if (existingDept != null)
                {
                    existingDept.Name = dept.Name;
                }
                else
                {
                    _data.Departments.Add(dept);
                }
            }

            // 更新考勤資料
            foreach (var att in Attendances)
            {
                var existingAtt = _data.Attendances.FirstOrDefault(a => a.Id == att.Id);
                if (existingAtt != null)
                {
                    existingAtt.EmployeeId = att.EmployeeId;
                    existingAtt.StartTime = att.StartTime;
                    existingAtt.EndTime = att.EndTime;
                    existingAtt.Note = att.Note;
                    existingAtt.IsHolidayWork = att.IsHolidayWork;
                }
                else
                {
                    _data.Attendances.Add(att);
                }
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 根據員工編號刪除員工及其考勤資料
        /// </summary>
        /// <param name="id">員工編號</param>
        public async Task DeleteEmployeeByIdAsync(int id)
        {
            var emp = _data.Employees.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                _data.Employees.Remove(emp);
                _data.Attendances.RemoveAll(a => a.EmployeeId == id);
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// 根據部門編號刪除部門，並處理相關員工
        /// </summary>
        /// <param name="id">部門編號</param>
        public async Task DeleteDepartmentByIdAsync(int id)
        {
            var dept = _data.Departments.FirstOrDefault(d => d.Id == id);
            if (dept != null)
            {
                _data.Departments.Remove(dept);

                // 將該部門員工的 DepartmentId 設為 0
                foreach (var emp in _data.Employees.Where(e => e.DepartmentId == id))
                {
                    emp.DepartmentId = 0;
                }
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// 根據考勤編號刪除考勤資料
        /// </summary>
        /// <param name="id">考勤編號</param>
        public async Task DeleteAttendanceByIdAsync(int id)
        {
            var att = _data.Attendances.FirstOrDefault(a => a.Id == id);
            if (att != null)
            {
                _data.Attendances.Remove(att);
            }
            await Task.CompletedTask;
        }
    }
}
