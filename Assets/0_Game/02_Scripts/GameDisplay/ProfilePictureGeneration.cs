using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfilePictureGeneration : MonoBehaviour
{
    // Borders feedbacking share state
    public SpriteRenderer coloredOutline;
    [Tooltip("0 = Fail | 1 = Neutral | 2 = Share | 3 = Fan!")]
    public List<Color> outlineColors;


    // Profile picture generation
    public SpriteRenderer buste;
    public Sprite[] bustes;

    public SpriteRenderer face;
    public Sprite[] faces;
    public List<Color> skinTones;

    public SpriteRenderer mouth;
    public Sprite[] mouths;

    public SpriteRenderer nose;
    public Sprite[] noses;

    public SpriteRenderer eye;
    public Sprite[] eyes;

    public SpriteRenderer hair;
    public Sprite[] hairs;

    public SpriteRenderer ear;
    public Sprite[] ears;

    public SpriteRenderer background;
    public List<Color> backgroundColors;

    public SpriteRenderer tShirt;
    public List<Color> tShirtColors;


    public void PopulateProfilePicture(
        int busteIndex,
        int faceIndex,
        int mouthIndex,
        int noseIndex,
        int eyeIndex,
        int hairIndex,
        int earIndex,
        int skinToneIndex,
        int bgColorIndex,
        int tShirtColorIndex)
    {
        buste.sprite = bustes[busteIndex];
        buste.color = skinTones[skinToneIndex];
        face.sprite = faces[faceIndex];
        face.color = skinTones[skinToneIndex];
        mouth.sprite = mouths[mouthIndex];
        nose.sprite = noses[noseIndex];
        eye.sprite = eyes[eyeIndex];
        hair.sprite = hairs[hairIndex];
        ear.sprite = ears[earIndex];
        ear.color = skinTones[skinToneIndex];
        background.color = backgroundColors[bgColorIndex];
        tShirt.color = tShirtColors[tShirtColorIndex];
    }

    public void MakeBot()
    {
        buste.gameObject.SetActive(false);
        face.sprite = faces[10];
        face.color = Color.white;
        mouth.gameObject.SetActive(false);
        nose.gameObject.SetActive(false);
        eye.gameObject.SetActive(false);
        hair.gameObject.SetActive(false);
        ear.gameObject.SetActive(false);
        background.color = backgroundColors[Random.Range(0, backgroundColors.Count)];
    }

    public void PopulateProfileBorder(int shareState)
    {
        coloredOutline.color = outlineColors[shareState];
    }
}
