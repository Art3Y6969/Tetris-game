using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Transform spawn;
    public GameObject[] groups;
    // Start is called before the first frame update
    void Start()
    {
        spawnNext();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void spawnNext()
    {
        int i = Random.Range(0, groups.Length);
        Instantiate(groups[i], spawn.position, Quaternion.identity);
    }
}
