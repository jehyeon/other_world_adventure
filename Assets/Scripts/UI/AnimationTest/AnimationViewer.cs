using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

[RequireComponent(typeof(Tab))]
public class AnimationViewer : MonoBehaviour
{
    [Header("Require")]
    [Header("Dropdown UI")]
    [SerializeField]
    private TMP_Dropdown dropdown;
    [SerializeField]
    private TMP_Dropdown detailDropDown;
    [Header("Option Name Text")]
    [SerializeField]
    private TextMeshProUGUI optionNameText;
    [Header("Prefabs")]
    [SerializeField]
    private AnimationView viewPref;
    [SerializeField]
    private AnimationElement entityPref;

    [Header("Path")]
    [SerializeField]
    private string animationsPath_1 = "Assets/Resources/Animations/";
    [SerializeField]
    private string animationsPath_2 = "Animations/";

    [Header("Element Interval")]
    [SerializeField]
    private float distBetweenElement = 1.0f;
    [SerializeField]
    private int countPerRow = 3;

    private string option;
    private string detailOption;
    private string selectedOption;      // full option name
    public string SelectedOption { get { return SelectedOption; } }

    // 활성화된 모든 viewer
    private Dictionary<string, int> viewIndexer = new Dictionary<string, int>();
    // 로딩된 viewer
    private Tab views;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        views = GetComponent<Tab>();
        views.Clear();

        dropdown.ClearOptions();
        detailDropDown.ClearOptions();

        DirectoryInfo di = new DirectoryInfo(animationsPath_1);
        foreach (DirectoryInfo option in di.GetDirectories())
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(option.Name));
        }

        dropdown.value = 0;     // default
        SelectOption();    
    }

    public void SelectOption()
    {
        detailDropDown.ClearOptions();

        option = dropdown.options[dropdown.value].text;
        DirectoryInfo optionsDi = new DirectoryInfo(animationsPath_1 + "/" + option);
        foreach (DirectoryInfo detailDi in optionsDi.GetDirectories())
        {
            detailDropDown.options.Add(new TMP_Dropdown.OptionData(detailDi.Name));
        }

        detailDropDown.value = 0;   // default
        SelectDetailOptions();
    }

    /// <summary>
    /// option + detail option의 animation 모두 보여줌
    /// ex. human/archer/1 ~ 6
    /// </summary>
    public void SelectDetailOptions()
    {
        // view
        detailOption = detailDropDown.options[detailDropDown.value].text;
        selectedOption = option + "/" + detailOption;

        int viewIndex;
        if (!viewIndexer.TryGetValue(selectedOption, out viewIndex))
        {
            AnimationView view = Instantiate(viewPref);
            view.SetInterval(countPerRow, distBetweenElement);

            DirectoryInfo animationDi = new DirectoryInfo(animationsPath_1 + "/" + selectedOption);
            foreach (DirectoryInfo typeDi in animationDi.GetDirectories())
            {
                string type = typeDi.Name;
                string animName;

                List<GameObject> elements = new List<GameObject>();
                foreach (FileInfo anim in typeDi.GetFiles("*.anim"))
                {
                    animName = anim.Name.Split(".")[0];
                    string path = string.Format("{0}{1}/{2}/{3}", 
                        animationsPath_2, selectedOption, type, animName);

                    AnimationClip clip = Resources.Load<AnimationClip>(path);

                    if (clip != null)
                    {
                        AnimationElement element = Instantiate(entityPref);
                        element.SetClip(clip);
                        element.SetName(animName);

                        elements.Add(element.gameObject);
                    }
                    else
                    {
                        Debug.LogError("Can't find animation clip");
                    }
                }

                // type 추가
                view.AddView(elements);
            }

            viewIndex = views.AddTab(view.gameObject);
            viewIndexer.Add(selectedOption, viewIndex);
        }

        views.OpenTab(viewIndex);
    }

    private void UpdateSelectedOption(string optionName)
    {
        optionNameText.text = optionName;
        selectedOption = optionName;
    }

    public void PreviousType()
    {
        // not good
        int viewIndex = viewIndexer[selectedOption];
        views.GetTab(viewIndex).GetComponent<AnimationView>().Tab.Previous();
    }

    public void NextType()
    {
        // not good
        int viewIndex = viewIndexer[selectedOption];
        views.GetTab(viewIndex).GetComponent<AnimationView>().Tab.Next();
    }
}
