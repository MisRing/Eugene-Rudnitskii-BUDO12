using UnityEngine;

public class PlayerOrderHandler : MonoBehaviour
{
    public UnitMovement unit;

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            unit.AddOrder(new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)), !Input.GetKeyDown(KeyCode.LeftShift));
        }
    }
}
