using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using WindHotfix.Core;

namespace Game.Knight.Test
{
    public class UnityWeldTestCase4 : THotfixMB<UnityWeldTestCase4>
    {
        private float cubeRotation = 0f;

        private string text = "0";

        [UnityWeld.Binding.Binding]
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text == value) return;

                text = value;

                try
                {
                    cubeRotation = float.Parse(text);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
                OnPropertyChanged("Text");
            }
        }
        
        [UnityWeld.Binding.Binding]
        public void RotateCube()
        {
            cubeRotation = cubeRotation + 10f;
            text = cubeRotation.ToString();

            OnPropertyChanged("Text");
        }

        public override void Update()
        {
            var cube = GameObject.Find("Cube");
            cube.transform.localEulerAngles = new Vector3(0f, cubeRotation, 0f);
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
