namespace MovieClub.Web.Common;
public class GetSelectList
{
    public static List<SelectListItem> AttendanceSelect(AttendanceStatus status)
    {
        List<SelectListItem> list = new();

        switch (status)
        {
            case AttendanceStatus.Declined:
                list.Add(new SelectListItem { Selected = true, Text = "Can't Attend", Value = ((int)status).ToString() });
                break;
            default:
                list.Add(new SelectListItem { Selected = true, Text = status.ToString(), Value = ((int)status).ToString() });
                break;
        }

        if (status is not AttendanceStatus.Attending)
        {
            list.Add(new SelectListItem { Text = AttendanceStatus.Attending.ToString(), Value = ((int)AttendanceStatus.Attending).ToString() });
        }

        if (status is not AttendanceStatus.Invited)
        {
            list.Add(new SelectListItem { Text = "Undecided", Value = ((int)AttendanceStatus.Invited).ToString() });
        }

        if (status is not AttendanceStatus.Declined)
        {
            list.Add(new SelectListItem { Text = "Can't Attend", Value = ((int)AttendanceStatus.Declined).ToString() });
        }

        return list;
    }
}
