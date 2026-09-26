using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]
 
    // Prefab for instantiating apples
    public GameObject applePrefab;
 
    // NEW: prefab for the falling object you don't want to catch
    public GameObject branchPrefab;
 
    // NEW: chance that a given drop is a branch instead of an apple
    [Range(0f, 1f)]
    public float branchChance = 0.12f; // ~1 in 8 drops - tweak to taste
 
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
        // NEW: randomly choose a branch instead of an apple
        GameObject prefabToDrop = (branchPrefab != null && Random.value < branchChance)
            ? branchPrefab
            : applePrefab;
 
        GameObject apple = Instantiate<GameObject>(prefabToDrop);
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