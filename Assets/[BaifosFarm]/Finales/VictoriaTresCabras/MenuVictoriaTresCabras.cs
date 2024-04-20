using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuVictoriaTresCabras : MonoBehaviour
{
    [SerializeField] GameObject objetoMenuVictoriaTresCabras;
    string NOMBRE_MENU = "Main";

    void Start()
    {
        objetoMenuVictoriaTresCabras.SetActive(false);
        //Factura.OnMoneyVictory += MostrarMenuVictoria;
    }

    private void OnDestroy()
    {
        //Factura.OnGameOver -= MostrarMenuVictoria;
    }

    void MostrarMenuVictoria()
    {
        if (objetoMenuVictoriaTresCabras != null)
        {
            objetoMenuVictoriaTresCabras.SetActive(true);
        }
        else
        {
            Debug.LogWarning("objetoMenuVictoriaTresCabras es nulo.");
        }
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene(NOMBRE_MENU);
    }
}