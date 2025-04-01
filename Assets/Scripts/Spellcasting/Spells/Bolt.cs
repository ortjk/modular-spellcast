using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bolt: MonoBehaviour
{
    public float lifetime = 0.5f;
    public float fadeoutTime = 0.1f;

    public float variance = 0.1f;
    public float segmentation = 1f;

    private float _timer;
    private LineRenderer _lineRenderer;

    public void SetEndpoints(Vector3 origin, Vector3 target)
    {
        float dist = Vector3.Distance(origin, target);
        int numSegments = Mathf.CeilToInt(dist / segmentation);

        Vector3[] points = new Vector3[numSegments];

        points[0] = origin;
        points[numSegments - 1] = target;
        for (int i = 1; i < numSegments - 1; i++)
        {
            points[i] = Vector3.Lerp(origin, target, (float)i / numSegments);
            points[i] += (new Vector3(Random.value, Random.value, Random.value) * variance);
        }

        _lineRenderer.positionCount = numSegments;
        _lineRenderer.SetPositions(points);
    }

    private void ReduceOpacity(float amount)
    {
        _lineRenderer.startColor *= new Color(_lineRenderer.startColor.r, _lineRenderer.startColor.g, _lineRenderer.startColor.b, 255 * amount);
        _lineRenderer.endColor *= new Color(_lineRenderer.endColor.r, _lineRenderer.endColor.g, _lineRenderer.endColor.b, 255 * amount);
    }
    
    private void Awake()
    {
        _lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (lifetime - fadeoutTime <= _timer)
        {
            ReduceOpacity(  (lifetime - _timer) / (lifetime - fadeoutTime));
        }

        if (lifetime <= _timer)
        {
            Destroy(gameObject);
        }
    }
}