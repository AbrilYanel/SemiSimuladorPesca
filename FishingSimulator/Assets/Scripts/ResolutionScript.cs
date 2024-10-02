using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionScript : MonoBehaviour
{
    public Toggle toggle;

    public TMP_Dropdown  ResolutionDropdown;
    Resolution[] resoluciones;
    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn= false;
        }

        RevisarResolucion();

        
    }


    void Update()
    {
        
    }

    public void RevisarResolucion()
    {
        resoluciones = Screen.resolutions;
        ResolutionDropdown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++) 
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            opciones.Add(opcion);
            if(Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }
        ResolutionDropdown.AddOptions(opciones);
        ResolutionDropdown.value = resolucionActual;
        ResolutionDropdown.RefreshShownValue();
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        Resolution resolucion = resoluciones[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }
}
