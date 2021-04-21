using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCursorCollisionCheck : MonoBehaviour
{
    public bool isBuildSiteClear = true;

    [SerializeField]
    List<Collider2D> collisions = new List<Collider2D>();

    SpriteRenderer sprite;

    [SerializeField]
    TilemapInteractor ti;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (collisions.Count <= 0)
        {
            isBuildSiteClear = true;
        }
        else
        {
            isBuildSiteClear = false;
        }

        sprite.enabled = ti.CanBuildHere();
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collisions.Contains(collider))
            collisions.Add(collider);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collisions.Contains(collider))
            collisions.Remove(collider);
    }
}
