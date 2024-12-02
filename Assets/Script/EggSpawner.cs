using System.Collections;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    public GameObject eggPrefab;               // Prefab trứng
    public Transform[] spawnPoints;            // Các vị trí sinh trứng
    public float initialSpawnDelay = 2f;       // Thời gian chờ ban đầu
    public float spawnDelayDecrement = 0.2f;   // Giảm thời gian chờ sau mỗi khoảng thời gian
    public float minimumSpawnDelay = 0.5f;     // Thời gian chờ tối thiểu giữa các lần sinh trứng
    public float spawnAccelerationInterval = 10f; // Khoảng thời gian để giảm thời gian chờ sinh trứng
    public float startDelay = 5f;              // Thời gian chờ trước khi bắt đầu thả trứng

    private float currentSpawnDelay;

    void Start()
    {
        currentSpawnDelay = initialSpawnDelay; // Khởi tạo thời gian chờ ban đầu
        StartCoroutine(SpawnEggs());           // Bắt đầu coroutine sinh trứng
        StartCoroutine(AccelerateSpawning());  // Bắt đầu coroutine tăng tần suất sinh trứng
    }

    // Coroutine sinh trứng liên tục
    private IEnumerator SpawnEggs()
    {
        // Chờ thời gian ban đầu trước khi bắt đầu thả trứng
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            SpawnEgg(); // Sinh trứng
            yield return new WaitForSeconds(currentSpawnDelay); // Đợi thời gian trước khi sinh trứng tiếp theo
        }
    }

    // Coroutine tăng tốc độ sinh trứng
    private IEnumerator AccelerateSpawning()
    {
        while (currentSpawnDelay > minimumSpawnDelay)
        {
            yield return new WaitForSeconds(spawnAccelerationInterval); // Đợi trước khi giảm thời gian chờ
            currentSpawnDelay = Mathf.Max(currentSpawnDelay - spawnDelayDecrement, minimumSpawnDelay); // Giảm thời gian chờ nhưng không vượt quá minimumSpawnDelay
        }
    }

    // Hàm sinh trứng tại vị trí ngẫu nhiên
    private void SpawnEgg()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length); // Chọn vị trí ngẫu nhiên để sinh trứng
        Transform spawnPoint = spawnPoints[randomIndex];

        // Sinh trứng tại vị trí được chọn
        Instantiate(eggPrefab, spawnPoint.position, Quaternion.identity);
    }
}
