using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName = "Player";
    public int score = 0;

    // Ʈ���� �浹�� �߻����� �� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� "Trash" �±׸� �������� Ȯ��
        if (other.CompareTag("Trash"))
        {
            // Trash ������Ʈ���� PlayerClick �޼��带 ȣ���Ͽ� Ŭ���� ����
            NK_Trash trash = other.GetComponent<NK_Trash>();
            if (trash != null)
            {
                trash.PlayerTouch(this); // Player�� Ŭ���� ����
            }
        }
    }

    // �÷��̾��� ���ھ� ����
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"{playerName} Score: {score}");
    }
}