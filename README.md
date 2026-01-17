# TR
<sup> See below for English ⬇️ </sup>
## 🎮 Matematik Arenası
Matematik Arenası, Unity ile geliştirilmiş, tarayıcı üzerinden oynanabilen eğlenceli ve eğitici bir matematik oyunudur.

🚀 **Hemen Oyna:** [oyundersligi.com](https://oyundersligi.com/)

## 🛠️ Kurulum ve Geliştirme
Bu proje WebGL platformu için **_DOKUNMATİK EKRANI_** olan cihazlara yönelik optimize edilmiştir.

## ⚖️ Lisans (License)
Bu proje **CC BY-NC 4.0 (Creative Commons Attribution-NonCommercial 4.0 International)** lisansı ile korunmaktadır.

- ✅ **Geliştirebilirsiniz:** Kodu inceleyebilir, fork'layabilir ve kendi bireysel projeleriniz için geliştirebilirsiniz.
- ✅ **Atıf Yapmalısınız:** Kullanılan kodlarda veya paylaşımlarda kaynak göstermek zorunludur.
- ❌ **Ticari Kullanım Yasaktır:** Bu proje veya türevleri üzerinden herhangi bir maddi kazanç elde edilemez, reklam içeren platformlarda izinsiz yayınlanamaz.

## 🎮 **Oyun Mekanikleri:**
Matematik Arenası, yerel çok oyunculu (local multiplayer) ve turnuva mantığıyla çalışan bir rekabet platformudur. Oyunun akışı şu şekildedir:

**Esnek Yapılandırma:** Ana menüden oyuncu sayısı, round başına soru sayısı, sayıların alt/üst limitleri ve hangi işlemlerin (toplama, çıkarma vb.) sorulacağı tamamen özelleştirilebilir.

**Lobi ve Turnuva:** Lobi ekranında oyuncular isimlerini seçer. Sistem otomatik olarak eşleştirme yapar ve turnuva ağacını oluşturur.

**Soru-Cevap Mantığı:** Bir oyuncu doğru cevap verirse puanı alır ve yeni soruya geçilir.

Yanlış cevap veren oyuncunun butonları *kilitlenir,* diğer oyuncunun cevabı beklenir.
İki oyuncu da yanlış bilirse kimse puan alamaz ve soru atlanır.

**Altın Soru (Eşitlik Bozmaca):** Round sonunda puanlar eşitse, sistem "Altın Soru" sorar. İlk bilen kazanır ve bir üst tura yükselir.

## 🛠 **_Teknik Detay ve Kullanılan Teknolojiler_**
Oyun, dokunmatik ekran öncelikli (Touch-first) bir UI mimarisine sahiptir ve tamamen dinamik bir "Tournament Manager" algoritması ile yönetilmektedir.
* **Unity 6 (6000.0.5f2):** Oyun motoru ve geliştirme ortamı.
* **Zenject / Extenject:** Dependency Injection (Bağımlılık Enjeksiyonu) mimarisi.
* **DOTween (Demigiant):** Programatik animasyon ve geçiş yönetimi.
* **Unity WebGL:** Tarayıcı tabanlı responsive yayınlama.
* **WordPress & ACF:** Dinamik veri ve içerik yönetimi.
  
---

# EN
## 🎮 Math Arena

Math Arena is an engaging and educational math game developed with Unity, playable directly in your browser.

🚀 **Play Now:** [oyundersligi.com](https://oyundersligi.com/)

## 🛠️ Installation and Development
This project has been optimized for devices with a **_TOUCHSCREEN_** for the WebGL platform.

## ⚖️ License
This project is protected under the **CC BY-NC 4.0 (Creative Commons Attribution-NonCommercial 4.0 International)** license.

- ✅ **Development:** You are free to examine, fork, and develop the code for your personal projects.
- ✅ **Attribution:** Providing credit to the original author in any shared work or derivatives is mandatory.
- ❌ **No Commercial Use:** This project or its derivatives may not be used for any commercial advantage or monetary compensation. Publishing on ad-supported platforms without permission is strictly prohibited.
  
## 🎮 **Gameplay Mechanics**
Math Arena is a competitive platform based on local multiplayer and tournament logic. The game flow consists of:

**Flexible Configuration:** Player count, questions per round, number ranges (min/max), and types of operations (addition, subtraction, etc.) are fully customizable from the main menu.

**Lobby & Tournament System:** Optimized for touch devices, the lobby allows players to select their names. The system automatically matches players and generates a tournament bracket.

**Core Mechanics:**  If a player answers correctly, they score and move to the next question.

The buttons of the player who gives the wrong answer are *locked,* and the other player's answer is awaited.
If both players answer incorrectly, no points are awarded, and a new question appears.

**Golden Question (Tie-Breaker):** If there is a tie at the end of the round, the system triggers a "Golden Question." The first player to answer correctly wins and advances to the next round.

## 🛠 **_Technical Details and Technologies Used_**
The game features a touch-first UI architecture and is managed by a fully dynamic "Tournament Manager" algorithm.
* **Unity 6 (6000.0.5f2):** Game engine and development environment.
* **Zenject / Extenject:** Dependency Injection (DI) architecture.
* **DOTween (Demigiant):** Programmatic animation and transition management.
* **Unity WebGL:** Browser-based responsive publishing.
* **WordPress & ACF:** Dynamic data and content management.
