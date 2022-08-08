namespace ArmenianChairDogsitting.Business;

public class ExistingEmailException : Exception
{
    public ExistingEmailException(string message) : base(message)
    {
    }
}
