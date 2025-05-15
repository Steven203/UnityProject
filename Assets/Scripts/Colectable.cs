using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colectable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisione con: " + other.name);

        //accediendo a los metdos y variables del player
        Move myobject = other.GetComponent<Move>();
        myobject.cambiaObjeto = true;
        Destroy(this.gameObject);
    }
}
