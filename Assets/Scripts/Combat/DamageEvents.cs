using System;

public static class DamageEvents
{
    // amount = сколько урона реально нанесено
    public static event Action<int> OnDamageDealt;

    public static void RaiseDamageDealt(int amount)
    {
        OnDamageDealt?.Invoke(amount);
    }
}
