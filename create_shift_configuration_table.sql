-- SQL Script untuk membuat tabel shift_configuration
-- Jalankan script ini di MySQL database Anda

CREATE TABLE IF NOT EXISTS shift_configuration (
    id INT AUTO_INCREMENT PRIMARY KEY,
    shift_name VARCHAR(50) NOT NULL UNIQUE,
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX idx_shift_name (shift_name)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Insert default shift configurations
INSERT INTO shift_configuration (shift_name, start_time, end_time, updated_at) 
VALUES 
    ('Shift 1', '07:56:00', '17:00:00', NOW()),
    ('Shift 2', '17:01:00', '00:25:00', NOW()),
    ('Shift 3', '00:26:00', '07:55:00', NOW())
ON DUPLICATE KEY UPDATE 
    start_time = VALUES(start_time),
    end_time = VALUES(end_time),
    updated_at = NOW();
