using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class PlayerNetworkSetup : NetworkBehaviour
{
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private AudioListener audioListener;

    void Start()
    {
        if (isLocalPlayer)
        {
            // disable scene camera
            GameObject camera = GameObject.Find("Scene Camera");

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

            // set player name
            gameObject.name = "Player " + netId;
        }
    }
}