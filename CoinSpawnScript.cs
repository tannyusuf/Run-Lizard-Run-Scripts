using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnScript : MonoBehaviour
{
    public GameObject coinPrefab;                      // Coin prefab�
    public List<Transform> spawnPoints;                // Coin koyulabilecek yerler
    public int numberOfCoinsToSpawn = 100;              // Her tur ka� tane coin ��kacak

    private List<GameObject> currentCoins = new List<GameObject>(); // Mevcut coinler

    void Start()
    {
        SpawnCoins();
    }

    public void SpawnCoins()
    {
        // �nceki coinleri temizle
        foreach (GameObject coin in currentCoins)
        {
            Destroy(coin);
        }
        currentCoins.Clear();

        // Spawn noktalar�n� kar��t�r
        List<Transform> shuffledPoints = new List<Transform>(spawnPoints);
        Shuffle(shuffledPoints);
        float fixedY = 0.15f;

        Quaternion coinRotation = Quaternion.Euler(90f, -443.304f, 994.083f);

        // �lk 40 tanesine coin yerle�tir
        for (int i = 0; i < numberOfCoinsToSpawn; i++)
        {

            Vector3 spawnPos = shuffledPoints[i].position;
            spawnPos.y = fixedY;
            GameObject newCoin = Instantiate(coinPrefab, shuffledPoints[i].position, coinPrefab.transform.rotation);

            newCoin.transform.position = new Vector3(spawnPos.x, fixedY, spawnPos.z);
            Collider col = newCoin.GetComponent<Collider>();
            if (col != null)
            {
                col.isTrigger = true;

                if (col is CapsuleCollider capsule)
                {
                    capsule.radius = 1.5f;   // Geni�lik
                    capsule.height = 2.0f;   // Y�kseklik
                    capsule.direction = 1;   // 0 = X, 1 = Y, 2 = Z
                }
            }

            currentCoins.Add(newCoin);
        }
    }

    // Listeyi kar��t�rmak i�in Fisher�Yates algoritmas�
    void Shuffle(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            Transform temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
