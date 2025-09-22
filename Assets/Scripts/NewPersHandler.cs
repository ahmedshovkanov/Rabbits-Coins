using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewPersHandler : MonoBehaviour
{
    public TMP_InputField NameInput;

    private void OnEnable()
    {
        NameInput.text = "Name";
    }

    private void OnDisable()
    {
        Person p = new Person();
        p.name = NameInput.text;
        SaveSystem.Instance.AddPerson(p);
        SaveSystem.Instance.SaveGame();
        SaveSystem.Instance.Initialize();
    }
}
