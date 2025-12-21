using UnityEngine;

[CreateAssetMenu(
    fileName = "OrbitingBladesData",
    menuName = "Abilities/Active/Orbiting Blades"
)]
public class OrbitingBladesData : AbilityData
{
    [Header("Blades Settings")]
    public GameObject bladePrefab;

    public int bladeCount = 2;
    public int damage = 5;
    public float radius = 1.8f;
    public float rotationSpeed = 180f;
}
