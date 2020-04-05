using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {
    private Vector3 mousePos;
    public GameObject roulette;
    private float x=0;
    private float y = 0;
    public GameObject[] activeInsideList;
    public GameObject[] inactiveInsideList;

    public GameObject actInside;
    public GameObject inactInside;
    public Image[] outside;
    private float width;
    private float height;
    public GameObject[] emoji;
    private GameObject actEmoji;
    private Image actOutside;
    public Color oriColor;
    // Start is called before the first frame update
    void Start() {
        width = Screen.width;
        height = Screen.height;
        oriColor = outside[0].color;
    }

    // Update is called once per frame
    void Update() {
        mousePos = Input.mousePosition;
        x = mousePos.x;
        y = mousePos.y;
        Debug.Log(x + " " + y);
        if (Input.GetMouseButton(0)) {
            roulette.SetActive(true);
            if (x-width/2 < 0 && y-height/2 > 0) {
                Change(0);
            }
            else if (x - width / 2 > 0 && y - height / 2 > 0) {
                Change(1);
            }
            else if (x - width / 2 > 0 && y - height / 2 < 0) {
                Change(2);
            }
            else {
                Change(3);
            }
            actInside.SetActive(true);
            inactInside.SetActive(false);
            actEmoji.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0)) {
            actInside.SetActive(false);
            inactInside.SetActive(true);
            roulette.SetActive(false);
            actOutside.color = oriColor;
            actEmoji.SetActive(false);
            //TODO:设置动画表情
        }
        
    }

    void Change(int index) {
        if (actInside != null && inactInside != null) {
            actInside.SetActive(false);
            inactInside.SetActive(true);
            actEmoji.SetActive(false);
            actInside.transform.parent.transform.parent.transform.Find("Outside").GetComponent<Image>().color = oriColor;
        }

        actEmoji=emoji[index];
        actInside = activeInsideList[index];
        inactInside = inactiveInsideList[index];
        actOutside = outside[index];
        outside[index].color = new Color(oriColor.r, oriColor.g, oriColor.b, oriColor.a + 0.17f);

    }
}
