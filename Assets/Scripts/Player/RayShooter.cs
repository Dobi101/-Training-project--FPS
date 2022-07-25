using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    [SerializeField]
    private Transform _firePlace;
    [SerializeField]
    private float _damage = 1f;
    [SerializeField]
    private float _range = 100f;
    private void Start()
    {
        _camera = GameObject.FindObjectOfType<Camera>().GetComponent<Camera>();
        

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit))
        {
            ReactiveTarget reactiveTarget = hit.transform.GetComponent<ReactiveTarget>();
            if(reactiveTarget != null)
            {
                reactiveTarget.ReactToHit();
            }
            Debug.Log("Shoot!");
        }
    }



}
