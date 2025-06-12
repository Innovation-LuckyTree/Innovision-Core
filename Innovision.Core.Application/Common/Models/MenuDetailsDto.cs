namespace Innovision.Core.Application.Common.Models;

public class MenuDetailsDto
{
    public int MenuId { get; set; }
    public string MenuCode { get; set; }
    public string MenuName { get; set; }
    public bool IsParent { get; set; }
    public int? ParentId { get; set; }

    public int? SecurityGroupId { get; set; }
    public int? CompanyId { get; set; }
    public int? UserTypeId { get; set; }
    public int? UserTypeMenuId { get; set; }
    public bool? ReadWrite { get; set; } = false;
    public bool? Enabled { get; set; } = true;
}
