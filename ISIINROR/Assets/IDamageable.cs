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

public interface IDamageDealler
{

}

public struct DamageData
{
    public int Damage;

    public IDamageDealler Source;
    public LayerMask Targets;
    public Vector2 Direction;   // из какого направлени€ пришЄл удар Ч полезно дл€ нокбэков

    public bool IsCritical;
    public float CriticalMultiplier;

    public bool IgnoreArmor;
    public int ArmorBreak;

    public float KnockbackForce; // если планируешь физическое отбрасывание

    public DamageData(
        int damage,
        IDamageDealler source,
        LayerMask targets,
        Vector2 direction,
        bool isCritical = false,
        float critMultiplier = 1.5f)
    {
        Damage = damage;
        Source = source;
        Targets = targets;
        Direction = direction;
        IsCritical = isCritical;
        CriticalMultiplier = critMultiplier;
        IgnoreArmor = false;
        ArmorBreak = 0;
        KnockbackForce = 0f;
    }
}

