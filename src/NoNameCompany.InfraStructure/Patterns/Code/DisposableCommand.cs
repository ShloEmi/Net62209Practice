namespace NoNameCompany.InfraStructure.Patterns.Code;

public class DisposableCommand : IDisposable
{
    private readonly Action action;


    public DisposableCommand(Action action) =>
        this.action = action;

    public void Dispose() =>
        action() /* TODO: Shlomi, try-catch + log! */;
}
