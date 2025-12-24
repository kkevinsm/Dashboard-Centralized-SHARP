# 📝 Instruksi untuk PC Office - Git Pull Update

## ✅ Yang Sudah Dilakukan

Saya sudah menambahkan **`.gitignore`** file yang akan mencegah folder `bin/` dan `obj/` masuk ke Git repository. Ini adalah best practice untuk project .NET.

## 🚀 Cara Update di PC Office

Ketika Anda `git pull` di PC office, **TIDAK PERLU** menghapus folder `bin` dan `obj` lagi!

### Langkah-langkah:

```bash
# 1. Pull update terbaru dari repository
git pull origin main

# 2. Restore dependencies (otomatis generate bin & obj baru)
dotnet restore

# 3. Build project (jika perlu)
dotnet build

# 4. Jalankan aplikasi
dotnet run
```

## 🔧 Setup Database di PC Office

Setelah pull, Anda perlu menjalankan SQL script untuk membuat tabel `shift_configuration`:

```bash
# Pastikan MySQL berjalan, kemudian jalankan:
mysql -u root -h localhost monitoring_centralized < create_shift_configuration_table.sql

# Atau gunakan password jika ada:
mysql -u root -p -h localhost monitoring_centralized < create_shift_configuration_table.sql
```

**Alternatif:** Gunakan phpMyAdmin atau MySQL Workbench:
1. Buka `create_shift_configuration_table.sql`
2. Copy-paste isinya ke SQL console
3. Execute

## 📊 Fitur Baru yang Ditambahkan

### 1. Shift Configuration (Settings Page)
- URL: `/settings`
- Fitur:
  - Edit waktu shift 1, 2, dan 3
  - Save ke database MySQL
  - Reset ke default values
  - Auto-load dari database

### 2. Database Baru
- Tabel: `shift_configuration`
- Tidak mengganggu tabel `centralized_logs` yang sudah ada
- Menyimpan konfigurasi waktu shift

### 3. File Baru
- `Models/ShiftConfiguration.cs` - Model data
- `Services/ShiftConfigurationService.cs` - Business logic
- `Components/Pages/Settings.razor` - UI halaman Settings
- `create_shift_configuration_table.sql` - SQL script
- `DATABASE_SETUP.md` - Dokumentasi teknis
- `SHIFT_CONFIGURATION_GUIDE.md` - Panduan lengkap
- `.gitignore` - Mengabaikan file build

## ⚠️ Penting!

### File yang TIDAK akan ter-push lagi ke Git:
- ✅ `bin/` folder (file compiled)
- ✅ `obj/` folder (file temporary build)
- ✅ `.vs/` folder (Visual Studio cache)
- ✅ `*.user` files (user settings)
- ✅ Build artifacts lainnya

### File yang AKAN ter-push ke Git:
- ✅ Source code (`.cs`, `.razor`, `.json`)
- ✅ SQL scripts (`.sql`)
- ✅ Documentation (`.md`)
- ✅ Configuration files (`appsettings.json`, `.csproj`)
- ✅ Static files (`wwwroot/`)

## 🔍 Troubleshooting

### ❌ Masalah: "Cannot find table shift_configuration"
**Solusi:**
```bash
mysql -u root -h localhost monitoring_centralized < create_shift_configuration_table.sql
```

### ❌ Masalah: "Port already in use"
**Solusi:**
```bash
# Windows
netstat -ano | findstr :5100
taskkill /PID <PID_NUMBER> /F

# macOS/Linux
lsof -ti:5100 | xargs kill -9
```

### ❌ Masalah: Git masih tracking bin/obj
**Solusi:** (Hanya jika diperlukan)
```bash
git rm -r --cached bin obj
git commit -m "Remove bin and obj from tracking"
git push
```

## 📞 Kontak

Jika ada masalah saat setup di PC office, hubungi saya atau cek dokumentasi lengkap di:
- `DATABASE_SETUP.md` - Setup database
- `SHIFT_CONFIGURATION_GUIDE.md` - Panduan fitur shift configuration
- `README.md` - Dokumentasi project

---

**Dibuat tanggal:** 24 Desember 2025
**Commit:** Add shift configuration feature with database storage
