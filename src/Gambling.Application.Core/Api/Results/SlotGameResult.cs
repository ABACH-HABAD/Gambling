using Gambling.Application.Core.BusinessModels.GameModels.Slots;

namespace Gambling.Application.Core.Api.Results;

public record SlotGameResult(bool Result, string Message, double Win, List<List<SlotElement>> Elements) : GameResult(Result, Message, Win);