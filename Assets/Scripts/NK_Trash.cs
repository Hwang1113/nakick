using UnityEngine;

public class NK_Trash : MonoBehaviour
{
    private int touchCount = 0; //��ġȽ�� �ޱ�
    private const int requiredtouchs = 3; //�� �ʿ� ��ġ Ƚ��(hp)

    public delegate void TrashTouchEvent(Player player);
    public static event TrashTouchEvent OnTrashtouched; // ��ġ �̺�Ʈ

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

    // ��ġ �̺�Ʈ�� ���� ������ ��� �� ����
    private void OnEnable()
    {
        OnTrashtouched += HandleTrashTouched;
    }

    private void OnDisable()
    {
        OnTrashtouched -= HandleTrashTouched;
    }

    // Trash Ŭ�� �Ϸ� �� ó�� (���ھ� ���� ��)
    private void HandleTrashTouched(Player player)
    {
        Debug.Log($"{player.name} has completed 10 clicks and earned a point!");
        player.AddScore(1); // Ŭ���� �Ϸ��� �÷��̾�� 1�� �߰�
    }
}
