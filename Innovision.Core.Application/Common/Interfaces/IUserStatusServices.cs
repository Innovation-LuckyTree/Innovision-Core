namespace Innovision.Core.Application.Common.Interfaces
{
    public interface IUserStatusServices
    {
        Task<List<long>> GetOnlineIds(CancellationToken cancellationToken);
        Task<List<long>> GetPlayingIds(Guid CompanyObjId, CancellationToken cancellationToken);
    }
}
