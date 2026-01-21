using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class UnitSelectorHUD : MonoBehaviour
{
    [SerializeField] private float _sizeMod = 0.75f;
    [SerializeField] private int _segments = 50;
    [SerializeField] private float _baseThickness = 0.075f;
    [SerializeField] private float _selectedThickness = 0.15f;
    [SerializeField] private Color _baseColor = Color.white;
    [SerializeField] private Color _selectedColor = Color.green;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = false;
    }

    public void SetSelector(bool selected, float unitSize)
    {
        if(selected)
        {
            DrawCircle(unitSize * _sizeMod, _selectedThickness, _selectedColor);
        }
        else
        {
            DrawCircle(unitSize * _sizeMod, _baseThickness, _baseColor);
        }
    }

    private void DrawCircle(float radius, float thickness, Color color)
    {
        _lineRenderer.startWidth = thickness;
        _lineRenderer.endWidth = thickness;

        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;

        _lineRenderer.positionCount = _segments + 1;

        Vector3[] points = new Vector3[_segments + 1];
        for (int i = 0; i <= _segments; i++)
        {
            float angle = Mathf.Deg2Rad * (i * 360f / _segments);

            points[i] = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
        }

        _lineRenderer.SetPositions(points);
    }
}
