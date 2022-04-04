using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class WebRequests {
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

    /// <summary>
    /// Supports downloading text and textures from the web. Downloading Textures with this method is kind of buggy.
    /// </summary>
    /// <param name="onSuccess">The downloaded data will be available as the first argument of this Action</param>
    private static IEnumerator GetCoroutine<T>(string url, Action<string> onError, Action<T> onSuccess) {
        // switch cases do not work here :(
        if (typeof(T) == typeof(string)) {
            using (UnityWebRequest request = UnityWebRequest.Get(url)) {
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                    onError.Invoke(request.error);
                else
                    onSuccess.Invoke((T)Convert.ChangeType(request.downloadHandler.text, typeof(T)));
            }
        } else if (typeof(T) == typeof(Texture2D)) {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url)) {
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
                    onError.Invoke(request.error);
                } else {
                    // TODO: figure out how to download gifs
                    DownloadHandlerTexture downloadHandler = (DownloadHandlerTexture)request.downloadHandler;
                    onSuccess.Invoke((T)Convert.ChangeType(downloadHandler.texture, typeof(T)));
                }
            }
        } else {
            Debug.LogError("Unsupported WebRequest type: " + typeof(T).ToString());
        }
    }
}
