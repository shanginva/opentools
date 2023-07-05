using System.Windows.Input;

namespace OpenTools.MVVM;
public class ActionCommand : ICommand
{
    private readonly Action<object?> execute;
    private readonly Func<object?, bool> canExecute;

    public event EventHandler? CanExecuteChanged;

    public ActionCommand(Action<object?> execute, Func<object?, bool> canExecute)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
        => canExecute(parameter);

    public void Execute(object? parameter)
        => execute(parameter);

    public void ChangeCanExecute() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
