using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    string levelSelected;
    bool enteringLevel;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enteringLevel = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if(!enteringLevel)
        animator.SetBool("clicked", true);
    }

    private void OnMouseUp()
    {
        animator.SetBool("clicked", false);
    }

    private void OnMouseExit()
    {
         animator.SetBool("clicked", false);
    }
    public void PlayerLeaveMenuAnimation()
    {
        GameManager.instance.ChangeScene(levelSelected);
    }
    public string changeLevelSelected
    {
        get { return levelSelected; }
        set { levelSelected = value; }
    }
    public bool gs_enteringLevel
    {
        get { return enteringLevel; }
        set { enteringLevel = value; }
    }
}
