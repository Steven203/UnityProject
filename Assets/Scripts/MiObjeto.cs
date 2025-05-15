using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiObjeto : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    private float posZ = 3.4f;
    // Start is called before the first frame update
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
