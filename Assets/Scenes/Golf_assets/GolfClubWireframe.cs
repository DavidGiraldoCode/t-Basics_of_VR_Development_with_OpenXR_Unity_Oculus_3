using System.Collections.Generic;
using UnityEngine;

public class GolfClubWireframe : MonoBehaviour
{
    [SerializeField] private Transform[] _golfClubWireframeNodes;
    private Vector3[] _golfClubWireframeNodesPositions;
    [SerializeField] private LineRenderer _lineRenderer = null;

    private void Awake()
    {
        //_golfClubWireframeNodes = new Transform[4];
        _golfClubWireframeNodesPositions = new Vector3[4];

        if(!_lineRenderer)
            _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.positionCount = 4;
    }

    private void Update()
    {
        ConnectNodesWithLineRenderer();
    }
    private void ConnectNodesWithLineRenderer()
    {
        for (uint i = 0; i < _golfClubWireframeNodes.Length; i++)
        {
            _golfClubWireframeNodesPositions[(int)i] = _golfClubWireframeNodes[(int)i].position;
        }
        _lineRenderer.positionCount = 4;
        _lineRenderer.SetPositions(_golfClubWireframeNodesPositions);
    }
}
