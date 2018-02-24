using Core;
using Framework.Hotfix;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeldTest : MonoBehaviour
{
    public HotfixMBContainer MBC;

    void Awake()
    {
        HotfixManager.Instance.Initialize();
        CoroutineManager.Instance.Initialize();
        HotfixEventManager.Instance.Initialize();

        string rDLLPath = "Assets/Game/Knight/GameAsset/Hotfix/Libs/KnightHotfixModule.bytes";
        string rPDBPath = "Assets/Game/Knight/GameAsset/Hotfix/Libs/KnightHotfixModule_PDB.bytes";

        HotfixManager.Instance.InitApp(File.ReadAllBytes(rDLLPath), File.ReadAllBytes(rPDBPath));
    }

    private void LoadHotfixDLL()
    {
        string rDLLPath = "Assets/StreamingAssets/Temp/Libs/KnightHotfixModule.dll";
        string rPDBPath = "Assets/StreamingAssets/Temp/Libs/KnightHotfixModule.pdb";
        HotfixManager.Instance.InitApp(rDLLPath, rPDBPath);
    }

    void Start()
    {
        //yield return new WaitForSeconds(2.0f);
        bool isValid = (bool)this.MBC.MBHotfixObject.Invoke("get_IsValid");
        Debug.LogError("WeldTest: " + isValid);
    }
}
