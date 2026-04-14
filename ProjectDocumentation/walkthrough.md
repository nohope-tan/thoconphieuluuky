# Hướng dẫn Cài đặt Menu UI trong Unity

Tôi đã hoàn thành việc viết script cho Menu của bạn. Bất cứ khi nào bạn mở Menu, nó sẽ tự động ẩn bảng Setting. Các thông số âm lượng sau khi chỉnh cũng sẽ tự động lưu lại! 

Dưới đây là các bước chi tiết để cấu hình hệ thống giao diện trong Editor:

## 1. Gắn Script `MenuController`
Đầu tiên, bạn cần gắn script vào một đối tượng chung quản lý Menu (ví dụ Main Camera, hoặc bạn có thể tạo một GameObject rỗng tên là `MenuManager`).

1. Trong Hierarchy của scene `menu`, chọn **Canvas** hoặc tạo 1 **Empty GameObject** (đặt tên `MenuManager`).
2. Kéo thả file script `Assets/Scripts/UI/MenuController.cs` vào đối tượng bạn vừa chọn.

## 2. Kết nối các biến trong Inspector
Sau khi đã gắn script, hãy nhìn sang bảng **Inspector** của đối tượng đó. Kéo thả các thành phần tương ứng từ Hierarchy vào:

- **Play Scene Name**: Gõ tên Scene bạn muốn chuyển đến (mặc định đã là `map`).
- **Settings Panel**: Kéo GameObject chứa giao diện Setting (bảng có dấu X đóng) vào đây.
- **Discord Link / Facebook Link**: Tôi đã điền sẵn link bạn cung cấp.
- **Volume Slider**: Tìm thanh kéo thả âm lượng của bạn (thường là một GameObject dùng UI Slider) và kéo vào đây.

## 3. Cấu hình Nút Bấm (Button)
Đối với mỗi nút, bạn di chuyển đến phần thành phần **Button** trong Inspector, bấm nút **+** ở `On Click()`:

1. **Nút Play (Hình Tam Giác):** Kéo `MenuManager` vào > Chọn `MenuController.PlayGame`.
2. **Nút Settings (Răng cưa - Nằm ngoài màn hình chính):** Kéo `MenuManager` vào > Chọn `MenuController.OpenSettings`.
3. **Nút Đóng Settings (Chữ X - Nằm trong bảng Settings):** Kéo `MenuManager` vào > Chọn `MenuController.CloseSettings`.
4. **Nút Discord:** Kéo `MenuManager` vào > Chọn `MenuController.OpenDiscord`.
5. **Nút Facebook:** Kéo `MenuManager` vào > Chọn `MenuController.OpenFacebook`.
6. **Nút Menu (Danh sách):** Kéo `MenuManager` vào > Chọn `MenuController.OpenLevelSelect` (hiện tại tính năng này chỉ là in câu chú thích trong Console, bạn có thể triển khai thêm sau).

## 4. Cấu hình Thanh kéo Âm lượng (Slider)
1. Chọn GameObject của thanh Slider.
2. Tìm vùng **On Value Changed (Single)** trong Inspector của Slider.
3. Bấm nút **+** > Kéo đối tượng `MenuManager` chứa `MenuController` vào ô đó.
4. Ở phần Function, hãy tìm danh sách thả xuống. **Quan Trọng**: Chọn mục **Dynamic float** -> Chọn `MenuController.SetGlobalVolume`. *(KHÔNG chọn ở phần Static Parameters).*

> [!WARNING]
> Để lỗi không xảy ra khi chuyển Map, xin đảm bảo rằng Scene `map` đã được thêm vào **File > Build Settings** nhé.

## 5. Nút bật/tắt tiếng (Tùy chọn)
Nếu bạn có hẳn các nút như biểu tượng `Nốt nhạc` để tắt/mở âm lượng hoàn toàn, bạn có thể thiết lập `On Click()` cho nút đó gọi hàm `MenuController.ToggleMute()`.
## 6. Âm thanh Nhảy cho Player
Tôi đã cập nhật Script `PlayerController` để hỗ trợ âm thanh khi nhảy:

1. Chọn đối tượng **Player** trong Hierarchy.
2. Bạn sẽ thấy thành phần **Audio Source** tự động được thêm vào (do yêu cầu của Script).
3. Trong Inspector của `PlayerController`, kéo file âm thanh nhảy (ví dụ: `mixkit-video-game-spin-jump-2648.wav` trong folder `Audio/SFX`) vào ô **Jump Sound**.
4. Âm lượng của tiếng nhảy này cũng sẽ tuân theo thanh Slider mà bạn đã thiết lập ở Menu!
