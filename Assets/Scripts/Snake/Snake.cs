using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TailGenerator))]
[RequireComponent(typeof(SnakeInput))]

public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead _head;
    [SerializeField] private float _speed;
    [SerializeField] private float _tailSpringles;
    [SerializeField] private int _tailSize;

    private SnakeInput _input;
    private List<Segment> _tail;
    private TailGenerator _tailGenerator;

    public event UnityAction<int> SizeUpdated;

    private void Awake()
    {
        _tailGenerator = GetComponent<TailGenerator>();
        _input = GetComponent<SnakeInput>();
        _tail = _tailGenerator.Generate(_tailSize);
        SizeUpdated?.Invoke(_tail.Count);
    }

    private void OnEnable()
    {
        _head.BlockCollided += OnBlockCollided;
        _head.BonusCollected += OnBonusCollected;
    }

    private void OnDisable()
    {
        _head.BlockCollided -= OnBlockCollided;
        _head.BonusCollected -= OnBonusCollected;
    }
    private void FixedUpdate()
    {
        Move(_head.transform.position + _head.transform.up * _speed * Time.fixedDeltaTime);
        _head.transform.up = _input.GetDirectionToClick(_head.transform.position);
    }

    private void Move(Vector3 nextPosition)
    {
        Vector3 previosPosition = _head.transform.position;

        foreach( var segment in _tail)
        {
            Vector3 tempPosition = segment.transform.position;
            segment.transform.position = Vector3.Lerp(segment.transform.position, previosPosition, _tailSpringles * Time.fixedDeltaTime);
            previosPosition = tempPosition;
        }
        _head.Move(nextPosition);
    }

    private void OnBlockCollided()
    {
        Segment deleteSegment = _tail[_tail.Count - 1];
        _tail.Remove(deleteSegment);
        Destroy(deleteSegment.gameObject);
        SizeUpdated?.Invoke(_tail.Count);
    }

    private void OnBonusCollected(int bonusSize)
    {
       _tail.AddRange(_tailGenerator.Generate(bonusSize));
        SizeUpdated?.Invoke(_tail.Count);
    }
}
