using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Factura : MonoBehaviour
{    
    public static Action OnGameOver; 
    public static Action OnMoneyVictory;

    //CantidadCabrasAtardecer cantidadCabrasAtardecer;
    PopUpsFacturaTutorial popUpsFacturaTutorial;
    ManejoCompras manejoCompras;
    UIFactura uIFactura;

    private void Awake()
    {
        //cantidadCabrasAtardecer = CantidadCabrasAtardecer.ObtenerInstancia();
        Application.targetFrameRate = 60;
        popUpsFacturaTutorial = GetComponent<PopUpsFacturaTutorial>();
        manejoCompras = GetComponent<ManejoCompras>();
        uIFactura = GetComponent<UIFactura>();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("TutorialCompleto") == 0)
        {
            
            StartCoroutine(popUpsFacturaTutorial.ShowPopUps());
        }
        else
        {
            popUpsFacturaTutorial.HidePopUps();
        }
    }

    private void Update()
    {
        VerificarCondicionesVictoriaDerrota();
    }

    private void VerificarCondicionesVictoriaDerrota()
    {
        if (IsGameOver())
        {
            OnGameOver?.Invoke();
        }
        else if (uIFactura.dinero >= 350)
        {
            OnMoneyVictory?.Invoke();
        }
    }

    private bool IsGameOver()
    {
        if (manejoCompras.numCabrasBlancas == 0 && uIFactura.dinero < manejoCompras.COSTO_CABRA + manejoCompras.COSTO_ALIMENTAR_CABRA)
        {
            return true;
        }
        else if (manejoCompras.EsGastoMayorQue(uIFactura.dinero))
        {
            return true;
        }
        return false;
    }

    public void Continuar()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Juego");
        PlayerPrefs.SetInt("LechesGuardadas", 0);
        manejoCompras.RestarDinero();
    }

}


//debugs

//Debug.Log("cabras negras: " + numCabrasNegras);
//Debug.Log("cabras blancas: " + numCabrasBlancas);
//Debug.Log("dinero: " + dinero);
//Debug.Log("COSTOCABRA + COSTOALIMENTAR: " + (COSTO_ALIMENTAR_CABRA + COSTO_CABRA));
//Debug.Log("Gastodiario: " + sistemaMonetario.CalcularGastoHeno());