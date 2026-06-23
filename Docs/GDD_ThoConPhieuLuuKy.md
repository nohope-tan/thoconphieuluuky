# TÀI LIỆU THIẾT KẾ GAME (GAME DESIGN DOCUMENT)

## 1. Thông tin tổng quan
- **Tên game:** Thỏ Con Phiêu Lưu Ký (Little Rabbit's Adventure)
- **Thể loại:** 2D Platformer / Adventure (phiêu lưu, giải đố nhẹ)
- **Nền tảng:** PC & Mobile (Android, iOS)
- **Đối tượng người chơi:** Mọi lứa tuổi, đặc biệt trẻ em và người yêu thích game dễ thương, dễ tiếp cận.
- **Góc nhìn:** 2D side‑scroll, top‑down camera cố định.
- **Mô hình kinh doanh:** Free‑to‑play với IAP (mua Carrot, skin, buff) và quảng cáo (rewarded ads).
- **Tình trạng dự án:** Đang trong giai đoạn phát triển core gameplay, các tính năng chính đã được triển khai.
- **Phiên bản tài liệu:** 1.0 – Cập nhật 2026‑05‑28
- **Người phụ trách:**(Nguyễn Minh Tân).

## 2. High Concept / Tóm tắt ý tưởng
- **Game là gì?** Người chơi điều khiển một chú thỏ nhỏ trong hành trình thu thập Cà Rốt Vàng và đồng tiền, vượt qua các cạm bẫy, chướng ngại vật và boss.
- **Điểm khác biệt chính:** Hệ thống “Carrot” làm tiền tệ duy nhất, cho phép mua skin, buff và hồi sinh qua quảng cáo; mini‑map hướng dẫn ở mỗi cấp đầu.
- **Lý do game này hấp dẫn:** Gameplay nhanh, dễ học, nhưng khó thành thạo nhờ cơ chế nhảy đôi, coyote time và giới hạn buff.
- **Mục tiêu trải nghiệm:** Cảm giác phiêu lưu vui nhộn, khám phá nhiều cấp, thu thập và tùy chỉnh nhân vật.

## 3. Trụ cột thiết kế (Design Pillars)
- **Nhanh, dễ hiểu, khó thành thạo** – Điều khiển đơn giản, nhưng combo và buff tạo chiều sâu.
- **Khám phá có thưởng** – Mini‑map, checkpoint và phần thưởng Carrot/đồng tiền.
- **Tiến trình rõ ràng** – 10 cấp, mỗi cấp 5 ải, hệ thống cấp độ và bảng xếp hạng.
- **Tùy chỉnh nhân vật** – Skin và buff mua bằng Carrot.

## 4. Phân tích đối tượng & thị trường
- **Chân dung người chơi mục tiêu:** Trẻ em 6‑12 tuổi và người chơi casual muốn trải nghiệm ngắn gọn, vui vẻ.
- **Game tham chiếu / đối thủ:** *Super Mario Run*, *Rayman Adventures*.
- **Điểm mạnh:** Đồ họa chibi, hệ thống Carrot độc đáo, mini‑map hướng dẫn.
- **USP:** Carrot làm tiền tệ duy nhất, tích hợp quảng cáo reward để hồi sinh và nhận tiền.

## 5. Core Gameplay Loop
- **Vòng lặp chính:** Khởi động → Di chuyển & nhảy → Thu thập đồng tiền & Carrot → Đánh bẫy / kẻ địch → Đạt checkpoint → Lưu tiến trình → Tiếp cấp.
- **Vòng lặp phụ:** Xem quảng cáo → Nhận Carrot → Mua skin/buff → Thay đổi visual.
- **Vòng lặp meta:** Hoàn thành 10 cấp → Nhận skin hiếm → Chơi lại để cải thiện điểm.

## 6. Game Mechanics
- **Điều khiển:** phím mũi tên / nút ảo trên mobile.
- **Di chuyển:** speed = 7.0, JumpForce = 14.0, Double Jump, Coyote Time (0.2s), Jump Buffer (0.2s).
- **Kỹ năng / buff:** +1 máu, +1 lần nhảy (tối đa 2), Xóa mod đối địch (10s).
- **Chết & hồi sinh:** Rơi dưới -10 → Hồi sinh tại checkpoint; xem quảng cáo để hồi sinh ngay.
- **Luật chơi cơ bản:** Thu thập 1 Carrot và một số đồng tiền mỗi ải để hoàn thành.
- **Hệ thống chỉ số:** Máu (max 1, có thể tăng), Số lần nhảy (max 1, có thể tăng), Carrot, Đồng tiền.

## 7. Hệ thống tiến trình
- **Level / EXP:** 10 cấp, mỗi cấp 5 ải.
- **Skill tree:** Không có, nhưng buff mua được.
- **Trang bị:** Skin (cosmetic) mua bằng Carrot.
- **Mở khóa nội dung:** Hoàn thành ải mở khóa Carrot, skin mới, buff mới.

## 8. Combat / Battle Design
- **Cấu trúc chiến đấu:** 1‑với‑nhiều (cạm bẫy, kẻ địch đơn giản, boss).
- **Đòn đánh:** Bẫy chông (Spike Trap) dựa trên Raycast, kẻ địch di chuyển cơ bản.
- **AI kẻ địch:** Di chuyển theo waypoint, tấn công khi tiếp cận.
- **Boss:** Đại Ca – thỏ xám, có pha chase, bẫy chông liên tục.
- **Feedback:** Hiệu ứng particle, âm thanh “ting!”, “yum!”, animation bounce.

## 9. Nhân vật
- **Nhân vật chính:** Thỏ Con – Chibi, 1 máu cơ bản, khả năng nhảy đôi.
- **Nhân vật phụ:** Thỏ Bố, Thỏ Mẹ (NPC, không chơi).
- **Kẻ địch:** SpikeTrap (bẫy), Enemy (các sinh vật forest), Đại Ca (boss).
- **Chi tiết:** Mỗi nhân vật có sprite, animation, health, behavior script.

## 10. Level / Map Design
- **Cấu trúc màn chơi:** 10 cấp, mỗi cấp 5 ải, mỗi ải có một Carrot mục tiêu.
- **Mini‑map hướng dẫn:** Hiển thị vị trí Carrot, checkpoint, bẫy trên màn đầu mỗi cấp.
- **Checkpoint:** Flag/Fin, cập nhật spawn point.
- **Mật độ kẻ địch & tài nguyên:** Tăng dần từ cấp 1 → 10.

## 11. UI / UX
- **Danh sách màn hình:** Main Menu, Level Select, Gameplay, Pause, Shop, Settings, Game Over.
- **Luồng menu:** Start → Level Select → Gameplay → (Success/Fail) → Result → Shop/Replay.
- **HUD:** Carrot, Đồng tiền, Máu, Nút “Xem quảng cáo”, Nút “Mua buff”.
- **Nguyên tắc hiển thị:** Thông tin quan trọng luôn ở góc trên trái; quảng cáo ở giữa và góc dưới phải.

## 12. Art Direction
- **Phong cách đồ họa:** Chibi cute, màu sắc rực rỡ, đường nét mềm mại.
- **Tông màu:** Palette pastel, xanh lá, cam, hồng.
- **Reference:** Động vật hoạt hình, game “Kirby”.
- **Animation style:** Frame‑based sprite animation, particle effects.
- **VFX style:** Sparkles, dust trails, Carrot glow.
- **UI style:** Minimalist, rounded corners, pastel UI.

## 13. Audio Direction
- **Nhạc nền (BGM):** Vui nhộn, tempo tăng khi vào khu vực hiểm.
- **SFX:** Jump, collect Carrot, collect Coin, Spike trap, boss roar, UI clicks.
- **Voice / thoại:** Không có (chỉ text).
- **Feedback âm thanh:** “Ting!” khi thu thập, “Yum!” khi ăn Carrot.

## 14. Narrative / Story
- **Bối cảnh:** Thung lũng xanh, ngôi nhà gỗ của gia đình thỏ.
- **Chủ đề:** Phiêu lưu, trưởng thành, bảo vệ gia đình.
- **Cốt truyện chính:** Thỏ Con lấy lại Cà Rốt Vàng bị Đại Ca đánh cắp.
- **Cốt truyện phụ:** Các ải phụ giúp gia đình thu thập thực phẩm, gặp nhân vật phụ.
- **Lore:** Carrot là “báu vật gia truyền”.
- **Kết thúc:** Hoàn thành 10 cấp, thỏ con trở thành anh hùng.

## 15. Economy / Reward System
- **Currency:** Carrot (mua skin, buff, xem quảng cáo để nhận). Đồng tiền (điểm, bảng xếp hạng).
- **Phần thưởng:** Carrot, đồng tiền, skin, buff.
- **Tỉ lệ drop:** Mỗi ải ít nhất 1 Carrot, 5‑10 đồng tiền.
- **Shop:** Mua skin (cosmetic) & buff (tăng máu, nhảy, xóa mod).
- **Chi tiêu tài nguyên:** Carrot dùng để mua, đồng tiền chỉ để xem bảng xếp hạng.

## 16. AI / Enemy Design
- **Các loại hành vi:** Patrol, Chase, Spike activation on player proximity.
- **Cấp độ thông minh:** Thấp – di chuyển theo waypoint, phản hồi khi chạm.
- **Pattern tấn công:** Spike trap bật lên, enemy tấn công gần.
- **Boss phases:** Đại Ca có 2 pha – chase + spike barrage.

## 17. Mission / Quest System
- **Nhiệm vụ chính:** Hoàn thành 10 cấp, thu thập 10 Carrot.
- **Nhiệm vụ phụ:** Thu thập tối đa đồng tiền, hoàn thành “speed run”.
- **Daily / weekly:** Không hiện tại (có thể bổ sung trong cập nhật).
- **Điều kiện hoàn thành:** Thu thập Carrot, đạt checkpoint.
- **Phần thưởng:** Carrot, skin đặc biệt.

## 18. Technical Requirements
- **Engine:** Unity 2022 LTS.
- **Target FPS:** 60 FPS trên mobile, 120 FPS trên PC.
- **Độ phân giải:** 1080p (mobile), 1920p (PC).
- **Lưu game:** Auto‑save tại checkpoint, data lưu ở PlayerPrefs.
- **Multiplayer:** Không có (solo).
- **Ràng buộc kỹ thuật:** Kích thước bundle < 150 MB, tối ưu cho Android 5.0+.

## 19. Monetization
- **Mô hình:** Free‑to‑play.
- **IAP:** Mua Carrot (paket 100/500/1000), skin premium, buff pack.
- **Ads:** Rewarded video (xem để hồi sinh, nhận Carrot), interstitial (thỉnh thoảng).
- **Battle pass:** Không có (có thể triển khai trong tương lai).
- **Nguyên tắc:** Không phá cân bằng; các IAP chỉ mua cosmetic/buff.

## 20. Production Scope
- **Danh sách tính năng:** Core movement, double jump, coyote time, SpikeTrap, MapData ScriptableObject, mini‑map, 10 cấp, 5 ải/ cấp, Carrot economy, shop, rewarded ads.
- **Ưu tiên tính năng:** Gameplay core → Economy → UI/UX → Ads → Skin.
- **Phạm vi MVP:** Core gameplay + 3 cấp (15 ải) + basic shop.
- **Phạm vi release:** Hoàn thiện 10 cấp, full shop, ads.
- **Những gì không làm ở bản đầu:** Multiplayer, daily quests, battle pass.

## 21. Acceptance Criteria / Định nghĩa hoàn thành
- **Tính năng được xem là xong khi:**
  - Player có thể di chuyển, nhảy đôi, coyote time.
  - 10 cấp, mỗi cấp 5 ải, Carrot và đồng tiền hiển thị.
  - Shop cho phép mua skin và buff bằng Carrot.
  - Quảng cáo reward cho hồi sinh và nhận Carrot.
  - UI hiển thị Carrot, đồng tiền, quảng cáo.
- **Tiêu chí test:** Playtest 20 người, không crash, UI không lỗi.
- **Tiêu chí quality:** FPS ≥ 55 trên Android, độ trễ input ≤ 30 ms.
- **Tiêu chí fun / balance:** Đánh giá fun ≥ 4/5 từ tester.

## 22. Appendices
- **Tài liệu tham chiếu:** Unity Manual, Google Mobile Ads SDK.
- **Diagram:** Flowchart gameplay loop (see `Docs/Flowchart.png`).
- **Bảng chỉ số:** Table of Carrot earn rates (see `Docs/CarrotRates.xlsx`).
- **Danh sách asset:** `Assets/Characters`, `Assets/Environments`, `Assets/UI`.
- **Thuật ngữ:** Carrot (currency), Buff (temporary power‑up), SpikeTrap (hazard), Mini‑map (tutorial map).
