using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{

    [SerializeField] private int coinAmmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            GameManager.Instance.AddCoin(coinAmmount);
            Destroy(gameObject);
        }
    }
}
