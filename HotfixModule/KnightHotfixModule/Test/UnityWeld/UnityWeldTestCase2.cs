using System;
using System.Collections.Generic;
using System.ComponentModel;
using WindHotfix.Core;

namespace Game.Knight.Test
{
    [UnityWeld.Binding.Binding]
    public class UnityWeldTestCase2 : THotfixMB<UnityWeldTestCase2>
    {
        private string text = "<Type some text>";

        [UnityWeld.Binding.Binding]
        public string Text
        {
            get { return text; }
            set
            {
                if (text == value) return; // No change.
                text = value;
                OnPropertyChanged("Text");
            }
        }
        
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
