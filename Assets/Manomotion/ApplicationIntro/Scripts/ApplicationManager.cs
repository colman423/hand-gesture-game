using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManoMotion.RunTime;
using System;
using ManoMotion.UI.Buttons;
public class ApplicationManager : MonoBehaviour
{
    private static ApplicationManager _instance;
    public static ApplicationManager Instance
    {
        get
        {
            return _instance;
        }
    }

    // public HowToInstructor howToInstructor;
    public RunTimeApplication runTimeApplication;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("More than 1 ApplicationManagers in the scene");
            Destroy(this.gameObject);
        }

        InitializeComponents();
    }

    /// <summary>
    /// Initializes the components needed in order to operate the application.
    /// </summary>
    void InitializeComponents()
    {
        try
        {
            runTimeApplication = this.GetComponent<RunTimeApplication>();
        }
        catch (Exception ex)
        {
            runTimeApplication = new RunTimeApplication();
        }

        runTimeApplication.InitializeRuntimeComponents();
    }

    private void Start()
    {
        Debug.Log("Start StartMainApplicationWithDefaultSettings");
        runTimeApplication.StartMainApplicationWithDefaultSettings();
    }


    /// <summary>
    /// Forces the instructions to be seen even if seen in the past. Used from within the main menu.
    /// </summary>
    public void ForceInstructions()
    {
        runTimeApplication.SaveDefalutFeaturesToDisplay();
        runTimeApplication.SetMenuIconVisibility();
        // howToInstructor.InitializeHowtoInstructor();
        runTimeApplication.HideApplicationComponents();
        runTimeApplication.ShouldShowBackground(true);
    }

}
