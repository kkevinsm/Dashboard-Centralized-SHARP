# 📋 Panduan Setup Shift Configuration

## ✅ Yang Sudah Selesai

### 1. **Model & Service** (SIAP DIGUNAKAN ✓)
- ✅ `Models/ShiftConfiguration.cs` - Model untuk data shift
- ✅ `Services/ShiftConfigurationService.cs` - Service untuk CRUD shift
- ✅ `Program.cs` - Service sudah didaftarkan di dependency injection

### 2. **Settings Page** (SIAP DIGUNAKAN ✓)
- ✅ `Components/Pages/Settings.razor` - Halaman konfigurasi shift
- ✅ Input waktu untuk 3 shift (Start & End)
- ✅ Tombol Save & Reset
- ✅ Auto-load dari database
- ✅ Pesan sukses/error

### 3. **SQL Script** (PERLU DIJALANKAN ⚠️)
- ✅ `create_shift_configuration_table.sql` - Script untuk membuat tabel
- ⚠️ **BELUM DIJALANKAN** - Perlu jalankan manual

---

## 🚀 Langkah Setup Database (WAJIB!)

### Step 1: Pastikan MySQL Berjalan

**Untuk XAMPP:**
```bash
# Buka XAMPP Control Panel
# Start MySQL service
```

**Untuk Homebrew MySQL:**
```bash
brew services start mysql
```

**Untuk MAMP:**
```bash
# Buka MAMP Control Panel
# Start Servers
```

### Step 2: Jalankan SQL Script

**Pilih salah satu cara berikut:**

#### Cara A: Menggunakan Terminal (Tercepat)
```bash
# Masuk ke folder project
cd /Users/mac/Documents/Codes/Dashboard-Centralized-SHARP

# Jalankan script SQL
mysql -u root -h localhost monitoring_centralized < create_shift_configuration_table.sql
```

#### Cara B: Menggunakan phpMyAdmin
1. Buka phpMyAdmin (http://localhost/phpmyadmin)
2. Pilih database `monitoring_centralized`
3. Klik tab **SQL**
4. Copy-paste isi file `create_shift_configuration_table.sql`
5. Klik **Go**

#### Cara C: Menggunakan MySQL Workbench
1. Buka MySQL Workbench
2. Connect ke localhost
3. Pilih database `monitoring_centralized`
4. File → Open SQL Script → Pilih `create_shift_configuration_table.sql`
5. Execute (⚡ icon atau Ctrl+Shift+Enter)

### Step 3: Verifikasi Tabel Berhasil Dibuat

```bash
mysql -u root -h localhost -e "USE monitoring_centralized; SHOW TABLES LIKE 'shift_configuration';"
```

**Expected output:**
```
+----------------------------------------------+
| Tables_in_monitoring_centralized (shift_configuration) |
+----------------------------------------------+
| shift_configuration                          |
+----------------------------------------------+
```

### Step 4: Cek Data Default

```bash
mysql -u root -h localhost -e "USE monitoring_centralized; SELECT * FROM shift_configuration;"
```

**Expected output:**
```
+----+------------+------------+----------+---------------------+
| id | shift_name | start_time | end_time | updated_at          |
+----+------------+------------+----------+---------------------+
|  1 | Shift 1    | 07:56:00   | 17:00:00 | 2025-12-24 XX:XX:XX |
|  2 | Shift 2    | 17:01:00   | 00:25:00 | 2025-12-24 XX:XX:XX |
|  3 | Shift 3    | 00:26:00   | 07:55:00 | 2025-12-24 XX:XX:XX |
+----+------------+------------+----------+---------------------+
```

✅ **Jika melihat output seperti di atas, setup BERHASIL!**

---

## 🎯 Cara Menggunakan

### 1. Jalankan Aplikasi
```bash
dotnet run
```

### 2. Buka Settings Page
- Navigasi ke sidebar → klik **Settings** ⚙️
- Atau akses langsung: `https://localhost:XXXX/settings`

### 3. Edit Waktu Shift
- Ubah waktu Start/End untuk setiap shift
- Gunakan format 24 jam (contoh: 07:56, 17:00)

### 4. Simpan Perubahan
- Klik tombol **"Save Changes"**
- Akan muncul pesan sukses berwarna hijau
- Data tersimpan di database `shift_configuration`

### 5. Reset ke Default (Opsional)
- Klik tombol **"Reset to Default"**
- Waktu akan kembali ke nilai default
- **Catatan:** Perlu klik "Save Changes" untuk menyimpan reset

---

## 📊 Struktur Tabel Database

### Tabel: `shift_configuration`

| Kolom       | Tipe         | Deskripsi                              |
|-------------|--------------|----------------------------------------|
| `id`        | INT          | Primary key, auto increment            |
| `shift_name`| VARCHAR(50)  | Nama shift (contoh: "Shift 1")         |
| `start_time`| TIME         | Waktu mulai (format HH:mm:ss)          |
| `end_time`  | TIME         | Waktu selesai (format HH:mm:ss)        |
| `updated_at`| DATETIME     | Waktu terakhir diupdate                |

**Index:** `idx_shift_name` untuk performa query cepat

---

## 🔍 Troubleshooting

### ❌ Error: "Can't connect to MySQL server"
**Solusi:**
```bash
# Cek status MySQL
ps aux | grep mysql

# Jika tidak berjalan, start MySQL:
# - XAMPP: Start dari control panel
# - Homebrew: brew services start mysql
# - MAMP: Start dari control panel
```

### ❌ Error: "Table 'shift_configuration' doesn't exist"
**Solusi:**
```bash
# Jalankan SQL script lagi
mysql -u root -h localhost monitoring_centralized < create_shift_configuration_table.sql
```

### ❌ Error: "Access denied for user 'root'@'localhost'"
**Solusi:**
```bash
# Tambahkan password jika ada:
mysql -u root -p -h localhost monitoring_centralized < create_shift_configuration_table.sql
# Enter password when prompted
```

### ⚠️ Warning saat Build
```
warning CS0414: The field 'Settings.isLoading' is assigned but its value is never used
```
**Status:** Aman diabaikan (variabel untuk future loading indicator)

---

## 🎨 Fitur yang Sudah Ada

✅ **Auto-load dari database** saat buka Settings page  
✅ **Validasi input** dengan HTML5 time picker  
✅ **Save ke database** MySQL  
✅ **Reset to default** dengan 1 klik  
✅ **Success/Error message** dengan auto-hide (3 detik)  
✅ **Responsive UI** untuk mobile & desktop  
✅ **Information card** dengan panduan penggunaan  

---

## 🔮 Fitur Selanjutnya (Opsional)

### 1. Integrasi dengan Home Dashboard
Modifikasi `Home.razor` untuk menggunakan shift times dari database:
```csharp
// Ganti hardcoded times dengan data dari ShiftConfigurationService
@inject ShiftConfigurationService ShiftService
```

### 2. Loading Indicator
Tampilkan spinner saat load/save data:
```html
@if (isLoading)
{
    <div class="spinner-border text-primary"></div>
}
```

### 3. Validasi Waktu
Cek overlap antar shift atau gap waktu.

---

## 📝 Catatan Penting

⚠️ **Tabel Terpisah dari `centralized_logs`**
- Tidak akan mengganggu data monitoring existing
- Database sama: `monitoring_centralized`
- Tabel baru: `shift_configuration`

✅ **Data Persisten**
- Data tersimpan di MySQL, bukan browser storage
- Tidak akan hilang saat clear cache/cookies
- Bisa diakses dari device manapun

🔄 **Auto-Initialize**
- Jika tabel kosong, sistem akan otomatis insert default values
- Terjadi saat pertama kali buka Settings page

---

## 📞 Pertanyaan?

Jika ada masalah atau pertanyaan:
1. Cek section **Troubleshooting** di atas
2. Pastikan MySQL server berjalan
3. Verifikasi tabel sudah dibuat dengan query di Step 3 & 4
4. Cek log error di terminal saat `dotnet run`

**Selamat menggunakan fitur Shift Configuration! 🎉**
