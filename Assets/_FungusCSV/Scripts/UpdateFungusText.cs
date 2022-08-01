using System;
using Fungus;
using UnityEngine;

namespace _FungusCSV
{
    public class UpdateFungusText : MonoBehaviour
    {
        [SerializeField] private TextDataList textDataList;
        [SerializeField] private Flowchart flowchart;
        private Localization _localization;

        private void Awake()
        {
            _localization = GetComponent<Localization>();
        }

        private async void Start()
        {
            var data = await LoadTextData.GetGameInfo<TextDataList>();
            textDataList = data;
            var fungusText = LoadTextData.ConvertFungusTextFormat(data);
            Debug.Log(fungusText);
            _localization.SetStandardText(fungusText);
            flowchart.enabled = true;
        }
    }
}