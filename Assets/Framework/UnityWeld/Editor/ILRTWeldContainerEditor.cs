using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityWeld.Binding.Internal;
using UnityWeld_Editor;

namespace UnityWeld.Binding.ILRT.Editor
{
    //[CustomEditor(typeof(ILRTWeldContainer))]
    public class ILRTWeldContainerEditor : BaseBindingEditor
    {
        private ILRTWeldContainer   targetScript;
        private bool                propertyPrefabModified;

        private void OnEnable()
        {
            targetScript = (ILRTWeldContainer)target;
        }

        public override void OnInspectorGUI()
        {
            if (CannotModifyInPlayMode())
            {
                GUI.enabled = false;
            }

            UpdatePrefabModifiedProperties();

            var availableViewModels = TypeResolver.TypesWithBindingAttribute
                .Select(type => type.ToString())
                .OrderBy(name => name)
                .ToArray();

            var selectedIndex = Array.IndexOf(
                availableViewModels,
                targetScript.ViewModelTypeName
            );

            var defaultLabelStyle = EditorStyles.label.fontStyle;
            EditorStyles.label.fontStyle = propertyPrefabModified
                ? FontStyle.Bold
                : defaultLabelStyle;

            var newSelectedIndex = EditorGUILayout.Popup(
                new GUIContent(
                    "Template view model",
                    "Type of the view model that this template will be bound to when it is instantiated."
                ),
                selectedIndex,
                availableViewModels
                    .Select(viewModel => new GUIContent(viewModel))
                    .ToArray()
            );

            EditorStyles.label.fontStyle = defaultLabelStyle;

            UpdateProperty(newValue => targetScript.ViewModelTypeName = newValue,
                selectedIndex < 0
                    ? string.Empty
                    : availableViewModels[selectedIndex],
                newSelectedIndex < 0
                    ? string.Empty
                    : availableViewModels[newSelectedIndex],
                "Set bound view-model for template"
            );
        }

        private void UpdatePrefabModifiedProperties()
        {
            var property = serializedObject.GetIterator();
            property.Next(true);
            do
            {
                if (property.name == "viewModelTypeName")
                {
                    propertyPrefabModified = property.prefabOverride;
                }
            }
            while (property.Next(false));
        }
    }
}
