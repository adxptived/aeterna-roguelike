public interface IAbilityFactory
{
    bool Grant(AbilityHolder holder);
    bool Has(AbilityHolder holder);
}
