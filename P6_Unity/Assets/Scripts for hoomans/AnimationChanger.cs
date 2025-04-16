using UnityEngine;

public class AnimationChanger : MonoBehaviour
{
    public GameObject guy, gal;
    Animator aniGuy, aniGal;

    Animator aniChosen;

    public bool animatorIsOnGuy;

    void Start()
    {
        aniGuy = guy.GetComponent<Animator>();
        aniGal = gal.GetComponent<Animator>();
    }

    void Update()
    {
        if(animatorIsOnGuy == true)
        {
            aniChosen = aniGuy;
        }else if(animatorIsOnGuy == false)
        {
            aniChosen = aniGal;
        }

        if (Input.GetKeyDown(KeyCode.U)) // FOR TESTING
        {
            ChangeAnimation();
        }
    }

    public void SetAnimatorGuy()
    {
        animatorIsOnGuy = true;
    }

    public void SetAnimatorGal()
    {
        animatorIsOnGuy = false;
    }

    public void ChangeAnimation() // Den er meget grov, da vi kun har 2 animationer at skulle skifte mellem
    {
        if(aniChosen.GetFloat("Speed") == 0)
        {
            aniChosen.SetFloat("Speed", 1);
        }else if(aniChosen.GetFloat("Speed") == 1)
        {
            aniChosen.SetFloat("Speed", 0);
        }
    }
}
