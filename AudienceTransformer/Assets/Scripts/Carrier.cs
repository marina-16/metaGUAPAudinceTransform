using UnityEngine;

public class Carrier : MonoBehaviour
{
    // �����, ������ �������
    [SerializeField] private Transform _head;
    // �����-��������������� ��������
    [SerializeField] private RaycastDrag _raycastDrag;
    // ����� ����
    [SerializeField] private float _rayLength = 5f;

    // ���� ��������������, true, ���� ������� ������ �� null
    public bool IsDraging => _raycastDrag.CurrentDragable != null;

    // �������� �������, ���� �� �� null
    public void TryRotate(float angle)
    {
        if (_raycastDrag.CurrentDragable == null)
        {
            return;
        }
        _raycastDrag.CurrentDragable.Rotate(angle);
    }

    // �������� �������, ���� �� �� null
    public void TryRemove()
    {
        if (_raycastDrag.CurrentDragable == null)
        {
            return;
        }

        var dragable = _raycastDrag.CurrentDragable;

        _raycastDrag.Put();

        Destroy(dragable.gameObject);
    }

    // ������� ������ �������������� �������, �� ������� �� �������
    public void TryDrag()
    {
        if (_raycastDrag.CurrentDragable != null)
        {
            return;
        }

        if (Physics.Raycast(_head.position + _head.forward,
            _head.forward, out RaycastHit hit, _rayLength))
        {
            if (hit.collider.transform.root.gameObject.TryGetComponent<Dragable>(out var dragable))
            {
                // ���� ��� ��, ����� ������
                _raycastDrag.Take(dragable);
            }
        }
    }

    // ������� ���������� ������
    public void TryDrop()
    {
        if (_raycastDrag.CurrentDragable == null 
            || !_raycastDrag.IsGrounded 
            || _raycastDrag.CurrentDragable.Collision)
        {
            return;
        }
        // ���� ��� ��, ���������
        _raycastDrag.Put();
    }
}
