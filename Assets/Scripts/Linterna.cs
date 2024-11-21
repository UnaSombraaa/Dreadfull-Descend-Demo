using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    public Light linterna;  // La luz de la linterna
    public KeyCode teclaActivar = KeyCode.F;  // Tecla para activar/desactivar la linterna
    public float duracionBateria = 100f;  // Batería total al 100%
    public float consumoPorSegundo = 1f;  // Consumo de batería por segundo cuando está encendida
    public float recargaPorSegundo = 5f;  // Velocidad de recarga de la batería cuando está apagada
    public float nivelBateria = 100f;  // Nivel actual de batería (0 a 100)
    public float umbralBateriaCritica = 20f;  // Umbral para apagar la linterna

    private bool estaEncendida = false;  // Estado de la linterna

    void Start()
    {
        // Si no se asignó la luz en el inspector, intentamos obtenerla automáticamente
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
        // Si la batería es menor al 20%, la linterna se apaga automáticamente
        if (nivelBateria <= umbralBateriaCritica && estaEncendida)
        {
            ApagarLinterna();
        }

        // Si la linterna está encendida, reduce la batería
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
            // Si la linterna está apagada, recarga la batería lentamente
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

        // Actualizamos el estado de la luz según el nivel de batería
        if (linterna != null)
        {
            linterna.enabled = estaEncendida;
        }
    }

    // Método para alternar el estado de la linterna
    void ToggleLinterna()
    {
        estaEncendida = !estaEncendida;
    }

    // Método para apagar la linterna, usado cuando la batería es baja
    void ApagarLinterna()
    {
        estaEncendida = false;
    }
}