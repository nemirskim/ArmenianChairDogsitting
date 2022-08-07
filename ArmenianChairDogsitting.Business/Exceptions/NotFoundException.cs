namespace ArmenianChairDogsitting.Business;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}
