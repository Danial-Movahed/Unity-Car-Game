using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reomvemesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshCollider[] colliders = FindObjectsOfType<MeshCollider>();
        int len = colliders.Length;
        for( int i = 0; i < len; i++ ) GameObject.Destroy( colliders[i] );

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
