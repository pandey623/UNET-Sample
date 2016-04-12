using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkSyncPosition : NetworkBehaviour
{
    [SerializeField, Range(0f, 50f)]
    private float lerpRate = 15f;

    [SerializeField]
    private float minThreshold = 0.1f;

    [SerializeField]
    private float snapThreshold = 20f;

    [SyncVar]
    private Vector3 syncPosition;

    private Vector3 prevPosition;

    void FixedUpdate()
    {
        SyncPosition();
        LerpPosition();
    }

    [ClientCallback]
    private void SyncPosition()
    {
        if (hasAuthority)
        {
            if (Vector3.Distance(transform.position, prevPosition) > minThreshold)
            {
                prevPosition = transform.position;
                CmdSyncPosition(transform.position);
            }
        }
    }

    private void LerpPosition()
    {
        if (!hasAuthority)
        {
            if (Vector3.Distance(transform.position, syncPosition) <= snapThreshold)
            {
                transform.position = Vector3.Lerp(transform.position, syncPosition, Time.deltaTime * lerpRate);
            }
            else
            {
                transform.position = syncPosition;
            }            
        }
    }

    [Command]
    private void CmdSyncPosition(Vector3 position)
    {
        syncPosition = position;
    }
}