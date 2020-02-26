using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CSDemo007_main : MonoBehaviour
{
    public Material material;
    public ComputeShader consumeBufferShader;

    const int width = 32;
    const float size = 5.0f;

    ComputeBuffer buffer;
    ComputeBuffer argBuffer;

    void Start()
    {
        buffer = new ComputeBuffer(width * width, sizeof(float) * 3, ComputeBufferType.Append);
        buffer.SetCounterValue(width * width);

        Vector3[] points = new Vector3[1024];
        Random.InitState(0);
        for (int i = 0; i < 1024; i++)
        {
            points[i].x = Random.Range(-size, size);
            points[i].y = Random.Range(-size, size);
            points[i].z = 0.0f;
        }

        buffer.SetData(points);

        int handle = consumeBufferShader.FindKernel("CSMain7");
        consumeBufferShader.SetBuffer(handle, "consumeBuffer", buffer);
        consumeBufferShader.SetFloat("size", size);
        consumeBufferShader.SetFloat("width", width);

        //consumeBufferShader.Dispatch(0, width/2 / 8, width/2 / 8, 1);
        consumeBufferShader.Dispatch(0, width / 8, width / 8, 1);

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
