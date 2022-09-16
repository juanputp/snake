using System.Collections.Generic;
using UnityEngine;

public class headScript : MonoBehaviour
{
    [SerializeField] public float frameRate = 0.2f;
    enum Dir{down,up,left,right,}

    Dir direction;
    public float step = 1.8f;
    public List<Transform> tail = new List<Transform>();
    public GameObject TailPrefab;
    public Vector2 horizontalRange;
    public Vector2 verticalRange;
    void Start()
    {
        InvokeRepeating("Move", frameRate, frameRate);
    }
    void Move()
    {
        lastPosition = transform.position;

        Vector3 nextPosition = Vector3.zero;
        if (direction == Dir.up)
            nextPosition = Vector3.up;
        else if (direction == Dir.down)
            nextPosition = Vector3.down;
        else if (direction == Dir.left)
            nextPosition = Vector3.left;
        else if (direction == Dir.right)
            nextPosition = Vector3.right;
        nextPosition *= step;
        transform.position += nextPosition;
        MoveTail();
    }

    void Update ()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
            direction = Dir.up;
        else if (Input.GetKeyUp(KeyCode.DownArrow))
            direction = Dir.down;
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
            direction = Dir.left;
        else if (Input.GetKeyUp(KeyCode.RightArrow))
            direction = Dir.right;
    }

    Vector3 lastPosition;
    void MoveTail()
    {
        for (int i = 0; i < tail.Count; i++)
        {
            Vector3 temp = tail[i].position;
            tail[i].position = lastPosition;
            lastPosition = temp;
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Block"))
        {
            print("Has perdido");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else if (col.CompareTag("Food"))
        {
            tail.Add(Instantiate(TailPrefab, tail[tail.Count - 1].position, Quaternion.identity).transform);
            col.transform.position = new Vector2(Random.Range(horizontalRange.x, horizontalRange.y), Random.Range(verticalRange.x, verticalRange.y));
        }
    }
}
