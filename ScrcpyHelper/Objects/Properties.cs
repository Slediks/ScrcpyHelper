namespace ScrcpyHelper.Objects;

public abstract class Properties
{
    public const string Brackets = "[]";
    protected const string Change = "Change";
    protected const string Open = "Open";
    
    protected static readonly string Changeable = $"{Brackets[0]}{Change}{Brackets[1]}";
    protected static readonly string Openable = $"{Brackets[0]}{Open}{Brackets[1]}";
    protected static readonly string ChangeableOpenable = Changeable + ' ' + Openable;

    public abstract void ChangeProps();
    public abstract override string ToString();
    public abstract string GetProperties();
}