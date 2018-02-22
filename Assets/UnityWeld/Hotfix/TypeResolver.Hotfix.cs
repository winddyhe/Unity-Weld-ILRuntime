using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Framework.Hotfix;
using System.Reflection;
using UnityWeld.Binding.Exceptions;

namespace UnityWeld.Binding.Internal
{
    public static partial class TypeResolver
    {
        private static Type[] typesWithBindingAttribute_Hotfix;

        public static IEnumerable<Type> TypesWithBindingAttribute_Hotfix
        {
            get
            {
                //if (typesWithBindingAttribute_Hotfix == null)
                {
                    typesWithBindingAttribute_Hotfix = FindTypesMarkedByAttribute_Hotfix(typeof(BindingAttribute));
                }
                return typesWithBindingAttribute_Hotfix;
            }
        }

        private static Type[] FindTypesMarkedByAttribute_Hotfix(Type attributeType)
        {
            var typesFound = new List<Type>();
            foreach (var type in GetAllTypes_Hotfix())
            {
                try
                {
                    if (type.GetCustomAttributes(attributeType, false).Any())
                    {
                        typesFound.Add(type);
                    }
                }
                catch (Exception)
                {
                }
            }
            return typesFound.ToArray();
        }

        private static IEnumerable<Type> GetAllTypes_Hotfix()
        {
            //return HotfixManager.Instance.GetTypes();
            string rHotfixDllPath = "Assets/Game/Knight/GameAsset/Hotfix/Libs/KnightHotfixModule.bytes";
            Assembly rHotfixAssembly = Assembly.LoadFile(rHotfixDllPath);
            return rHotfixAssembly.GetTypes();
        }

        private static IEnumerable<Type> FindAvailableViewModelTypes_Hotfix(AbstractMemberBinding memberBinding)
        {
            var foundAtLeastOneBinding = false;

            var trans = memberBinding.transform;
            var components = trans.GetComponentsInParent<HotfixMBContainer>(true);
            foreach (var component in components)
            {
                if (component == null || component == memberBinding)
                {
                    continue;
                }

                var viewModelTypeName = component.HotfixName;
                if (string.IsNullOrEmpty(viewModelTypeName))
                {
                    continue;
                }

                foundAtLeastOneBinding = true;

                Debug.LogError(GetViewModelType_Hotfix(component.HotfixName));
                yield return GetViewModelType_Hotfix(component.HotfixName);
            }

            if (!foundAtLeastOneBinding)
            {
                Debug.LogError("UI binding " + memberBinding.gameObject.name + " must be placed underneath at least one bindable component.", memberBinding);
            }
        }

        public static BindableMember<PropertyInfo>[] FindBindableProperties_Hotfix(AbstractMemberBinding target)
        {
            return FindAvailableViewModelTypes_Hotfix(target)
                .SelectMany(type => GetPublicProperties(type)
                    .Select(p => new BindableMember<PropertyInfo>(p, type))
                )
                .Where(p => p.Member
                    .GetCustomAttributes(typeof(BindingAttribute), false)
                    .Any() // Filter out properties that don't have [Binding].
                )
                .ToArray();
        }


        private static Type GetViewModelType_Hotfix(string viewModelTypeName)
        {
            Debug.LogError(viewModelTypeName);
            var type = TypesWithBindingAttribute_Hotfix
                .FirstOrDefault(t => t.ToString() == viewModelTypeName);

            if (type == null)
            {
                //throw new ViewModelNotFoundException("Could not find the specified view model \"" + viewModelTypeName + "\"");
            }

            return type;
        }
    }
}
