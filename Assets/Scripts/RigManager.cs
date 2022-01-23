using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigManager : MonoBehaviour
{

    public Rig handRaiseRig;

    float smoothVelocity = 0.25f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandRaise") && !IsInvoking())
        {
            StartCoroutine(ChangeSpeed(0f, 1f, 1f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HandRaise") && !IsInvoking())
        {
            StartCoroutine(ChangeSpeed(1f, 0f, 1f));
        }
    }

    IEnumerator ChangeSpeed(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            handRaiseRig.weight = Mathf.Lerp(v_start, v_end, elapsed / duration);
            //handRaiseRig.weight = Mathf.SmoothDamp(v_start, v_end, ref smoothVelocity, elapsed / duration);
            //handRaiseRig.weight = Mathf.SmoothStep(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        handRaiseRig.weight = v_end;
    }
}
