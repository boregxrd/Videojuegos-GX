using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�������������������������������������������������������SCRIPT ACCI�N ALIMENTAR������������������������������������������������������
//Este script ha de estar en Mano dentro de Personaje

public class Alimentar : MonoBehaviour
{
    [SerializeField] ControladorAccionesPersonaje controladorAccionesPersonaje;

    [SerializeField] private BarraAlimento barraAlimento;

    public bool isHenoMejorado = false;
    private void Awake()
    {
        enabled = false;
    }

    public void DarComida(Collider other)
    {
        var children = other.gameObject.GetComponentsInChildren<Transform>(); //dentro de la cabra busco el objeto barraAlimento y luego su script
        foreach (var child in children)
        {
            if (child.name == "BarraAlimentos")
            {
                barraAlimento = child.GetComponent<BarraAlimento>();
                float incremento = 25f;
                if(isHenoMejorado){
                    incremento = 50f;
                }
                barraAlimento.incrementarNivelAlimentacion(incremento);
            }
        }
    }
}
