namespace BusinessLogic.ApiServices.Requests;

public record SlotSpinRequest(double Bid, int LinesCount, int ColumnsCount) : BaseRequest();