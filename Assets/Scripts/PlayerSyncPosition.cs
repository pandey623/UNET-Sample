using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerSyncPosition : NetworkBehaviour
{
    [SerializeField] private float lerpRate = 15f;

    [SyncVar] private Vector3 syncPos;

    private Vector3 lastPos;
    private float threshold = 0.5f;

    void FixedUpdate()
    {
        SyncPosition();
        LerpPosition();
    }

    [ClientCallback]
    private void SyncPosition()
    {
        if (isLocalPlayer && Vector3.Distance(transform.position, lastPos) > threshold)
        {
            lastPos = transform.position;
            CmdSyncPosition(transform.position);
        }
    }

    private void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    private void CmdSyncPosition(Vector3 pos)
    {
        syncPos = pos;
    }
}