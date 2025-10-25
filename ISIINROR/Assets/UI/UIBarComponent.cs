using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarComponent : MonoBehaviour
{
    [SerializeField] private RectTransform _faceImage;
    [SerializeField] private Text _barText;

    public void ChangeBar(float currentValue, float maxValue)
    {
        float percent = currentValue / maxValue;
        _faceImage.localScale = new Vector3(percent, 1f, 1f);

        _barText.text = $"{currentValue.ToString()}/{maxValue.ToString()}";
    }
}
