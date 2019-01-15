using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Services
{
    public class AttendanceService: IAttendance
    {
        private DataContext _context;
        public AttendanceService(DataContext context)
        {
            _context = context;
        }

        public void AddAttendance(Attendance attendance, string userName, int userId, int concernId)
        {
            var todayDate = DateTime.Now;
            attendance.CreationDate = todayDate;
            attendance.CreatorId = userId;
            attendance.IsDelete = 0;
            attendance.ConcernId = concernId;
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
        }

        public ResponseAttendance AttendanceDetails(int attendanceId, string userName, int userId, string culture)
        {
            throw new NotImplementedException();
        }

        public Attendance AttendanceDetilsbyId(int attendanceId, string userName, int userId)
        {
            var attendance = _context.Attendances.FirstOrDefault(a => a.AttendanceId == attendanceId && a.IsDelete == 0);
            return attendance;
        }

        public IEnumerable<ResponseAttendance> Attendances(int concernId, string userName, int userId)
        {
            List<ResponseAttendance> responseAttendancesList = new List<ResponseAttendance>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_GetAttendanceIndex @concernId");
                command.Parameters.Add(new SqlParameter("concernId", concernId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            ResponseAttendance responseAttendance = new ResponseAttendance()
                            {
                                SerialNumber = Convert.ToInt32(result[0]),
                                AttendanceId = Convert.ToInt32(result[1]),
                                AttendanceDate = Convert.ToDateTime(result[2]),
                                InTime = Convert.ToDateTime(result[3]),
                                OutTime = Convert.ToDateTime(result[4]),
                                EmployeeId = Convert.ToInt32(result[5]),
                                EmployeeFirstName = Convert.ToString(result[6]),
                                EmployeeLastName = Convert.ToString(result[7]),
                                TotalHour = Convert.ToInt32(result[8])
                            };
                            responseAttendancesList.Add(responseAttendance);
                        }
                    }
                }
                _context.Database.Connection.Close();
            }
            return responseAttendancesList;
        }

        public void DeleteAttendance(int attendanceId, string userName, int userId)
        {
            var attendance = AttendanceDetilsbyId(attendanceId, userName, userId);
            var todayDate = DateTime.Now;
            attendance.IsDelete = 1;
            attendance.ModifierId = userId;
            attendance.ModificationDate = todayDate;
            _context.SaveChanges();
        }

        public void UpdateAttendance(Attendance attendance, int attendanceId, string userName, int userId)
        {
            var attendanceById = AttendanceDetilsbyId(attendanceId, userName, userId);
            var todayDate = DateTime.Now;
            attendanceById.ModifierId = userId;
            attendanceById.ModificationDate = todayDate;
            attendanceById.AttendanceDate = attendance.AttendanceDate;
            attendanceById.InTime = attendance.InTime;
            attendanceById.OutTime = attendance.OutTime;
            attendanceById.EmployeeId = attendance.EmployeeId;
            _context.SaveChanges();

        }
    }
}