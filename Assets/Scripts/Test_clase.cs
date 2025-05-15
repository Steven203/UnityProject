using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_clase : MonoBehaviour
{
    public float posX = -0.205f;
    public float posY = 0.654f;
    public float posZ = 0.701f;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Mi nombre es:" + name);
        //Debug.Log("posicion en x:" + transform.position.x);
        transform.position = new Vector3(posX, posY, posZ);
        Debug.Log("Nuevo posicion camara:" + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
