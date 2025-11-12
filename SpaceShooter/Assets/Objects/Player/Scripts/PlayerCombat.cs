using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : ShipCombat
{
    private void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            Fire();
        }
    }
}
