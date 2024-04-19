using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondicionesAvisos : MonoBehaviour
{

    [SerializeField] private List<Cabra> cabrasEnEscena;
    private LimitesCamara limitesCamara;
    private ControlAvisos controlAvisos;

    
    [SerializeField] private GameObject prefabAvisoHambre;
    [SerializeField] private GameObject prefabAvisoLeche;
    [SerializeField] private GameObject prefabAvisoMuerte;
    

    private float valorAlertaHambre = 30f;
    private float valorAlertaMuerte = 5f;
    private float valorLecheCompleta = 100f;

    private void Awake()
    {
        limitesCamara = GetComponent<LimitesCamara>();
        controlAvisos = FindObjectOfType<ControlAvisos>();
    }

    private void Update()
    {
        if(cabrasEnEscena.Count == 0) //para que solo ejecute este c�digo una vez
        {
            cabrasEnEscena = ObtenerCabrasDeEscena(); //las cabras se crean despu�s de start() por eso lo he puesto en update(), ser�a mejor cambiar esto en un futuro
        }
        else
        {
            comprobarDatosDeCabras();
        }

    }

    private List<Cabra> ObtenerCabrasDeEscena()
    {
        List<Cabra> cabrasEnEscena = new List<Cabra>();

        Cabra[] cabrasEncontradas = FindObjectsOfType<Cabra>();

        foreach (Cabra cabra in cabrasEncontradas)
        {
            cabrasEnEscena.Add(cabra);
        }

        return cabrasEnEscena;
    }

    private void comprobarDatosDeCabras()
    {
        
        foreach (Cabra cabra in cabrasEnEscena)
        {
            if (cabra != null)
            {
                // Verificar si la cabra ha muerto
                if (cabra.nivelDeAlimentacion() <= 0)
                {
                    controlAvisos.DestruirAviso(cabra);
                    continue; // Pasar a la siguiente cabra
                }

                bool estaFueraDeCamara = limitesCamara.ObjetoFueraDeCamara(cabra.transform.position);

                if (estaFueraDeCamara)
                {
                    if (cabra.nivelDeAlimentacion() <= valorAlertaMuerte)
                    {
                        controlAvisos.GenerarOActualizarAviso(cabra, cabra.transform.position, prefabAvisoMuerte);
                        
                    }
                    else if (cabra.nivelDeAlimentacion() <= valorAlertaHambre)
                    {
                        controlAvisos.GenerarOActualizarAviso(cabra, cabra.transform.position, prefabAvisoHambre);
                    }
                    else if (cabra.nivelDeLeche() == valorLecheCompleta)
                    {
                        controlAvisos.GenerarOActualizarAviso(cabra, cabra.transform.position, prefabAvisoLeche);
                        // Debug.Log($"{cabra.name} tiene LECHE");
                    }

                }
                else
                {
                    controlAvisos.DestruirAviso(cabra);
                }
            }
            else
            {
                cabrasEnEscena.Remove(cabra);
            }
           
            
        }
    }
    
}
