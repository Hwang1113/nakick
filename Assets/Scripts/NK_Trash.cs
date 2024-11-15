using UnityEngine;

public class NK_Trash : MonoBehaviour
{
    private int touchCount = 0; //��ġȽ�� �ޱ�
    private const int requiredtouchs = 3; //�� �ʿ� ��ġ Ƚ�� 

    public delegate void TrashClickedEvent(Player player);
    public static event TrashClickedEvent OnTrashtouched; // Ŭ�� �̺�Ʈ

    // �÷��̾ Ŭ��(��ġ)�� ������ ȣ��Ǵ� �޼���
    public void PlayerTouch(Player player)
    {
        touchCount++;  // ��ġ Ƚ�� ����

        // 10���� ��ġ�� �Ϸ��� ���
        if (touchCount >= requiredtouchs)
        {
            // ���� 10�� ��ġ�� �÷��̾�� ���ھ� �ο�
            OnTrashtouched?.Invoke(player);

            Destroy(gameObject);
        }
    }

    // Ŭ�� �̺�Ʈ�� ���� ������ ��� �� ����
    private void OnEnable()
    {
        OnTrashtouched += HandleTrashClicked;
    }

    private void OnDisable()
    {
        OnTrashtouched -= HandleTrashClicked;
    }

    // Trash Ŭ�� �Ϸ� �� ó�� (���ھ� ���� ��)
    private void HandleTrashClicked(Player player)
    {
        Debug.Log($"{player.name} has completed 10 clicks and earned a point!");
        player.AddScore(1); // Ŭ���� �Ϸ��� �÷��̾�� 1�� �߰�
    }
}
