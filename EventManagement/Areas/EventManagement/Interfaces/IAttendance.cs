using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IAttendance
    {
        IEnumerable<ResponseAttendance> Attendances(int concernId, string userName, int userId);
        ResponseAttendance AttendanceDetails(int attendanceId, string userName, int userId, string culture);
        Attendance AttendanceDetilsbyId(int attendanceId, string userName, int userId);
        void AddAttendance(Attendance attendance, string userName, int userId, int concernId);
        void DeleteAttendance(int attendanceId, string userName, int userId);
        void UpdateAttendance(Attendance attendance, int attendanceId, string userName, int userId);
    }
}
