using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class WebRequests
{
    private class WebRequestsMB : MonoBehaviour { }

    private static WebRequestsMB webRequestsMB;

    private static void init() {
        if (webRequestsMB == null) {
            webRequestsMB = new GameObject("WebRequests").AddComponent<WebRequestsMB>();
        }
    }

    public static void Get<T>(string url, Action<string> onError, Action<T> onSuccess) {
        init();
        webRequestsMB.StartCoroutine(GetCoroutine(url, onError, onSuccess));
    }

    private static IEnumerator GetCoroutine<T>(string url, Action<string> onError, Action<T> onSuccess) {
        using (UnityWebRequest uwr = UnityWebRequest.Get(url)) {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError) {
                onError.Invoke(uwr.error);
            } else {
                // switch cases do not work here :(
                if (typeof(T)==typeof(string)) {
                    onSuccess.Invoke((T)Convert.ChangeType(uwr.downloadHandler.text, typeof(T)));
                } else if (typeof(T) == typeof(Texture2D)) {
                    var downloadHandler = uwr.downloadHandler as DownloadHandlerTexture;
                    onSuccess.Invoke((T)Convert.ChangeType(downloadHandler.texture, typeof(T)));
                } else {
                    Debug.LogError("Unsupported WebRequest type: " + typeof(T).ToString());
                }
            }
        }
    }
}
