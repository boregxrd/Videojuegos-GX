using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    public ParticleSystem[] particleEffects; // Efectos de part�culas para cada paso

    private int popUpIndex;
    [SerializeField] private RecogerAlimento recogerAlimento;
    [SerializeField] private Alimentar alimentar;
    [SerializeField] private Ordeniar ordeniar;
    [SerializeField] private DejarLecheEnCaja dejarLecheEnCaja;
    [SerializeField] private Character movimientoPersonaje;

    private void Start()
    {
        ShowNextPopUp();
    }

    private void Update()
    {
        // Aqu� solo mantienes la l�gica de verificaci�n de pasos
        CheckCompletion();
    }

    private void ShowNextPopUp()
    {
        // Ocultar todos los pop-ups y desactivar los efectos de part�culas
        foreach (var popUp in popUps)
        {
            popUp.SetActive(false);
        }

        foreach (var effect in particleEffects)
        {
            effect.Stop();
        }

        if (popUpIndex < popUps.Length)
        {
            // Activar el efecto de part�culas correspondiente
            particleEffects[popUpIndex].Play();

            // Mostrar el pop-up
            popUps[popUpIndex].SetActive(true);
            Debug.Log($"Mostrando pop-up {popUpIndex + 1}"); // Mensaje de depuraci�n
        }
        else
        {
            PlayerPrefs.SetInt("TutorialCompleto", 1); // Marcar el tutorial como completado
            Debug.Log("Tutorial completado");
        }
    }

    private void CheckCompletion()
    {
        switch (popUpIndex)
        {
            case 0: // Movimiento
                if (movimientoPersonaje.HasMoved())
                {
                    CompleteStep();
                    Debug.Log("Movimiento completado");
                }
                break;

            case 1: // Recoger Heno
                if (recogerAlimento.henoRecogido)
                {
                    CompleteStep();
                    Debug.Log("Recoger Heno completado");
                }
                break;

            case 2: // Alimentar
                if (alimentar.alimentacionRealizada)
                {
                    CompleteStep();
                    Debug.Log("Alimentar completado");
                }
                break;

            case 3: // Recoger Leche
                if (ordeniar.orde�oIniciado)
                {
                    CompleteStep();
                    Debug.Log("Recoger Leche completado");
                }
                break;

            case 4: // Guardar Leche
                if (dejarLecheEnCaja.lecheGuardada)
                {
                    CompleteStep();
                    Debug.Log("Guardar Leche completado");
                }
                break;

            default:
                break;
        }
    }

    private void CompleteStep()
    {
        // Detener el efecto de part�culas actual
        particleEffects[popUpIndex].Stop();

        // Pasar al siguiente pop-up
        NextPopUp();
    }

    private void NextPopUp()
    {
        // Ocultar el pop-up actual
        popUps[popUpIndex].SetActive(false);

        // Incrementar el �ndice del pop-up
        popUpIndex++;

        // Mostrar el siguiente pop-up
        ShowNextPopUp();
    }
}
