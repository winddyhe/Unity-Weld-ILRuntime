using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;
using WindHotfix.Core;

namespace Game.Knight.Test
{
    public class UnityWeldTestCase7 : THotfixMB<UnityWeldTestCase7>
    {
        private string[] options = new string[]
        {
            "Option-A",
            "Option-B",
            "Option-C"
        };

        private string selectedItem = "Option-B";

        [Binding]
        public string SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if (selectedItem == value)
                {
                    return;
                }
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public string[] Options
        {
            get
            {
                return options;
            }
        }
        
        public override void Start()
        {
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
