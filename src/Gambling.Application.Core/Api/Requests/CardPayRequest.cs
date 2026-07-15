using Gambling.Application.Core.BusinessModels;

namespace Gambling.Application.Core.Api.Requests;

public abstract record CardPayRequest(int UserId, PayCard Card) : BaseRequiringIdRequest(UserId);