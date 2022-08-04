namespace ArmenianChairDogsitting.Business;

public class AccessDeniedException : Exception
{
    public AccessDeniedException(string message) : base(message)
    {
    }
}
