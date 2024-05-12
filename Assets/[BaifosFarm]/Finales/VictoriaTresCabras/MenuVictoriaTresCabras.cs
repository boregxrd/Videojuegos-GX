using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuVictoriaTresCabras : MenuBase
{
    protected override void Start()
    {
        base.Start();
        AccionesAtardecer.OnThreeBlackGoatsVictory += ShowMenu;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        AccionesAtardecer.OnThreeBlackGoatsVictory -= ShowMenu;
    }
}
