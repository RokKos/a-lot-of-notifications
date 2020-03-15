using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickerScreenController : BaseScreenController
{
    const int INITIAL_POST_NUM = 10;

    [Header("Values to randomly pick from")]
    public List<Sprite> images;
    public List<Sprite> avatars;
    public List<string> usernames;
    public List<string> texts;

    GameObject scrollViewContent;

    public static ClickerScreenController instance;

    int lastPost = 0;
    List<IGPost> posts;

    public override void OnScreenEnter()
    {
        base.OnScreenEnter();

        // Find references
        scrollViewContent = transform.Find("Scroll View/Viewport/Content").gameObject;
        IGPost.postPrefab = scrollViewContent.transform.Find("tpl_ig_post").gameObject;

        // Hide post template
        IGPost.postPrefab.SetActive(false);

        /*
         * Seed initial posts
         */
        posts = new List<IGPost>();
        
        for (lastPost = 0; lastPost < INITIAL_POST_NUM; lastPost++)
        {
            IGPost post = makeRandomPost(lastPost);
            posts.Add(post);
            attachPost(post);
        }

    }

    public override void OnScreenUpdate()
    {
        base.OnScreenUpdate();
        // TODO: Actual game code!
    }

    void attachPost(IGPost post)
    {
        post.gameObject.transform.SetParent(scrollViewContent.transform, false);
        scrollViewContent.GetComponent<RectTransform>().sizeDelta = scrollViewContent.GetComponent<RectTransform>().sizeDelta + new Vector2(0, IGPost.HEIGHT);
    }

    public override void OnScreenExit()
    {
        base.OnScreenExit();

        // Clean up all posts
        foreach (IGPost post in posts)
        {
            post.destroy();
        }
        posts.Clear();
    }

    public IGPost makeRandomPost(int number)
    {
        // TODO: some less...stupid logic
        return IGPost.make(number, pickRandom(usernames), Random.Range(0, 99999999), pickRandom(texts), pickRandom(images), pickRandom(avatars));
    }

    // TODO: maybe move utils somewhere else?
    static T pickRandom<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
