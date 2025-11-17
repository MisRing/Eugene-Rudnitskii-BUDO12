using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : ShipCombat
{
    private EnemyShipService _shipService;
    [SerializeField] private LayerMask _targets;

    private void Awake()
    {
        _shipService = GetComponent<EnemyShipService>();
    }

    private void Update()
    {
        if (!_shipService.IsControllable) return;

        switch(_shipService.FireType)
        {
            case FireType.OnCooldown:
                Fire(Quaternion.Euler(0f, 180f, 0f));
                break;
            case FireType.WhenSeeTarget:
                FireIfOnLine();
                break;
        }
    }

    private void FireIfOnLine()
    {
        if(Physics.Raycast(transform.position, transform.forward, 100f, _targets))
        {
            Fire(Quaternion.Euler(0f, 180f, 0f));
        }
    }
}

public enum FireType
{
    None,
    OnCooldown,
    WhenSeeTarget
}