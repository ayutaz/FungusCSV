using System;
using System.Collections.Generic;

namespace _FungusCSV
{
    [Serializable]
    public class TextData
    {
        public string Key;
        public string Description;
        public string Standard;
    }

    [Serializable]
    public class TextDataList
    {
        public List<TextData> textData;
    }
}