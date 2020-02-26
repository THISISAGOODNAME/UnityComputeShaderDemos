using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CSDemo006_main : MonoBehaviour
{
    public Material material;
    public ComputeShader appendBufferShader;

    const int width = 32;
    const float size = 5.0f;

    ComputeBuffer buffer;
    ComputeBuffer argBuffer;

    void Start()
    {
        buffer = new ComputeBuffer(width * width, sizeof(float) * 3, ComputeBufferType.Append);
        buffer.SetCounterValue(0);

        int handle = appendBufferShader.FindKernel("CSMain6");
        appendBufferShader.SetBuffer(handle, "appendBuffer", buffer);
        appendBufferShader.SetFloat("size", size);
        appendBufferShader.SetFloat("width", width);

        appendBufferShader.Dispatch(0, width / 8, width / 8, 1);

        argBuffer = new ComputeBuffer(4, sizeof(int), ComputeBufferType.IndirectArguments); // ComputeBufferType.DrawIndirect

        int[] args = new int[] { 0, 1, 0, 0 };
        argBuffer.SetData(args);

        ComputeBuffer.CopyCount(buffer, argBuffer, 0);
        argBuffer.GetData(args);

        Debug.Log("vertex count " + args[0]);
        Debug.Log("instance count " + args[1]);
        Debug.Log("start vertex " + args[2]);
        Debug.Log("start instance " + args[3]);
    }

    void Update()
    {
        
    }

    void OnPostRender()
    {
        material.SetPass(0);
        material.SetBuffer("buffer", buffer);
        material.SetColor("col", Color.red);

        Graphics.DrawProceduralIndirectNow(MeshTopology.Lines, argBuffer);
    }

    void OnDestroy()
    {
        buffer.Release();
        argBuffer.Release();
    }
}
