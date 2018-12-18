public static class EPermission
{
    public static string FullAccess = "Full Access";
    public static string ViewOnly = "View Only";
    public static string NoAccess = "No Access";

    public static string[] GetList()
    {
        return new[] { "Full Access", "View Only", "No Access" };
    }
}