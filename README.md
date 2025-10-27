# 📞 SignalWire IVR (.NET 8 + cXML)

An interactive voice response (IVR) system built with **.NET 8 WebAPI** and **SignalWire cXML** scripts.  
This project demonstrates how to manage multi-level voice menus, collect DTMF input, and navigate between menus using simple XML-based responses.

---

## 🧩 Overview

This IVR app provides:
- A **main menu** with 3 options  
- **Nested submenus** for each option  
- A **“back” navigation** (press 9) to return to the previous menu  
- Stateless XML-based routing compatible with SignalWire cXML

The system responds to SignalWire webhook requests and generates cXML responses using `<Response>`, `<Say>`, `<Gather>`, and `<Redirect>` tags.

---

## ⚙️ Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SignalWire Account](https://signalwire.com)
- [ngrok](https://ngrok.com) (for local testing)

---

## 🚀 Running the Project

1) **Restore and run**
```bash
dotnet restore
dotnet run
```

2) **Expose your local server**
```bash
ngrok http 5000
```
Copy the generated HTTPS URL (for example, `https://abc123.ngrok-free.app`).

3) **Default local URL**
```
http://localhost:5000
```

---

## 📞 SignalWire Configuration

1. In your **SignalWire Dashboard**, create a **cXML Script**  
   - **Handle Using:** External URL  
   - **Primary Script URL:** `https://YOUR-NGROK-URL/voice/incoming`  
   - **Method:** `POST`  
   - Save.

2. Go to **Phone Numbers → Your Number**  
   - Under **Inbound Call Settings**, click **Assign Resource**  
   - Choose **Script** and select the cXML script you created  
   - Save.

All inbound calls to your SignalWire number will now be routed to your IVR.

---

## 🧠 How It Works

- SignalWire sends an HTTP **POST** to `/voice/incoming` when a call starts.  
- The app responds with cXML like:
```xml
<Response>
  <Say>Welcome to the main menu</Say>
  <Gather numDigits="1" action="/voice/handle-main" method="POST"/>
</Response>
```
- The caller presses 1–3 to access submenus, or 9 to go back.  
- Each submenu uses `<Redirect>` to move between menus.

---

## 🧪 Testing

1. Run the app locally (`dotnet run`)  
2. Start ngrok (`ngrok http 5000`)  
3. Call your SignalWire number  
4. Navigate with DTMF: 1/2/3 for options, 9 to go back

---

## 🧡 Author

Developed by **Elmer Chacón**  
