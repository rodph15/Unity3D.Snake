using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public List<Transform> tail;
    bool ate = false;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            direction = Vector3.up;
            


        if (Input.GetKeyDown(KeyCode.DownArrow))
            direction = Vector3.down;
            


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector3.right;
            GetComponent<SpriteRenderer>().flipX = false;
            tail.ForEach(item => item.GetComponent<SpriteRenderer>().flipX = false);
        }
            


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector3.left;
            GetComponent<SpriteRenderer>().flipX = true;
            tail.ForEach(item => item.GetComponent<SpriteRenderer>().flipX = true);
        }
            

    }

    private void Move()
    {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(direction);


        // Ate something? Then insert new Element into gap
        if (ate)
        {
            // Load Prefab into the world
            var g = Instantiate(GameObject.FindWithTag("cola"),
                                                  v,
                                                  Quaternion.identity);

            g.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("cola")) 
            //SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("cerveja"))
        {
            ate = true;
            Instantiate(collision.gameObject, new Vector3(Random.Range(-22, 21), Random.Range(-8, 8), 0), Quaternion.identity);
            Destroy(collision.gameObject);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
 
}
