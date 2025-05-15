using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.7f;
    private float normalSpeed;
    private float boostedSpeed = 0.8f;          // Velocidad aumentada tras boost
    private float jumpForce = 3.1f;              // Fuerza para saltar
    public float horizontalInput;
    public float verticalInput;

    public GameObject miObjeto;
    public GameObject miOtroObjeto;
    public GameObject plataformas;
    public bool cambiaObjeto = false;

    public GameObject objetoADesaparecer;
    private float tiempoAntesDeDesaparecer = 40f;
    private Coroutine cronometroCoroutine;

    private Rigidbody rb;
    private bool canJump = false;            // Controla si jugador puede saltar


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("El jugador debe tener un Rigidbody para el salto.");
        }
        normalSpeed = speed;  // Guardamos la velocidad normal al iniciar
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(miObjeto, transform.position, Quaternion.identity);
            cambiaMiObjeto();
        }

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);

        //limites verticales del escenario
        if (transform.position.z > 3.1f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 3.1f);
        }
        else if (transform.position.z < 1.1f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 1.1f);
        }

        //limites horizontales del escenario
        if (transform.position.x > 0.51f)
        {
            transform.position = new Vector3(0.51f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -0.84f)
        {
            transform.position = new Vector3(-0.84f, transform.position.y, transform.position.z);
        }

        // Saltar
        if (canJump == true && Input.GetMouseButtonDown(0))
        {
            if (rb != null)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canJump = false;
                
            }
        }

    }

    private void cambiaMiObjeto()
    {
        if (cambiaObjeto == true)
        {
            Instantiate(miOtroObjeto, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(miObjeto, transform.position, Quaternion.identity);
        }
    }

    private void GenerarObjetosPrefabricados()
    {
        // Aquí puedes instanciar los objetos que desees en posiciones específicas
        // Por ejemplo, instanciar 3 objetos en diferentes posiciones
        Vector3[] posiciones = new Vector3[]
        {
            new Vector3(0.5090392f, 0.483f, 1.397f)
        };

        foreach (Vector3 posicion in posiciones)
        {
            Instantiate(plataformas, posicion, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el objeto con el que chocamos tiene el tag que asignamos
        if (other.CompareTag("ObjetoEspecifico"))
        {
            Debug.Log($"El objeto encerrado en la esquina superior derecha desaparecerá en {tiempoAntesDeDesaparecer} segundos.");
            // Iniciar corutina para desaparecer el objeto después de tiempo
            StartCoroutine(DesaparecerObjetoDespuesDeTiempo());
            // Control del cronómetro con mensaje en consola en últimos 10 segundos
            if (cronometroCoroutine != null)
                StopCoroutine(cronometroCoroutine);
            cronometroCoroutine = StartCoroutine(CronometroDesaparicionObjeto(tiempoAntesDeDesaparecer));
        }
        if (other.gameObject == objetoADesaparecer)
        {
            // Detener el cronómetro si está corriendo
            if (cronometroCoroutine != null)
            {
                StopCoroutine(cronometroCoroutine);
                Debug.Log("Cronómetro detenido al colisionar con objetoADesaparecer.");
            }
            
            Debug.Log("You Win!");
            // Pausar el juego
            Time.timeScale = 0f;
        }

        // Colisión con objeto especifico para boost y salto
        if (other.CompareTag("SpeedBoost"))
        {
            speed = boostedSpeed;  // Aumentamos velocidad
            canJump = true;        // Habilitamos salto
            Debug.Log("Presiona clic izquierdo del mouse para saltar sobre las plataformas");
            GenerarObjetosPrefabricados();


        }
    }
    // Esta función espera el tiempo y luego oculta el objeto
    private IEnumerator DesaparecerObjetoDespuesDeTiempo()
    {
        
        yield return new WaitForSeconds(tiempoAntesDeDesaparecer);
        
        if (objetoADesaparecer != null)
        {
            objetoADesaparecer.SetActive(false);
            Debug.Log("Objeto desaparecido.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Permitir volver a saltar al tocar suelo, que debe tener tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            speed = normalSpeed;
        }
    }

    private IEnumerator CronometroDesaparicionObjeto(float tiempo)
    {
        float tiempoRestante = tiempo;
        Debug.Log("Contando para desaparecer el objeto...");
        while (tiempoRestante > 0)
        {
            if (tiempoRestante <= 10f)
            {
                Debug.Log($"Tiempo restante: {tiempoRestante.ToString("F1")} segundos");
            }
            yield return new WaitForSecondsRealtime(1f);
            tiempoRestante -= 1f;
        }
        if (objetoADesaparecer != null)
        {
            objetoADesaparecer.SetActive(false);
            Debug.Log($"Objeto desaparecido: {objetoADesaparecer.name}");
            Debug.Log("Game Over");
            Time.timeScale = 0f; // Pausar el juego
        }
    }

}