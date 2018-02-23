using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using WindHotfix.Core;

namespace Game.Knight.Test
{
    [UnityWeld.Binding.Binding]
    public class UnityWeldTestCase1 : THotfixMB<UnityWeldTestCase1>
    {
        private float timer = 0;

        private string text = "1234";
        [UnityWeld.Binding.Binding]
        public  string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text == value) return; // No change.
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public override void Start()
        {
            SetRandomText();
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            
            if (timer >= 1f)
            {
                SetRandomText();
                timer = 0f;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void SetRandomText()
        {
            Text = UnityEngine.Random.Range(0f, 10000f).ToString();
        }
    }
}
