using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace API.Speech_AI
{
    public static class SpeechService
    {
        public static IEnumerator UploadSpeech(byte[] speechData)
        {
            var formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormFileSection("file", speechData, "speech.wav", "audio/wav"));

            var postRequest = UnityWebRequest.Post("http://localhost:8000/predict", formData);
            yield return postRequest.SendWebRequest();

            if (postRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(postRequest.error);
            }
            else
            {
                var text = postRequest.downloadHandler.text;
                if (text == "undefined")
                    Debug.Log("Unknown speech recognizer error");
                else
                    Debug.Log(JsonUtility.FromJson<SpeechResult>(postRequest.downloadHandler.text));
            }
        }
    }
}