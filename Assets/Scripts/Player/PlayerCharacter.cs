using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int _healthAmount;
    private int _health;

    private void Start()
    {
        _health = _healthAmount;
    }

    public void Hurt(int damage)
    {
        _health -= damage; 
        Debug.Log("Health: " + _health);
        if(_health <= 0)
        {
            Debug.Log("Death");
            Application.Quit();
        }
    }
}
