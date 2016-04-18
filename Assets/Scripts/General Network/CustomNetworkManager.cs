using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CustomNetworkManager : NetworkManager
{
    // server: called when a server is started - including when a host is started
    public override void OnStartServer()
    {
        base.OnStartServer();

        Debug.Log("SERVER: Server has started running.");
    }

    // server: called when a client connects 
    public override void OnServerConnect(NetworkConnection conn)
    {
        /*
         * At the moment, this seems to be called twice after starting a host - one for localClient, and one for localServer.
         * When a remote client connects, it works as expected. When starting a server (only), this is not called.
         * Unsure whether this is the expected behavior. 
         * http://forum.unity3d.com/threads/host-onserverconnect.382752/
         * https://issuetracker.unity3d.com/issues/onserverconnect-function-is-called-twice
         */

        base.OnServerConnect(conn);

        Debug.Log("SERVER: A client has connected.");
    }

    // server: called when a client disconnects
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);

        Debug.Log("SERVER: A client has disconnected.");
    }

    // server: called when a client is ready
    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);

        Debug.Log("SERVER: A client is now ready.");
    }

    // server: called when a scene has completed loading, when the scene load was initiated by the server with ServerChangeScene()
    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);

        Debug.Log("SERVER: Scene has changed.");
    }

    // client: called when connected to a server
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        Debug.Log("CLIENT: Connected to server.");
    }

    // client: called when disconnected from a server
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        Debug.Log("CLIENT: Disconnected from server.");
    }

    // client: called when a scene has completed loading, when the scene load was initiated by the server
    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);

        Debug.Log("CLIENT: Scene has changed.");
    }
}