using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace _FungusCSV
{
    public class LoadTextData
    {
        private const string URL = "https://script.google.com/macros/s/AKfycbzyK8eY1JpBGWrNM8TAa731Op0jFwqI1x0xsuBc9BZgb9qpz7I0PHf2nHdJTHt3hqCk-Q/exec";
        private const string SheetName = "info";

        /// <summary>
        /// ゲーム情報をスプレッドシートから取得
        /// </summary>
        /// <returns></returns>
        public static async UniTask<T> GetGameInfo<T>(CancellationToken token)
        {
            var request = UnityWebRequest.Get($"{URL}?sheetName={SheetName}");
            await request.SendWebRequest().ToUniTask(cancellationToken: token);
            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError or UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log("fail to get card info from google sheet");
                throw new Exception(request.error);
            }

            var json = request.downloadHandler.text;
            var data = JsonUtility.FromJson<T>(json);
            return data;
        }

        public static string ConvertFungusTextFormat(TextDataList data)
        {
            var fungusText = "";
            foreach (var info in data.textData)
            {
                fungusText += $"#{info.Key}\n";
                fungusText += $"{info.Standard}\n";
                fungusText += "\n";
            }

            return fungusText;
        }
    }
}