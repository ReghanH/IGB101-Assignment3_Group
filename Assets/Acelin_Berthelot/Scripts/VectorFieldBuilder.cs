// Assets/Editor/VectorFieldBuilder.cs
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

public class VectorFieldBuilder : EditorWindow
{
    const int SIZE = 32;   // 32×32×32 texels
    const string CS_PATH = "Assets/Acelin_Berthelot/Shaders/TeleportField.compute";
    const string OUT_PATH = "Assets/Acelin_Berthelot/Shaders/TeleportVectorField.asset";

    [MenuItem("Tools/Generate Vector‑Field Texture")]
    static void Build()
    {
        var cs = AssetDatabase.LoadAssetAtPath<ComputeShader>(CS_PATH);
        if (!cs)
        {
            Debug.LogError($"Compute shader not found at '{CS_PATH}'.");
            return;
        }

        RenderTexture rt = new RenderTexture(SIZE, SIZE, 0, RenderTextureFormat.ARGBHalf)
        {
            dimension = TextureDimension.Tex3D,
            volumeDepth = SIZE,
            enableRandomWrite = true,
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Bilinear
        };
        rt.Create();

        int kernel = cs.FindKernel("TeleportField");           
        cs.SetInts("FieldSize", SIZE, SIZE, SIZE);
        cs.SetTexture(kernel, "Result", rt);

        int groups = Mathf.CeilToInt(SIZE / 8f);         // 8×8×8 thread groups
        cs.Dispatch(kernel, groups, groups, groups);

        Texture3D tex = new Texture3D(SIZE, SIZE, SIZE, TextureFormat.RGBAHalf, false)
        {
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Bilinear
        };
        Graphics.CopyTexture(rt, tex);                   // GPU‑side copy

        AssetDatabase.DeleteAsset(OUT_PATH);
        AssetDatabase.CreateAsset(tex, OUT_PATH);
        AssetDatabase.SaveAssets();

        Debug.Log($"Vector field saved to {OUT_PATH}");

        rt.Release();
    }
}