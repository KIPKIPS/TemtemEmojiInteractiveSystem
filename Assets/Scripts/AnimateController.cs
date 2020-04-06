using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateController : MonoBehaviour {
    public Animator anim;
    public GameObject roulette;
    public PlayerInput PI;
    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void CloseStart() {
        PI.inputEnable = false;
        //anim.cullingMode = AnimatorCullingMode.CullCompletely;
    }
    public void CloseEnd() {
        roulette.SetActive(false);
        anim.SetBool("close", false);
        anim.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        Invoke("Act",0.6f);
    }

    void Act() {
        PI.inputEnable = true;
    }
}
