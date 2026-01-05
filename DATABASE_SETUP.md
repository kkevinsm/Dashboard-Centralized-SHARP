# Database Setup untuk Shift Configuration

## Langkah-langkah Setup:

### 1. Pastikan MySQL Server Berjalan
```bash
# Untuk macOS dengan Homebrew:
brew services start mysql

# Atau jika menggunakan XAMPP/MAMP, start MySQL dari control panel
```

### 2. Buat Tabel shift_configuration

Ada 2 cara untuk membuat tabel:

#### Cara 1: Menggunakan SQL File (Otomatis)
```bash
mysql -u root -h localhost monitoring_centralized < create_shift_configuration_table.sql
```

#### Cara 2: Manual melalui MySQL Console
```bash
# Login ke MySQL
mysql -u root -h localhost

# Pilih database
USE monitoring_centralized;

# Copy-paste query berikut:
CREATE TABLE IF NOT EXISTS shift_configuration (
    id INT AUTO_INCREMENT PRIMARY KEY,
    shift_name VARCHAR(50) NOT NULL UNIQUE,
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_shift_name (shift_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO shift_configuration (shift_name, start_time, end_time, updated_at) 
VALUES 
    ('Shift 1', '07:56:00', '17:00:00', NOW()),
    ('Shift 2', '17:01:00', '00:25:00', NOW()),
    ('Shift 3', '00:26:00', '07:55:00', NOW())
ON DUPLICATE KEY UPDATE 
    start_time = VALUES(start_time),
    end_time = VALUES(end_time),
    updated_at = NOW();
```

### 3. Verifikasi Tabel Berhasil Dibuat
```bash
mysql -u root -h localhost -e "USE monitoring_centralized; DESCRIBE shift_configuration;"
```

Output yang diharapkan:
```
+-------------+-------------+------+-----+-------------------+-------------------+
| Field       | Type        | Null | Key | Default           | Extra             |
+-------------+-------------+------+-----+-------------------+-------------------+
| id          | int         | NO   | PRI | NULL              | auto_increment    |
| shift_name  | varchar(50) | NO   | UNI | NULL              |                   |
| start_time  | time        | NO   |     | NULL              |                   |
| end_time    | time        | NO   |     | NULL              |                   |
| updated_at  | datetime    | YES  |     | CURRENT_TIMESTAMP | DEFAULT_GENERATED |
+-------------+-------------+------+-----+-------------------+-------------------+
```

### 4. Cek Data Default
```bash
mysql -u root -h localhost -e "USE monitoring_centralized; SELECT * FROM shift_configuration;"
```

Output yang diharapkan:
```
+----+------------+------------+----------+---------------------+
| id | shift_name | start_time | end_time | updated_at          |
+----+------------+------------+----------+---------------------+
|  1 | Shift 1    | 07:56:00   | 17:00:00 | 2025-12-24 ...      |
|  2 | Shift 2    | 17:01:00   | 00:25:00 | 2025-12-24 ...      |
|  3 | Shift 3    | 00:26:00   | 07:55:00 | 2025-12-24 ...      |
+----+------------+------------+----------+---------------------+
```

## Struktur Tabel

**shift_configuration** - Menyimpan konfigurasi waktu shift

| Column     | Type         | Description                           |
|------------|--------------|---------------------------------------|
| id         | INT          | Primary key, auto increment           |
| shift_name | VARCHAR(50)  | Nama shift (contoh: "Shift 1")        |
| start_time | TIME         | Waktu mulai shift (format HH:mm:ss)   |
| end_time   | TIME         | Waktu selesai shift (format HH:mm:ss) |
| updated_at | DATETIME     | Waktu terakhir diupdate               |

## Fitur yang Telah Ditambahkan

### 1. Model: ShiftConfiguration.cs
- Model untuk merepresentasikan data shift configuration
- Properties: Id, ShiftName, StartTime, EndTime, UpdatedAt

### 2. Service: ShiftConfigurationService.cs
- `GetAllShifts()` - Ambil semua konfigurasi shift
- `GetShiftByName()` - Ambil shift berdasarkan nama
- `SaveShiftConfiguration()` - Simpan/update konfigurasi shift
- `InitializeDefaultShifts()` - Inisialisasi shift default jika tabel kosong

### 3. Settings Page (Settings.razor)
- UI untuk mengatur waktu shift
- Auto-load dari database saat halaman dibuka
- Tombol Save untuk menyimpan ke database
- Tombol Reset untuk kembali ke default
- Pesan sukses/error dengan auto-hide

## Catatan Penting

⚠️ **Tabel ini TERPISAH dari `centralized_logs`**
- Tidak akan mengganggu tabel existing
- Menggunakan tabel baru: `shift_configuration`
- Koneksi database sama: `monitoring_centralized`

✅ **Fitur Auto-Initialize**
- Saat pertama kali buka Settings page, sistem akan otomatis membuat data default jika tabel kosong
- Tidak perlu insert manual jika sudah menjalankan SQL script

🔄 **Integrasi dengan Home Dashboard (Opsional)**
- Untuk menggunakan shift configuration di Home.razor, perlu modifikasi method `GetShiftByTime()`
- Akan diimplementasikan di tahap berikutnya jika diperlukan
