using System;
using System.Collections.Generic;
using WindHotfix.Core;
using UnityWeld.Binding;
using System.ComponentModel;

namespace Game.Knight.Test
{
    [Binding]
    public class ItemVM
    {
        [Binding]
        public string DisplayText
        {
            get;
            private set;
        }

        public ItemVM(string displayText)
        {
            this.DisplayText = displayText;
        }
    }

    public class UnityWeldTestCase8 : THotfixMB<UnityWeldTestCase8>
    {
        private ObservableList<ItemVM> items = new ObservableList<ItemVM>()
        {
            new ItemVM("1"),
            new ItemVM("2"),
            new ItemVM("3")
        };

        [Binding]
        public ObservableList<ItemVM> Items
        {
            get { return items; }
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
