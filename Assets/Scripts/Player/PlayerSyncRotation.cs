using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerSyncRotation : NetworkBehaviour
{
    [SerializeField] private Transform camTransform;
    [SerializeField] private float lerpRate = 15f;

    [SyncVar] private Quaternion syncPlayerRot;
    [SyncVar] private Quaternion syncCamRot;

    private Quaternion lastPlayerRot;
    private Quaternion lastCamRot;
    private float threshold = 5f;

    void FixedUpdate()
    {
        SyncRotations();
        LerpRotations();
    }

    [ClientCallback]
    private void SyncRotations()
    {
        if (isLocalPlayer && (Quaternion.Angle(transform.rotation, lastPlayerRot) > threshold || Quaternion.Angle(camTransform.rotation, lastCamRot) > threshold))
        {
            lastPlayerRot = transform.rotation;
            lastCamRot = camTransform.rotation;
            CmdSyncRotations(transform.rotation, camTransform.rotation);
        }
    }

    private void LerpRotations()
    {
        if (!isLocalPlayer)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, syncPlayerRot, Time.deltaTime * lerpRate);
            camTransform.rotation = Quaternion.Lerp(camTransform.rotation, syncCamRot, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    private void CmdSyncRotations(Quaternion playerRot, Quaternion camRot)
    {
        syncPlayerRot = playerRot;
        syncCamRot = camRot;
    }
}