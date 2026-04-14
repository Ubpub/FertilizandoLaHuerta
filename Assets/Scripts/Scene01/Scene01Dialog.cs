using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene01Dialog : MonoBehaviour
{
    [Header("Characters")]

    public GameObject charZanahorio;

    [Header("Text")]
    public GameObject textBox;
    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;

    void Update()
    {
        textLength = TextCreator.charCount;
    }

    void Start()
    {
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        charZanahorio.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        textToSpeak = "";
        mainTextObject.SetActive(true);
        textToSpeak = "¿Qué ha pasado? ¿Por qué están todos tan alborotados?";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        textBox.SetActive(true);
    }
}
