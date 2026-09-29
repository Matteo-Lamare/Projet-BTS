namespace EduGest.Infrastructure.Authorization;

public static class Permissions
{
    public static readonly string[] All = [
        "students.read", "students.write", "teachers.read", "teachers.write", "classes.read", "classes.write",
        "subjects.read", "subjects.write", "grades.read", "grades.write", "attendance.read", "attendance.write",
        "documents.read", "documents.write", "messages.read", "messages.write", "timetable.read",
        "statistics.read", "users.manage", "roles.manage", "audit.read", "settings.manage"
    ];

    public static string Policy(string permission) => "Permission:" + permission;
}
