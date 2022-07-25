using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    //[SerializeField]
    //private Transform _startPoint;
    [SerializeField] 
    private int _healthAmount;
    private Vector3 _respawnPoint;
    private int _health;

    private void Start()
    {
        _health = _healthAmount;
        _respawnPoint = transform.position;
    }

    public void Hurt(int damage)
    {
        _health -= damage; 
        Debug.Log("Health: " + _health);
        if(_health <= 0)
        {
            Die();
        }
    }

    public void ChangeRespawnPoint(Vector3 newRespawnPoint)
    {
        _respawnPoint = newRespawnPoint;
    }

    public void Die()
    {
        transform.position = _respawnPoint;
    }
}
