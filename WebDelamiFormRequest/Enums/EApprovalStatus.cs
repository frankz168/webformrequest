public static class EApprovalStatus
{
    public static string OnApprovedHD = "Submit";
    public static string OnApprovedBM = "Submit-Over-Budget";
    public static string OnAccepted = "On-Accepted";
    public static string OnRevise = "On-Revise";
    public static string OnReviseForm = "On-Revise-Form";
    public static string OnReviseDesign = "On-Revise-Design";
    public static string OnRevisePhoto = "On-Revise-Photo";
    public static string OnRevisePhotoDI = "On-Revise-Photo-DI";
    public static string OnReviseBudgetControl = "On-Revise-Budget-Control";
    public static string OnReviseContent = "On-Revise-Content";
    public static string OnWorking = "On-Working";
    public static string ApprovedHD = "Approved-Head-Department";
    public static string ApprovedGMMarkom = "Approved-GM-Markom";
    public static string ApprovedHDPhoto = "Approved-Head-Department-Photo";
    public static string ApprovedHDDI = "Approved-Head-Department-DI";
    public static string ApprovedHDVM = "Approved-Head-VM";
    public static string ApprovedHDVMPhoto = "Approved-Head-VM-Photo";
    public static string ApprovedHDVMDI = "Approved-Head-VM-DI";
    public static string ApprovedHDSD = "Approved-Head-Department-Store-Design";
    public static string ApprovedPhoto = "Approved-Photo";
    public static string ApprovedDI = "Approved-DI";
    public static string ApprovedBM = "Approved-Brand-Manager";
    public static string ApprovedBMPhoto = "Approved-Brand-Manager-Photo";
    public static string ApprovedBMDI = "Approved-Brand-Manager-DI";
    public static string ApprovedBMBudget = "Approved-Brand-Manager-Budget";
    public static string ApprovedDM = "Approved-Digital-Marketing";
    public static string ApprovedProject = "Approved-Project";
    public static string ApprovedComercialDirector = "Approved-Comercial-Director";
    public static string ApprovedComercialDirectorBudget = "Approved-Comercial-Director-Budget";
    public static string ApprovedBudgetControl = "Approved-Budget-Control";
    public static string ApprovedStoreDesign = "Approved-Store-Design";
    public static string AcknowledgeGM = "Acknowledge-GM";
    public static string Accepted = "Accepted";
    public static string AcceptedHeadDesign = "Approved-Admin-Creative";
    public static string AcceptedGraphicDesign = "Posting-Design";
    public static string ApprovedCreativeManager = "Approved-Creative-Director";
    public static string ApprovedCreativeManagerPG = "Approved-Creative-Director-Photo";
    public static string ApprovedCreativeManagerDI = "Approved-Creative-Director-DI";
    public static string Cancel = "Cancel";
    public static string Done = "Done";
    public static string DoneProduction = "In-Production";
    public static string DoneProductionCetakMateri = "In-Production-Cetak-Materi";
    public static string DeliveredDistribution = "Delivered-Distribution";

    public static string[] GetList()
    {
        return new[] { "Submit", "Submit-Over-Budget", "On-Approved-BM", "On-Accepted", "On-Revise", "On-Revise-Form", "On-Revise-Design", "On-Revise-Photo", "On-Revise-Photo-DI", "On-Revise-Budget-Control", "On-Working", "Approved-Head-Department", "Approved-GM-Markom", "Approved-Head-Department-Photo","Approved-Head-VM", "Approved-Head-VM-Photo", "Approved-Head-VM-DI", "Approved-Head-Department-Store-Design", "Approved-Photo", "Approved-DI", "Approved-Brand-Manager", "Approved-Brand-Manager-Photo", "Approved-Brand-Manager-DI", "Approved-Brand-Manager-Budget", "Approved-Digital-Marketing", "Approved-Project", "Approved-Comercial-Director", "Approved-Comercial-Director-Budget", "Approved-Budget-Control", "Approved-Store-Design", "Acknowledge-GM", "Accepted", "Approved-Admin-Creative", "Posting-Design", "Approved-Creative-Director", "Approved-Creative-Director-Photo", "Approved-Creative-Director-DI", "Cancel", "Done", "In-Production", "In-Production-Cetak-Materi", "Delivered-Distribution" };
    }
}