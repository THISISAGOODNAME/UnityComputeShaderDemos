using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CSDemo010_main : MonoBehaviour
{
    public ComputeShader sumShader;

    int csHandle;

    void Start()
    {
        ComputeBuffer buffer = new ComputeBuffer(1024, sizeof(int));

        int[] data = new int[1024];
        for (int i = 0; i < 1024; i++)
            data[i] = i;

        int cpuSumResult = 0;
        for (int i = 0; i < 1024; i++)
            cpuSumResult += data[i];

        Debug.Log("CPU result: " + cpuSumResult);
        buffer.SetData(data);

        csHandle = sumShader.FindKernel("CSMain10_sum");
        sumShader.SetBuffer(csHandle, "buffer", buffer);
        sumShader.Dispatch(csHandle, 1024 / 64, 1, 1);

        int[] dataOut = new int[1024];
        buffer.GetData(dataOut);

        Debug.Log("GPU result: " + dataOut[0]);

        buffer.Release();
    }

    void Update()
    {
        // Just for debug
        //ComputeBuffer buffer = new ComputeBuffer(1024, sizeof(int));

        //int[] data = new int[1024];
        //for (int i = 0; i < 1024; i++)
        //    data[i] = i;

        //buffer.SetData(data);

        //sumShader.SetBuffer(csHandle, "buffer", buffer);
        //sumShader.Dispatch(csHandle, 1024 / 64, 1, 1);

        //int[] dataOut = new int[1024];
        //buffer.GetData(dataOut);

        //Debug.Log("GPU result: " + dataOut[0]);

        //buffer.Release();
    }

    void OnDestroy()
    {
        
    }
}
