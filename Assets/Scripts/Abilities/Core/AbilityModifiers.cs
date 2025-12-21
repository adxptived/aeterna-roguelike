using System;
using UnityEngine;

public class AbilityModifiers : MonoBehaviour
{
    public float auraRadiusMultiplier = 1f;
    public float auraTickMultiplier = 1f;

    public event Action OnModifiersChanged;

    public void ModifyAuraRadius(float multiplier)
    {
        auraRadiusMultiplier *= multiplier;
        OnModifiersChanged?.Invoke();
    }

    public void ModifyAuraTick(float multiplier)
    {
        auraTickMultiplier *= multiplier;
        OnModifiersChanged?.Invoke();
    }
}
