using UnityEngine;

public class Instancer : MonoBehaviour
{
    // ����������� ������
    [SerializeField] private Dragable _dragable;
    // �����-��������������� ��������
    [SerializeField] private RaycastDrag _raycastDrag;

    // �������� ������ �� �����
    public void Attach(Dragable dragable)
    {
        _dragable = Instantiate(dragable);
        _raycastDrag.Take(_dragable);
    }

    // ��������� ���������� �������
    public void Apply()
    {
        _raycastDrag.Put();

        _dragable = null;
    }

    // �������� ���������� �������
    public void Cancel()
    {
        _raycastDrag.Put();

        Destroy(_dragable.gameObject);

        _dragable = null;
    }
}
