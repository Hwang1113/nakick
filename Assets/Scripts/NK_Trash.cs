using UnityEngine;

public class NK_Trash : MonoBehaviour
{
    private int touchCount = 0; // ��ġ Ƚ�� �ޱ�
    private const int requiredtouchs = 3; // �� �ʿ� ��ġ Ƚ��(hp)
    private bool isTouched = false; // �̹� ��ġ�ؼ� ���� �ο��� �޾Ҵ��� Ȯ��

    public delegate void TrashTouchEvent(NK_Player player);
    public static event TrashTouchEvent OnTrashtouched; // ��ġ �̺�Ʈ

    // �÷��̾ Ŭ��(��ġ)�� ������ ȣ��Ǵ� �޼���
    public void PlayerTouch(NK_Player player)
    {
        // �̹� ������ �ο����� ��� �� �̻� ��ġ���� �ʵ��� ó��
        if (isTouched) return;

        touchCount++;  // ��ġ Ƚ�� ����

        // 3���� ��ġ�� �Ϸ��� ���
        if (touchCount >= requiredtouchs)
        {
            // ���� 3�� ��ġ�� �÷��̾�� ���ھ� �ο�
            OnTrashtouched?.Invoke(player);

            // ���� �ο� �� �� �̻� ������ �ο����� �ʵ��� ����
            isTouched = true;

            // ������Ʈ �ı�
            Destroy(gameObject);
        }
    }

    // ��ġ �̺�Ʈ�� ���� ������ ��� �� ����
    private void OnEnable()
    {
        // �̺�Ʈ�� �̹� ��ϵǾ� �ִ��� üũ�Ͽ� �ߺ� ����� ����
        if (OnTrashtouched == null)
        {
            OnTrashtouched += HandleTrashTouched;
        }
    }

    private void OnDisable()
    {
        // �̺�Ʈ �����ʸ� ����
        OnTrashtouched -= HandleTrashTouched;
    }

    // Trash Ŭ�� �Ϸ� �� ó�� (���ھ� ���� ��)
    private void HandleTrashTouched(NK_Player player)
    {
        Debug.Log($"{player.name} has completed 3 clicks and earned a point!");
        player.AddScore(1); // Ŭ���� �Ϸ��� �÷��̾�� 1�� �߰�
    }
}