using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalerySpawner : MonoBehaviour
{
    public GameObject paintingPrefab;
    private List<GameObject> paintings;
    private float currentOffset = 1.5f;
    private float currentPosition = 0;
    private float lastSpawn = 0;
    public float speed = 1.5f;
    
    // Start is called before the first frame update
    void Start(){
        paintings = new List<GameObject>();

        for (int i = 0; i < 4; i++)
        {
            SpawnRow();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float ver = Input.GetAxis("Vertical");
        if (ver != 0)
        {
            MovePaintings(ver * speed);
        }
    }

    void MovePaintings(float axis)
    {
        currentPosition -= axis * Time.deltaTime;
        for (int i = 0; i < paintings.Count; i++)
        {
            paintings[i].transform.position -= Vector3.forward * axis * Time.deltaTime;

            if (paintings[i].transform.position.z < -10)
            {
                Destroy(paintings[i]);
                paintings.RemoveAt(i);
            }
        }

        if (lastSpawn - currentPosition > 5)
        {
            SpawnRow();
        }
    }
    
    void SpawnRow()
    {
        paintings.Add(Instantiate(paintingPrefab, new Vector3(2.5f, 2f, currentPosition + currentOffset), Quaternion.identity));
        paintings.Add(Instantiate(paintingPrefab, new Vector3(-2.5f, 2f, currentPosition + currentOffset), Quaternion.Euler(0, 180, 0)));
        currentOffset += 5;
        lastSpawn = currentPosition;
    }
}
