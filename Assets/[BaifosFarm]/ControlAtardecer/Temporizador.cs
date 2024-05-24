using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour
{
    //Fases del reloj
    [SerializeField] private Image seisManyana;
    [SerializeField] private Image ocho;
    [SerializeField] private Image diez;
    [SerializeField] private Image doce;
    [SerializeField] private Image dos;
    [SerializeField] private Image cuatro;
    [SerializeField] private Image seisTarde;


    private AccionesAtardecer accionesAtardecer;
    private DeteccionCabrasNegras deteccionCabrasNegras;

    private DateTime horaInicio;
    private float duracionDia = 90; 

    [SerializeField]
    public float tiempoRestante = 90; 
    [SerializeField] private Text contadorText;

    [SerializeField] GraneroInicio graneroInicio;

    private void Awake()
    {
        IniciarCuentaRegresiva();
        graneroInicio.activarGallo();
        deteccionCabrasNegras = gameObject.AddComponent<DeteccionCabrasNegras>();
    }

    public void IniciarCuentaRegresiva()
    {
        horaInicio = DateTime.Now;
        StartCoroutine(CuentaRegresiva());
    }

    private IEnumerator CuentaRegresiva()
    {
        while (tiempoRestante > 0)
        {
            yield return new WaitForSeconds(1f);
            tiempoRestante -= 1f;

            if (contadorText != null)
            {
                contadorText.text = ObtenerTemporizadorActual();
                MostrarFasesReloj(contadorText.text);
            }
        }

        EjecutarAccionesAtardecer();
    }

    private void EjecutarAccionesAtardecer()
    {
        accionesAtardecer = GetComponent<AccionesAtardecer>();
        if (accionesAtardecer != null)
        {
            StartCoroutine(accionesAtardecer.EjecutarAccionesAtardecer());
        }
    }

    private string ObtenerTemporizadorActual()
    {
       
        int horaEnJuego = Mathf.FloorToInt(6 + (1 - tiempoRestante / duracionDia) * 12); 
        if (horaEnJuego >= 18)
        {
            horaEnJuego = 18; 
        }

        string horaFormateada = horaEnJuego.ToString("00") + ":00";

        return horaFormateada;
    }

    private void MostrarFasesReloj(String hora)
    {
        if (hora == "08:00")
        {
            seisManyana.gameObject.SetActive(false);
            ocho.gameObject.SetActive(true);
        }
        
        else if (hora == "10:00")
        {
            ocho.gameObject.SetActive(false);
            diez.gameObject.SetActive(true);
        }

        else if (hora == "12:00")
        {
            diez.gameObject.SetActive(false);
            doce.gameObject.SetActive(true);
        }

        else if (hora == "14:00")
        {
            doce.gameObject.SetActive(false);
            dos.gameObject.SetActive(true);
        }

        else if (hora == "16:00")
        {
            dos.gameObject.SetActive(false);
            cuatro.gameObject.SetActive(true);
        }

        else if (hora == "18:00")
        {
            cuatro.gameObject.SetActive(false);
            seisTarde.gameObject.SetActive(true);
        }


    }



}

