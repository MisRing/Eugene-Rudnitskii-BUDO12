using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarComponent : MonoBehaviour
{
    [SerializeField] private Color _increaseColor;
    [SerializeField] private Color _decreaseColor;
    [SerializeField] private float _animationDelay = 0.3f;
    [SerializeField] private float _animateSpeed = 0.2f;
    [SerializeField] private RectTransform _changeImage;
    [SerializeField] private RectTransform _faceImage;
    [SerializeField] private Text _barText;

    private float _lastPercent = 1;
    private float _currentPercent = 1;
    private bool _isAnimate = false;
    private RectTransform _animateImage;

    public void ChangeBar(float currentValue, float maxValue)
    {
        _isAnimate = false;
        StopAllCoroutines();
        _currentPercent = currentValue / maxValue;

        if(_lastPercent < _currentPercent)
        {
            _changeImage.localScale = new Vector3(_currentPercent, 1f, 1f);
            _changeImage.GetComponent<Image>().color = _increaseColor;
            _animateImage = _faceImage;
        }
        else
        {
            _faceImage.localScale = new Vector3(_currentPercent, 1f, 1f);
            _changeImage.GetComponent<Image>().color = _decreaseColor;
            _animateImage = _changeImage;
        }

        _barText.text = $"{currentValue}/{maxValue}";

        StartCoroutine(Animate());
    }

    private void Update()
    {
        if(_isAnimate)
        {
            float value = _animateImage.localScale.x;

            if (value < _currentPercent)
            {
                value += _animateSpeed * Time.deltaTime;

                if(value >= _currentPercent)
                {
                    _isAnimate = false;
                    value = _currentPercent;
                }
            }
            else
            {
                value -= _animateSpeed * Time.deltaTime;

                if (value <= _currentPercent)
                {
                    _isAnimate = false;
                    value = _currentPercent;
                }
            }

            _lastPercent = value;

            _animateImage.localScale = new Vector3(value, 1f, 1f);
        }
    }

    private IEnumerator Animate()
    {
        yield return new WaitForSeconds(_animationDelay);

        _isAnimate = true;
    }
}
