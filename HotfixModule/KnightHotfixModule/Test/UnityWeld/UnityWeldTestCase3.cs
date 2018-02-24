using System;
using System.Collections.Generic;
using UnityEngine;
using WindHotfix.Core;

namespace Game.Knight.Test
{
    public class UnityWeldTestCase3 : THotfixMB<UnityWeldTestCase3>
    {
        private float cubeRotation = 0f;

        [UnityWeld.Binding.Binding]
        public void RotateCube()
        {
            cubeRotation = cubeRotation + 10f;
            var cube = GameObject.Find("Cube");
            cube.transform.localEulerAngles = new Vector3(0f, cubeRotation, 0f);
        }
    }
}
