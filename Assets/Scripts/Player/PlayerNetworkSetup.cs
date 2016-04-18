using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class PlayerNetworkSetup : NetworkBehaviour
{
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private AudioListener audioListener;

    // called on every NetworkBehaviour when it is activated on a client
    public override void OnStartClient()
    {
        base.OnStartClient();

        // set player name
        gameObject.name = "Player " + netId;
    }

    // called by an ownership message from the server when the local player object has been set up, after OnStartClient()
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // disable scene camera
        GameObject camera = Camera.main.gameObject;

        if (camera)
        {
            camera.GetComponent<AudioListener>().enabled = false;
            camera.GetComponent<Camera>().enabled = false;
        }

        // enable fps camera
        GetComponent<FirstPersonController>().enabled = true;
        GetComponent<CharacterController>().enabled = true;

        fpsCamera.enabled = true;
        audioListener.enabled = true;

        // set local player name
        gameObject.name = gameObject.name + " (Local)";
    }
}