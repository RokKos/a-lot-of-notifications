using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[ExecuteInEditMode]
public class IGPost : MonoBehaviour
{
    public const int HEIGHT = 700;

    public string Username;
    public int Likes;
    public string Text;
    public Sprite Image;
    public Sprite Avatar;
    public int Score;

    [Tooltip("Position/index in the list")]
    public int Number;

    public static GameObject postPrefab;
    public static ClickerScreenController controller;

    Button likeBtn;
    
    public static IGPost make(int number, string username, int likes, string text, Sprite image, Sprite avatar, int score)
    {
        GameObject go = Instantiate(postPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        IGPost post = go.GetComponent<IGPost>();
        post.likeBtn = go.transform.Find("btn_like").GetComponent<Button>();

        post.Number = number;
        post.Username = username;
        post.Likes = likes;
        post.Text = text;
        post.Image = image;
        post.Avatar = avatar;
        post.Score = score;

        // We're cloning from a hidden template, so make yourself visible
        go.SetActive(true);

        // Update values
        post.update();

        return post;
    }

    public void update()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -this.Number * HEIGHT, 0);

        transform.Find("Image").GetComponent<Image>().sprite = Image;
        transform.Find("Avatar image").GetComponent<Image>().sprite = Avatar;
        transform.Find("Like number").GetComponent<Text>().text = String.Format("{0:n0}", Likes) + " likes";
        transform.Find("Username").GetComponent<Text>().text = Username;
        transform.Find("Text").GetComponent<Text>().text =  "<b>" + Username + "</b> " + Text;
    }

    public override string ToString()
    {
        return "[IGPost " + Number + " with score " + Score + " by " + Username + "]";
    }

    public void likeClicked()
    {
        Debug.Log("Liked " + this);

        likeBtn.interactable = false;
        likeBtn.transform.DOPunchScale(new Vector3(.3f, .3f, .3f), 0.4f, 3, 1f);

        controller.postClicked(this);
    }

    internal void destroy()
    {
        Destroy(this.gameObject);
    }

    // Makes it work in the editor. No touch!
    void OnValidate()
    {
        update();
    }
}