using System;
using UnityWeld.Binding;

[Adapter(typeof(Exception), typeof(int))]
public class ExceptionValidationAdapter : IAdapter
{
    public object Convert(object valueIn, AdapterOptions options)
    {
        return (Exception)valueIn == null ? 1 : 0;
    }
}
