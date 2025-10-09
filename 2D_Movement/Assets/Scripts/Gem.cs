using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null)
            return;
        if (collision.gameObject.tag != "Player")
            return;

        Destroy(gameObject);
    }
}
