using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Private Members

    private bool mMenuAnimating;
    private bool mAreMenusShowing;
    private float mMenuAnimationTransition;
    private float mAnimationDuration = 0.2f;

    #endregion

    #region Public Members

    public RectTransform ColorMenu;
    public RectTransform ActionMenu;

    #endregion

    /// <summary>
    /// Default update function
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            OnTheOneButtonClick();

        if(mMenuAnimating)
        {
            if(mAreMenusShowing)
            {
                mMenuAnimationTransition += Time.deltaTime / mAnimationDuration;
                if(mMenuAnimationTransition >= 1)
                {
                    mMenuAnimationTransition = 1;
                    mMenuAnimating = false;
                }
            }
            else
            {
                mMenuAnimationTransition -= Time.deltaTime / mAnimationDuration;
                if(mMenuAnimationTransition <= 0)
                {
                    mMenuAnimationTransition = 0;
                    mMenuAnimating = false;
                }
            }

            ColorMenu.anchoredPosition = Vector2.Lerp(new Vector2(0, 500), new Vector2(0, -125), mMenuAnimationTransition);
            ActionMenu.anchoredPosition = Vector2.Lerp(new Vector2(-375, 0), new Vector2(125, 0), mMenuAnimationTransition);
        }
    }

    /// <summary>
    /// Plays the animation for the menu
    /// </summary>
    private void PlayMenuAnimation()
    {
        mMenuAnimating = true;
    }

    /// <summary>
    /// Opens the menu on button click
    /// </summary>
    public void OnTheOneButtonClick()
    {
        mAreMenusShowing = !mAreMenusShowing;
        PlayMenuAnimation();
        Debug.Log(mAreMenusShowing);
    }


}
