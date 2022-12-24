using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waves : MonoBehaviour
{
    public float perlinScale;
    public float waveSpeed;
    public float waveHeight;
    public float offset;
    private bool storming;
    private bool waveing;
    private bool bottling;
    private bool music;
    private Vector3 scale;
    private Vector3 bottleScale;
    public Animator anime;
    public AudioSource source;
    public AudioClip[] clips = new AudioClip[3];
    public GameObject bottle;
    private void Start()
    {
        storming = false;
        waveing = true;
        bottling = false;
        music = false;
        scale = new Vector3(20, 0, 20);
        
    }
    void Update()
    {
        CalcNoise();

        while (music == true)
        {
            waveHeight += source.clip.frequency;
        }
    }

    void CalcNoise()
    {
        MeshFilter mF = GetComponent<MeshFilter>();
        MeshCollider mC = GetComponent<MeshCollider>();

        mC.sharedMesh = mF.mesh;

        Vector3[] verts = mF.mesh.vertices;

        for (int i = 0; i < verts.Length; i++)
        {
            float pX = (verts[i].x * perlinScale) + (Time.timeSinceLevelLoad * waveSpeed) + offset;
            float pZ = (verts[i].z * perlinScale) + (Time.timeSinceLevelLoad * waveSpeed) + offset;
            verts[i].y = Mathf.PerlinNoise(pX, pZ) * waveHeight;
        }

        mF.mesh.vertices = verts;
        mF.mesh.RecalculateNormals();
        mF.mesh.RecalculateBounds();
    }

    public void storm()
    {
        if(storming == false)
        {
            waveHeight += 3;
            storming = true;
            waveing = false;
            bottling = false;
            anime.SetBool("Storm", true);
            source.clip = clips[1];
            source.Play();
           

        }
        
    }

    public void wave()
    {
        if (waveing == false)
        {
            waveHeight -= 3;
            storming = false;
            waveing = true;
            bottling = false;
            anime.SetBool("Storm", false);
            source.clip = clips[0];
            source.Play();
            
            
        }

    }

    public void shipbottle()
    {
        if (bottling == false)
        {
            waveHeight -= 3;
            storming = false;
            waveing = false;
            bottling = true;
            anime.SetBool("Storm", false);
            source.clip = clips[2];
            source.Play();
            transform.localScale += scale;
            music = true;
        }

    }
}
