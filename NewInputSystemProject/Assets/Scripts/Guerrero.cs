using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guerrero : MonoBehaviour
{
    [Header("Variables del Guerrero")]
    [SerializeField] private float velocidadDeMovimiento = 5.0f;
    [SerializeField] private float velocidadDeRotacion = 90.0f;

    [Header("Objetos")]
    [SerializeField] private GameObject escudoEspalda;
    [SerializeField] private GameObject escudoBrazo;
    [SerializeField] private GameObject espadaEspalda;
    [SerializeField] private GameObject espadaMano;

    //Variables
    private bool espadaDesenvainada;
    private bool seEstaCubriendo;
    private CharacterController controlador;
    private Animator animador;
    private Rigidbody cuerpoRigido;

    //Inicializacion
    private void Awake()
    {
        controlador = GetComponent<CharacterController>();
        animador = GetComponentInChildren<Animator>();
        cuerpoRigido = GetComponent<Rigidbody>();
        NoCubrirse();
        GuardarEspada();
    }

    //Metodos
    private void MoverGuerrero(Vector2 direccionDelGuerrero)
    {
        if (!seEstaCubriendo)
        {
            Vector3 direccionRelativa = new Vector3(direccionDelGuerrero.x, 0, direccionDelGuerrero.y);
            controlador.SimpleMove(direccionRelativa * velocidadDeMovimiento);
            animador.SetFloat("speed", direccionRelativa.magnitude);
        }
    }

    private void GirarGuerrero(Vector2 direccionDelGuerrero)
    {
        Vector3 direccionRelativa = new Vector3( direccionDelGuerrero.x, 0, direccionDelGuerrero.y );
        if (direccionRelativa.magnitude > 0.1f)
        {
            Quaternion direccionDeseada = Quaternion.LookRotation(direccionRelativa, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, direccionDeseada, Time.deltaTime * velocidadDeRotacion);
        }
    }

    private void Atacar()
    {
        animador.SetTrigger("attack_1");
        if (!espadaDesenvainada)
        {
            DesenvainarEspada();
        }
    }

    private void Cubrirse()
    {
        animador.SetBool("isShielding", true);
        escudoBrazo.SetActive(true);
        escudoEspalda.SetActive(false);
        seEstaCubriendo = true;
    }

    private void NoCubrirse()
    {
        animador.SetBool("isShielding", false);
        escudoBrazo.SetActive(false);
        escudoEspalda.SetActive(true);
        seEstaCubriendo = false;
    }

    private void DesenvainarEspada()
    {
        espadaMano.SetActive(true);
        espadaEspalda.SetActive(false);
        espadaDesenvainada = true;
    }

    private void GuardarEspada()
    {
        espadaMano.SetActive(false);
        espadaEspalda.SetActive(true);
        espadaDesenvainada = false;
    }

    /*
    private void Update()
    {
        Vector2 direccionDelControl = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        MoverGuerrero( direccionDelControl );
        GirarGuerrero( direccionDelControl );

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Atacar();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Cubrirse();
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            NoCubrirse();
        }
    }
    */
}
