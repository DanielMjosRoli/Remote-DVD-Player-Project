# 📀 Local DVD Streaming System (iSCSI-based)

A self-hosted, local-network DVD playback system that exposes **1:1 encrypted DVD images** as virtual DVD-ROM devices using **iSCSI**, allowing playback through **licensed DVD players on the client machine**.

This project is intentionally designed to **avoid DRM circumvention** by ensuring that:
- DVDs are stored as **raw, encrypted sector copies**
- Decryption happens **only inside licensed DVD players**
- The server never decrypts or processes protected content

Think of this system as a **remote DVD changer over LAN**, not a traditional streaming service.

---

## ✨ Features

- 📁 Store DVDs as raw, encrypted 1:1 images on a server
- 🔌 Expose DVD images as virtual DVD-ROM drives via iSCSI
- 🎬 Playback using licensed DVD players (PowerDVD, WinDVD, etc.)
- 🧠 Centralized metadata and session control
- 🖥️ Web UI for browsing and initiating playback
- 🔐 LAN-only, authenticated access (CHAP)
- 📺 Optional TV playback via screencapture / casting

---

## ⚠️ Legal & Design Principles (Important)

This project is designed with EU copyright law in mind.

### What this system DOES:
- Copies DVDs **bit-for-bit** without decryption
- Preserves all DRM (CSS) exactly as on the original disc
- Uses licensed players to perform decryption at playback time
- Operates strictly on a **private local network**

### What this system DOES NOT do:
- ❌ Does not decrypt DVDs on the server
- ❌ Does not extract CSS keys
- ❌ Does not convert DVDs to VIDEO_TS / MKV / MP4
- ❌ Does not stream decrypted video directly
- ❌ Does not expose content over the public internet

If you are unsure about legality in your jurisdiction, consult a legal professional.

---

## 🧠 High-Level Architecture

```
+-------------------+        iSCSI        +---------------------+
|                   | <----------------> |                     |
|   Linux Server    |                    |   Client Machine    |
|                   |                    |                     |
| - Raw DVD images  |                    | - iSCSI Initiator   |
| - iSCSI Target    |                    | - Licensed Player   |
| - .NET Backend    |                    | - Optional Capture  |
| - PostgreSQL      |                    |                     |
+-------------------+                    +---------------------+
          ^
          |
          | REST API
          |
+-------------------+
|   React Frontend  |
+-------------------+
```

---

## 🧱 Technology Stack

### Server
- **OS:** Linux (Ubuntu LTS recommended)
- **iSCSI Target:** LIO (`targetcli`)
- **Backend:** .NET 8 (ASP.NET Core)
- **Database:** PostgreSQL
- **Storage:** Raw DVD images (`.raw`)

### Client
- **OS:** Windows (recommended)
- **iSCSI Initiator:** Microsoft iSCSI Initiator
- **DVD Player:** PowerDVD / WinDVD / Windows DVD Player
- **Optional:** OBS / FFmpeg for TV streaming

### Frontend
- React
- TypeScript
- TanStack Query
- Tailwind / MUI (optional)

---

## 📂 Project Structure

```
dvd-streaming/
├── backend/
├── frontend/
├── scripts/
├── docs/
└── README.md
```

---

## 📀 Importing a DVD (Raw, Encrypted Copy)

```bash
sudo dd if=/dev/sr0 of=/data/dvds/movie_name.raw bs=2048 status=progress
```

---

## 🔌 Exposing a DVD via iSCSI

```bash
/backstores/fileio create name=movie1 file_or_dev=/data/dvds/movie1.raw read_only=1
/iscsi create iqn.2026-02.local:dvd.movie1
/iscsi/iqn.2026-02.local:dvd.movie1/tpg1/luns create /backstores/fileio/movie1
```

---

## ▶️ Playback Flow

1. User selects a movie in the web UI
2. Backend creates or assigns an iSCSI target
3. Client connects via iSCSI initiator
4. Virtual DVD-ROM appears on client OS
5. Licensed DVD player opens the disc

---

## 🗺️ Roadmap

- [ ] Backend API
- [ ] Frontend UI
- [ ] Client companion tool
- [ ] TV streaming support

---

## ❤️ Philosophy

> This system does not try to break DRM.  
> It treats DRM as a constraint and designs *around* it.
