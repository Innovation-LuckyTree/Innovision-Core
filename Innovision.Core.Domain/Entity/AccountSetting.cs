namespace Innovision.Core.Domain.Entity;

public class AccountSetting
{
    public long AccountSettingId { get; set; }
    public long AccountInfoId { get; set; }
    public bool InAppNotification { get; set; } = false;
    public bool SmsNotification { get; set; } = false;
    public bool EmailNotification { get; set; } = false;

    public virtual Account AccountInfo { get; set; }
}

