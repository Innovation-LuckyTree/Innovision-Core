using Innovision.Core.Application.Common.Models;
using Innovision.Core.Application.Requests.JackpotWinners.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.JackpotWinners.Commands.AddJackpotWinner;

public class UpdateJackpotWinnerCommand : IRequest<JackpotWinnerDto>
{
    public long JackpotWinnerId { get; set; }
    public decimal TaxPercentage { get; set; }
    public int JackpotWinnerStatusId { get; set; }
    public string Remarks { get; set; }

    public IEnumerable<AttachmentRequest> Attachments { get; set; }
}
