using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public struct DamageData
{
    public int Damage;
    public float CriticalDamageMultiplier;
    public bool IsCritical;
    public bool IsArmorIgnore;
    public int ArmorBrakeAmount;

}
