public static class EApprovalStatus
{
    public static string OnApproved = "On-Approved";
    public static string OnApprovedBM = "On-Approved-BM";
    public static string OnAccepted = "On-Accepted";
    public static string OnRevise = "On-Revise";
    public static string Approved = "Approved";
    public static string ApprovedBM = "Approved-Brand-Manager";
    public static string AcknowledgeGM = "Acknowledge-GM";
    public static string Accepted = "Accepted";
    public static string AcceptedAdmDesign = "Accepted-Adm-Design";
    public static string AcceptedGraphicDesign = "Accepted-Graphic-Design";
    public static string AcceptedCreativeManager = "Accepted-Creative-Manager";
    public static string Cancel = "Cancel";
    public static string Done = "Done";

    public static string[] GetList()
    {
        return new[] { "On-Approved", "On-Approved-BM", "On-Accepted", "On-Revise", "Approved", "Approved-Brand-Manager", "Acknowledge-GM", "Accepted", "Accepted-Adm-Design", "Accepted-Graphic-Design", "Accepted-Creative-Manager",  "Cancel", "Done" };
    }
}