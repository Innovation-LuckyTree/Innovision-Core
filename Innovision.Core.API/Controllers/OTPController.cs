using Core.Application.Request.Otps.Queries.GetPendingOTP;
using Innovision.Core.API.Controllers;
using Innovision.Core.Application.Requests.Otps.Commands.GenerateLoginOtp;
using Innovision.Core.Application.Requests.Otps.Commands.GenerateOtp;
using Innovision.Core.Application.Requests.Otps.Commands.GenerateRegistrationOtp;
using Innovision.Core.Application.Requests.Otps.Commands.GenerateVerificationOtp;
using Innovision.Core.Application.Requests.Otps.Commands.VerifyOtp;
using Innovision.Core.Application.Requests.Otps.Commands.VerifyVerificationOTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.API.Controllers;

public class OTPController : ApiBaseController
{
    private readonly ILogger<OTPController> _logger;

    public OTPController(ILogger<OTPController> logger)
    {
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("generate")]
    public async Task<ActionResult> Generate(GenerateOtpCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpPost("generate/registration")]
    public async Task<ActionResult> GenerateRegistration(GenerateRegistrationOtpCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpPost("generate/verification")]
    public async Task<ActionResult> GenerateVerification(GenerateVerificationOtpCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpPost("generate/login")]
    public async Task<ActionResult> GenerateLogin(GenerateLoginOtpCommand generatePlayerOtpCommand, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(generatePlayerOtpCommand, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpPut("verifyOTP")]
    public async Task<ActionResult> VerifyOTPV2(VerifyV2OtpCommand verifyOtpCommand, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(verifyOtpCommand, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    [HttpPut("verifyVerificationOTP")]
    [AllowAnonymous]
    public async Task<ActionResult> VerifyVerificationOTP(VerifyVerificationOTPCommand verifyOtpCommand, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(verifyOtpCommand, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpGet("pending")]
    public async Task<ActionResult> GetPendingOTP(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetPendingOTPQuery(), cancellationToken);
        return Ok(result);
    }
}