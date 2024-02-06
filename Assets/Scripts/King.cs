using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{

    public Transform player;

    public GameObject prefabTomatoes;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throw() 
    {
        GameObject newObject = Instantiate(prefabTomatoes, transform.position, Quaternion.identity);

        ObjectThrower objectThrower = newObject.GetComponent<ObjectThrower>();
        objectThrower.targetPoint = player.transform;
        objectThrower.throwPoint = this.transform;
    }

    public void SetSprite(int index)
    {
        spriteRenderer.sprite = sprites[index];
        StartCoroutine(BackToDefault());
    }

    IEnumerator BackToDefault()
    {
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = sprites[0];
    }
}
