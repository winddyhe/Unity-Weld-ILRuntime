using System;
using System.Collections.Generic;
using UnityEngine;
using UnityWeld.Binding.Exceptions;
using UnityWeld.Binding.Internal;

namespace UnityWeld.Binding
{
    public class HotfixViewModelBinding : AbstractMemberBinding, IViewModelProvider
    {
        [SerializeField]
        private string viewModelTypeName;
        public string ViewModelTypeName
        {
            get { return viewModelTypeName; }
            set { viewModelTypeName = value; }
        }
        private object viewModel;
        private PropertyWatcher viewModelPropertyWatcher;

        [SerializeField]
        private string viewModelPropertyName;
        public string ViewModelPropertyName
        {
            get { return viewModelPropertyName; }
            set { viewModelPropertyName = value; }
        }

        public object GetViewModel()
        {
            if (viewModel == null)
            {
                Connect();
            }
            return viewModel;
        }

        public string GetViewModelTypeName()
        {
            return viewModelTypeName;
        }

        public override void Connect()
        {
            if (viewModelPropertyWatcher != null)
            {
                return;
            }

            string propertyName;
            object parentViewModel;
            ParseViewModelEndPointReference(viewModelPropertyName, out propertyName, out parentViewModel);

            viewModelPropertyWatcher = new PropertyWatcher(parentViewModel, propertyName, NotifyPropertyChanged_PropertyChanged);

            UpdateViewModel();
        }

        public override void Disconnect()
        {
            if (viewModelPropertyWatcher != null)
            {
                viewModelPropertyWatcher.Dispose();
                viewModelPropertyWatcher = null;
            }
        }

        private void UpdateViewModel()
        {
            string propertyName;
            object parentViewModel;
            ParseViewModelEndPointReference(viewModelPropertyName, out propertyName, out parentViewModel);

            var propertyInfo = parentViewModel.GetType().GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new MemberNotFoundException(string.Format("Could not find property \"{0}\" on view model \"{1}\".", propertyName, parentViewModel.GetType()));
            }

            viewModel = propertyInfo.GetValue(parentViewModel, null);
        }

        private void NotifyPropertyChanged_PropertyChanged()
        {
            UpdateViewModel();

            // Rebind all children.
            foreach (var memberBinding in GetComponentsInChildren<AbstractMemberBinding>())
            {
                if (memberBinding == this)
                {
                    continue;
                }
                memberBinding.Init();
            }
        }
    }
}
