using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NumberBox : MonoBehaviour
{
    private Action<int, int> swapFunc = null;
    public int index = 0;
    int x = 0;
    int y = 0;
    public void Init(int i, int j, int index, Sprite sprite, Action<int, int> swapFunc)
    {
        this.index = index;
        this.GetComponent<SpriteRenderer>().sprite = sprite;
        UpdatePos(i, j);
        this.swapFunc = swapFunc;
    }
    public void UpdatePos(int i, int j)
    {
        x = i;
        y = j;
        // this.gameObject.transform.localPosition = new Vector2(i, j);
        StartCoroutine(Move());

    }
    void OnMouseDown()
    {
        if (Input.GetMouseButton(0) && swapFunc != null)
        {
            swapFunc(x, y);
        }
    }
    public bool IsEmpty()
    {
        return index == 16;
    }

    IEnumerator Move()
    {
        float elapsedTime = 0;
        float duration = 0.1f;
        Vector2 start = this.gameObject.transform.localPosition;
        Vector2 end = new Vector2(x, y);

        while (elapsedTime < duration)
        {
            this.gameObject.transform.localPosition = Vector2.Lerp(start, end, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        this.gameObject.transform.localPosition = end; 
    }
}
