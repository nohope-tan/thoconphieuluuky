using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class MenuController : MonoBehaviour
{
    [Header("Menu Settings")]
    [Tooltip("Tên của Scene bạn muốn load khi bấm Play (ví dụ: 'map')")]
    public string playSceneName = "map";

    [Header("UI Panels")]
    [Tooltip("Thả GameObject chứa bảng Setting vào đây")]
    public GameObject settingsPanel;

    [Header("Social Links")]
    public string discordLink = "https://discord.gg/kBX8bmMFhq";
    public string facebookLink = "https://www.facebook.com/tan.minh.5361/";

    [Header("Audio Settings")]
    [Tooltip("Thả UI Slider điều chỉnh âm lượng vào đây để nó tự cập nhật giá trị đã lưu khi mới vào game")]
    public Slider volumeSlider;

    
    private const string VOLUME_PREF_KEY = "GameVolume";

    private void Start()
    {
        
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        
        float savedVolume = PlayerPrefs.GetFloat(VOLUME_PREF_KEY, 1f); 
        AudioListener.volume = savedVolume; 

        
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }
    }

    #region Scene Navigation
    
    
    public void PlayGame()
    {
        Debug.Log($"Loading Scene: {playSceneName}");
        SceneManager.LoadScene(playSceneName);
    }

    
    public void OpenLevelSelect()
    {
        Debug.Log("Chưa có chức năng chọn màn - Bạn có thể thêm code mở Panel chọn màn ở đây");
        
    }

    #endregion

    #region Settings Panel
    
    
    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    
    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    #endregion

    #region Audio Control

    
    
    public void SetGlobalVolume(float volume)
    {
        
        AudioListener.volume = volume;
        
        
        PlayerPrefs.SetFloat(VOLUME_PREF_KEY, volume);
        PlayerPrefs.Save();
    }

    
    public void ToggleMute()
    {
        if (AudioListener.volume > 0f)
        {
            
            SetGlobalVolume(0f);
            if(volumeSlider != null) volumeSlider.value = 0f;
        }
        else
        {
            
            SetGlobalVolume(1f);
            if(volumeSlider != null) volumeSlider.value = 1f;
        }
    }

    #endregion

    #region Social Media
    
    
    public void OpenDiscord()
    {
        Debug.Log("Mở link Discord");
        Application.OpenURL(discordLink);
    }

    
    public void OpenFacebook()
    {
        Debug.Log("Mở link Facebook");
        Application.OpenURL(facebookLink);
    }

    #endregion
}

