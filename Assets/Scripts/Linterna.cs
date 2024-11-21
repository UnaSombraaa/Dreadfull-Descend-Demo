using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    public Light linterna;  // La luz de la linterna
    public KeyCode teclaActivar = KeyCode.F;  // Tecla para activar/desactivar la linterna
    public float duracionBateria = 100f;  // Bater�a total al 100%
    public float consumoPorSegundo = 1f;  // Consumo de bater�a por segundo cuando est� encendida
    public float recargaPorSegundo = 5f;  // Velocidad de recarga de la bater�a cuando est� apagada
    public float nivelBateria = 100f;  // Nivel actual de bater�a (0 a 100)
    public float umbralBateriaCritica = 20f;  // Umbral para apagar la linterna

    private bool estaEncendida = false;  // Estado de la linterna

    void Start()
    {
        // Si no se asign� la luz en el inspector, intentamos obtenerla autom�ticamente
        if (linterna == null)
        {
            linterna = GetComponentInChildren<Light>();
        }

        // Aseguramos que la linterna empieza apagada
        if (linterna != null)
        {
            linterna.enabled = false;
        }
    }

    void Update()
    {
        // Si la bater�a es menor al 20%, la linterna se apaga autom�ticamente
        if (nivelBateria <= umbralBateriaCritica && estaEncendida)
        {
            ApagarLinterna();
        }

        // Si la linterna est� encendida, reduce la bater�a
        if (estaEncendida)
        {
            nivelBateria -= consumoPorSegundo * Time.deltaTime;
            if (nivelBateria < 0f)
            {
                nivelBateria = 0f;
            }
        }
        else
        {
            // Si la linterna est� apagada, recarga la bater�a lentamente
            if (nivelBateria < 100f)
            {
                nivelBateria += recargaPorSegundo * Time.deltaTime;
                if (nivelBateria > 100f)
                {
                    nivelBateria = 100f;
                }
            }
        }

        // Comprobamos si se presiona la tecla para encender o apagar la linterna
        if (Input.GetKeyDown(teclaActivar))
        {
            if (nivelBateria > 0f)
            {
                ToggleLinterna();
            }
        }

        // Actualizamos el estado de la luz seg�n el nivel de bater�a
        if (linterna != null)
        {
            linterna.enabled = estaEncendida;
        }
    }

    // M�todo para alternar el estado de la linterna
    void ToggleLinterna()
    {
        estaEncendida = !estaEncendida;
    }

    // M�todo para apagar la linterna, usado cuando la bater�a es baja
    void ApagarLinterna()
    {
        estaEncendida = false;
    }
}