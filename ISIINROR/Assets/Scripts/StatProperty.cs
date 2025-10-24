using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatProperty
{
    [SerializeField]
    private bool _isInverted;
    [SerializeField]
    private bool _isAllwaysPositive;

    [HideInInspector]
    public bool IsInverted { get { return _isInverted; } }

    [SerializeField]
    private float _baseValue;
    public float BaseValue { get { return _baseValue; } }

    [SerializeField]
    private float _value;
    public float Value { get { return _value; } }


    [SerializeField]
    private List<float> _percentBuffs = new List<float>();

    [SerializeField]
    private List<float> _trueBuffs = new List<float>();

    public event Action<float> StatChanged;

    public StatProperty(float baseValue, bool isInverted = false, bool isAllwaysPositive = true)
    {
        _baseValue = baseValue;
        _isInverted = isInverted;
        _isAllwaysPositive = isAllwaysPositive;

        UpdateStat();
    }

    public void AddPercentBuff(float buff)
    {
        _percentBuffs.Add(buff);
        UpdateStat();
    }

    public void RemovePercentBuff(float buff)
    {
        _percentBuffs.Remove(buff);
        UpdateStat();
    }

    public void AddTrueBuff(float buff)
    {
        _trueBuffs.Add(buff);
        UpdateStat();
    }

    public void RemoveTrueBuff(float buff)
    {
        _trueBuffs.Remove(buff);
        UpdateStat();
    }

    public void UpdateStat()
    {
        float newValue = _baseValue;

        foreach (float buff in _trueBuffs)
        {
            newValue += buff;
        }

        float newPercent = 1f;

        foreach (float buff in _percentBuffs)
        {
            newPercent += buff;
        }

        newValue *= newPercent;

        if(_isAllwaysPositive && newValue <= 0)
        {
            newValue = 0;
        }

        _value = newValue;

        StatChanged?.Invoke(_value);
    }
}