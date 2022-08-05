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
            catch (Exception e)
            {
                Console.WriteLine(e);
                // throw;
            }
        }
    }
}