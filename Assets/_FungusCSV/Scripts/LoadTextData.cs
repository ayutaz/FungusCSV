using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace _FungusCSV
{
    public class LoadTextData
    {
        private const string url = "https://script.google.com/macros/s/AKfycbzyK8eY1JpBGWrNM8TAa731Op0jFwqI1x0xsuBc9BZgb9qpz7I0PHf2nHdJTHt3hqCk-Q/exec";
        private const string sheetName = "info";

        /// <summary>
        /// ゲーム情報をスプレッドシートから取得
        /// </summary>
        /// <returns></returns>
        public static async UniTask<T> GetGameInfo<T>()
        {
            var request = UnityWebRequest.Get($"{url}?sheetName={sheetName}");
            await request.SendWebRequest();
            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError or UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log("fail to get card info from google sheet");
            }
            else
            {
                var json = request.downloadHandler.text;
                var data = JsonUtility.FromJson<T>(json);
                return data;
            }

            return default;
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