using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailGenerator : MonoBehaviour
{
    [SerializeField] private Segment _segmanteTemplate; 

    public List<Segment> Generate(int count) // вызываем в Snake
    {
        List<Segment> tail = new List<Segment>();
        for(int i = 0; i< count; i++)
        {
            tail.Add(Instantiate(_segmanteTemplate, transform));
        }
        return tail;
    }
}
