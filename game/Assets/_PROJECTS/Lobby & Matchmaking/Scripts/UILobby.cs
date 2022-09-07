﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.Networking;
using System.Text;

public class UILobby : MonoBehaviour
{
    public class MatchData
    {
        public string match_id;
        public int port;
    }

    public class JoinMatchRequest
    {
        public string match_id;
    }

    public class Hello
    {
        public string message;
    }

    [SerializeField] InputField joinMatchInput;

    public void HostPublic()
    {
        Debug.Log("--- Host Public ---");
        StartCoroutine(HostPublicRequest());
    }

    private IEnumerator HostPublicRequest()
    {
        var postRequest = CreateRequest("http://52.29.249.251:3000/host_public", RequestType.GET, null);
        yield return postRequest.SendWebRequest();
        var res = JsonUtility.FromJson<MatchData>(postRequest.downloadHandler.text);
        Debug.Log(res.match_id);
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<TelepathyTransport>().port = (ushort)res.port;
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>().StartClient();
    }

    public void HostPrivate()
    {
        Debug.Log("--- HostPrivate ---");
        StartCoroutine(HostPrivateRequest());
    }

    private IEnumerator HostPrivateRequest()
    {
        var postRequest = CreateRequest("http://52.29.249.251:3000/host_private", RequestType.GET, null);
        yield return postRequest.SendWebRequest();
        var res = JsonUtility.FromJson<MatchData>(postRequest.downloadHandler.text);
        Debug.Log(res.match_id);
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<TelepathyTransport>().port = (ushort)res.port;
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>().StartClient();
    }

    public void SearchGame()
    {
        Debug.Log("--- SearchGame ---");
    }

    private IEnumerator SearchRequest()
    {
        var postRequest = CreateRequest("http://52.29.249.251:3000/search_match", RequestType.POST, null);
        yield return postRequest.SendWebRequest();
        var res = JsonUtility.FromJson<MatchData>(postRequest.downloadHandler.text);
        Debug.Log(res.port);
        Debug.Log(res.match_id);
    }

    public void JoinGame()
    {
        Debug.Log("--- JoinGame ---");
        Debug.Log("---  \"" + joinMatchInput.text + "\" ---");
        StartCoroutine(JoinRequest(new JoinMatchRequest() { match_id = joinMatchInput.text }));
    }

    private IEnumerator JoinRequest(JoinMatchRequest dataToPost)
    {
        var postRequest = CreateRequest("http://52.29.249.251:3000/join_match", RequestType.POST, dataToPost);
        yield return postRequest.SendWebRequest();
        var res = JsonUtility.FromJson<MatchData>(postRequest.downloadHandler.text);
        Debug.Log(res.match_id);
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<TelepathyTransport>().port = (ushort)res.port;
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManager>().StartClient();
    }

    // ------------------------------------------------------------------------------------------------------

    private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null)
    {
        var request = new UnityWebRequest(path, type.ToString());

        if (data != null)
        {
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }

    private void AttachHeader(UnityWebRequest request, string key, string value)
    {
        request.SetRequestHeader(key, value);
    }

    public enum RequestType
    {
        GET = 0,
        POST = 1,
        PUT = 2
    }
}