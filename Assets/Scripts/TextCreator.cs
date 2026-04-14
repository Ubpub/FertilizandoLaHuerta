using System.Collections;
using UnityEngine;

public class TextCreator : MonoBehaviour
{
    [Header("Values")]
    public static TMPro.TMP_Text viewText;
    public static bool runTextPrint;
    public static int charCount;

    [Header("Count")]
    [SerializeField] string transferText;
    [SerializeField] int internalCount;

    [Header("Audio")]
    [SerializeField] AudioSource zanahorio;

    void Update()
    {
        internalCount = charCount;
        charCount = GetComponent<TMPro.TMP_Text>().text.Length;
        if (runTextPrint == true)
        {
            runTextPrint = false;
            viewText =  GetComponent<TMPro.TMP_Text>();
            transferText = viewText.text;
            viewText.text = "";
            StartCoroutine(RollText());
        }
    }

    IEnumerator RollText()
    {
        foreach (char c in transferText)
        {
            viewText.text += c;
            zanahorio.Play();
            yield return new WaitForSeconds(0.03f);
        }
    }
}

