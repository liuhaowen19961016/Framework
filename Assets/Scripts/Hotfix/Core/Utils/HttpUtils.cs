using System;
using System.Collections;
using System.Text;
using UnityEngine.Networking;

public static class HttpUtils
{
    public static IEnumerator Post(string url, string jsonData, Action<string> onSuccess, Action<string> onFail = null)
    {
        var request = new UnityWebRequest(url);
        request.method = "POST";
        request.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
        //request.certificateHandler = new WebReqSkipCert();//跳过ssl验证
        request.downloadHandler = new DownloadHandlerBuffer();
        byte[] byteArray = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(byteArray);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            onSuccess?.Invoke(request.downloadHandler.text);
            //CommonLog.Info($"[HTTP Success] {request.downloadHandler.text}");
        }
        else
        {
            onFail?.Invoke(request.error);
        }
        request.Abort();
        request.Dispose();
    }

    public class WebReqSkipCert : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}
