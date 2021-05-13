using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Block))]
public class BlockView : MonoBehaviour
{
    [SerializeField] private TMP_Text _view;
    private Block _block;

    private void Awake()
    {
        _block = GetComponent<Block>();
    }

    private void OnEnable()
    {
        _block.FillingUpdate += OnFillingUpdated;
    }

    private void OnDisable()
    {
        _block.FillingUpdate -= OnFillingUpdated;
    }

    private void OnFillingUpdated(int leftToFill)
    {
        _view.text = leftToFill.ToString();
    }
}
