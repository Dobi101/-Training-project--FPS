using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandaringAI : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _obstacleRange = 5.0f;
    [SerializeField] private GameObject _fireballPrefab;
    [SerializeField] private float _speedFireball = 1.5f;
    
    private GameObject _fireball;
    private bool _alive;

    private void Start()
    {
        _alive = true;
    }

    private void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, _speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject  = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if(_fireball == null)
                    {
                        _fireball = Instantiate(_fireballPrefab) as GameObject;
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * _speedFireball);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < _obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void SetAlive (bool alive)
    {
        _alive = alive;
    }
}
