using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallForRun : MonoBehaviour
{
    [SerializeField]
    private WallRun _wallrun;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _wallrun.TryStartWallRun();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _wallrun.StopWallRun();
        }
    }
}
