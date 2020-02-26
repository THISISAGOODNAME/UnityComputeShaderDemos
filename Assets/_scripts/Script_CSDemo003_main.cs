using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CSDemo003_main : MonoBehaviour
{
    public ComputeShader computeShader;

    // Start is called before the first frame update
    void Start()
    {
#if false
        ComputeBuffer buffer = new ComputeBuffer(4, sizeof(int));
        computeShader.SetBuffer(0, "buffer1", buffer);

        computeShader.Dispatch(0, 1, 1, 1);

        int[] data = new int[4];
        buffer.GetData(data);

        for(int i = 0; i < 4; i++)
        {
            Debug.Log("Data[" + i +  "]: " + data[i]);
        }

        buffer.Release();
#endif

#if true
        ComputeBuffer buffer = new ComputeBuffer(4 * 4 * 2 * 2, sizeof(int));

        int kernel = computeShader.FindKernel("CSMain3_2");
        computeShader.SetBuffer(kernel, "buffer2", buffer);

        computeShader.SetInt("scale", 2);

        computeShader.Dispatch(kernel, 2, 2, 1);

        int[] data = new int[4 * 4 * 2 * 2];
        buffer.GetData(data);

        for (int i = 0; i < 8; i++)
        {
            string line = "";
            for (int j = 0; j < 8; j++)
            {
                line += " " + data[j + i * 8];
            }
            Debug.Log(line);
        }

        buffer.Release();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
