using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CSDemo005_main : MonoBehaviour
{
    public Material material;
    public ComputeShader computeShader;
    ComputeBuffer buffer;
    ComputeBuffer buffer2;
    int handle;

    const int count = 1024;
    const float size = 5.0f;

    struct Vert
    {
        public Vector3 position;
        public Vector3 color;
    }

    // Start is called before the first frame update
    void Start()
    {
        buffer = new ComputeBuffer(count, sizeof(float) * 6, ComputeBufferType.Default);
        buffer2 = new ComputeBuffer(count, sizeof(float) * 6, ComputeBufferType.Default);

        Vert[] points = new Vert[count];

        // Random.seed = 0;
        Random.InitState(0);
        for (int i = 0; i < count; i++)
        {
            points[i] = new Vert();

            points[i].position = new Vector3();
            points[i].position.x = Random.Range(-size, size);
            points[i].position.y = Random.Range(-size, size);
            points[i].position.z = 0.0f;

            points[i].color = new Vector3();
            points[i].color.x = Random.value;
            points[i].color.y = Random.value;
            points[i].color.z = Random.value;
        }

        buffer.SetData(points);

        handle = computeShader.FindKernel("CSMain5");
        computeShader.SetBuffer(handle, "buffer", buffer);
        computeShader.SetBuffer(handle, "buffer2", buffer2);
    }

    // Update is called once per frame
    void Update()
    {
        computeShader.Dispatch(handle, count / 64, 1, 1);
    }

    void OnPostRender()
    {
        material.SetPass(0);
        material.SetBuffer("buffer", buffer2);
        //Graphics.DrawProceduralNow(MeshTopology.Points, count, 1);
        Graphics.DrawProceduralNow(MeshTopology.Lines, count, 1);
    }

    void OnDestroy()
    {
        buffer.Release();
        buffer2.Release();
    }
}
