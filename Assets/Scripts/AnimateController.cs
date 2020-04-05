using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateController : MonoBehaviour {
    public Animator anim;
    public GameObject roulette;
    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void CloseStart() {

    }
    public void CloseEnd() {
        roulette.SetActive(false);
        anim.SetBool("close", false);
    }
}
