using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private PlayerHealth ph;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ph.TakeDamage(20);
        }
    }
}
