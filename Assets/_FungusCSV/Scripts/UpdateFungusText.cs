using System;
using Cysharp.Threading.Tasks;
using Fungus;
using UnityEngine;

namespace _FungusCSV
{
    public class UpdateFungusText : MonoBehaviour
    {
        private Localization _localization;
        [SerializeField] private GameStarted gameStarted;

        private void Awake()
        {
            _localization = GetComponent<Localization>();
        }

        private async void Start()
        {
            try
            {
                var data = await LoadTextData.GetGameInfo<TextDataList>(this.GetCancellationTokenOnDestroy());
                var fungusText = LoadTextData.ConvertFungusTextFormat(data);
                _localization.SetStandardText(fungusText);
                gameStarted.enabled = true;
            }
            catch (OperationCanceledException)
            {
                // GameObject が破棄されて読み込みが中断された
            }
            catch (Exception e)
            {
                // Console.WriteLine は Unity のコンソールに出ないため、失敗が見えなくなっていた
                Debug.LogException(e, this);
            }
        }
    }
}