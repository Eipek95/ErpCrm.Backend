namespace ErpCrm.Application.Common.Constants;

public static class AuthorizationPolicies
{
    public const string AdminOnly = "AdminOnly";
    public const string ManagerOrAdmin = "ManagerOrAdmin";
    public const string EmployeeOrAbove = "EmployeeOrAbove";
}