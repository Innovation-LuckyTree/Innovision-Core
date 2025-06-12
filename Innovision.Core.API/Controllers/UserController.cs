using Innovision.Core.Application.Requests.Accounts.Users.AccountApproval.Commands;
using Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Commands.UserRegistration;
using Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Queries.GetAgents;
using Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Queries.GetPlayers;
using Innovision.Core.Application.Requests.Accounts.Users.MasterAgent.Commands.CreateMasterAgent;
using Innovision.Core.Application.Requests.Accounts.Users.MasterAgent.Queries.GetMasterAgents;
using Innovision.Core.Application.Requests.Accounts.Users.Operator.Commands;
using Innovision.Core.Application.Requests.Accounts.Users.Operator.Queries.GetOperator;
using Innovision.Core.Application.Requests.Users.AccountApproval.Commands;
using Innovision.Core.Application.Requests.Users.AccountApproval.Commands.RequestUserVerification;
using Innovision.Core.Application.Requests.Users.AccountApproval.Queries.GetUsersForApprovalAll;
using Innovision.Core.Application.Requests.Users.AccountApproval.Queries.GetUsersForApprove;
using Innovision.Core.Application.Requests.Users.AgentAndPlayer.Commands.RecruiterRegistration;
using Innovision.Core.Application.Requests.Users.AgentAndPlayer.Commands.UpdateUserByAccountObjectId;
using Innovision.Core.Application.Requests.Users.AgentAndPlayer.Queries.GetDownlineAgents;
using Innovision.Core.Application.Requests.Users.AgentAndPlayer.Queries.GetDownlineCounts;
using Innovision.Core.Application.Requests.Users.AgentAndPlayer.Queries.GetDownlinePlayers;
using Innovision.Core.Application.Requests.Users.Commands.BulkCreateUser;
using Innovision.Core.Application.Requests.Users.Commands.CreateSystemUser;
using Innovision.Core.Application.Requests.Users.Commands.UpdateAddress;
using Innovision.Core.Application.Requests.Users.Commands.UpdateNotificationSetting;
using Innovision.Core.Application.Requests.Users.Commands.UpdatePersonalDetails;
using Innovision.Core.Application.Requests.Users.Commands.UpdateProfession;
using Innovision.Core.Application.Requests.Users.Commands.UpdateProfileImage;
using Innovision.Core.Application.Requests.Users.Commands.UpdateProofInfo;
using Innovision.Core.Application.Requests.Users.Commands.UpdateUserDownline;
using Innovision.Core.Application.Requests.Users.Commands.UpdateUserStatuses;
using Innovision.Core.Application.Requests.Users.MasterAgent.Queries.GetCompanyGFM;
using Innovision.Core.Application.Requests.Users.Operator.Queries.GetMainOperator;
using Innovision.Core.Application.Requests.Users.Queries.GetAgentsByBranchId;
using Innovision.Core.Application.Requests.Users.Queries.GetDefaultAgent;
using Innovision.Core.Application.Requests.Users.Queries.GetDownlineUsers;
using Innovision.Core.Application.Requests.Users.Queries.GetFullyVerifiedUsers;
using Innovision.Core.Application.Requests.Users.Queries.GetFullyVerifiedUsersExport;
using Innovision.Core.Application.Requests.Users.Queries.GetLoadUserPaginate;
using Innovision.Core.Application.Requests.Users.Queries.GetLoadUsers;
using Innovision.Core.Application.Requests.Users.Queries.GetPaginatedUsers;
using Innovision.Core.Application.Requests.Users.Queries.GetPaginateUserByRole;
using Innovision.Core.Application.Requests.Users.Queries.GetPaginateVerifiedUser;
using Innovision.Core.Application.Requests.Users.Queries.GetPlayerByObjectId;
using Innovision.Core.Application.Requests.Users.Queries.GetPlayersList;
using Innovision.Core.Application.Requests.Users.Queries.GetSemiVerifiedUsers;
using Innovision.Core.Application.Requests.Users.Queries.GetSemiVerifiedUsersExport;
using Innovision.Core.Application.Requests.Users.Queries.GetSystemUserById;
using Innovision.Core.Application.Requests.Users.Queries.GetSystemUsers;
using Innovision.Core.Application.Requests.Users.Queries.GetSystemUsersByObjectID;
using Innovision.Core.Application.Requests.Users.Queries.GetUserByInfoId;
using Innovision.Core.Application.Requests.Users.Queries.GetUserForNotification;
using Innovision.Core.Application.Requests.Users.Queries.GetUserForVerification;
using Innovision.Core.Application.Requests.Users.Queries.GetUserUpline;
using Innovision.Core.Application.Requests.Users.Queries.GetVerifiedUsers;
using Innovision.Core.Application.Requests.Users.Queries.ValidateMobileNumber;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers
{
    public class UserController : ApiBaseController
    {

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// Agent and Player registration
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("registration")]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] UserRegistrationCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// recruiter registration
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("recruiter/registration")]
        [AllowAnonymous]
        public async Task<ActionResult> RecruiterRegistration([FromBody] RecruiterRegistrationCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Agent and Player update info -  semi verified registration
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("update/getverified")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateUserByAccountObjectId([FromBody] UpdateUserByAccountObjectIdCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }


        /// <summary>
        /// 
        /// Update user proof info
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("proof/info")]
        public async Task<ActionResult> UpdateProofInfo(UpdateProofInfoCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Update user personal details
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("personal/details")]
        public async Task<ActionResult> UpdatePersonalDetails(UpdatePersonalDetailsCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Update user personal details
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("profession")]
        public async Task<ActionResult> UpdateProfession(UpdateProfessionCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Update user Address Info
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("address")]
        public async Task<ActionResult> UpdateUserAddress(UpdateAddressCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Add/Update user account setting
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("account/setting")]
        public async Task<ActionResult> UpdateAccountSetting(UpdateNotificationSettingCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Update user profile image
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("profile/image")]
        public async Task<ActionResult> UpdateProfileImage(UpdateProfileImageCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Add new master agent
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("masteragent")]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] CreateMasterAgentCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Add new operator
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("operator")]
        public async Task<ActionResult> Post([FromBody] CreateOperatorCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        ///  Get list of operators by company ID
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("operators/search")]
        public async Task<IActionResult> Post([FromBody] GetOperatorQuery command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        ///  Get list of Master Agents
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("masteragents/search")]
        public async Task<IActionResult> Post([FromBody] GetMasterAgentsQuery command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        ///  Get list of Agents
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("agents/search")]
        public async Task<IActionResult> Post([FromBody] GetAgentsQuery command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        ///  Get list of players
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("players/search")]
        public async Task<IActionResult> Post([FromBody] GetPlayersQuery command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        ///  Get all operators
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("operators")]
        public async Task<IActionResult> GetAllOperators(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetOperatorQuery());
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        ///  Get all Master Agents
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("masteragents")]
        public async Task<IActionResult> GetAllMasterAgents(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetMasterAgentsQuery());
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        ///  Get all Agents
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("agents")]
        public async Task<IActionResult> GetAllAgents(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAgentsQuery());
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        ///  Get all players
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("players")]
        public async Task<IActionResult> GetAllPlayers(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetPlayersQuery());
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// List of users for Approval=
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("request/approval")]
        public async Task<ActionResult> GetUsersForApprove([FromBody] GetUsersForApproveQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// List of users for Approval to all branches
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("request/approval/all")]
        public async Task<ActionResult> GetUsersForApprovalAll([FromBody] GetUsersForApprovalAllQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Approve user
        /// 
        /// </summary>
        /// <param name="approvedUserCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("approved")]
        public async Task<ActionResult> Approved(ApprovedUserCommand approvedUserCommand, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(approvedUserCommand, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Decline user
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("declined")]
        public async Task<ActionResult> Declined(DeclineUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Create system user
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("system/create")]
        public async Task<ActionResult> CreateSystemUserCommand(CreateSystemUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Create multiple users
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("bulk/create")]
        public async Task<ActionResult> BulkCreateUserCommand(BulkCreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Create system user
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("mobile/validate")]
        public async Task<ActionResult> ValidateMobileNumber(ValidateMobileNumberQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get user information by AccountInfoId
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserById(Guid userId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetSystemUserByIdQuery(userId), cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get user information by AccountInfoId
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("details/{accountInfoId}")]
        public async Task<ActionResult> GetAccountByInfoId(long accountInfoId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUserByInfoIdCommand(accountInfoId), cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }
        [HttpGet("mobileNumber")]
        [AllowAnonymous]
        public async Task<ActionResult> GetMobileNumberByObjectID([FromQuery] GetPlayerNumberByObjectIdQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Get user information by AccountInfoId
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("accountObjectId")]
        public async Task<ActionResult> GetSystemUsersByObjectID([FromQuery] GetSystemUsersByObjectIDQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get list of users filtered by company, branch and role.
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<ActionResult> GetSystemUsersQuery([FromQuery] GetSystemUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }


        /// <summary>
        /// 
        /// Get pageinated list of users filtered by company, branch and role.
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("paginated-list")]
        public async Task<ActionResult> GetPaginatedUserList([FromBody] GetPaginatedUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get list of users for loading
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("loading/list")]
        public async Task<ActionResult> GetLoadUsersQuery([FromQuery] GetLoadUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get list of users for loading
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("paginate/loading/list")]
        public async Task<ActionResult> GetLoadUsersPaginateQuery(GetLoadUserPaginateQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }


        /// <summary>
        /// 
        /// Get list of users by role
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("paginate/list/byrole")]
        public async Task<ActionResult> GetListOfUsersByRole(GetPaginateUserByRoleQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get paginate verified users
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("paginate/list/verified")]
        public async Task<ActionResult> GetPaginateVerifiedUser(GetPaginateVerifiedUserQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get list of downline agents
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("downline/agents")]
        public async Task<ActionResult> GetDownlineAgentsQuery([FromBody] GetDownlineAgentsQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get list of downline agents
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("downline/players")]
        public async Task<ActionResult> GetDownlinePlayersQuery([FromBody] GetDownlinePlayersQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get downline counts
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("downline/counts/{accountObjectId}")]
        public async Task<ActionResult> GetDownlineCounts(Guid accountObjectId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetDownlineCountsQuery(accountObjectId), cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get all users for verification
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("request/for-verifcation")]
        public async Task<ActionResult> GetUserForVerification(GetUserForVerificationQuery command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Approve user verification
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("verified")]
        public async Task<ActionResult> ApprovedVerification(ApproveUserVerificationCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Decline user verification
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("verification/declined")]
        public async Task<ActionResult> VerificationDeclined(DeclinedUserVerificationCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Request user verification
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("getverified/request")]
        public async Task<ActionResult> RequestGetVerified(RequestUserVerificationCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// List all verified users
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("verified/list")]
        public async Task<ActionResult> VerifiedUsersList([FromQuery] GetVerifiedUsersQuery command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// List all semi-verified users
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("semi-verified/list")]
        public async Task<ActionResult> SemiVerifiedUsersList([FromBody] GetSemiVerifiedUsersQuery command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Export all semi-verified users
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("semi-verified/list/export")]
        public async Task<ActionResult> SemiVerifiedUsersListExport([FromBody] GetSemiVerifiedUsersExportQuery request, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// List all fully-verified users
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("fully-verified/list")]
        public async Task<ActionResult> FullyVerifiedUsersList([FromBody] GetFullyVerifiedUsersQuery command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Export all fully-verified users
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("fully-verified/list/export")]
        public async Task<ActionResult> FullyVerifiedUsersListExport([FromBody] GetFullyVerifiedUsersExportQuery request, CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// List all verified users
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("players/list")]
        public async Task<ActionResult> GetPlayersList([FromQuery] GetPlayersListQuery command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Update user statuses
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("update/statuses")]
        public async Task<ActionResult> UpdateUserStatuses(UpdateUserStatusesCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get list of Agents and Master agents by branch id
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("agents/list")]
        public async Task<ActionResult> GetListOfAgentsByBranchId([FromQuery] GetAgentsByBranchIdQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get upline recruiter
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("upline")]
        public async Task<ActionResult> GetUserUpline([FromQuery] GetUserUplineQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get Users for Notifincation
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("list/to-notify")]
        public async Task<ActionResult> GetUserListToNotify([FromQuery] GetUserForNotificationQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get company GFM
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("company/gfm")]
        public async Task<ActionResult> GetCompanyGFM([FromQuery] GetCompanyGFMQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get company Main Operator
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("company/main/operator")]
        public async Task<ActionResult> GetCompanyMainOperator([FromQuery] GetMainOperatorQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get downline users
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("downline")]
        public async Task<ActionResult> GetDownlineUsers([FromQuery] GetDownlineUsersQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Update downline user
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPatch("update/downline")]
        public async Task<ActionResult> GetDownlineUsers(UpdateUserDownlineCommand request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Get Default Agent by company
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("default/agent")]
        public async Task<ActionResult> GetDefaultAgent([FromQuery] GetDefaultAgentQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }
    }
}
