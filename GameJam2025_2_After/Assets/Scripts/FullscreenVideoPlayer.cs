using UnityEngine;
using UnityEngine.Video;

public class FullscreenVideoPlayer : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer; // Unity's VideoPlayer

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        videoPlayer.loopPointReached += OnVideoFinished; // Event when video ends
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video finished, quitting game.");
        Application.Quit(); // Quit the game

        // If running in the editor, stop playing
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
