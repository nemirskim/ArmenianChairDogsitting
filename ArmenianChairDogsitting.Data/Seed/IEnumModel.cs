namespace ArmenianChairDogsitting.Data.Seed;

public interface IEnumModel<TModel, TModelIdType>
{
    TModelIdType Id { get; set; }
}
