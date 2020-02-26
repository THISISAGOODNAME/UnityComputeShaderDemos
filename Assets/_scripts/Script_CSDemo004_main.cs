using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CSDemo004_main : MonoBehaviour
{
    public ComputeShader shader;
    public ComputeShader shaderCopy;

    RenderTexture tex;
    RenderTexture texCopy;

    void Start()
    {
#if false
        tex = new RenderTexture(64, 64, 0);
        tex.enableRandomWrite = true;
        tex.Create();

        int handle = shader.FindKernel("CSMain4");

        shader.SetFloat("w", tex.width);
        shader.SetFloat("h", tex.height);
        shader.SetTexture(handle, "tex", tex);
        shader.Dispatch(handle, tex.width / 8, tex.height / 8, 1);
#endif

#if true
        tex = new RenderTexture(64, 64, 0);
        tex.enableRandomWrite = true;
        tex.Create();

        texCopy = new RenderTexture(64, 64, 0);
        texCopy.enableRandomWrite = true;
        texCopy.Create();

        int handleCSMain4_2tex = shader.FindKernel("CSMain4_2tex");
        int handleCSMain4_2texCpy = shaderCopy.FindKernel("CSMain4_2texCpy");

        shader.SetTexture(handleCSMain4_2tex, "tex", tex);
        shader.Dispatch(handleCSMain4_2tex, tex.width / 8, tex.height / 8, 1);

        shaderCopy.SetTexture(handleCSMain4_2texCpy, "tex", tex);
        shaderCopy.SetTexture(handleCSMain4_2texCpy, "texCopy", texCopy);
        shaderCopy.Dispatch(handleCSMain4_2texCpy, tex.width / 8, tex.height / 8, 1);
#endif
    }

    void Update()
    {
        
    }

    void OnGUI()
    {
        int w = Screen.width / 2;
        int h = Screen.height / 2;
        int s = 512;

        GUI.DrawTexture(new Rect(w - s / 2, h - s / 2, s, s), texCopy);
    }

    void OnDestroy()
    {
        tex.Release();    
    }
}
