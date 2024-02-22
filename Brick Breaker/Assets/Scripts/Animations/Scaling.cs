using UnityEngine;
using System.Collections;

public class Scaling : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _maxSize = 1.3f;
    [SerializeField] private float _minSize = .8f;

    private Vector3 _maxSizeVector => Vector3.one * _maxSize;
    private Vector3 _minSizeVector => Vector3.one * _minSize;

    private void Start()
    {
        StartCoroutine(Breath());
    }

    private IEnumerator Breath()
    {
      Vector3 targetScale = _maxSizeVector;
 
        while (true)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, Time.deltaTime * _speed);
 
            if (transform.localScale == targetScale)
            {
                if (targetScale == _maxSizeVector)
                    targetScale = _minSizeVector;
                else
                    targetScale = _maxSizeVector;
            }
 
            yield return null;
        }
    }
}
