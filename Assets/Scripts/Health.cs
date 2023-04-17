using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;
    public void GetDamage()
    {
        _health--;
        if(_health == 0)
        {
            Destroy(gameObject);
        }
    }
}
