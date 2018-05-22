using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mango : MonoBehaviour
{
    public StageManager StageManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            StageManager.Win(player);
        }
    }
}
