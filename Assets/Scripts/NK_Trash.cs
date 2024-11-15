using UnityEngine;

public class NK_Trash : MonoBehaviour
{
    private int touchCount = 0; // 터치 횟수 받기
    private const int requiredtouchs = 3; // 총 필요 터치 횟수(hp)
    private bool isTouched = false; // 이미 터치해서 점수 부여를 받았는지 확인

    public delegate void TrashTouchEvent(Player player);
    public static event TrashTouchEvent OnTrashtouched; // 터치 이벤트

    // 플레이어가 클릭(터치)할 때마다 호출되는 메서드
    public void PlayerTouch(Player player)
    {
        // 이미 점수를 부여받은 경우 더 이상 터치되지 않도록 처리
        if (isTouched) return;

        touchCount++;  // 터치 횟수 증가

        // 3번의 터치를 완료한 경우
        if (touchCount >= requiredtouchs)
        {
            // 먼저 3번 터치한 플레이어에게 스코어 부여
            OnTrashtouched?.Invoke(player);

            // 점수 부여 후 더 이상 점수를 부여하지 않도록 설정
            isTouched = true;

            // 오브젝트 파괴
            Destroy(gameObject);
        }
    }

    // 터치 이벤트에 대한 리스너 등록 및 해제
    private void OnEnable()
    {
        // 이벤트가 이미 등록되어 있는지 체크하여 중복 등록을 방지
        if (OnTrashtouched == null)
        {
            OnTrashtouched += HandleTrashTouched;
        }
    }

    private void OnDisable()
    {
        // 이벤트 리스너를 해제
        OnTrashtouched -= HandleTrashTouched;
    }

    // Trash 클릭 완료 후 처리 (스코어 증가 등)
    private void HandleTrashTouched(Player player)
    {
        Debug.Log($"{player.name} has completed 3 clicks and earned a point!");
        player.AddScore(1); // 클릭을 완료한 플레이어에게 1점 추가
    }
}