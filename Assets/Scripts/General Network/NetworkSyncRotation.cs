using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkSyncRotation : NetworkBehaviour
{
    [SerializeField, Range(0f, 50f)]
    private float lerpRate = 15f;

    [SerializeField]
    private float minThreshold = 0.1f;

    [SyncVar]
    private Quaternion syncRotation;

    private Quaternion prevRotation;

    void FixedUpdate()
    {
        SyncRotation();
        LerpRotation();
    }

    [ClientCallback]
    private void SyncRotation()
    {
        if (hasAuthority)
        {
            if (Quaternion.Angle(transform.rotation, prevRotation) > minThreshold)
            {
                prevRotation = transform.rotation;
                CmdSyncRotation(transform.rotation);
            }
        }
    }

    private void LerpRotation()
    {
        if (!hasAuthority)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, syncRotation, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    private void CmdSyncRotation(Quaternion rotation)
    {
        syncRotation = rotation;
    }
}