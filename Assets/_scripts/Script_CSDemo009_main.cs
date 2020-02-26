using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Script_CSDemo009_main : MonoBehaviour
{
    public ComputeShader tintShader;
    public Color color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    private RenderTexture tempDestination = null;
    private int handle;

    void Start()
    {
        handle = tintShader.FindKernel("CSMain9_tint");
    }

    void Update()
    {
        
    }

    void OnDestroy()
    {
        if (tempDestination != null)
        {
            tempDestination.Release();
            tempDestination = null;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (tintShader == null || handle < 0 || source == null)
        {
            Graphics.Blit(source, destination); // just copy
            return;
        }

        // resize window
        if (tempDestination == null || source.width != tempDestination.width || source.height != tempDestination.height)
        {
            if (tempDestination != null)
            {
                tempDestination.Release();
            }
            tempDestination = new RenderTexture(source.width, source.height, source.depth);
            tempDestination.enableRandomWrite = true;
            tempDestination.Create();
        }

        tintShader.SetTexture(handle, "Source", source);
        tintShader.SetTexture(handle, "Destination", tempDestination);
        tintShader.SetVector("Color", (Vector4)color);
        tintShader.Dispatch(handle, (tempDestination.width + 7) / 8, (tempDestination.height + 7) / 8, 1);

        // show
        Graphics.Blit(tempDestination, destination);
    }
}
