using UnityEngine;
using System.Collections.Generic;

public class OrbitingBladesAbility : ActiveAbility, IAbilityWithData
{
    private OrbitingBladesData data;

    private List<GameObject> blades = new();
    private float currentAngle;

    public void Init(AbilityData abilityData)
    {
        data = abilityData as OrbitingBladesData;

        if (data == null)
        {
            Debug.LogError("Wrong data type passed to OrbitingBladesAbility");
            return;
        }

        SpawnBlades();
    }

    private void Update()
    {
        RotateBlades();
    }

    void SpawnBlades()
    {
        for (int i = 0; i < data.bladeCount; i++)
        {
            float angle = i * (360f / data.bladeCount);
            Vector3 pos = GetPosition(angle);

            GameObject blade = Instantiate(
                data.bladePrefab,
                transform.position + pos,
                Quaternion.identity,
                transform
            );

            blade.GetComponent<Blade>().damage = data.damage;
            blades.Add(blade);
        }
    }

    void RotateBlades()
    {
        currentAngle += data.rotationSpeed * Time.deltaTime;

        for (int i = 0; i < blades.Count; i++)
        {
            float angle = currentAngle + i * (360f / blades.Count);
            blades[i].transform.localPosition = GetPosition(angle);
        }
    }

    Vector3 GetPosition(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(
            Mathf.Cos(rad) * data.radius,
            Mathf.Sin(rad) * data.radius,
            0f
        );
    }
}
