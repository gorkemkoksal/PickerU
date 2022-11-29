using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections.ObjectModel;
using TMPro;

public class Collector : MonoBehaviour
{
    [SerializeField] private Animator blockerAnimator;
    [SerializeField] private Animator platformAnimator;
    [SerializeField] private ParticleSystem _particleCelebration;
    [SerializeField] private TextMeshPro collectionText;
    [SerializeField] private int _targetNumber = 10;
    [SerializeField] private List<GameObject> _collection = new List<GameObject>();

    private readonly int blockerOpeningHash = Animator.StringToHash("BlockerOpening");
    private readonly int platformTransportingHash = Animator.StringToHash("PlatformTransporting");

    public static event Action OpeningBorders;
    public static event Action StartPlayer;
    public static event Action PlayerFail;

    private float countTime = 2f;
    private bool countStarted;
    private void Start()
    {
        collectionText.text = $"{_collection.Count}  /  {_targetNumber}";
    }
    private void Update()
    {
        CountingCollectables();
    }
    private void CountingCollectables()
    {
        if (countStarted)
        {
            countTime -= Time.deltaTime;
            if (countTime <= 0)
            {
                countStarted = false;
                if (_collection.Count >= _targetNumber)
                {
                    StartCoroutine(OpeningSequence());
                }
                else
                {
                    PlayerFail?.Invoke();
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!_collection.Contains(collision.gameObject))
        {
            countStarted = true;
            countTime = 1f;
            _collection.Add(collision.gameObject);
            collectionText.text = $"{_collection.Count}   / /  {_targetNumber}";
        }
    }
    IEnumerator OpeningSequence()
    {
        OpeningBorders?.Invoke();
        yield return new WaitForSeconds(1);

        platformAnimator.Play(platformTransportingHash);
        yield return new WaitForSeconds(0.5f);
       
        _particleCelebration.Play();
        blockerAnimator.Play(blockerOpeningHash);
        yield return new WaitForSeconds(1);

        StartPlayer?.Invoke();
    }
}
