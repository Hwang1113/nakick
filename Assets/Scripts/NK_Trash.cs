using UnityEngine;

public class NK_Trash : MonoBehaviour
{
    private int touchCount = 0; //터치횟수 받기
    private const int requiredtouchs = 3; //총 필요 터치 횟수(hp)

    public delegate void TrashTouchEvent(Player player);
    public static event TrashTouchEvent OnTrashtouched; // 터치 이벤트

    // 플레이어가 클릭(터치)할 때마다 호출되는 메서드
    public void PlayerTouch(Player player)
    {
        touchCount++;  // 터치 횟수 증가

        // 10번의 터치를 완료한 경우
        if (touchCount >= requiredtouchs)
        {
            // 먼저 10번 터치한 플레이어에게 스코어 부여
            OnTrashtouched?.Invoke(player);

            Destroy(gameObject);
        }
    }

    // 터치 이벤트에 대한 리스너 등록 및 해제
    private void OnEnable()
    {
        OnTrashtouched += HandleTrashTouched;
    }

    private void OnDisable()
    {
        OnTrashtouched -= HandleTrashTouched;
    }

    // Trash 클릭 완료 후 처리 (스코어 증가 등)
    private void HandleTrashTouched(Player player)
    {
        Debug.Log($"{player.name} has completed 10 clicks and earned a point!");
        player.AddScore(1); // 클릭을 완료한 플레이어에게 1점 추가
    }
}
