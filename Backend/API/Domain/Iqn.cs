public class Iqn
{
    private string Value { get; }

    public Iqn(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("Invalid IQN format");

        Value = value;
    }

    private static bool IsValid(string iqn)
    {
        return iqn.StartsWith("iqn.");
    }

    public override string ToString() => Value;
}