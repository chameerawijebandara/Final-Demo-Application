/*============================================================================== 
 * Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System.Collections;

/// <summary>
/// This class manages different views in the scene like AboutPage, SplashPage and ARCameraView.
/// All of its Init, Update and Draw calls take place via SceneManager's Monobehaviour calls to ensure proper sync across all updates
/// </summary>
public class AppManager : MonoBehaviour {
    
    #region PUBLIC_MEMBER_VARIABLES
    public ISampleAppUIEventHandler m_UIEventHandler;
    #endregion PUBLIC_MEMBER_VARIABLES
    
    #region PROTECTED_MEMBER_VARIABLES
    public static ViewType mActiveViewType;
    public enum ViewType {SPLASHVIEW, ARCAMERAVIEW};
    #endregion PROTECTED_MEMBER_VARIABLES
    
    #region PRIVATE_MEMBER_VARIABLES
    private SplashScreenView mSplashView;
    private float mSecondsVisible = 2.0f;
    #endregion PRIVATE_MEMBER_VARIABLES
    
    //This gets called from SceneManager's Start() 
    public virtual void InitManager () 
    {
        mSplashView = new SplashScreenView();
        InputController.SingleTapped        += OnSingleTapped;
        InputController.DoubleTapped        += OnDoubleTapped;
        InputController.BackButtonTapped    += OnBackButtonTapped;
        
        mSplashView.LoadView();
        StartCoroutine(LoadAboutPageForFirstTime());
        mActiveViewType = ViewType.SPLASHVIEW;
    }
    
    public virtual void UpdateManager()
    {
        //Does nothing but anyone extending AppManager can run their update calls here
    }
    
    public virtual void Draw()
    {
        m_UIEventHandler.UpdateView(false);
        switch(mActiveViewType)
        {
            case ViewType.SPLASHVIEW:
                mSplashView.UpdateUI(true);
                break;
            
            case ViewType.ARCAMERAVIEW:
                break;
        }
    }

    
    #region PRIVATE_METHODS
    
    private void OnSingleTapped()
    {
        
    }
	private void OnDoubleTapped()
	{

	}
    private void OnBackButtonTapped()
    {
        if(mActiveViewType == ViewType.ARCAMERAVIEW) //if it's in ARCameraView
        {
			Application.Quit();
        }
        
    }
    
    private void OnTappedOnCloseButton()
    {
        mActiveViewType = ViewType.ARCAMERAVIEW;
    }
    
    private void OnAboutStartButtonTapped()
    {
        mActiveViewType = ViewType.ARCAMERAVIEW;
    }
    
    private IEnumerator LoadAboutPageForFirstTime()
    {
        yield return new WaitForSeconds(mSecondsVisible);
		mActiveViewType = ViewType.ARCAMERAVIEW;
        yield return null;
    }
    #endregion PRIVATE_METHODS
    
}
