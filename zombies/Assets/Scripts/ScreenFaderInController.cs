using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFaderInController : MonoBehaviour {
    public GameObject faderInPanelObject;
    public Material victoryFaderMaterial;
    public Material lostFaderMaterial;

    // 0 = none, 1 = victory, 2 = lost
    private static int fadingIn;

    private const float fadeSpeed = 0.5f;

    public static void UnchangablySetFadeInOfVictory() {
        if (fadingIn == 0) {
            fadingIn = 1;
        }
    }

    public static void UnchangablySetFadeInOfLost() {
        if (fadingIn == 0) {
            fadingIn = 2;
        }
    }

    void Start() {
        fadingIn = 0;
    }

    void Update() {
        Color color;
        if (fadingIn == 0) {
            color = faderInPanelObject.GetComponent<Image>().color;
            color.a = 0.0f;
        } else {
            if (fadingIn == 1) {
                color = victoryFaderMaterial.color;
            } else {
                color = lostFaderMaterial.color;
            }
            float currentAlpha = faderInPanelObject.GetComponent<Image>().color.a;
            color.a = currentAlpha + Time.deltaTime * fadeSpeed;
        }
        faderInPanelObject.GetComponent<Image>().color = color;

        if (color.a >= 0.90f) {
            if (fadingIn == 1) {
                SceneManager.LoadScene("Victory");
            } else if (fadingIn == 2) {
                SceneManager.LoadScene("Lost");
            }
        }
    }
}
