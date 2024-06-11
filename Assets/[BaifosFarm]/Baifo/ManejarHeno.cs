using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejarHeno : MonoBehaviour
{
    private Jugador jugador;
    private GameObject heno;
    private Animator animator;

    // Para el tutorial
    public bool alimentacionRealizada = false;

    Animator animatorHeno;
    [SerializeField] GameObject montonHeno;
    [SerializeField] GameObject montonHenoEspecial;

    [SerializeField] ParticleSystem particulasHeno;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] sonidosCogerHeno;


    private void Start()
    {
        if (PlayerPrefs.GetInt("HenoMejorado", 0) == 0)
        {
            animatorHeno = montonHeno.GetComponentInChildren<Animator>();
        }
        else
        {
            animatorHeno = montonHenoEspecial.GetComponentInChildren<Animator>();
        }
        jugador = GetComponent<Jugador>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    public void CogerHeno(GameObject prefabheno, Transform mano)
    {
        // Para el tutorial
        alimentacionRealizada = false;

        jugador.HenoRecogido = true;
        heno = Instantiate(prefabheno);
        
        animator.SetTrigger("heno");
        fxMontonHeno();
        
        audioSource.PlayOneShot(sonidosCogerHeno[Random.Range(0, sonidosCogerHeno.Length)]);

        heno.transform.position = mano.position;
        heno.transform.SetParent(mano);

        
    }

    public void DejarHeno()
    {
        Destroy(heno);
        jugador.HenoRecogido = false;

        animator.SetTrigger("dejarObjeto");

        // Para el tutorial
        alimentacionRealizada = true;
    }

    private void fxMontonHeno()
    {
        animatorHeno.SetTrigger("coger");
        particulasHeno.Play();
    }
}
