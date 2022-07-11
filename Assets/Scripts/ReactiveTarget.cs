using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    [SerializeField] private int _timeToDie = 1;
    public void ReactToHit()
    {
        WandaringAI behavior = GetComponent<WandaringAI>();
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);

        yield return new WaitForSeconds(_timeToDie);
        
        Destroy(this.gameObject);
    }
}
