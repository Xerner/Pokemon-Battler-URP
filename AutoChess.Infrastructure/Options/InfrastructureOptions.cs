using Microsoft.Extensions.Options;

namespace AutoChess.Infrastructure.Options;

public class InfrastructureOptions
{
    public const string Key = "AutoChessInfrastructure";
    public string ConnectionString { get; } = "";
}
