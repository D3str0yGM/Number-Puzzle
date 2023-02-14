using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle : MonoBehaviour
{
    [SerializeField] GameObject Menu;

    public NumberBox boxPrefab;
    public NumberBox[,] boxes = new NumberBox[4, 4];
    public Sprite[] sprites;
    public List<GameObject> BoxesList;

    private void Update()
    {
        if (BoxesList[15].gameObject.transform.localPosition.x == 0 && //1
        BoxesList[15].gameObject.transform.localPosition.y == 3 &&
       BoxesList[14].gameObject.transform.localPosition.x == 1 && //2
       BoxesList[14].gameObject.transform.localPosition.y == 3 &&
       BoxesList[13].gameObject.transform.localPosition.x == 2 && //3
       BoxesList[13].gameObject.transform.localPosition.y == 3
       &&
       BoxesList[12].gameObject.transform.localPosition.x == 3 && //4
       BoxesList[12].gameObject.transform.localPosition.y == 3
       &&
       BoxesList[11].gameObject.transform.localPosition.x == 0 && //5
       BoxesList[11].gameObject.transform.localPosition.y == 2
       &&
       BoxesList[10].gameObject.transform.localPosition.x == 1 && //6
       BoxesList[10].gameObject.transform.localPosition.y == 2
       &&
       BoxesList[9].gameObject.transform.localPosition.x == 2 && //7
       BoxesList[9].gameObject.transform.localPosition.y == 2
       &&
       BoxesList[8].gameObject.transform.localPosition.x == 3 && //8
       BoxesList[8].gameObject.transform.localPosition.y == 2
       &&
       BoxesList[7].gameObject.transform.localPosition.x == 0 && //9
       BoxesList[7].gameObject.transform.localPosition.y == 1
       &&
       BoxesList[6].gameObject.transform.localPosition.x == 1 && //10
       BoxesList[6].gameObject.transform.localPosition.y == 1
       &&
       BoxesList[5].gameObject.transform.localPosition.x == 2 && //11
       BoxesList[5].gameObject.transform.localPosition.y == 1
       &&
       BoxesList[4].gameObject.transform.localPosition.x == 3 && //12
       BoxesList[4].gameObject.transform.localPosition.y == 1
       &&
       BoxesList[3].gameObject.transform.localPosition.x == 0 && //13
       BoxesList[3].gameObject.transform.localPosition.y == 0
       &&
       BoxesList[2].gameObject.transform.localPosition.x == 1 && //14
       BoxesList[2].gameObject.transform.localPosition.y == 0
       &&
       BoxesList[1].gameObject.transform.localPosition.x == 2 && //15
       BoxesList[1].gameObject.transform.localPosition.y == 0)
        {
            Menu.SetActive(true);
        }
        else
        {
Menu.SetActive(false);
        }
    }

    private void Awake()
    {

    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    void Start()
    {
        Init();


        foreach (var item in FindObjectsOfType<NumberBox>())
        {
            BoxesList.Add(item.gameObject);
        }


        for (int i = 0; i < 3; i++)
            Shuffle();
        Menu.SetActive(false);
    }

    void Init()
    {
        int n = 0;
        for (int y = 3; y >= 0; y--)
        {
            for (int x = 0; x < 4; x++)
            {
                NumberBox box = Instantiate(boxPrefab, new Vector2(x, y), Quaternion.identity);
                box.Init(x, y, n + 1, sprites[n], ClickToSwap);
                boxes[x, y] = box;
                n++;
                boxPrefab.tag = n.ToString();
            }
        }
    }

    void ClickToSwap(int x, int y)
    {

        int dx = getDx(x, y);
        int dy = getDy(x, y);
        Swap(x, y, dx, dy);

    }

    void Swap(int x, int y, int dx, int dy)
    {


        var from = boxes[x, y];
        var target = boxes[x + dx, y + dy];

        boxes[x, y] = target;
        boxes[x + dx, y + dy] = from;

        from.UpdatePos(x + dx, y + dy);
        target.UpdatePos(x, y);
    }

    int getDx(int x, int y)
    {
        if (x < 3 && boxes[x + 1, y].IsEmpty())
            return 1;

        if (x > 0 && boxes[x - 1, y].IsEmpty())
            return -1;

        return 0;
    }
    int getDy(int x, int y)
    {
        if (y < 3 && boxes[x, y + 1].IsEmpty())
            return 1;
        if (y > 0 && boxes[x, y - 1].IsEmpty())
            return -1;

        return 0;
    }
    void Shuffle()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (boxes[i, j].IsEmpty())
                {
                    Vector2 pos = getValidMove(i, j);
                    Swap(i, j, (int)pos.x, (int)pos.y);
                }
            }
        }
    }

    private Vector2 lastMove;

    Vector2 getValidMove(int x, int y)
    {
        Vector2 pos = new Vector2();
        do
        {
            int n = Random.Range(0, 4);
            if (n == 0)
                pos = Vector2.left;
            else if (n == 1)
                pos = Vector2.right;
            else if (n == 2)
                pos = Vector2.up;
            else
                pos = Vector2.down;
        } while (!(isValidRange(x + (int)pos.x) && isValidRange(y + (int)pos.y)) || isRepeatMove(pos));

        lastMove = pos;
        return pos;
    }
    bool isValidRange(int n)
    {
        return n >= 0 && n <= 3;
    }

    bool isRepeatMove(Vector2 pos)
    {
        return pos * -1 == lastMove;
    }
}
