using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    [SerializeField] private int _startPool;

    [SerializeField] private GameObject _effectPref;
    private Queue<GameObject> _effectsQ;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _effectsQ = new Queue<GameObject>();

        for (int i = 0; i < _startPool; i++)
        {
            CreateEffect();
        }
    }

    private void CreateEffect()
    {
        GameObject effect = Instantiate(_effectPref);
        effect.transform.SetParent(transform);
        effect.transform.localPosition = Vector2.zero;
        effect.SetActive(false);

        _effectsQ.Enqueue(effect);
    }

    public EffectAnimator GetEffect(Vector2 spawnPos, int damage, bool isCritical)
    {
        if (_effectsQ.Count == 0)
        {
            CreateEffect();
        }

        GameObject effect = _effectsQ.Dequeue();
        effect.SetActive(true);
        effect.transform.SetParent(null);
        spawnPos += new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
        effect.transform.position = spawnPos;

        effect.GetComponentInChildren<TextMeshPro>().text = "-" + damage;

        EffectAnimator effectAnimator = effect.GetComponent<EffectAnimator>();
        effectAnimator.OnAnimationEnds += ReturnEffect;

        return effectAnimator;
    }

    public void ReturnEffect(GameObject effect)
    {
        if (_effectsQ.Contains(effect)) return;

        _effectsQ.Enqueue(effect);

        EffectAnimator effectAnimator = effect.GetComponent<EffectAnimator>();
        effectAnimator.OnAnimationEnds -= ReturnEffect;

        effect.transform.SetParent(transform);
        effect.transform.localPosition = Vector2.zero;
        effect.SetActive(false);
    }
}
