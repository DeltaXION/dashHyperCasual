using UnityEngine;

public class BlockSpawner : MonoBehaviour {

	public Transform[] blockSpawnPoints;

    public Transform[] dashSpwanPoints;

	public GameObject blockPrefab;
    public GameObject dashPrefab;

	public float timeBetweenWaves = 1f;

	private float timeToSpawn = 2f;

	void Update () {

		if (Time.time >= timeToSpawn)
		{
			SpawnBlocks();
            SpawnDash();
			timeToSpawn = Time.time + timeBetweenWaves;
		}

	}

	void SpawnBlocks ()
	{
		int randomIndex = Random.Range(0, blockSpawnPoints.Length);

		for (int i = 0; i < blockSpawnPoints.Length; i++)
		{
			if (randomIndex != i)
			{
				Instantiate(blockPrefab, blockSpawnPoints[i].position, Quaternion.identity);
			}
		}
	}

    void SpawnDash()
    {
        int randomIndex = Random.Range(0, dashSpwanPoints.Length);

        for (int i = 0; i < dashSpwanPoints.Length; i++)
        {
            if (randomIndex == i)
            {
                Instantiate(dashPrefab, dashSpwanPoints[i].position, Quaternion.identity);
            }
        }
    }
}
