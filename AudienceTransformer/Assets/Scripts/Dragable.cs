using UnityEngine;

public class Dragable : MonoBehaviour
{
    // ���� ������������ 
    public bool Collision {  get; private set; }

    // ������ ��������������
    public void Drag()
    {
        // ������ �� ����, ������������ ����
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        // ������ ���� �������� �������� ����
        foreach(Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }

    public void Drop()
    {
        // ������ �� ����������� ����
        gameObject.layer = LayerMask.NameToLayer("Default");

        // ������ ���� �������� �������� ����
        foreach (Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    // ������� �������
    public void Rotate(float angle)
    {
        transform.localEulerAngles += Vector3.up * angle;
    }

    // ���� �������� ��������
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Dragable>(out var _))
        {
            Collision = true;
        }
    }

    // ���� �������� �� ��������
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Dragable>(out var _))
        {
            Collision = false;
        }
    }
}
