namespace ArmenianChairDogsitting.Business.Exceptions;

public class ExceptionMessage
{
    public const string ChoosenSitterDoesNotExist = "Нет ситера с id = ";
    public const string ChoosenAdminDoesNotExist = "Нет администратора с id = ";
    public const string NoCommentsYet = "Комментариев еще нет";
    public const string ChoosenCommentDoesNotExist = "Нет комментария с id = ";
    public const string ChoosenOrderDoesNotExist = "Нет заказа с id = ";
    public const string NoOrdersYet = "Заказов еще нет";
    public const string ActionIsNotAllowed = "Действие недоступно";
    public const string OldPasswordEqualNew = "Введите новый пароль отличный от старого";
}
