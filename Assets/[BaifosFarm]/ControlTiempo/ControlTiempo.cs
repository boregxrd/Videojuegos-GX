using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlTiempo : MonoBehaviour
{
    public Text contadorText; // Referencia al objeto Text donde se mostrará el contador
    [SerializeField]
    public float tiempoRestante = 120f; // 2 minutos en segundos
    private Alimentar alimentar;
    public SistemaMonetario sistemaMonetario; // Referencia al C# Script de sistema de dinero
    public Text textoDinero; // Referencia al objeto de texto que mostrará el dinero total
    int dineroTotal;
    [SerializeField] public ControladorAccionesPersonaje accionesBaifo; 

    private DeteccionCabrasNegras deteccionCabrasNegras;

    //-------------------------------------------------------------
    //  Esto debe ir en otra clase en otro momento cuando
    //  reorganizemos el codigo entre todos
    public static Action OnThreeBlackGoatsVictory;
    //-------------------------------------------------------------
    
    void Awake()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("LechesGuardadas", 0);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        alimentar = GetComponent<Alimentar>();
        alimentar.GestionarAparienciaMontonHeno();

        RecogerDatosDinero();
        ActivarCuentaRegresiva();
    }

    private void RecogerDatosDinero()
    {
        dineroTotal = PlayerPrefs.GetInt("DineroTotal", 0);
        sistemaMonetario = FindObjectOfType<SistemaMonetario>();
        // Actualizar el texto del dinero total
        textoDinero.text = "Dinero: $" + dineroTotal.ToString();
    }

    private void ActivarCuentaRegresiva()
    {
        if (contadorText == null)
        {
            contadorText = GetComponent<Text>();
        }
        contadorText.text = "Tiempo restante: " + obtenerTemporizadorActual();
        // Comenzar la cuenta regresiva
        StartCoroutine(CuentaRegresiva());
    }

    IEnumerator CuentaRegresiva()
    {
        deteccionCabrasNegras = new DeteccionCabrasNegras();
        deteccionCabrasNegras.VerificarSiHayTresCabrasNegrasAlInicio();

        while (tiempoRestante > 0)
        {

            yield return new WaitForSeconds(1f); // Esperar un segundo
            tiempoRestante -= 1f; // Restar un segundo al tiempo restante

            // Actualizar el contador Text en formato minutos:segundos
            if (contadorText != null)
            {
                int minutos = Mathf.FloorToInt(tiempoRestante / 60f);
                int segundos = Mathf.FloorToInt(tiempoRestante % 60f);
                contadorText.text = "Tiempo restante: " + obtenerTemporizadorActual();
            }
        }

        // Cuando el tiempo llega a cero...
        congelarBarrasCabras();
        desabilitarAccionesBaifo();

        GameObject camion = GameObject.Find("Camion");
        LlegadaCamión llegadaCamión = camion.GetComponent<LlegadaCamión>();
        llegadaCamión.empezarMovimientoCamion();

        while (llegadaCamión.enMovimiento)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);



        Time.timeScale = 0f;
        // Llamada para sumar el dinero
        ControladorTextoCaja controladorTextoCaja = FindObjectOfType<ControladorTextoCaja>();

        if (controladorTextoCaja != null)
        {
            controladorTextoCaja.SumarDineroPorBotella();
            Debug.Log("Sumo el dinero por botella");
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        VerificarYCargarEscena();
    }

    private void desabilitarAccionesBaifo()
    {
        if(accionesBaifo != null) {
            accionesBaifo.gameObject.SetActive(false);
        }
    }

    private void congelarBarrasCabras()
    {
        // Find all BarraAlimento and BarraLeche components in the scene
        BarraAlimento[] barrasAlimento = FindObjectsOfType<BarraAlimento>();
        BarraLeche[] barrasLeche = FindObjectsOfType<BarraLeche>();

        // Disable all found BarraAlimento components
        foreach (BarraAlimento barraAlimento in barrasAlimento)
        {
            barraAlimento.enabled = false;
        }

        // Disable all found BarraLeche components
        foreach (BarraLeche barraLeche in barrasLeche)
        {
            barraLeche.enabled = false;
        }
    }

    void VerificarYCargarEscena()
    {
        if (deteccionCabrasNegras.CuidasteLasCabrasNegrasAlFinal())
        {
            Debug.Log("Intento invocar al evento");
            OnThreeBlackGoatsVictory?.Invoke();
            return;
        }
        else
        {
            deteccionCabrasNegras.DestruirCabrasCadaUna();

            SceneManager.LoadScene("Factura");
        }
    }

    

    private string obtenerTemporizadorActual()
    {
        int minutos = Mathf.FloorToInt(tiempoRestante / 60f);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60f);
        return minutos.ToString("00") + ":" + segundos.ToString("00");
    }
}
