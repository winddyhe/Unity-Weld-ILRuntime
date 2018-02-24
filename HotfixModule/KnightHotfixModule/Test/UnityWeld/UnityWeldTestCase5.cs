using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;
using WindHotfix.Core;

namespace Game.Knight.Test
{
    public class UnityWeldTestCase5 : THotfixMB<UnityWeldTestCase5>
    {
        private float sliderValue = 2.5f;
        protected int isValid = 1;

        [HotfixBinding("Cube")]
        private GameObject rotatingCube = null;
        
        [Binding]
        public float SliderValue
        {
            get
            {
                return sliderValue;
            }
            set
            {
                if (sliderValue == value)
                {
                    return; // No change.
                }

                sliderValue = value;

                rotatingCube.transform.localEulerAngles = new Vector3(0f, sliderValue, 0f);

                OnPropertyChanged("SliderValue");
            }
        }
        
        [Binding]
        public int IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                if (isValid == value)
                {
                    return;
                }

                isValid = value;

                OnPropertyChanged("IsValid");
            }
        }

        public override void Awake()
        {
            base.Awake();
        }

        public override void Start()
        {
            rotatingCube.transform.localEulerAngles = new Vector3(0f, sliderValue, 0f);
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
