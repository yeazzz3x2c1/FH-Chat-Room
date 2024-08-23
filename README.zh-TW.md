# FH-Chat Room

[English](README.md) | [繁體中文](README.zh-TW.md)

# FH-Chat Room

FH-Chat Room 是一個教育專案，旨在帶領學生一步一步實現基於TCP Socket的即時通訊軟體。本教材的主要目的是幫助學生通過構建支援文字訊息傳輸與檔案傳輸的簡單聊天應用程式來學習並應用Socket程式設計概念。

## 開發語言

- **伺服器 (Server):** C#
- **客戶端 (Client):** C# 與 WPF

## 作者的話

這個專案開源的目的是為了提供對即時通訊軟體開發感興趣的人一個參考資源。在 `codes_records` 資料夾中，我們提供了每次課程的專案進度紀錄，這對於想了解聊天系統開發流程的人是一個額外的參考資料。

本專案使用 MIT 授權，允許商業用途。但請注意，這個專案是在以做中學的教學方式中開發的，因此在通訊協議的設計上以簡單易懂為主，並未加入加密通訊等進階安全措施。若要將本專案應用於商業開發，請務必確保通訊的安全性與保密性，並適時修改程式碼。

## 專案結構

### 伺服器 (Server)
- 伺服器端的原始碼位於 `Server_Application` 資料夾中。
- 啟動伺服器：
  - 使用 Visual Studio 開啟專案並直接執行。
  - 或者執行已編譯好的Windows執行檔，路徑位於 `Server_Application\Server_Application\bin\Debug\net6.0\Server.exe`。

### 客戶端 (Client)
- 客戶端的原始碼位於 `Client_Application` 資料夾中。
- 啟動客戶端：
  - 使用 Visual Studio 開啟專案並直接執行。
  - 或者執行已編譯好的Windows執行檔，路徑位於 `Client_Application\Client_Application\bin\Debug\net6.0-windows\Client_Application.exe`。

### 測試流程
1. 啟動伺服器。
2. 開啟一個或多個客戶端。
3. 在客戶端輸入使用者暱稱。
4. 開始聊天或分享檔案。
5. Enjoy!

## 自訂設定

- **伺服器端口:** 修改 `Server_Application\Server_Application\Server.cs` 第14行的 `Port` 變數來變更伺服器端口。
- **客戶端連線位址:** 修改 `Client_Application\Client_Application\Server.cs` 第42行的 `Server.Connect` 方法參數來變更客戶端連線位址。修改後重新編譯專案即可。

## 授權條款

本專案採用 MIT 授權條款。詳細資訊請參閱 [LICENSE](LICENSE) 文件。

## 聯絡方式

**作者:** Feng-Hao, Yeh  
**Email:** zzz3x2c1@gmail.com
