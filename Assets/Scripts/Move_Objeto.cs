using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Objeto : MonoBehaviour
{


    [SerializeField]
    private float speed = 0.7f;
    private float posZ = 3.4f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.z > 3.4f)
        {
            Destroy(this.gameObject);
        }
    }
}
