using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CSDemo002_main : MonoBehaviour
{
    public ComputeShader computeShader;
    public int textureRes = 256; // Size of render texture we will be making

    private Renderer rend;
    private RenderTexture renderTexture;

    // Start is called before the first frame update
    void Start()
    {
        renderTexture = new RenderTexture(textureRes, textureRes, 24) // We have 24 as parameter for depth so that we will have depth buffer & stencil buffer support
        {
            enableRandomWrite = true
        };
        renderTexture.Create();//Need to call this to actually make it available in graphics memory

        rend = GetComponent<Renderer>();
        rend.enabled = true;

        int kernelHandle = computeShader.FindKernel("CSMain2"); //Find entry point to our Compute Shader
        computeShader.SetTexture(kernelHandle, "Result", renderTexture); //Assigning our render texture in our Compute Shader which is called 'Result'
        computeShader.Dispatch(kernelHandle, textureRes / 8, textureRes / 8, 1); // Executes code on GPU with the input we have provided.
        rend.material.SetTexture("_MainTex", renderTexture); //Telling out render's material to use the render texture as it's texture
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        renderTexture.Release(); // Remove the render texture from graphics memory
    }
}
