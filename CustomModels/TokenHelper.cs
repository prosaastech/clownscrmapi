namespace ClownsCRMAPI.CustomModels
{
    public static class TokenHelper
    {
        public static int GetBranchId(HttpContext context)
        {
            return Convert.ToInt32(context.Items["BranchId"]);
        }

        public static int GetCompanyId(HttpContext context)
        {
            return Convert.ToInt32(context.Items["CompanyId"]);
        }
    }

}
