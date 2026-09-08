using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]

    // Prefab for instantiating apples
    public GameObject applePrefab;

    // Speed of apple tree
    public float speed = 1f;

    //Distance where apple tree turns around
    public float leftAndRightEdge = 10f;

    //Chance that apple tree will change direction
    public float changeDirChance = 0.1f;

    //Seconds between apple instantiations
    public float appleDropDelay = 1f;



    // Start is called before the first frame update
    void Start()
    {
        //Start Dropping Apples
        Invoke("DropApple", 2f);
    }

    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", appleDropDelay);
    }
    // Update is called once per frame
    void Update()
    {
        //basic movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        //changing direction
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);
        } else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed);
        } //else if (Random.value < changeDirChance)
        //{
            //speed *= -1;    
        //}

    }

    void FixedUpdate()
    {
        if (Random.value < changeDirChance)
        {
            speed *= -1;
        }
    }
}
