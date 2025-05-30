using UnityEngine;
using UnityEditor;

public class TeleportFieldBuilder : EditorWindow
{
    const int SIZE = 32;                               // 32³ is plenty
    const string ASSET_PATH = "Assets/TeleportField.asset";

    [MenuItem("Tools/Generate Teleport Vector Field")]
    static void Generate()
    {
        // 1️⃣ create empty 3-D texture
        var tex = new Texture3D(SIZE, SIZE, SIZE,
                                TextureFormat.RGBAHalf, /*mips*/ false)
        { wrapMode = TextureWrapMode.Clamp };

        // 2️⃣ load/compile compute shader
        var cs = AssetDatabase.LoadAssetAtPath<ComputeShader>(
                                   "Assets/TeleportField.compute");
        if (!cs) { Debug.LogError("Compute shader not found"); return; }

        int kernel = cs.FindKernel("TeleportField");
        cs.SetInts("FieldSize", SIZE, SIZE, SIZE);

        // 3️⃣ dispatch – result goes straight into the texture
        cs.SetTexture(kernel, "Result", tex);
        int groups = Mathf.CeilToInt(SIZE / 8f);
        cs.Dispatch(kernel, groups, groups, groups);

        // 4️⃣ save as asset (overwrite if exists)
        AssetDatabase.DeleteAsset(ASSET_PATH);
        AssetDatabase.CreateAsset(tex, ASSET_PATH);
        AssetDatabase.SaveAssets();

        Debug.Log("Vector-field texture written to " + ASSET_PATH);
    }
}
