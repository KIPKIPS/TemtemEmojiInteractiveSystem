using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {
    public Animator roleAnim;//人物角色动画机
    private bool canPlay = true;//是否可以播放人物角色的动画
    public bool inputEnable = true;//鼠标是否可以输入
    private Vector3 mousePos;
    public GameObject roulette;
    private float x = 0;
    private float y = 0;
    public GameObject[] activeInsideList;//激活状态的内圈轮盘
    public GameObject[] inactiveInsideList;//未激活状态的内驱轮盘
    public GameObject actInactiveEmoji;//处于待激活状态的未激活的外圈表情[鼠标按下悬停但未释放]
    public GameObject actInside;//当前激活的外圈轮盘
    public GameObject inactInside;//取消激活的内圈轮盘
    public Image[] outside;//外圈轮盘
    private float width;
    private float height;
    public GameObject[] emoji;//外圈表情
    private GameObject actEmoji;//当前激活的外圈表情
    private Image actOutside;//当前激活的外圈
    public Color oriColor;
    public GameObject[] inactive;//未激活的外圈表情
    public AudioSource[] audios;
    public AudioClip[] clips;
    public int actIndex;//当前激活的物体索引,笛卡尔坐标象限
    public Animator anim;

    public GameObject[] emojisAnim;
    //public int last=-1;
    // Start is called before the first frame update
    void Start() {
        roulette.SetActive(false);
        width = Screen.width;
        height = Screen.height;
        oriColor = outside[0].color;
        audios = Camera.main.GetComponents<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (inputEnable) {
            //Debug.Log(outside[actIndex].color.a);
            if (Input.GetMouseButtonDown(0)) {
                audios[0].clip = clips[4];
                audios[0].Play();
            }

            mousePos = Input.mousePosition;
            x = mousePos.x;
            y = mousePos.y;
            //Debug.Log(x + " " + y);
            if (Input.GetMouseButton(0)) {
                anim.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
                //Debug.Log(mousePos.x+" "+mousePos.y);
                roulette.SetActive(true);
                if (
                    Vector2.Distance(new Vector2(x, y), new Vector2(width / 2, height / 2)) >= 
                    Vector2.Distance(new Vector2(width / 2 + width * 0.05f, height / 2 + width * 0.05f), new Vector2(width / 2, height / 2))) {
                    if (x - width / 2 < 0 && y - height / 2 > 0) {
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
                else {
                    if (actInside != null) {
                        actInside.SetActive(false);
                    }
                    if (actEmoji != null) {
                        actEmoji.SetActive(false);
                    }
                    if (inactInside != null) {
                        inactInside.SetActive(true);
                    }
                    if (actOutside != null) {
                        actOutside.color = oriColor;
                    }
                    if (actInactiveEmoji != null) {
                        actInactiveEmoji.transform.localScale = new Vector3(1, 1, 1);
                    }
                    if (actEmoji != null) {
                        actEmoji.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0)) {
                anim.cullingMode = AnimatorCullingMode.AlwaysAnimate;
                if (audios[0].isPlaying == false) {
                    audios[0].clip = clips[5];
                    audios[0].Play();
                }
                if (actInside != null) {
                    actInside.SetActive(false);
                }
                if (inactInside != null) {
                    inactInside.SetActive(true);
                }
                if (actOutside != null) {
                    actOutside.color = oriColor;
                }
                if (actEmoji != null) {
                    actEmoji.SetActive(false);
                }

                MouseUpEvent(actIndex);
                Invoke("Close", 0.2f);
                //TODO:设置动画表情
            }
        }
    }

    public bool isPlaying;
    void Close() {
        anim.SetBool("close", true);
        canPlay = true;
    }
    void MouseUpEvent(int index) {
        if (Vector2.Distance(new Vector2(x, y), new Vector2(width / 2, height / 2)) >= Vector2.Distance(
                new Vector2(width / 2 + width * 0.13f, height / 2 + width * 0.13f),
                new Vector2(width / 2, height / 2))) {
            emojisAnim[index].SetActive(true);
            if (canPlay) {
                switch (index) {
                    case 0:
                        roleAnim.SetTrigger("wave");
                        break;
                    case 1:
                        roleAnim.SetTrigger("provoke");
                        break;
                    case 2:
                        roleAnim.SetTrigger("laugh");
                        break;
                    case 3:
                        roleAnim.SetTrigger("kiss");
                        break;
                }
                canPlay = false;
            }
            if (audios[1].isPlaying == false) {
                audios[1].clip = clips[index];
                audios[1].Play();
            }
        }
    }
    void Change(int index) {
        actIndex = index;
        //if (last!=index) {
        //Debug.Log(last+" "+index); 
        //   audios[0].Play();
        //}
        if (actInside != null && inactInside != null) {
            actInside.SetActive(false);
            inactInside.SetActive(true);
            actEmoji.SetActive(false);
            actInside.transform.parent.transform.parent.transform.Find("Outside").GetComponent<Image>().color = oriColor;
        }

        if (actInactiveEmoji!=null) {
            actInactiveEmoji.transform.localScale = new Vector3(1,1,1);
        }
        if (actEmoji!=null) {
            actEmoji.transform.localScale = new Vector3(1,1,1);
        }
        
        actEmoji = emoji[index];
        actInside = activeInsideList[index];
        inactInside = inactiveInsideList[index];
        actOutside = outside[index];
        actInactiveEmoji = inactive[index];

        inactive[index].transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        actEmoji.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        outside[index].color = new Color(oriColor.r, oriColor.g, oriColor.b, oriColor.a + 0.2f);
        //last = index;
    }
}
