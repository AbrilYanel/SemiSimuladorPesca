using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QualityScript : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int calidad;
    void Start()
    {
        calidad = PlayerPrefs.GetInt("NumeroDeCalidad", 2);
        dropdown.value = calidad;
        AjustarCalidad();
    }

   
  public void AjustarCalidad()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("NumeroDeCalidad", dropdown.value);
        calidad = dropdown.value;
    }
}
