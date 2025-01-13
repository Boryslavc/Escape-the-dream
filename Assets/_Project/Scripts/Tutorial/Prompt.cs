using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Prompt
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image image;
    [SerializeField] private float lifeDuration;

    public float GetLifeTime() => lifeDuration;
    public void ActivateSelf()
    {
        if(text != null)
            text.gameObject.SetActive(true);

        if(image != null)
            image.gameObject.SetActive(true);
    }

    public void DeactivateSelf()
    {
        if (text != null)
            text.gameObject.SetActive(false);

        if (image != null)
            image.gameObject.SetActive(false);
    }
}
