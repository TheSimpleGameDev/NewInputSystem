using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    [SerializeField] private Transform transformDelJugador;
    [SerializeField] private float velocidadDeSeguimiento = 10.0f;
    Vector3 diferenciaInicial;

    private void Start()
    {
        transformDelJugador = GameObject.FindGameObjectWithTag("Player").transform;
        diferenciaInicial = transform.position - transformDelJugador.position;
    }

    private void LateUpdate()
    {
        Vector3 posicionDeseada = Vector3.Lerp(transform.position, transformDelJugador.position + diferenciaInicial, Time.deltaTime * velocidadDeSeguimiento);
        transform.position = posicionDeseada;
    }


}
