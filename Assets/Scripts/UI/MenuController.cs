using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Cần thiết nếu bạn muốn thao tác trực tiếp với UI trong code

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

    // Tên key dùng để lưu trữ cài đặt âm lượng vào memory (PlayerPrefs)
    private const string VOLUME_PREF_KEY = "GameVolume";

    private void Start()
    {
        // 1. Ẩn bảng Setting khi mới vào Menu (tránh trường hợp quên tắt trong Editor)
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        // 2. Thiết lập Âm lượng từ dữ liệu đã lưu
        float savedVolume = PlayerPrefs.GetFloat(VOLUME_PREF_KEY, 1f); // Mặc định là 1 (100%) nếu chưa lưu
        AudioListener.volume = savedVolume; // Áp dụng âm lượng cho toàn bộ game

        // 3. Cập nhật thanh Slider trên UI cho khớp với âm lượng đã lưu
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }
    }

    #region Scene Navigation
    
    // Hàm này gọi khi bấm nút Play
    public void PlayGame()
    {
        Debug.Log($"Loading Scene: {playSceneName}");
        SceneManager.LoadScene(playSceneName);
    }

    // (Tuỳ chọn) Hàm này cho nút Menu (biểu tượng danh sách)
    public void OpenLevelSelect()
    {
        Debug.Log("Chưa có chức năng chọn màn - Bạn có thể thêm code mở Panel chọn màn ở đây");
        // Ví dụ: levelSelectPanel.SetActive(true);
    }

    #endregion

    #region Settings Panel
    
    // Gọi hàm này khi bấm nút mở Setting (hình răng cưa)
    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    // Gọi hàm này khi bấm nút đóng Setting (hình chữ X)
    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    #endregion

    #region Audio Control

    // CHÚ Ý CHỖ NÀY DÀNH CHO THANH SLIDER (Tăng/Giảm âm lượng)
    // Inspector: Gắn script này vào OnValueChanged của Slider, nhớ chọn 'Dynamic float'
    public void SetGlobalVolume(float volume)
    {
        // Điều chỉnh âm lượng toàn cục của game
        AudioListener.volume = volume;
        
        // Lưu lại mức âm lượng mới
        PlayerPrefs.SetFloat(VOLUME_PREF_KEY, volume);
        PlayerPrefs.Save();
    }

    // Gọi hàm này nếu bạn có nút Tắt/Bật âm lượng riêng (Mute button)
    public void ToggleMute()
    {
        if (AudioListener.volume > 0f)
        {
            // Đang có tiếng -> Tắt tiếng (Lưu thành 0)
            SetGlobalVolume(0f);
            if(volumeSlider != null) volumeSlider.value = 0f;
        }
        else
        {
            // Bật lại tiếng (100%)
            SetGlobalVolume(1f);
            if(volumeSlider != null) volumeSlider.value = 1f;
        }
    }

    #endregion

    #region Social Media
    
    // Gọi khi bấm nút Discord
    public void OpenDiscord()
    {
        Debug.Log("Mở link Discord");
        Application.OpenURL(discordLink);
    }

    // Gọi khi bấm nút Facebook
    public void OpenFacebook()
    {
        Debug.Log("Mở link Facebook");
        Application.OpenURL(facebookLink);
    }

    #endregion
}
