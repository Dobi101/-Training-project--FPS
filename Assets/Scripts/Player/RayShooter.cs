using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    private Ray _ray;
    private RaycastHit _hit;
    private Vector3 _aim;

    [SerializeField] private int _sizeUIAim = 20;
    [SerializeField] private int _timeToDwstroySphere = 20;



    private void Start()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnGUI()
    {
        float posX = _camera.pixelWidth / 2 - _sizeUIAim / 4;
        float posY = _camera.pixelHeight / 2 - _sizeUIAim / 2;
        GUI.Label(new Rect(posX, posY, _sizeUIAim, _sizeUIAim), "*");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _aim = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            _ray = _camera.ScreenPointToRay(_aim);
            if (Physics.Raycast(_ray, out _hit))
            {
                GameObject hitObject = _hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicator(_hit.point));
                }
               

            }
            
        }
    }

    private IEnumerator SphereIndicator(Vector3 position)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = position;

        yield return new WaitForSeconds(_timeToDwstroySphere);

        Destroy(sphere);
    }
}
