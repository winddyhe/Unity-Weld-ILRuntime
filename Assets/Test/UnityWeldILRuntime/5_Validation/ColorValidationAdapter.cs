using Assets._5_Validation;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace ValidationExample
{
    [Adapter(typeof(int), typeof(ColorBlock), typeof(ColorValidationAdapterOptions))]
    public class ColorValidationAdapter : IAdapter
    {
        public object Convert(object valueIn, AdapterOptions adapterOptions)
        {
            var isValid = (int)valueIn;
            var options = (ColorValidationAdapterOptions) adapterOptions;

            return isValid == 1 ? options.NormalColor : options.InvalidColor;
        }
    }
}
