namespace Gambling.Application.Core.Api.Requests;

public record SlotSpinRequest(int UserId, double Bid, int LinesCount, int ColumnsCount) : BaseRequiringIdRequest(UserId);