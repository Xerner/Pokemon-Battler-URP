using Microsoft.Extensions.Options;

namespace AutoChess.Infrastructure.Options;

public interface IAutoChessInfrastructureOptions : IOptions<IAutoChessInfrastructureOptions>
{
    public const string Key = "AutoChessInfrastructure";
    string ConnectionString { get; }
}
