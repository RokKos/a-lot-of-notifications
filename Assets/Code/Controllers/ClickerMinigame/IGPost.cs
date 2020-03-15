using System;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class IGPost : MonoBehaviour
{
    public const int HEIGHT = 700;

    public string Username;
    public int Likes;
    public string Text;
    public Sprite Image;
    public Sprite Avatar;

    [Tooltip("Position/index in the list")]
    public int Number;

    public static GameObject postPrefab;
    
    public static IGPost make(int number, string username, int likes, string text, Sprite image, Sprite avatar)
    {
        GameObject go = Instantiate(postPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        IGPost post = go.GetComponent<IGPost>();

        post.Number = number;
        post.Username = username;
        post.Likes = likes;
        post.Text = text;
        post.Image = image;
        post.Avatar = avatar;

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
        transform.Find("Like number").GetComponent<Text>().text = String.Format("{0:n0}", Likes);
        transform.Find("Username").GetComponent<Text>().text = Username;
        transform.Find("Text").GetComponent<Text>().text =  "<b>" + Username + "</b> " + Text;
    }

    public override string ToString()
    {
        return "[IGPost " + Number + " by " + Username + "]";
    }

    public void likeClicked()
    {
        Debug.Log("Like clicked on post " + this);
        // TODO: actual gameplay??
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