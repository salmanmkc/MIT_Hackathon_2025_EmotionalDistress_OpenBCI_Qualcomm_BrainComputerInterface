using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HS_CallBackParent : MonoBehaviour
{
    [SerializeField]protected Transform parentObject;

    protected virtual void OnParticleSystemStopped()
    {
        if (parentObject != null)
        {
            transform.parent = parentObject;
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
        }
        else
            Destroy(gameObject);
    }
}
