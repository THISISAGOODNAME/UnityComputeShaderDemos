using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CSDemo001_main : MonoBehaviour
{
    public ComputeShader ShaderCSMain;
    public RenderTexture tex;

    private bool _supportComputeShader;

    // Start is called before the first frame update
    void Start()
    {
        _supportComputeShader = SystemInfo.supportsComputeShaders;

        if (!_supportComputeShader)
        {
            Debug.Log("System not support Conpute shader");
        }
        else
        {
            Debug.Log("System support Conpute shader");
        }

        tex = new RenderTexture(256, 256, 24);
        tex.enableRandomWrite = true;
        tex.Create();
    }

    // Update is called once per frame
    void Update()
    {
        RunShader();
    }

    void RunShader()
    {
        if (_supportComputeShader)
        {
            // int kernelHandle = ShaderCSMain.FindKernel("FillWithRed");
            int kernelHandle = ShaderCSMain.FindKernel("CSMain");

            int width = tex.width;
            int height = tex.height;

            ShaderCSMain.SetTexture(kernelHandle, "Result", tex);
            ShaderCSMain.Dispatch(kernelHandle, width / 8, height / 8, 1);
        }
    }
}
